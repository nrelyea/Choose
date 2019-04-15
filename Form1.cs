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
        public string str = "";
        public List<string> maybeList = new List<string> { "pizza", "mexican", "vegan", "thai", "sushi" };
        
        public Form1(string s)
        {
            this.KeyPreview = true;
            InitializeComponent();
            str = s;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Form1_Paint);

        }

        public void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            write(e, str, 200, 200, "Bold", 20);

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


        public void write(System.Windows.Forms.PaintEventArgs e, string str, int x, int y, string fontType, int fontSize)
        {

            Font[] fonts = { new Font("Arial", fontSize, FontStyle.Regular),
            new Font("Arial", fontSize, FontStyle.Bold),
            new Font("Arial", fontSize, FontStyle.Italic) };

            int fontTypeIndex = 0;

            if(fontType == "Bold") { fontTypeIndex = 1; }
            else if (fontType == "Italic") { fontTypeIndex = 2; }

            TextFormatFlags flags = TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis;
            
            TextRenderer.DrawText(e.Graphics, str, fonts[fontTypeIndex],
            new Point(x,y),
            SystemColors.ControlText, flags);
            
        }

        private void tnrAppTimer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "meme";
            this.Invalidate();
        }
    }
}
