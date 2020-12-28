using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace Clips
{
    public partial class formAbout : Form
    {
        public formAbout()
        {
            InitializeComponent();
        }

        private void formAbout_Load(object sender, EventArgs e)
        {
            lblName.Text = Funcs.GetName();
            lblVersion.Text = "Version: " + Funcs.GetVersion();

            // Center over parent since CenterParent only works if the form is shown as a dialog.
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkEmail.Text);
        }

        private void formAbout_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
