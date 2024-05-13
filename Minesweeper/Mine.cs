﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Mine
    {
        public Point location;
        public bool isMine;
        public bool isFlagged;
        public bool isOpened;
        public bool explode;
        public bool showMine;
        public int number;

        public Mine(Point location)
        {
            this.location = location;
            isMine = false;
            isFlagged = false;
            isOpened = false;
            explode = false;
            showMine = false;
            number = -1;
        }
        public void draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.DarkGray);
            Pen p = new Pen(Color.Gray);

            if (isOpened)
                b = new SolidBrush(Color.White);

            if (isFlagged)
                b = new SolidBrush(Color.Orange);

            if (showMine)
                b = new SolidBrush(Color.Red);

            if (explode)
                b = new SolidBrush(Color.Black);

            g.FillRectangle(b, location.X, location.Y, 22, 22);
            g.DrawRectangle(p, location.X, location.Y, 22, 22);
            b.Dispose();
            p.Dispose();

            if (number != -1)
            {
                Color color;
                if (number == 1)
                    color = Color.Green;
                else if (number == 2)
                    color = Color.Blue;
                else if (number == 3)
                    color = Color.Red;
                else if (number == 4)
                    color = Color.Purple;
                else color = Color.Brown;

                Font font = new Font("Arial", 12, FontStyle.Regular);
                Brush b1 = new SolidBrush(color);
                g.DrawString(number.ToString(), font, b1, location.X + 3, location.Y + 3);
                b1.Dispose();
            }
        }
        public void enterNumber(int number)
        {
            this.number = number;
        }
        public int flag(Point mouseLocation)
        {
            if (mouseLocation.X < location.X + 22 && mouseLocation.X > location.X && mouseLocation.Y < location.Y + 22 && mouseLocation.Y > location.Y)
            {
                if (isOpened)
                    return -1;
                if (isFlagged)
                {
                    isFlagged = false;
                    return 0;
                }
                else
                {
                    isFlagged = true;
                    return 1;
                }
            }
            return -1;
        }
        public int open(Point mouseLocation)
        {
            if (mouseLocation.X < location.X + 22 && mouseLocation.X > location.X && mouseLocation.Y < location.Y + 22 && mouseLocation.Y > location.Y)
            {
                if (isMine && !isFlagged)
                {
                    showMine = true;
                    explode = true;
                    return 0;
                }
                if (!isOpened && !isFlagged)
                {
                    isOpened = true;
                    return 1;
                }
            }
            return -1;
        }
        public int open()
        {
            if (isClean())
            {
                isOpened = true;
                return 1;
            }
            return -1;
        }
        public bool isClean()
        {
            return !isMine && !isFlagged && !isOpened;
        }
    }
}
