using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class End_Game : Form
    {
        GameSettings gameSettings;
        int timer;
        int flags;
        double completionPercentage;
        bool victory;
        public Log log {  get; set; }
        public End_Game(GameSettings gameSettings, int timer, int flags, double completionPercentage, bool victory)
        {
            InitializeComponent();
            this.gameSettings = gameSettings;
            this.timer = timer;
            this.flags = flags;
            this.completionPercentage = completionPercentage;
            this.victory = victory;
            log = null;
        }

        private void End_Game_Load(object sender, EventArgs e)
        {
            if (victory)
            {
                label1.Text = "Congratulations, You Won!";
                progressBar1.ForeColor = Color.Green;
                progressBar1.Value = 100;
            }
            else
            {
                label1.Text = "Good Luck Next Time, You Lost!";
                progressBar1.ForeColor = Color.Red;
                progressBar1.Value = (int) completionPercentage;
            }
            Console.Write($"Completion Percentage: {completionPercentage}");
            label3.Text = "Game Settings: " + gameSettings.fieldSizeStr + " Field on " + gameSettings.difficulty + " Mode";
            label4.Text = "Time Played: " + (timer / 60).ToString("00") + ":" + (timer % 60).ToString("00");
            label5.Text = "Username: " + gameSettings.username;

            log = new Log(gameSettings.username, timer, completionPercentage, gameSettings.fieldSizeStr, gameSettings.difficulty, victory);
        }
    }
}
