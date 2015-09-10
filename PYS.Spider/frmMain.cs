using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace PysSpider
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                // client.DownloadFile("http://yoursite.com/page.html", @"C:\localfile.html");
                // Or you can get the file content without saving it:
                string htmlCode = client.DownloadString("http://www.china.com.cn/book/node_7063582.htm");
            }
        }
    }
}
