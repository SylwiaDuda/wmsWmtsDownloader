using ArcGIS.Desktop.Framework.Contracts;
using System.Windows.Forms;

namespace wmsWmtsDownloader
{
    internal class ButtonExportMap : ArcGIS.Desktop.Framework.Contracts.Button
    {
        protected override void OnClick()
        {
            //if (!Module1.thereIsDownloadNow)
            {
                FormExportMap formExportMap = new FormExportMap();
                formExportMap.ShowDialog();
            }
            /*else
            {
                MessageBox.Show("Please wait for the previous download to finish.");
            }
            if (Module1.formData.saveFileDialog != null && Module1.thereIsDownloadNow == false)
            {
                Module1.thereIsDownloadNow = true;
                Application.Run(new FormProgress());
            }*/
           

        }
    }
}
