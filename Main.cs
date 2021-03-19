using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Utility;
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

        private ClipButton MenuMainButton { get; set; }
        private ClipMenu MenuMain { get; set; }
        private Config Config { get; set; }
        private ClipPanel Clips { get; set; }
        private ClipSearch SearchClips { get; set; }

        private bool inAbout = false;
        private bool inClose = false;
        private bool inMenu = false;
        private bool inSettings = false;

        #region Todo
        // TODO Add ability to pin a clip.
        // TODO Add support for actually clipping the files from a list of files.
        // TODO Add edit/favorite text editor in config.
        // TODO Add button pad amount that way users can decide how much to pad the ClipButton, rather than just using an arbritrary 8px.
        // TODO Add max form height (to work with auto-size).
        #endregion

        #region Events
        private void ConfigChanged(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void ClipAdded(ClipButton Clip, bool ClipSavedToDisk)
        {
            if (ClipSavedToDisk)
                AutoSizeForm(true);
        }

        private void ClipClicked(ClipButton Clip)
        {
            if (Config.AutoHide)
                 ToggleShow(true, true);
        }

        private void ClipDeleted()
        {
            AutoSizeForm(false);
        }

        private void ClipsLoaded()
        {
            AutoSizeForm(true);
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

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if ((SearchClips.Text == "") && (e.KeyCode == Keys.Escape) && (Opacity > 0)) 
                {
                    ToggleShow(true);
            }
            else
            if (e.KeyCode == Keys.Escape)
            {
                SearchClips.Text = "";
            }
            else
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if (SearchClips.Text.Length > 0)
                    SearchClips.Text = SearchClips.Text.Substring(0, (SearchClips.Text.Length - 1));
            }
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Any(x => char.IsLetterOrDigit(x) || char.IsPunctuation(x) || char.IsSeparator(x) || char.IsSymbol(x)))
            {
                SearchClips.Text += e.KeyChar.ToString();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            HideScrollbar();
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            Config.FormSize = Size;
            Config.FormTop = Top;
            Config.FormLeft = Left;
        }

        private void Main_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                BringToFront();
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            inAbout = true;
            About AboutForm = new About(Config);
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

        private void MenuMonitorClipboard_Click(object sender, EventArgs e)
        {
            bool b = false;
            if (((ToolStripMenuItem)sender).Checked)
                b = false;
            else
                b = true;
            ((ToolStripMenuItem)sender).Checked = b;
            Clips.MonitorClipboard = b;
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

        private void SearchTextChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            bool includeImages = (SearchClips.Text.Trim() == ":image");

            if (SearchClips.Text.Trim() == "")
            {
                foreach (ClipButton b in Clips.Controls)
                {
                    if (!b.Visible)
                        b.Visible = true;
                }               
            }
            else
            {
                foreach (ClipButton b in Clips.Controls)
                {
                    if ((includeImages) && (b.FullImage != null))
                    {
                        b.Visible = true;
                    }
                    else
                    {
                        if ((b.FullText == null) && (!includeImages))
                            b.Visible = false;
                        else
                        if (b.FullText.ToLower().Contains(SearchClips.Text.ToLower().Trim()))
                            b.Visible = true;
                        else
                            b.Visible = false;
                    }
                }
            }
            Clips.First();
            ResumeLayout();
            AutoSizeForm(false);
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

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) //WM_HOTKEY
            {
                ToggleShow(true);
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Methods
        
        private void AutoSizeForm(bool ScrollToTop)
        {
            if (Clips.InLoad) return;

            if (Config.AutoSizeHeight)
            {
                int c = 0;
                int ButtonCount = 0;

                for (int i = Clips.Controls.Count-1; i > -1; i--)
                {
                    if (Clips.Controls[i].Visible)
                    {
                        c = c + (Clips.Controls[i].Height);

                        ButtonCount++;
                        if (ButtonCount >= Config.ClipsToDisplay)
                        {
                            break;
                        }                           
                    }
                }

                //Height = c + SystemInformation.CaptionHeight + SystemInformation.BorderSize.Height + pTop.Height + pMain.Padding.Top + pMain.Padding.Bottom;
                Height = c + 68;
            }
            
            // select the first control.
            if (ScrollToTop)
                Clips.First();               
        }

        private void HideScrollbar()
        {
            if (Clips == null)
                return;

            Clips.Top = 0;
            Clips.Left = 0;
            Clips.Height = pMain.Height;
            Clips.Width = pMain.Width + SystemInformation.VerticalScrollBarWidth; 
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
                t = new ToolStripMenuItem("&About");
                t.Click += new EventHandler(MenuAbout_Click);
                MenuMain.Items.Add(t);
                MenuMain.Items.Add(new ToolStripSeparator());
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
                MenuMainButton = new ClipButton(Config, true)
                {
                    Text = "...",
                    Width = 25,
                    Parent = pTop,
                    Dock = DockStyle.Left
                };
                MenuMainButton.Click += MainButton_Click;
                MenuMainButton.Padding = new Padding(0,0,0,3);
                MenuMainButton.TextAlign = ContentAlignment.MiddleCenter;
                
                SearchClips = new ClipSearch();
                SearchClips.Parent = pTop;
                SearchClips.Dock = DockStyle.Fill;
                SearchClips.Margin = new Padding(0, 0, 0, 0);
                SearchClips.Padding = new Padding(0, 0, 0, 0);
                SearchClips.TextAlign = ContentAlignment.MiddleCenter;
                SearchClips.TextChanged += new EventHandler(SearchTextChanged);

                pTop.Controls.SetChildIndex(SearchClips, 0);
                notifyClips.ContextMenuStrip = MenuMain;
                               
                Clips = new ClipPanel(Config);
                Clips.OnClipClicked += new ClipPanel.ClipClickedHandler(ClipClicked);
                Clips.OnClipAdded += new ClipPanel.ClipAddedHandler(ClipAdded);
                Clips.OnClipDeleted += new ClipPanel.ClipDeletedHandler(ClipDeleted);
                Clips.OnClipsLoaded += new ClipPanel.ClipsLoadedHandler(ClipsLoaded);
                Clips.Parent = pMain;
                //Clips.Dock = DockStyle.Fill;
                SetFormPos();
            }

            Text = Funcs.GetName() + " v" + Funcs.GetVersion();
            pTop.BackColor = Config.ClipsHeaderColor;
            BackColor = Config.ClipsBackColor;
            SearchClips.Text = "";
            SearchClips.BackColor = Config.MenuButtonColor;
            SearchClips.ForeColor = Config.MenuFontColor;
            
            RegisterHotKey(this.Handle, 1, Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), Config.PopupHotkey)).GetHashCode());
        }

        private Process RunningInstance()
        {
#if !DEBUG            
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            foreach (Process process in processes)
                if (process.Id != current.Id)
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                        return process;
            
#endif
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
            if ((!Override) && (inClose || inAbout || Clips.InPreview || Clips.InMenu || inMenu || inSettings))
                return;
            else
            {
                if (Opacity > 0)
                {
                    Opacity = 0;
                    // Turn off KeyPreview while the form is hidden so we don't accidentally pick
                    // up keystrokes before the form actually loses focus.
                    KeyPreview = false;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    AutoSizeForm(true);
                    if (Config.OpenFormAtCursor)
                        Funcs.MoveFormToCursor(this, false);                   
                    Opacity = 100;
                    Activate();
                    KeyPreview = true;
                }
            }
        }
        #endregion
    } // Main
}