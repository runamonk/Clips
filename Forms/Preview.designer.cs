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
            this.TimerShowForm = new System.Windows.Forms.Timer(this.components);
            this.MainPanel = new System.Windows.Forms.Panel();
            this.PreviewImage = new System.Windows.Forms.PictureBox();
            this.PreviewText = new System.Windows.Forms.RichTextBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // TimerShowForm
            // 
            this.TimerShowForm.Interval = 500;
            this.TimerShowForm.Tick += new System.EventHandler(this.TimerShowForm_Tick);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.PreviewImage);
            this.MainPanel.Controls.Add(this.PreviewText);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(3);
            this.MainPanel.Size = new System.Drawing.Size(50, 25);
            this.MainPanel.TabIndex = 1;
            // 
            // PreviewImage
            // 
            this.PreviewImage.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewImage.Location = new System.Drawing.Point(3, 3);
            this.PreviewImage.Name = "PreviewImage";
            this.PreviewImage.Size = new System.Drawing.Size(44, 19);
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
            this.PreviewText.Size = new System.Drawing.Size(44, 19);
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
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(50, 25);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(9000, 9000);
            this.MinimizeBox = false;
            this.Name = "Preview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.VisibleChanged += new System.EventHandler(this.Preview_VisibleChanged);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TimerShowForm;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.RichTextBox PreviewText;
        private System.Windows.Forms.PictureBox PreviewImage;
    }
}