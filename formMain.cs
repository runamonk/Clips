using System;
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

        private void loadConfig()
        {
            if (_Config == null)
            {
                _Config = new Config();
                _Config.ConfigChanged += new EventHandler(ConfigChanged);
            }

            RegisterHotKey(this.Handle, 1, _Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), _Config.PopupHotkey)).GetHashCode());
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            loadConfig();
        }

        private void clipBoard_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                AddItem(clipboard.ClipboardText);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                AddItem(clipboard.ClipboardImage);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                string s = string.Join(", ", clipboard.ClipboardFiles.Select(i => i.ToString()).ToArray());
                AddItem(s);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            _Config.ShowConfigForm();
        }

        private void menuMonitorClipboard_Click(object sender, EventArgs e)
        {
            clipboard.MonitorClipboard = menuMonitorClipboard.Checked;
        }

        #endregion
        
        // methods
        private void AddItem(string text)
        {

        }

        private void AddItem(Image image)
        {

        }
        private void LoadItems()
        {

        }
    } // formMain
    

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
            set { FullText = value; }
        }
    } // ClipItem
}
