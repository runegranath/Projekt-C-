using System;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== VÄLKOMMEN TILL STEN, SAX, PÅSE ===");
            Console.WriteLine();

            // Skapa spelare och spel
            Player human = new Player { Name = "Spelare" };
            Player computer = new Player { Name = "Datorn" };
            Game game = new Game(human, computer);

            Random random = new Random();
            int maxScore = 3; // Maxpoäng för att vinna

            // Spelloop
            while (human.Score < maxScore && computer.Score < maxScore)
            {
                Console.WriteLine($"\n--- Poäng: {human.Name}: {human.Score} | {computer.Name}: {computer.Score} ---");
                Console.WriteLine("Välj ditt drag:");
                Console.WriteLine("0: Sten");
                Console.WriteLine("1: Sax");
                Console.WriteLine("2: Påse");
                Console.Write("Ditt val (0-2): ");

                string input = Console.ReadLine() ?? ""; // Hanterar null-värden med att sätta input till en tom sträng
                
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
            }
            else
            {
                Console.WriteLine($"DATORN VANN MATCHEN! Bättre lycka nästa gång.");
            }
            Console.WriteLine("=================================");
        }
    }
}