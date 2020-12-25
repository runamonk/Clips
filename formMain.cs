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
        formPreview formPreview = new formPreview();
        private bool inPreview = false;
        private bool inClose = false;
        private bool inSettings = false;
        Image lastImage = null;
        string lastText = null;
        
        #region Events
        private void ConfigChanged(object sender, EventArgs e)
        {
            loadConfig();
            cleanupCache();
        }

        private void clipBoard_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {    
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                addItem(clipboard.ClipboardText, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                addItem(clipboard.ClipboardImage, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                string s = string.Join(", ", clipboard.ClipboardFiles.Select(i => i.ToString()).ToArray());
                addItem(s, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
            }
        }

        private void ClipsButtonClick(Object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) || ((e.Button == MouseButtons.Middle) && (((ClipButton)sender).FullImage != null)))
                return;

            //DoSwapVisibility();
            //// Copy to clipboard
            //// Remove old clip (shift it to the top)

            //if (((ClipsButton)sender).FullImage != null)
            //{
            //    Image i = ((ClipsButton)sender).FullImage;
            //    panelClips.Controls.Remove((ClipsButton)sender);
            //    Clipboard.SetImage(i);
            //}
            //else
            //    if (((ClipsButton)sender).Text != "")
            //{
            //    string s = ((ClipsButton)sender).Text;
            //    panelClips.Controls.Remove((ClipsButton)sender);
            //    Clipboard.SetText(s);

            //    if (((Form.ModifierKeys == Keys.Shift) || (e.Button == MouseButtons.Middle)) && (Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute) == true))
            //    {
            //        System.Diagnostics.Process.Start(s);
            //    }
            //}

            //DoSetSize();
            //DoSaveClips();
        }

        private void formMain_Deactivate(object sender, EventArgs e)
        {
            if ((Visible == true) && (inPreview == false) && (!inSettings))
                toggleShow();
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            inClose = true;
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            loadConfig();
            loadItems();
        }

        private void menuClips_Click(object sender, EventArgs e)
        {
            inPreview = true;

            if (sender == menuSave)
            {
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.InitialDirectory = "c:\\";

                if (((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).Text != "")
                {
                    dlg.Filter = "Text (*.txt)|Any (*.*)";
                    dlg.FilterIndex = 1;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        System.IO.File.WriteAllText(dlg.FileName, ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).Text);
                }
                else
                    if (((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).FullImage != null)
                {
                    dlg.Filter = "Picture (*.png)|Jpeg (*.jpg)";
                    dlg.FilterIndex = 1;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).FullImage.Save(dlg.FileName);
                }
            }
            else
            if (sender == menuDelete)
            {
                ClipButton b = ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
                if (File.Exists(b.FileName))
                    File.Delete(b.FileName);
                pClips.Controls.Remove(b);

            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            inSettings = true;
            _Config.ShowConfigForm();
            inSettings = false;
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
        private void addItem(string text, string fileName, bool saveToDisk = false)
        {
            if (text == lastText) return;

            lastText = text;
            ClipButton b = newClipButton();
            b.FullText = text;

            var plainTextBytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(plainTextBytes);
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "TEXT", base64));
            else
                b.FileName = fileName;

            var result = text.TrimStart().Split(new string[] { "\\n" }, StringSplitOptions.None);
            // if (Uri.IsWellFormedUriString(Text, UriKind.Absolute))
            //   b.Font = new Font (b.Font, FontStyle.Underline);
            // TODO set height based on number of lines to show.
            b.Text = result[0];
        }

        private void addItem(Image image, string fileName, bool saveToDisk = false)
        {           
            if ((lastImage != null) && (image.Size == lastImage.Size)) return;
            lastImage = image;

            ClipButton b = newClipButton();
            b.Height = 60;
            b.FullImage = image;

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            string base64 = Convert.ToBase64String(ms.ToArray());
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));
            else
                b.FileName = fileName;
                        
            b.Image = image.GetThumbnailImage(60, 60, null, IntPtr.Zero);
        }

        private void cleanupCache()
        {
            if (pClips.Controls.Count >= _Config.MaxClips)
            {
                int clipsToDelete = (pClips.Controls.Count - _Config.MaxClips);
                while (clipsToDelete > 0)
                {
                    deleteOldestClip();
                    clipsToDelete--;
                }
            }
        }

        private void deleteOldestClip()
        {
            ClipButton cb = ((ClipButton)pClips.Controls[pClips.Controls.Count - 1]);
            if (File.Exists(cb.FileName))
                File.Delete(cb.FileName);
            pClips.Controls.RemoveAt(pClips.Controls.Count - 1);
        }

        private void loadConfig()
        {
            if (_Config == null)
            {
                _Config = new Config();
                _Config.ConfigChanged += new EventHandler(ConfigChanged);
            }
            pClips.AutoScroll = true;
            pClips.VerticalScroll.Visible = true;
            RegisterHotKey(this.Handle, 1, _Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), _Config.PopupHotkey)).GetHashCode());
        }

        private void loadItems()
        {
            SuspendLayout();
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
                        addItem(img, file, false);
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
                    addItem(decodedString, file, false);
                }
                doc = null;
            }
            ResumeLayout();

            //pClips.AutoScrollPosition = new Point(pClips.AutoScrollPosition.X, 0);
            pClips.VerticalScroll.Value = 0;
        }

        private ClipButton newClipButton()
        {
            if (pClips.Controls.Count >= _Config.MaxClips)
                deleteOldestClip();

            ClipButton b = new ClipButton();
            b.TabStop = false;
            b.Parent = pClips;
            b.Dock = DockStyle.Top;
            b.Height = 20;
            b.MouseUp += new MouseEventHandler(ClipsButtonClick);
            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);

            //b.BackColor = ControlPaint.Dark(_Config.BackColor, 75);
            //b.BackColor = _Config.BackColor;
            //b.ForeColor = _Config.FontColor;

            b.FlatAppearance.BorderSize = 0;
            b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            b.TextAlign = ContentAlignment.TopLeft;
            b.AutoEllipsis = false;
            b.ContextMenuStrip = menuClips;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            return b;
        }

        private void PreviewHide(object sender, EventArgs e)
        {
            formPreview.HidePreview();
            inPreview = false;
        }

        private void PreviewShow(object sender, EventArgs e)
        {
            inPreview = true;
            ((ClipButton)sender).Select();

            formPreview.BackColor = _Config.BackColor;
            formPreview.ForeColor = _Config.FontColor;
            formPreview.ShowPreview(((ClipButton)sender).FullText, ((ClipButton)sender).FullImage, _Config.PreviewPopupDelay);
        }

        private void toggleShow()
        {
            if (inClose) return;

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