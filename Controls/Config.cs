using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Resolve.HotKeys;
using Utility;
using static Clips.Controls.BasePanel;

namespace Clips.Controls
{
    public class Config
    {
        public const string ConfigFilename = "Clips.cfg";
        private List<string> _config;

        public Config()
        {
            _config = new List<string>();
            LoadConfiguration();
        }

        // properties
        public bool AutoHide
        {
            get
            {
                string s = FindKey("auto_hide");
                if (s == "")
                    s = SetKey("auto_hide", "false");
                return bool.Parse(s);
            }
            set => SetKey("auto_hide", value.ToString());
        }

        public bool AutoSizeHeight
        {
            get
            {
                string s = FindKey("auto_size_height");
                if (s == "")
                    s = SetKey("auto_size_height", "false");
                return bool.Parse(s);
            }
            set => SetKey("auto_size_height", value.ToString());
        }

        public Color ClipsBackColor
        {
            get
            {
                string s = FindKey("clips_back_color");
                if (s == "")
                    s = SetKey("clips_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("clips_back_color", value.ToArgb().ToString());
        }

        public Color ClipsFontColor
        {
            get
            {
                string s = FindKey("clips_font_color");
                if (s == "")
                    s = SetKey("clips_font_color", Color.Black.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("clips_font_color", value.ToArgb().ToString());
        }

        public int ClipsLinesPerRow
        {
            get
            {
                string s = FindKey("clips_lines_per_row");
                if (s == "")
                    s = SetKey("clips_lines_per_row", "1");
                return Convert.ToInt32(s);
            }
            set => SetKey("clips_lines_per_row", value.ToString());
        }

        public Color ClipsRowBackColor
        {
            get
            {
                string s = FindKey("clips_row_back_color");
                if (s == "")
                    s = SetKey("clips_row_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("clips_row_back_color", value.ToArgb().ToString());
        }

        public Color ClipsSelectedColor
        {
            get
            {
                string s = FindKey("clips_selected_color");
                if (s == "")
                    s = SetKey("clips_selected_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("clips_selected_color", value.ToArgb().ToString());
        }

        public int ClipsMaxClips
        {
            get
            {
                string s = FindKey("clips_max_clips");
                if (s == "")
                    s = SetKey("clips_max_clips", "50");
                return Convert.ToInt32(s);
            }
            set => SetKey("clips_max_clips", value.ToString());
        }

        public int ClipsToDisplay
        {
            get
            {
                string s = FindKey("clips_to_display");
                if (s == "")
                    s = SetKey("clips_to_display", "20");
                return Convert.ToInt32(s);
            }
            set => SetKey("clips_to_display", value.ToString());
        }

        public int FormLeft
        {
            get
            {
                string s = FindKey("form_left");
                if (s == "")
                    s = SetKey("form_left", "0", true);
                return Convert.ToInt32(s);
            }
            set => SetKey("form_left", value.ToString(), true);
        }

        public int FormTop
        {
            get
            {
                string s = FindKey("form_top");
                if (s == "")
                    s = SetKey("form_top", "0", true);
                return Convert.ToInt32(s);
            }
            set => SetKey("form_top", value.ToString(), true);
        }

        public Size FormSize
        {
            get
            {
                string s = FindKey("form_size");
                SizeConverter sc = new SizeConverter();

                if (s == "")
                {
                    Size sz = new Size(400, 300);
                    s = SetKey("form_size", sc.ConvertToString(sz), true);
                }

                return (Size)sc.ConvertFromString(s);
            }
            set
            {
                SizeConverter sc = new SizeConverter();
                SetKey("form_size", sc.ConvertToString(value), true);
            }
        }

        public Keys GpHotkey
        {
            get
            {
                string s = FindKey("gp_hotkey");
                if (s == "")
                    s = SetKey("gp_hotkey", "None");
                return Funcs.StringToKey(s);
            }
            set => SetKey("gp_hotkey", value.ToString());
        }

        public ModifierKey GpHotkeyModifier
        {
            /* Modifier
               None = 0,
               Alt = 1,
               Control = 2,
               Shift = 4,
               WinKey = 8*/
            get
            {
                string s = FindKey("gp_hotkey_modifier");
                if (s == "")
                    s = SetKey("gp_hotkey_modifier", "0");
                return (ModifierKey)Int32.Parse(s);
            }
            set => SetKey("gp_hotkey_modifier", ((int)value).ToString());
        }

        public int GpSize
        {
            get
            {
                string s = FindKey("gp_size");
                if (s == "")
                    s = SetKey("gp_size", "5", true);
                return Convert.ToInt32(s);
            }
            set => SetKey("gp_size", value.ToString(), true);
        }

        public bool GpIncNumbers
        {
            get
            {
                string s = FindKey("gp_incnumbers");
                if (s == "")
                    s = SetKey("gp_incnumbers", "false");
                return bool.Parse(s);
            }
            set => SetKey("gp_incnumbers", value.ToString());
        }

        public bool GpIncSymbols
        {
            get
            {
                string s = FindKey("gp_incsymbols");
                if (s == "")
                    s = SetKey("gp_incsymbols", "false");
                return bool.Parse(s);
            }
            set => SetKey("gp_incsymbols", value.ToString());
        }

        public Color HeaderBackColor
        {
            get
            {
                string s = FindKey("header_back_color");
                if (s == "")
                    s = SetKey("header_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("header_back_color", value.ToArgb().ToString());
        }

        public Color HeaderButtonColor
        {
            get
            {
                string s = FindKey("header_button_color");
                if (s == "")
                    s = SetKey("header_button_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("header_button_color", value.ToArgb().ToString());
        }

        public Color HeaderFontColor
        {
            get
            {
                string s = FindKey("header_font_color");
                if (s == "")
                    s = SetKey("header_font_color", Color.Black.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("header_font_color", value.ToArgb().ToString());
        }

        public Color HeaderButtonSelectedColor
        {
            get
            {
                string s = FindKey("header_button_selected_color");
                if (s == "")
                    s = SetKey("header_button_selected_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("header_button_selected_color", value.ToArgb().ToString());
        }

        public string IgnoreWindows
        {
            get
            {
                string s = FindKey("ignorewindows");
                if (s == "")
                    s = SetKey("ignorewindows", "");
                return s;
            }
            set => SetKey("ignorewindows", value);
        }

        public bool KeepOnTop
        {
            get
            {
                string s = FindKey("keep_on_top");
                if (s == "")
                    s = SetKey("keep_on_top", "false");
                return bool.Parse(s);
            }
            set => SetKey("keep_on_top", value.ToString());
        }

        public Color MenuBackColor
        {
            get
            {
                string s = FindKey("menu_back_color");
                if (s == "")
                    s = SetKey("menu_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("menu_back_color", value.ToArgb().ToString());
        }

        public Color MenuBorderColor
        {
            get
            {
                string s = FindKey("menu_border_color");
                if (s == "")
                    s = SetKey("menu_border_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("menu_border_color", value.ToArgb().ToString());
        }

        public Color MenuFontColor
        {
            get
            {
                string s = FindKey("menu_font_color");
                if (s == "")
                    s = SetKey("menu_font_color", Color.Black.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("menu_font_color", value.ToArgb().ToString());
        }

        public Color MenuSelectedColor
        {
            get
            {
                string s = FindKey("menu_selected_color");
                if (s == "")
                    s = SetKey("menu_selected_color", Color.Gray.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("menu_selected_color", value.ToArgb().ToString());
        }

        public bool OpenFormAtCursor
        {
            get
            {
                string s = FindKey("open_form_at_cursor");
                if (s == "")
                    s = SetKey("open_form_at_cursor", "false");
                return bool.Parse(s);
            }
            set => SetKey("open_form_at_cursor", value.ToString());
        }

        public Keys PopupHotkey
        {
            get
            {
                string s = FindKey("popup_hotkey");
                if (s == "")
                    s = SetKey("popup_hotkey", "None");
                return Funcs.StringToKey(s);
            }
            set => SetKey("popup_hotkey", value.ToString());
        }

        public ModifierKey PopupHotkeyModifier
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
                return (ModifierKey)Int32.Parse(s);
            }
            set => SetKey("popup_hotkey_modifier", ((int)value).ToString());
        }

        public Color PreviewBackColor
        {
            get
            {
                string s = FindKey("preview_back_color");
                if (s == "")
                    s = SetKey("preview_back_color", Color.White.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("preview_back_color", value.ToArgb().ToString());
        }

        public Color PreviewFontColor
        {
            get
            {
                string s = FindKey("preview_font_color");
                if (s == "")
                    s = SetKey("preview_font_color", Color.Black.ToArgb().ToString());
                return Color.FromArgb(Convert.ToInt32(s));
            }
            set => SetKey("preview_font_color", value.ToArgb().ToString());
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
            set => SetKey("preview_popup_delay", value.ToString());
        }

        public int PreviewMaxLines
        {
            get
            {
                string s = FindKey("preview_max_lines");
                if (s == "")
                    s = SetKey("preview_max_lines", "50");
                return Convert.ToInt32(s);
            }
            set => SetKey("preview_max_lines", value.ToString());
        }

        private string FindKey(string key)
        {
            foreach (string s in _config)
                if (s.Length > 0 && s.Substring(0, s.IndexOf('=')) == key)
                    return s.Substring(s.IndexOf('=') + 1, s.Length - (s.IndexOf('=') + 1));

            return "";
        }

        private int GetKeyIndex(string key)
        {
            for (int i = 0; i < _config.Count; i++)
                if (_config[i].IndexOf(key, StringComparison.Ordinal) > -1 && _config[i].Substring(0, key.Length) == key)
                    return i;
            return -1;
        }

        private void LoadConfiguration()
        {
            try
            {
                if (!File.Exists(Funcs.AppPath(ConfigFilename)))
                {
                    FileStream fs = File.Create(Funcs.AppPath(ConfigFilename));
                    fs.Close();
                }

                _config = File.ReadAllLines(Funcs.AppPath(ConfigFilename), Encoding.ASCII).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception("Cannot LoadConfiguration()" + Environment.NewLine + ee.Message);
            }
        }

        private void SaveConfiguration()
        {
            if (File.Exists(Funcs.AppPath(ConfigFilename)))
                File.Delete(Funcs.AppPath(ConfigFilename));
            File.WriteAllLines(Funcs.AppPath(ConfigFilename), _config.ToArray());
        }

        private string SetKey(string key, string value, bool saveNow = false)
        {
            int i = GetKeyIndex(key);
            if (i == -1)
                _config.Add(key + "=" + value);
            else
                _config[i] = key + "=" + value;

            if (saveNow) SaveConfiguration();
            return value;
        }

        public void ShowConfigForm(bool parentIsVisible)
        {
            Settings f = new Settings(this);
            if (!parentIsVisible)
            {
                f.StartPosition = FormStartPosition.Manual;
                Funcs.MoveFormToCursor(f);
            }
            else
            {
                f.StartPosition = FormStartPosition.CenterParent;
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                SaveConfiguration();
                ConfigChanged?.Invoke();
            }

            f.Close();
        }

        public event ConfigChangedHandler ConfigChanged;

        ~Config()
        {
            _config.Clear();
            _config = null;
        }

        private class Settings : Forms.Settings
        {
            public Settings() { }

            public Settings(Config config) { Config = config ?? throw new Exception("Config cannot be null."); }

            private Config Config { get; }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                OK.Click += OkButtonClick;

                // fill out the hotkey modifiers
                /* Modifier
                       None = 0,
                       Alt = 1,
                       Control = 2,
                       Shift = 4,
                       WinKey = 8*/

                int m = (int)Config.PopupHotkeyModifier;
                Alt.Checked = m == 1 || m == 3 || m == 5 || m == 9;
                Control.Checked = m == 2 || m == 3 || m == 6 || m == 10;
                Shift.Checked = m == 4 || m == 5 || m == 6 || m == 12;
                Windows.Checked = m == 8 || m == 9 || m == 10 || m == 12;

                m = (int)Config.GpHotkeyModifier;
                gpAlt.Checked = m == 1 || m == 3 || m == 5 || m == 9;
                gpControl.Checked = m == 2 || m == 3 || m == 6 || m == 10;
                gpShift.Checked = m == 4 || m == 5 || m == 6 || m == 12;
                gpWindows.Checked = m == 8 || m == 9 || m == 10 || m == 12;

                AutoHide.Checked = Config.AutoHide;
                AutoSizeHeight.Checked = Config.AutoSizeHeight;
                BackColor = Config.ClipsBackColor;
                ClipBackColor.BackColor = Config.ClipsBackColor;
                ClipFontColor.BackColor = Config.ClipsFontColor;
                ClipRowColor.BackColor = Config.ClipsRowBackColor;
                ClipSelected.BackColor = Config.ClipsSelectedColor;
                ClipsLinesPerRow.BackColor = Config.ClipsBackColor;
                ClipsLinesPerRow.ForeColor = Config.ClipsFontColor;
                ClipsLinesPerRow.Value = Config.ClipsLinesPerRow;
                ClipsMaxClips.BackColor = Config.ClipsBackColor;
                ClipsMaxClips.ForeColor = Config.ClipsFontColor;
                ClipsMaxClips.Value = Config.ClipsMaxClips;
                ClipsToDisplay.BackColor = Config.ClipsBackColor;
                ClipsToDisplay.ForeColor = Config.ClipsFontColor;
                ClipsToDisplay.Value = Config.ClipsToDisplay;
                ForeColor = Config.ClipsFontColor;
                gbGenPass.ForeColor = Config.ClipsFontColor;
                gbgpShortcut.ForeColor = Config.ClipsFontColor;
                gpExample.BackColor = Config.ClipsBackColor;
                gpExample.ForeColor = Config.ClipsFontColor;
                gpKey.BackColor = Config.ClipsBackColor;
                gpKey.ForeColor = Config.ClipsFontColor;
                gpKey.Text = Config.GpHotkey.ToString();
                gpNumbers.Checked = Config.GpIncNumbers;
                gpSize.Value = Config.GpSize;
                gpSymbols.Checked = Config.GpIncSymbols;
                GroupClips.ForeColor = Config.ClipsFontColor;
                GroupColorClips.ForeColor = Config.ClipsFontColor;
                GroupColorHeader.ForeColor = Config.ClipsFontColor;
                GroupColorMenu.ForeColor = Config.ClipsFontColor;
                GroupColorPreview.ForeColor = Config.ClipsFontColor;
                GroupColors.ForeColor = Config.ClipsFontColor;
                GroupHotkey.ForeColor = Config.ClipsFontColor;
                GroupPreview.ForeColor = Config.ClipsFontColor;
                HeaderBackColor.BackColor = Config.HeaderBackColor;
                HeaderButtonColor.BackColor = Config.HeaderButtonColor;
                HeaderButtonSelectedColor.BackColor = Config.HeaderButtonSelectedColor;
                HeaderFontColor.BackColor = Config.HeaderFontColor;
                IgnoreWindows.BackColor = Config.ClipsBackColor;
                IgnoreWindows.ForeColor = Config.ClipsFontColor;
                IgnoreWindows.Text = Config.IgnoreWindows;
                KeepOnTop.Checked = Config.KeepOnTop;
                Key.BackColor = Config.ClipsBackColor;
                Key.ForeColor = Config.ClipsFontColor;
                Key.Text = Config.PopupHotkey.ToString();
                MenuBackColor.BackColor = Config.MenuBackColor;
                MenuBorderColor.BackColor = Config.MenuBorderColor;
                MenuFontColor.BackColor = Config.MenuFontColor;
                MenuSelectedColor.BackColor = Config.MenuSelectedColor;
                OpenAtMouse.Checked = Config.OpenFormAtCursor;
                PreviewBackColor.BackColor = Config.PreviewBackColor;
                PreviewFontColor.BackColor = Config.PreviewFontColor;
                PreviewMaxLines.BackColor = Config.ClipsBackColor;
                PreviewMaxLines.ForeColor = Config.ClipsFontColor;
                PreviewMaxLines.Value = Config.PreviewMaxLines;
                PreviewPopupDelay.BackColor = Config.ClipsBackColor;
                PreviewPopupDelay.ForeColor = Config.ClipsFontColor;
                PreviewPopupDelay.Value = Config.PreviewPopupDelay;
                Startup.Checked = Funcs.StartWithWindows;
            }

            private void OkButtonClick(object sender, EventArgs e)
            {
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
                Config.PopupHotkeyModifier = (ModifierKey)i;

                i = 0;
                if (gpAlt.Checked) i++;
                if (gpControl.Checked) i += 2;
                if (gpShift.Checked) i += 4;
                if (gpWindows.Checked) i += 8;
                Config.GpHotkeyModifier = (ModifierKey)i;

                Config.AutoHide = AutoHide.Checked;
                Config.AutoSizeHeight = AutoSizeHeight.Checked;
                Config.ClipsBackColor = ClipBackColor.BackColor;
                Config.ClipsFontColor = ClipFontColor.BackColor;
                Config.ClipsLinesPerRow = Convert.ToInt32(ClipsLinesPerRow.Value);
                Config.ClipsMaxClips = Convert.ToInt32(ClipsMaxClips.Value);
                Config.ClipsRowBackColor = ClipRowColor.BackColor;
                Config.ClipsSelectedColor = ClipSelected.BackColor;
                Config.ClipsToDisplay = Convert.ToInt32(ClipsToDisplay.Value);
                Config.GpHotkey = Funcs.StringToKey(gpKey.Text);
                Config.GpIncNumbers = gpNumbers.Checked;
                Config.GpIncSymbols = gpSymbols.Checked;
                Config.GpSize = (int)gpSize.Value;
                Config.HeaderBackColor = HeaderBackColor.BackColor;
                Config.HeaderButtonColor = HeaderButtonColor.BackColor;
                Config.HeaderButtonSelectedColor = HeaderButtonSelectedColor.BackColor;
                Config.HeaderFontColor = HeaderFontColor.BackColor;
                Config.IgnoreWindows = IgnoreWindows.Text;
                Config.KeepOnTop = KeepOnTop.Checked;
                Config.MenuBackColor = MenuBackColor.BackColor;
                Config.MenuBorderColor = MenuBorderColor.BackColor;
                Config.MenuFontColor = MenuFontColor.BackColor;
                Config.MenuSelectedColor = MenuSelectedColor.BackColor;
                Config.OpenFormAtCursor = OpenAtMouse.Checked;
                Config.PopupHotkey = Funcs.StringToKey(Key.Text);
                Config.PreviewBackColor = PreviewBackColor.BackColor;
                Config.PreviewFontColor = PreviewFontColor.BackColor;
                Config.PreviewMaxLines = Convert.ToInt32(PreviewMaxLines.Value);
                Config.PreviewPopupDelay = Convert.ToInt32(PreviewPopupDelay.Value);
                Funcs.StartWithWindows = Startup.Checked;

                DialogResult = DialogResult.OK;
            }
        }
    }
}