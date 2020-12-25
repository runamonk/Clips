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
            this.timerShowForm = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.textPreview = new System.Windows.Forms.RichTextBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // timerShowForm
            // 
            this.timerShowForm.Interval = 500;
            this.timerShowForm.Tick += new System.EventHandler(this.timerShowForm_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbPreview);
            this.panel1.Controls.Add(this.textPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(600, 400);
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
            this.textPreview.Size = new System.Drawing.Size(594, 394);
            this.textPreview.TabIndex = 3;
            this.textPreview.TabStop = false;
            this.textPreview.Text = "";
            this.textPreview.Visible = false;
            this.textPreview.WordWrap = false;
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.SystemColors.Control;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(3, 3);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(594, 394);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 4;
            this.pbPreview.TabStop = false;
            this.pbPreview.Visible = false;
            // 
            // formPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.panel1);
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
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerShowForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox textPreview;
        private System.Windows.Forms.PictureBox pbPreview;
    }
}