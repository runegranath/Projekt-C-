using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt
{
    // enum för val
    public enum Choice
    {
        Sten,
        Sax,
        Påse
    }

    // spelarklass
    public class Player
    {
        public string Name { get; set; } = ""; // namn, sätts automatiskt till tom sträng
        public int Score { get; set; } = 0;    // börjar på 0
    }
}
