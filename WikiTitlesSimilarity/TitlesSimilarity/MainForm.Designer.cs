namespace TitlesSimilarity
{
    partial class MainForm
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
            this.imageTitlesTxtBox = new System.Windows.Forms.TextBox();
            this.locationsCmbBx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.goBtn = new System.Windows.Forms.Button();
            this.opStatusLbl = new System.Windows.Forms.Label();
            this.wikiRequester = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // imageTitlesTxtBox
            // 
            this.imageTitlesTxtBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.imageTitlesTxtBox.Location = new System.Drawing.Point(12, 65);
            this.imageTitlesTxtBox.Multiline = true;
            this.imageTitlesTxtBox.Name = "imageTitlesTxtBox";
            this.imageTitlesTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.imageTitlesTxtBox.Size = new System.Drawing.Size(632, 470);
            this.imageTitlesTxtBox.TabIndex = 0;
            // 
            // locationsCmbBx
            // 
            this.locationsCmbBx.FormattingEnabled = true;
            this.locationsCmbBx.Location = new System.Drawing.Point(12, 25);
            this.locationsCmbBx.Name = "locationsCmbBx";
            this.locationsCmbBx.Size = new System.Drawing.Size(203, 21);
            this.locationsCmbBx.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select location";
            // 
            // goBtn
            // 
            this.goBtn.Location = new System.Drawing.Point(221, 25);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(75, 21);
            this.goBtn.TabIndex = 3;
            this.goBtn.Text = "Go!";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // opStatusLbl
            // 
            this.opStatusLbl.AutoSize = true;
            this.opStatusLbl.Location = new System.Drawing.Point(12, 49);
            this.opStatusLbl.Name = "opStatusLbl";
            this.opStatusLbl.Size = new System.Drawing.Size(0, 13);
            this.opStatusLbl.TabIndex = 4;
            // 
            // wikiRequester
            // 
            this.wikiRequester.WorkerReportsProgress = true;
            this.wikiRequester.WorkerSupportsCancellation = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 547);
            this.Controls.Add(this.opStatusLbl);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.locationsCmbBx);
            this.Controls.Add(this.imageTitlesTxtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Similar image titles searcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox imageTitlesTxtBox;
        private System.Windows.Forms.ComboBox locationsCmbBx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.Label opStatusLbl;
        private System.ComponentModel.BackgroundWorker wikiRequester;
    }
}

