﻿namespace Clips
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notifyClips = new System.Windows.Forms.NotifyIcon(this.components);
            this.pTop = new System.Windows.Forms.Panel();
            this.pMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // notifyClips
            // 
            this.notifyClips.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyClips.Icon")));
            this.notifyClips.Text = "zuul Clips";
            this.notifyClips.Visible = true;
            this.notifyClips.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyClips_MouseDoubleClick);
            // 
            // pTop
            // 
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Margin = new System.Windows.Forms.Padding(0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(200, 28);
            this.pTop.TabIndex = 3;
            // 
            // pMain
            // 
            this.pMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 28);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(200, 67);
            this.pMain.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 95);
            this.ControlBox = false;
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(9999, 9999);
            this.MinimumSize = new System.Drawing.Size(200, 95);
            this.Name = "Main";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clips";
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.VisibleChanged += new System.EventHandler(this.Main_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Main_KeyPress);
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyClips;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pMain;
    }
}

