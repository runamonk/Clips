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
            this.PreviewImage = new System.Windows.Forms.PictureBox();
            this.PreviewText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // TimerShowForm
            // 
            this.TimerShowForm.Interval = 500;
            this.TimerShowForm.Tick += new System.EventHandler(this.TimerShowForm_Tick);
            // 
            // PreviewImage
            // 
            this.PreviewImage.BackColor = System.Drawing.Color.Red;
            this.PreviewImage.Location = new System.Drawing.Point(10, 3);
            this.PreviewImage.Margin = new System.Windows.Forms.Padding(0);
            this.PreviewImage.Name = "PreviewImage";
            this.PreviewImage.Size = new System.Drawing.Size(11, 17);
            this.PreviewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewImage.TabIndex = 7;
            this.PreviewImage.TabStop = false;
            this.PreviewImage.Visible = false;
            // 
            // PreviewText
            // 
            this.PreviewText.AutoSize = true;
            this.PreviewText.BackColor = System.Drawing.Color.Blue;
            this.PreviewText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviewText.ForeColor = System.Drawing.Color.White;
            this.PreviewText.Location = new System.Drawing.Point(30, 3);
            this.PreviewText.Margin = new System.Windows.Forms.Padding(0);
            this.PreviewText.Name = "PreviewText";
            this.PreviewText.Size = new System.Drawing.Size(11, 17);
            this.PreviewText.TabIndex = 8;
            this.PreviewText.Text = "T";
            this.PreviewText.UseCompatibleTextRendering = true;
            this.PreviewText.UseMnemonic = false;
            // 
            // Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(100, 100);
            this.ControlBox = false;
            this.Controls.Add(this.PreviewText);
            this.Controls.Add(this.PreviewImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(100, 100);
            this.MinimizeBox = false;
            this.Name = "Preview";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer TimerShowForm;
        private System.Windows.Forms.PictureBox PreviewImage;
        private System.Windows.Forms.Label PreviewText;
    }
}