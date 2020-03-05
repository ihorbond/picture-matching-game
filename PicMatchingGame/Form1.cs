using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PicMatchingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked;
        Label secondClicked;

        int timeLeft = 45;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach(Control c in tableLayoutPanel1.Controls)
            {
                if (c is Label iconLabel)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = Color.DeepSkyBlue;
                    icons.RemoveAt(randomNumber);
                }
            }
            
        }

        private void label_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching 
            // icons have been shown to the player, 
            // so ignore any clicks if the timer is running
            if (timer1.Enabled) return;

            if (sender is Label label && label.ForeColor != Color.Black)
            {
                if (firstClicked == null)
                {
                    firstClicked = label;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = label;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label iconLabel && iconLabel.ForeColor == iconLabel.BackColor)
                {
                    return;
                }
            }

            timeElapsedTimer.Stop();
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateTimer();
            (sender as Button).Visible = false;
            timeElapsedTimer.Start();
        }

        private void timeElapsedTimer_Tick(object sender, EventArgs e)
        {
            if(timeLeft > 0)
            {
                timeLeft--;
                UpdateTimer();
            }
            else
            {
                timeElapsedTimer.Stop();
                MessageBox.Show("Time is up!", "Sorry");
                Close();
            }
        }

        private void UpdateTimer()
        {
            var timespan = TimeSpan.FromSeconds(timeLeft);
            timeElapsedLabel.Text = timespan.ToString("mm\\:ss");
        }
    }
}
