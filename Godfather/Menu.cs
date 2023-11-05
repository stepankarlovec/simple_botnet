using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godfather
{
    public class Menu
    {
         public void renderIntro()
        {
            Console.Write("/$$                   /$$                           /$$    \n| $$                  | $$                          | $$    \n| $$$$$$$   /$$$$$$  /$$$$$$   /$$$$$$$   /$$$$$$  /$$$$$$  \n| $$__  $$ /$$__  $$| _  $$_ /  | $$__  $$ /$$__  $$| _  $$_ /\n| $$  \\ $$| $$  \\ $$  | $$    | $$  \\ $$| $$$$$$$$  | $$    \n| $$  | $$| $$  | $$  | $$ /$$| $$  | $$| $$_____ /  | $$ /$$\n| $$$$$$$/|  $$$$$$/  |  $$$$/| $$  | $$|  $$$$$$$  |  $$$$/\n| _______ /  \\______ /    \\___ /  | __ /  | __ / \\_______ /   \\___ /\n");
            Console.WriteLine();
            Console.WriteLine("this is example of a botnet main commmand server.");
            Console.WriteLine();
            Console.WriteLine("Avaliable commands - [bots, execute, reset]");
        }
    }
}
