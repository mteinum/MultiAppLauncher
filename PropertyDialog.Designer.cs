namespace MultiAppLauncher
{
    partial class PropertyDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDelay = new System.Windows.Forms.TextBox();
            this.groupBoxSoftStart = new System.Windows.Forms.GroupBox();
            this.groupBoxSoftStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(197, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Delay in seconds";
            // 
            // textBoxDelay
            // 
            this.textBoxDelay.Location = new System.Drawing.Point(6, 32);
            this.textBoxDelay.Name = "textBoxDelay";
            this.textBoxDelay.Size = new System.Drawing.Size(100, 20);
            this.textBoxDelay.TabIndex = 2;
            this.textBoxDelay.Text = "0";
            // 
            // groupBoxSoftStart
            // 
            this.groupBoxSoftStart.Controls.Add(this.textBoxDelay);
            this.groupBoxSoftStart.Controls.Add(this.label1);
            this.groupBoxSoftStart.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSoftStart.Name = "groupBoxSoftStart";
            this.groupBoxSoftStart.Size = new System.Drawing.Size(260, 65);
            this.groupBoxSoftStart.TabIndex = 3;
            this.groupBoxSoftStart.TabStop = false;
            this.groupBoxSoftStart.Text = "Softstart";
            // 
            // PropertyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 119);
            this.Controls.Add(this.groupBoxSoftStart);
            this.Controls.Add(this.button1);
            this.Name = "PropertyDialog";
            this.Text = "Properties";
            this.groupBoxSoftStart.ResumeLayout(false);
            this.groupBoxSoftStart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDelay;
        private System.Windows.Forms.GroupBox groupBoxSoftStart;
    }
}