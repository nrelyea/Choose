using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace What_To_Eat
{
    public partial class Form1 : Form
    {
        public List<string> maybeList = new List<string> { };
        public List<string> evaluations = new List<string> { };
        public int index = 0;



        public string readout = "readout";


        public Form1(List<string> mL)
        {
            this.KeyPreview = true;
            InitializeComponent();
            maybeList = mL;
            button1.Text = maybeList[0];
            button2.Text = maybeList[1];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Form1_Paint);

        }

        public void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (index == -1)
            {
                write(e, "evaluation complete!", 50, 50, "Italic", 10);
                write(e, "________________", 50, 51, "Italic", 10);
                button1.Hide();
                button2.Hide();

                for (int i = 0; i < maybeList.Count; i++)
                {
                    write(e, (i + 1) + ". " + maybeList[i], 50, 100 + (20 * i), "Italic", 10);
                }
            }
            else
            {
                write(e, "incomplete index = " + index, 50, 50, "Italic", 10);
                button1.Text = maybeList[index];
                button2.Text = maybeList[index + 1];

                write(e, "or", 280, 275, "Bold", 20);

                try
                {
                    write(e, evaluations[evaluations.Count - 1], 400, 400, "Bold", 20);
                }
                catch (Exception f) { }
            }

        }


        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Form.KeyPress: '" +
                e.KeyChar.ToString() + "' pressed.");

            switch (e.KeyChar)
            {
                case (char)49:
                case (char)52:
                case (char)55:
                    MessageBox.Show("Form.KeyPress: '" +
                        e.KeyChar.ToString() + "' consumed.");
                    e.Handled = true;
                    break;
            }

        }


        public int IncompleteIndex(List<string> maybeList, List<string> evaluations)
        {
            for (int i = 0; i < maybeList.Count - 1; i++)
            {
                if (!evaluations.Contains(maybeList[i] + ">" + maybeList[i + 1]))
                {
                    readout = "eval does NOT contain " + maybeList[i + 1] + ">" + maybeList[i];
                    return i;
                }
            }
            return -1;
        }

        public List<string> SwapStrings(List<string> list, int index1, int index2)
        {
            string temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;

            return list;
        }

        public void write(System.Windows.Forms.PaintEventArgs e, string str, int x, int y, string fontType, int fontSize)
        {

            Font[] fonts = { new Font("Arial", fontSize, FontStyle.Regular),
            new Font("Arial", fontSize, FontStyle.Bold),
            new Font("Arial", fontSize, FontStyle.Italic) };

            int fontTypeIndex = 0;

            if (fontType == "Bold") { fontTypeIndex = 1; }
            else if (fontType == "Italic") { fontTypeIndex = 2; }

            TextFormatFlags flags = TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis;

            TextRenderer.DrawText(e.Graphics, str, fonts[fontTypeIndex],
            new Point(x, y),
            SystemColors.ControlText, flags);

        }

        private void tnrAppTimer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            evaluations.Add(button1.Text + ">" + button2.Text);
            index = IncompleteIndex(maybeList, evaluations);
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            evaluations.Add(button2.Text + ">" + button1.Text);
            maybeList = SwapStrings(maybeList, index, index + 1);
            index = IncompleteIndex(maybeList, evaluations);
            this.Invalidate();
        }
    }
}
