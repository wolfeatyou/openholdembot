using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.Classes
{
    static class OH
    {
        #region Public properties

        /* Limits */
        public static double sblind { get;  set; }
        public static double bblind { get;  set; }

        /* Chairs */
        public static int MyChair { get;  set; }
        public static int raischair { get; set; }

        /* Rounds and position */
        public static Rounds BetRound { get; set; }
        public static int br { get; set; }
        public static int betposition { get; set; }
        public static int callposition { get; set; }
        public static int dealpositionrais { get; set; }

        /* Probabilities */
        public static double prwinnow { get; set; }
        public static double prwin { get; set; }
        public static double prtie { get; set; }
        public static double prlos { get; set; }

        /* Chip amounts */
        /// <summary> Your balance</summary>
        public static double balance { get; set; }
        public static double balance0 { get; set; }
        public static double balance1 { get; set; }
        public static double balance2 { get; set; }
        public static double balance3 { get; set; }
        public static double balance4 { get; set; }
        public static double balance5 { get; set; }
        public static double currentbet0 { get; set; }
        public static double currentbet1 { get; set; }
        public static double currentbet2 { get; set; }
        public static double currentbet3 { get; set; }
        public static double currentbet4 { get; set; }
        public static double currentbet5 { get; set; }
        /// <summary>the amount you need to call</summary>
        public static double call { get; set; }
        /// <summary>the amount of a single initial bet or raise for current round</summary>
        public static double bet { get; set; }        
        public static double bet4 { get; set; }
        public static double pot { get; set; }
        /// <summary>amount of chips in the middle</summary>
        public static double potcommon { get; set; }

        /* Number of bets */
        /// <summary>total number of additional bets required to call.</summary>
        public static int nbetstocall { get; set; }
        /// <summary>total number of bets currently in front of you</summary>
        public static int ncurrentbets { get; set; }
        /// <summary>total number of bets you would have on the table if you call</summary>
        public static int ncallbets { get; set; }
        /// <summary>total number of bets you would have on the table if you raise</summary>
        public static int nraisbets { get; set; }

        /* Poker values */
        /// <summary>Absolute poker value for your 5 card hand</summary>
        public static int pokerval { get; set; }
        /// <summary>Absolute poker value for your 2 card in hand</summary>
        public static int pokervalplayer { get; set; }
        /// <summary>Absolute poker value for the common cards</summary>
        public static int pokervalcommon { get; set; }
        public static int pcbits { get; set; }



        /* Hand tests */
        /// <summary>true if your hand has gone up a level (i.e. from 1 pair to 2 pair)</summary>
        public static bool ishandup { get; set; }
        public static bool isonepair { get; set; }
        public static bool istwopair { get; set; }
        public static bool isthreeofakind { get; set; }
        public static bool isstraight { get; set; }
        public static bool isflush { get; set; }
        public static bool isfullhouse { get; set; }
        public static bool isfourofakind { get; set; }
        public static bool isstraightflush { get; set; }
        public static bool isroyalflush { get; set; }

        /* Pocket test */
        /// <summary>true when your two dealt pocket cards are rank equal (0-1)</summary>
        public static bool ispair { get { return MyHand.ispair; } }
        public static bool issuited { get { return MyHand.issuited; } }
        public static bool isconnector { get { return MyHand.isconnector; } }

        /* Pocket - common test */
        public static bool ishipair { get; set; }
        public static bool islopair { get; set; }
        public static bool ismidpair { get; set; }
        public static bool ishiflush { get; set; }

        /* Players - opponents */
        public static int nplayersdealt { get; set; }
        public static int nplayersplaying { get; set; }        
        public static int nopponentsbetting { get; set; }
        public static int nopponentscalling { get; set; }
        public static int nopponentsplaying { get; set; }
        public static int nopponentsraising { get; set; }
        public static int nopponentsseated { get; set; }
        /// <summary>number of chairs dealt after your chair</summary>
        public static int nchairsdealtleft { get; set; }

        /* History */
        /// <summary>number of players that began the current betting round</summary>
        public static int nplayersround { get; set; }
        /// <summary>number of players that began betting round 1 - round 4</summary>
        public static int nplayersround2 { get; set; }
        /// <summary> the number of times the autoplayer has checked during the current round </summary>
        public static bool didchec { get; set; }
        /// <summary> the number of times the autoplayer has called during the current round </summary>
        public static bool didcall { get; set; }
        /// <summary> the number of times the autoplayer has raised during the current round</summary>
        //public static bool didswag { get; set; }
        /// <summary> the number of times the autoplayer has swag'd during the current round (*) </summary>
        public static bool didswag { get; set; }
        public static int nbetsround1 { get; set; }
        public static int nbetsround2 { get; set; }
        public static int nbetsround3 { get; set; }
        public static int nbetsround4 { get; set; }
        /// <summary>the largest number of bets in front of any player right now  </summary>
        public static int nbetsround { get; set; }
        public static bool didcallround1 { get; set; }        
        public static bool didcallround2 { get; set; }
        public static bool didcallround3 { get; set; }
        public static bool didraisround1 { get; set; }
        public static bool didraisround2 { get; set; }
        public static bool didraisround3 { get; set; }
        public static bool didswaground1 { get; set; }
        public static bool didswaground2 { get; set; }
        public static bool didswaground3 { get; set; }

        /* My hand*/
        /// <summary> number of outs (HTC's formula)  </summary>
        public static int mh_nouts { get; set; }     
        /// <summary> 0-5 (5 best) of the relative strength of your straight flush</summary>
        public static int strength_straightflush { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your four of a kind </summary>
        public static int strength_quads { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your full house </summary>
        public static int strength_fullhouse { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your flush </summary>
        public static int strength_flush { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your straight</summary>
        public static int strength_straight { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your three of a kind </summary>
        public static int strength_trips { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your two pair </summary>
        public static int strength_twopair { get; set; }
        /// <summary> 0-5 (5 best) of the relative strength of your one pair </summary>
        public static int strength_onepair { get; set; }    
        


        /// <summary> your deal position (1=sblind,2=bblind ... nplayersdealt=dealer). Dealposition will not change as players fold. </summary>
        public static int dealposition { get; set; }

        public static int handrank169 { get; set; }              

        public static bool IsMyTurn { get; set; } 
       
        //preflop
        public static Position MyPreflopPosition { get; set; }

        //updating card values
        public static Hand MyHand { get; set; }
        public static Board Board { get; set; }        
        

        /// <summary>
        /// //total number of same ranked cards in the middle (1-4)
        /// </summary>
        public static int nrankedcommon { get; set; }

        /// <summary>
        /// //highest card rank (14-2) (commons)
        /// </summary>
        public static int rankhicommon { get; set; }

        /// <summary>
        /// //total number of connected common cards (1-5)
        /// </summary>
        public static int nstraightcommon { get; set; }

        /// <summary>
        /// total number of cards needed to fill a common straight (0-5)
        /// </summary>
        public static int nstraightfillcommon { get; set; }

        
        

        /// <summary>
        /// total number of same suited cards in the middle (1-5)
        /// </summary>
        public static int nsuitedcommon { get; set; }      

        

        public static double potplayer { get; set; }        

        
        

        
           

        public static int nsuited { get; set; }

        public static int nstraightfill { get; set; }

        public static int nstraight { get; set; }

        /// <summary>
        /// nhandshi
        /// </summary>
        public static int nhandshi { get; set; }        

        public static int trank { get; set; }        

        public static int nstraightflushfillcommon { get; set; }

        public static int ncardsunknown { get; set; }

        public static int srankbits { get; set; }
        public static int srankhiplayer { get; set; }

        public static int rankloplayer { get; set; }
        public static int rankhiplayer { get; set; }
        public static int ranklocommon { get; set; }
        public static int trankcommon { get; set; }

        
        public static double callshort { get; set; }
        public static double raisshort { get; set; }
           

        public static double randomround { get; set; }

       

        public static int nranked { get; set; }
        public static int rankbits { get; set; }

        /// <summary>which chairs raised in round 1 </summary>
        public static int raisbits1 { get; set; }
        /// <summary>which chairs raised in round 2 </summary>
        public static int raisbits2 { get; set; }

      

        public static int srankloplayer { get; set; }     

        

        public static int opponentsplayingbits { get; set; }

        public static int nplayersseated { get; set; }

        public static int nchairs { get; set; }

        

        #region Roba del winngy bot
        
        /// <summary>
        /// ##f$dpd##
        /// dynamic dealer position
        /// </summary>
        public static bool dpd { get { return betposition == nplayersplaying; } }

        /// <summary>
        ///  ##f$spl##
        /// static position late
        /// </summary>
        public static bool spl {get{return dealposition>=nplayersdealt-1;}}

        /// <summary>
        /// ##f$spd##
        /// static position dealer
        /// </summary>
        public static bool spd {get{return dealposition==nplayersdealt;}}

        /// <summary>
        ///  ##f$spe##
        /// static position early
        /// </summary>
        public static bool spe { get { return dealposition > 2 && dealposition >= nplayersdealt - 8 && dealposition <= nplayersdealt - 5; } }

        /// <summary>
        ///  ##f$spm##
        /// static position middle
        /// </summary>
        public static bool spm { get { return dealposition > nplayersdealt - 5 && dealposition < nplayersdealt - 1; } }

         /// <summary>
        ///  ##f$dpe##
        /// dynamic position early
        /// </summary>
        public static bool dpe { get { return betposition == 1 || betposition / nplayersplaying <= (1 / 2); } }

        /// <summary>
        /// ##f$dpm##
        /// dynamic middle position
        /// </summary>
        public static bool dpm { get { return betposition / nplayersplaying > (1 / 2) && betposition / nplayersplaying <= (3 / 4) && betposition != nplayersplaying; } }


        /// <summary>
        /// ##f$dpl##
        /// dynamic late position
        /// </summary>
        public static bool dpl { get { return betposition / nplayersplaying > (3 / 4); } }

        /// <summary>
        /// ##f$top2pair##
        /// you hold the two highest pairs on board 
        /// </summary>
        public static bool top2pair { get { return istwopair && (pokerval & 15) < ((pokerval >> 4) & 15); } }

        /// <summary>
        /// ##f$value_to_raise##
        /// </summary>
        public static double value_to_raise{get{return (((prwin * (pot+raisshort))+(prtie * (pot+raisshort/2)))-(prlos * (call+bet)));}}

            
        /// <summary>
        /// ##f$value_to_call##
        /// </summary>
        public static double value_to_call{get{return (((prwin * (pot+callshort))+(prtie * (pot+callshort/2)))-(prlos * call));}}

        
        /// <summary>
        /// ##f$cost_to_fold##
        /// </summary>
        public static double cost_to_fold { get { return ((prwin * pot) + (prtie * (pot / 2))); } }

        public static double dbg;

        #endregion

        #region Roba del Gecko bot

        /// <summary> ##f$prwin## </summary>
        public static double PrWin { get { return (OH.prwin + (OH.prtie / 2)); } }

        /// <summary>  ##f$Position_Early## </summary>       
        public static bool OutOfPosition { get { return betposition == 1; } }

        /// <summary>  ##f$Position_Middle## </summary>       
        public static bool OopMiddle { get { return !OutOfPosition && !InPosition; } }

        /// <summary>  ##f$Position_Late## </summary>       
        public static bool InPosition { get { return betposition == nplayersplaying; } }

        #endregion


        #endregion

        
      
    }
} 
    
