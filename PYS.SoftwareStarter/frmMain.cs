using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PYS.SoftwareStarter
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private PysImageBox box;
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            // this.Location = new Point(-100, -100);

            box = new PysImageBox();
            box.Image = global::PYS.SoftwareStarter.Properties.Resources.task;
            box.Location = new System.Drawing.Point(23, 23);
            this.Controls.Add(box);
        }
    }

    public class PysImageBox : PictureBox
    {
        public string ApplicationName { get; set; }

        public PysImageBox()
            : base()
        {
            this.TabStop = false;
            this.Size = new System.Drawing.Size(36, 36);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Cursor = Cursors.Hand;
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
            this.Cursor = Cursors.Arrow;
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\cli\Desktop\tools\Proxy.exe");
            this.Parent.Visible = false;
            base.OnClick(e);
        }
    }
}
