namespace Clips
{
    partial class formPreview
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
            this.components = new System.ComponentModel.Container();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.timerShowForm = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.textPreview = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(584, 329);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            // 
            // timerShowForm
            // 
            this.timerShowForm.Interval = 500;
            this.timerShowForm.Tick += new System.EventHandler(this.timerShowForm_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(584, 329);
            this.panel1.TabIndex = 1;
            // 
            // textPreview
            // 
            this.textPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textPreview.DetectUrls = false;
            this.textPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textPreview.Location = new System.Drawing.Point(3, 3);
            this.textPreview.Name = "textPreview";
            this.textPreview.ReadOnly = true;
            this.textPreview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textPreview.Size = new System.Drawing.Size(578, 323);
            this.textPreview.TabIndex = 3;
            this.textPreview.TabStop = false;
            this.textPreview.Text = "";
            this.textPreview.WordWrap = false;
            // 
            // formPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(584, 329);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 400);
            this.MinimizeBox = false;
            this.Name = "formPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "formPreview";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Timer timerShowForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox textPreview;
    }
}