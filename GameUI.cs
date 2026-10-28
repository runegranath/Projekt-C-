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
            Console.WriteLine("=============================================");
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

            Console.WriteLine(" ===");
            Console.WriteLine("=============================================\n");
        }

        public static void PrintRoundResult(string resultText, bool humanWon, bool computerWon)
        {
            if (humanWon)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (computerWon)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Oavgjort
            }

            Console.WriteLine(resultText);
            Console.ResetColor();
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
        public static void PrintEasterEgg()
        {
            Console.WriteLine("\n  *** PÅSKÄGG HITTAT! ***");
            Console.WriteLine(@"    _______");
            Console.WriteLine(@"   /       \");
            Console.WriteLine(@"  | (O) (O) |");
            Console.WriteLine(@"  |    ^    |");
            Console.WriteLine(@"   _   _   _ ");
            Console.WriteLine(@"  | | | | | |");
            Console.WriteLine(@"  | |_| |_| |");
            Console.WriteLine(@"   \       /");
            Console.WriteLine(@"    \_____/");
            Console.WriteLine("\nMänniskorna har nu vunnit tre matcher. Ett steg mot att hålla maskinen på mattan.");
            Console.WriteLine("Skynet-protokollet inaktiverat... tills vidare.\n");
        }
    }
}
