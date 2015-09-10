using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CheckDomain
{
    delegate void Show(string count);

    public partial class Form1 : Form
    {
        StringBuilder sbResult = new StringBuilder();

        int iCount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (var file = new StreamReader("domain.txt"))
            {
                var url = "";
                while ((url = file.ReadLine()) != null)
                {
                    iCount++;
                    Show(iCount.ToString());
                    sbResult.AppendLine(string.Format("{0},{1}", url, CheckDoamin(url)));
                }
            }

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtResult.Text = sbResult.ToString();
        }

        private string CheckDoamin(string domain)
        {            
            string ret = "N/A";
            Process command = new Process();
            command.StartInfo.FileName = @"C:\Windows\System32\PING.EXE";
            command.StartInfo.RedirectStandardOutput = true;
            command.StartInfo.UseShellExecute = false;
            command.StartInfo.Arguments = String.Format(" {0} -n 1", domain);
            bool a = command.Start();
            var outputResult = command.StandardOutput.ReadToEnd();
            if (outputResult.Contains("199.85.97.78"))
            {
                ret = "199.85.97.78";
            }
            else if (outputResult.Contains("69.48.201.78"))
            {
                ret = "69.48.201.78";
            }
            else if (outputResult.Contains("["))
            {
                ret = outputResult.Substring(outputResult.IndexOf("[") + 1, outputResult.IndexOf("]") - outputResult.IndexOf("[")-1);
            }
            else if (!domain.Contains("www."))
            {
                ret = CheckDoamin(string.Format("www.{0}", domain));
            }
            return ret;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void Show(string count)
        {

            if (this.lblTip.InvokeRequired)
            {
                Show d = new Show(Show);
                this.Invoke(d, new object[] { count });
            }
            else
            {
                lblTip.Text = count;
            }
        }
    }
}
