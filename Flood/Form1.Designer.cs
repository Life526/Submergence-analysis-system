namespace Flood
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.spatialToolStripPanel1 = new DotSpatial.Controls.SpatialToolStripPanel();
            this.map1 = new DotSpatial.Controls.Map();
            this.legend1 = new DotSpatial.Controls.Legend();
            this.spatialToolStrip1 = new DotSpatial.Controls.SpatialToolStrip();
            this.spatialDockManager1 = new DotSpatial.Controls.SpatialDockManager();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.spatialStatusStrip1 = new DotSpatial.Controls.SpatialStatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.appManager1 = new DotSpatial.Controls.AppManager();
            this.spatialHeaderControl1 = new DotSpatial.Controls.SpatialHeaderControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spatialDockManager1)).BeginInit();
            this.spatialDockManager1.Panel1.SuspendLayout();
            this.spatialDockManager1.Panel2.SuspendLayout();
            this.spatialDockManager1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.spatialStatusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spatialHeaderControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = "C:\\Users\\dell-01\\Desktop\\ShiXi\\Procedure\\Flood\\Flood\\bin\\Debug\\Skin\\MSN.ssk";
            this.skinEngine1.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinEngine1.SkinStreamMain")));
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(44, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.spatialToolStripPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 61);
            this.panel1.TabIndex = 1;
            // 
            // spatialToolStripPanel1
            // 
            this.spatialToolStripPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spatialToolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.spatialToolStripPanel1.Name = "spatialToolStripPanel1";
            this.spatialToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.spatialToolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.spatialToolStripPanel1.Size = new System.Drawing.Size(873, 61);
            // 
            // map1
            // 
            this.map1.AllowDrop = true;
            this.map1.BackColor = System.Drawing.Color.White;
            this.map1.CollisionDetection = false;
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.ExtendBuffer = false;
            this.map1.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.map1.IsBusy = false;
            this.map1.IsZoomedToMaxExtent = false;
            this.map1.Legend = this.legend1;
            this.map1.Location = new System.Drawing.Point(3, 3);
            this.map1.Name = "map1";
            this.map1.ProgressHandler = null;
            this.map1.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.RedrawLayersWhileResizing = false;
            this.map1.SelectionEnabled = true;
            this.map1.Size = new System.Drawing.Size(564, 431);
            this.map1.TabIndex = 0;
            this.map1.ZoomOutFartherThanMaxExtent = false;
            // 
            // legend1
            // 
            this.legend1.BackColor = System.Drawing.Color.White;
            this.legend1.ControlRectangle = new System.Drawing.Rectangle(0, 0, 277, 431);
            this.legend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.legend1.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.legend1.HorizontalScrollEnabled = true;
            this.legend1.Indentation = 30;
            this.legend1.IsInitialized = false;
            this.legend1.Location = new System.Drawing.Point(3, 3);
            this.legend1.MinimumSize = new System.Drawing.Size(5, 5);
            this.legend1.Name = "legend1";
            this.legend1.ProgressHandler = null;
            this.legend1.ResetOnResize = false;
            this.legend1.SelectionFontColor = System.Drawing.Color.Black;
            this.legend1.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.legend1.Size = new System.Drawing.Size(277, 431);
            this.legend1.TabIndex = 0;
            this.legend1.Text = "legend1";
            this.legend1.UseLegendForSelection = true;
            this.legend1.VerticalScrollEnabled = true;
            // 
            // spatialToolStrip1
            // 
            this.spatialToolStrip1.ApplicationManager = null;
            this.spatialToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.spatialToolStrip1.Location = new System.Drawing.Point(438, 0);
            this.spatialToolStrip1.Map = this.map1;
            this.spatialToolStrip1.Name = "spatialToolStrip1";
            this.spatialToolStrip1.Size = new System.Drawing.Size(404, 25);
            this.spatialToolStrip1.TabIndex = 6;
            // 
            // spatialDockManager1
            // 
            this.spatialDockManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spatialDockManager1.Location = new System.Drawing.Point(0, 61);
            this.spatialDockManager1.Name = "spatialDockManager1";
            // 
            // spatialDockManager1.Panel1
            // 
            this.spatialDockManager1.Panel1.Controls.Add(this.tabControl1);
            // 
            // spatialDockManager1.Panel2
            // 
            this.spatialDockManager1.Panel2.Controls.Add(this.tabControl2);
            this.spatialDockManager1.Size = new System.Drawing.Size(873, 463);
            this.spatialDockManager1.SplitterDistance = 291;
            this.spatialDockManager1.TabControl1 = this.tabControl1;
            this.spatialDockManager1.TabControl2 = this.tabControl2;
            this.spatialDockManager1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 463);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.legend1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(283, 437);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Legend";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(578, 463);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.map1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(570, 437);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Map";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // spatialStatusStrip1
            // 
            this.spatialStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.spatialStatusStrip1.Location = new System.Drawing.Point(0, 524);
            this.spatialStatusStrip1.Name = "spatialStatusStrip1";
            this.spatialStatusStrip1.ProgressBar = this.toolStripProgressBar1;
            this.spatialStatusStrip1.ProgressLabel = this.toolStripStatusLabel1;
            this.spatialStatusStrip1.Size = new System.Drawing.Size(873, 22);
            this.spatialStatusStrip1.TabIndex = 1;
            this.spatialStatusStrip1.Text = "spatialStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // appManager1
            // 
            this.appManager1.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager1.Directories")));
            this.appManager1.DockManager = this.spatialDockManager1;
            this.appManager1.HeaderControl = this.spatialHeaderControl1;
            this.appManager1.Legend = this.legend1;
            this.appManager1.Map = this.map1;
            this.appManager1.ProgressHandler = this.spatialStatusStrip1;
            // 
            // spatialHeaderControl1
            // 
            this.spatialHeaderControl1.ApplicationManager = this.appManager1;
            this.spatialHeaderControl1.MenuStrip = this.menuStrip1;
            this.spatialHeaderControl1.ToolbarsContainer = this.spatialToolStripPanel1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 546);
            this.Controls.Add(this.spatialDockManager1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.spatialStatusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.spatialDockManager1.Panel1.ResumeLayout(false);
            this.spatialDockManager1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spatialDockManager1)).EndInit();
            this.spatialDockManager1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.spatialStatusStrip1.ResumeLayout(false);
            this.spatialStatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spatialHeaderControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private DotSpatial.Controls.SpatialToolStripPanel spatialToolStripPanel1;
        private DotSpatial.Controls.Map map1;
        private DotSpatial.Controls.SpatialDockManager spatialDockManager1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DotSpatial.Controls.Legend legend1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private DotSpatial.Controls.SpatialStatusStrip spatialStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private DotSpatial.Controls.AppManager appManager1;
        private DotSpatial.Controls.SpatialHeaderControl spatialHeaderControl1;
        private DotSpatial.Controls.SpatialToolStrip spatialToolStrip1;
    }
}

