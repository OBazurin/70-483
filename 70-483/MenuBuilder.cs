using System;
using System.Linq;
using System.Reflection;

namespace _70_483
{
    public static class MenuBuilder
    {
        private static void PrintMenu(string[] menu, int choise)
        {
            Console.Clear();
            for (int i = 0; i < menu.Length; i++)
            {
                if (i > 0)
                {
                    if (i == choise)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(string.Concat($"{i}. ", menu[i]));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine(string.Concat($"{i}. ", menu[i]));
                    }
                }
                else
                {
                    Console.WriteLine(menu[i]);
                }
            }
        }

        /// <summary>
        /// Takes a typeof() of method, and shows all puclic members as a menu items
        /// </summary>
        /// <param name="type"></param>
        /// <returns>String name of currently selected menu item</returns>
        private static string Menu(Type type)
        {
            int choise = 1;

            MethodInfo[] MethodNames = type.GetMethods(BindingFlags.Static | BindingFlags.Public);

            var currentMenu = new string[MethodNames.Length + 1];
            currentMenu[0] = type.Name;
            MethodNames.Select(x => x.Name).ToArray().CopyTo(currentMenu, 1);

            PrintMenu(currentMenu, choise);
            var key = ConsoleKey.E;
            while (!key.Equals(ConsoleKey.Enter))
            {
                key = Console.ReadKey(true).Key;
                if (key.Equals(ConsoleKey.DownArrow))
                {
                    choise++;
                    if (choise == currentMenu.Length)
                    {
                        choise = 1;
                    }
                    PrintMenu(currentMenu, choise);
                }
                if (key.Equals(ConsoleKey.UpArrow))
                {
                    choise--;
                    if (choise < 1)
                    {
                        choise = currentMenu.Length - 1;
                    }
                    PrintMenu(currentMenu, choise);
                }
            }
            return currentMenu[choise];
        }

        public static void BuildMenu(Type type)
        {
            string choise = Menu(type);
            type.GetMethod(choise).Invoke(type, null);
        }
    }
}
