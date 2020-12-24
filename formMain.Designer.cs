namespace Clips
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.notifyClips = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuNotifyMonitorClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboard = new WK.Libraries.SharpClipboardNS.SharpClipboard(this.components);
            this.pTop = new System.Windows.Forms.Panel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuMonitorClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.pMain = new System.Windows.Forms.Panel();
            this.pClips = new System.Windows.Forms.Panel();
            this.menuNotify.SuspendLayout();
            this.pTop.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.pMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyClips
            // 
            this.notifyClips.ContextMenuStrip = this.menuNotify;
            this.notifyClips.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyClips.Icon")));
            this.notifyClips.Text = "Clips";
            this.notifyClips.Visible = true;
            this.notifyClips.DoubleClick += new System.EventHandler(this.notifyClips_DoubleClick);
            // 
            // menuNotify
            // 
            this.menuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNotifyMonitorClipboard,
            this.notifyMenuSettings,
            this.notifyMenuClose});
            this.menuNotify.Name = "menuNotify";
            this.menuNotify.Size = new System.Drawing.Size(171, 70);
            // 
            // menuNotifyMonitorClipboard
            // 
            this.menuNotifyMonitorClipboard.Checked = true;
            this.menuNotifyMonitorClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuNotifyMonitorClipboard.Name = "menuNotifyMonitorClipboard";
            this.menuNotifyMonitorClipboard.Size = new System.Drawing.Size(170, 22);
            this.menuNotifyMonitorClipboard.Text = "Monitor clipboard";
            this.menuNotifyMonitorClipboard.Click += new System.EventHandler(this.menuMonitorClipboard_Click);
            // 
            // notifyMenuSettings
            // 
            this.notifyMenuSettings.Name = "notifyMenuSettings";
            this.notifyMenuSettings.Size = new System.Drawing.Size(170, 22);
            this.notifyMenuSettings.Text = "Settings";
            this.notifyMenuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // notifyMenuClose
            // 
            this.notifyMenuClose.Name = "notifyMenuClose";
            this.notifyMenuClose.Size = new System.Drawing.Size(170, 22);
            this.notifyMenuClose.Text = "Close";
            this.notifyMenuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // clipboard
            // 
            this.clipboard.MonitorClipboard = true;
            this.clipboard.ObservableFormats.All = true;
            this.clipboard.ObservableFormats.Files = true;
            this.clipboard.ObservableFormats.Images = true;
            this.clipboard.ObservableFormats.Others = true;
            this.clipboard.ObservableFormats.Texts = true;
            this.clipboard.ObserveLastEntry = false;
            this.clipboard.Tag = null;
            this.clipboard.ClipboardChanged += new System.EventHandler<WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs>(this.clipBoard_ClipboardChanged);
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.toolStripMain);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Margin = new System.Windows.Forms.Padding(0);
            this.pTop.Name = "pTop";
            this.pTop.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.pTop.Size = new System.Drawing.Size(800, 28);
            this.pTop.TabIndex = 3;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStripMain.Location = new System.Drawing.Point(5, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripMain.Size = new System.Drawing.Size(790, 28);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            this.toolStripMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripMain_MouseDown);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMonitorClipboard,
            this.menuSettings,
            this.menuClose});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(46, 25);
            this.toolStripDropDownButton1.Text = "Clips";
            // 
            // menuMonitorClipboard
            // 
            this.menuMonitorClipboard.Checked = true;
            this.menuMonitorClipboard.CheckOnClick = true;
            this.menuMonitorClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuMonitorClipboard.Name = "menuMonitorClipboard";
            this.menuMonitorClipboard.Size = new System.Drawing.Size(170, 22);
            this.menuMonitorClipboard.Text = "Monitor clipboard";
            this.menuMonitorClipboard.Click += new System.EventHandler(this.menuMonitorClipboard_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(170, 22);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(170, 22);
            this.menuClose.Text = "Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.pClips);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 28);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(800, 422);
            this.pMain.TabIndex = 4;
            // 
            // pClips
            // 
            this.pClips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pClips.Location = new System.Drawing.Point(0, 0);
            this.pClips.Name = "pClips";
            this.pClips.Size = new System.Drawing.Size(800, 422);
            this.pClips.TabIndex = 1;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Name = "formMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.formMain_Deactivate);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.menuNotify.ResumeLayout(false);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.pMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyClips;
        private WK.Libraries.SharpClipboardNS.SharpClipboard clipboard;
        private System.Windows.Forms.ContextMenuStrip menuNotify;
        private System.Windows.Forms.ToolStripMenuItem notifyMenuSettings;
        private System.Windows.Forms.ToolStripMenuItem notifyMenuClose;
        private System.Windows.Forms.ToolStripMenuItem menuNotifyMonitorClipboard;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem menuMonitorClipboard;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pClips;
    }
}

