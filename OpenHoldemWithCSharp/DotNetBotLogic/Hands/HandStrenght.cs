using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Enums;
using DotNetBotLogic.Helpers;

namespace DotNetBotLogic.Classes
{
    class HandStrenght
    {
        public static long ROYAL_FLUSH { get { return 2148457658; } }
        public static long STRAIGHT_FLUSH { get { return 1 << 31; } }
        public static long POKER { get { return 1 << 30; } }
        public static long FULL_HOUSE { get { return 1 << 29; } }
        public static long FLUSH { get { return 1 << 28; } }
        public static long THREE_OF_A_KIND { get { return 1 << 26; } }
        public static long TWO_PAIR { get { return 1 << 25; } }
        public static long PAIR { get { return 1 << 24; } }       

    }

}




