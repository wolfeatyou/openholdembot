using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetBotLogic.Helpers
{
    class BitHelpers
    {
        /// <summary>
        /// sostituisce ' in openholdem
        /// http://www.dotnetperls.com/bitcount
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int BitCount(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count++;
                n &= (n - 1);
            }
            return count;
        }
    }
}
