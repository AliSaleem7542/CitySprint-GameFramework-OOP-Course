using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstDesktopApp
{
    public partial class GameOverForm : Form
    {
       
            public GameOverForm(int score)
        {
            Width = 400;
            Height = 300;
            Text = "Game Over";

            Label lbl = new Label()
            {
                Text = $"GAME OVER\nScore: {score}",
                Font = new Font("Arial", 20),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(lbl);
        }
    }
    }

