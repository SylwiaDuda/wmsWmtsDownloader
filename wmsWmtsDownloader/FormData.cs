using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace wmsWmtsDownloader
{
    class FormData
    {
        // coordinateW, coordinateS, coordinateE;
        public FormData()
        {
            
        }
        public double coordinateTopX {get;set;}
        public double coordinateTopY {get; set;}
        public double coordinateCameraX { get; set; }
        public double coordinateCameraY { get; set; }
        public double coordinateBottomX {get; set;}
        public double coordinateBottomY {get; set;}
        public double scale { get; set; }
        public int dpi { get; set; }
        public bool saveGeoreference { get; set; }
        public double OutputImageSizeX { get; set; }
        public double OutputImageSizeY { get; set; }
        public string tileSize { get; set; }
        public SaveFileDialog saveFileDialog { get; set; }
    }
}
