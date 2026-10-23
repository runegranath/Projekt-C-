using System;
using System.IO;
using System.Text.Json;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
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
            Console.Write("-SIMULATOR");

            Console.WriteLine(" ===\n");

            // Ladda in tidigare matchstatistik om filen finns
            string filePath = "stats.json";
            GameStats stats = new GameStats(); // Skapar tomt objekt från början

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                stats = JsonSerializer.Deserialize<GameStats>(jsonString) ?? new GameStats(); // Om deserialisering misslyckas, skapa ett nytt GameStats-objekt
                Console.WriteLine($"[Tidigare matchvinster -> Människor: {stats.HumanWins} | Datorn: {stats.ComputerWins}]\n");
            }

            Console.Write("\nAnge ditt namn: ");

            string playerName = Console.ReadLine() ?? "";       // Hanterar null-värden med att sätta input till en tom sträng
            if (string.IsNullOrWhiteSpace(playerName))
            {
                playerName = "Spelare";                         // Standardnamn om inmatningen är tom
            }

            // Skapa spelare och spel
            Player human = new Player { Name = playerName };    // new bygger en ny instans av player-klassen som sätter namnet till användarinmatningen
            Player computer = new Player { Name = "Datorn" };
            Game game = new Game(human, computer);              // Gameklassen tar de två spelarna som argument

            Random random = new Random();                       // slumptalsgenerator för datorns val
            int maxScore = 3;                                   // Maxpoäng för att vinna

            // Spelloop
            while (human.Score < maxScore && computer.Score < maxScore)
            {
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"\n   --- Poäng: {human.Name}: {human.Score} | {computer.Name}: {computer.Score} ---\n");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                Console.WriteLine("Välj ditt drag:");
                Console.WriteLine("1: Sten");
                Console.WriteLine("2: Sax");
                Console.WriteLine("3: Påse");
                Console.Write("Ditt val (1-3): \n");

                string input = Console.ReadLine() ?? "";

                // Validera inmatningen
                if (int.TryParse(input, out int choiceNumber) && choiceNumber >= 1 && choiceNumber <= 3)
                {
                    Choice playerChoice = (Choice)choiceNumber;
                    Choice computerChoice = (Choice)random.Next(1, 4); // Slumpar 1, 2 eller 3

                    PrintChoiceAscii(human.Name, playerChoice);        // Anropar metoden för att skriva ut ASCII-konst för spelarens val
                    PrintChoiceAscii(computer.Name, computerChoice);

                    // Kör vinstlogiken från Game-klassen
                    string result = game.DetermineWinner(playerChoice, computerChoice);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Ogiltigt val! Ange en siffra mellan 1 och 3.");
                }
            }

            // När loopen avslutas har någon vunnit matchen
            Console.WriteLine("\n=================================");
            if (human.Score >= maxScore)
            {
                Console.WriteLine($"GRATTIS! {human.Name} vann hela matchen!\n");
                stats.HumanWins++;          // ökar spelarens vinster

                if (stats.HumanWins == 3)   // Påskägg för tre vinster
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
            else
            {
                Console.WriteLine($"DATORN VANN MATCHEN! Bättre lycka nästa gång.\n");
                stats.ComputerWins++; // ökar datorns vinster
            }

            Console.WriteLine("=================================");

            // spara uppdaterad statistik till fil
            string updatedJson = JsonSerializer.Serialize(stats);
            File.WriteAllText(filePath, updatedJson);
            Console.WriteLine($"Statistik sparad till: {Path.GetFullPath(filePath)}");
        }
        static void PrintChoiceAscii(string playerName, Choice choice)
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