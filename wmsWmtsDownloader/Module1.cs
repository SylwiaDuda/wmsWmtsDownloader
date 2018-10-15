﻿using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace wmsWmtsDownloader
{
    internal class Module1 : Module
    {
        private static Module1 _this = null;
        public static bool thereIsDownloadNow;
        public static FormData formData;
        public static LayerInfo layerInfo;
        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public Module1()
        {
            thereIsDownloadNow = false;
            formData = new FormData();
            layerInfo = new LayerInfo();
           
        }
        public static Module1 Current
        {
            get
            {
                return _this ?? (_this = (Module1)FrameworkApplication.FindModule("wmsWmtsDownloader_Module"));
            }
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
