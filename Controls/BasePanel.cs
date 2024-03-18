using System;
using System.IO;
using System.Windows.Forms;

namespace Clips.Controls
{
    public class BasePanel : Panel
    {
        internal readonly ClipMenu MenuRc;

        public BasePanel(Config myConfig)
        {
            ClipsConfig = myConfig;
            ClipsConfig.ConfigChanged += ConfigChanged;

            MenuRc = new ClipMenu(ClipsConfig)
            {
                ShowCheckMargin = false,
                ShowImageMargin = false
            };
        }

        [Obsolete]
        public BasePanel()
        {
        }

        protected virtual void ClipButtonClicked(ClipButton clip)
        {
        }

        public void DeleteClip(ClipButton clip)
        {
            if (string.IsNullOrEmpty(clip.FileName))
                return;

            if (File.Exists(clip.FileName))
                File.Delete(clip.FileName);

            if (Controls.IndexOf(clip) > -1)
            {
                clip.OnClipButtonClicked -= ClipButtonClicked;
                Controls[Controls.IndexOf(clip)].Dispose();
            }

            ClipDeleted();
        }

        protected virtual void SetColors()
        {
            BackColor = ClipsConfig.ClipsBackColor;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Hide scrollbars and then enable AutoScroll.
            AutoScroll = false;
            HorizontalScroll.Maximum = 0;
            HorizontalScroll.Visible = false;
            VerticalScroll.Maximum = 0;
            VerticalScroll.Visible = false;
            AutoScroll = true;
            DoubleBuffered = true;
            SetColors();
        }

        #region Properties

        internal Config ClipsConfig { get; set; }
        public bool InMenu { get; set; }
        public bool InLoad { get; set; }

        #endregion

        #region Events

        public delegate void ConfigChangedHandler();

        public event ConfigChangedHandler OnConfigChanged;

        public delegate void ClipAddedHandler(ClipButton clip);

        public event ClipAddedHandler OnClipAdded;

        public delegate void ClipClickedHandler(ClipButton clip);

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

        protected void ClipAdded(ClipButton clip)
        {
            OnClipAdded?.Invoke(clip);
        }

        protected void ClipClicked(ClipButton clip)
        {
            OnClipClicked?.Invoke(clip);
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
    }
}