using ArcGIS.Core.CIM;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace wmsWmtsDownloader
{
    public partial class FormExportMap : Form
    {
        FormData formData;
        LayerInfo selestedLayerInfo;
        bool correctCoordinateTopX;
        bool correctCoordinateTopY;
        bool correctCoordinateBottomX;
        bool correctCoordinateBottomY;
        bool correctCoordinate;
        bool correctScale;
        string[] availableCRS;
        public FormExportMap()
        {
            InitializeComponent();
            formData = Module1.formData;
            selestedLayerInfo = new LayerInfo();
            formData.setCurrentCoordinate();
            formData.setCurrentScale();
            correctCoordinateTopX = true;
            correctCoordinateTopY = true;
            correctCoordinateBottomX = true;
            correctCoordinateBottomY = true;
            correctCoordinate = true;
            correctScale = true;
            availableCRS = new string[]{ "EPSG:2180", "EPSG:2176", "EPSG:2177", "EPSG:2178", "EPSG:2179"};
            this.comboBoxResolution.Text = formData.dpi.ToString();     
            setCoordinatesAndScale();
            addActiveLayer();
            this.labelOutputIS.Visible = false;
            this.labelOutputImageSPX.Visible=false;
            this.labelOutputImageSPX.Text = setOutputImageSize();
        }
        private void FormExportMap_Load(object sender, EventArgs e)
        {
          
        }
        private void buttonCurrentView_Click(object sender, EventArgs e)
        {
            setCurrentView();
        }
        private void setCurrentView()
        {
            formData.setCurrentCoordinate();
            formData.setCurrentScale();
            setCoordinatesAndScale();
            this.comboBoxExtendFromLayer.Text = "none";
        }
        private void setCoordinatesAndScale()
        {
            this.textBoxCoTopX.Text = formData.coordinateTopX.ToString();
            this.textBoxCoTopY.Text = formData.coordinateTopY.ToString();
            this.textBoxCoBottomX.Text = formData.coordinateBottomX.ToString();
            this.textBoxCoBottomY.Text = formData.coordinateBottomY.ToString();
            this.textBoxExportScale.Text = formData.scale.ToString();
        }
        private string setOutputImageSize()
        {
            //(((|pX2-pX1|)/(scale/1000 because we shuld have 1mm-...m))*dpi)/cal
            if (correctCoordinateTopX && correctCoordinateTopY && correctCoordinateBottomX && correctCoordinateBottomY && correctScale)
            {
                double scaleDivide = formData.scale / 1000;
                double distanceX = Math.Abs(formData.coordinateBottomX - formData.coordinateTopX);
                double pxX = (((distanceX / scaleDivide) * formData.dpi)) / (25.4);
                formData.OutputImageSizeX = Math.Ceiling(pxX);
                double distanceY = Math.Abs(formData.coordinateBottomY - formData.coordinateTopY);
                double pxY = (((distanceY / scaleDivide) * formData.dpi)) / (25.4);
                formData.OutputImageSizeY = Math.Ceiling(pxY);
                return formData.OutputImageSizeX.ToString() + "x" + formData.OutputImageSizeY.ToString() + " px";
            }else {
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                if (!correctScale)
                {
                    return "Erroneous scale!";
                }
                return "Erroneous coordinates!";
            }
            
        }
        private void correctDataActiveButtonNext()
        {
            if (correctCoordinateTopX && correctCoordinateTopY && correctCoordinateBottomX && correctCoordinateBottomY && correctScale && !comboBoxLayer.Text.Equals("none"))
            {
                this.buttonNext.Enabled = true;
            } else
            {
                this.buttonNext.Enabled = false;
            }
        }
        private void comboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            formData.dpi = Int32.Parse(this.comboBoxResolution.SelectedItem.ToString());
            labelOutputImageSPX.Text = setOutputImageSize();
        }
        private void textBoxExportScale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            correctScale = Double.TryParse(textBoxExportScale.Text.ToString(), out newScale);
            if (newScale <= 0) correctScale = false;
            if (correctScale) {
                
                formData.scale = newScale;
                textBoxExportScale.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.Text = setOutputImageSize();
            }
            else {
                textBoxExportScale.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous scale!";
            }
            correctDataActiveButtonNext();
        }
        private void comboBoxTileSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            formData.tileSize=this.comboBoxTileSize.Text.ToString();
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            
           if (!this.comboBoxLayer.Text.Equals("none"))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Map";
                saveFileDialog.Filter = "TIFF (*.tif)|*.tif|PNG Files(*.png)|*.png|JPeg Image(*.jpg)|*.jpg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    formData.saveFileDialog = saveFileDialog;
                    Module1.formData = formData;
                    Module1.layerInfo = selestedLayerInfo;
                    this.Close();
                }
                else
                {
                    Module1.formData.saveFileDialog = null;
                }
            }
            else
            {
                this.labelChooseLayer.Visible = true;
            }
            
        }
        private void textBoxCoTopY_TextChanged(object sender, EventArgs e)
        {
            double newCoordinate = getCoordinate(sender);
            correctCoordinateTopY = correctCoordinate;
            if (correctCoordinate)
            {
                labelOutputImageSPX.Text = setOutputImageSize();
                formData.coordinateTopY = newCoordinate;
            }
            correctDataActiveButtonNext();
        }   
        private void textBoxCoTopX_TextChanged(object sender, EventArgs e)
        {
            double newCoordinate = getCoordinate(sender);
            correctCoordinateTopX= correctCoordinate;
            if (correctCoordinate)
            {
                labelOutputImageSPX.Text = setOutputImageSize();
                formData.coordinateTopX = newCoordinate;
            }
            correctDataActiveButtonNext();
        }
        private void textBoxCoBottomX_TextChanged(object sender, EventArgs e)
        {
            double newCoordinate = getCoordinate(sender);
            correctCoordinateBottomX= correctCoordinate;
            if (correctCoordinate)
            {
                labelOutputImageSPX.Text = setOutputImageSize();
                formData.coordinateBottomX = newCoordinate;
            }
            correctDataActiveButtonNext();
        }
        private void textBoxCoBottomY_TextChanged(object sender, EventArgs e)
        {
            double newCoordinate = getCoordinate(sender);
            correctCoordinateBottomY = correctCoordinate;
            if (correctCoordinate)
            {
                labelOutputImageSPX.Text = setOutputImageSize();
                formData.coordinateBottomY = newCoordinate;
            }
            correctDataActiveButtonNext();
        }
        private double getCoordinate(object sender)
        {
            double newCoordinate;
            TextBox textBox = sender as TextBox;
            string coordinate = textBox.Text;
            correctCoordinate = Double.TryParse(coordinate, out newCoordinate);
            if (correctCoordinate)
            {
                textBox.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                textBox.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous coordinates!";
            }
            return newCoordinate;
        }
        private void checkBoxGeoreferences_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGeoreferences.CheckState.ToString().Equals("Checked"))formData.saveGeoreference = true;
            else formData.saveGeoreference = false;
        }
        private void comboBoxExtendFromLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLayerName = this.comboBoxExtendFromLayer.Text;
            if (!selectedLayerName.Equals("none"))
            {
                bool coordinateHasBeenSet = false;          
                ReadOnlyCollection<Layer> layers = MapView.Active.Map.Layers;
                List<Layer> activeLayers = (layers.Where((layer) => { return layer.IsVisible; })).ToList();
                foreach (Layer activeLayer in activeLayers)
                {
                    if (activeLayer.Name.Equals(selectedLayerName))
                    {
                        this.labelChooseLayer.Visible = false;
                        Envelope envelope = null;
                        Task task = QueuedTask.Run(() =>
                        {
                            envelope = activeLayer.QueryExtent();
                        });
                        task.Wait();
                        this.textBoxCoTopX.Text = envelope.XMin.ToString();
                        formData.coordinateTopX = envelope.XMin;
                        this.textBoxCoTopY.Text = envelope.YMax.ToString();
                        formData.coordinateTopY = envelope.YMax;
                        this.textBoxCoBottomX.Text = envelope.XMax.ToString();
                        formData.coordinateBottomX = envelope.XMax;
                        this.textBoxCoBottomY.Text = envelope.YMin.ToString();
                        formData.coordinateBottomY = envelope.YMin;
                        coordinateHasBeenSet = true;
                        break;                
                    }

                }
                if (!coordinateHasBeenSet)
                {
                    string crs = this.comboBoxProjectionEPSGcode.Text;
                    if (!crs.Equals(""))
                    {
                        foreach (Layer activeLayer in activeLayers)
                        {
                            String layerName = "";
                            XmlDocument xmlDocument = new XmlDocument();
                            Task t = QueuedTask.Run(() =>
                            {
                                CIMDataConnection cimDataConnection = activeLayer.GetDataConnection();
                                xmlDocument.LoadXml(cimDataConnection.ToXml());
                                try
                                {
                                    layerName = xmlDocument.SelectSingleNode("CIMWMSServiceConnection/LayerName").InnerText;
                                }
                                catch { }
                            });
                            t.Wait();
                            if (layerName.Equals(selectedLayerName))
                            {
                                setCoordinateFromLayerXml(activeLayer);
                                coordinateHasBeenSet = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        this.comboBoxExtendFromLayer.Text = "none";
                        this.labelChooseLayer.Visible = true;
                    }
                }
            }
            else
            {
                this.labelChooseLayer.Visible = false;
                setCurrentView();
            }
        }
       
        private void setCoordinateFromLayerXml(Layer layer)
        {
            string address = "";
            XmlDocument xml = new XmlDocument();
            Task task = QueuedTask.Run(() =>
            {
                xml.LoadXml(layer.GetDataConnection().ToXml());
                address += xml.SelectSingleNode("CIMWMSServiceConnection/ServerConnection/URL").InnerText;
            });
            task.Wait();
            address += "REQUEST=GetCapabilities&SERVICE=WMS";
            string xmlDownloaded = "";
        tryDownloadXml:
            try
            {
                WebClient client = new WebClient();
                xmlDownloaded = client.DownloadString(address);
                if (xmlDownloaded.Contains("ServiceExceptionReport")) {
                    goto tryDownloadXml;
                }    
            }
            catch
            {
                goto tryDownloadXml;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlDownloaded);
            XmlNodeList xmlNodeListLayer = xmlDocument.GetElementsByTagName("Layer");
            foreach(XmlNode nodeLayer in xmlNodeListLayer)
            {
                try
                {
                    XmlNodeList xmlNodeListChildLayer = nodeLayer.ChildNodes;
                    foreach (XmlNode xmlNodeChildLayer in xmlNodeListChildLayer)
                    {
                        if (xmlNodeChildLayer.Name.Equals("Name"))
                        {
                            string layerName = xmlNodeChildLayer.InnerXml;
                            if (layerName.Equals(comboBoxExtendFromLayer.Text))
                            { 
                                foreach(XmlNode xmlNodeChildLayerBoundingBox in xmlNodeListChildLayer)
                                {
                                    if (xmlNodeChildLayerBoundingBox.Name.Equals("BoundingBox")){
                                        XmlAttributeCollection attributeCollection= xmlNodeChildLayerBoundingBox.Attributes;
                                        string crs = attributeCollection["CRS"].Value;
                                        if (crs.Equals(comboBoxProjectionEPSGcode.Text))
                                        {
                                            textBoxCoTopX.Text = attributeCollection["minx"].Value.Replace(".",",");
                                            textBoxCoBottomX.Text = attributeCollection["maxx"].Value.Replace(".", ",");
                                            textBoxCoTopY.Text = attributeCollection["maxy"].Value.Replace(".", ",");
                                            textBoxCoBottomY.Text = attributeCollection["miny"].Value.Replace(".", ",");
                                        }
                                    }
                                } 
                            }
                        }
                    }
                }
                catch {  }
            }
        }
        private void addCRS(string requestCapabilities) { 
            this.comboBoxProjectionEPSGcode.Items.Clear();
            string xmlCapabilities="";
            tryDownloadXml:
            try
            {
                WebClient client = new WebClient();
                xmlCapabilities = client.DownloadString(requestCapabilities);
                if (xmlCapabilities.Contains("ServiceExceptionReport"))
                {
                    goto tryDownloadXml;
                }
                selestedLayerInfo.xmlDocumentCapabilities = new XmlDocument();
                selestedLayerInfo.xmlDocumentCapabilities.LoadXml(xmlCapabilities);
                List<string> crsFromXml = new List<string>();
                XmlNodeList xmlNodesCRSList =null;
                if (selestedLayerInfo.serverType.Equals("WMS"))
                {
                    xmlNodesCRSList = selestedLayerInfo.xmlDocumentCapabilities.GetElementsByTagName("CRS");
                }
                if (selestedLayerInfo.serverType.Equals("WMTS"))
                {
                    xmlNodesCRSList = selestedLayerInfo.xmlDocumentCapabilities.GetElementsByTagName("TileMatrixSet");
                }
                foreach (XmlNode xmlNodeCRS in xmlNodesCRSList)
                {
                    crsFromXml.Add(xmlNodeCRS.InnerText);
                }
                IEnumerable<String> crs = crsFromXml.Distinct();
                string crsText="";
                foreach (IEnumerable item in crs)
                {
                    if (availableCRS.Contains(item.ToString())){
                        this.comboBoxProjectionEPSGcode.Items.Insert(this.comboBoxProjectionEPSGcode.Items.Count, item);
                        if (!item.ToString().Equals("EPSG:2180")){
                            crsText = item.ToString(); 
                        }
                    }
                }
                if (crs.Contains("EPSG:2180"))
                {
                    this.comboBoxProjectionEPSGcode.Text = "EPSG:2180";
                }else if(!crsText.Equals(""))
                {
                    this.comboBoxProjectionEPSGcode.Text = crsText;
                }
            }
            catch {
                goto tryDownloadXml;
            }
        }
        private void addTileSize()
        {
            this.comboBoxTileSize.Items.Clear();
            if (selestedLayerInfo.serverType.Equals("WMS"))
            {
                this.comboBoxTileSize.Items.AddRange(new object[] {
                "64x64",
                "128x128",
                "256x256",
                "512x512",
                "1024x1024",
                "2048x2048"});
                 this.comboBoxTileSize.Text = "512x512";
            }
            if (selestedLayerInfo.serverType.Equals("WMTS"))
            {
                XmlNodeList xmlNodeListTileMatrix = selestedLayerInfo.xmlDocumentCapabilities.GetElementsByTagName("TileMatrix");
                List<String> tileSizes = new List<String>();
                string epsg = this.comboBoxProjectionEPSGcode.Text;
                foreach (XmlNode nodeTileMatrix in xmlNodeListTileMatrix)
                {
                    XmlNodeList xmlNodeListChildTileMatrix = nodeTileMatrix.ChildNodes;
                    foreach (XmlNode xmlNodeChildTileMatrix in xmlNodeListChildTileMatrix)
                    {
                        if (xmlNodeChildTileMatrix.Name.Equals("ows:Identifier") && xmlNodeChildTileMatrix.InnerText.Contains(epsg))
                        {
                           if(xmlNodeChildTileMatrix.NextSibling.NextSibling.NextSibling.Name.Equals("TileWidth"))
                            {
                                String size = xmlNodeChildTileMatrix.NextSibling.NextSibling.NextSibling.InnerText;
                                size += "x" + size;
                                tileSizes.Add(size);
                            } 
                        }
                    }
                }
                IEnumerable<String> tileSizesToAdd =tileSizes.Distinct();
                foreach (IEnumerable item in tileSizesToAdd)
                {

                    this.comboBoxTileSize.Items.Insert(this.comboBoxTileSize.Items.Count, item);
                    this.comboBoxTileSize.Text = item.ToString();
                }
            }
        }
        private void addActiveLayer()
        {
            this.comboBoxLayer.Text = "none";
            this.comboBoxExtendFromLayer.Text = "none";
            this.comboBoxExtendFromLayer.Items.Insert(comboBoxExtendFromLayer.Items.Count, "none");
            ReadOnlyCollection<Layer> layers = MapView.Active.Map.Layers;
            List<Layer> activeLayers = (layers.Where((layer) => { return layer.IsVisible; })).ToList();
            foreach (Layer activeLayer in activeLayers)
            {  
                Task taskConnectionActiveLayer = QueuedTask.Run(() =>
                {
                    CIMDataConnection connection = activeLayer.GetDataConnection();
                    string typeConnections = connection.GetType().Name;
                    switch (typeConnections)
                    {
                        case "CIMWMSServiceConnection": //WMS
                            XmlDocument xmlActiveLayerWMS = new XmlDocument();
                            xmlActiveLayerWMS.LoadXml(connection.ToXml());
                            string activeLayerWMSName = xmlActiveLayerWMS.SelectSingleNode("CIMWMSServiceConnection/LayerName").InnerText;
                            this.comboBoxLayer.Items.Insert(comboBoxLayer.Items.Count, activeLayerWMSName);
                            this.comboBoxExtendFromLayer.Items.Insert(comboBoxExtendFromLayer.Items.Count, activeLayerWMSName);
                            break;
                        case "CIMWMTSServiceConnection"://WMTS
                            XmlDocument xmlActiveLayerWMTS = new XmlDocument();
                            xmlActiveLayerWMTS.LoadXml(connection.ToXml());
                            string activeLayerWMTSName = xmlActiveLayerWMTS.SelectSingleNode("CIMWMTSServiceConnection/LayerName").InnerText;
                            this.comboBoxLayer.Items.Insert(comboBoxLayer.Items.Count, activeLayerWMTSName);

                            break;
                        case "CIMStandardDataConnection": //Polygon
                            XmlDocument xmlActiveLayerPolygon = new XmlDocument();
                            xmlActiveLayerPolygon.LoadXml(connection.ToXml());
                            string workspaceFactory = xmlActiveLayerPolygon.SelectSingleNode("CIMStandardDataConnection/WorkspaceFactory").InnerText;
                            if (workspaceFactory.Equals("Shapefile"))
                            {
                                this.comboBoxExtendFromLayer.Items.Insert(comboBoxExtendFromLayer.Items.Count, activeLayer.Name);
                            }
                            break;
                        default:
                            break;
                    }
                });
                taskConnectionActiveLayer.Wait(); 
            }
        }
        private void checkBoxBigTIFF_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Module1.formData.saveFileDialog = null;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLayerName = this.comboBoxLayer.SelectedItem.ToString();
            if (!selectedLayerName.Equals("none"))
            {
                this.labelChooseLayer.Visible = false;
                string requestAddressCapabiliteies="";
                ReadOnlyCollection<Layer> layers = MapView.Active.Map.Layers;
                List<Layer> activeLayers = (layers.Where((layer) => { return layer.IsVisible; })).ToList();
                foreach (Layer activeLayer in activeLayers)
                {
                    Task taskConnectionActiveLayer = QueuedTask.Run(() =>
                    {
                        CIMDataConnection connection = activeLayer.GetDataConnection();
                        string typeConnections = connection.GetType().Name;
                        if (typeConnections.Equals("CIMWMSServiceConnection") || typeConnections.Equals("CIMWMTSServiceConnection"))
                        {
                            XmlDocument xmlLayer = new XmlDocument();
                            xmlLayer.LoadXml(connection.ToXml());
                            string layerName = xmlLayer.SelectSingleNode(typeConnections + "/LayerName").InnerText;
                            if (layerName.Equals(selectedLayerName))
                            {
                                selestedLayerInfo.setLayerInfo(layerName, typeConnections, xmlLayer);
                                requestAddressCapabiliteies = selestedLayerInfo.getRequestAddressCapabiliteies();
                            }
                        }
                    });
                    taskConnectionActiveLayer.Wait();
                    if (!requestAddressCapabiliteies.Equals(""))
                    {
                        if (selestedLayerInfo.serverType.Equals("WMTS"))
                        {
                            this.comboBoxResolution.Text = "90.71428571429";
                            this.comboBoxResolution.Enabled = false;
                            this.labelOutputImageSPX.Visible = false;
                            this.labelOutputIS.Visible = false;




                        }
                        else
                        {
                            this.comboBoxResolution.Text = "96";
                            this.comboBoxResolution.Enabled = true;
                            this.labelOutputImageSPX.Visible = true;
                            this.labelOutputIS.Visible = true;
                        }
                        addCRS(requestAddressCapabiliteies);
                        
                        if (correctCoordinateTopX && correctCoordinateTopX && correctCoordinateBottomX && correctCoordinateBottomY && correctScale)
                        {
                            buttonNext.Enabled = true;
                        }
                        else
                        {
                            buttonNext.Enabled = false ;
                        }
                        break;
                    }
                }
            }
        }
        private void comboBoxProjectionEPSGcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            formData.epsg = this.comboBoxProjectionEPSGcode.Text;
            if (!comboBoxExtendFromLayer.Text.Equals("none")){
                string selectedLayerName = this.comboBoxExtendFromLayer.Text;
                if (!selectedLayerName.Equals("none"))
                {
                    bool coordinateHasBeenSet = false;
                    ReadOnlyCollection<Layer> layers = MapView.Active.Map.Layers;
                    List<Layer> activeLayers = (layers.Where((layer) => { return layer.IsVisible; })).ToList();
                    foreach (Layer activeLayer in activeLayers)
                    {
                        if (activeLayer.Name.Equals(selectedLayerName))
                        {
                            coordinateHasBeenSet = true;
                            break;
                        }
                    }
                    if (!coordinateHasBeenSet)
                    {
                        string crs = this.comboBoxProjectionEPSGcode.Text;
                        if (!crs.Equals(""))
                        {
                            foreach (Layer activeLayer in activeLayers)
                            {
                                String layerName = "";
                                XmlDocument xmlDocument = new XmlDocument();
                                Task t = QueuedTask.Run(() =>
                                {
                                    CIMDataConnection cimDataConnection = activeLayer.GetDataConnection();
                                    xmlDocument.LoadXml(cimDataConnection.ToXml());
                                    try
                                    {
                                        layerName = xmlDocument.SelectSingleNode("CIMWMSServiceConnection/LayerName").InnerText;
                                    }
                                    catch { }
                                });
                                t.Wait();
                                if (layerName.Equals(selectedLayerName))
                                {
                                    setCoordinateFromLayerXml(activeLayer);
                                    coordinateHasBeenSet = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            addTileSize();
        }
    }
}
