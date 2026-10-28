using System.Text.Json;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Visa välkomstskärmen
            GameUI.PrintWelcomeScreen();

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

            bool playAgain;

            // Yttre loop för hela matcher
            do
            {
                // Nollställ poängen inför varje ny match
                human.Score = 0;
                computer.Score = 0;

                // Inre loop som körs tills någon når maxScore
                while (human.Score < maxScore && computer.Score < maxScore)
                {
                    Console.Clear(); // Rensa konsol för varje nytt drag

                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine($"   --- Poäng: {human.Name}: {human.Score} | {computer.Name}: {computer.Score} ---");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    Console.WriteLine("Välj ditt drag:");
                    Console.WriteLine("1: Sten");
                    Console.WriteLine("2: Sax");
                    Console.WriteLine("3: Påse");
                    Console.Write("Ditt val (1-3): ");

                    string input = Console.ReadLine() ?? "";

                    // Inmatningen valideras och konverteras till Choice enum
                    if (int.TryParse(input, out int choiceNumber) && choiceNumber >= 1 && choiceNumber <= 3)
                    {
                        Choice playerChoice = (Choice)choiceNumber;        // Choice från Player.cs 
                        Choice computerChoice = (Choice)random.Next(1, 4); // Slumpar 1, 2 eller 3

                        Console.WriteLine("\nSten...");
                        Thread.Sleep(400);                                 // Pausar i 400 millisekunder 
                        Console.WriteLine("Sax...");
                        Thread.Sleep(400);
                        Console.WriteLine("Påse..!\n");
                        Thread.Sleep(400);

                        // sparar poängen innan resultatet skrivs ut för att den yttre loopen ska kunna avgöra vem som vann matchen
                        int previousHumanScore = human.Score;
                        int previousComputerScore = computer.Score;

                        string result = game.DetermineWinner(playerChoice, computerChoice);

                        GameUI.PrintChoiceAscii(human.Name, playerChoice);        // Anropar metoden för att skriva ut ASCII-konst för spelarens val
                        GameUI.PrintChoiceAscii(computer.Name, computerChoice);

                        bool humanWon = human.Score > previousHumanScore;
                        bool computerWon = computer.Score > previousComputerScore;

                        // Skickar text och resultat till UI-klassen
                        GameUI.PrintRoundResult(result, humanWon, computerWon);

                        Console.WriteLine("\nTryck på valfri tangent för att fortsätta..."); // En paus här för att spelaren ska hinna se resultat innan Clear()
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt val! Ange en siffra mellan 1 och 3.");
                        Console.WriteLine("Tryck på valfri tangent för att försöka igen...");
                        Console.ReadKey();
                    }
                }

                // Slutet av matchen, visa resultat och uppdatera statistik
                Console.Clear();
                Console.WriteLine("\n=================================");
                if (human.Score >= maxScore)
                {
                    Console.WriteLine($"GRATTIS! {human.Name} vann hela matchen!\n");
                    stats.HumanWins++;          // ökar spelarens vinster

                    if (stats.HumanWins == 3)   // Easter egg för tre vinster
                    {
                        GameUI.PrintEasterEgg();
                    }
                }
                else
                {
                    Console.WriteLine($"\nDATORN VANN MATCHEN! Bättre lycka nästa gång.\n");
                    stats.ComputerWins++; // ökar datorns vinster
                }

                Console.WriteLine("=================================");

                // spara uppdaterad statistik till fil
                string updatedJson = JsonSerializer.Serialize(stats);
                File.WriteAllText(filePath, updatedJson);
                Console.WriteLine($"\nStatistik sparad till: {Path.GetFullPath(filePath)}");

                // Fråga om en ny match
                Console.Write("\nVill du spela en ny match? (j/n): ");
                string playAgainInput = Console.ReadLine()?.Trim().ToLower() ?? "";
                playAgain = playAgainInput == "j" || playAgainInput == "ja";          // allt annat än ja eller j stänger av loopen

            } while (playAgain);

            Console.WriteLine("Bra spelat!");
        }
    }
}