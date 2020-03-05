using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    iconLabel.Visible = false;
                    icons.RemoveAt(randomNumber);
                }
            }
            
        }
    }
}
