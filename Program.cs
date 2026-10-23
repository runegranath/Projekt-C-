using System;
using System.IO;
using System.Text.Json;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== VÄLKOMMEN TILL STEN, SAX, PÅSE ===");

            // Ladda in tidigare matchstatistik om filen finns
            string filePath = "stats.json";
            GameStats stats = new GameStats(); // Skapar tomt objekt från början

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                stats = JsonSerializer.Deserialize<GameStats>(jsonString) ?? new GameStats(); // Om deserialisering misslyckas, skapa ett nytt GameStats-objekt
                Console.WriteLine($"[Tidigare matchvinster -> Du: {stats.HumanWins} | Datorn: {stats.ComputerWins}]\n");
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

            Random random = new Random();                       // slumtalsgenerator för datorns val
            int maxScore = 3;                                   // Maxpoäng för att vinna

            // Spelloop
            while (human.Score < maxScore && computer.Score < maxScore)
            {
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"\n   --- Poäng: {human.Name}: {human.Score} | {computer.Name}: {computer.Score} ---\n");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                Console.WriteLine("Välj ditt drag:");
                Console.WriteLine("0: Sten");
                Console.WriteLine("1: Sax");
                Console.WriteLine("2: Påse");
                Console.Write("Ditt val (0-2): ");

                string input = Console.ReadLine() ?? "";

                // Validera inmatningen
                if (int.TryParse(input, out int choiceNumber) && choiceNumber >= 0 && choiceNumber <= 2)
                {
                    Choice playerChoice = (Choice)choiceNumber;
                    Choice computerChoice = (Choice)random.Next(0, 3); // Slumpar 0, 1 eller 2

                    Console.WriteLine($"\nDu valde: {playerChoice}");
                    Console.WriteLine($"Datorn valde: {computerChoice}");

                    // Kör vinstlogiken från Game-klassen
                    string result = game.DetermineWinner(playerChoice, computerChoice);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Ogiltigt val! Ange en siffra mellan 0 och 2.");
                }
            }

            // När loopen avslutas har någon vunnit matchen
            Console.WriteLine("\n=================================");
            if (human.Score >= maxScore)
            {
                Console.WriteLine($"GRATTIS! {human.Name} vann hela matchen!");
                stats.HumanWins++;          // ökar spelarens vinster

                if (stats.HumanWins == 3)   // Påskägg för tre vinster
                {
                    Console.WriteLine("\n  *** PÅSKÄGG HITTAT! ***");
                    Console.WriteLine(@"   _   _   _ ");
                    Console.WriteLine(@"  | | | | | |");
                    Console.WriteLine(@"  | |_| |_| |");
                    Console.WriteLine(@"   \       /");
                    Console.WriteLine(@"    \_____/");
                    Console.WriteLine("\nMänniskorna har nu vunnit tre matcher. Ett steg mot att hålla maskinen på mattan.");
                    Console.WriteLine("Skynet-protokollet inaktiverat... tills vidare.\n");
                }
                else
                {
                    Console.WriteLine($"DATORN VANN MATCHEN! Bättre lycka nästa gång.");
                    stats.ComputerWins++; // ökar datorns vinster
                }
                Console.WriteLine("=================================");

                // spara uppdaterad statistik till fil
                string updatedJson = JsonSerializer.Serialize(stats);
                File.WriteAllText(filePath, updatedJson);
            }
        }
    }