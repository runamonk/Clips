using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using Utility;

namespace Clips
{
    public class Config
    {
        public const string CONFIG_FILENAME = "Clips.cfg";

        private partial class _formSettings : FormSettings
        {
            public _formSettings()
            {

            }

            public _formSettings(Config Config)
            {
                if (Config == null)
                    throw new Exception("Config cannot be null.");
                else
                {
                    _Config = Config;
                    btnOK.Click += new EventHandler(ButtonClick);
                    textHotkey.Text = _Config.PopupHotkey;

                    // fill out the hotkey modifiers
                    /* Modifier
                       None = 0,
                       Alt = 1,
                       Control = 2,
                       Shift = 4,
                       WinKey = 8*/

                    int m = _Config.PopupHotkeyModifier;
                    chkAlt.Checked = (m == 1 || m == 3 || m == 5 || m == 9);
                    chkControl.Checked = (m == 2 || m == 3 || m == 6 || m == 10);
                    chkShift.Checked = (m == 4 || m == 5 || m == 6 || m == 12);
                    chkWin.Checked = (m == 8 || m == 9 || m == 10 || m == 12);
                    chkStartup.Checked = _Config.StartWithWindows;
                    nudClipsLinesPerRow.Value = _Config.ClipsLinesPerRow;
                    nudMaxClips.Value = _Config.ClipsMaxClips;
                    nudPreviewMaxLines.Value = _Config.PreviewMaxLines;
                    nudPreviewPopupDelay.Value = _Config.PreviewPopupDelay;
                    pnlPreviewBackColor.BackColor = _Config.PreviewBackColor;
                    pnlPreviewFontColor.BackColor = _Config.PreviewFontColor;
                    pnlClipsBackColor.BackColor = _Config.ClipsBackColor;
                    pnlClipsFontColor.BackColor = _Config.ClipsFontColor;
                    pnlClipsRowColor.BackColor = _Config.ClipsRowBackColor;
                    pnlHeaderColor.BackColor = _Config.ClipsHeaderColor;
                    pnlMenuBackColor.BackColor = _Config.MenuBackColor;
                    pnlMenuBorderColor.BackColor = _Config.MenuBorderColor;
                    pnlMenuFontColor.BackColor = _Config.MenuFontColor;
                    pnlMenuSelectedColor.BackColor = _Config.MenuSelectedColor;
                    chkOpenAtCursor.Checked = _Config.OpenFormAtCursor;
                    chkAutoSizeHeight.Checked = _Config.AutoSizeHeight;
                }
            }

            private Config _Config;
            public Config Config
            {
                get { return _Config; }
                set { _Config = value; }
            }

            private void ButtonClick(object sender, EventArgs e)
            {
                _Config.PopupHotkey = textHotkey.Text;
                /* Modifier
                   None = 0,
                   Alt = 1,
                   Control = 2,
                   Shift = 4,
                   WinKey = 8*/
                int i = 0;
                if (chkAlt.Checked) i = (i + 1);
                if (chkControl.Checked) i = (i + 2);
                if (chkShift.Checked) i = (i + 4);
                if (chkWin.Checked) i = (i + 8);

                _Config.PopupHotkeyModifier = i;
                _Config.ClipsMaxClips = Convert.ToInt32(nudMaxClips.Value);
                _Config.ClipsLinesPerRow = Convert.ToInt32(nudClipsLinesPerRow.Value);
                _Config.ClipsRowBackColor = pnlClipsRowColor.BackColor;
                _Config.ClipsFontColor = pnlClipsFontColor.BackColor;
                _Config.ClipsBackColor = pnlClipsBackColor.BackColor;
                _Config.ClipsHeaderColor = pnlHeaderColor.BackColor;
                _Config.PreviewMaxLines = Convert.ToInt32(nudPreviewMaxLines.Value);
                _Config.PreviewPopupDelay = Convert.ToInt32(nudPreviewPopupDelay.Value);
                _Config.StartWithWindows = chkStartup.Checked;
                _Config.PreviewBackColor = pnlPreviewBackColor.BackColor;
                _Config.PreviewFontColor = pnlPreviewFontColor.BackColor;
                _Config.MenuBackColor = pnlMenuBackColor.BackColor;
                _Config.MenuBorderColor = pnlMenuBorderColor.BackColor;
                _Config.MenuFontColor = pnlMenuFontColor.BackColor;
                _Config.MenuSelectedColor = pnlMenuSelectedColor.BackColor;
                _Config.OpenFormAtCursor = chkOpenAtCursor.Checked;
                _Config.AutoSizeHeight = chkAutoSizeHeight.Checked;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        public event EventHandler ConfigChanged;
        List<string> _Config;

        public Config()
        {
            _Config = new List<string>();
            LoadConfiguration();
        }
        ~Config()
        {
            _Config.Clear();
            _Config = null;
        }

        private int GetKeyIndex(string Key)
        {
            for (int i = 0; i < _Config.Count; i++)
            {
                if (_Config[i].IndexOf(Key) > -1 && _Config[i].Substring(0, Key.Length) == Key)
                {
                    return i;
                }
            }
            return -1;
        }

        private string FindKey(string Key)
        {
            foreach (String s in _Config)
            {

                if ((s.Length > 0) && (s.Substring(0, s.IndexOf('=')) == Key))
                {
                    return s.Substring((s.IndexOf('=') + 1), (s.Length - (s.IndexOf('=') + 1)));
                }
            }

            return "";
        }

        private string SetKey(string Key, string Value, bool SaveNow = false)
        {
            int i = GetKeyIndex(Key);
            if (i == -1)
                _Config.Add(Key + "=" + Value);
            else
                _Config[i] = Key + "=" + Value;

            if (SaveNow) SaveConfiguration();
            return Value;
        }

        private void LoadConfiguration()
        {
            try
            {
                if (!(File.Exists(Funcs.AppPath(CONFIG_FILENAME))))
                {
                    FileStream fs = File.Create(Funcs.AppPath(CONFIG_FILENAME));
                    fs.Close();
                }

                _Config = File.ReadAllLines(Funcs.AppPath(CONFIG_FILENAME), Encoding.ASCII).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception("Cannot LoadConfiguration()" + System.Environment.NewLine + ee.Message);
            }
        }

        private void SaveConfiguration()
        {
            if (File.Exists(Funcs.AppPath(CONFIG_FILENAME)))
                File.Delete(Funcs.AppPath(CONFIG_FILENAME));
            File.WriteAllLines(Funcs.AppPath(CONFIG_FILENAME), _Config.ToArray());
        }

        public void ShowConfigForm()
        {
            _formSettings f = new _formSettings(this);

            if (f.ShowDialog() == DialogResult.OK)
            {
                SaveConfiguration();
                ConfigChanged(this, null);
            }
            f.Close();
        }

        // properties
        public Boolean AutoSizeHeight
        {
            get {
                string s = FindKey("auto_size_height");
                if (s == "")
                    s = SetKey("auto_size_height", "false");
                return bool.Parse(s);
            }
            set { SetKey("auto_size_height", value.ToString()); }
        }

        public Color ClipsBackColor
        {
            get {
                string s = FindKey("clips_back_color");
                if (s == "")
                    s = SetKey("clips_back_color", Color.FromName("Control").ToArgb().ToString());
                    return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_back_color", value.ToArgb().ToString()); }
        }

        public Color ClipsFontColor
        {
            get {
                string s = FindKey("clips_font_color");
                if (s == "")
                    s = SetKey("clips_font_color", Color.FromName("ControlText").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_font_color", value.ToArgb().ToString()); }
        }

        public Color ClipsHeaderColor
        {
            get {
                string s = FindKey("clips_header_color");
                if (s == "")
                    s = SetKey("clips_header_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_header_color", value.ToArgb().ToString()); }
        }

        public int ClipsLinesPerRow
        {
            get {
                string s = FindKey("clips_lines_per_row");
                if (s == "")
                    s = SetKey("clips_lines_per_row", "1");
                return Convert.ToInt32(s);
            }
            set { SetKey("clips_lines_per_row", value.ToString()); }
        }

        public Color ClipsRowBackColor
        {
            get {
                string s = FindKey("clips_row_back_color");
                if (s == "")
                    s = SetKey("clips_row_back_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_row_back_color", value.ToArgb().ToString()); }
        }

        public int ClipsMaxClips
        {
            get {
                string s = FindKey("clips_max_clips");
                if (s == "")
                    s = SetKey("clips_max_clips", "50");
                return Convert.ToInt32(s);
            }
            set { SetKey("clips_max_clips", value.ToString()); }
        }

        public int FormLeft
        {
            get {
                string s = FindKey("form_left");
                if (s == "")
                    s = SetKey("form_left", "0", true);
                return Convert.ToInt32(s);
            }
            set {SetKey("form_left", value.ToString(), true);}
        }

        public int FormTop
        {
            get {
                string s = FindKey("form_top");
                if (s == "")
                    s = SetKey("form_top", "0", true);
                return Convert.ToInt32(s);
            }
            set {SetKey("form_top", value.ToString(), true);}
        }

        public Size FormSize
        {
            get {
                string s = FindKey("form_size");
                Size sz = new Size(400, 300);
                SizeConverter sc = new SizeConverter();

                if (s == "")
                {
                    sz = new Size(400, 300);
                    sc = new SizeConverter();
                    s = SetKey("form_size", sc.ConvertToString(sz), true);
                }
                sc = new SizeConverter();
                sz = (Size)sc.ConvertFromString(s);
                return sz;
            }
            set {
                SizeConverter sc = new SizeConverter();
                SetKey("form_size", sc.ConvertToString(value), true);
            }
        }

        public Color MenuBackColor
        {
            get {
                string s = FindKey("menu_back_color");
                if (s == "")
                    s = SetKey("menu_back_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_back_color", value.ToArgb().ToString()); }
        }

        public Color MenuBorderColor
        {
            get {
                string s = FindKey("menu_border_color");
                if (s == "")
                    s = SetKey("menu_border_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_border_color", value.ToArgb().ToString()); }
        }

        public Color MenuFontColor
        {
            get {
                string s = FindKey("menu_font_color");
                if (s == "")
                    s = SetKey("menu_font_color", Color.FromName("ControlText").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_font_color", value.ToArgb().ToString()); }
        }

        public Color MenuSelectedColor
        {
            get {
                string s = FindKey("menu_selected_color");
                if (s == "")
                    s = SetKey("menu_selected_color", Color.FromName("MenuHighlight").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_selected_color", value.ToArgb().ToString()); }
        }

        public Boolean OpenFormAtCursor
        {
            get
            {
                string s = FindKey("open_form_at_cursor");
                if (s == "")
                    s = SetKey("open_form_at_cursor", "false");
                return bool.Parse(s);
            }
            set { SetKey("open_form_at_cursor", value.ToString()); }
        }

        public string PopupHotkey
        {
            get
            {
                string s = FindKey("popup_hotkey");
                if (s == "")
                    s = SetKey("popup_hotkey", "None");
                return s;
            }
            set { SetKey("popup_hotkey", value.ToString()); }
        }

        public int PopupHotkeyModifier
        {
             /* Modifier
                None = 0,
                Alt = 1,
                Control = 2,
                Shift = 4,
                WinKey = 8*/
            
            get
            {
                string s = FindKey("popup_hotkey_modifier");
                if (s == "")
                    s = SetKey("popup_hotkey_modifier", "0");
                return Convert.ToInt32(s);
            }
            set { SetKey("popup_hotkey_modifier", value.ToString()); }        
        }

        public Color PreviewBackColor
        {
            get {
                string s = FindKey("preview_back_color");
                if (s == "")
                    s = SetKey("preview_back_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("preview_back_color", value.ToArgb().ToString()); }
        }

        public Color PreviewFontColor
        {
            get {
                string s = FindKey("preview_font_color");
                if (s == "")
                    s = SetKey("preview_font_color", Color.FromName("ControlText").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("preview_font_color", value.ToArgb().ToString()); }
        }

        public int PreviewPopupDelay
        {
            get
            {
                string s = FindKey("preview_popup_delay");
                if (s == "")
                    s = SetKey("preview_popup_delay", "500");
                return Convert.ToInt32(s);
            }
            set { SetKey("preview_popup_delay", value.ToString()); }
        }

        public int PreviewMaxLines
        {
            get {
                string s = FindKey("preview_max_lines");
                if (s == "")
                    s = SetKey("preview_max_lines", "50");
                return Convert.ToInt32(s);
            }
            set { SetKey("preview_max_lines", value.ToString()); }
        }

        public Boolean StartWithWindows
        {
            get {
                RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
                string subKey = (string)key.GetValue(Application.ProductName, "");
                key.Close();

                if (subKey != "")
                {
                    return true;
                }
                else
                    return false;
            }
            set {
                if (value == false)
                {
                    RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
                    if (key != null)
                    {
                        key.DeleteValue(Application.ProductName, false);
                        key.Close();
                    }
                }
                else
                {
                    RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
                    key.SetValue(Application.ProductName, '"' + Application.ExecutablePath.ToString() + '"');
                    key.Close();
                }
            }
        }
    }
}
