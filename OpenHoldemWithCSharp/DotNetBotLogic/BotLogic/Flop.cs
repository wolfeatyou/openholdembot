using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Classes;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.BotLogic
{
    class Flop
    {
        /// <summary>
        /// ##f$flop_raise##
        /// </summary>
        /// <returns></returns>
        public static bool Raise()
        {
            if (!OH.didswag && !OH.didchec) // 1st round of bets
            {
                if (Gecko.IRaisedOnRoundBefore)
                    return flop_raise_initiative();

                if (Gecko.OpponentRaisedOnRoundBefore)
                    return flop_RaiseVillainInitiative();

                if (Gecko.NoBetOnRoundBefore)
                    return flop_raise_noaction();
            }
            else  // uno mi ha reraisato
            {
               return RaiseTheReraise();
            }
            return false;
        }        

        /// <summary>
        /// ##f$flop_call##
        /// </summary>
        public static bool Call()
        {
            if (Odds.Odds_and_Outs()) 
                return true;

            if (!OH.didswag) // 1o round of bet
            {
                if (Gecko.IRaisedOnRoundBefore)
                    return flop_call_initiative();

                if (Gecko.OpponentRaisedOnRoundBefore)
                    return flop_call_noinitiative();

                if (Gecko.NoBetOnRoundBefore)
                    return flop_call_noaction();
            }
            else // got reraised
            {
                return CallTheReraise();
            }
            return false;
        }        

        /// <summary>
        /// ##f$flop_raise_initiative##
        /// </summary>
        private static bool flop_raise_initiative()
        {
            if (OH.OutOfPosition)
            { 
                if (Gecko.NoBetsOrMinRaise)
                {      
                    if (OH.nplayersplaying <= 3) //cbet 1v1 or 1v2
                    {                        
                        return true;
                    }
                    if (OH.nplayersplaying >= 4)
                    {                        
                        if (OH.PrWin > 0.65)
                            return true;
                    }

                    return false;
                }
                
                if (Gecko.StandardRaise)
                    return OH.PrWin > 0.75;

                if (Gecko.Overbet)
                    return OH.PrWin > 0.76;

                if (Gecko.BigOverbet)
                    return OH.PrWin > 0.77;
            }


            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                  

                    if (OH.nplayersplaying <= 3)
                        return true;
                    
                    if (OH.nplayersplaying >= 4)
                        return OH.PrWin > 0.65;
                    return false;
                }
                
                if (Gecko.StandardRaise)
                {
                    if (OH.nplayersplaying == 2)
                        return OH.PrWin > .75;
                    if (OH.nplayersplaying >= 3)
                        return OH.PrWin > .78;
                    return false;
                }
                if (Gecko.Overbet)
                    return OH.PrWin > .78;
                if (Gecko.BigOverbet)
                    return OH.PrWin > .78;
            }

            if (OH.InPosition)
            {  
                if (Gecko.NoBetsOrMinRaise)
                {                

                    if (OH.nplayersplaying <= 3)                    
                        return true;
                    
                    if (OH.nplayersplaying >= 4) return OH.PrWin > 0.65;
                    return false;
                }                

                if (Gecko.StandardRaise)
                {
                    if (OH.nplayersplaying == 2)
                        return OH.PrWin > .70;

                    if (OH.nplayersplaying >= 3)
                        return OH.PrWin > .70;
                    return false;
                }
                if (Gecko.Overbet)
                    return OH.PrWin > .80;
                if (Gecko.BigOverbet)
                    return OH.PrWin > .80;
            }
            return false;
        }        

        /// <summary>
        /// ##f$flop_raise_noinitiative##
        /// </summary>
        /// <returns></returns>
        private static bool flop_RaiseVillainInitiative()
        {
            /* Villain raised, i called */ 
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise && OH.nplayersplaying == 2) // donk 
                {
                    /* never donk*/
                    return false;
                }

                if (Gecko.StandardRaise)  // raise cbet
                {
                    if (OH.nplayersplaying == 2)
                    {
                        if (OH.nplayersround2 == 2) return OH.PrWin > 0.70;
                        if (OH.nplayersround2 >= 3) return OH.PrWin > 0.75; return false;
                    }

                    if (OH.nplayersplaying >= 3)
                    {
                        if (OH.nplayersround2 == 3) return OH.PrWin > 0.75;
                        if (OH.nplayersround2 >= 4) return OH.PrWin > 0.80; return false;
                    }
                    return false;
                }

                if (Gecko.Overbet)
                {
                    if (Gecko.Draws() > 1 && OH.nplayersplaying == 2) return true;
                    if (OH.PrWin > 0.75 && OH.PrWin < 0.95) return true;
                    return false;
                }

                if (Gecko.BigOverbet)
                    return OH.PrWin > .80;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) // donk   
                    /* never donk */
                    return false;   
      
                if (Gecko.StandardRaise) return OH.PrWin > .78; // raise cbet
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .80;
            }


            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise)    // donk
                {
                    /* never donk */
                    return false;
                }

                if (Gecko.StandardRaise)  // raise cbet
                {
                    if (Gecko.Draws() > 1) return true;
                    if (OH.PrWin > 0.80) return true; return false;
                }

                if (Gecko.Overbet)
                {
                    if (Gecko.Draws() > 3) return true;
                    if (OH.PrWin > 0.80 ) return true; return false;
                }
                if (Gecko.BigOverbet) 
                    return OH.PrWin > .80;
            }
            return false;
        }


        /// <summary>
        /// ##f$flop_raise_noaction##
        /// </summary>
        /// <returns></returns>
        private static bool flop_raise_noaction()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) //donk oop
                {                  
                    return OH.PrWin > .70;
                }
                if (Gecko.StandardRaise) return OH.PrWin > .75;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .92;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) //donk oop 
                {                   
                    return OH.PrWin > .70;
                }
                if (Gecko.StandardRaise) return OH.PrWin > .75;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .92;
            }


            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {                    
                    return OH.PrWin > .55; 
                }
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .92;
            }

            return false;
        }        

        ///##f$flop_call_initiative##
        /// Call donks
        public static bool flop_call_initiative()
        {          
            if (OH.OutOfPosition)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .60;
                if (Gecko.Overbet) return OH.PrWin > .65;
                if (Gecko.BigOverbet) return OH.PrWin > .70;
            }

            if (OH.OopMiddle)
            {

                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .60;
                if (Gecko.Overbet) return OH.PrWin > .80;
                if (Gecko.BigOverbet) return OH.PrWin > .80;
            }

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .60;
                if (Gecko.Overbet) return OH.PrWin > .80;   // su bet grosse al flop chiama i nuts
                if (Gecko.BigOverbet) return OH.PrWin > .80;
            }

            return false;
        }

        ///##f$flop_call_noaction##
        /// Call su piatti dove non ho raisato preflop
        public static bool flop_call_noaction()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .70;
                if (Gecko.BigOverbet) return OH.PrWin > .75;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .70;
                if (Gecko.BigOverbet) return OH.PrWin > .70;
            }

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .70;
                if (Gecko.BigOverbet) return OH.PrWin > .80;
            }

            return false;
        }


        ///##f$flop_call_noinitiative##
        /// Call quando villain raisa preflop
        public static bool flop_call_noinitiative()
        {
            // Early position
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .75;
                if (Gecko.BigOverbet) return OH.PrWin > .80;
            }
            // Middle position
            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .75;
                if (Gecko.BigOverbet) return OH.PrWin > .75;
            }

            // Late position
            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .15;
                if (Gecko.StandardRaise) return OH.PrWin > .70;
                if (Gecko.Overbet) return OH.PrWin > .75;
                if (Gecko.BigOverbet) return OH.PrWin > .75;
            }
            return false;
        }

        private static bool RaiseTheReraise()
        { 
            if (OH.PrWin > 0.80)
                return true;           
            
            return false;
        }

        private static bool CallTheReraise()
        {
            /* TODO */
            return false;
        }      
    }
}
