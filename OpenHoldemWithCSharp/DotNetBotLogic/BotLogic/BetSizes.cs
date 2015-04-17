using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.BotLogic;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.Classes
{
    class BetSizes
    {       

        /// <summary>
        /// ##f$Betsize_Adjusted##
        /// </summary>
        /// <returns></returns>
        public static double Betsize_Adjusted()
        {
            double result = 0;
            
            if (OH.br == 1) //preflop
            {
                result = GetPreflopRaiseAmount();
                return result;
            }          


            if (OH.br == 2)
            {
                result = GetFlopRaiseAmount();
                return result;
            }

            
            if (OH.br == 3)
            {
                if (OH.call <= 0)
                {
                     return OH.pot * .7;                    
                }
                if (OH.call > 0 && OH.call <= 2 * OH.bblind)
                {
                    if (OH.pot <= 25 * OH.bblind) 
                        return OH.pot * 0.75;

                    if (OH.pot > 25 * OH.bblind) 
                        return OH.pot * .65;
                }

                if (OH.call > 2 * OH.bblind)
                {
                    if (BetLine()) return OH.balance;
                    if (!BetLine())
                    {
                        if (OH.call <= OH.potcommon * .33) return OH.pot * 0.7;
                        if (OH.call > OH.potcommon * .33) return Currentbet_Raischair() * 3;
                    }
                }
            }

            /* River */
            if (OH.br == 4)
            {
                if (OH.call <= 0)
                {
                    if (OH.pot <= 10 * OH.bblind) return OH.pot * .8;
                    if (OH.pot > 10 * OH.bblind && OH.pot <= 80 * OH.bblind) return OH.pot * .6;
                    if (OH.pot > 80 * OH.bblind)
                    {
                        if (OH.PrWin <= 0.8) return OH.pot * .25;
                        if (OH.PrWin > 0.80) return OH.pot * .60;
                    }
                }

                if (OH.call > 0 && OH.call <= 2 * OH.bblind) return OH.pot;
                if (OH.call > 2 * OH.bblind)
                {
                    if (OH.call <= OH.potcommon * .33) return OH.pot;
                    if (OH.call > OH.potcommon * .33) return Currentbet_Raischair() * 3.5;
                }
            }

            // ---------------------------

            if (OH.nplayersplaying >= 3)
            {
                if (OH.br == 2)
                {
                    if (OH.call <= OH.bblind)
                    {
                        if (OH.nbetsround1 <= 6) return OH.pot * .62;
                        if (OH.nbetsround1 > 6) return OH.pot * .60;
                    }

                    if (OH.call > OH.bblind)
                    {
                        if (!OH.didswag)
                        {
                            if (OH.call <= OH.potcommon * .33) return OH.pot * 0.8;
                            if (OH.call > OH.potcommon * .33) return Currentbet_Raischair() * 4;
                        }

                        if (OH.didswag)
                        {
                            if (OH.nbetsround1 <= 1) return Currentbet_Raischair() * 4;
                            if (OH.nbetsround1 > 1) return OH.balance;
                        }
                    }
                }


                if (OH.br == 3)
                {
                    if (OH.call <= OH.bblind)
                    {
                        if (OH.pot <= 25 * OH.bblind) return OH.pot * .70;
                        if (OH.pot > 25 * OH.bblind) return OH.pot * .60;
                    }

                    if (OH.call > OH.bblind)
                    {
                        if (OH.call <= OH.potcommon * .33) return OH.pot;
                        if (OH.call > OH.potcommon * .33) return Currentbet_Raischair() * 4;
                    }
                }

                if (OH.br == 4)
                {
                    if (OH.call <= OH.bblind)
                    {
                        if (OH.pot <= 10 * OH.bblind) return OH.pot;
                        if (OH.pot > 10 * OH.bblind) return OH.pot * .60;
                    }

                    if (OH.call > OH.bblind)
                    {
                        if (OH.call <= OH.potcommon * .33) return OH.pot;
                        if (OH.call > OH.potcommon * .33) return Currentbet_Raischair() * 4;
                    }
                }
            }

            return OH.pot*0.7;
        }

        private static double GetFlopRaiseAmount()
        {
            if (OH.call <= OH.bblind)
            {
                if (OH.nbetsround1 <= 6) return OH.pot * .75;
                if (OH.nbetsround1 > 6) return OH.pot * .55;
                return OH.pot * 0.7;
            }

            else
            {
                if (!OH.didswag)
                {
                    if (OH.call <= OH.potcommon * .33) return OH.pot;
                    if (OH.call > OH.potcommon * .33) return Currentbet_Raischair() * 2.8;
                }

                if (OH.didswag)
                {
                    return Math.Round(Currentbet_Raischair() * 2.8, 1);
                }

                return OH.pot * 0.7;
            }
        }

        private static double GetPreflopRaiseAmount()
        {
            double result = 0;
            if (OH.ncallbets <= 1)
            {

                switch (Preflop.GetPosition())
                {
                    case Position.UTG:
                    case Position.UTG_1:
                        result = 3.5 * OH.bblind + (OH.nopponentscalling * OH.sblind);
                        break;

                    case Position.CO:
                        result = 3 * OH.bblind + (OH.nopponentscalling * OH.sblind);
                        break;

                    case Position.BTN:
                        result = 2.5 * OH.bblind + (OH.nopponentscalling * OH.sblind);
                        break;
                    case Position.SB:
                        result = 3 * OH.bblind + (OH.nopponentscalling * OH.sblind);
                        break;

                    case Position.BB:
                        result = 3 * OH.bblind + (OH.nopponentscalling * OH.sblind);
                        break;
                }

                if (OH.MyHand.RangeNutsPreflop())  /* AA KK QQ AK aumenta la bet di 1 sb */
                    result += OH.sblind;

                return result;
            }
            else
            {                
                return BetSizes.Currentbet_Raischair() * 3.5;
            }
        }

        /// <summary>
        /// ##f$currentbet_raischair##
        /// </summary>
        /// <returns></returns>
        public static double Currentbet_Raischair()
        {

            if (OH.raischair == 0) return OH.currentbet0;
            if (OH.raischair == 1) return OH.currentbet1;
            if (OH.raischair == 2) return OH.currentbet2;
            if (OH.raischair == 3) return OH.currentbet3;
            if (OH.raischair == 4) return OH.currentbet4;
            if (OH.raischair == 5) return OH.currentbet5;
            return 0;
        }

        /// <summary>
        ///  ##f$betline##
        /// </summary>
        /// <returns></returns>
        public static bool BetLine()
        {
            return (!OH.didcallround1 && OH.nbetsround1 > 1 && OH.nbetsround1 <= 7 && // raise preflop
                      OH.didswaground2 && !OH.didcallround2 && OH.nbetsround2 > 0 && OH.nbetsround2 < 12 && // cbet flop
                      OH.OutOfPosition && !OH.didswag); // early position, did not act yet
        }       

        /// <summary>
        ///  ##f$balance##
        /// </summary>
        /// <returns></returns>
        public static double balance()
        {
            return (OH.balance + OH.bblind);
        }

        /// <summary>
        ///  ##f$stackraiser##
        /// </summary>
        /// <returns></returns>
        public static double GetRaiserStack()
        {
            if (OH.raischair == 0) return OH.balance0;
            if (OH.raischair == 1) return OH.balance1;
            if (OH.raischair == 2) return OH.balance2;
            if (OH.raischair == 3) return OH.balance3;
            if (OH.raischair == 4) return OH.balance4;
            if (OH.raischair == 5) return OH.balance5;
            return 0;
        }

        public static double GetMaxStackOfPlayingOpponents()
        {
            int OpponentplayingBits = OH.opponentsplayingbits;
            double maxStack = 0;

            int i = 0;
            while (OpponentplayingBits != 0)
            {
                if ((OpponentplayingBits & 1) == 1)
                {
                    if (i == 1)
                        maxStack = OH.balance1;
                    if (i == 2 && OH.balance2 > maxStack)
                        maxStack = OH.balance2;
                    if (i == 3 && OH.balance3 > maxStack)
                        maxStack = OH.balance3;
                    if (i == 4 && OH.balance4 > maxStack)
                        maxStack = OH.balance4;
                    if (i == 5 && OH.balance5 > maxStack)
                        maxStack = OH.balance5;
                    
                }
                OpponentplayingBits = OpponentplayingBits >> 1;
                i++;
            }
            return maxStack;
        }
    }
}
