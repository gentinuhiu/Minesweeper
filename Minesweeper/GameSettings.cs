using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public enum FIELD_SIZE
    {
        Small, Medium, Large
    }
    public enum DIFFICULTY
    {
        Easy, Medium, Hard
    }
    public class GameSettings
    {
        public string username { get; set; }
        public int fieldSize { get; set; }
        public int minesCount { get; set; }
        public FIELD_SIZE fieldSizeStr { get; set; }
        public DIFFICULTY difficulty { get; set; }

        public GameSettings(string username, FIELD_SIZE fieldSizeStr, DIFFICULTY difficulty)
        {
            this.username = username;
            this.fieldSizeStr = fieldSizeStr;
            this.difficulty = difficulty;
            this.fieldSize = 10;
            minesCount = 10;

            if (fieldSizeStr == FIELD_SIZE.Medium)
                fieldSize = 15;
            else if (fieldSizeStr == FIELD_SIZE.Large)
                fieldSize = 20;

            if (difficulty == DIFFICULTY.Easy)
                minesCount = fieldSize;
            else if (difficulty == DIFFICULTY.Medium)
                minesCount = (int)(fieldSize * 1.5); 
            else minesCount = (int)(fieldSize * 2.5); 
        }

        public override string ToString()
        {
            return $"{username} ({fieldSizeStr})";
        }
    }
}
