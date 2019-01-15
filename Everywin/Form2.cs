using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Everywin
{
    public partial class Form2 : Form
    {
        private bool recording = false;
        //private uint recorded_modifiers = 0;
        //private Keys recorded_key = Keys.None;
        private Shortcut recorded_shortcut = new Shortcut();
        private Form1 main_form;

        public Form2(Form1 main_form)
        {
            InitializeComponent();
            this.main_form = main_form;

            shortcut_textbox.Text = main_form.GetCurrentShortcut().ToString();

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (recording)
            {
                // should set shortcut now
                recording = false;
                button1.Text = "Record";

                bool shortcut_valid = false;

                // user entered 
                if (recorded_shortcut.is_valid() && shortcut_textbox.Text != "")
                {
                    shortcut_valid = main_form.SetNewShortcut(recorded_shortcut);
                }
                
                if (!shortcut_valid)
                {
                    // invalid shortcut
                    recorded_shortcut = new Shortcut();
                    main_form.RemoveShortcut();

                    // update text
                    shortcut_textbox.Text = main_form.GetCurrentShortcut().ToString();
                }

                shortcut_textbox.Enabled = false;
            }
            else
            {
                // start recording
                recording = true;

                // change button to "Set"
                button1.Text = "Set";

                // Ready textbox for user input
                shortcut_textbox.Enabled = true;
                shortcut_textbox.Text = "";
                shortcut_textbox.Focus();

                // Disable current shortcut
                main_form.RemoveShortcut();
            }           

        }

        private void shortcut_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void shortcut_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                return;
            }
            else
            if (e.KeyCode == Keys.Back)
            {
                shortcut_textbox.Text = "";
                recorded_shortcut = new Shortcut();
                return;
            }
            
            
            uint modifiers = 0;

            modifiers |= (uint)(e.Control ? Everywin.ModifierKeys.Control : 0);
            modifiers |= (uint)(e.Alt ? Everywin.ModifierKeys.Alt : 0);
            modifiers |= (uint)(e.Shift ? Everywin.ModifierKeys.Shift : 0);

            //modifiers |= (uint)(e. Everywin.ModifierKeys.Control : 0);

            //main_form.SetNewShortcut((Everywin.ModifierKeys)modifiers, e.KeyData);

            //recorded_key = e.KeyCode;
            //recorded_modifiers = modifiers;

            recorded_shortcut = new Shortcut(modifiers, e.KeyCode);


            shortcut_textbox.Text = recorded_shortcut.ToString();
        }


    }

}
