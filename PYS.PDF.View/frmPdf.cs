using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace PYS.PDF.View
{
    public partial class frmPdf : Form
    {
        private const int StepSize = 50;
        public frmPdf()
        {
            InitializeComponent();
            this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(frmPdf_KeyPress);
            this.KeyUp += new KeyEventHandler(frmPdf_KeyUp);
        }

        void frmPdf_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a': { this.Location = new Point(this.Location.X - 5, this.Location.Y); } break;
                case 'd': { this.Location = new Point(this.Location.X + 5, this.Location.Y); } break;
                case 'w': { this.Location = new Point(this.Location.X, this.Location.Y - 5); } break;
                case 's': { this.Location = new Point(this.Location.X, this.Location.Y + 5); } break;
                default: break;
            }
        }

        void frmPdf_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // case Keys.Tab: { this.Visible = false; } break;
                case Keys.A: { this.Location = new Point(this.Location.X - StepSize, this.Location.Y); } break;
                case Keys.D: { this.Location = new Point(this.Location.X + StepSize, this.Location.Y); } break;
                case Keys.W: { this.Location = new Point(this.Location.X, this.Location.Y - StepSize); } break;
                case Keys.S: { this.Location = new Point(this.Location.X, this.Location.Y + StepSize); } break;

                default: break;
            }
        }

        private void frmPdf_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri(ConfigurationManager.AppSettings["url"]);
            // this.Location = new Point(100, 700);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = !this.Visible;
            if (this.Visible)
            {
                this.Show();
                // this.TopMost = true;
                this.BringToFront();
            }
        }

        private void frmPdf_Deactivate(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
