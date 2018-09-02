using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Internal.Mapping;
using System.Windows;

namespace wmsWmtsDownloader
{
    internal class Module1 : Module
    {
        private static Module1 _this = null;
        public static bool thereIsDownloadNow;
        public static FormData formData;
        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public Module1()
        {
            thereIsDownloadNow = false;
            formData = new FormData();
            setCurrentCoordinateAndScale();
            formData.dpi = 96;
            formData.saveGeoreference = true;
            formData.tileSize = "512x512";

        }
        public static Module1 Current
        {
            get
            {
                return _this ?? (_this = (Module1)FrameworkApplication.FindModule("wmsWmtsDownloader_Module"));
            }
        }
        public static void setCurrentCoordinateAndScale()
        {
            formData.coordinateCameraX = MapView.Active.Camera.X;
            formData.coordinateCameraY = MapView.Active.Camera.Y;

            Task t = QueuedTask.Run(() =>
            {
                formData.coordinateTopX = MapView.Active.ClientToMap(new Point(0, 0)).X;
                formData.coordinateTopY = MapView.Active.ClientToMap(new Point(0, 0)).Y; 
            });
            t.Wait();
            formData.coordinateBottomX = formData.coordinateCameraX + formData.coordinateCameraX - formData.coordinateTopX;
            formData.coordinateBottomY = formData.coordinateCameraY + formData.coordinateCameraY - formData.coordinateTopY;
            formData.scale = MapView.Active.Camera.Scale;        
        }

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }

        #endregion Overrides

    }
}
