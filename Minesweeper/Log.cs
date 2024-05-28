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
            return $"({getCoefficientStr()}) {username}   -   Time: {time}   -   Completed: {completionPercentage.ToString("00")}%   ({fieldSize} Field on {difficulty} Mode)";
        }
        public double getCoefficient()
        {
            double size = 10;
            double difficulty = 1;

            if (fieldSize == FIELD_SIZE.Medium)
                size = 15;
            else if (fieldSize == FIELD_SIZE.Large)
                size = 20;

            if (this.difficulty == DIFFICULTY.Medium)
                difficulty = 1.5;
            else if (this.difficulty == DIFFICULTY.Hard)
                difficulty = 2.5;

            return (double)(size * difficulty * completionPercentage) / (double) timer;
        }
        public string getCoefficientStr()
        {
            return getCoefficient().ToString("00.00");
        }
    }
}
