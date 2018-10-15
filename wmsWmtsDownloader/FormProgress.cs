using ArcGIS.Core.Internal.CIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace wmsWmtsDownloader
{
    public partial class FormProgress : Form
    {
        LayerInfo layerInfo;
        ManualResetEvent manualResetEvent;
        private bool canRun = true;
        XmlDocument xmlDocumentCapabilities;
        public FormProgress()
        {
            layerInfo = Module1.layerInfo;
            xmlDocumentCapabilities = new XmlDocument();
            xmlDocumentCapabilities = layerInfo.xmlDocumentCapabilities;
            manualResetEvent = new ManualResetEvent(false);
            InitializeComponent();
            InitializeBackgroundWorker();
            this.backgroundWorkerDownload.RunWorkerAsync();
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorkerDownload.DoWork += new DoWorkEventHandler(backgroundWorkerDownload_DoWork);
            backgroundWorkerDownload.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerDownload_ProgressChanged);
            backgroundWorkerDownload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerDownload_RunWorkerCompleted);
            
        }
        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorkerDownload = sender as BackgroundWorker;
            if (backgroundWorkerDownload.CancellationPending)
            {
                e.Cancel = true;
            }
            downloadWizard(backgroundWorkerDownload, e);
            if (backgroundWorkerDownload.CancellationPending)
            {
                e.Cancel = true;
            }
            backgroundWorkerDownload.ReportProgress(100);

        }
        private void downloadWizard(BackgroundWorker backgroundWorker, DoWorkEventArgs e)
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName));
            int tileSize = Convert.ToInt32(Module1.formData.tileSize.Split('x')[0]);
            string tileExtension="";
            string fileGeoreferencedExtension="";
            string format = "";
            switch (Module1.formData.saveFileDialog.FilterIndex)
            {
                case 1:
                    tileExtension = ".tif";
                    fileGeoreferencedExtension = ".tfw";
                    format = "image/tiff";
                    break;
                case 2:
                    tileExtension = ".png";
                    fileGeoreferencedExtension = ".pgw";
                    format = "image/png";
                    break;
                case 3:
                    tileExtension = ".jpg";
                    fileGeoreferencedExtension = ".jgw";
                    format = "image/png";
                    break;
            }
            int numberOfFilesToCreate;
            int numberOfFilesCreated=0;
            switch (layerInfo.serverType)
            {
                case "WMS":
                    int numberOfTilesHorizontally = (int)Math.Ceiling(Module1.formData.OutputImageSizeX / tileSize);
                    int numberOfTilesVertically = (int)Math.Ceiling(Module1.formData.OutputImageSizeY / tileSize);
                    double distanceXminXmax = Math.Abs((25.4 * tileSize * Module1.formData.scale) / (Module1.formData.dpi * 1000));
                    double distanceYminYmax = Math.Abs((25.4 * tileSize * Module1.formData.scale) / (Module1.formData.dpi * 1000));
                    double xmin = Module1.formData.coordinateTopX;
                    double xmax = Module1.formData.coordinateTopX + distanceXminXmax;
                    double ymin = Module1.formData.coordinateTopY - distanceYminYmax;
                    double ymax = Module1.formData.coordinateTopY;
                    numberOfFilesToCreate = numberOfTilesHorizontally * numberOfTilesVertically;
                    if (Module1.formData.saveGeoreference) numberOfFilesToCreate = numberOfFilesToCreate * 2;
                    int tileSizeX = tileSize;
                    int tileSizeY = tileSize;
                    for (int i = 0; i < numberOfTilesVertically; i++)
                    {
                        if (i == numberOfTilesVertically - 1)
                        {
                            ymin = Module1.formData.coordinateBottomY;
                            tileSizeY = (int)Module1.formData.OutputImageSizeY % tileSize;
                        }
                        for (int j = 0; j < numberOfTilesHorizontally; j++)
                        {
                            if (j == numberOfTilesHorizontally - 1)
                            {
                                xmax = Module1.formData.coordinateBottomX;
                                tileSizeX = (int)Module1.formData.OutputImageSizeX % tileSize;
                            }
                            else tileSizeX = tileSize;
                            if (backgroundWorkerDownload.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {
                                if (Module1.formData.saveGeoreference)
                                {
                                    string fileGeoreferencedName = System.IO.Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "__x=" + xmin + "_y=" + ymax + fileGeoreferencedExtension;
                                    double scaleDivide = Module1.formData.scale / 1000;
                                    double pxY = (((Math.Abs(ymin - ymax) / scaleDivide) * Module1.formData.dpi)) / (25.4);
                                    double yscale = (ymin - ymax) / pxY;
                                    double pxX = (((Math.Abs(xmax - xmin) / scaleDivide) * Module1.formData.dpi)) / (25.4);
                                    double xscale = (xmax - xmin) / pxX;
                                    if (!canRun) manualResetEvent.WaitOne();
                                    createGeoreferenced(fileGeoreferencedName, xmin, ymax, xscale, yscale);
                                    numberOfFilesCreated++;
                                    if (!canRun) manualResetEvent.WaitOne();
                                    calculateProgress(numberOfFilesToCreate, numberOfFilesCreated);
                                }
                                // String address = "http://sdi.gdos.gov.pl/wms?TRANSPARENT=TRUE&FORMAT=image/png&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=&LAYERS=ParkiKrajobrazowe&SRS=EPSG:2180&BBOX=";
                                String address = layerInfo.serverURL + "REQUEST=GetMap&version=1.1.1&service=WMS&format="+format+"&STYLES=&LAYERS=" + layerInfo.layerName + "&SRS="+Module1.formData.epsg+"&BBOX=";
                                address += xmin.ToString().Replace(',', '.') + ","
                                    + ymin.ToString().Replace(',', '.') + ","
                                    + xmax.ToString().Replace(',', '.') + ","
                                    + ymax.ToString().Replace(',', '.') + "&"
                                    + "width=" + tileSizeX.ToString()
                                    + "&height=" + tileSizeY.ToString(); ;

                                
                                string fileName = System.IO.Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "__x=" + xmin + "_y=" + ymax + tileExtension;
                                int attemptToDdownloadTile = 0;
                            tryDownloadingAgain:
                                if (backgroundWorkerDownload.CancellationPending) break;
                                try
                                {
                                    attemptToDdownloadTile++;
                                    using (WebClient client = new WebClient())
                                    {
                                        if (!canRun) manualResetEvent.WaitOne();
                                        client.DownloadFile(address, fileName);
                                        numberOfFilesCreated++;
                                        if (!canRun)
                                        {
                                            manualResetEvent.WaitOne();
                                        }
                                        calculateProgress(numberOfFilesToCreate, numberOfFilesCreated);
                                    }
                                    xmin = xmax;
                                    xmax += distanceXminXmax;
                                }
                                catch
                                {
                                    if (attemptToDdownloadTile <= 5) goto tryDownloadingAgain;
                                }
                            }
                        }

                        if (backgroundWorkerDownload.CancellationPending)
                        {
                            i = (int)numberOfTilesVertically;
                            break;
                        }
                        xmin = Module1.formData.coordinateTopX;
                        xmax = Module1.formData.coordinateTopX + distanceXminXmax;
                        ymax = ymin;
                        ymin -= distanceYminYmax;
                    }
                    break;
                case "WMTS":
                    int indexDenominatorScale = getNearestScaleIndex();
                    MyPoint topLeftCornerFromXML = getTopLeftCorner(indexDenominatorScale);
                    double WMTSResolution = getWMTSResolution(Module1.formData.scale);
                    double WMTSTileLength = WMTSResolution * tileSize;

                    double firstColumn = Math.Floor((Math.Min(Module1.formData.coordinateTopX, Module1.formData.coordinateTopX) - topLeftCornerFromXML.y) / WMTSTileLength);
                    double firstRow = Math.Floor((topLeftCornerFromXML.x - Math.Max(Module1.formData.coordinateTopY, Module1.formData.coordinateTopY)) / WMTSTileLength);
                    double lastColumn = Math.Floor((Math.Max(Module1.formData.coordinateBottomX, Module1.formData.coordinateBottomX) - topLeftCornerFromXML.y) / WMTSTileLength);
                    double lastRow = Math.Floor((topLeftCornerFromXML.x- Math.Min(Module1.formData.coordinateBottomY, Module1.formData.coordinateBottomY)) / WMTSTileLength);
                    numberOfFilesToCreate = Convert.ToInt32((lastColumn - firstColumn + 1) * (lastRow - firstRow + 1));
                    if (Module1.formData.saveGeoreference) numberOfFilesToCreate = numberOfFilesToCreate * 2;

                    if (backgroundWorkerDownload.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        for (var y = firstRow; y <= lastRow; y++)
                        {
                            for (var x = firstColumn; x <= lastColumn; x++)
                            {
                                if (Module1.formData.saveGeoreference)
                                {
                                    double dpi = 90.71428571429;
                                    string fileGeoreferencedName = System.IO.Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "__x=" + x + "_y=" + y + fileGeoreferencedExtension;
                                    double scaleDivide = Module1.formData.scale * 1.0 / 1000.000000;
                                    double pxY = (((Math.Abs((topLeftCornerFromXML.x - (y + 1) * WMTSTileLength) - (topLeftCornerFromXML.x - y * WMTSTileLength)) / scaleDivide) * dpi * 1.0)) / (25.4);
                                    double yscale = ((topLeftCornerFromXML.x - (y + 1) * WMTSTileLength) - (topLeftCornerFromXML.x - y * WMTSTileLength)) / pxY;
                                    double pxX = (((((Math.Abs((x + 1) * WMTSTileLength + topLeftCornerFromXML.y)) - (Math.Abs(x * WMTSTileLength + topLeftCornerFromXML.y))) / scaleDivide) * dpi * 1.0)) / (25.4);
                                    double xscale = ((Math.Abs((x + 1) * WMTSTileLength + topLeftCornerFromXML.y)) - (Math.Abs(x * WMTSTileLength + topLeftCornerFromXML.y))) / pxX;
                                    if (!canRun) manualResetEvent.WaitOne();
                                    double xMinWMTS = (x * 1.0 * WMTSTileLength + topLeftCornerFromXML.y);
                                    double yMaxWMTS = (topLeftCornerFromXML.x - y * 1.0 * WMTSTileLength);
                                    createGeoreferenced(fileGeoreferencedName, xMinWMTS, yMaxWMTS, xscale, yscale);
                                    numberOfFilesCreated++;
                                    if (!canRun) manualResetEvent.WaitOne();
                                    calculateProgress(numberOfFilesToCreate, numberOfFilesCreated);
                                }
                                string request = layerInfo.serverURL + "SERVICE=WMTS&REQUEST=GetTile&VERSION=" + layerInfo.serverVersion + "&Layer=" + layerInfo.layerName + "&Style=&Format=" + format + "&TileMatrixSet=" + Module1.formData.epsg + "&TileMatrix=" + Module1.formData.epsg + ":" + indexDenominatorScale + "&TileRow=" + y + "&TileCol=" + x;
                               
                                string fileName = System.IO.Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "__x=" + x + "_y=" + y + tileExtension;
                                int attemptToDdownloadTile = 0;
                            tryDownloadingAgain:
                                if (backgroundWorkerDownload.CancellationPending) break;
                                try
                                {
                                    attemptToDdownloadTile++;
                                    using (WebClient client = new WebClient())
                                    {
                                        if (!canRun) manualResetEvent.WaitOne();
                                        client.DownloadFile(request, fileName);
                                        numberOfFilesCreated++;
                                        if (!canRun)
                                        {
                                            manualResetEvent.WaitOne();
                                        }
                                        calculateProgress(numberOfFilesToCreate, numberOfFilesCreated);
                                    }

                                }
                                catch
                                {
                                    if (attemptToDdownloadTile <= 5) goto tryDownloadingAgain;
                                }
                            }
                            if (backgroundWorkerDownload.CancellationPending)
                            {
                                y = lastRow;
                                break;
                            }
                        }
                    }
                    break;
            }

    }
    private int getNearestScaleIndex()
        {
            XmlNodeList xmlNodeListTileMatrix = xmlDocumentCapabilities.GetElementsByTagName("TileMatrix");
            List<double> scaleDenominatorList = new List<double>();
            string epsg = Module1.formData.epsg;
            foreach (XmlNode nodeTileMatrix in xmlNodeListTileMatrix)
            {
                XmlNodeList xmlNodeListChildTileMatrix = nodeTileMatrix.ChildNodes;
                foreach (XmlNode xmlNodeChildTileMatrix in xmlNodeListChildTileMatrix)
                {
                    if (xmlNodeChildTileMatrix.Name.Equals("ows:Identifier") && xmlNodeChildTileMatrix.InnerText.Contains(epsg) && xmlNodeChildTileMatrix.NextSibling.Name.Equals("ScaleDenominator"))
                    {
                        string scaleFromXml = xmlNodeChildTileMatrix.NextSibling.InnerText;
                        double scaleDenominator = double.Parse(scaleFromXml, CultureInfo.InvariantCulture);
                        scaleDenominatorList.Add(scaleDenominator);
                    }
                }
            }
            int indexScale = scaleDenominatorList.Count - 1;
            double minimumDistance= Double.MaxValue;
            double originalScale = Module1.formData.scale;
            for (int i = scaleDenominatorList.Count - 1; i >= 0; i--)
            {
                double distance = Math.Abs(scaleDenominatorList[i] - originalScale);
                if (distance < minimumDistance) { 
                    minimumDistance = distance;
                    Module1.formData.scale = scaleDenominatorList[i];
                    indexScale = i;
                }
            }
            return indexScale;
        }
        private void createGeoreferenced(String fileName, double xmin, double ymax, double xscale, double yscale)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                file.WriteLine(xscale.ToString().Replace(',', '.'));
                file.WriteLine("0.0");
                file.WriteLine("0.0");
                file.WriteLine(yscale.ToString().Replace(',', '.'));
                file.WriteLine(xmin.ToString().Replace(',', '.'));
                file.WriteLine(ymax.ToString().Replace(',', '.'));
            }
        }
        private MyPoint getTopLeftCorner(int indexDenominatorScale)
        {
            MyPoint topLeftCorner = new MyPoint();
            XmlNodeList xmlNodeListTileMatrix = xmlDocumentCapabilities.GetElementsByTagName("TileMatrix");
            string epsg = Module1.formData.epsg;
            epsg += ":" + indexDenominatorScale;
            foreach (XmlNode nodeTileMatrix in xmlNodeListTileMatrix)
            {
                XmlNodeList xmlNodeListChildTileMatrix = nodeTileMatrix.ChildNodes;
                foreach (XmlNode xmlNodeChildTileMatrix in xmlNodeListChildTileMatrix)
                {
                    if (xmlNodeChildTileMatrix.Name.Equals("ows:Identifier") && xmlNodeChildTileMatrix.InnerText.Equals(epsg) && xmlNodeChildTileMatrix.NextSibling.NextSibling.Name.Equals("TopLeftCorner"))
                    {
                        string topLeftCornerXY = xmlNodeChildTileMatrix.NextSibling.NextSibling.InnerText;
                        string[] cornerXY = topLeftCornerXY.Split(' ');
                        topLeftCorner.x = double.Parse(cornerXY[0], CultureInfo.InvariantCulture);
                        topLeftCorner.y = double.Parse(cornerXY[1], CultureInfo.InvariantCulture);
                        return topLeftCorner;
                    }
                }
            }
            return topLeftCorner;
        }
        private double getWMTSResolution(Double scale)
        {
            double inch = 0.0254;
            double dpi = 90.71428571429;
            double resolution = scale * (inch / dpi);
            return resolution;
        }
        private void backgroundWorkerDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                Module1.thereIsDownloadNow = false;
                this.Close();
            }
            else if (e.Cancelled)
            {
                Module1.thereIsDownloadNow = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Download completed.");
                Module1.thereIsDownloadNow = false;
                this.Close();
            }
        }
        private void backgroundWorkerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarExportMap.Value = e.ProgressPercentage;
            this.labelProgress.Text = e.ProgressPercentage.ToString()+" %";
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.backgroundWorkerDownload.CancelAsync();        
        }
        private void calculateProgress(int numberOfFilesToCreate, int numberOfFilesCreated)
        {
            double percentComplete = (double)numberOfFilesCreated / (double)numberOfFilesToCreate;
            percentComplete = percentComplete * 100;
            backgroundWorkerDownload.ReportProgress((int)percentComplete);    
        }
        private void buttonStopStart_Click(object sender, EventArgs e)
        {
            if (canRun) manualResetEvent.Reset();
            else { manualResetEvent.Set(); }
            canRun = !canRun;
            buttonStopStart.Text = canRun ? "Stop" : "Start";
        }
    }
}
