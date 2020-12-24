﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS;
using System.Runtime.InteropServices;

namespace Clips
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        Config _Config;

        #region Events
        private void ConfigChanged(object sender, EventArgs e)
        {
            loadConfig();
        }

        private void clipBoard_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                addItem(clipboard.ClipboardText);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                addItem(clipboard.ClipboardImage);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                string s = string.Join(", ", clipboard.ClipboardFiles.Select(i => i.ToString()).ToArray());
                addItem(s);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
            }
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show(sender.ToString());
            e.Cancel = true;
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            loadConfig();
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            closeForm();
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            _Config.ShowConfigForm();
        }

        private void menuMonitorClipboard_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            clipboard.MonitorClipboard = item.Checked;

            if (item != menuMonitorClipboard)
                menuNotifyMonitorClipboard.Checked = item.Checked;
            else
                menuMonitorClipboard.Checked = item.Checked;
        }

        private void notifyClips_DoubleClick(object sender, EventArgs e)
        {
            toggleShow();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            Opacity = 0;
            base.OnLoad(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) //WM_HOTKEY
            {
                toggleShow();
            }
            base.WndProc(ref m);
        }
        #endregion

        // methods
        private void addItem(string text)
        {

        }

        private void addItem(Image image)
        {

        }

        private void closeForm()
        {
            Close();
        }

        private void loadConfig()
        {
            if (_Config == null)
            {
                _Config = new Config();
                _Config.ConfigChanged += new EventHandler(ConfigChanged);
            }

            RegisterHotKey(this.Handle, 1, _Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), _Config.PopupHotkey)).GetHashCode());
        }

        private void loadItems()
        {

        }

        private void toggleShow()
        {
            // for some reason during form closing event the opacity is set to 1.
            if ((Visible) && (Opacity == 100) || (Opacity == 1))
            {
                Visible = false;
                Opacity = 0;
            }
                
            else
            {
                Opacity = 100;
                Visible = true;
            }
        }


    } // formMain

    // ClipItem
    public partial class ClipItem: ListViewItem
    {
        public ClipItem()
        {

        }

        private Image fullImage;
        public Image FullImage
        {
            get { return fullImage; }
            set { fullImage = value; }
        }

        private string fullText;
        public string FullText
        {
            get { return fullText; }
            set { fullText = value; }
        }
    } // ClipItem
}
