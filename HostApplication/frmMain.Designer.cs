namespace HostApplication
{
    partial class frmMain
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
            this.btnMerge = new System.Windows.Forms.Button();
            this.tbTemplateFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenTemplateFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbResultsFolder = new System.Windows.Forms.TextBox();
            this.btnOpenResultsFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(32, 175);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(185, 54);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "Merge Documents";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // tbTemplateFolder
            // 
            this.tbTemplateFolder.Location = new System.Drawing.Point(29, 49);
            this.tbTemplateFolder.Name = "tbTemplateFolder";
            this.tbTemplateFolder.Size = new System.Drawing.Size(420, 20);
            this.tbTemplateFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Template folder:";
            // 
            // btnOpenTemplateFolder
            // 
            this.btnOpenTemplateFolder.Location = new System.Drawing.Point(455, 47);
            this.btnOpenTemplateFolder.Name = "btnOpenTemplateFolder";
            this.btnOpenTemplateFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenTemplateFolder.TabIndex = 3;
            this.btnOpenTemplateFolder.Text = "Open...";
            this.btnOpenTemplateFolder.UseVisualStyleBackColor = true;
            this.btnOpenTemplateFolder.Click += new System.EventHandler(this.btnOpenTemplateFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destination folder:";
            // 
            // tbResultsFolder
            // 
            this.tbResultsFolder.Location = new System.Drawing.Point(29, 117);
            this.tbResultsFolder.Name = "tbResultsFolder";
            this.tbResultsFolder.Size = new System.Drawing.Size(420, 20);
            this.tbResultsFolder.TabIndex = 4;
            // 
            // btnOpenResultsFolder
            // 
            this.btnOpenResultsFolder.Location = new System.Drawing.Point(455, 115);
            this.btnOpenResultsFolder.Name = "btnOpenResultsFolder";
            this.btnOpenResultsFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenResultsFolder.TabIndex = 6;
            this.btnOpenResultsFolder.Text = "Open...";
            this.btnOpenResultsFolder.UseVisualStyleBackColor = true;
            this.btnOpenResultsFolder.Click += new System.EventHandler(this.btnOpenResultsFolder_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 256);
            this.Controls.Add(this.btnOpenResultsFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbResultsFolder);
            this.Controls.Add(this.btnOpenTemplateFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTemplateFolder);
            this.Controls.Add(this.btnMerge);
            this.Name = "frmMain";
            this.Text = "Test Host Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.TextBox tbTemplateFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenTemplateFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbResultsFolder;
        private System.Windows.Forms.Button btnOpenResultsFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

