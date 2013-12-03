using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Balan
{
    public partial class frmMain : Form
    {
        string output;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lblResult.ResetText();
            string input = txtInput.Text;
            Stack<char> s = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                switch (c)
                {
                    case '(':
                        s.Push(c);
                        break;
                    case ')':
                        while (s.Count != 0 && s.Peek() != '(')
                            output += s.Pop();
                        s.Pop();
                        break;
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        while (s.Count != 0 && priority(s.Peek()) >= priority(c))
                            output += s.Pop();
                        s.Push(c);
                        break;
                    default:
                        output += c;
                        break;
                }
            }
            while (s.Count != 0)
                output += s.Pop();

            lblResult.Text = output;

            Stack<string> sr = new Stack<string>();
            int temp = 0;
            for (int i = 0; i < output.Length; i++)
            {
                char c = output[i];
                if (isOperator(c))
                {
                    int op1 = Int32.Parse(sr.Pop().ToString());
                    int op2 = Int32.Parse(sr.Pop().ToString());

                    switch (c)
                    {
                        case '+':
                            temp = op2 + op1;
                            break;
                        case '-':
                            temp = op2 - op1;
                            break;
                        case '*':
                            temp = op2 * op1;
                            break;
                        case '/':
                            temp = op2 / op1;
                            break;
                        default:
                            break;
                    }

                    sr.Push(temp.ToString());
                }
                else if (!isOperator(c))
                {
                    sr.Push(c.ToString());
                }
            }
            int valueResult = int.Parse(sr.Peek().ToString());
            lblValue.Text = valueResult.ToString();
        }

        private bool isOperator(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/');
        }

        private int priority(char c)
        {
            switch (c)
            {
                case '*':
                case '/':
                    return 2;
                case '+':
                case '-':
                    return 1;
                case '(':
                    return 0;
                default:
                    return -1;
            }
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
