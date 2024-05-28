using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    /// <summary>
    /// PROJECT DONE BY GENTI NUHIU, 2024 
    /// </summary>
    public partial class Form1 : Form
    {
        GameSettings gameSettings;
        Field field;
        int timer;
        int flags;
        List<Log> logs;
        public Form1()
        {
            InitializeComponent();
            logs = new List<Log>();
            setUp(true, true);
        }
        public void setUp(bool isFirst, bool showForm)
        {
            Settings form = new Settings();
            if(!isFirst)
                form.configure(gameSettings, showForm);

            if(showForm)
                form.ShowDialog();

            gameSettings = form.gameSettings;
            field = new Field(gameSettings.fieldSize, gameSettings.minesCount);
            
            label1.Text = gameSettings.ToString();
            label2.Text = "0/" + gameSettings.minesCount;
            lblTime.Text = "00:00";
            timer = 0;
            flags = 0;
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setUp(false, true);
        }
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History form = new History(logs);
            form.ShowDialog();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            field.show(e.Graphics, gameSettings.showMines);   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            int seconds = timer % 60;
            int minutes = timer / 60;
            lblTime.Text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int result = field.open(e.Location);

                if(result != -1)
                {
                    Invalidate();
                    if (result == 0)
                    {
                        End_Game form = new End_Game(gameSettings, timer, flags, field.completionPercentage(), false);
                        form.ShowDialog();
                        logs.Add(form.log);
                        setUp(false, false);
                    }
                    else
                    {
                        if (checkStatus(-1))
                        {
                            End_Game form = new End_Game(gameSettings, timer, flags, field.completionPercentage(), true);
                            form.ShowDialog();
                            logs.Add(form.log);
                            setUp(false, false);
                        }
                    }
                }
            }
            else
            {
                if (field.completionPercentage() == 0)
                    return; 

                int result = field.flag(e.Location);

                if (result != -1)
                {
                    Invalidate();
                    if (checkStatus(result))
                    {
                        End_Game form = new End_Game(gameSettings, timer, flags, field.completionPercentage(), true);
                        form.ShowDialog();
                        logs.Add(form.log);
                        setUp(false, false);
                    }
                }

            }
        }
        private bool checkStatus(int result)
        {
            if (result == 1)
                flags++;
            else if (result == 0)
                flags--;
            
            label2.Text = flags + "/" + gameSettings.minesCount;

            if (flags == gameSettings.minesCount)
                return field.isFinished();

            return false;
        }
    }
}
