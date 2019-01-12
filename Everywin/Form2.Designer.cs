namespace Everywin
{
    partial class Form2
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
            this.shortcut_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shortcut_textbox
            // 
            this.shortcut_textbox.Enabled = false;
            this.shortcut_textbox.Location = new System.Drawing.Point(68, 12);
            this.shortcut_textbox.Name = "shortcut_textbox";
            this.shortcut_textbox.Size = new System.Drawing.Size(100, 20);
            this.shortcut_textbox.TabIndex = 0;
            this.shortcut_textbox.UseWaitCursor = false;
            this.shortcut_textbox.TextChanged += new System.EventHandler(this.shortcut_textbox_TextChanged);
            this.shortcut_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.shortcut_textbox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Shortcut:";
            this.label1.UseWaitCursor = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 19);
            this.button1.TabIndex = 2;
            this.button1.Text = "Record";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.UseWaitCursor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 37);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shortcut_textbox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form2";
            this.Text = "Configure";
            this.UseWaitCursor = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shortcut_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}