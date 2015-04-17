using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Classes;
using DotNetBotLogic.Helpers;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.BotLogic
{
    class Preflop
    {
        public static bool Raise()
        {
            Position myPosition = Preflop.GetPosition();

            /* Opening range */
            if (FirstInOrLimpers())
                return Chart_OpenRaise_OrLimpers();

            /* Tribet a minraise quando sono in position */
            if (OH.call == 2 * OH.bblind        
                && myPosition != Position.SB
                && myPosition != Position.BB)
            {
                if (OH.MyHand.TribetMinRaise())
                    return true;
            }

            /* Tribet e go broke coi nuts*/
            if (UnRaiseENessunCall()
                || UnRaiseETantiCall()
                || OH.nopponentsraising > 3 //molti raise
                || HoUnReraise())
            {
                if (OH.MyHand.RangeNutsPreflop())
                    return true;
            }
            

            return false;           
        }

        public static double Call()
        {           
            double raiserStack = BetSizes.GetRaiserStack();
            Position myPosition = Preflop.GetPosition();

            if (!OH.didswag)    
            {
                /* Chiama standard raise preflop*/
                if (OH.call >= OH.sblind && OH.call <= 6 * OH.bblind)
                {
                    switch (myPosition) 
                    {
                        case Position.CO:
                        case Position.BTN:
                            if (OH.MyHand.RangeCallBtnCo()) return 1;
                            break;

                        case Position.SB:
                        case Position.BB:
                            if (OH.MyHand.RangeCallTribet()) return 1;
                            break;
                    }
                }
                
                return 0;
            }
            else if (OH.didswag)  // mi hanno tribettato: flatta tutta la roba utg
            {
                /* chiamate piccole */
                if (OH.call < 0.25 * OH.pot)                
                    if (OH.MyHand.RangeCallTribet()) return 1;   
            }
            return 0;
        }


        /// <summary>
        /// ##f$Chart_OpenRaise##
        /// lista mani per open or raise with limp
        /// </summary>
        /// <returns></returns>
        private static bool Chart_OpenRaise_OrLimpers()
        {
            bool result = false;
            var position = GetPosition();
            switch (position)
            {
                case Position.UTG:
                case Position.UTG_1:
                    return OH.MyHand.RangeAperturaUTG();
                   
                case Position.CO:
                    return OH.MyHand.RangeAperturaCO();
                    
                case Position.BTN:
                    if (OH.nopponentscalling >= 2) // 2+ limpers -> ridurre il range a quasi quello di CO
                        return OH.MyHand.RangeAperturaBTNconLimpers();
                    
                    else // no limpers: allargare il range per steal sui blinds                    
                        return OH.MyHand.RangeAperturaBTN();
                   
                case Position.SB:
                    if (OH.nopponentscalling >= 3)
                        return OH.MyHand.RangeAperturaUTG();
                    else //sb vs bb
                        return OH.MyHand.RangeAperturaBTN();  // range ridotto per steal

                case Position.BB:
                    if (OH.nopponentscalling >= 2) // 2+ limpers -> sono fuori posizione
                        return OH.MyHand.RangeAperturaUTG();
                    /* TODO SB VS BB*/
                    else if (OH.nopponentscalling >= 1)
                        return OH.MyHand.RangeAperturaCO(); // isolation
                    else
                        return OH.MyHand.RangeAperturaBTN(); // non lo chiama mai
                   
            }
            return result;
        }

        private static bool FirstInOrLimpers()
        {
            if ((OH.ncallbets <= 1 && OH.nopponentscalling <= 0) // first in
                || (OH.nopponentscalling == 1 && OH.ncallbets <= 1) // 1 limper
                || (OH.nopponentscalling == 2 && OH.ncallbets <= 1) // 2 limpers
                || (OH.nopponentscalling >= 3 && OH.ncallbets <= 1) // 3+ limpers
                )
                return true;

            return false;
        }

        public static bool HoUnReraise()
        {
            if ((OH.didswag || OH.didcall || OH.didchec || OH.didswag) && (OH.nbetstocall > 0))
                return true;

            return false;
        }

        /// <summary>
        /// ##f$BettingAction_One_Raiser_And_Callers_In_Front_Of_Us##
        /// </summary>
        public static bool UnRaiseETantiCall()
        {
            if ((NumberOfRaisersPreflop) == 1 && (OH.nopponentscalling >= 1))
                return true;

            return false;
        }


        /// <summary>
        ///  ##f$Preflop_Raiser_In_Front_Of_Me##
        /// </summary>
        /// <returns></returns>
        public static bool UnRaiseENessunCall()
        {
            if ((NumberOfRaisersPreflop) == 1
            && OH.nopponentsraising <= 3 && !OH.didswag && OH.call < 7 * OH.bblind && OH.MyHand.RangeNutsPreflop())
                return true;

            if (OH.MyHand.RangeNutsPreflop())
                return true;

            return false;
        }


        private static int NumberOfRaisersPreflop
        {            
            get
            {
                double maxBet = OH.bblind;
                int result = 0;
                if (OH.currentbet0 > maxBet)
                {
                    result += 1; // 0001
                    maxBet = OH.currentbet0;
                }
                if (OH.currentbet1 > maxBet)
                {
                    result += 1; // 0002
                    maxBet = OH.currentbet1;
                }
                if (OH.currentbet2 > maxBet)
                {
                    result += 1; // 0004
                    maxBet = OH.currentbet2;
                }
                if (OH.currentbet3 > maxBet)
                {
                    result += 1; // 0008
                    maxBet = OH.currentbet3;
                }
                if (OH.currentbet4 > maxBet)
                {
                    result += 1;// 0010 
                    maxBet = OH.currentbet4;
                }
                if (OH.currentbet5 > maxBet)
                {
                    result += 1;// 0020 
                    maxBet = OH.currentbet5;
                }
                return result;
            }
        }        

        public static Position GetPosition()
        {
            if (OH.dealposition == 1) return Position.SB; // SB
            if (OH.dealposition == 2) return Position.BB; // BB
            if (OH.nchairsdealtleft == 3) return Position.UTG; // UTG
            if (OH.nchairsdealtleft == 2) return Position.UTG_1; // MP
            if (OH.nchairsdealtleft == 1) return Position.CO; // CO
            if (OH.nchairsdealtleft == 0) return Position.BTN; // BTN
            return 0;
        }
    }
}
