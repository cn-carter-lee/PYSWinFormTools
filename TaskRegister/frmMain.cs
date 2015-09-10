using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace TaskRegister
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadRegisteredFiles();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            frmHelp f = new frmHelp();
            f.ShowDialog();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (lvwPendingFiles.Items.Count == 0)
            {
                MessageBox.Show("No Pending Files Need to register.", "Register Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                foreach (ListViewItem item in lvwPendingFiles.Items)
                {
                    RegisterTask(item.Tag as FileInfo);
                    MessageBox.Show("Successfully registered.", "Register Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadRegisteredFiles();
            }
            catch
            {
                MessageBox.Show("Failed to register, please make sure you run this tool as administrator!", "Register Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            ClearPendingFiles();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearPendingFiles();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (lvwRegisteredFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item.", "UnInstall Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                FileInfo fileInfo = lvwRegisteredFiles.SelectedItems[0].Tag as FileInfo;
                UnRegisterTask(fileInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occured, details is - {0} ", ex.Message), "UnInstall Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadRegisteredFiles();
        }

        /// <summary>
        /// Add dll files to pending ListView
        /// </summary>
        void AddPendingFiles(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "DLL Files (.dll)|*.dll"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    ListViewItem item = new ListViewItem();
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Extension.ToLower() == ".dll")
                    {
                        item.Text = fileInfo.Name;
                        item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
                        item.Tag = fileInfo;
                        lvwPendingFiles.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Remove all pending dll files
        /// </summary>
        void ClearPendingFiles()
        {
            lvwPendingFiles.Items.Clear();
        }

        /// <summary>
        /// Register a dll file
        /// </summary>
        /// <param name="fileInfo"></param>
        bool RegisterTask(FileInfo fileInfo)
        {
            bool operationResult = false;
            // Copy the dll file to working folder
            string targetFilePath = string.Format(@"C:\Program Files (x86)\Microsoft SQL Server\110\DTS\Tasks\{0}", fileInfo.Name);
            FileInfo targetFileInfo = fileInfo.CopyTo(targetFilePath, true);

            // Start a process to register dll file
            Process m_gacutil = new Process();
            m_gacutil.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\x64\gacutil.exe ";
            m_gacutil.StartInfo.RedirectStandardOutput = true;
            m_gacutil.StartInfo.UseShellExecute = false;
            m_gacutil.StartInfo.Arguments = String.Format("/i \"{0}\"", targetFileInfo.FullName);
            m_gacutil.Start();
            if (m_gacutil.StandardOutput.ReadToEnd().Contains("successfully "))
                operationResult = true;
            return operationResult;
        }

        void LoadRegisteredFiles()
        {
            lvwRegisteredFiles.Items.Clear();
            string taskDirectory = @"C:\Program Files (x86)\Microsoft SQL Server\110\DTS\Tasks";
            DirectoryInfo directoryInfo = new DirectoryInfo(taskDirectory);
            if (!directoryInfo.Exists) return;

            foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dll"))
            {
                ListViewItem item = new ListViewItem();
                item.Text = fileInfo.Name;
                item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
                item.Tag = fileInfo;
                lvwRegisteredFiles.Items.Add(item);
            }
        }

        bool UnRegisterTask(FileInfo fileInfo)
        {
            bool operationResult = false;
            Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
            if (assembly.GlobalAssemblyCache)
            {
                Process m_gacutil = new Process();
                m_gacutil.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\x64\gacutil.exe ";
                m_gacutil.StartInfo.RedirectStandardOutput = true;
                m_gacutil.StartInfo.UseShellExecute = false;
                m_gacutil.StartInfo.Arguments = String.Format("/u \"{0}\"", assembly.GetName().Name);
                bool a = m_gacutil.Start();
                var outputResult = m_gacutil.StandardOutput.ReadToEnd();
                if (outputResult.Contains("uninstalled = 1"))
                {
                    fileInfo.Delete();
                    operationResult = true;
                    LoadRegisteredFiles();
                    MessageBox.Show(string.Format("{0}Successfully Unregistered.", assembly.GetName().Name), "UnInstall Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(string.Format("{0} was not registered to global assembly cache.", fileInfo.Name), "UnInstall Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return operationResult;
        }

        // Drap-drop function cannot work because this program must be run as administrator.
        //private void lvwPendingFiles_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //        e.Effect = DragDropEffects.Copy;
        //}

        //private void lvwPendingFiles_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        //    foreach (string file in files)
        //    {
        //        ListViewItem item = new ListViewItem();
        //        FileInfo fileInfo = new FileInfo(file);
        //        if (fileInfo.Extension.ToLower() == ".dll")
        //        {
        //            item.Text = fileInfo.Name;
        //            item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
        //            item.Tag = fileInfo;
        //            lvwPendingFiles.Items.Add(item);
        //        }
        //    }
        //}
    }

    public class PYSTaskManager
    {
        public static bool UnRegisterTask(FileInfo fileInfo)
        {
            bool operationResult = false;
            Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
            if (assembly.GlobalAssemblyCache)
            {
                Process m_gacutil = new Process();
                m_gacutil.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\x64\gacutil.exe ";
                m_gacutil.StartInfo.RedirectStandardOutput = true;
                m_gacutil.StartInfo.UseShellExecute = false;
                m_gacutil.StartInfo.Arguments = String.Format("/u \"{0}\"", assembly.GetName().Name);
                bool a = m_gacutil.Start();
                var outputResult = m_gacutil.StandardOutput.ReadToEnd();
                if (outputResult.Contains("uninstalled = 1"))
                {
                    fileInfo.Delete();
                    operationResult = true;
                    // LoadRegisteredFiles();
                    MessageBox.Show(string.Format("{0}Successfully Unregistered.", assembly.GetName().Name), "UnInstall Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(string.Format("{0} was not registered to global assembly cache.", fileInfo.Name), "UnInstall Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return operationResult;
        }
    }
}
