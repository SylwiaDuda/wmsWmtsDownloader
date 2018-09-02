namespace wmsWmtsDownloader
{
    partial class FormProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgress));
            this.progressBarExportMap = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.buttonStopStart = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.backgroundWorkerDownload = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBarExportMap
            // 
            this.progressBarExportMap.Location = new System.Drawing.Point(28, 44);
            this.progressBarExportMap.Name = "progressBarExportMap";
            this.progressBarExportMap.Size = new System.Drawing.Size(591, 23);
            this.progressBarExportMap.TabIndex = 0;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(638, 47);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(36, 20);
            this.labelProgress.TabIndex = 1;
            this.labelProgress.Text = "0 %";
            // 
            // buttonStopStart
            // 
            this.buttonStopStart.Location = new System.Drawing.Point(494, 99);
            this.buttonStopStart.Name = "buttonStopStart";
            this.buttonStopStart.Size = new System.Drawing.Size(104, 29);
            this.buttonStopStart.TabIndex = 2;
            this.buttonStopStart.Text = "Stop";
            this.buttonStopStart.UseVisualStyleBackColor = true;
            this.buttonStopStart.Click += new System.EventHandler(this.buttonStopStart_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(618, 99);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 29);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // backgroundWorkerDownload
            // 
            this.backgroundWorkerDownload.WorkerReportsProgress = true;
            this.backgroundWorkerDownload.WorkerSupportsCancellation = true;
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(742, 143);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStopStart);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBarExportMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormProgress";
            this.Text = "Export Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarExportMap;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonStopStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDownload;
    }
}