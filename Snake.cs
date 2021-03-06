using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ConsoleGame_Snake
{
    class Snake
    {
        Random rnd = new Random();
        Program program = new Program();

        //Game World
        bool canMove;
        int[,] map; 
        int[,] bodyLifeTime;
        int score;
        int x, y;
        Timer fps; //fps in miliseconds

        //GameStates
        private const int freeSpace = 0, wall = 1, point = 2, snake = 3;
        ConsoleKeyInfo lastKey; //last key pressed

        public Snake(int mapWidth, int mapHeight, int speed)
        {
            canMove = false;
            lastKey = new ConsoleKeyInfo((char)ConsoleKey.RightArrow,ConsoleKey.RightArrow, false, false, false); //necessary for initial direction
            fps = new Timer(GameUpdate, null, 0, speed);
            score = 1;
            map = new int[mapWidth, mapHeight]; //set world size
            bodyLifeTime = new int[mapWidth, mapHeight]; //set world size for life value of each block

            for (int x = 0; x < mapWidth; x++) //setting up walls X cordinaties, body has same properties as wall, so it's not necessary to create separate ID for wall
            {
                map[x, 0] = wall;
                map[x, mapHeight - 1] = wall;
            }

            for (int y = 0; y < mapHeight; y++) //setting up walls Y cordinaties
            {
                map[0, y] = wall;
                map[mapWidth - 1, y] = wall;
            }

            //initial point generation (it's not random.)
            map[mapWidth / 2 + 3, mapHeight / 2] = point;
            x = mapWidth / 2 - 4;
            y = mapHeight / 2;

            //initial player location
            map[mapWidth / 2 - 4, mapHeight / 2] = snake;
            bodyLifeTime[mapWidth / 2 - 4, mapHeight / 2] = score;

            MapRender();
        }

        public void Movement() //called by the user
        {
            lastKey = Console.ReadKey(true);
        }

        private void AutomaticMovement(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.DownArrow:
                    y++;
                    break;

                case ConsoleKey.UpArrow:
                    y--;
                    break;

                case ConsoleKey.LeftArrow:
                    x--;
                    break;

                case ConsoleKey.RightArrow:
                    x++;
                    break;

                case ConsoleKey.Escape:
                    End();
                    break;

                default:
                    x++;
                    break;
            }
            ObstacleDetection();
        }

        private void RandomPointGeneration()
        {
            int rndX = rnd.Next(1, map.GetLength(0)-1), rndY = rnd.Next(1, map.GetLength(1)-1);
            if (map[rndX, rndY] != snake)
            {
                map[rndX, rndY] = point; //generate new point
            }
            else
                RandomPointGeneration(); //if generation fails bacause there is body, try again. (I know this is a horrible way to do this. In case of optimalization)
        }

        public void PlayerUpdate() //Updating path behind player
        {
            if (map[x, y] == point)
            {
                score++;
                RandomPointGeneration();
            }

            map[x, y] = snake;
            bodyLifeTime[x, y] = score;

            MapRender();
            UIRender();
        }

        private void GameUpdate(Object o)
        {
            AutomaticMovement(lastKey);

            if (canMove) //this is required because we dont want to run PlayerUpdate() in first load after constructor runs line 30.
                PlayerUpdate();
            canMove = true;
        } //called every n-miliseconds

        private void ObstacleDetection()
        {
            if (map[x, y] == wall)
                End();

            else
            {
                switch (lastKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (map[x, y + 1] == snake)
                            End();

                        break;

                    case ConsoleKey.UpArrow:
                        if (map[x, y - 1] == snake)
                            End();
                        break;

                    case ConsoleKey.LeftArrow:
                        if (map[x-1, y] == snake)
                            End();
                        break;

                    case ConsoleKey.RightArrow:
                        if (map[x+1, y] == snake)
                            End();
                        break;

                    default:
                        break;
                }
            }
        }

        private void End()
        {
            fps.Dispose();
            Console.Clear();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("You lost!");
            Console.WriteLine($"Your score was: {score-1}");
            Program.gameContinues = false;
            canMove = false;
        }

        private void MapRender()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    switch (map[x, y])
                    {
                        case wall:
                            Console.SetCursorPosition(x, y);
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write(" ");
                            Console.ResetColor();
                            break;

                        case point:
                            Console.SetCursorPosition(x, y);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("#");
                            Console.ResetColor();
                            break;

                        case snake:
                            Console.SetCursorPosition(x, y);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(" ");
                            Console.ResetColor();
                            break;

                        default: //freeSpaceb  
                            Console.SetCursorPosition(x, y);
                            Console.Write(" ");
                            break;
                    }

                    if (bodyLifeTime[x, y] > 0)
                        bodyLifeTime[x, y]--;
                    else
                    {
                        if (map[x,y] == snake)
                            map[x, y] = freeSpace;
                    }
                }
            }
        }

        private void UIRender()
        {
            Console.SetCursorPosition(0, map.GetLength(1) + 1);
            Console.WriteLine($"Your score: {score-1}");
        }

    }
}
