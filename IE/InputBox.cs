using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xyz.blockers.lowBrowser
{
    public partial class InputBox : Form
    {
        public delegate void CallBack(string text); 
        CallBack callBack;
        public InputBox(CallBack callBack)
        {
            InitializeComponent();
            this.callBack = callBack;
        }
        public string BoxText { set { textBox1.Text = value; } }
        public string TipText { set { label1.Text = value; } }
        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            callBack(textBox1.Text);
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                button1_Click(sender, e);
            }
        }
    }
}
