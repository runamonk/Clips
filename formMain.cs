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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            loadConfig();
        }

        private void clipBoard_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                // Get the cut/copied text.
                string s = clipboard.ClipboardText;

                //MessageBox.Show(s.Substring(1, 50));
            }

            // Is the content copied of image type?
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                // Get the cut/copied image.
                Image img = clipboard.ClipboardImage;
            }

            // Is the content copied of file type?
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                // Get the cut/copied file/files.
                MessageBox.Show(clipboard.ClipboardFiles.ToString());
            }

            // If the cut/copied content is complex, use 'Other'.
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _Config.ShowConfigForm();
        }
    }
}
