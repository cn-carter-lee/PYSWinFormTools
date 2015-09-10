using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace PYSNeClient
{
    public partial class frmMain : Form
    {
        private TcpClient tc;
        private NetworkStream ns;

        public frmMain()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Register local's port 8888
            tc = new TcpClient("localhost", 8888);

            // Initialize tc
            ns = tc.GetStream();
            string temp = txtSend.Text;

            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            // Send message to server end
            sw.WriteLine(temp);
            sw.Flush();
            
            // Receive character string from server
            string str = sr.ReadLine();
            txtReive.Text = str;
            sr.Close();
            sw.Close();
        }
    }
}
