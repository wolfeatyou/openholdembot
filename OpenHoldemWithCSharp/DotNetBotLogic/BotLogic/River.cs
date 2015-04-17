using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Classes;

namespace DotNetBotLogic.BotLogic
{
    class River
    {
        /// <summary>
        /// ##f$river_raise##
        /// </summary>
        /// <returns></returns>
        public static bool Raise()
        {
            if (!OH.didswag && !OH.didchec) // 1o giro di bet
            {
                if (Gecko.IRaisedOnRoundBefore)
                    return river_raise_initiative();

                if (Gecko.OpponentRaisedOnRoundBefore)
                    return river_raise_noinitiative();

                if (Gecko.NoBetOnRoundBefore)
                    return river_raise_noaction();

                if (Gecko.IRaisedGotReraisedAndICalled)
                    return river_raise_wegotraised();
            }
            else // uno mi ha reraisato
            {
                return RaiseTheReraise();
            }
            return false;
        }

        

        /// <summary>
        /// ##f$river_call##
        /// </summary>
        public static bool Call()
        {
            if (!OH.didswag)    // 1o giro di bet
            {
            if (Gecko.IRaisedOnRoundBefore) 
                return river_call_initiative();

            if (Gecko.OpponentRaisedOnRoundBefore) 
                return river_call_noinitiative();

            if (Gecko.NoBetOnRoundBefore)
                return river_call_noaction();

            if (Gecko.IRaisedGotReraisedAndICalled) 
                return river_call_wegotraised();
            }
            else // uno mi ha reraisato
            {
                return CallTheReraise();
            }
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

        /// <summary>
        /// ##f$river_raise_initiative##
        /// </summary>
        /// <returns></returns>
        private static bool river_raise_initiative()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    if (CheckBack() == true) return false;

                    if (OH.nplayersplaying == 2)
                        return OH.PrWin > .78;

                    if (OH.nplayersplaying >= 3)
                        return OH.PrWin > .80;
                    return false;

                }                
               
                if (Gecko.StandardRaise) return OH.PrWin > .98;
                if (Gecko.Overbet) return OH.PrWin > .98;
                if (Gecko.BigOverbet) return OH.PrWin > .98; return false;
            }


            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .80;

                if (Gecko.StandardRaise) return OH.PrWin > .90;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .98; return false;
            }

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    if (CheckBack() == true) return false;
                    if (OH.nplayersplaying == 2 && OH.nplayersround2 == 2 && !Gecko.safeboard1() && !OH.didcallround1 && OH.nbetsround1 > 1 && OH.nbetsround1 <= 6 && OH.didswaground2 && !OH.didcallround2 && OH.nbetsround2 < 11) return OH.PrWin > 0.00;
                    if (OH.nplayersplaying == 2 && OH.nbetsround2 == 0) return OH.PrWin > 0.70;
                    if (OH.nplayersplaying >= 3 && OH.nbetsround2 == 0) return OH.PrWin > 0.75;
                    if (OH.PrWin > .80) return true; 
                    return false;
                }             
               
                if (Gecko.StandardRaise) return OH.PrWin > .90;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .98; return false;
            }

            return false;
        }

        private static bool CheckBack()
        {
            if ((OH.strength_onepair > 0 && OH.strength_onepair <= 3)
                 || (OH.strength_twopair > 0 && OH.strength_twopair <= 3))
                return true;

            return false;
        }

        /// <summary>
        /// ##f$river_raise_noinitiative##
        /// </summary>
        /// <returns></returns>
        private static bool river_raise_noinitiative()
        {

            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .80;
                
                if (Gecko.StandardRaise) return OH.PrWin > .90;
                if (Gecko.Overbet) return OH.PrWin > .90;
                if (Gecko.BigOverbet) return OH.PrWin > .90; return false;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .80;               
                
                if (Gecko.StandardRaise) return OH.PrWin > .86;
                if (Gecko.Overbet) return OH.PrWin > .90;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .80;              
                
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .90;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }

            return false;
        }

        /// <summary>
        /// ##f$river_raise_noaction##
        /// </summary>
        /// <returns></returns>
        private static bool river_raise_noaction()
        {           

            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise)
                {
                    if (OH.nplayersround2 == 2)
                    {
                        //if (OH.nbetsround1 < 6 && OH.nbetsround2 < 15) 
                        //    return OH.PrWin > 0;
                        if (OH.PrWin > .65) 
                            return true; 
                        
                        return false;
                    }

                    if (OH.nplayersround2 >= 3) return OH.PrWin > .65; return false;
                }

                
                if (Gecko.StandardRaise) return OH.PrWin > .98;
                if (Gecko.Overbet) return OH.PrWin > .98;
                if (Gecko.BigOverbet) return OH.PrWin > .98; return false;
            }

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .64;
              
                if (Gecko.StandardRaise) return OH.PrWin > .80;
                if (Gecko.Overbet) return OH.PrWin > .82;
                if (Gecko.BigOverbet) return OH.PrWin > .86; return false;
            }

            if (OH.InPosition)
            {
           

                if (Gecko.NoBetsOrMinRaise)
                {
                    if (OH.nplayersround2 == 2) return OH.PrWin > .60;
                    if (OH.nplayersround2 >= 3) return OH.PrWin > .65; return false;
                }

                
                
                   
                if (Gecko.StandardRaise) 
                    return OH.PrWin > .80;
                if (Gecko.Overbet) 
                    return OH.PrWin > .90;
                if (Gecko.BigOverbet) 
                    return OH.PrWin > .96; return false;
            }

            return false;
        }

        /// <summary>
        /// ##f$river_raise_wegotraised##
        /// </summary>
        /// <returns></returns>
        private static bool river_raise_wegotraised()
        {           
            if (OH.OutOfPosition)
            {
                //  if( Utils.actionpostflop_1() ) return OH.PrWin>.76 ; // let the aggressor do the betting
               
                if (Gecko.StandardRaise) return OH.PrWin > .96;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }
            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .96;
                
                if (Gecko.StandardRaise) return OH.PrWin > .96;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }
            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .82;
                
                if (Gecko.StandardRaise) return OH.PrWin > .96;
                if (Gecko.Overbet) return OH.PrWin > .96;
                if (Gecko.BigOverbet) return OH.PrWin > .96; return false;
            }
            return false;
        }

        

        /// <summary>
        ///  ##f$river_call_wegotraised##
        /// </summary>
        /// <returns></returns>
        private static bool river_call_wegotraised()
        {
            if (OH.OutOfPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .65;
                if (Gecko.StandardRaise) return OH.PrWin > 80;
                if (Gecko.Overbet) return OH.PrWin > .85;
                if (Gecko.BigOverbet) return OH.PrWin > .90; 
                return false;
            };

            if (OH.OopMiddle)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .65;
                if (Gecko.StandardRaise) return OH.PrWin > .90;
                if (Gecko.Overbet) return OH.PrWin > .90;
                if (Gecko.BigOverbet) return OH.PrWin > .95; return false;
            };

            if (OH.InPosition)
            {
                if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .65;
                if (Gecko.StandardRaise) return OH.PrWin > .90;
                if (Gecko.Overbet) return OH.PrWin > .90;
                if (Gecko.BigOverbet) return OH.PrWin > .95; return false;
            };
            return false;
        }

        ///##f$river_call_initiative##
        public static bool river_call_initiative()
        {
            if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;         
            
            if (Gecko.StandardRaise)
            {
                
                if (OH.nplayersplaying == 2) return OH.PrWin > .65;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .70; 
                return false;
            };

            if (Gecko.Overbet)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .80;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .75; 
                return false;
            };

            if (Gecko.BigOverbet)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .90;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .95; 
                return false;
            };


            return false;
        }

        ///##f$river_call_noaction##
        public static bool river_call_noaction()
        {

            if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;        

            if (Gecko.StandardRaise)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .65;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .70; return false;
            };

            if (Gecko.Overbet)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .80;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .75; return false;
            };

            if (Gecko.BigOverbet)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .90;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .95; return false;
            };

            return false;
        }

        ///##f$river_call_noinitiative##
        public static bool river_call_noinitiative()
        {

            if (Gecko.NoBetsOrMinRaise) return OH.PrWin > .50;        

            if (Gecko.StandardRaise)
            {
                if (OH.nplayersplaying == 2) return OH.PrWin > .65;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .70; return false;
            };

            if (Gecko.Overbet)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .80;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .75; return false;
            };

            if (Gecko.BigOverbet)
            {

                if (OH.nplayersplaying == 2) return OH.PrWin > .90;
                if (OH.nplayersplaying >= 3) return OH.PrWin > .95; return false;
            };


            return false;
        }

    }
}
