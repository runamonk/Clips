﻿using System;
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
        private ClipButton MenuButton { get; set; }
        private Config Config { get; set; }
        private bool inAbout = false;
        private bool inClose = false;
        private bool inMenu = false;
        private bool inLoad = false;
        private bool inPreview = false;
        private bool inSettings = false;
        private bool isVisible = false;
        private Image LastImage { get; set; }
        private string LastText { get; set; }
        private Preview PreviewForm = new Preview();
        private SharpClipboard clipboard;


        // TODO Add ability to pin a clip.
        // TODO Add support for actually clipping the files from a list of files.
        // TODO Add edit/favorite text editor in config.

        #region Events
        private void ConfigChanged(object sender, EventArgs e)
        {
            LoadConfig();
            CleanupCache();
            LoadItems();
            AutoSizeForm();
        }

        private void ClipBoard_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
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
            }
        }

        private void ClipsButtonClick(Object sender, MouseEventArgs e)
        {
            
            void deleteClip()
            {
                string fileName = ((ClipButton)sender).FileName;
                if (File.Exists(fileName))
                    File.Delete(fileName);
                pClips.Controls.Remove((ClipButton)sender);
            }


            if ((e.Button == MouseButtons.Right) || ((e.Button == MouseButtons.Middle) && (((ClipButton)sender).FullImage != null)))
                return;

            SuspendLayout();

            LastImage = null;
            LastText = null;

            if (Config.AutoHide)
                ToggleShow(true, true);

            bool skipShiftToTop = false;

            // Don't delete and readd the most recent clip, it ends up just looking like a flicker.
            if (((ClipButton)sender) == pClips.Controls[pClips.Controls.Count-1])
                skipShiftToTop = true;

            if (((ClipButton)sender).FullImage != null)
            {
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
                if (skipShiftToTop)
                    LastText = s;
                else
                    deleteClip();
                Clipboard.SetText(s);
                
                // TODO max url length is 2048, check for that?
                //if (((Form.ModifierKeys == Keys.Shift) || (e.Button == MouseButtons.Middle)) && (Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute) == true))
                //{
                //    System.Diagnostics.Process.Start(s);
                //}
            }

            ResumeLayout();           
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
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

        private void MainButton_Click(object sender, EventArgs e)
        {
            Button b = ((Button)sender);
            menuMain.Show(b.Left + b.Width + this.Left, b.Top + b.Height + this.Top);
        }

        private void MenuInsertTestClips_Click(object sender, EventArgs e)
        {
            int toInsert = (Config.ClipsMaxClips - pClips.Controls.Count);
            if (toInsert > 0)
                while (toInsert > 0)
                {
                    AddItem(toInsert.ToString(), null, true);
                    toInsert--;
                }
        }

        private void MenuClips_Click(object sender, EventArgs e)
        {
            inPreview = true;

            if (sender == menuSave)
            {
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
            }
            else
            if (sender == menuDelete)
            {
                ClipButton b = ((ClipButton)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
                if (File.Exists(b.FileName))
                    File.Delete(b.FileName);
                pClips.Controls.Remove(b);
                AutoSizeForm();

            }
            inPreview = false;
        }

        private void MenuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            inSettings = true;
            Config.ShowConfigForm(isVisible);
            inSettings = false;
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

        private void NotifyClips_DoubleClick(object sender, EventArgs e)
        {
            ToggleShow(false,false);
        }

        private void menuClips_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            inMenu = false;
        }

        private void menuClips_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            inMenu = true;
        }

        protected override void OnShown(EventArgs e)
        {
            Visible = false;
            Deactivate += Main_Deactivate;
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
        
        // methods
        private string new_xml_file = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DATA PINNED=\"{0}\" TYPE=\"{1}\">{2}\r\n</DATA>";
        
        private void AddItem(string text, string fileName, bool saveToDisk = false)
        {
            if (text == LastText) return;

            LastText = text;
            ClipButton b = NewClipButton();
            b.FullText = text;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(plainTextBytes);
            if (saveToDisk)
                b.FileName = Funcs.SaveToCache(string.Format(new_xml_file, "N", "TEXT", base64));
            else
                b.FileName = fileName;

            string[] s = text.TrimStart().Replace("\r","").Split(new string[] { "\n" }, StringSplitOptions.None);

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

            b.Height = (s.Count() > 0 && s.Count() >= Config.ClipsLinesPerRow ? Config.ClipsLinesPerRow * 20 : 22);
            AutoSizeForm();
        }

        private void AddItem(Image image, string fileName, bool saveToDisk = false)
        {           
            if ((LastImage != null) && (image.Size == LastImage.Size)) return;
            LastImage = image;

            ClipButton b = NewClipButton();
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
            AutoSizeForm();
        }

        private void AutoSizeForm()
        {
            if (inLoad) return;

            if (Config.AutoSizeHeight)
            {
                int c = 68;
                for (int i=0; i <= pClips.Controls.Count-1; i++)
                {
                    c = c + pClips.Controls[i].Height;
                }
                if (c < MaximumSize.Height)
                    Height = c;
                else
                    Height = MaximumSize.Height;
            }
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
                MenuButton = new ClipButton
                {
                    Text = "...",
                    Width = 25,
                    Parent = pTop,
                    Dock = DockStyle.Left
                };
                MenuButton.Click += MainButton_Click;
                SetFormPos();
            }
            Text = Funcs.GetName() + " v" + Funcs.GetVersion();
            menuMain.Renderer = null;
            menuMain.Renderer = new CustomToolstripRenderer(Config);
            menuMain.BackColor = Config.MenuBackColor;
            menuMain.ForeColor = Config.MenuFontColor;
            menuClips.Renderer = null;
            menuClips.Renderer = new CustomToolstripRenderer(Config);
            menuClips.BackColor = Config.MenuBackColor;
            menuClips.ForeColor = Config.MenuFontColor;
            MenuButton.ForeColor = Config.MenuFontColor;
            MenuButton.BackColor = Config.MenuButtonColor;
            pTop.BackColor = Config.ClipsHeaderColor;
            
            pClips.AutoScroll = true;
            pClips.VerticalScroll.Visible = true;
            pClips.BackColor = Config.ClipsBackColor;
            this.BackColor = Config.ClipsBackColor;

            clipboard = new SharpClipboard();
            clipboard.MonitorClipboard = true;
            clipboard.ObservableFormats.All = true;
            clipboard.ObservableFormats.Files = true;
            clipboard.ObservableFormats.Images = true;
            clipboard.ObservableFormats.Others = true;
            clipboard.ObservableFormats.Texts = true;
            clipboard.ObserveLastEntry = false;
            clipboard.Tag = null;
            clipboard.ClipboardChanged += new EventHandler<SharpClipboard.ClipboardChangedEventArgs>(ClipBoard_ClipboardChanged);
            RegisterHotKey(this.Handle, 1, Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), Config.PopupHotkey)).GetHashCode());
        }

        private void LoadItems()
        {
            SuspendLayout();
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
            
            ResumeLayout();
            pClips.VerticalScroll.Value = 0;
        }

        private ClipButton NewClipButton()
        {
            if (pClips.Controls.Count >= Config.ClipsMaxClips)
                DeleteOldestClip();

            ClipButton b = new ClipButton
            {
                TabStop = false,
                Parent = pClips,
                Dock = DockStyle.Top
            };
            b.FlatAppearance.BorderColor = BackColor;
            b.MouseUp += new MouseEventHandler(ClipsButtonClick);
            b.MouseHover += new EventHandler(PreviewShow);
            b.MouseLeave += new EventHandler(PreviewHide);
            b.BackColor = ControlPaint.Dark(Config.ClipsRowBackColor, 75);
            b.BackColor = Config.ClipsRowBackColor;
            b.ForeColor = Config.ClipsFontColor;
            b.ContextMenuStrip = menuClips;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            return b;
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
                if (isVisible)
                {
                    Visible = false;
                    isVisible = false;
                    Opacity = 1;
                }
                else
                {
                    AutoSizeForm();
                    if (Config.OpenFormAtCursor)
                        Funcs.MoveFormToCursor(this, IgnoreBounds);
                    Opacity = 100;
                    isVisible = true;
                    Visible = true;
                    Activate();
                }
            }
        }


    } // Main

    public class CustomToolstripRenderer : ToolStripProfessionalRenderer
    {
       public CustomToolstripRenderer(Config MyConfig) : base(new CustomColors(MyConfig)) { }
    }

    public class CustomColors : ProfessionalColorTable
    {
        Config config;
        public CustomColors(Config MyConfig)
        {
            config = MyConfig;
        }

        public override Color ButtonSelectedBorder
        {
            get { return Color.Transparent; } 
        }
        public override Color ImageMarginGradientBegin
        {
            get { return config.MenuBackColor; }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return config.MenuBackColor; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return config.MenuBackColor; }
        }
        public override Color MenuItemSelected
        {
            get { return config.MenuSelectedColor; }
        }
        public override Color MenuItemBorder
        {
            get { return config.MenuSelectedColor; }
        }
        public override Color MenuBorder
        {
            get { return config.MenuBorderColor; }
        }
        public override Color CheckSelectedBackground
        {
            get { return config.MenuSelectedColor; }
        }
        public override Color CheckBackground
        {
            get { return config.MenuBackColor; }
        }
        public override Color CheckPressedBackground
        {
            get { return config.MenuBackColor; }
        }
    }
}