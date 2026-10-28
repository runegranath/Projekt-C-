using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt
{
    // enum som definerar konstanterna för val, sätts automatiskt till 1, 2, 3 för Sten, Sax, Påse istället för att börja på 0
    public enum Choice
    {
        Sten = 1,
        Sax = 2,
        Påse = 3
    }

    // spelarklass
    public class Player
    {
        public string Name { get; set; } = ""; // namn, sätts automatiskt till tom sträng
        public int Score { get; set; } = 0;    // börjar på 0
    }
}
