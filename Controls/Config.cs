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

        private partial class Settings : Clips.Settings
        {
            public Settings()
            {

            }

            public Settings(Config Config)
            {
                if (Config == null)
                    throw new Exception("Config cannot be null.");
                else
                {
                    _Config = Config;
                    OK.Click += new EventHandler(ButtonClick);
                    Key.Text = _Config.PopupHotkey;

                    // fill out the hotkey modifiers
                    /* Modifier
                       None = 0,
                       Alt = 1,
                       Control = 2,
                       Shift = 4,
                       WinKey = 8*/

                    int m = _Config.PopupHotkeyModifier;
                    Alt.Checked = (m == 1 || m == 3 || m == 5 || m == 9);
                    Control.Checked = (m == 2 || m == 3 || m == 6 || m == 10);
                    Shift.Checked = (m == 4 || m == 5 || m == 6 || m == 12);
                    Windows.Checked = (m == 8 || m == 9 || m == 10 || m == 12);
                    Startup.Checked = _Config.StartWithWindows;
                    AutoHide.Checked = _Config.AutoHide;
                    AutoSizeHeight.Checked = _Config.AutoSizeHeight;
                    ClipBackColor.BackColor = _Config.ClipsBackColor;
                    ClipFontColor.BackColor = _Config.ClipsFontColor;
                    HeaderBackColor.BackColor = _Config.HeaderBackColor;
                    HeaderButtonColor.BackColor = _Config.HeaderButtonColor;
                    HeaderButtonSelectedColor.BackColor = _Config.HeaderButtonSelectedColor;
                    HeaderFontColor.BackColor = _Config.HeaderFontColor;
                    ClipRowColor.BackColor = _Config.ClipsRowBackColor;
                    ClipsLinesPerRow.Value = _Config.ClipsLinesPerRow;
                    ClipsMaxClips.Value = _Config.ClipsMaxClips;
                    ClipsToDisplay.Value = _Config.ClipsToDisplay;
                    ClipSelected.BackColor = _Config.ClipsSelectedColor;
                    MenuBackColor.BackColor = _Config.MenuBackColor;
                    MenuBorderColor.BackColor = _Config.MenuBorderColor;
                    MenuFontColor.BackColor = _Config.MenuFontColor;
                    MenuSelectedColor.BackColor = _Config.MenuSelectedColor;
                    OpenAtMouse.Checked = _Config.OpenFormAtCursor;
                    PreviewBackColor.BackColor = _Config.PreviewBackColor;
                    PreviewFontColor.BackColor = _Config.PreviewFontColor;
                    PreviewMaxLines.Value = _Config.PreviewMaxLines;
                    PreviewPopupDelay.Value = _Config.PreviewPopupDelay;
                    BackColor = _Config.ClipsBackColor;
                    ForeColor = _Config.ClipsFontColor;
                    GroupClips.ForeColor = _Config.ClipsFontColor;
                    GroupColorHeader.ForeColor = _Config.ClipsFontColor;
                    GroupColorClips.ForeColor = _Config.ClipsFontColor;
                    GroupColorMenu.ForeColor = _Config.ClipsFontColor;
                    GroupColorPreview.ForeColor = _Config.ClipsFontColor;
                    GroupColors.ForeColor = _Config.ClipsFontColor;
                    GroupHotkey.ForeColor = _Config.ClipsFontColor;
                    GroupPreview.ForeColor = _Config.ClipsFontColor;
                    Key.BackColor = _Config.ClipsBackColor;
                    ClipsMaxClips.BackColor = _Config.ClipsBackColor;
                    ClipsLinesPerRow.BackColor = _Config.ClipsBackColor;
                    PreviewMaxLines.BackColor = _Config.ClipsBackColor;
                    PreviewPopupDelay.BackColor = _Config.ClipsBackColor;
                    Key.ForeColor = _Config.ClipsFontColor;
                    ClipsMaxClips.ForeColor = _Config.ClipsFontColor;
                    ClipsLinesPerRow.ForeColor = _Config.ClipsFontColor;
                    ClipsToDisplay.BackColor = _Config.ClipsBackColor;
                    ClipsToDisplay.ForeColor = _Config.ClipsFontColor;
                    PreviewMaxLines.ForeColor = _Config.ClipsFontColor;
                    PreviewPopupDelay.ForeColor = _Config.ClipsFontColor;
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
                _Config.PopupHotkey = Key.Text;
                /* Modifier
                   None = 0,
                   Alt = 1,
                   Control = 2,
                   Shift = 4,
                   WinKey = 8*/
                int i = 0;
                if (Alt.Checked) i++;
                if (Control.Checked) i += 2;
                if (Shift.Checked) i += 4;
                if (Windows.Checked) i += 8;
                _Config.AutoHide = AutoHide.Checked;
                _Config.AutoSizeHeight = AutoSizeHeight.Checked;
                _Config.ClipsBackColor = ClipBackColor.BackColor;
                _Config.ClipsFontColor = ClipFontColor.BackColor;                
                _Config.ClipsLinesPerRow = Convert.ToInt32(ClipsLinesPerRow.Value);
                _Config.ClipsMaxClips = Convert.ToInt32(ClipsMaxClips.Value);
                _Config.ClipsRowBackColor = ClipRowColor.BackColor;
                _Config.ClipsToDisplay = Convert.ToInt32(ClipsToDisplay.Value);
                _Config.ClipsSelectedColor = ClipSelected.BackColor;
                _Config.HeaderBackColor = HeaderBackColor.BackColor;
                _Config.HeaderButtonColor = HeaderButtonColor.BackColor;
                _Config.HeaderButtonSelectedColor = HeaderButtonSelectedColor.BackColor;
                _Config.HeaderFontColor = HeaderFontColor.BackColor;
                _Config.MenuBackColor = MenuBackColor.BackColor;
                _Config.MenuBorderColor = MenuBorderColor.BackColor;
                _Config.MenuFontColor = MenuFontColor.BackColor;
                _Config.MenuSelectedColor = MenuSelectedColor.BackColor;
                _Config.OpenFormAtCursor = OpenAtMouse.Checked;
                _Config.PopupHotkeyModifier = i;
                _Config.PreviewBackColor = PreviewBackColor.BackColor;
                _Config.PreviewFontColor = PreviewFontColor.BackColor;
                _Config.PreviewMaxLines = Convert.ToInt32(PreviewMaxLines.Value);
                _Config.PreviewPopupDelay = Convert.ToInt32(PreviewPopupDelay.Value);
                _Config.StartWithWindows = Startup.Checked;

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

        public void ShowConfigForm(bool ParentIsVisible)
        {
            Settings f = new Settings(this);
            if (!ParentIsVisible)
            {
                f.StartPosition = FormStartPosition.Manual;
                Funcs.MoveFormToCursor(f, false);
            }
            else
            {
                f.StartPosition = FormStartPosition.CenterParent;
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                SaveConfiguration();
                ConfigChanged(this, null);
            }
            f.Close();
        }

        // properties
        public Boolean AutoHide
        {
            get {
                string s = FindKey("auto_hide");
                if (s == "")
                    s = SetKey("auto_hide", "false");
                return bool.Parse(s);
            }
            set { SetKey("auto_hide", value.ToString()); }
        }

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
                    s = SetKey("clips_back_color", Color.White.ToArgb().ToString());
                    return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_back_color", value.ToArgb().ToString()); }
        }

        public Color ClipsFontColor
        {
            get {
                string s = FindKey("clips_font_color");
                if (s == "")
                    s = SetKey("clips_font_color", Color.Black.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_font_color", value.ToArgb().ToString()); }
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
                    s = SetKey("clips_row_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_row_back_color", value.ToArgb().ToString()); }
        }

        public Color ClipsSelectedColor
        {
            get {
                string s = FindKey("clips_selected_color");
                if (s == "")
                    s = SetKey("clips_selected_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("clips_selected_color", value.ToArgb().ToString()); }
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

        public int ClipsToDisplay
        {
            get {
                string s = FindKey("clips_to_display");
                if (s == "")
                    s = SetKey("clips_to_display", "20");
                return Convert.ToInt32(s);
            }
            set { SetKey("clips_to_display", value.ToString()); }
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
                Size sz;
                SizeConverter sc;

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

        public Color HeaderBackColor
        {
            get {
                string s = FindKey("header_back_color");
                if (s == "")
                    s = SetKey("header_back_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("header_back_color", value.ToArgb().ToString()); }
        }

        public Color HeaderButtonColor
        {
            get {
                string s = FindKey("header_button_color");
                if (s == "")
                    s = SetKey("header_button_color", Color.FromName("Control").ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("header_button_color", value.ToArgb().ToString()); }
        }

        public Color HeaderFontColor
        {
            get {
                string s = FindKey("header_font_color");
                if (s == "")
                    s = SetKey("header_font_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("header_font_color", value.ToArgb().ToString()); }
        }

        public Color HeaderButtonSelectedColor
        {
            get {
                string s = FindKey("header_button_selected_color");
                if (s == "")
                    s = SetKey("header_button_selected_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("header_button_selected_color", value.ToArgb().ToString()); }
        }

        public Color MenuBackColor
        {
            get {
                string s = FindKey("menu_back_color");
                if (s == "")
                    s = SetKey("menu_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_back_color", value.ToArgb().ToString()); }
        }

        public Color MenuBorderColor
        {
            get {
                string s = FindKey("menu_border_color");
                if (s == "")
                    s = SetKey("menu_border_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_border_color", value.ToArgb().ToString()); }
        }

        public Color MenuFontColor
        {
            get {
                string s = FindKey("menu_font_color");
                if (s == "")
                    s = SetKey("menu_font_color", Color.Black.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("menu_font_color", value.ToArgb().ToString()); }
        }

        public Color MenuSelectedColor
        {
            get {
                string s = FindKey("menu_selected_color");
                if (s == "")
                    s = SetKey("menu_selected_color", Color.Gray.ToArgb().ToString());
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
                    s = SetKey("preview_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set { SetKey("preview_back_color", value.ToArgb().ToString()); }
        }

        public Color PreviewFontColor
        {
            get {
                string s = FindKey("preview_font_color");
                if (s == "")
                    s = SetKey("preview_font_color", Color.Black.ToArgb().ToString());
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
                var k = key.GetValue(Funcs.GetFileName());

                if (k != null)
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
                    var k = key.GetValue(Funcs.GetFileName());
                    if ((k != null) && (!k.ToString().Contains(Funcs.GetFilePathAndName())))
                        return;

                    key.DeleteValue(Funcs.GetFileName(), false);
                    key.Close();
                }
                else
                {
                    RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
                    var k = key.GetValue(Funcs.GetFileName());
                    if ((k != null) && (!k.ToString().Contains(Funcs.GetFilePathAndName())))
                        return;

                    key.SetValue(Funcs.GetFileName(), '"' + Funcs.GetFilePathAndName() + '"');
                    key.Close();
                }
            }
        }
    }
}
