using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace UW.Domain.Test
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnVisit_Click(object sender, EventArgs e)
        {
            btnVisit.Enabled = false;
            txtContent.Text = "";


            string origionString = @"276243";
            var userids = origionString.Split(',');
            foreach (var userid in userids)
            {
                string url = String.Format("http://uw.theknot.com/admin/WebServices/CustomerService/DeleteWebsite.ashx?userId={0}&partnerId=9", userid); ;
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("Apigeetkhandshake", "_-_w3lc0m3_+_");
                        string htmlCode = client.DownloadString(url);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            btnVisit.Enabled = true;

        }
    }
}
