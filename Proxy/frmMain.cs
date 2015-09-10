using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;

namespace LocalAreaNetwork_Setting
{
    public partial class frmMain : Form
    {
        [DllImport("wininet.dll")]
        static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        const int INTERNET_OPTION_REFRESH = 37;
        InternetKey internetKey;
        QuickSwichKey quickSwichKey;

        public frmMain()
        {
            internetKey = new InternetKey();
            quickSwichKey = new QuickSwichKey();
            InitializeComponent();

            DateTime dt = DateTime.Now;
            dt.ToString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Proxy setting
            txtAddress.Text = internetKey.ProxyServer.Substring(0, internetKey.ProxyServer.IndexOf(":"));
            txtPort.Text = internetKey.ProxyServer.Substring(internetKey.ProxyServer.IndexOf(":") + 1);
            chkProxyEnable.Checked = internetKey.ProxyEnable;
            chkProxyEnable.CheckState = internetKey.ProxyEnable ? CheckState.Checked : CheckState.Unchecked;
            txtAddress.Enabled = internetKey.ProxyEnable;
            txtPort.Enabled = internetKey.ProxyEnable;
            UpdateTrayBar(true);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            internetKey.SetValue(chkProxyEnable.Checked, string.Format("{0}:{1}", txtAddress.Text.Trim(), txtPort.Text.Trim()));
            UpdateTrayBar(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateTrayBar(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            internetKey.SetValue(!internetKey.ProxyEnable, string.Format("{0}:{1}", txtAddress.Text.Trim(), txtPort.Text.Trim()));
            chkProxyEnable.Checked = internetKey.ProxyEnable;
            chkProxyEnable.CheckState = internetKey.ProxyEnable ? CheckState.Checked : CheckState.Unchecked;
            UpdateTrayBar(true);
        }

        void UpdateTrayBar(bool isShowTrayBar)
        {
            // string[] array = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string iconSourceName = internetKey.ProxyEnable ? "Proxy.Resources.IconEnabed.ico" : "Proxy.Resources.IconDisEnabed.ico";
            notifyIcon1.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(iconSourceName));
            notifyIcon1.Text = internetKey.ProxyEnable ? "Proxy Available" : "Proxy UnAvailable";
            // notifyIcon1.Visible = isShowTrayBar;
            this.Visible = !isShowTrayBar;
        }

        private void chkProxyEnable_CheckedChanged(object sender, EventArgs e)
        {
            txtAddress.Enabled = chkProxyEnable.Checked;
            txtPort.Enabled = chkProxyEnable.Checked;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;
            e.Cancel = true;
            UpdateTrayBar(true);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            UpdateTrayBar(false);
        }
    }

    public class InternetKey
    {
        [DllImport("wininet.dll")]
        static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        const int INTERNET_OPTION_REFRESH = 37;
        RegistryKey registryKey;

        public bool ProxyEnable;

        public string ProxyServer;

        public InternetKey()
        {
            registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            ProxyEnable = registryKey.GetValue("ProxyEnable").ToString() == "1" ? true : false;
            ProxyServer = registryKey.GetValue("ProxyServer").ToString();
        }

        public void SetValue(bool proxyEnable, string proxyServer)
        {
            ProxyEnable = proxyEnable;
            ProxyServer = proxyServer;
            registryKey.SetValue("ProxyEnable", proxyEnable ? "1" : "0", RegistryValueKind.DWord);
            registryKey.SetValue("ProxyServer", proxyServer, RegistryValueKind.String);
            var settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            var refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
    }

    interface IQuickSwichKey
    {
        bool GetValue();
        void SetValue(bool enabled);
    }

    public class QuickSwichKey : IQuickSwichKey
    {
        RegistryKey _proxyKey;
        private const string _proxyKeyPath = @"Software\PYSSoft\Proxy";
        private const string _key = "EnableQuickSwitch";

        public bool Enabled
        {
            get
            {
                return GetValue();
            }
        }

        public QuickSwichKey()
        {
            _proxyKey = Registry.CurrentUser.OpenSubKey(_proxyKeyPath, true);
        }

        private void CreateKey()
        {
            _proxyKey = Registry.CurrentUser.CreateSubKey(_proxyKeyPath);
            _proxyKey.SetValue(_key, "1", RegistryValueKind.DWord);
        }

        public void SetValue(bool enabled)
        {
            _proxyKey.SetValue(_key, enabled ? "1" : "0", RegistryValueKind.DWord);
        }

        public bool GetValue()
        {
            if (_proxyKey == null) CreateKey();
            RegistryKey proxyKey = Registry.CurrentUser.OpenSubKey(_proxyKeyPath, true);
            bool value = proxyKey.GetValue(_key).ToString() == "1" ? true : false;
            return value;
        }
    }
}
////////////////////////////////////////////////////////////////////////////////////////////////
/*
if (Member == null || EmptySite == null)
{
    throw new ArgumentException();
}
if (EmptySite.Id != 0)
{
    throw new SiteExistsException(string.Format("WebsitePartnerSiteCreator was passed a non-empty site with site id: {0}", EmptySite.Id));
}




*/
////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////