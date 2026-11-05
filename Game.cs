using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt
{
    public class Game
    {
        public Player Human { get; set; }
        public Player Computer { get; set; }

        // Konstruktor för att initiera spelare som tar två Player-objekt som argument
        public Game(Player human, Player computer) 
        {
            Human = human;
            Computer = computer;
        }

        // Metod för att avgöra vinnaren
        public string DetermineWinner(Choice playerChoice, Choice computerChoice)
        {
            // Oavgjort kontrolleras allra först
            if (playerChoice == computerChoice)
            {
                return "Oavgjort!";
            }

            // logik för att avgöra vinnaren, antingen vinner spelaren och alla andra fall är att datorn vinner
            bool playerWins = (playerChoice == Choice.Sten && computerChoice == Choice.Sax) ||
                              (playerChoice == Choice.Sax && computerChoice == Choice.Påse) ||
                              (playerChoice == Choice.Påse && computerChoice == Choice.Sten);

            if (playerWins)
            {
                Human.Score++;                  // Ökar spelarens poäng med 1
                return "Du vann omgången!";
            }
            else
            {
                Computer.Score++;               // Ökar datorns poäng med 1
                return "Datorn vann omgången!";
            }
        }
    }
}