using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Log
    {
        public string username { get; set; }
        public int timer { get; set; }
        public double completionPercentage { get; set; }
        public FIELD_SIZE fieldSize { get; set; }
        public DIFFICULTY difficulty { get; set; }
        public bool victory { get; set; }

        public Log(string username, int timer, double completionPercentage, FIELD_SIZE fieldSize, DIFFICULTY difficulty, bool victory)
        {
            this.username = username;
            this.timer = timer;
            this.completionPercentage = completionPercentage;
            this.fieldSize = fieldSize;
            this.difficulty = difficulty;
            this.victory = victory;
        }

        public override string ToString()
        {
            string time = (timer / 60).ToString("00") + ":" + (timer % 60).ToString("00");
            return $"{username}   -   Time: {time}   -   Completed: {completionPercentage.ToString("00")}%   ({fieldSize} Field on {difficulty} Mode)";
        }
    }
}
