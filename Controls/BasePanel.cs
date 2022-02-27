using System;
using System.IO;
using System.Windows.Forms;

namespace Clips.Controls
{
    public partial class BasePanel : Panel
    {
        public BasePanel(Config myConfig)
        {
            // Hide scrollbars and then enable AutoScroll.
            AutoScroll = false;
            HorizontalScroll.Maximum = 0;
            HorizontalScroll.Visible = false;
            VerticalScroll.Maximum = 0;
            VerticalScroll.Visible = false;
            AutoScroll = true;
            DoubleBuffered = true;

            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += new ConfigChangedHandler(ConfigChanged);

            MenuRC = new ClipMenu(ClipsConfig)
            {
                ShowCheckMargin = false,
                ShowImageMargin = false
            };

            SetColors();
        }

        [Obsolete]
        public BasePanel()
        {

        }

        #region Properties
        internal Config ClipsConfig { get; set; }
        public bool InMenu { get; set; }
        public bool InLoad { get; set; }
        #endregion

        internal readonly ClipMenu MenuRC;

        #region Events
        public delegate void ConfigChangedHandler();
        public event ConfigChangedHandler OnConfigChanged;

        public delegate void ClipAddedHandler(ClipButton Clip);
        public event ClipAddedHandler OnClipAdded;

        public delegate void ClipClickedHandler(ClipButton Clip);
        public event ClipClickedHandler OnClipClicked;

        public delegate void ClipDeletedHandler();
        public event ClipDeletedHandler OnClipDeleted;

        public delegate void ClipsLoadedHandler();
        public event ClipsLoadedHandler OnClipsLoaded;

        protected virtual void ConfigChanged()
        {
            SetColors();
            OnConfigChanged?.Invoke();
        }

        protected void ClipAdded(ClipButton Clip)
        {
            OnClipAdded?.Invoke(Clip);
        }

        protected void ClipClicked(ClipButton Clip)
        {
            OnClipClicked?.Invoke(Clip);
        }

        protected void ClipDeleted()
        {
            OnClipDeleted?.Invoke();
        }

        protected void ClipsLoaded()
        {
            OnClipsLoaded?.Invoke();
        }
        #endregion

        protected virtual void ClipButtonClicked(ClipButton Clip)
        {

        }

        public void DeleteClip(ClipButton Clip)
        {
            if (string.IsNullOrEmpty(Clip.FileName))
                return;

            if (File.Exists(Clip.FileName))
                File.Delete(Clip.FileName);

            if (Controls.IndexOf(Clip) > -1)
            {
                Clip.OnClipButtonClicked -= ClipButtonClicked;
                Controls[Controls.IndexOf(Clip)].Dispose();
            }
        }

        protected virtual void SetColors()
        {
            BackColor = ClipsConfig.ClipsBackColor;
        }
    }
}
