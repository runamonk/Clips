﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Utility;
using System.Diagnostics;
using System.Reflection;
using zuulWindowTracker;

#region Todo
// TODO Add support for setting a description of a pinned ? any clip.
// TODO Add support for actually clipping the files from a list of files.
// TODO Add edit/favorite text editor in config.
// TODO Add max form height (to work with auto-size).
#endregion

namespace Clips
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region Imports
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        #endregion

        #region Properties
        private ClipButton MenuMainButton { get; set; }
        private ClipButton PinButton { get; set; }
        private ClipButton PasswordButton { get; set; }
        private ClipMenu MenuMain { get; set; }
        private Config Config { get; set; }
        private ClipPanel Clips { get; set; }
        private ClipPinnedPanel PinnedClips { get; set; }
        private ClipSearch SearchClips { get; set; }
        #endregion

        #region Privates
        private bool inAbout = false;
        private bool inClose = false;
        private bool inMenu = false;
        private bool inSettings = false;
        private int HotkeyId = 1;
        private int gpHotkeyId = 2;

        private bool hotkeyEnabled = false;
        private string[] ignoreWindowsList;
        private WindowTracker windowTracker;

        #endregion

        #region Events
        private void ConfigChanged()
        {
            LoadConfig();
            AutoSizeForm(true);
        }

        private void ClipAdded(ClipButton Clip)
        {
            AutoSizeForm(true);
        }

        private void ClipClicked(ClipButton Clip)
        {
            if (Config.AutoHide)
                 ToggleShow(true);
        }

        private void ClipDeleted()
        {
            AutoSizeForm(false);
        }

        private void ClipsLoaded()
        {
            AutoSizeForm(true);
        }

        private void ClipPinned(ClipButton Clip, bool doSave)
        {
            PinnedClips.AddClipButton(Clip, doSave);
            AutoSizeForm(true);
        }

        private void ClipUnpinned(ClipButton Clip)
        {
            Clips.AddClipButton(Clip, true);
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
            if ((Control.ModifierKeys == Keys.Control) && (e.KeyCode == Keys.P))
                PinButton.PerformClick();
            else
            if ((SearchClips.Text == "") && (e.KeyCode == Keys.Escape) && (Opacity > 0)) 
                ToggleShow(true);
            else
            if (e.KeyCode == Keys.Escape)
                SearchClips.Text = "";
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

        private void MenuGeneratePassword_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void MenuMonitorClipboard_Click(object sender, EventArgs e)
        {
            bool b;
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
            Config.ShowConfigForm((Opacity > 0));
            inSettings = false;
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            Button b = ((Button)sender);
            MenuMain.Show(b.Left + b.Width + Left, b.Top + b.Height + Top);
        }

        private void OnWindowChanged(IntPtr handle)
        {
            try
            {
                uint pid;
                GetWindowThreadProcessId(handle, out pid);
                string t = Process.GetProcessById((int)pid).MainWindowTitle;

                if (InWindowList(t))
                {
                    DisableHotkey();
                }
                else
                    EnableHotkey();
            }
            catch 
            {
                EnableHotkey();
            }
        }

        private void PasswordButton_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void PinButton_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }

        private void notifyClips_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToggleShow(false);
        }

        private void SearchTextChanged(object sender, EventArgs e)
        {
            Clips.SuspendLayout();
            bool includeImages = (SearchClips.Text.Trim().ToLower() == ":image");
            bool includeLinks = (SearchClips.Text.Trim().ToLower() == ":link");
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
                    if (includeLinks)
                    {
                        b.Visible = (b.Text.ToLower().StartsWith("https://") || b.Text.ToLower().StartsWith("http://") || b.Text.ToLower().StartsWith("www.") || b.Text.ToLower().StartsWith("ftp://"));
                    }
                    else
                    if (includeImages)
                    {
                        b.Visible = (b.PreviewImage != null);
                    }
                    else
                    {
                        b.Visible = ((b.FullText != null) && (b.FullText.ToLower().Contains(SearchClips.Text.ToLower().Trim())));
                    }
                }
            }
            Clips.First();            
            AutoSizeForm(false);
            Funcs.MoveFormToCursor(this);
            Clips.ResumeLayout();
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
                        c += (Clips.Controls[i].Height);

                        ButtonCount++;
                        if (ButtonCount >= Config.ClipsToDisplay)
                        {
                            break;
                        }                           
                    }
                }

                Height = c + 68 + PinnedClips.Height;

                if (Height >= Screen.PrimaryScreen.WorkingArea.Height)
                    Height = Screen.PrimaryScreen.WorkingArea.Height;
            }
            
            // select the first control.
            if (ScrollToTop)
                Clips.First();               
        }

        private void LoadConfig()
        {
            if (Config == null)
            {
                Config = new Config();
                Config.ConfigChanged += new BasePanel.ConfigChangedHandler(ConfigChanged);
                MenuMain = new ClipMenu(Config);
                MenuMain.Opening += new System.ComponentModel.CancelEventHandler(MenuClips_Opening);
                MenuMain.Closed += new ToolStripDropDownClosedEventHandler(MenuClips_Closed);
                Funcs.AddMenuItem(MenuMain, "About", MenuAbout_Click);
                MenuMain.Items.Add(new ToolStripSeparator());
                ToolStripMenuItem t = Funcs.AddMenuItem(MenuMain, "Monitor Clipboard", MenuMonitorClipboard_Click);
                t.Checked = true;
                t.CheckState = CheckState.Checked;
                
                Funcs.AddMenuItem(MenuMain, "Generate Password", MenuGeneratePassword_Click);
                Funcs.AddMenuItem(MenuMain, "Settings", MenuSettings_Click);
                Funcs.AddMenuItem(MenuMain, "Close", MenuClose_Click);

                PasswordButton = new ClipButton(Config, ButtonType.PasswordGen, "", null)
                {
                    Parent = pTop,
                    Dock = DockStyle.Left
                };
                PasswordButton.Click += PasswordButton_Click;

                MenuMainButton = new ClipButton(Config, ButtonType.Menu, "", null)
                {
                    Parent = pTop,
                    Dock = DockStyle.Left
                };
                MenuMainButton.Click += MainButton_Click;

                PinButton = new ClipButton(Config, ButtonType.Pin, "", null)
                {
                    Parent = pTop,
                    Dock = DockStyle.Right
                };
                PinButton.Click += PinButton_Click;

                SearchClips = new ClipSearch
                {
                    Parent = pTop,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 0, 0, 0),
                    Padding = new Padding(0, 0, 0, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                SearchClips.TextChanged += new EventHandler(SearchTextChanged);
                pTop.Controls.SetChildIndex(SearchClips, 0);
                notifyClips.ContextMenuStrip = MenuMain;

                PinnedClips = new ClipPinnedPanel(Config);
                PinnedClips.Parent = pMain;
                PinnedClips.Dock = DockStyle.Top;
                PinnedClips.OnClipClicked += new ClipPanel.ClipClickedHandler(ClipClicked);
                PinnedClips.OnClipDeleted += new ClipPanel.ClipDeletedHandler(ClipDeleted);
                PinnedClips.OnClipUnpinned += new ClipPinnedPanel.ClipUnpinnedHandler(ClipUnpinned);
                PinnedClips.OnSetClipboardMonitoring += new ClipPinnedPanel.SetClipboardMonitoring(SetClipboardMonitoring);
                pMain.Controls.SetChildIndex(PinnedClips, 0);

                Clips = new ClipPanel(Config);
                Clips.OnClipClicked += new ClipPanel.ClipClickedHandler(ClipClicked);
                Clips.OnClipAdded += new ClipPanel.ClipAddedHandler(ClipAdded);
                Clips.OnClipDeleted += new ClipPanel.ClipDeletedHandler(ClipDeleted);
                Clips.OnClipsLoaded += new ClipPanel.ClipsLoadedHandler(ClipsLoaded);
                Clips.OnClipPinned += new ClipPanel.ClipPinnedHandler(ClipPinned);
                Clips.LoadItems();
                Clips.MonitorClipboard = true;

                Clips.Parent = pMain;
                Clips.Dock = DockStyle.Fill;
                pMain.Controls.SetChildIndex(Clips, 0);
                SetFormPos();
            }

            Text = Funcs.GetNameAndVersion();
            pTop.BackColor = Config.HeaderBackColor;
            BackColor = Config.HeaderBackColor;
            ignoreWindowsList = Config.IgnoreWindows.Split(',');
            SearchClips.Text = "";
            SearchClips.BackColor = Config.HeaderBackColor;
            SearchClips.ForeColor = Config.HeaderFontColor;
            
            if ((Config.AutoSizeHeight) && Visible)
                AutoSizeForm(false);

            EnableHotkey();

            if (Config.gpHotkey != "None")
            {
                RegisterHotKey(this.Handle, gpHotkeyId, Config.gpHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), Config.gpHotkey)).GetHashCode());
            }
            else
            {
                UnregisterHotKey(this.Handle, gpHotkeyId);
            }

            MonitorWindowChanges();
        }

        public void EnableHotkey()
        {
            if (Config.PopupHotkey == "None")
            {
                hotkeyEnabled = false;
            }
            else
            if (hotkeyEnabled == false)
            {
                hotkeyEnabled = true;
                RegisterHotKey(this.Handle, HotkeyId, Config.PopupHotkeyModifier, ((Keys)Enum.Parse(typeof(Keys), Config.PopupHotkey)).GetHashCode());
            }
        }

        public void DisableHotkey()
        {
            if (hotkeyEnabled)
            {
                hotkeyEnabled = false;
                UnregisterHotKey(this.Handle, HotkeyId);
            }
        }

        private void GeneratePassword()
        {
            Clipboard.SetText(Funcs.GeneratePassword(Config.gpIncNumbers, Config.gpIncSymbols, Config.gpSize));
        }
        
        private bool InWindowList(string title)
        {
            if (title != "")
                foreach (string s in ignoreWindowsList)
                {
                    if ((s.Trim() != "") && (title.ToLower().Contains(s.ToLower())))
                        return true;
                }

            return false;
        }

        private void MonitorWindowChanges()
        {
            if (windowTracker == null)
            {
                windowTracker = new WindowTracker();
                windowTracker.WindowChanged += OnWindowChanged;
            }
        }

        private Process RunningInstance()
        {
            if (!Debugger.IsAttached)
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);

                foreach (Process process in processes)
                    if (process.Id != current.Id)
                        if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                            return process;
            }
            return null;
        }

        private void SetClipboardMonitoring(bool SetMonitorClipboard)
        {
            Clips.MonitorClipboard = SetMonitorClipboard;
        }

        private void SetFormPos()
        {
            Top = Config.FormTop;
            Left = Config.FormLeft;
            Size = Config.FormSize;
        }

        private void ToggleShow(bool Override = false)
        {
            if ((TopMost) || (!Override) && (inClose || inAbout || Clips.InMenu || inMenu || inSettings))
                return;
            else
            {
                if (Opacity > 0)
                {
                    // Set visible=false to hide the form, then change the opacity that way we can show the form later and then
                    // resize it without it jumping around the screen (if the mouse has moved to a new position).
                    // Setting visible=false will automatically hide all submenus if they are displayed.
                    Visible = false;
                    Opacity = 0;
                    // Turn off KeyPreview while the form is hidden so we don't accidentally pick
                    // up keystrokes before the form actually loses focus.
                    KeyPreview = false;
                }
                else
                {
                    Visible = true;
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

        #region Overrides
        protected override void OnHandleDestroyed(EventArgs e)
        {
            windowTracker = null;
            DisableHotkey();
            base.OnHandleDestroyed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (RunningInstance() != null)
            {
                MessageBox.Show("There is already a version of zuulClips running.");
                Application.Exit();
            }
            else
                base.OnLoad(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (m.WParam.ToInt32() == gpHotkeyId)
                {
                    GeneratePassword(); 
                }

                if (m.WParam.ToInt32() == HotkeyId)
                    ToggleShow();
               
            }
            base.WndProc(ref m);
        }
        #endregion
    } // Main
}