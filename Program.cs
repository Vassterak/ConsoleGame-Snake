using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                switch ((MainMenu.MenuStates)MainMenu.ShowMainMenu())
                {
                    case MainMenu.MenuStates.Play:
                        Console.Clear();

                        break;

                    case MainMenu.MenuStates.CustomGame:
                        Console.Clear();

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
