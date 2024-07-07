# Minesweeper Game Documentation
Project in .NET Windows Form by Genti Nuhiu

## Game Description
The goal of Minesweeper is to uncover all the squares on a grid that do not contain mines without being "blown up" by clicking on a square with a mine underneath. Safe squares have numbers telling you how many mines touch the square. You can use the number clues to solve the game by opening all of the safe squares. If you click on a mine you lose the game!

<div style="display:grid;grid-template-columns:auto auto;">
<img src="https://github.com/gentinuhiu/Minesweeper/blob/main/Minesweeper/github-images/grid-empty.png" width="49%">
<img src="https://github.com/gentinuhiu/Minesweeper/blob/main/Minesweeper/github-images/mines-hide.png" width="49%">
</div>

## Gameplay
### 1. Objective
Click all the squares that do not contain mines and flag the squares you suspect of having mines underneath.

### 2. Controls
<ul>
  <li>Left-click: open mine</li>
  <li>Right-click: flag mine</li>
</ul>

### 3. Rules
<ul>
  <li>The game is won only when all safe squares have been opened and all mines have been flagged</li>
  <li>The game is lost only when a mine square has been opened</li>
</ul>

## Game Functionalities
### 1. Settings
The game starts with a window where you can enter your game name, select grid size, select difficulty (number of mines in the grid) and an option to show the hidden mines.

<img src="https://github.com/gentinuhiu/Minesweeper/blob/main/Minesweeper/github-images/settings.png" width="35%">

### 2. Initializing grid
The game creates a grid of cells according to the selected grid size in settings:
<ul>
  <li>Small: 10x10</li>
  <li>Medium: 15x15</li>
  <li>Large: 20x20</li>
</ul>

### 3. Planting mines
The game will plant the mines across the grid only after the first click has been made, in order to avoid clicking the mine in the first try.<br>
The ratio mines to squares in the grid is determined by the selected difficulty in settings:
<ul>
  <li>Easy: 10/100</li>
  <li>Medium: 15/100</li>
  <li>Hard: 25/100</li>
</ul>
For example: in a game with large field (20x20) and hard difficulty, the number of mines is 50.<br>
The game allows you to show the position of the hidden mines in the grid (used for testing purposes only).<br>

<img src="https://github.com/gentinuhiu/Minesweeper/blob/main/Minesweeper/github-images/mines-show.png" width="35%">

### 4. Opening safe squares and flagging
After clicking a safe square, the BFS algorithm will traverse through all safe squares starting from the one we clicked, till it reaches a square which is neighboring at least one mine. When such mine is reached, a number representing the neighboring mines will be written in the square. With right-click, we can flag the square we suspect of having mine.

<img src="https://github.com/gentinuhiu/Minesweeper/blob/main/Minesweeper/github-images/grid-click.png" width="35%">

Function for opening all safe squares<br>
      
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

                int counter = checkMine(x, y); // counting neighboring mines
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
  
Function for counting neighboring mines<br>

  
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
  
### 5. History and ranking
History window keeps a log for each game played. The log will be saved only when you finish or lose the game, not when you restart it. The history window has the option to sort all the games according to their coefficient in order to assure fairness among players who play in different grid size and difficulty. The coefficient is calculated in the following way:<br>
(size * difficulty * completion percentage) / (timer in seconds)

<img src="https://github.com/gentinuhiu/Minesweeper/blob/main/Minesweeper/github-images/history.png" width="35%">

### 6. New game 
The game saves your settings so when you start a new game, the user selected settings will show up, instead of the default ones.  
