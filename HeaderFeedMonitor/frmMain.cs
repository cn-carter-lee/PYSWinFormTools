using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Runtime.InteropServices;

namespace HeaderFeedMonitor
{
    public partial class frmMain : Form
    {
        private const string connectionString = @"Data Source=PRDMSSQLCLSDB01\DW;Initial Catalog=RegistryOps;Integrated Security=SSPI;";

        ICollection<Retailer> listRetailer = new List<Retailer>();
        ICollection<BatchInfo> listBatchInfo = new List<BatchInfo>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadBasics();
        }

        DataTable GetDataTable(string commandString)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlDataAdapter adp = new SqlDataAdapter(new SqlCommand(commandString, connection));
                adp.Fill(table);
            }
            catch { }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return table;
        }

        DataRowCollection GetRows(string commandString)
        {
            return GetDataTable(commandString).Rows;
        }

        private void tsBtnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            string commandString = string.Format("SELECT {0} i.RetailerName,h.RetailerId,h.DataFileId,h.Filename,h.LastErrorMessage,h.StartTime,h.ImportStartTime,h.CompareStartTime,h.SyncStartTime,h.EndTime,h.SuccessCount,h.FailureCount,h.InsertCount,h.UpdateCount,h.DeleteCount  FROM header.BatchHistory h  inner join header.RetailerInformation i on h.RetailerId =i.RetailerId where 1 = 1 ", tscmbPageSize.SelectedItem == null ? "" : string.Format(" top {0} ", tscmbPageSize.SelectedItem.ToString()));

            if (tscmbBatchList.SelectedItem != null || tscmbRetailers.SelectedItem != null)
            {
                if (tscmbBatchList.SelectedItem != null)
                {
                    commandString += string.Format(" and h.batchId = {0}", listBatchInfo.ElementAt(tscmbBatchList.SelectedIndex).BatchId);
                }

                if (tscmbRetailers.SelectedItem != null)
                {
                    commandString += string.Format(" and h.RetailerId = {0}", listRetailer.ElementAt(tscmbRetailers.SelectedIndex).RetailerId);
                }
            }
            else
            {
                commandString += " and h.batchId=(select max(BatchNo) from header.BatchSequence) ";
            }

            commandString += " order by h.DataFileId desc ";

            DataTable table = GetDataTable(commandString);
            lvwBatch.Items.Clear();
            lvwBatch.Columns.Clear();
            foreach (DataColumn column in table.Columns)
            {
                lvwBatch.Columns.Add(column.ColumnName, 150);
            }
            foreach (DataRow row in table.Rows)
            {
                ListViewItem item = new ListViewItem();
                foreach (DataColumn column in table.Columns)
                {
                    string text = row[column.ColumnName].ToString();
                    if (table.Columns.IndexOf(column) == 0)
                        item.Text = text;
                    else
                        item.SubItems.Add(text);
                }
                lvwBatch.Items.Add(item);
            }
        }

        void LoadBasics()
        {
            LoadRetailers();
            LoadBatch();
        }

        void LoadRetailers()
        {
            tscmbRetailers.Items.Clear();
            string commandString = "SELECT RetailerId,RetailerName FROM Header.RetailerInformation  order by ExecutionOrder";
            DataRowCollection rows = GetRows(commandString);
            foreach (DataRow row in rows)
            {
                Retailer retailer = new Retailer()
                {
                    RetailerId = Convert.ToInt32(row["RetailerId"]),
                    RetailerName = row["RetailerName"].ToString()
                };
                listRetailer.Add(retailer);
                tscmbRetailers.Items.Add(retailer.RetailerName);
            }
        }

        void LoadBatch()
        {
            tscmbBatchList.Items.Clear();
            string commandString = "select BatchNo,RunTime from header.BatchSequence order by 2 desc";
            DataRowCollection rows = GetRows(commandString);
            foreach (DataRow row in rows)
            {
                BatchInfo batchInfo = new BatchInfo()
                {
                    BatchId = Convert.ToInt32(row["BatchNo"]),
                    RunTime = Convert.ToDateTime(row["RunTime"])
                };
                listBatchInfo.Add(batchInfo);
                tscmbBatchList.Items.Add(batchInfo.RunTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private void tsBtnEnableRefresh_Click(object sender, EventArgs e)
        {
            tsBtnEnableRefresh.Enabled = false;
            timer1.Stop();
            return;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            tsBtnEnableRefresh.Image = ((System.Drawing.Image)(resources.GetObject("delete.ico")));

            //((Bitmap)tsBtnEnableRefresh.Image).DrawText("C", Font, SystemBrushes.ControlText,
            //   new RectangleF(new PointF(0, 0), tsBtnEnableRefresh.Image.Size));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NotifyIcon ni = new NotifyIcon();
            ni.Visible = true;
        }
    }

    class Retailer
    {
        public int RetailerId { get; set; }
        public string RetailerName { get; set; }
    }

    class BatchInfo
    {
        public int BatchId { get; set; }
        public DateTime RunTime { get; set; }
    }


}

/*
internal class NativeMethods
{
    // All definitions taken from http://pinvoke.net 

    [DllImport("shell32.dll")]
    public static extern IntPtr SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);


    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(
    string lpClassName,
    string lpWindowName
    );
    public const string TaskbarClass = "Shell_TrayWnd";

    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        public static APPBARDATA Create()
        {
            APPBARDATA appBarData = new APPBARDATA();
            appBarData.cbSize = Marshal.SizeOf(typeof(NativeMethods.APPBARDATA));
            return appBarData;
        }
        public int cbSize;
        public IntPtr hWnd;
        public uint uCallbackMessage;
        public uint uEdge;
        public RECT rc;
        public int lParam;
    }
    public const int ABM_QUERYPOS = 0x00000002,
    ABM_GETTASKBARPOS = 5;
    public const int ABE_LEFT = 0;
    public const int ABE_TOP = 1;
    public const int ABE_RIGHT = 2;
    public const int ABE_BOTTOM = 3;


    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left_, int top_, int right_, int bottom_)
        {
            Left = left_;
            Top = top_;
            Right = right_;
            Bottom = bottom_;
        }

        public int Height { get { return Bottom - Top + 1; } }
        public int Width { get { return Right - Left + 1; } }
        public Size Size { get { return new Size(Width, Height); } }

        public Point Location { get { return new Point(Left, Top); } }

        // Handy method for converting to a System.Drawing.Rectangle 
        public Rectangle ToRectangle()
        {
            return Rectangle.FromLTRB(Left, Top, Right, Bottom);
        }

        public static RECT FromRectangle(Rectangle rectangle)
        {
            return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public override int GetHashCode()
        {
            return Left ^ ((Top << 13) | (Top >> 0x13))
            ^ ((Width << 0x1a) | (Width >> 6))
            ^ ((Height << 7) | (Height >> 0x19));
        }

        #region Operator overloads

        public static implicit operator Rectangle(RECT rect)
        {
            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static implicit operator RECT(Rectangle rect)
        {
            return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        #endregion
    }
}
*/