using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PYS.MemCached
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Couple couple = new Couple()
            {
                CoupleId = 1,
                Name = "CarterLee"
            };

            CacheService.Set(couple.CoupleId.ToString(), couple);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            Couple couple = CacheService.Get("1") as Couple;
        }
    }
}
