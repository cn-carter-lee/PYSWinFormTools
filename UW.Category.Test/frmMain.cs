using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Security.Cryptography;

namespace UW.Category.Test
{
    public partial class frmMain : Form
    {
        delegate void AddViewItem(string catalog, string category, string count);
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            lvwResult.Items.Clear();
            btnTest.Enabled = false;
            Thread thread = new Thread(new ThreadStart(ProcessCategory));
            thread.Start();
        }

        void ProcessCategory()
        {
            try
            {
                string[] catalogString = { "KnotShop", "WeddingChannel" };
                string categoryString = txtCategoryString.Text.Trim();
                foreach (var catalog in catalogString)
                {
                    foreach (var category in categoryString.Split(','))
                    {
                        string categoryName = category.Split(':')[1];
                        int count = CheckIfExistedItems(catalog, categoryName);
                        AddViewItemToList(catalog, category.Split(':')[0], count.ToString());
                    }
                }
            }
            catch { }
        }

        void AddViewItemToList(string catalog, string category, string count)
        {
            if (this.lvwResult.InvokeRequired)
            {
                AddViewItem d = new AddViewItem(AddViewItemToList);
                this.Invoke(d, new object[] { catalog, category, count });
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Text = catalog;
                item.SubItems.Add(category);
                item.SubItems.Add(count);
                lvwResult.Items.Add(item);
            }
        }

        private int CheckIfExistedItems(string catalogName, string categoryName)
        {
            int count = 0;
            string url = string.Format("http://rss.commerce.theknot.com/RSSFeed.ashx?catalogName={0}&categoryName={1}&resultCount=50", catalogName, categoryName);
            WebRequest req = WebRequest.Create(url);
            WebResponse rsp = null;
            string str = string.Empty;
            XmlDocument doc = new XmlDocument();
            try
            {
                rsp = req.GetResponse();

                using (Stream sr = rsp.GetResponseStream())
                {
                    doc.Load(sr);
                    XmlNodeList list = doc.GetElementsByTagName("item");
                    count = list.Count;
                }

                rsp.Close();
            }
            catch (WebException x)
            {
                string s = x.Message;
                if (s.ToLower() == "unable to connect to the remote server")
                {
                    throw;
                }
            }
            return count;
        }


        #region UW Test

        private static byte[] IV_192 = new byte[] { 0x21, 0x36, 0x6c, 0x9b, 0xcc, 0xde, 0x31, 0x76 };
        private static byte[] KEY_192 = new byte[] { 0xde, 0x42, 0xdb, 0x76, 0x2c, 0x87, 0x48, 0x66 };
        private static byte[] KEY_HMAC = new byte[] { 
            0x4b, 0x4c, 0x41, 0x53, 0x4a, 0x44, 70, 0x4c, 0x55, 0x33, 0x34, 0x38, 0x37, 0x45, 0x52, 0x48, 
            0x4a, 0x38, 0x39, 0x47, 0x59, 50, 0x34, 0x4a, 0x49, 0x4f, 80, 0x51, 70, 0x4a, 0x43, 0x37, 
            0x39, 0x30, 0x33, 0x34, 0x39, 0x30, 0x56, 0x33, 0x34, 0x39, 0x30, 0x56, 0x4d, 0x48, 0x47, 0x4a, 
            0x30, 0x44, 0x4b, 0x58, 0x57, 0x33, 0x30, 0x48, 0x47, 0x43, 50, 0x39, 0x30, 0x43, 0x59, 0x35
         };

        public static string DecryptDesFromWeb(string input)
        {
            if ((input != null) && (input.Length != 0))
            {
                return DecryptDes(System.Web.HttpUtility.UrlDecode(input));
            }
            return string.Empty;
        }

        public static string DecryptDes(string input)
        {
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(input);
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
            catch (FormatException)
            {
                return string.Empty;
            }
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = null;
            CryptoStream stream2 = null;
            StreamReader reader = null;
            string str = string.Empty;
            try
            {
                stream = new MemoryStream(buffer);
                stream2 = new CryptoStream(stream, provider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
                reader = new StreamReader(stream2);
                str = reader.ReadLine();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return str;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sbResult = new StringBuilder();
            foreach (ListViewItem item in lvwResult.Items)
            {
                sbResult.AppendLine(string.Format("{0},{1},{2}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text));
            }

            txtResult.Text = sbResult.ToString();
        }
    }
}
