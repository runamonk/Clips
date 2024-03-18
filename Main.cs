using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Clips.Controls;
using Clips.Forms;
using Utility;
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
        private static extern IntPtr GetForegroundWindow();

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

        private bool _inAbout;
        private bool _inClose;
        private bool _inMenu;
        private bool _inSettings;
        private readonly int _hotkeyId = 1;
        private readonly int _gpHotkeyId = 2;

        private bool _hotkeyEnabled;
        private string[] _ignoreWindowsList;
        private WindowTracker _windowTracker;

        #endregion

        #region Events

        private void ConfigChanged()
        {
            LoadConfig();
            AutoSizeForm(true);
        }

        private void ClipAdded(ClipButton clip)
        {
            AutoSizeForm(true);
        }

        private void ClipClicked(ClipButton clip)
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

        private void ClipPinned(ClipButton clip, bool doSave)
        {
            PinnedClips.AddClipButton(clip, doSave);
            AutoSizeForm(true);
        }

        private void ClipUnpinned(ClipButton clip)
        {
            Clips.AddClipButton(clip, true);
            AutoSizeForm(true);
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            if (Opacity > 0)
                ToggleShow();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _inClose = true;
            _windowTracker = null;
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.P)
                PinButton.PerformClick();
            else if (SearchClips.Text == "" && e.KeyCode == Keys.Escape && Opacity > 0)
                ToggleShow(true);
            else if (e.KeyCode == Keys.Escape)
                SearchClips.Text = "";
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                if (SearchClips.Text.Length > 0)
                    SearchClips.Text = SearchClips.Text.Substring(0, SearchClips.Text.Length - 1);
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Any(x =>
                    char.IsLetterOrDigit(x) || char.IsPunctuation(x) || char.IsSeparator(x) || char.IsSymbol(x)))
                SearchClips.Text += e.KeyChar.ToString();
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
            _inAbout = true;
            var aboutForm = new About(Config);
            aboutForm.Show(this);
            _inAbout = false;
        }

        private void MenuClips_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            _inMenu = false;
        }

        private void MenuClips_Opening(object sender, CancelEventArgs e)
        {
            _inMenu = true;
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
            var b = !((ToolStripMenuItem)sender).Checked;
            ((ToolStripMenuItem)sender).Checked = b;
            Clips.MonitorClipboard = b;
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            _inSettings = true;
            Config.ShowConfigForm(Opacity > 0);
            _inSettings = false;
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            MenuMain.Show(b.Left + b.Width + Left, b.Top + b.Height + Top);
        }

        private void OnWindowChanged(IntPtr handle)
        {
            if (_inClose) return;
            try
            {
                GetWindowThreadProcessId(handle, out var pid);
                var t = Process.GetProcessById((int)pid).MainWindowTitle;

                if (InWindowList(t))
                    DisableHotkey();
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
            ToggleShow();
        }

        private void SearchTextChanged(object sender, EventArgs e)
        {
            Clips.SuspendLayout();
            var includeImages = SearchClips.Text.Trim().ToLower() == ":image";
            var includeLinks = SearchClips.Text.Trim().ToLower() == ":link";
            if (SearchClips.Text.Trim() == "")
            {
                foreach (ClipButton b in Clips.Controls)
                    if (!b.Visible)
                        b.Visible = true;
            }
            else
            {
                foreach (ClipButton b in Clips.Controls)
                    if (includeLinks)
                        b.Visible = b.Text.ToLower().StartsWith("https://") || b.Text.ToLower().StartsWith("http://") ||
                                    b.Text.ToLower().StartsWith("www.") || b.Text.ToLower().StartsWith("ftp://");
                    else if (includeImages)
                        b.Visible = b.PreviewImage != null;
                    else
                        b.Visible = b.FullText != null &&
                                    b.FullText.ToLower().Contains(SearchClips.Text.ToLower().Trim());
            }

            Clips.First();
            AutoSizeForm(false);
            Funcs.MoveFormToCursor(this);
            Clips.ResumeLayout();
        }

        #endregion

        #region Methods

        private void AutoSizeForm(bool scrollToTop)
        {
            if (Clips.InLoad) return;

            if (Config.AutoSizeHeight)
            {
                var c = 0;
                var buttonCount = 0;

                for (var i = Clips.Controls.Count - 1; i > -1; i--)
                    if (Clips.Controls[i].Visible)
                    {
                        c += Clips.Controls[i].Height;

                        buttonCount++;
                        if (buttonCount >= Config.ClipsToDisplay) break;
                    }

                Height = c + 68 + PinnedClips.Height;

                if (Height >= Screen.PrimaryScreen.WorkingArea.Height)
                    Height = Screen.PrimaryScreen.WorkingArea.Height;
            }

            // select the first control.
            if (scrollToTop)
                Clips.First();
        }

        private void LoadConfig()
        {
            if (Config == null)
            {
                Config = new Config();
                Config.ConfigChanged += ConfigChanged;
                MenuMain = new ClipMenu(Config);
                MenuMain.Opening += MenuClips_Opening;
                MenuMain.Closed += MenuClips_Closed;
                Funcs.AddMenuItem(MenuMain, "About", MenuAbout_Click);
                MenuMain.Items.Add(new ToolStripSeparator());
                var t = Funcs.AddMenuItem(MenuMain, "Monitor Clipboard", MenuMonitorClipboard_Click);
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
                SearchClips.TextChanged += SearchTextChanged;
                pTop.Controls.SetChildIndex(SearchClips, 0);
                notifyClips.ContextMenuStrip = MenuMain;

                PinnedClips = new ClipPinnedPanel(Config);
                PinnedClips.Parent = pMain;
                PinnedClips.Dock = DockStyle.Top;
                PinnedClips.OnClipClicked += ClipClicked;
                PinnedClips.OnClipDeleted += ClipDeleted;
                PinnedClips.OnClipUnpinned += ClipUnpinned;
                PinnedClips.OnSetClipboardMonitoring += SetClipboardMonitoring;
                pMain.Controls.SetChildIndex(PinnedClips, 0);

                Clips = new ClipPanel(Config);
                Clips.OnClipClicked += ClipClicked;
                Clips.OnClipAdded += ClipAdded;
                Clips.OnClipDeleted += ClipDeleted;
                Clips.OnClipsLoaded += ClipsLoaded;
                Clips.OnClipPinned += ClipPinned;
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
            _ignoreWindowsList = Config.IgnoreWindows.Split(',');
            SearchClips.Text = "";
            SearchClips.BackColor = Config.HeaderBackColor;
            SearchClips.ForeColor = Config.HeaderFontColor;

            if (Config.AutoSizeHeight && Visible)
                AutoSizeForm(false);

            EnableHotkey();

            if (Config.GpHotkey != "None")
                RegisterHotKey(Handle, _gpHotkeyId, Config.GpHotkeyModifier,
                    ((Keys)Enum.Parse(typeof(Keys), Config.GpHotkey)).GetHashCode());
            else
                UnregisterHotKey(Handle, _gpHotkeyId);

            MonitorWindowChanges();
        }

        public void EnableHotkey()
        {
            if (Config.PopupHotkey == "None")
            {
                _hotkeyEnabled = false;
            }
            else if (_hotkeyEnabled == false)
            {
                _hotkeyEnabled = true;
                RegisterHotKey(Handle, _hotkeyId, Config.PopupHotkeyModifier,
                    ((Keys)Enum.Parse(typeof(Keys), Config.PopupHotkey)).GetHashCode());
            }
        }

        public void DisableHotkey()
        {
            if (_hotkeyEnabled)
            {
                _hotkeyEnabled = false;
                UnregisterHotKey(Handle, _hotkeyId);
            }
        }

        private void GeneratePassword()
        {
            Clipboard.SetText(Funcs.GeneratePassword(Config.GpIncNumbers, Config.GpIncSymbols, Config.GpSize));
        }

        private bool InWindowList(string title)
        {
            if (title != "")
                foreach (var s in _ignoreWindowsList)
                    if (s.Trim() != "" && title.ToLower().Contains(s.ToLower()))
                        return true;

            return false;
        }

        private void MonitorWindowChanges()
        {
            if (_windowTracker == null)
            {
                _windowTracker = new WindowTracker();
                _windowTracker.WindowChanged += OnWindowChanged;
            }
        }

        private void SetClipboardMonitoring(bool setMonitorClipboard)
        {
            Clips.MonitorClipboard = setMonitorClipboard;
        }

        private void SetFormPos()
        {
            Top = Config.FormTop;
            Left = Config.FormLeft;
            Size = Config.FormSize;
        }

        private void ToggleShow(bool @override = false)
        {
            if (TopMost || (!@override && (_inClose || _inAbout || Clips.InMenu || _inMenu || _inSettings))) return;

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
                    Funcs.MoveFormToCursor(this);
                Opacity = 100;
                Activate();
                KeyPreview = true;
            }
        }

        #endregion

        #region Overrides

        protected override void OnHandleDestroyed(EventArgs e)
        {
            _windowTracker = null;
            DisableHotkey();
            base.OnHandleDestroyed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Funcs.IsRunningDoShow()) Application.Exit();
            base.OnLoad(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (m.WParam.ToInt32() == _gpHotkeyId) GeneratePassword();

                if (m.WParam.ToInt32() == _hotkeyId)
                    ToggleShow();
            }

            base.WndProc(ref m);
        }

        #endregion
    } // Main
}