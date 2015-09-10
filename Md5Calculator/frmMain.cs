using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Md5Calculator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void tsbtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    ListViewItem item = new ListViewItem();
                    FileInfo fileInfo = new FileInfo(file);
                    item.Text = fileInfo.Name;
                    item.SubItems.Add(CalculateMd5CheckSum(fileInfo.FullName));
                    lvwFiles.Items.Add(item);
                }
            }
        }

        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            lvwFiles.Items.Clear();
        }

        private string CalculateMd5CheckSum(string fileLocation)
        {
            System.Security.Cryptography.HashAlgorithm hmacMd5 = new System.Security.Cryptography.HMACMD5();
            byte[] hashByte;
            using (Stream fileStream = new FileStream(fileLocation, FileMode.Open))
            using (Stream bufferedStream = new BufferedStream(fileStream, 1200000))
                hashByte = hmacMd5.ComputeHash(bufferedStream);
            StringBuilder sbResult = new StringBuilder();
            for (int i = 0; i < hashByte.Length; i++)
            {
                sbResult.Append(hashByte[i].ToString("x2"));
            }
            return sbResult.ToString();
        }
    }
}
