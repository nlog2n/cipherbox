using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CipherBox
{
    public partial class Form4 : Form
    {
        public bool Done = false;

        public Form4()
        {
            InitializeComponent();
        }

        // Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        // OK
        private void button3_Click(object sender, EventArgs e)
        {
            Done = true;
            Close();
        }

        // Choose an image file
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Show the folder browser.
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "JPG Files|*.jpg|GIF Files|*.gif|PNG Files|*.png|All Files|*.*";
                openFileDialog1.Title = "Select an Image File";
                
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog1.FileName;

                    this.textBox2.Text = filename;
                    this.radioButton2.Checked = true;
                    this.radioButton1.Checked = false;
                }
            }
            catch (Exception)
            { }
        }
    }
}
