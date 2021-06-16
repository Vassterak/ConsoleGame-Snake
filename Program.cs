using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_Snake
{
    class Program
    {
        public static bool gameContinues = true;
        static void Main(string[] args)
        {
            Snake snake;
            int x = 25, y = 25, speed = 1; //default values of map size and snake speed

            while (true)
            {
                switch ((MainMenu.MenuStates)MainMenu.ShowMainMenu())
                {
                    case MainMenu.MenuStates.Play:
                        Console.Clear();
                        gameContinues = true;
                        snake = new Snake(x, y, speed);
                        while (gameContinues)
                        {
                            snake.Movement();
                            snake.PlayerUpdate();
                        }

                        break;

                    case MainMenu.MenuStates.Settings:
                        Console.Clear();
                        var value = MainMenu.Settings();
                        x = value.Item1;
                        y = value.Item2;
                        speed = value.Item3;
                        break;

                    case MainMenu.MenuStates.HowToPlay:
                        Console.Clear();
                        MainMenu.Instructions();
                        Console.ReadKey(true); //wait until user press any key to return to main menu.
                        break;

                    case MainMenu.MenuStates.End:
                        System.Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }


    }
}
