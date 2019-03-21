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
            this.search_bar = new System.Windows.Forms.TextBox();
            this.windows_olv = new BrightIdeasSoftware.ObjectListView();
            this.colmun1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.context_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quit = new System.Windows.Forms.ToolStripMenuItem();
            this.conf = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.windows_olv)).BeginInit();
            this.context_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // search_bar
            // 
            this.search_bar.Location = new System.Drawing.Point(12, 12);
            this.search_bar.Name = "search_bar";
            this.search_bar.Size = new System.Drawing.Size(776, 20);
            this.search_bar.TabIndex = 0;
            this.search_bar.TextChanged += new System.EventHandler(this.search_bar_TextChanged);
            this.search_bar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_bar_KeyDown);
            this.search_bar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.search_bar_KeyUp);
            // 
            // windows_olv
            // 
            this.windows_olv.AllColumns.Add(this.colmun1);
            this.windows_olv.AllColumns.Add(this.column2);
            this.windows_olv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.windows_olv.CellEditUseWholeCell = false;
            this.windows_olv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colmun1,
            this.column2});
            this.windows_olv.Cursor = System.Windows.Forms.Cursors.Default;
            this.windows_olv.HideSelection = false;
            this.windows_olv.Location = new System.Drawing.Point(12, 38);
            this.windows_olv.Name = "windows_olv";
            this.windows_olv.ShowGroups = false;
            this.windows_olv.Size = new System.Drawing.Size(776, 476);
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
            this.colmun1.AspectName = "Image";
            this.colmun1.ImageAspectName = "ProcessPath";
            this.colmun1.Text = "image";
            this.colmun1.Width = 118;
            // 
            // column2
            // 
            this.column2.AspectName = "Title";
            this.column2.FillsFreeSpace = true;
            this.column2.Text = "title";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 526);
            this.Controls.Add(this.windows_olv);
            this.Controls.Add(this.search_bar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Everywin";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.windows_olv)).EndInit();
            this.context_menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox search_bar;
        private BrightIdeasSoftware.ObjectListView windows_olv;
        private BrightIdeasSoftware.OLVColumn colmun1;
        private BrightIdeasSoftware.OLVColumn column2;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip context_menu;
        private System.Windows.Forms.ToolStripMenuItem quit;
        private System.Windows.Forms.ToolStripMenuItem conf;
        //private System.Diagnostics.PerformanceCounter performanceCounter1;
    }
}

