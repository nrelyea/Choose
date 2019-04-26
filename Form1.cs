using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace What_To_Eat
{
    public partial class Form1 : Form
    {
        public List<string> rinaList = new List<string> { };
        public List<string> nathanList = new List<string> { };
        public List<string> evaluations = new List<string> { };
        public int index = 0;

        public string state = "start";
        public bool nathanNext = false;
        public bool finalNext = false;

        public string readout = "readout";


        public Form1(List<string> rL, List<string> nL)
        {
            this.KeyPreview = false;
            InitializeComponent();
            rinaList = rL;
            nathanList = nL;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Form1_Paint);

        }

        public void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (state == "start")
            {
                write(e, "Which quiz?", 190, 175, "Bold", 24);
                write(e, "or", 280, 275, "Bold", 20);
                button1.Text = "Rina";
                button2.Text = "Rina / Nathan";
            }
            else if (state != "nathan" && state != "final")
            {
                if (index == -1)
                {
                    write(e, "evaluation complete!", 50, 50, "Italic", 10);
                    write(e, "________________", 50, 51, "Italic", 10);
                    button1.Hide();
                    if (state == "rina")
                    {
                        button2.Hide();
                    }
                    else
                    {
                        button2.Text = "Continue";
                        nathanNext = true;
                    }

                    Thread.Sleep(0);
                    for (int i = 0; i < rinaList.Count; i++)
                    {
                        write(e, (i + 1) + ". " + rinaList[i], 55, 75 + (20 * i), "Italic", 10);
                    }

                    File.WriteAllText(@"c:../../rinaList.json", JsonConvert.SerializeObject(rinaList));
                }
                else
                {
                    button1.Text = rinaList[index];
                    button2.Text = rinaList[index + 1];

                    write(e, "Rina, which sounds better?", 90, 175, "Bold", 24);
                    write(e, "or", 280, 275, "Bold", 20);
                }
            }
            else if (state != "final")
            {

                if (index == -1)
                {
                    write(e, "evaluation complete!", 50, 50, "Italic", 10);
                    write(e, "________________", 50, 51, "Italic", 10);
                    button1.Hide();
                    button2.Text = "Combined List";
                    finalNext = true;


                    for (int i = 0; i < nathanList.Count; i++)
                    {
                        write(e, (i + 1) + ". " + nathanList[i], 55, 75 + (20 * i), "Italic", 10);
                    }

                    File.WriteAllText(@"c:../../nathanList.json", JsonConvert.SerializeObject(nathanList));
                }
                else
                {
                    button1.Text = nathanList[index];
                    button2.Text = nathanList[index + 1];

                    write(e, "Nathan, which sounds better?", 70, 175, "Bold", 24);
                    write(e, "or", 280, 275, "Bold", 20);
                }

            }
            else
            {
                write(e, "  Combined Lists: ", 50, 50, "Italic", 10);
                write(e, "________________", 50, 51, "Italic", 10);
                button1.Hide();
                button2.Hide();

                List<CombinedItem> finalList = CombineLists(rinaList, nathanList);
                for (int i = 0; i < finalList.Count; i++)
                {
                    write(e, (i + 1) + ". " + finalList[i].Option + "    (score = " + finalList[i].Score + ")", 55, 75 + (20 * i), "Italic", 10);
                }
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

        public List<CombinedItem> CombineLists(List<string> rL, List<string> nL)
        {
            List<CombinedItem> combinedItemList = new List<CombinedItem> { };

            for (int i = 0; i < rL.Count; i++)
            {
                combinedItemList.Add(new CombinedItem(rL[i], Score(rL, rL[i]) + Score(nL, rL[i])));
            }

            combinedItemList = combinedItemList.OrderByDescending(o => o.Score).ToList();

            for (int i = 0; i < combinedItemList.Count - 1; i++)
            {
                if (combinedItemList[i].Score == combinedItemList[i + 1].Score && rL.IndexOf(combinedItemList[i + 1].Option) < nL.IndexOf(combinedItemList[i + 1].Option))
                {
                    combinedItemList = SwapItems(combinedItemList, i, i + 1);
                }
            }

            return combinedItemList;
        }

        public int Score(List<string> lst, string str)
        {
            return lst.Count - lst.IndexOf(str);
        }

        public int IncompleteIndex(List<string> rinaList, List<string> evaluations)
        {
            for (int i = 0; i < rinaList.Count - 1; i++)
            {
                if (!evaluations.Contains(rinaList[i] + ">" + rinaList[i + 1]))
                {
                    readout = "eval does NOT contain " + rinaList[i + 1] + ">" + rinaList[i];
                    return i;
                }
            }
            return -1;
        }

        public List<CombinedItem> SwapItems(List<CombinedItem> list, int index1, int index2)
        {
            CombinedItem temp = new CombinedItem(list[index1].Option, list[index1].Score);

            list[index1].Option = list[index2].Option;
            list[index1].Score = list[index2].Score;

            list[index2].Option = temp.Option;
            list[index2].Score = temp.Score;

            return list;
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
            if (state == "start")
            {
                state = "rina";
            }
            else if (state == "rina" || state == "both")
            {
                evaluations.Add(button1.Text + ">" + button2.Text);
                index = IncompleteIndex(rinaList, evaluations);
            }
            else
            {
                evaluations.Add(button1.Text + ">" + button2.Text);
                index = IncompleteIndex(nathanList, evaluations);
            }
            this.Invalidate();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (state == "start")
            {
                state = "both";
            }
            else if (nathanNext)
            {
                evaluations.Clear();
                button1.Show();
                index = 0;
                nathanNext = false;
                state = "nathan";
            }
            else if (finalNext)
            {
                finalNext = false;
                state = "final";

            }
            else if (state == "rina" || state == "both")
            {
                evaluations.Add(button2.Text + ">" + button1.Text);
                rinaList = SwapStrings(rinaList, index, index + 1);
                index = IncompleteIndex(rinaList, evaluations);
            }
            else
            {
                evaluations.Add(button2.Text + ">" + button1.Text);
                nathanList = SwapStrings(nathanList, index, index + 1);
                index = IncompleteIndex(nathanList, evaluations);
            }
            this.Invalidate();
        }
    }
}
