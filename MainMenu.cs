using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_Snake
{
     static class MainMenu
    {
        public enum MenuStates
        {
            Play = 0,
            Settings = 1,
            HowToPlay = 2,
            End = 3
        }
        public static int ShowMainMenu()
        {
            int selectedItemID = 0;
            int enumLenght = Enum.GetNames(typeof(MenuStates)).Length; //Enum.GetNames(typeof(ManuStates)).Length return number of names is enum
            bool selectionFinished = false;
            ConsoleKeyInfo PressedKey;

            Console.Clear(); //clear console at the beginning of the program

            while (!selectionFinished) //lopp until user selected item in menu
            {
                Console.SetCursorPosition(0, 1); //so it's not displaying menu items from top of the window. (It looks better)

                for (int i = 0; i < enumLenght; i++)
                {
                    if (selectedItemID == i)
                        Console.BackgroundColor = ConsoleColor.Blue;

                    Console.WriteLine((MenuStates)i);
                    Console.ResetColor();
                }

                PressedKey = Console.ReadKey(true); //when true => character wont show up in console

                if (PressedKey.Key == ConsoleKey.DownArrow && selectedItemID < enumLenght - 1) //setup a limit so you cannot go out of selection menu
                    selectedItemID++;

                else if (PressedKey.Key == ConsoleKey.UpArrow && selectedItemID > 0) //setup a limit so you cannot go out of selection menu
                    selectedItemID--;

                else if (PressedKey.Key == ConsoleKey.Enter) //confirm your selection
                    selectionFinished = true;
            }
            return selectedItemID; //return selected item id
        }

        public static void Instructions()
        {
            Console.WriteLine("Goal of this game is to last long as possible while trying to reach highest score.");
            Console.WriteLine("To raise your score you need to pick up as many points as you can.");
            Console.WriteLine("But while you are doing that your lenght is also increasing so it becomes harder to move around.");
            Console.WriteLine("Game ends when you crash into yourself or into the wall.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to continue.");
            Console.ResetColor();
        }

        public static Tuple<int, int, int> Settings()
        {
            int x, y, speed;
            Console.WriteLine("Set size of map");

            Console.Write("Enter the game field width(10-40) : ");
            while (!int.TryParse(Console.ReadLine().Trim().ToLower(), out x))
                Console.WriteLine("Invalit input! Please try again.");

            Console.Write("Enter the game field height(10-25) : ");
            while (!int.TryParse(Console.ReadLine().Trim().ToLower(), out y))
                Console.WriteLine("Invalit input! Please try again.");

            Console.Write("Enter the speed of game frames in ms(50-500) : ");
            while (!int.TryParse(Console.ReadLine().Trim().ToLower(), out speed))
                Console.WriteLine("Invalit input! Please try again.");

            if (x < 10 || x > 40)
                x = 25;
            if (y < 10 || y > 25)
                y = 15;
            if (speed < 50 || speed > 500)
                speed = 100;


            return Tuple.Create(x, y, speed);
        }
    }
}
