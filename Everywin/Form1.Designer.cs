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
            this.search_bar = new System.Windows.Forms.TextBox();
            this.WindowsListbox = new System.Windows.Forms.ListBox();
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
            // 
            // WindowsListbox
            // 
            this.WindowsListbox.ColumnWidth = 5;
            this.WindowsListbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.WindowsListbox.FormattingEnabled = true;
            this.WindowsListbox.ItemHeight = 20;
            this.WindowsListbox.Location = new System.Drawing.Point(12, 38);
            this.WindowsListbox.Name = "WindowsListbox";
            this.WindowsListbox.Size = new System.Drawing.Size(776, 384);
            this.WindowsListbox.TabIndex = 1;
            this.WindowsListbox.SelectedIndexChanged += new System.EventHandler(this.WindowsListbox_SelectedIndexChanged);
            this.WindowsListbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WindowsListbox_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 429);
            this.Controls.Add(this.WindowsListbox);
            this.Controls.Add(this.search_bar);
            this.Name = "Form1";
            this.Text = "Everywin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox search_bar;
        private System.Windows.Forms.ListBox WindowsListbox;
    }
}

