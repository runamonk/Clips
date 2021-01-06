using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using Utility;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Reflection;
using Clips.Controls;

namespace Clips
{
    public partial class Main : Form
    {
        public Main()
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

        private About AboutForm = new About();
        private ClipButton MenuMainButton { get; set; }
        private ClipMenu MenuMain { get; set; }
        private ClipMenu MenuRC { get; set; }
        private Config Config { get; set; }
        private bool inAbout = false;
        private bool inClose = false;
        private bool inMenu = false;
        private bool inLoad = false;
        private bool inPreview = false;
        private bool inSettings = false;
        private Image LastImage { get; set; }
        private string LastText { get; set; }
        private Preview PreviewForm = new Preview();
        private SharpClipboard clipboard;
        
        // TODO Add ability to pin a clip.
        // TODO Add support for actually clipping the files from a list of files.
        // TODO Add edit/favorite text editor in config.
        // TODO Add Search.
        // TODO Add button pad amount that way users can decide how much to pad the ClipButton, rather than just using an arbritrary 8px.
        // TODO Add max form height (to work with auto-size).
        // TODO Change scrollbar colors to match themes.

        #region Events
        private void ConfigChanged(object sender, EventArgs e)
        {
            LoadConfig();
            CleanupCache();
            LoadItems();
            AutoSizeForm(true);
        }

        private void ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {    
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                AddItem(clipboard.ClipboardText.Trim(), null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                AddItem(clipboard.ClipboardImage, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                string s = string.Join("\n", clipboard.ClipboardFiles.Select(i => i.ToString()).ToArray());
                AddItem(s, null, true);
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'clipboard.ClipboardObject' or 'e.Content' here...
               
                AddItem(clipboard.ClipboardObject.ToString(), null, true);
            }
        }

        private void ClipsButtonClick(Object sender, MouseEventArgs e)
        {
            
            void deleteClip()
            {
                string fileName = ((ClipButton)sender).FileName;
                if (File.Exists(fileName))
                    File.Delete(fileName);
                ClipButton b = (ClipButton)sender;

                pClips.Controls.Remove(b);

                if (Funcs.IsSame(b.FullImage, LastImage))
                    LastImage = null;
                else
                if (b.FullText == LastText)
                    LastText = null;
                GC.Collect();
            }
            
            if ((e.Button == MouseButtons.Right) || ((e.Button == MouseButtons.Middle) && (((ClipButton)sender).FullImage != null)))
                return;

            SuspendLayout();

            if (Config.AutoHide)
                ToggleShow(true, true);

            bool skipShiftToTop = false;

            // Don't delete and read the most recent clip, it ends up just looking like a flicker.
            if (((ClipButton)sender) == pClips.Controls[pClips.Controls.Count - 1])
                skipShiftToTop = true;

            if (((ClipButton)sender).FullImage != null)
            {
                LastImage = null;
                Image i = ((ClipButton)sender).FullImage;
                if (skipShiftToTop)
                    LastImage = i;
                else
                    deleteClip();
                Clipboard.SetImage(i);
            }
            else
            if (((ClipButton)sender).Text != "")
            {
                string s = ((ClipButton)sender).FullText;
                if ((Form.ModifierKeys == Keys.Control) && (e.Button == MouseButtons.Left) && Funcs.IsUrl(s))
                {
                    System.Diagnostics.Process.Start(s);
                }
                else
                {
                    LastText = null;
                    if (skipShiftToTop)
                        LastText = s;
                    else
                        deleteClip();
                    Clipboard.SetText(s);
                }
            }

            ResumeLayout();           
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            if (Opacity > 0)
                ToggleShow();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            inClose = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadConfig();
            LoadItems();
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            Config.FormSize = Size;
            Config.FormTop = Top;
            Config.FormLeft = Left;
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            inAbout = true;
            AboutForm.Show(this);
            inAbout = false;
        }

        private void MenuClips_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            inMenu = false;
        }

        private void MenuClips_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            inMenu = true;
        }

        private void MenuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            inMenu = true;
            ClipButton b = ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
            if (File.Exists(b.FileName))
                File.Delete(b.FileName);

            if (Funcs.IsSame(b.FullImage, LastImage))
                LastImage = null;
            else
            if (b.FullText == LastText)
                LastText = null;

            pClips.Controls.Remove(b);
            GC.Collect();
            AutoSizeForm(false);
            inMenu = false;
        }

        private void MenuMonitorClipboard_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            bool b = false;
            if (item.Checked)
                b = false;
            else
                b = true;
            item.Checked = b;
            clipboard.MonitorClipboard = b;
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            inMenu = true;
            SaveFileDialog dlg = new SaveFileDialog
            {
                InitialDirectory = "c:\\"
            };

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
            inMenu = false;
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            inSettings = true;
            Config.ShowConfigForm((Opacity > 1));
            inSettings = false;
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            Button b = ((Button)sender);
            MenuMain.Show(b.Left + b.Width + Left, b.Top + b.Height + Top);
        }

        private void NotifyClips_DoubleClick(object sender, EventArgs e)
        {
            ToggleShow(false,false);
        }

        private void PTop_MouseDown(object sender, MouseEventArgs e)
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
            if (RunningInstance() != null)
            {
                MessageBox.Show("There is already a version of Clips running.");
                Application.Exit();
            }
            else
                base.OnLoad(e);
        }

        private void PreviewHide(object sender, EventArgs e)
        {
            PreviewForm.HidePreview();
            inPreview = false;
        }

        private void PreviewShow(object sender, EventArgs e)
        {
            if (inAbout) return;

            inPreview = true;
            ((ClipButton)sender).Select();

            PreviewForm.BackColor = Config.PreviewBackColor;
            PreviewForm.ForeColor = Config.PreviewFontColor;
            PreviewForm.ShowPreview(((ClipButton)sender).FullText, ((ClipButton)sender).FullImage, Config.PreviewPopupDelay, Config.PreviewMaxLines);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) //WM_HOTKEY
            {
                ToggleShow();
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Methods
        private string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" TYPE=\"{1}\">{2}\r\n</DATA>";

        private ClipButton AddClipButton()
        {
            if (pClips.Controls.Count >= Config.ClipsMaxClips)
                DeleteOldestClip();

            ClipButton b = new ClipButton
            {
                TabStop = false,
                Dock = DockStyle.Top,
                BackColor = Config.ClipsRowBackColor,
                ForeColor = Config.ClipsFontColor,
                FlatStyle = FlatStyle.Flat
            };

            b.FlatAppearance.BorderColor = BackColor;
            b.MouseUp += new MouseEventHandler(ClipsButtonClick);
            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);
            b.ContextMenuStrip = MenuRC;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            b.Parent = pClips;

            return b;
        }

        private void AddItem(string text, string fileName, bool saveToDisk = false)
        {
            if (text == LastText) return;

            LastText = text;
            ClipButton b = AddClipButton();
            
            b.AutoSize = false;
            b.AutoEllipsis = false;
            b.FullText = text;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(plainTextBytes);
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "TEXT", base64));
            else
                b.FileName = fileName;

            //TODO Come up with a better way to handle displaying multiple lines per ClipButton
            string[] s = text.TrimStart().Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.None);
            if (s.Count() >= Config.ClipsLinesPerRow)
                for (int i = 0; i < Config.ClipsLinesPerRow; i++)
                {
                    if (string.IsNullOrEmpty(b.Text))
                        b.Text = s[i] + "\n";
                    else
                        b.Text = b.Text + s[i] + "\n";
                }
            else
                b.Text = text;

            SizeF ss = TextRenderer.MeasureText("X", b.Font);
            int FHeight = Convert.ToInt32(ss.Height);

            b.Height = (s.Count() > 0 && s.Count() >= Config.ClipsLinesPerRow ? Config.ClipsLinesPerRow * FHeight + 8 : FHeight + 8);
            AutoSizeForm(true);
        }

        private void AddItem(Image image, string fileName, bool saveToDisk = false)
        {           
            if ((LastImage != null) && Funcs.IsSame(image, LastImage)) return;
            
            LastImage = image;

            ClipButton b = AddClipButton();
            b.Height = 60;
            b.FullImage = image;

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            string base64 = Convert.ToBase64String(ms.ToArray());
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "IMAGE", base64));
            else
                b.FileName = fileName;
            // TODO DEFAULT IMAGE THUMBNAIL SIZE.             
            b.Image = image.GetThumbnailImage(60, 60, null, IntPtr.Zero);
            AutoSizeForm(true);
        }

        private void AutoSizeForm(bool ScrollToTop)
        {
            if (inLoad) return;

            if (Config.AutoSizeHeight)
            {
                int c = 68;
                for (int i = 0; i <= pClips.Controls.Count - 1; i++)
                {
                    c = c + pClips.Controls[i].Height;
                }

                if (c < MaximumSize.Height)
                    Height = c;
                else
                    Height = MaximumSize.Height;
            }

            // select the first control.
            if (pClips.Controls.Count > 0)
                pClips.Controls[pClips.Controls.Count-1].Select();
        }

        private void CleanupCache()
        {
            if (pClips.Controls.Count >= Config.ClipsMaxClips)
            {
                int clipsToDelete = (pClips.Controls.Count-Config.ClipsMaxClips);
                while (clipsToDelete > 0)
                {
                    DeleteOldestClip();
                    clipsToDelete--;
                }
            }
        }

        private void DeleteOldestClip()
        {
            ClipButton cb = ((ClipButton)pClips.Controls[0]);
            if (File.Exists(cb.FileName))
                File.Delete(cb.FileName);
            pClips.Controls.RemoveAt(0);
        }

        private void LoadConfig()
        {
            if (Config == null)
            {
                Config = new Config();
                Config.ConfigChanged += new EventHandler(ConfigChanged);
                MenuMain = new ClipMenu(Config);
                MenuMain.Opening += new System.ComponentModel.CancelEventHandler(MenuClips_Opening);
                MenuMain.Closed += new ToolStripDropDownClosedEventHandler(MenuClips_Closed);
                ToolStripMenuItem t;

                t = new ToolStripMenuItem("&Monitor Clipboard");
                t.Checked = true;
                t.CheckState = CheckState.Checked;
                t.Click += new EventHandler(MenuMonitorClipboard_Click);
                MenuMain.Items.Add(t);

                t = new ToolStripMenuItem("&Settings");
                t.Click += new EventHandler(MenuSettings_Click);
                MenuMain.Items.Add(t);

                t = new ToolStripMenuItem("&Close");
                t.Click += new EventHandler(MenuClose_Click);
                MenuMain.Items.Add(t);

                MenuRC = new ClipMenu(Config);
                t = new ToolStripMenuItem("&Save");
                t.Click += new EventHandler(MenuSave_Click);
                MenuRC.Items.Add(t);

                t = new ToolStripMenuItem("&Delete");
                t.Click += new EventHandler(MenuDelete_Click);
                MenuRC.Items.Add(t);
                
                MenuMainButton = new ClipButton
                {
                    Text = "...",
                    Width = 25,
                    Parent = pTop,
                    Dock = DockStyle.Left
                };
                MenuMainButton.Click += MainButton_Click;
                MenuMainButton.Padding = new Padding(0,0,0,3);
                MenuMainButton.TextAlign = ContentAlignment.MiddleCenter;
                SetFormPos();
            }
            Text = Funcs.GetName() + " v" + Funcs.GetVersion();

            pTop.BackColor = Config.ClipsHeaderColor;
            pClips.AutoScroll = true;
            pClips.BackColor = Config.ClipsBackColor;
            BackColor = Config.ClipsBackColor;

            if (clipboard == null)
            {
                clipboard = new SharpClipboard();
                clipboard.MonitorClipboard = true;
                clipboard.ObservableFormats.All = true;
                clipboard.ObservableFormats.Files = true;
                clipboard.ObservableFormats.Images = true;
                clipboard.ObservableFormats.Others = true;
                clipboard.ObservableFormats.Texts = true;
                clipboard.ObserveLastEntry = false;
                clipboard.Tag = null;
                clipboard.ClipboardChanged += new EventHandler<SharpClipboard.ClipboardChangedEventArgs>(ClipboardChanged);
            }
            RegisterHotKey(this.Handle, 1, Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), Config.PopupHotkey)).GetHashCode());
        }

        private void LoadItems()
        {
            pClips.Controls.Clear();
            LastImage = null;
            LastText = "";
            inLoad = true;
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
                        AddItem(img, file, false);
                        if (img != null) img = null;
                    }
                    finally
                    {
                        ms.Dispose();
                    }
                }
                else
                {
                    byte[] base64EncodedBytes = Convert.FromBase64String(data.InnerText);
                    string decodedString = Encoding.UTF8.GetString(base64EncodedBytes);
                    AddItem(decodedString, file, false);
                }
                doc = null;
            }
            inLoad = false;
        }

        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            foreach (Process process in processes)
                if (process.Id != current.Id)
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                        return process;
            return null;
        }

        private void SetFormPos()
        {
            Top = Config.FormTop;
            Left = Config.FormLeft;
            Size = Config.FormSize;
        }

        private void ToggleShow(bool Override = false, bool IgnoreBounds = true)
        {
            if ((!Override) && (inClose || inAbout || inPreview || inMenu || inSettings))
                return;
            else
            {
                if (Opacity > 0)
                {
                    Opacity = 0;
                }
                else
                {
                    AutoSizeForm(true);
                    if (Config.OpenFormAtCursor)
                        Funcs.MoveFormToCursor(this, IgnoreBounds);
                    Opacity = 100;
                    Activate();
                }
            }
        }

        #endregion
    } // Main
}