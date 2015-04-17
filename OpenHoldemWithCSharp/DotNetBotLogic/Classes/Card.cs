using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.Classes
{
    public enum Symbol { C = 1, D = 2, H = 3, S = 4 }

    class Card
    {
        public int Rank { get; set; }

        public Symbol Suit { get; set; }
    }
}
