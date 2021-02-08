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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 owner = (Form1)this.Owner;
            this.textBox1.Text = owner.m_config.DefaultPassword;
            this.textBox2.Text = owner.m_config.DefaultPassword;
        }

        // OK button
        private void button1_Click(object sender, EventArgs e)
        {
            string masterPwd = this.textBox1.Text.ToString();
            string repeatPwd = this.textBox2.Text.ToString();
            if (masterPwd == repeatPwd)
            {
                Form1 owner = (Form1)this.Owner;
                owner.m_config.DefaultPassword = masterPwd;

                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
