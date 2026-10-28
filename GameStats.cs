using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt
{
    // Statistikklass med egenskaper för mänskliga och datorns vinster
    public class GameStats
    {
        public int HumanWins { get; set; } = 0;
        public int ComputerWins { get; set; } = 0;
    }
}