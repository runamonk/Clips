using System;
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
        }

        //protected override CreateParams CreateParams
        //{
        //    // Force the scrollbar to always be in position. That way we can just hide it all the time without
        //    // having to try and account for it during the autosize or resize.
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.Style |= 0x00200000; // WS_VSCROLL
        //        return cp;
        //    }
        //}

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

        protected void SetColors()
        {
            BackColor = ClipsConfig.ClipsBackColor;
        }
    }
}
