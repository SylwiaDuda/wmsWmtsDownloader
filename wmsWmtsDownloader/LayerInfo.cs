using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace wmsWmtsDownloader
{
    class LayerInfo
    {
        public string serverType { get; set; }
        public string serverURL { get; set; }
        public string layerName { get; set; }
        public string serverVersion { get; set; }

        public XmlDocument xmlDocumentCapabilities { get; set; }

        public LayerInfo()
        {
            xmlDocumentCapabilities = new XmlDocument();
        }

        public void setLayerInfo(string layerName, string typeConnections, XmlDocument xmlLayer)
        {
            this.layerName = layerName;
            this.serverURL = xmlLayer.SelectSingleNode(typeConnections + "/ServerConnection/URL").InnerText;
            this.serverType = xmlLayer.SelectSingleNode(typeConnections + "/ ServerConnection/ServerType").InnerText;
            this.serverVersion = xmlLayer.SelectSingleNode(typeConnections + "/Version").InnerText;
        }
        public string getRequestAddressCapabiliteies()
        {
            return serverURL + "SERVICE=" + serverType + "&REQUEST=GetCapabilities&VERSION=" + serverVersion;
        }
    }
}
