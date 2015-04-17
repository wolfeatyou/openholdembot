using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Classes;
using DotNetBotLogic.Helpers;

namespace DotNetBotLogic.BotLogic
{
    class Turn
    {
        /// <summary>
        ///  ##f$turn_raise##
        /// </summary>
        /// <returns></returns>
        public static bool Raise()
        {           
            if (!OH.didswag && !OH.didchec) // 1o giro di bet
            {
                if (Gecko.IRaisedOnRoundBefore)
                {
                    return turn_raise_initiative();
                }

                if (Gecko.OpponentRaisedOnRoundBefore)
                    return turn_raise_noinitiative();

                if (Gecko.NoBetOnRoundBefore)
                {                    
                    return turn_raise_noaction();
                }

                if (Gecko.IRaisedGotReraisedAndICalled)
                    return turn_raise_wegotraised();
            }
            else // uno mi ha reraisato
            {
                return RaiseTheReraise();
            }
            return false;
        }

        

        /// <summary>
        /// ##f$turn_call##
        /// </summary>
        public static bool Call()
        {
            if (Odds.Odds_and_Outs()) 
                return true;

            if (!OH.didswag)    // 1o giro di bet
            {
                if (Gecko.IRaisedOnRoundBefore)
                    return turn_call_initiative();

                if (Gecko.OpponentRaisedOnRoundBefore)
                    return turn_call_noinitiative();

                if (Gecko.NoBetOnRoundBefore)
                    return turn_call_noaction();

                if (Gecko.IRaisedGotReraisedAndICalled)
                    return turn_call_wegotraised();
            }
            else // uno mi ha reraisato
            {
                return CallTheReraise();
            }

            return false;
        }        

        /// <summary>
        /// ##f$turn_raise_initiative##
        /// </summary>
        /// <returns></returns>
        private static bool turn_raise_initiative()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) //2nd barrell
                {
                    if (OH.nplayersplaying == 2 && OH.nplayersround2 == 2)
                    {
                        if (OH.didcallround1 && OH.nbetsround1 > 1 && OH.nbetsround1 <= 5) return OH.PrWin > 0; // donkbet and 2nd barrel turn

                        if (Gecko.Draws() > 1) return true;
                        if (OH.PrWin > .70 && !(OH.ismidpair && OH.rankhicommon == 14 && OH.nrankedcommon == 1)) return true; return false;
                    }
                    if (OH.nplayersplaying >= 2 && OH.nplayersround2 >= 3 && !(OH.ismidpair && OH.rankhicommon == 14 && OH.nrankedcommon == 1))
                        return OH.PrWin > .74 ; 
                    
                    return false;
                }      
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .80; // check push allin
                if (Gecko.BigOverbet) return OH.PrWin > .90;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .72;                
                if (Gecko.StandardRaise) return OH.PrWin > .85;
                if (Gecko.Overbet) return OH.PrWin > .90;
                if (Gecko.BigOverbet) return OH.PrWin > .95;
            }

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    if (OH.nplayersplaying == 2 && OH.nplayersround2 == 2)
                    {

                        if (OH.PrWin >= 0.78)
                            return true;

                        return false;
                    }

                    // se c'e' stata action al flop e al turn tocca a me senza nessun raise
                    if (OH.nplayersplaying >= 2 && OH.nplayersround2 >= 3 && OH.ranklocommon < 10 && !(OH.ismidpair && OH.rankhicommon == 14 && OH.nrankedcommon == 1))
                        return OH.PrWin > 0.68;
                    return false;
                }                
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .85;
                if (Gecko.BigOverbet) return OH.PrWin > .95;
            }

            return false;
        }

        /// <summary>
        ///  ##f$turn_raise_noinitiative##
        /// </summary>
        /// <returns></returns>
        private static bool turn_raise_noinitiative()
        {
            if (OH.OutOfPosition)
            {
                if (OH.nplayersplaying == 2)
                {
                    if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .88; // check raise                    
                    if (Gecko.StandardRaise) return OH.PrWin > .78;
                    if (Gecko.Overbet) return OH.PrWin > .80;
                    if (Gecko.BigOverbet) return OH.PrWin > .95;
                }
                if (OH.nplayersplaying >= 3)
                {
                    if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .75 && OH.PrWin < .96;                    
                    if (Gecko.StandardRaise) return OH.PrWin > .96;
                    if (Gecko.Overbet) return OH.PrWin > .96;
                    if (Gecko.BigOverbet) return OH.PrWin > .96;
                }
            }
            if (OH.OopMiddle)
            {

                if (Gecko.NoBetsOrMinRaise)
                    return OH.PrWin > .80;
                if (Gecko.StandardRaise) 
                    return OH.PrWin > .86;
                if (Gecko.Overbet) 
                    return OH.PrWin > .90; 
                if (Gecko.BigOverbet) return OH.PrWin > .96;
            }
            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) 
                    return OH.PrWin > .80;
                if (Gecko.StandardRaise) 
                    return OH.PrWin > .80;
                if (Gecko.Overbet) 
                    return OH.PrWin > .90; 
                if (Gecko.BigOverbet) return OH.PrWin > .96;
            }
            return false;
        }

        /// <summary>
        /// ##f$turn_raise_noaction##
        /// </summary>
        /// <returns></returns>
        private static bool turn_raise_noaction()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    if (OH.nplayersplaying == 2)
                    {
                        if (Gecko.Draws() > 1) return true;
                        if (OH.PrWin > .64) return true; return false;
                    }
                    if (OH.nplayersplaying >= 3) return OH.PrWin > .64; return false;
                }              
                if (Gecko.StandardRaise) return OH.PrWin > .98;
                if (Gecko.Overbet) return OH.PrWin > .98;
                if (Gecko.BigOverbet) return OH.PrWin > .98;
            }
            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    if (Gecko.Draws() > 1) return true;
                    if (OH.PrWin > .62) return true; return false;
                }               
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .82;
                if (Gecko.BigOverbet) return OH.PrWin > .86;
            }
            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    //if (OH.nplayersround2 == 2) return OH.PrWin > 0;
                    if (OH.nplayersround2 >= 3)
                    {
                        if (Gecko.Draws() > 1) return true;
                        if (OH.PrWin > .60) return true; return false;
                    } return false;
                }                
                if (Gecko.StandardRaise) return OH.PrWin > .78;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .86;
            }
            return false;
        }

        /// <summary>
        /// ##f$turn_raise_wegotraised##
        /// </summary>
        /// <returns></returns>
        private static bool turn_raise_wegotraised()
        {
            if (OH.OutOfPosition)
            {
                if (OH.nplayersplaying == 2)
                {
                    if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .90;                   
                    if (Gecko.StandardRaise) return OH.PrWin > .95;
                    if (Gecko.Overbet) return OH.PrWin > .95;
                    if (Gecko.BigOverbet) return OH.PrWin > .95; return false;
                };

                if (OH.nplayersplaying >= 3)
                {
                    if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .90;                    
                    if (Gecko.StandardRaise) return OH.PrWin > .95;
                    if (Gecko.Overbet) return OH.PrWin > .95;
                    if (Gecko.BigOverbet) return OH.PrWin > .95; return false;
                }; return false;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .90;                
                if (Gecko.StandardRaise) return OH.PrWin > .96;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .90;                
                if (Gecko.StandardRaise) return OH.PrWin > .96;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }
            return false;
        }       


        /// <summary>
        ///  	##f$turn_call_wegotraised##
        /// </summary>
        /// <returns></returns>
        private static bool turn_call_wegotraised()
        {

            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .60;
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            }

            if (OH.OopMiddle)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .60;
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            }

            if (OH.InPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .60;
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            }

            return false;
        }

        ///##f$turn_call_initiative##
        public static bool turn_call_initiative()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .55;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .77;
                if (Gecko.BigOverbet) return OH.PrWin > .85;
            }
            if (OH.OopMiddle)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .60;
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .85;
            }
            if (OH.InPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .60;
                if (Gecko.StandardRaise) return OH.PrWin > .75;
                if (Gecko.Overbet) return OH.PrWin > .77;
                if (Gecko.BigOverbet) return OH.PrWin > .85;
            }
            return false;
        }

        ///##f$turn_call_noaction##
        public static bool turn_call_noaction()
        {
            if (OH.OutOfPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;
                if (Gecko.StandardRaise) return OH.PrWin > .75;
                if (Gecko.Overbet) return OH.PrWin > .78;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            };

            if (OH.OopMiddle)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .55;
                if (Gecko.StandardRaise) return OH.PrWin > .75;
                if (Gecko.Overbet) return OH.PrWin > .78;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            };

            if (OH.InPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .60;
                if (Gecko.StandardRaise) return OH.PrWin > .75;
                if (Gecko.Overbet) return OH.PrWin > .78;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            };

            return false;

        }

        ///##f$turn_call_noinitiative##
        public static bool turn_call_noinitiative()
        {

            if (OH.OutOfPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .70;
                if (Gecko.BigOverbet) return OH.PrWin > .80; return false;
            };

            if (OH.OopMiddle)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .55;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .70;
                if (Gecko.BigOverbet) return OH.PrWin > .80; return false;
            };

            if (OH.InPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .70;
                if (Gecko.BigOverbet) return OH.PrWin > .85; return false;
            };

            return false;
        }

        private static bool RaiseTheReraise()
        {
            /* TODO */
            return false;
        }

        private static bool CallTheReraise()
        {   
            /* TODO */
            return false;
        }
    
    
    }
}
