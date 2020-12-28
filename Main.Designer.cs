namespace Clips
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
            this.menuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuNotifyMonitorClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboard = new WK.Libraries.SharpClipboardNS.SharpClipboard(this.components);
            this.pTop = new System.Windows.Forms.Panel();
            this.pMain = new System.Windows.Forms.Panel();
            this.pClips = new System.Windows.Forms.Panel();
            this.menuClips = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMain.SuspendLayout();
            this.pMain.SuspendLayout();
            this.menuClips.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyClips
            // 
            this.notifyClips.ContextMenuStrip = this.menuMain;
            this.notifyClips.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyClips.Icon")));
            this.notifyClips.Text = "Clips";
            this.notifyClips.Visible = true;
            this.notifyClips.DoubleClick += new System.EventHandler(this.NotifyClips_DoubleClick);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNotifyMonitorClipboard,
            this.notifyMenuSettings,
            this.menuAbout,
            this.toolStripSeparator1,
            this.notifyMenuClose});
            this.menuMain.Name = "menuNotify";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuMain.Size = new System.Drawing.Size(181, 120);
            // 
            // menuNotifyMonitorClipboard
            // 
            this.menuNotifyMonitorClipboard.Checked = true;
            this.menuNotifyMonitorClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuNotifyMonitorClipboard.Name = "menuNotifyMonitorClipboard";
            this.menuNotifyMonitorClipboard.Size = new System.Drawing.Size(180, 22);
            this.menuNotifyMonitorClipboard.Text = "&Monitor clipboard";
            this.menuNotifyMonitorClipboard.Click += new System.EventHandler(this.MenuMonitorClipboard_Click);
            // 
            // notifyMenuSettings
            // 
            this.notifyMenuSettings.Name = "notifyMenuSettings";
            this.notifyMenuSettings.Size = new System.Drawing.Size(180, 22);
            this.notifyMenuSettings.Text = "&Settings";
            this.notifyMenuSettings.Click += new System.EventHandler(this.MenuSettings_Click);
            // 
            // notifyMenuClose
            // 
            this.notifyMenuClose.Name = "notifyMenuClose";
            this.notifyMenuClose.Size = new System.Drawing.Size(180, 22);
            this.notifyMenuClose.Text = "&Close";
            this.notifyMenuClose.Click += new System.EventHandler(this.MenuClose_Click);
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
            this.clipboard.ClipboardChanged += new System.EventHandler<WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs>(this.ClipBoard_ClipboardChanged);
            // 
            // pTop
            // 
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Margin = new System.Windows.Forms.Padding(0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(184, 28);
            this.pTop.TabIndex = 3;
            this.pTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTop_MouseDown);
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.pClips);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 28);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(184, 56);
            this.pMain.TabIndex = 4;
            // 
            // pClips
            // 
            this.pClips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pClips.Location = new System.Drawing.Point(0, 0);
            this.pClips.Name = "pClips";
            this.pClips.Size = new System.Drawing.Size(184, 56);
            this.pClips.TabIndex = 1;
            // 
            // menuClips
            // 
            this.menuClips.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSave,
            this.menuDelete});
            this.menuClips.Name = "menuClips";
            this.menuClips.Size = new System.Drawing.Size(108, 48);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(107, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.MenuClips_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(107, 22);
            this.menuDelete.Text = "Delete";
            this.menuDelete.Click += new System.EventHandler(this.MenuClips_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(180, 22);
            this.menuAbout.Text = "&About";
            this.menuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 84);
            this.ControlBox = false;
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "Main";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clips";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.menuMain.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.menuClips.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyClips;
        private WK.Libraries.SharpClipboardNS.SharpClipboard clipboard;
        private System.Windows.Forms.ContextMenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem notifyMenuSettings;
        private System.Windows.Forms.ToolStripMenuItem notifyMenuClose;
        private System.Windows.Forms.ToolStripMenuItem menuNotifyMonitorClipboard;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pClips;
        private System.Windows.Forms.ContextMenuStrip menuClips;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

