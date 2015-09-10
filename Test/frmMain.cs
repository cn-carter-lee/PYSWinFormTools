using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XO.Registry.SDK;
using XO.Registry.Entities;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
namespace Test
{
    public partial class frmMain : Form
    {
        delegate void SetNumTextCallback(string text);
        delegate void SetNullTextCallback(string text);

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string url = string.Format("http://www.oxforddictionaries.com/definition/english/{0}", txtWords.Text.Trim());
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(url);
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
                Regex regx = new Regex("<article>(?<theBody>.*)</article>", options);
                Match match = regx.Match(htmlCode);
                if (match.Success)
                {
                    string theBody = match.Groups["theBody"].Value;
                    webBrowser1.DocumentText = theBody;
                }
            }
        }

        void ProcessCouple()
        {
            int flag = 0;
            string filePath = Environment.CurrentDirectory + @"\AltRetailerRegistryCode.txt";
            StreamWriter sw = new StreamWriter(filePath, true);
            long coupleId = 5703705;
            while (coupleId > 100)
            {
                try
                {
                    DataTable dt = PsyDataBase.GetDataTable(string.Format("select top 500  coupleid,CoupleRegistryId,AltRetailerRegistryCode from CoupleRegistry where IsDeleted = 0 and RetailerId in(12160,14590) and coupleid <{0} order by coupleid desc", coupleId));
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (long.TryParse(row[0].ToString(), out coupleId))
                            {
                                Couple couple = RegistryProviderFactory.CurrentProvider.GetCouple(coupleId, false);
                                foreach (CoupleRegistry registry in couple.CoupleRegistries)
                                {
                                    if (registry.RetailerId == 14590 || registry.RetailerId == 12160)
                                    {
                                        if (registry.AltRetailerRegistryCode.ToString() != row["AltRetailerRegistryCode"].ToString())
                                        {
                                            sw.WriteLine(string.Format("{0},", coupleId));
                                            SetNullText(string.Format("{0}{1}\r\n", lblNullInfo.Text, coupleId));
                                            break;
                                        }
                                    }
                                }
                                ++flag;
                                SetNumText(flag.ToString());
                            }
                        }
                    }
                    sw.Flush();
                    sw.Close();
                }
                catch { }
            }
            btnStart.Enabled = true;
        }

        void SetNumText(string text)
        {
            if (this.lblNum.InvokeRequired)
            {
                SetNumTextCallback d = new SetNumTextCallback(SetNumText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblNum.Text = text;
            }
        }

        void SetNullText(string text)
        {
            if (this.lblNum.InvokeRequired)
            {
                SetNullTextCallback d = new SetNullTextCallback(SetNullText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblNullInfo.Text = text;
            }
        }
    }
}