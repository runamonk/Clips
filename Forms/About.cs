using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Clips.Controls;
using Utility;

namespace Clips.Forms
{
    public partial class About : Form
    {
        public About(Config myConfig)
        {
            InitializeComponent();
            ClipsConfig = myConfig;
        }

        internal Config ClipsConfig { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = ClipsConfig.ClipsBackColor;
            ForeColor = ClipsConfig.ClipsFontColor;
            linkEmail.BackColor = BackColor;
            linkEmail.ForeColor = ForeColor;
            linkEmail.LinkColor = ForeColor;
        }

        private void About_Load(object sender, EventArgs e)
        {
            lblName.Text = Funcs.GetName();
            lblVersion.Text = "Version: " + Funcs.GetNameAndVersion();

            // Center over parent since CenterParent only works if the form is shown as a dialog.
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:" + linkEmail.Text);
        }

        private void About_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Hide();
        }
    }
}