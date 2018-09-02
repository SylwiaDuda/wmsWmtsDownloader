using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Timers;
namespace wmsWmtsDownloader
{
    public partial class FormProgress : Form
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private bool canRun = true;

        public FormProgress()
        {
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
            downloadTiles(backgroundWorkerDownload, e);
            if (backgroundWorkerDownload.CancellationPending)
            {
                e.Cancel = true;
            }
            backgroundWorkerDownload.ReportProgress(100);

        }

        private void downloadTiles(BackgroundWorker backgroundWorker, DoWorkEventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName));
            
            int tileSize = Convert.ToInt32(Module1.formData.tileSize.Split('x')[0]);
            int numberOfTilesHorizontally =(int)Math.Ceiling(Module1.formData.OutputImageSizeX / tileSize);
            int numberOfTilesVertically = (int)Math.Ceiling(Module1.formData.OutputImageSizeY / tileSize);
            double distanceXminXmax = Math.Abs((25.4 * tileSize * Module1.formData.scale) / (Module1.formData.dpi * 1000));
            double distanceYminYmax = Math.Abs((25.4 * tileSize * Module1.formData.scale) / (Module1.formData.dpi * 1000));
            double xmin = Module1.formData.coordinateTopX;
            double xmax = Module1.formData.coordinateTopX + distanceXminXmax;
            double ymin = Module1.formData.coordinateTopY - distanceYminYmax;
            double ymax = Module1.formData.coordinateTopY;
            int numberOfFilesToCreate = numberOfTilesHorizontally * numberOfTilesVertically;
            int numberOfFilesCreated = 0;
            if (Module1.formData.saveGeoreference) numberOfFilesToCreate = numberOfFilesToCreate * 2;
            int tileSizeX = tileSize;
            int tileSizeY= tileSize;
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
                            string tfwFileName = Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "__x=" + xmin + "_y=" + ymax + ".tfw";
                            if (!canRun) manualResetEvent.WaitOne();
                            createWorldFile(Module1.formData.saveFileDialog, tfwFileName, xmin, ymin, xmax, ymax);
                            numberOfFilesCreated++;
                            if (!canRun) manualResetEvent.WaitOne();
                            calculateProgress(numberOfFilesToCreate, numberOfFilesCreated);
                        }
                        // String address = "http://sdi.gdos.gov.pl/wms?TRANSPARENT=TRUE&FORMAT=image/png&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=&LAYERS=ParkiKrajobrazowe&SRS=EPSG:2180&BBOX=";
                        String address = "http://mapy.geoportal.gov.pl/wss/service/img/guest/ORTO/MapServer/WMSServer?REQUEST=GetMap&version=1.1.1&service=WMS&format=image/png&STYLES=&LAYERS=Raster&SRS=EPSG:2180&BBOX=";
                        //  http://mapy.geoportal.gov.pl/wss/service/img/guest/ORTO/MapServer/WMSServer?request=getmap&version=1.1.1&service=WMS&srs=EPSG:4326&format=image/png&bbox
                        address += xmin.ToString().Replace(',', '.') + ","
                            + ymin.ToString().Replace(',', '.') + ","
                            + xmax.ToString().Replace(',', '.') + ","
                            + ymax.ToString().Replace(',', '.') + "&"
                            + "width=" + tileSizeX.ToString()
                            + "&height=" + tileSizeY.ToString(); ;

                        Debug.WriteLine(address);
                        string fileName = Path.GetDirectoryName(Module1.formData.saveFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(Module1.formData.saveFileDialog.FileName) + i + j + "__x=" + xmin + "_y=" + ymax + ".tif";
                        int attemptToDdownloadTile = 0;
                    tryDownloadingAgain:
                        if (backgroundWorkerDownload.CancellationPending)break;
                        try
                        {
                            attemptToDdownloadTile++;
                            using (WebClient client = new WebClient())
                            {
                                if (!canRun) manualResetEvent.WaitOne();
                                Debug.WriteLine("tryyyyyy");
                                client.DownloadFile(address, fileName);
                                numberOfFilesCreated++;
                             
                                if (!canRun)
                                {
                                    Debug.WriteLine("blokada");
                                    manualResetEvent.WaitOne();
                                }
                                Debug.WriteLine("nie blokada");
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
        }
        private void createWorldFile(SaveFileDialog saveFileDialog, String tfwFileName, double xmin, double ymin, double xmax, double ymax)
        {
            using (System.IO.StreamWriter fileTFW = new System.IO.StreamWriter(tfwFileName))
            {
                //xscale =  difference x1 and x2 divisible by the width of the output image in px
                double scaleDivide = Module1.formData.scale / 1000;
                double pxY = (((Math.Abs(ymin - ymax) / scaleDivide) * Module1.formData.dpi)) / (25.4);
                double yscale = (ymin - ymax) / pxY;
                double pxX = (((Math.Abs(xmax - xmin) / scaleDivide) * Module1.formData.dpi)) / (25.4);
                double xscale = (xmax - xmin) / pxX;
                fileTFW.WriteLine(xscale.ToString().Replace(',', '.'));
                fileTFW.WriteLine("0.0");
                fileTFW.WriteLine("0.0");
                fileTFW.WriteLine(yscale.ToString().Replace(',', '.'));
                fileTFW.WriteLine(xmin.ToString().Replace(',', '.'));
                fileTFW.WriteLine(ymax.ToString().Replace(',', '.'));
            }
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
                Debug.WriteLine("Cancel");
                Module1.thereIsDownloadNow = false;
                this.Close();
            }
            else
            {
                Debug.WriteLine("Succes");
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
            Debug.WriteLine(numberOfFilesToCreate+" % "+ numberOfFilesCreated) ;
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
