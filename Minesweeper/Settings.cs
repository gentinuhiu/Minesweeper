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
    public partial class Settings : Form
    {
        public GameSettings gameSettings { get; set; }
        public Settings()
        {
            InitializeComponent();
            tbUsername.Text = "user";
            rbMedium.Checked = true;
            rbMediumDifficulty.Checked = true;
            gameSettings = new GameSettings(tbUsername.Text, FIELD_SIZE.Medium, DIFFICULTY.Medium);
        }
        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FIELD_SIZE fieldSize;
            DIFFICULTY difficulty;

            if (rbSmall.Checked)
                fieldSize = FIELD_SIZE.Small;
            else if (rbMedium.Checked)
                fieldSize = FIELD_SIZE.Medium;
            else fieldSize = FIELD_SIZE.Large;

            if (rbEasy.Checked)
                difficulty = DIFFICULTY.Easy;
            else if (rbMediumDifficulty.Checked)
                difficulty = DIFFICULTY.Medium;
            else difficulty = DIFFICULTY.Hard;

            gameSettings = new GameSettings(tbUsername.Text, fieldSize, difficulty);
            DialogResult = DialogResult.OK;
        }
        public void configure(GameSettings gameSettings)
        {
            tbUsername.Text = gameSettings.username;

            if (gameSettings.fieldSizeStr == FIELD_SIZE.Small)
                rbSmall.Checked = true;
            else if(gameSettings.fieldSizeStr == FIELD_SIZE.Medium)
                rbMedium.Checked = true;
            else rbLarge.Checked = true;

            if (gameSettings.difficulty == DIFFICULTY.Easy)
                rbEasy.Checked = true;
            else if(gameSettings.difficulty == DIFFICULTY.Medium)
                rbMediumDifficulty.Checked = true;
            else rbHard.Checked = true;
        }
    }
}
