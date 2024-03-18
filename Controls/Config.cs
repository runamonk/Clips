using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                var s = FindKey("auto_hide");
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
                var s = FindKey("auto_size_height");
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
                var s = FindKey("clips_back_color");
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
                var s = FindKey("clips_font_color");
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
                var s = FindKey("clips_lines_per_row");
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
                var s = FindKey("clips_row_back_color");
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
                var s = FindKey("clips_selected_color");
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
                var s = FindKey("clips_max_clips");
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
                var s = FindKey("clips_to_display");
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
                var s = FindKey("form_left");
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
                var s = FindKey("form_top");
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
                var s = FindKey("form_size");
                var sc = new SizeConverter();

                if (s == "")
                {
                    var sz = new Size(400, 300);
                    s = SetKey("form_size", sc.ConvertToString(sz), true);
                }

                return (Size)sc.ConvertFromString(s);
            }
            set
            {
                var sc = new SizeConverter();
                SetKey("form_size", sc.ConvertToString(value), true);
            }
        }

        public string GpHotkey
        {
            get
            {
                var s = FindKey("gp_hotkey");
                if (s == "")
                    s = SetKey("gp_hotkey", "None");
                return s;
            }
            set => SetKey("gp_hotkey", value);
        }

        public int GpHotkeyModifier
        {
            /* Modifier
               None = 0,
               Alt = 1,
               Control = 2,
               Shift = 4,
               WinKey = 8*/

            get
            {
                var s = FindKey("gp_hotkey_modifier");
                if (s == "")
                    s = SetKey("gp_hotkey_modifier", "0");
                return Convert.ToInt32(s);
            }
            set => SetKey("gp_hotkey_modifier", value.ToString());
        }

        public int GpSize
        {
            get
            {
                var s = FindKey("gp_size");
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
                var s = FindKey("gp_incnumbers");
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
                var s = FindKey("gp_incsymbols");
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
                var s = FindKey("header_back_color");
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
                var s = FindKey("header_button_color");
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
                var s = FindKey("header_font_color");
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
                var s = FindKey("header_button_selected_color");
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
                var s = FindKey("ignorewindows");
                if (s == "")
                    s = SetKey("ignorewindows", "");
                return s;
            }
            set => SetKey("ignorewindows", value);
        }

        public Color MenuBackColor
        {
            get
            {
                var s = FindKey("menu_back_color");
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
                var s = FindKey("menu_border_color");
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
                var s = FindKey("menu_font_color");
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
                var s = FindKey("menu_selected_color");
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
                var s = FindKey("open_form_at_cursor");
                if (s == "")
                    s = SetKey("open_form_at_cursor", "false");
                return bool.Parse(s);
            }
            set => SetKey("open_form_at_cursor", value.ToString());
        }

        public string PopupHotkey
        {
            get
            {
                var s = FindKey("popup_hotkey");
                if (s == "")
                    s = SetKey("popup_hotkey", "None");
                return s;
            }
            set => SetKey("popup_hotkey", value);
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
                var s = FindKey("popup_hotkey_modifier");
                if (s == "")
                    s = SetKey("popup_hotkey_modifier", "0");
                return Convert.ToInt32(s);
            }
            set => SetKey("popup_hotkey_modifier", value.ToString());
        }

        public Color PreviewBackColor
        {
            get
            {
                var s = FindKey("preview_back_color");
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
                var s = FindKey("preview_font_color");
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
                var s = FindKey("preview_popup_delay");
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
                var s = FindKey("preview_max_lines");
                if (s == "")
                    s = SetKey("preview_max_lines", "50");
                return Convert.ToInt32(s);
            }
            set => SetKey("preview_max_lines", value.ToString());
        }

        public event ConfigChangedHandler ConfigChanged;

        ~Config()
        {
            _config.Clear();
            _config = null;
        }

        private int GetKeyIndex(string key)
        {
            for (var i = 0; i < _config.Count; i++)
                if (_config[i].IndexOf(key, StringComparison.Ordinal) > -1 && _config[i].Substring(0, key.Length) == key)
                    return i;
            return -1;
        }

        private string FindKey(string key)
        {
            foreach (var s in _config)
                if (s.Length > 0 && s.Substring(0, s.IndexOf('=')) == key)
                    return s.Substring(s.IndexOf('=') + 1, s.Length - (s.IndexOf('=') + 1));

            return "";
        }

        private string SetKey(string key, string value, bool saveNow = false)
        {
            var i = GetKeyIndex(key);
            if (i == -1)
                _config.Add(key + "=" + value);
            else
                _config[i] = key + "=" + value;

            if (saveNow) SaveConfiguration();
            return value;
        }

        private void LoadConfiguration()
        {
            try
            {
                if (!File.Exists(Funcs.AppPath(ConfigFilename)))
                {
                    var fs = File.Create(Funcs.AppPath(ConfigFilename));
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

        public void ShowConfigForm(bool parentIsVisible)
        {
            var f = new Settings(this);
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

        private class Settings : Forms.Settings
        {
            public Settings()
            {
            }

            public Settings(Config config)
            {
                Config = config ?? throw new Exception("Config cannot be null.");
            }

            private Config Config { get; }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                OK.Click += OkButtonClick;
                Key.Text = Config.PopupHotkey;

                // fill out the hotkey modifiers
                /* Modifier
                       None = 0,
                       Alt = 1,
                       Control = 2,
                       Shift = 4,
                       WinKey = 8*/

                var m = Config.PopupHotkeyModifier;
                Alt.Checked = m == 1 || m == 3 || m == 5 || m == 9;
                Control.Checked = m == 2 || m == 3 || m == 6 || m == 10;
                Shift.Checked = m == 4 || m == 5 || m == 6 || m == 12;
                Windows.Checked = m == 8 || m == 9 || m == 10 || m == 12;
                Startup.Checked = Funcs.StartWithWindows;
                AutoHide.Checked = Config.AutoHide;
                AutoSizeHeight.Checked = Config.AutoSizeHeight;
                ClipBackColor.BackColor = Config.ClipsBackColor;
                ClipFontColor.BackColor = Config.ClipsFontColor;
                HeaderBackColor.BackColor = Config.HeaderBackColor;
                HeaderButtonColor.BackColor = Config.HeaderButtonColor;
                HeaderButtonSelectedColor.BackColor = Config.HeaderButtonSelectedColor;
                HeaderFontColor.BackColor = Config.HeaderFontColor;
                ClipRowColor.BackColor = Config.ClipsRowBackColor;
                ClipsLinesPerRow.Value = Config.ClipsLinesPerRow;
                ClipsMaxClips.Value = Config.ClipsMaxClips;
                ClipsToDisplay.Value = Config.ClipsToDisplay;
                ClipSelected.BackColor = Config.ClipsSelectedColor;
                MenuBackColor.BackColor = Config.MenuBackColor;
                MenuBorderColor.BackColor = Config.MenuBorderColor;
                MenuFontColor.BackColor = Config.MenuFontColor;
                MenuSelectedColor.BackColor = Config.MenuSelectedColor;
                OpenAtMouse.Checked = Config.OpenFormAtCursor;
                PreviewBackColor.BackColor = Config.PreviewBackColor;
                PreviewFontColor.BackColor = Config.PreviewFontColor;
                PreviewMaxLines.Value = Config.PreviewMaxLines;
                PreviewPopupDelay.Value = Config.PreviewPopupDelay;
                BackColor = Config.ClipsBackColor;
                ForeColor = Config.ClipsFontColor;
                GroupClips.ForeColor = Config.ClipsFontColor;
                GroupColorHeader.ForeColor = Config.ClipsFontColor;
                GroupColorClips.ForeColor = Config.ClipsFontColor;
                GroupColorMenu.ForeColor = Config.ClipsFontColor;
                GroupColorPreview.ForeColor = Config.ClipsFontColor;
                GroupColors.ForeColor = Config.ClipsFontColor;
                GroupHotkey.ForeColor = Config.ClipsFontColor;
                GroupPreview.ForeColor = Config.ClipsFontColor;
                gbGenPass.ForeColor = Config.ClipsFontColor;
                gbgpShortcut.ForeColor = Config.ClipsFontColor;
                gpKey.ForeColor = Config.ClipsFontColor;
                gpKey.BackColor = Config.ClipsBackColor;
                gpExample.ForeColor = Config.ClipsFontColor;
                gpExample.BackColor = Config.ClipsBackColor;
                IgnoreWindows.Text = Config.IgnoreWindows;
                IgnoreWindows.BackColor = Config.ClipsBackColor;
                IgnoreWindows.ForeColor = Config.ClipsFontColor;
                Key.BackColor = Config.ClipsBackColor;
                ClipsMaxClips.BackColor = Config.ClipsBackColor;
                ClipsLinesPerRow.BackColor = Config.ClipsBackColor;
                PreviewMaxLines.BackColor = Config.ClipsBackColor;
                PreviewPopupDelay.BackColor = Config.ClipsBackColor;
                Key.ForeColor = Config.ClipsFontColor;
                ClipsMaxClips.ForeColor = Config.ClipsFontColor;
                ClipsLinesPerRow.ForeColor = Config.ClipsFontColor;
                ClipsToDisplay.BackColor = Config.ClipsBackColor;
                ClipsToDisplay.ForeColor = Config.ClipsFontColor;
                PreviewMaxLines.ForeColor = Config.ClipsFontColor;
                PreviewPopupDelay.ForeColor = Config.ClipsFontColor;
                gpKey.Text = Config.GpHotkey;
                m = Config.GpHotkeyModifier;
                gpAlt.Checked = m == 1 || m == 3 || m == 5 || m == 9;
                gpControl.Checked = m == 2 || m == 3 || m == 6 || m == 10;
                gpShift.Checked = m == 4 || m == 5 || m == 6 || m == 12;
                gpWindows.Checked = m == 8 || m == 9 || m == 10 || m == 12;
                gpNumbers.Checked = Config.GpIncNumbers;
                gpSymbols.Checked = Config.GpIncSymbols;
                gpSize.Value = Config.GpSize;
            }

            private void OkButtonClick(object sender, EventArgs e)
            {
                Config.PopupHotkey = Key.Text;
                /* Modifier
                   None = 0,
                   Alt = 1,
                   Control = 2,
                   Shift = 4,
                   WinKey = 8*/
                var i = 0;

                if (Alt.Checked) i++;
                if (Control.Checked) i += 2;
                if (Shift.Checked) i += 4;
                if (Windows.Checked) i += 8;
                Config.PopupHotkeyModifier = i;
                Config.AutoHide = AutoHide.Checked;
                Config.AutoSizeHeight = AutoSizeHeight.Checked;
                Config.ClipsBackColor = ClipBackColor.BackColor;
                Config.ClipsFontColor = ClipFontColor.BackColor;
                Config.ClipsLinesPerRow = Convert.ToInt32(ClipsLinesPerRow.Value);
                Config.ClipsMaxClips = Convert.ToInt32(ClipsMaxClips.Value);
                Config.ClipsRowBackColor = ClipRowColor.BackColor;
                Config.ClipsToDisplay = Convert.ToInt32(ClipsToDisplay.Value);
                Config.ClipsSelectedColor = ClipSelected.BackColor;
                Config.GpHotkey = gpKey.Text;
                i = 0;
                if (gpAlt.Checked) i++;
                if (gpControl.Checked) i += 2;
                if (gpShift.Checked) i += 4;
                if (gpWindows.Checked) i += 8;
                Config.GpHotkeyModifier = i;
                Config.GpIncNumbers = gpNumbers.Checked;
                Config.GpIncSymbols = gpSymbols.Checked;
                Config.GpSize = (int)gpSize.Value;
                Config.HeaderBackColor = HeaderBackColor.BackColor;
                Config.HeaderButtonColor = HeaderButtonColor.BackColor;
                Config.HeaderButtonSelectedColor = HeaderButtonSelectedColor.BackColor;
                Config.HeaderFontColor = HeaderFontColor.BackColor;
                Config.IgnoreWindows = IgnoreWindows.Text;
                Config.MenuBackColor = MenuBackColor.BackColor;
                Config.MenuBorderColor = MenuBorderColor.BackColor;
                Config.MenuFontColor = MenuFontColor.BackColor;
                Config.MenuSelectedColor = MenuSelectedColor.BackColor;
                Config.OpenFormAtCursor = OpenAtMouse.Checked;
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