namespace Everywin
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.windows_olv = new BrightIdeasSoftware.ObjectListView();
            this.colmun1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ramUsage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.context_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quit = new System.Windows.Forms.ToolStripMenuItem();
            this.conf = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.configureF1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.search_bar = new Everywin.ToolStripSpringTextBox();
            this.toggleGroupProcesses = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.windows_olv)).BeginInit();
            this.context_menu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windows_olv
            // 
            this.windows_olv.AllColumns.Add(this.colmun1);
            this.windows_olv.AllColumns.Add(this.ramUsage);
            this.windows_olv.AllColumns.Add(this.column2);
            this.windows_olv.CellEditUseWholeCell = false;
            this.windows_olv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colmun1,
            this.column2});
            this.tableLayoutPanel1.SetColumnSpan(this.windows_olv, 2);
            this.windows_olv.Cursor = System.Windows.Forms.Cursors.Default;
            this.windows_olv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windows_olv.EmptyListMsg = "No matching processes were found";
            this.windows_olv.FullRowSelect = true;
            this.windows_olv.HideSelection = false;
            this.windows_olv.Location = new System.Drawing.Point(3, 28);
            this.windows_olv.Name = "windows_olv";
            this.windows_olv.ShowGroups = false;
            this.windows_olv.ShowItemCountOnGroups = true;
            this.windows_olv.Size = new System.Drawing.Size(794, 495);
            this.windows_olv.TabIndex = 2;
            this.windows_olv.UseCompatibleStateImageBehavior = false;
            this.windows_olv.UseFiltering = true;
            this.windows_olv.View = System.Windows.Forms.View.Details;
            this.windows_olv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.windows_olv_KeyDown);
            this.windows_olv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.windows_olv_KeyPress);
            this.windows_olv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.windows_olv_MouseDoubleClick);
            this.windows_olv.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.windows_olv_PreviewKeyDown);
            // 
            // colmun1
            // 
            this.colmun1.AspectName = "Process";
            this.colmun1.ImageAspectName = "ProcessPath";
            this.colmun1.Text = "Process";
            this.colmun1.Width = 118;
            // 
            // ramUsage
            // 
            this.ramUsage.AspectName = "RamUsage";
            this.ramUsage.DisplayIndex = 1;
            this.ramUsage.IsVisible = false;
            this.ramUsage.Text = "RAM Usage";
            this.ramUsage.Width = 150;
            // 
            // column2
            // 
            this.column2.AspectName = "Title";
            this.column2.FillsFreeSpace = true;
            this.column2.Text = "Title";
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.context_menu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Everywin";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // context_menu
            // 
            this.context_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quit,
            this.conf});
            this.context_menu.Name = "context_menu";
            this.context_menu.Size = new System.Drawing.Size(151, 48);
            this.context_menu.Text = "Quit";
            this.context_menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // quit
            // 
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(150, 22);
            this.quit.Text = "Quit";
            // 
            // conf
            // 
            this.conf.Name = "conf";
            this.conf.Size = new System.Drawing.Size(150, 22);
            this.conf.Text = "Configure (F1)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.windows_olv, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 526);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.search_bar,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toggleGroupProcesses});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.AutoToolTip = false;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureF1ToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(62, 22);
            this.toolStripDropDownButton1.Text = "Options";
            // 
            // configureF1ToolStripMenuItem
            // 
            this.configureF1ToolStripMenuItem.Name = "configureF1ToolStripMenuItem";
            this.configureF1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configureF1ToolStripMenuItem.Text = "Configure (F1)";
            this.configureF1ToolStripMenuItem.Click += new System.EventHandler(this.configureF1ToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // search_bar
            // 
            this.search_bar.Name = "search_bar";
            this.search_bar.Size = new System.Drawing.Size(594, 25);
            this.search_bar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_bar_KeyDown);
            this.search_bar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.search_bar_KeyUp);
            this.search_bar.TextChanged += new System.EventHandler(this.search_bar_TextChanged);
            // 
            // toggleGroupProcesses
            // 
            this.toggleGroupProcesses.CheckOnClick = true;
            this.toggleGroupProcesses.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toggleGroupProcesses.Image = ((System.Drawing.Image)(resources.GetObject("toggleGroupProcesses.Image")));
            this.toggleGroupProcesses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleGroupProcesses.Name = "toggleGroupProcesses";
            this.toggleGroupProcesses.Size = new System.Drawing.Size(98, 22);
            this.toggleGroupProcesses.Text = "Group Processes";
            this.toggleGroupProcesses.ToolTipText = "Toggle grouping by process name";
            this.toggleGroupProcesses.CheckedChanged += new System.EventHandler(this.toggleGroupProcesses_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 526);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Everywin";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.windows_olv)).EndInit();
            this.context_menu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private BrightIdeasSoftware.ObjectListView windows_olv;
        private BrightIdeasSoftware.OLVColumn colmun1;
        private BrightIdeasSoftware.OLVColumn column2;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip context_menu;
        private System.Windows.Forms.ToolStripMenuItem quit;
        private System.Windows.Forms.ToolStripMenuItem conf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BrightIdeasSoftware.OLVColumn ramUsage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem configureF1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripSpringTextBox search_bar;
        private System.Windows.Forms.ToolStripButton toggleGroupProcesses;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        //private System.Diagnostics.PerformanceCounter performanceCounter1;
    }
}

