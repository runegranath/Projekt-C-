using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt
{
    public class GameUI
    {
        public static void PrintWelcomeScreen()
        {
            Console.Write("=== VÄLKOMMEN TILL ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("STEN");
            Console.ResetColor();

            Console.Write(", ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("SAX");
            Console.ResetColor();

            Console.Write(", ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("PÅSE");
            Console.ResetColor();
            Console.Write("-SPELET");

            Console.WriteLine(" ===\n");
        }
        public static void PrintChoiceAscii(string playerName, Choice choice)
        {
            Console.WriteLine($"{playerName} valde {choice}:");

            switch (choice)
            {
                case Choice.Sten:
                    Console.WriteLine(@"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
");
                    break;

                case Choice.Sax:
                    Console.WriteLine(@"
   _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)
");
                    break;

                case Choice.Påse:
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
          _______)
         _______)
---.__________)
");
                    break;
            }
        }
    }
}
