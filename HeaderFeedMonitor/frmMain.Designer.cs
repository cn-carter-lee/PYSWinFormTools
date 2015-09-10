namespace HeaderFeedMonitor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscmbBatchList = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscmbRetailers = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tscmbPageSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnLoad = new System.Windows.Forms.ToolStripButton();
            this.tsBtnEnableRefresh = new System.Windows.Forms.ToolStripButton();
            this.lvwBatch = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscmbBatchList,
            this.toolStripLabel2,
            this.tscmbRetailers,
            this.toolStripLabel3,
            this.tscmbPageSize,
            this.toolStripSeparator1,
            this.tsBtnLoad,
            this.tsBtnEnableRefresh,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(802, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 22);
            this.toolStripLabel1.Text = "Loaded Time :";
            // 
            // tscmbBatchList
            // 
            this.tscmbBatchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscmbBatchList.Name = "tscmbBatchList";
            this.tscmbBatchList.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel2.Text = "Retailer :";
            // 
            // tscmbRetailers
            // 
            this.tscmbRetailers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscmbRetailers.Name = "tscmbRetailers";
            this.tscmbRetailers.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel3.Text = "PageSize:";
            // 
            // tscmbPageSize
            // 
            this.tscmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscmbPageSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tscmbPageSize.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.tscmbPageSize.Name = "tscmbPageSize";
            this.tscmbPageSize.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnLoad
            // 
            this.tsBtnLoad.Image = global::HeaderFeedMonitor.Properties.Resources.reload;
            this.tsBtnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLoad.Name = "tsBtnLoad";
            this.tsBtnLoad.Size = new System.Drawing.Size(65, 22);
            this.tsBtnLoad.Text = "Refresh";
            this.tsBtnLoad.Click += new System.EventHandler(this.tsBtnLoad_Click);
            // 
            // tsBtnEnableRefresh
            // 
            this.tsBtnEnableRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnEnableRefresh.Image = global::HeaderFeedMonitor.Properties.Resources.edit_clear;
            this.tsBtnEnableRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnEnableRefresh.Name = "tsBtnEnableRefresh";
            this.tsBtnEnableRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsBtnEnableRefresh.Text = "Timer";
            this.tsBtnEnableRefresh.Click += new System.EventHandler(this.tsBtnEnableRefresh_Click);
            // 
            // lvwBatch
            // 
            this.lvwBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwBatch.FullRowSelect = true;
            this.lvwBatch.Location = new System.Drawing.Point(0, 25);
            this.lvwBatch.Name = "lvwBatch";
            this.lvwBatch.Size = new System.Drawing.Size(802, 410);
            this.lvwBatch.TabIndex = 1;
            this.lvwBatch.UseCompatibleStateImageBehavior = false;
            this.lvwBatch.View = System.Windows.Forms.View.Details;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 435);
            this.Controls.Add(this.lvwBatch);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Header Feed Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView lvwBatch;
        private System.Windows.Forms.ToolStripButton tsBtnLoad;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripComboBox tscmbBatchList;
        private System.Windows.Forms.ToolStripComboBox tscmbRetailers;
        private System.Windows.Forms.ToolStripComboBox tscmbPageSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsBtnEnableRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

