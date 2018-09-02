using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wmsWmtsDownloader
{
    public partial class FormExportMap : Form
    {
        public FormExportMap()
        {
            InitializeComponent();
            this.comboBoxResolution.Text = Module1.formData.dpi.ToString();
            if (!Module1.formData.saveGeoreference) this.checkBoxGeoreferences.CheckState = CheckState.Unchecked;
            this.comboBoxTileSize.Text = Module1.formData.tileSize;
            setCoordinatesAndScale();
            setOutputImageSize();   
            

        } 
        private void FormExportMap_Load(object sender, EventArgs e)
        {
          
        }
        private void buttonCurrentView_Click(object sender, EventArgs e)
        {
            Module1.setCurrentCoordinateAndScale();
            setCoordinatesAndScale();
        }
        string warstwy="";
        private void setCoordinatesAndScale()
        {
            this.textBoxCoTopX.Text = Module1.formData.coordinateTopX.ToString();
            this.textBoxCoTopY.Text = Module1.formData.coordinateTopY.ToString();
            this.textBoxCoBottomX.Text = Module1.formData.coordinateBottomX.ToString();
            this.textBoxCoBottomY.Text = Module1.formData.coordinateBottomY.ToString();
            this.textBoxExportScale.Text = Module1.formData.scale.ToString();
           /* 
            ReadOnlyCollection<Layer> layers = MapView.Active.Map.Layers;
            List<Layer> activeLayers = (layers.Where((layer) => { return layer.IsVisible; })).ToList();
            
            foreach (Layer activeLayer in activeLayers)
            {
                var zm = " ";
                Task t = QueuedTask.Run(() =>
                {
                    var con = activeLayer.GetDataConnection();
                    Debug.WriteLine(con);
                });
                t.Wait();
                Debug.WriteLine("foreach");
                warstwy +=activeLayer.Name +" : " + zm.ToString()+ " ;; ";
            }


            MessageBox.Show(warstwy);*/
        }
        private void setOutputImageSize()
        {
            //(((|pX2-pX1|)/(scale/1000 because we shuld have 1mm-...m))*dpi)/cal
            double scaleDivide = Module1.formData.scale / 1000;
            double distanceX = Math.Abs(Module1.formData.coordinateBottomX - Module1.formData.coordinateTopX);
            double pxX = (((distanceX / scaleDivide) * Module1.formData.dpi))/(25.4);
            Module1.formData.OutputImageSizeX = Math.Ceiling(pxX);
            double distanceY = Math.Abs(Module1.formData.coordinateBottomY - Module1.formData.coordinateTopY);
            double pxY = (((distanceY / scaleDivide) * Module1.formData.dpi)) / (25.4);
            Module1.formData.OutputImageSizeY = Math.Ceiling(pxY);
            labelOutputImageSPX.Text = Module1.formData.OutputImageSizeX.ToString() + "x" + Module1.formData.OutputImageSizeY.ToString() + " px";
        }
        private void comboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            Module1.formData.dpi = Int32.Parse(this.comboBoxResolution.SelectedItem.ToString());
            setOutputImageSize();
        }
        private void textBoxExportScale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            bool trueScale = Double.TryParse(textBoxExportScale.Text.ToString(), out newScale);
            if (newScale <= 0) trueScale = false;
            if (trueScale) { 
                Module1.formData.scale = newScale;
                textBoxExportScale.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
                setOutputImageSize();
            }
            else {
                textBoxExportScale.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous scale!";
            }           
        }
        private void comboBoxTileSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Module1.formData.tileSize=this.comboBoxTileSize.Text.ToString();
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Map";
            saveFileDialog.Filter = "TIFF (*.tif)|*.tif|PNG Files(*.png)|*.png|txt files (*.txt)|*.txt";
            Module1.formData.saveFileDialog = null;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Module1.formData.saveFileDialog = saveFileDialog;
                this.Close();
            }
        }
        private void textBoxCoTopY_TextChanged(object sender, EventArgs e)
        {
            double newCoordinateTopY;
            if (Double.TryParse(textBoxCoTopY.Text.ToString(), out newCoordinateTopY))
            {
                Module1.formData.coordinateTopY = newCoordinateTopY;
                textBoxCoTopY.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
                setOutputImageSize();
            }
            else
            {
                textBoxCoTopY.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous coordinates!";
            }
        }
        private void textBoxCoTopX_TextChanged(object sender, EventArgs e)
        {
            double newCoordinateTopX;
            if (Double.TryParse(textBoxCoTopX.Text.ToString(), out newCoordinateTopX))
            {
                Module1.formData.coordinateTopX = newCoordinateTopX;
                textBoxCoTopX.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
                setOutputImageSize();
            }
            else
            {
                textBoxCoTopX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous coordinates!";
            }
        }
        private void textBoxCoBottomX_TextChanged(object sender, EventArgs e)
        {
            double newCoordinateBottomX;
            if (Double.TryParse(textBoxCoBottomX.Text.ToString(), out newCoordinateBottomX))
            {
                Module1.formData.coordinateBottomX = newCoordinateBottomX;
                textBoxCoBottomX.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
                setOutputImageSize();
            }
            else
            {
                textBoxCoBottomX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous coordinates!";
            }
        }
        private void textBoxCoBottomY_TextChanged(object sender, EventArgs e)
        {
            double newCoordinateBottomY;
            if (Double.TryParse(textBoxCoBottomY.Text.ToString(), out newCoordinateBottomY))
            {
                Module1.formData.coordinateBottomY = newCoordinateBottomY;
                textBoxCoBottomY.ForeColor = System.Drawing.Color.Black;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Black;
                setOutputImageSize();
            }
            else
            {
                textBoxCoBottomY.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.ForeColor = System.Drawing.Color.Red;
                labelOutputImageSPX.Text = "Erroneous coordinates!";
            }
        }
        private void checkBoxGeoreferences_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGeoreferences.CheckState.ToString().Equals("Checked"))Module1.formData.saveGeoreference = true;
            else Module1.formData.saveGeoreference = false;
        }
        private void comboBoxExtendFromLayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void checkBoxBigTIFF_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
