using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_Snake
{
    class Snake
    {
        Random rnd = new Random(); //random for point generation

        //Game World
        int[,] map;
        int[,] mapTime;
        int x, y;

        //GameStates
        private const int freeSpace = 0, body = 1, point = 2;
        private int snakeBodyLifeTime;

        private int LastX = 1, LastY = 1;

        public Snake(int mapWidth, int mapHeight, int speed)
        {
            map = new int[mapWidth, mapHeight];
            mapTime = new int[mapWidth, mapHeight]; //for time to live body

            map[mapWidth / 2 + 4, mapHeight / 2] = point; //initial point generation
            x = mapWidth / 2 - 4;
            y = mapHeight / 2;

            for (int x = 0; x < mapWidth; x++) //setting up walls X cordinaties, body has same properties as wall, so it's not necessary to create separate ID for wall
            {
                map[x, 0] = body;
                map[x, mapHeight - 1] = body;
            }

            for (int y = 0; y < mapHeight; y++) //setting up walls Y cordinaties
            {
                map[0, y] = body;
                map[mapWidth - 1, y] = body;
            }
            MapRender();
        }

        public void Movement()
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);

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
                    Program.gameContinues = false;
                    break;
            }
            GameMapUpdate();

        }

        private void GameMapUpdate() //how many points are left + checking if player is eligible to finish game
        {
            if (map[x, y] == point) //if point is picked up
            {
                snakeBodyLifeTime++;
                map[x, y] = 0;
                PointGeneration();
            }
        }

        private void PointGeneration()
        {
            int rndX = rnd.Next(1, map.GetLength(0)), rndY = rnd.Next(1, map.GetLength(1));
            if (map[rndX, rndY] != body)
            {
                map[rndX, rndY] = point; //generate new point
                MapRender();
            }
            else
                PointGeneration(); //if generation fails bacause there is body, try again. (I know this is a horrible way to do this. In case of optimalization)
        }

        public void PlayerUpdate() //Updating path behind player
        {
            mapTime[LastX, LastY] = snakeBodyLifeTime;
            map[LastX, LastY] = body;

            for (int x = 0; x < mapTime.GetLength(0); x++)
            {
                for (int y = 0; y < mapTime.GetLength(1); y++)
                {
                    //if (mapTime[x, y] != 0)
                    //{
                    //    mapTime[x, y] -= 1;
                    //    Console.SetCursorPosition(LastX, LastY);
                    //    Console.BackgroundColor = ConsoleColor.White;
                    //    Console.Write(" ");
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y);
                    //    Console.BackgroundColor = ConsoleColor.White;
                    //    Console.Write(" ");
                    //    Console.ResetColor();
                    //}
                }
            }





            LastX = x;
            LastY = y;
        }


        private void MapRender()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == body) //obstacles (walls)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Console.ResetColor();

                    }
                    else if (map[x, y] == point)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("#");
                        Console.ResetColor();
                    }
                }
            }
        }

    }
}
