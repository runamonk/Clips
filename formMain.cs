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
using System.IO;
using System.Xml;
using Utility;
using System.Drawing.Imaging;

namespace Clips
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // allow form to be dragged.
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        Config _Config;
        formPreview _formPreview = new formPreview();
        private bool _InPreview = false;

        #region Events
        private void ConfigChanged(object sender, EventArgs e)
        {
            loadConfig();
        }

        private void clipBoard_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                addItem(clipboard.ClipboardText, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                addItem(clipboard.ClipboardImage, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                string s = string.Join(", ", clipboard.ClipboardFiles.Select(i => i.ToString()).ToArray());
                addItem(s, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            loadConfig();
            loadItems();
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

        private void toolStripMain_MouseDown(object sender, MouseEventArgs e)
        {
            // drag form.
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
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

        string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" TYPE=\"{1}\">{2}\r\n</DATA>";
 
        // methods
        private void addItem(string text, bool saveToDisk = false)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(plainTextBytes);
            if (saveToDisk)
                Funcs.SaveToCache(string.Format(new_xml_file, "N", "TEXT", base64));

            ClipButton b = new ClipButton();
            b.Parent = pClips;
            b.Dock = DockStyle.Top;
            b.Height = 20;
            //b.MouseUp += new MouseEventHandler(Button_Click);
            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);

            //b.BackColor = ControlPaint.Dark(_Config.BackColor, 75);
            //b.BackColor = _Config.BackColor;
            //b.ForeColor = _Config.FontColor;

            b.FlatAppearance.BorderSize = 0;
            b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            b.TextAlign = ContentAlignment.TopLeft;
            b.AutoEllipsis = false;
            //b.ContextMenuStrip = menuClips;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            var result = text.TrimStart().Split(new string[] { "\\n" }, StringSplitOptions.None);
 
            b.FullText = text;
            b.Text = result[0];

            // if (Uri.IsWellFormedUriString(Text, UriKind.Absolute))
            //   b.Font = new Font (b.Font, FontStyle.Underline);
            //b.Image = Image.GetThumbnailImage(_Config.DefaultClipHeight - 5, _Config.DefaultClipHeight - 5, null, IntPtr.Zero);
        }

        private void addItem(Image image, bool saveToDisk = false)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            string base64 = Convert.ToBase64String(ms.ToArray());
            if (saveToDisk)
                Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));
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
            string[] files = Funcs.GetFiles(Funcs.AppPath() + "\\Cache", "*.xml");
            foreach (string file in files)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNode data = doc.DocumentElement.SelectSingleNode("/DATA");
                string type = data.Attributes["TYPE"]?.InnerText;

                if (type == "IMAGE")
                {
                    MemoryStream ms = new MemoryStream(Convert.FromBase64String(data.InnerText));
                    try
                    {
                        Bitmap img = new Bitmap(ms);
                        addItem(img);
                        if (img != null) img = null;
                    }
                    finally
                    {
                        ms.Dispose();
                    }
                }
                else
                {
                    var base64EncodedBytes = Convert.FromBase64String(data.InnerText);
                    string decodedString = Encoding.UTF8.GetString(base64EncodedBytes);
                    addItem(decodedString);
                }
                doc = null;
            }

        }

        private void PreviewHide(object sender, EventArgs e)
        {
            _formPreview.HidePreview();
            _InPreview = false;
        }

        private void PreviewShow(object sender, EventArgs e)
        {
            _InPreview = true;
            _formPreview.BackColor = _Config.BackColor;
            _formPreview.ForeColor = _Config.FontColor;
            _formPreview.ShowPreview(((ClipButton)sender).FullText, ((ClipButton)sender).FullImage, _Config.PreviewPopupDelay);
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
                Activate();
            }
        }


    } // formMain

    // ClipButton
    public partial class ClipButton : Button
    {
        public ClipButton()
        {

        }

        // Stops the black default border from being displayed on button when the preview form is shown.
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
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
    } // ClipButton
}
