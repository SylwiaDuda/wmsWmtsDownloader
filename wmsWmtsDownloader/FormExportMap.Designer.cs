namespace wmsWmtsDownloader
{
    partial class FormExportMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExportMap));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.LabelResolution = new System.Windows.Forms.Label();
            this.textBoxExportScale = new System.Windows.Forms.TextBox();
            this.labelColorMode = new System.Windows.Forms.Label();
            this.comboBoxColorMode = new System.Windows.Forms.ComboBox();
            this.textBoxCoTopY = new System.Windows.Forms.TextBox();
            this.textBoxCoTopX = new System.Windows.Forms.TextBox();
            this.textBoxCoBottomY = new System.Windows.Forms.TextBox();
            this.textBoxCoBottomX = new System.Windows.Forms.TextBox();
            this.buttonCurrentView = new System.Windows.Forms.Button();
            this.labelExtendFromLayer = new System.Windows.Forms.Label();
            this.comboBoxExtendFromLayer = new System.Windows.Forms.ComboBox();
            this.checkBoxBigTIFF = new System.Windows.Forms.CheckBox();
            this.groupBoxExportExtend = new System.Windows.Forms.GroupBox();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.groupBoxCompression = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBoxExportScale = new System.Windows.Forms.GroupBox();
            this.labelOutputIS = new System.Windows.Forms.Label();
            this.labelOutputImageSPX = new System.Windows.Forms.Label();
            this.labelExportScale = new System.Windows.Forms.Label();
            this.checkBoxGeoreferences = new System.Windows.Forms.CheckBox();
            this.labelTileSize = new System.Windows.Forms.Label();
            this.comboBoxTileSize = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelChooseLayer = new System.Windows.Forms.Label();
            this.comboBoxProjectionEPSGcode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.groupBoxTiledType = new System.Windows.Forms.GroupBox();
            this.groupBoxExportExtend.SuspendLayout();
            this.groupBoxCompression.SuspendLayout();
            this.groupBoxExportScale.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxTiledType.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(533, 535);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(124, 35);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(674, 536);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LabelResolution
            // 
            this.LabelResolution.AutoSize = true;
            this.LabelResolution.Location = new System.Drawing.Point(24, 498);
            this.LabelResolution.Name = "LabelResolution";
            this.LabelResolution.Size = new System.Drawing.Size(130, 20);
            this.LabelResolution.TabIndex = 2;
            this.LabelResolution.Text = "Resolution (DPI):";
            // 
            // textBoxExportScale
            // 
            this.textBoxExportScale.Location = new System.Drawing.Point(34, 40);
            this.textBoxExportScale.Name = "textBoxExportScale";
            this.textBoxExportScale.Size = new System.Drawing.Size(148, 26);
            this.textBoxExportScale.TabIndex = 3;
            this.textBoxExportScale.Text = "unknown";
            this.textBoxExportScale.TextChanged += new System.EventHandler(this.textBoxExportScale_TextChanged);
            // 
            // labelColorMode
            // 
            this.labelColorMode.AutoSize = true;
            this.labelColorMode.Location = new System.Drawing.Point(202, 498);
            this.labelColorMode.Name = "labelColorMode";
            this.labelColorMode.Size = new System.Drawing.Size(94, 20);
            this.labelColorMode.TabIndex = 4;
            this.labelColorMode.Text = "Color Mode:";
            // 
            // comboBoxColorMode
            // 
            this.comboBoxColorMode.FormattingEnabled = true;
            this.comboBoxColorMode.Items.AddRange(new object[] {
            "24-bit True Color",
            "32-bit with Alpha",
            "8-bit Adaptive Palette",
            "8-bit Grayscale"});
            this.comboBoxColorMode.Location = new System.Drawing.Point(206, 535);
            this.comboBoxColorMode.Name = "comboBoxColorMode";
            this.comboBoxColorMode.Size = new System.Drawing.Size(204, 28);
            this.comboBoxColorMode.TabIndex = 7;
            this.comboBoxColorMode.Text = "24-bit True Color";
            // 
            // textBoxCoTopY
            // 
            this.textBoxCoTopY.Location = new System.Drawing.Point(98, 25);
            this.textBoxCoTopY.Name = "textBoxCoTopY";
            this.textBoxCoTopY.Size = new System.Drawing.Size(170, 26);
            this.textBoxCoTopY.TabIndex = 10;
            this.textBoxCoTopY.TextChanged += new System.EventHandler(this.textBoxCoTopY_TextChanged);
            // 
            // textBoxCoTopX
            // 
            this.textBoxCoTopX.Location = new System.Drawing.Point(16, 67);
            this.textBoxCoTopX.Name = "textBoxCoTopX";
            this.textBoxCoTopX.Size = new System.Drawing.Size(156, 26);
            this.textBoxCoTopX.TabIndex = 11;
            this.textBoxCoTopX.TextChanged += new System.EventHandler(this.textBoxCoTopX_TextChanged);
            // 
            // textBoxCoBottomY
            // 
            this.textBoxCoBottomY.Location = new System.Drawing.Point(98, 111);
            this.textBoxCoBottomY.Name = "textBoxCoBottomY";
            this.textBoxCoBottomY.Size = new System.Drawing.Size(170, 26);
            this.textBoxCoBottomY.TabIndex = 12;
            this.textBoxCoBottomY.TextChanged += new System.EventHandler(this.textBoxCoBottomY_TextChanged);
            // 
            // textBoxCoBottomX
            // 
            this.textBoxCoBottomX.Location = new System.Drawing.Point(190, 67);
            this.textBoxCoBottomX.Name = "textBoxCoBottomX";
            this.textBoxCoBottomX.Size = new System.Drawing.Size(175, 26);
            this.textBoxCoBottomX.TabIndex = 13;
            this.textBoxCoBottomX.TextChanged += new System.EventHandler(this.textBoxCoBottomX_TextChanged);
            // 
            // buttonCurrentView
            // 
            this.buttonCurrentView.Location = new System.Drawing.Point(36, 154);
            this.buttonCurrentView.Name = "buttonCurrentView";
            this.buttonCurrentView.Size = new System.Drawing.Size(310, 28);
            this.buttonCurrentView.TabIndex = 14;
            this.buttonCurrentView.Text = "Current view";
            this.buttonCurrentView.UseVisualStyleBackColor = true;
            this.buttonCurrentView.Click += new System.EventHandler(this.buttonCurrentView_Click);
            // 
            // labelExtendFromLayer
            // 
            this.labelExtendFromLayer.AutoSize = true;
            this.labelExtendFromLayer.Location = new System.Drawing.Point(12, 211);
            this.labelExtendFromLayer.Name = "labelExtendFromLayer";
            this.labelExtendFromLayer.Size = new System.Drawing.Size(132, 20);
            this.labelExtendFromLayer.TabIndex = 15;
            this.labelExtendFromLayer.Text = "Extend from layer";
            // 
            // comboBoxExtendFromLayer
            // 
            this.comboBoxExtendFromLayer.FormattingEnabled = true;
            this.comboBoxExtendFromLayer.Location = new System.Drawing.Point(16, 241);
            this.comboBoxExtendFromLayer.Name = "comboBoxExtendFromLayer";
            this.comboBoxExtendFromLayer.Size = new System.Drawing.Size(349, 28);
            this.comboBoxExtendFromLayer.TabIndex = 16;
            this.comboBoxExtendFromLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxExtendFromLayer_SelectedIndexChanged);
            // 
            // checkBoxBigTIFF
            // 
            this.checkBoxBigTIFF.AutoSize = true;
            this.checkBoxBigTIFF.Location = new System.Drawing.Point(454, 445);
            this.checkBoxBigTIFF.Name = "checkBoxBigTIFF";
            this.checkBoxBigTIFF.Size = new System.Drawing.Size(257, 24);
            this.checkBoxBigTIFF.TabIndex = 17;
            this.checkBoxBigTIFF.Text = "Save as Big TIFF (no 4GB limit)";
            this.checkBoxBigTIFF.UseVisualStyleBackColor = true;
            this.checkBoxBigTIFF.CheckedChanged += new System.EventHandler(this.checkBoxBigTIFF_CheckedChanged);
            // 
            // groupBoxExportExtend
            // 
            this.groupBoxExportExtend.Controls.Add(this.textBoxCoTopY);
            this.groupBoxExportExtend.Controls.Add(this.textBoxCoTopX);
            this.groupBoxExportExtend.Controls.Add(this.textBoxCoBottomY);
            this.groupBoxExportExtend.Controls.Add(this.buttonCurrentView);
            this.groupBoxExportExtend.Controls.Add(this.labelExtendFromLayer);
            this.groupBoxExportExtend.Controls.Add(this.comboBoxExtendFromLayer);
            this.groupBoxExportExtend.Controls.Add(this.textBoxCoBottomX);
            this.groupBoxExportExtend.Location = new System.Drawing.Point(28, 186);
            this.groupBoxExportExtend.Name = "groupBoxExportExtend";
            this.groupBoxExportExtend.Size = new System.Drawing.Size(382, 291);
            this.groupBoxExportExtend.TabIndex = 18;
            this.groupBoxExportExtend.TabStop = false;
            this.groupBoxExportExtend.Text = "Export extend";
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Items.AddRange(new object[] {
            "96",
            "144",
            "200",
            "300",
            "600"});
            this.comboBoxResolution.Location = new System.Drawing.Point(28, 535);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(167, 28);
            this.comboBoxResolution.TabIndex = 19;
            this.comboBoxResolution.Text = "96";
            this.comboBoxResolution.SelectedIndexChanged += new System.EventHandler(this.comboBoxResolution_SelectedIndexChanged);
            // 
            // groupBoxCompression
            // 
            this.groupBoxCompression.Controls.Add(this.comboBox1);
            this.groupBoxCompression.Location = new System.Drawing.Point(454, 12);
            this.groupBoxCompression.Name = "groupBoxCompression";
            this.groupBoxCompression.Size = new System.Drawing.Size(341, 72);
            this.groupBoxCompression.TabIndex = 20;
            this.groupBoxCompression.TabStop = false;
            this.groupBoxCompression.Text = "Compression";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "<no compression>",
            "LZW",
            "ZIP",
            "JPEG",
            "PackBITS"});
            this.comboBox1.Location = new System.Drawing.Point(6, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(329, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "LZW";
            // 
            // groupBoxExportScale
            // 
            this.groupBoxExportScale.Controls.Add(this.labelOutputIS);
            this.groupBoxExportScale.Controls.Add(this.labelOutputImageSPX);
            this.groupBoxExportScale.Controls.Add(this.labelExportScale);
            this.groupBoxExportScale.Controls.Add(this.textBoxExportScale);
            this.groupBoxExportScale.Location = new System.Drawing.Point(454, 99);
            this.groupBoxExportScale.Name = "groupBoxExportScale";
            this.groupBoxExportScale.Size = new System.Drawing.Size(341, 144);
            this.groupBoxExportScale.TabIndex = 21;
            this.groupBoxExportScale.TabStop = false;
            this.groupBoxExportScale.Text = "Export scale";
            // 
            // labelOutputIS
            // 
            this.labelOutputIS.AutoSize = true;
            this.labelOutputIS.Location = new System.Drawing.Point(91, 87);
            this.labelOutputIS.Name = "labelOutputIS";
            this.labelOutputIS.Size = new System.Drawing.Size(143, 20);
            this.labelOutputIS.TabIndex = 6;
            this.labelOutputIS.Text = "Output Image size:";
            // 
            // labelOutputImageSPX
            // 
            this.labelOutputImageSPX.AutoSize = true;
            this.labelOutputImageSPX.Location = new System.Drawing.Point(30, 107);
            this.labelOutputImageSPX.Name = "labelOutputImageSPX";
            this.labelOutputImageSPX.Size = new System.Drawing.Size(68, 20);
            this.labelOutputImageSPX.TabIndex = 5;
            this.labelOutputImageSPX.Text = "... x ... px";
            // 
            // labelExportScale
            // 
            this.labelExportScale.AutoSize = true;
            this.labelExportScale.Location = new System.Drawing.Point(6, 43);
            this.labelExportScale.Name = "labelExportScale";
            this.labelExportScale.Size = new System.Drawing.Size(22, 20);
            this.labelExportScale.TabIndex = 4;
            this.labelExportScale.Text = "1:";
            // 
            // checkBoxGeoreferences
            // 
            this.checkBoxGeoreferences.AutoSize = true;
            this.checkBoxGeoreferences.Checked = true;
            this.checkBoxGeoreferences.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGeoreferences.Location = new System.Drawing.Point(454, 397);
            this.checkBoxGeoreferences.Name = "checkBoxGeoreferences";
            this.checkBoxGeoreferences.Size = new System.Drawing.Size(182, 24);
            this.checkBoxGeoreferences.TabIndex = 22;
            this.checkBoxGeoreferences.Text = "Save georeferences ";
            this.checkBoxGeoreferences.UseVisualStyleBackColor = true;
            this.checkBoxGeoreferences.CheckedChanged += new System.EventHandler(this.checkBoxGeoreferences_CheckedChanged);
            // 
            // labelTileSize
            // 
            this.labelTileSize.AutoSize = true;
            this.labelTileSize.Location = new System.Drawing.Point(2, 32);
            this.labelTileSize.Name = "labelTileSize";
            this.labelTileSize.Size = new System.Drawing.Size(138, 20);
            this.labelTileSize.TabIndex = 24;
            this.labelTileSize.Text = "Tile size (in pixels):";
            // 
            // comboBoxTileSize
            // 
            this.comboBoxTileSize.FormattingEnabled = true;
            this.comboBoxTileSize.Location = new System.Drawing.Point(6, 64);
            this.comboBoxTileSize.Name = "comboBoxTileSize";
            this.comboBoxTileSize.Size = new System.Drawing.Size(329, 28);
            this.comboBoxTileSize.TabIndex = 25;
            this.comboBoxTileSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxTileSize_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelChooseLayer);
            this.groupBox1.Controls.Add(this.comboBoxProjectionEPSGcode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxLayer);
            this.groupBox1.Location = new System.Drawing.Point(28, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 151);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layer";
            // 
            // labelChooseLayer
            // 
            this.labelChooseLayer.AutoSize = true;
            this.labelChooseLayer.ForeColor = System.Drawing.Color.Red;
            this.labelChooseLayer.Location = new System.Drawing.Point(181, 16);
            this.labelChooseLayer.Name = "labelChooseLayer";
            this.labelChooseLayer.Size = new System.Drawing.Size(184, 20);
            this.labelChooseLayer.TabIndex = 29;
            this.labelChooseLayer.Text = "Choose a layer to export!";
            this.labelChooseLayer.Visible = false;
            // 
            // comboBoxProjectionEPSGcode
            // 
            this.comboBoxProjectionEPSGcode.FormattingEnabled = true;
            this.comboBoxProjectionEPSGcode.Location = new System.Drawing.Point(16, 103);
            this.comboBoxProjectionEPSGcode.Name = "comboBoxProjectionEPSGcode";
            this.comboBoxProjectionEPSGcode.Size = new System.Drawing.Size(187, 28);
            this.comboBoxProjectionEPSGcode.TabIndex = 27;
            this.comboBoxProjectionEPSGcode.SelectedIndexChanged += new System.EventHandler(this.comboBoxProjectionEPSGcode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Projection EPSG code:";
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Location = new System.Drawing.Point(16, 42);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(349, 28);
            this.comboBoxLayer.TabIndex = 0;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            // 
            // groupBoxTiledType
            // 
            this.groupBoxTiledType.Controls.Add(this.labelTileSize);
            this.groupBoxTiledType.Controls.Add(this.comboBoxTileSize);
            this.groupBoxTiledType.Location = new System.Drawing.Point(454, 267);
            this.groupBoxTiledType.Name = "groupBoxTiledType";
            this.groupBoxTiledType.Size = new System.Drawing.Size(341, 118);
            this.groupBoxTiledType.TabIndex = 27;
            this.groupBoxTiledType.TabStop = false;
            this.groupBoxTiledType.Text = "Tiled type";
            // 
            // FormExportMap
            // 
            this.AcceptButton = this.buttonNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(818, 593);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxTiledType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxGeoreferences);
            this.Controls.Add(this.checkBoxBigTIFF);
            this.Controls.Add(this.groupBoxExportScale);
            this.Controls.Add(this.groupBoxCompression);
            this.Controls.Add(this.comboBoxResolution);
            this.Controls.Add(this.comboBoxColorMode);
            this.Controls.Add(this.labelColorMode);
            this.Controls.Add(this.LabelResolution);
            this.Controls.Add(this.groupBoxExportExtend);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonNext);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExportMap";
            this.Text = "Export Map";
            this.Load += new System.EventHandler(this.FormExportMap_Load);
            this.groupBoxExportExtend.ResumeLayout(false);
            this.groupBoxExportExtend.PerformLayout();
            this.groupBoxCompression.ResumeLayout(false);
            this.groupBoxExportScale.ResumeLayout(false);
            this.groupBoxExportScale.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxTiledType.ResumeLayout(false);
            this.groupBoxTiledType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label LabelResolution;
        private System.Windows.Forms.TextBox textBoxExportScale;
        private System.Windows.Forms.Label labelColorMode;
        private System.Windows.Forms.ComboBox comboBoxColorMode;
        private System.Windows.Forms.TextBox textBoxCoTopY;
        private System.Windows.Forms.TextBox textBoxCoTopX;
        private System.Windows.Forms.TextBox textBoxCoBottomY;
        private System.Windows.Forms.TextBox textBoxCoBottomX;
        private System.Windows.Forms.Button buttonCurrentView;
        private System.Windows.Forms.Label labelExtendFromLayer;
        private System.Windows.Forms.ComboBox comboBoxExtendFromLayer;
        private System.Windows.Forms.CheckBox checkBoxBigTIFF;
        private System.Windows.Forms.GroupBox groupBoxExportExtend;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.GroupBox groupBoxCompression;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBoxExportScale;
        private System.Windows.Forms.Label labelExportScale;
        private System.Windows.Forms.Label labelOutputIS;
        private System.Windows.Forms.Label labelOutputImageSPX;
        private System.Windows.Forms.CheckBox checkBoxGeoreferences;
        private System.Windows.Forms.Label labelTileSize;
        private System.Windows.Forms.ComboBox comboBoxTileSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxLayer;
        private System.Windows.Forms.ComboBox comboBoxProjectionEPSGcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelChooseLayer;
        private System.Windows.Forms.GroupBox groupBoxTiledType;
    }
}