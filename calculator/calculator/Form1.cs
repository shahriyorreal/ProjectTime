using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        Double resultValue = 0;
        String oop = "";
        bool iso = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0") || (iso))
                textBox1.Clear();

            Button button = (Button)sender;
            textBox1.Text = textBox1.Text + button.Text;
            iso = false;
        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            oop = button.Text;
            resultValue = Double.Parse(textBox1.Text);
            textBox1.Text = resultValue +" " + oop;
            iso = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            switch (oop)
            {
                case "+":
                    textBox1.Text = (resultValue + Double.Parse(textBox1.Text)).ToString();
                    break;
                case "-":
                    textBox1.Text = (resultValue - Double.Parse(textBox1.Text)).ToString();
                    break;
                case "*":
                    textBox1.Text = (resultValue * Double.Parse(textBox1.Text)).ToString();
                    break;
                case "/":
                    textBox1.Text = (resultValue / Double.Parse(textBox1.Text)).ToString();
                    break;
            }
        }

        private void labell(object sender, EventArgs e)
        {

        }
    }
}
