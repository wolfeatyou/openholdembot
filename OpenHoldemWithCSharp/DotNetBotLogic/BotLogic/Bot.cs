using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Classes;

namespace DotNetBotLogic.BotLogic
{
    class Bot
    {
        public static double GetRaiseAction()
        {
            /* Protezione da misscraping su balance */
            if (OH.balance == 0)
                return 0;

            if (OH.br == 1)
            {
                if (Preflop.Raise())
                    return 1;

                return 0;
            }

            if (OH.br == 2)
            {
                if (Flop.Raise())
                    return 1;
                return 0;
            }
            if (OH.br == 3)
            {
                if (Turn.Raise())
                    return 1;
                return 0;
            }
            if (OH.br == 4)
            {
                if (River.Raise())
                    return 1;
                return 0;
            }
            return 0;
        }

        public static double getCallAction()
        {
            /* Protezione da misscraping su balance */
            if (OH.balance == 0)
                return 0;

            /* Basic calls with nuts or free to check    */
            if (GetRaiseAction() > 0) return 1; // this calls all in
            if (OH.prwin >= 0.98) return 1;
            if (OH.call <= 0.001) return 1;
            if (OH.br > 1 && OH.call <= OH.bblind && OH.nbetsround1 > 1 && OH.PrWin > .15) return 1;
            if (OH.br > 1 && OH.call <= OH.bblind && OH.pot > 6 * OH.bblind && OH.PrWin > .50) return 1;
            if (OH.br > 1 && Gecko.Committed()) return 1;


            if (OH.br == 1)
                return Preflop.Call();

            if (OH.br == 2)
            {
                if (Flop.Call())
                    return 1;
                else
                    return 0;
            }
            if (OH.br == 3)
            {
                if (Turn.Call())
                    return 1;
                return 0;
            }
            if (OH.br == 4)
            {
                if (River.Call())
                    return 1;
                else
                    return 0;
            }

            return 0;
        }

        public static double GetRaiseAmount()
        {
            double betsizeAdjusted = BetSizes.Betsize_Adjusted();
            double balance = BetSizes.balance();


            if (OH.call <= 0) // 1st to raise: no one called before me
            {
                if (betsizeAdjusted >= OH.balance && OH.balance > 0)
                    return balance;

                if (betsizeAdjusted < OH.balance)
                    return betsizeAdjusted;

                return 0;
            }
            if (OH.call > 0) // limpers or raisers
            {
                double stackRaiser = BetSizes.GetRaiserStack();
                if (OH.nopponentsplaying > 1 && stackRaiser == 0)
                {
                    stackRaiser = BetSizes.GetMaxStackOfPlayingOpponents();
                }

                if (stackRaiser > 0 && !(stackRaiser >= (BetSizes.Currentbet_Raischair() - OH.bblind) && stackRaiser <= BetSizes.Currentbet_Raischair()) && betsizeAdjusted >= OH.balance * 0.75)
                    return OH.call + stackRaiser;

                if (stackRaiser > 0 && !(stackRaiser >= (BetSizes.Currentbet_Raischair() - OH.bblind) && stackRaiser <= BetSizes.Currentbet_Raischair()) && betsizeAdjusted < OH.balance * 0.75)
                    return betsizeAdjusted;

                return 0;
            }

            return 0;
        }
    }
}
