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
        private int mapHeight, mapWidht;
        int[,] map;

        //GameStates
        private const int freeSpace = 0, body = 1, point = 2;

    }
}
