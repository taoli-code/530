using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RichMan
{
    public partial class RichMan : Form
    {
        static string livePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        static Image[] images = new Image[8];
        static int i = 0;
        static bool isWheelSpinning = false;
        static Thread spinWheel = new Thread(SpinTheWheel);
        static PictureBox wh = new PictureBox(); //did this to be able to close the spinWheel thread anywher in code

        public RichMan()
        {
            InitializeComponent();
            wh = Wheel_PictureBox;
        }

        static void ChangeWheel_Image()
        {
            if (i + 1 >= images.Length)
                i = 0;
            else
                i++;

            images[i] = Image.FromFile(livePath + "\\resources\\Slot " + (i + 1) + " active.png");
            wh.Invoke(new Action(() => wh.BackgroundImage = images[i]));
        }

        static void SpinTheWheel()
        {
            Random rand = new Random();
            int cycle = 1;

            for (int y = 0; y < 3; y++)
            {
                //Fast spin speed
                int fastSpin = rand.Next((55 - ((cycle - 1) * 20)), (65 - ((cycle - 1) * 15)));
                for (int x = 0; x < fastSpin; x++)
                {
                    ChangeWheel_Image();
                    Thread.Sleep(40 * cycle);
                }
                cycle++;
            }

            isWheelSpinning = false;
            MessageBox.Show("Congrats!! You won prize " + (i + 1));
            spinWheel.Abort();
        }

        private void Spin_Button_Click(object sender, EventArgs e)
        {
            if (!isWheelSpinning)
            {
                Thread spinWheel = new Thread(SpinTheWheel);
                isWheelSpinning = true;
                spinWheel.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the game designed for learning of network security. \t\n" +
                "How to play: \t\n" +
                "1. As a rich man, click start button to start the game.\t\n" +
                "2. Spin wheel to move rich man forward to related cell. point(s) of wheel is between 1 and 8.\t\n" +
                "3. Different color of Cells represent different complexity level of question\t\n" +
                "4. Player will get score after answering the question.\t\n" +
                "5. Once player arrives end, game is over.\t\n");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            Spin_Button.Enabled = true;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

