namespace MultiAppLauncher
{
    partial class FileScanner
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
            this.scanButton = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelPattern = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scanButton
            // 
            this.scanButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scanButton.Location = new System.Drawing.Point(319, 68);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(75, 23);
            this.scanButton.TabIndex = 0;
            this.scanButton.Text = "&Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Location = new System.Drawing.Point(30, 25);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(364, 20);
            this.textBoxFolder.TabIndex = 1;
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.Location = new System.Drawing.Point(30, 68);
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.Size = new System.Drawing.Size(283, 20);
            this.textBoxPattern.TabIndex = 2;
            this.textBoxPattern.Text = "NServiceBus.Host.exe";
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(27, 9);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(29, 13);
            this.labelPath.TabIndex = 3;
            this.labelPath.Text = "Path";
            // 
            // labelPattern
            // 
            this.labelPattern.AutoSize = true;
            this.labelPattern.Location = new System.Drawing.Point(30, 52);
            this.labelPattern.Name = "labelPattern";
            this.labelPattern.Size = new System.Drawing.Size(41, 13);
            this.labelPattern.TabIndex = 4;
            this.labelPattern.Text = "Pattern";
            // 
            // FileScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 109);
            this.Controls.Add(this.labelPattern);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxPattern);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.scanButton);
            this.Name = "FileScanner";
            this.Text = "FileScanner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelPattern;
    }
}