using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Field
    {
        List<List<Mine>> field;
        //int x, y;
        public Field(int fieldSize, int minesCount)
        {
            field = new List<List<Mine>>();
            //x = -1;
            //y = -1;
            int horizontalOffset = 120;
            int verticalOffset = 120;

            if (fieldSize == 15)
            {
                horizontalOffset = 65;
                verticalOffset = 65;
            }

            else if (fieldSize == 20)
            {
                horizontalOffset = 8;
                verticalOffset = 25;
            }

            List<Point> mines = new List<Point>();
            Random random = new Random();
            int counter = 0;


            while (counter < minesCount)
            {
                int x = random.Next(0, fieldSize);
                int y = random.Next(0, fieldSize);
                bool flag = true;

                foreach (Point p in mines)
                {
                    if (p.X.Equals(x) && p.Y.Equals(y))
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    mines.Add(new Point(x, y));
                    counter++;
                }
            }


            for (int i = 0; i < fieldSize; i++)
            {
                List<Mine> row = new List<Mine>();
                for (int j = 0; j < fieldSize; j++)
                    row.Add(new Mine(new Point(horizontalOffset + (i * 22), verticalOffset + (j * 22))));
                field.Add(row);
            }
            foreach (Point p in mines)
            {
                field[p.X][p.Y].isMine = true;
            }
        }
        public void show(Graphics g)
        {
            foreach (List<Mine> row in field)
                foreach (Mine m in row)
                    m.draw(g);
        }
        public int flag(Point mouseLocation)
        {
            foreach (List<Mine> row in field)
                foreach (Mine m in row)
                {
                    int result = m.flag(mouseLocation);
                    if (result != -1)
                        return result;
                }
            return -1;
        }
        public int open(Point mouseLocation)
        {
            for (int i = 0; i < field.Count; i++)
                for (int j = 0; j < field[i].Count; j++)
                {
                    int result = field[i][j].open(mouseLocation);
                    if (result == 0)
                    {
                        foreach (List<Mine> row in field)
                            foreach (Mine m in row)
                                if (m.isMine)
                                    m.showMine = true;
                        return 0;
                    }
                    if (result == 1)
                    {
                        //x = i;
                        //y = j;
                        openMines(i, j);
                        return 1;
                    }
                }
            return -1;
        }
        public void openMines(int x, int y)
        {
            int counter = checkMine(x, y);
            if (counter != 0)
            {
                field[x][y].enterNumber(counter);
                return;
            }
            BFS(x, y);
        }
        public int checkMine(int x1, int y1)
        {

            if (!(x1 >= 0 && x1 < field.Count && y1 >= 0 && y1 < field.Count))
                return 0;

            int counter = 0;
            if (x1 > 0 && field[x1 - 1][y1].isMine)
                counter++;
            if (x1 > 0 && y1 > 0 && field[x1 - 1][y1 - 1].isMine)
                counter++;
            if (x1 > 0 && y1 < field.Count - 1 && field[x1 - 1][y1 + 1].isMine)
                counter++;
            if (y1 < field.Count - 1 && field[x1][y1 + 1].isMine)
                counter++;
            if (y1 > 0 && field[x1][y1 - 1].isMine)
                counter++;
            if (x1 < field.Count - 1 && field[x1 + 1][y1].isMine)
                counter++;
            if (x1 < field.Count - 1 && y1 > 0 && field[x1 + 1][y1 - 1].isMine)
                counter++;
            if (x1 < field.Count - 1 && y1 < field.Count - 1 && field[x1 + 1][y1 + 1].isMine)
                counter++;

            return counter;
        }
        public double completionPercentage()
        {
            int counter = 0;
            foreach (List<Mine> row in field)
            {
                foreach (Mine m in row)
                {
                    if (m.isOpened && !m.isFlagged && !m.isMine)
                        counter++;
                }
            }
            double result = (double) (100 / (double) (field.Count * field.Count)) * counter;
            return result;
        }
        public bool isFinished()
        {
            foreach (List<Mine> row in field)
            {
                foreach (Mine m in row)
                {
                    if (!m.isOpened && !m.isFlagged)
                        return false;
                }
            }
            return true;
        }
        public void BFS(int startX, int startY)
        {
            int rows = field.Count;
            int cols = field.Count;
            bool[,] visited = new bool[rows, cols];
            Queue<Point> queue = new Queue<Point>();

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            queue.Enqueue(new Point(startX, startY));
            visited[startX, startY] = true;

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                int x = current.X;
                int y = current.Y;

                if (field[x][y].isMine)
                    continue;

                field[x][y].open();

                int counter = checkMine(x, y);
                if (counter != 0)
                {
                    field[x][y].enterNumber(counter);
                    continue;
                }

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && !visited[nx, ny])
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue(new Point(nx, ny));
                    }
                }
            }
        }
    }
}
