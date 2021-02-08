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
    public partial class Form3 : Form
    {
        public bool Done = false;

        public Form3()
        {
            InitializeComponent();
        }

        // Cancel
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        // OK
        private void button2_Click(object sender, EventArgs e)
        {
            Done = true;
            Close();
        }
    }
}
