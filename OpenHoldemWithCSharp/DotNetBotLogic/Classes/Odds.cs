using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Helpers;
using DotNetBotLogic.BotLogic;

namespace DotNetBotLogic.Classes
{
    public class Odds
    {       

        /// <summary>
        /// ##f$Odds_and_Outs##
        /// </summary>
        /// <returns></returns>
        public static bool Odds_and_Outs()
        {
            var outOdds = OutOdds();
            var potOdds = PotOdds();

            if (OH.br == 2) 
                return (outOdds * 2.0) > potOdds;

            if (OH.br == 3) 
                return (outOdds * 1.2) > potOdds;

            return false;
        }  

        /// <summary>
        ///  ##f$PotOdds##
        /// </summary>
        /// <returns></returns>
        public static double PotOdds()
        {
            if (OH.call == 0) 
                return (OH.bet / (OH.pot + ImpliedOdds()));
            
            if (OH.call > 0) 
                return (OH.call / (OH.pot + ImpliedOdds())); 
            
            return 0;
        }


        /// <summary>
        /// ##f$ImpliedOdds##
        /// </summary>
        /// <returns></returns>
        private static double ImpliedOdds()
        {
            double result = 0;
            if (OH.br == 2)
                result = (((double)OH.nopponentsbetting * .50) * (double)OH.bet4 * 1.5) + ((((double)OH.nplayersplaying - (double)1 - (double)OH.nopponentsbetting) * .275) * (double)OH.bet4 * 1.25);
            if (OH.br == 3)
                result = (((double)OH.nopponentsbetting * .50) * (double)OH.bet4) + ((((double)OH.nplayersplaying - (double)1 - (double)OH.nopponentsbetting) * .275) * (double)OH.bet4);
            return result;
        }

        /// <summary>
        /// ##f$OutOdds##
        /// </summary>
        /// <returns></returns>
        public static double OutOdds()
        {
            int nouts = OH.mh_nouts;
            double result = (double)nouts / (double)46;
            return result;
        }        
    }
}
