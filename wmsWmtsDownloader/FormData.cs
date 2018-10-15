using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace wmsWmtsDownloader
{
    class FormData
    {
        public double coordinateTopX {get;set;}
        public double coordinateTopY {get; set;}
        public double coordinateCameraX { get; set; }
        public double coordinateCameraY { get; set; }
        public double coordinateBottomX {get; set;}
        public double coordinateBottomY {get; set;}

        public double scale { get; set; }
        public string epsg { get; set; }
        public int dpi { get; set; }
        public bool saveGeoreference { get; set; }
        public double OutputImageSizeX { get; set; }
        public double OutputImageSizeY { get; set; }
        public string tileSize { get; set; }
        
        public SaveFileDialog saveFileDialog { get; set; }
        public FormData()
        {
            dpi = 96;
            saveGeoreference = true;
            tileSize = "512x512";
            saveFileDialog = null;
          
        }
        public void setCurrentCoordinate()
        {
            MapView activeMapView = MapView.Active;

            this.coordinateCameraX = activeMapView.Camera.X;
            this.coordinateCameraY = activeMapView.Camera.Y;

            Task t = QueuedTask.Run(() =>
            {
                MapPoint topLeftPoint = activeMapView.ClientToMap(new Point(0, 0));
                this.coordinateTopX = topLeftPoint.X;
                this.coordinateTopY = topLeftPoint.Y;
            });
            t.Wait();
            this.coordinateBottomX = coordinateCameraX + coordinateCameraX - coordinateTopX;
            this.coordinateBottomY = coordinateCameraY + coordinateCameraY - coordinateTopY;
        }
        public void setCurrentScale()
        {
            MapView activeMapView = MapView.Active;
            Camera camera = activeMapView.Camera;  
            this.scale = camera.Scale;
        }
    }
}
