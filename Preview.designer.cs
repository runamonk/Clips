namespace Clips
{
    partial class Preview
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
            this.PreviewImage = new System.Windows.Forms.PictureBox();
            this.PreviewText = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // timerShowForm
            // 
            this.timerShowForm.Interval = 500;
            this.timerShowForm.Tick += new System.EventHandler(this.TimerShowForm_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PreviewImage);
            this.panel1.Controls.Add(this.PreviewText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(600, 400);
            this.panel1.TabIndex = 1;
            // 
            // PreviewImage
            // 
            this.PreviewImage.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewImage.Location = new System.Drawing.Point(3, 3);
            this.PreviewImage.Name = "PreviewImage";
            this.PreviewImage.Size = new System.Drawing.Size(594, 394);
            this.PreviewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewImage.TabIndex = 4;
            this.PreviewImage.TabStop = false;
            this.PreviewImage.Visible = false;
            // 
            // PreviewText
            // 
            this.PreviewText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PreviewText.DetectUrls = false;
            this.PreviewText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewText.Location = new System.Drawing.Point(3, 3);
            this.PreviewText.Name = "PreviewText";
            this.PreviewText.ReadOnly = true;
            this.PreviewText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PreviewText.Size = new System.Drawing.Size(594, 394);
            this.PreviewText.TabIndex = 3;
            this.PreviewText.TabStop = false;
            this.PreviewText.Text = "";
            this.PreviewText.Visible = false;
            this.PreviewText.WordWrap = false;
            // 
            // Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.Name = "Preview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "formPreview";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerShowForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox PreviewText;
        private System.Windows.Forms.PictureBox PreviewImage;
    }
}