using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Classes;
using DotNetBotLogic.Helpers;
using System.Globalization;

namespace DotNetBotLogic.BotLogic
{
    class Gecko
    {
        public static double PotSenzaUltimaBet { get { return OH.potcommon + OH.currentbet0; } }

        /// <summary>
        /// ##f$draws##
        /// </summary>
        /// <returns></returns>
        public static int Draws()
        {
            if ((!OH.istwopair && OH.nsuited == 4 && OH.nsuitedcommon == 2 && OH.nstraight == 4 && OH.nstraightcommon < 3 && !WheelDraw() && OH.nrankedcommon == 1) ||
                (!OH.istwopair && OH.nsuited == 4 && OH.nsuitedcommon == 3 && OH.nstraight == 4 && !WheelDraw() && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) <= 3 && OH.nstraightcommon < 3 && OH.nrankedcommon == 1) ||
                (OH.br == 2 && !OH.istwopair && OH.nsuited == 4 && OH.nsuitedcommon == 2 && OH.nstraight == 4 && !WheelDraw() && OH.nstraightcommon < 3 && OH.nrankedcommon == 2) ||
                (OH.br == 2 && !OH.istwopair && OH.nsuited == 4 && OH.nsuitedcommon == 3 && OH.nstraight == 4 && !WheelDraw() && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) <= 3 && OH.nstraightcommon < 3 && OH.nrankedcommon == 2))
                return 4;


            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 0 && OH.nsuitedcommon == 2 && OH.nrankedcommon == 1) ||
                (!OH.istwopair && OH.rankloplayer > OH.rankhicommon && OH.nstraightcommon < 3 && OH.nrankedcommon == 1 && OH.nsuited == 4 && OH.nsuitedcommon == 2) ||
                (!OH.istwopair && OH.MyHand.IsAA && OH.nsuitedcommon == 3 && OH.nsuited == 4 && OH.nrankedcommon == 1) ||
                (!OH.istwopair && OH.nstraight == 4 && OH.nstraight > OH.nstraightcommon && !WheelDraw() && OH.ishipair && OH.nsuitedcommon < 3 && OH.nrankedcommon == 1 && (OH.pokervalcommon < HandStrenght.PAIR)) ||
                (OH.nsuited == 4 && OH.nsuited > OH.nsuitedcommon && OH.ishipair && OH.nrankedcommon == 1))
                return 3;


            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 0 && OH.nsuitedcommon == 3 && OH.nrankedcommon == 1)) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 0 && OH.nsuitedcommon == 3 && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.nstraight == 4 && OH.nstraight > OH.nstraightcommon && !WheelDraw() && OH.ismidpair && OH.nrankedcommon < 3 && OH.nsuitedcommon < 3 && OH.nrankedcommon < 3 && (OH.pokervalcommon < HandStrenght.PAIR))) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && OH.nsuited > OH.nsuitedcommon && OH.ismidpair && OH.nrankedcommon < 3 && (OH.pokervalcommon < HandStrenght.PAIR))) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 1 && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 0 && OH.nsuitedcommon == 2 && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 0 && OH.nsuitedcommon == 2 && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && BitHelpers.BitCount((~OH.srankbits & 0x7fff) >> OH.srankhiplayer) == 2 && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.nsuited == 4 && OH.nsuitedcommon == 2 && OH.nrankedcommon < 3)) return 2;
            if ((OH.isthreeofakind && OH.br < 4 && OH.nrankedcommon == 1)) return 2;
            if ((OH.rankloplayer > OH.rankhicommon && OH.br < 4 && SafeBoard && OH.isonepair && !OH.ispair)) return 2;
            if ((OH.rankloplayer > OH.rankhicommon && OH.br < 4 && OH.nsuitedcommon < 3 && OH.nstraightcommon < 3 && OH.nstraightfillcommon > 1 && OH.istwopair && OH.nrankedcommon == 2)) return 2;
            if ((OH.nstraightfill == 1 && OH.br < 4 && OH.istwopair && OH.nrankedcommon == 2 && OH.nsuitedcommon < 3)) return 2;
            //if ((OH.nstraightfill == 1 && OH.br < 4 && OH.rankloplayer > OH.rankhicommon && SafeBoard)) return 2;  // non 2nd barrell
            if ((OH.nstraightfill == 1 && OH.br < 4 && OH.nsuited == 3 && OH.br == 2 && OH.rankhiplayer > OH.rankhicommon && SafeBoard)) return 2;
            if ((!OH.istwopair && OH.br < 4 && OH.nsuited == 4 && OH.nsuitedcommon == 3 && OH.rankloplayer > 11 && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.br < 4 && OH.nsuited == 3 && OH.nsuitedcommon == 1 && OH.nstraight == 3 && OH.nstraightcommon <= 2 && OH.rankloplayer > OH.rankhicommon && OH.nrankedcommon == 1 && OH.br == 2)) return 2;
            if ((!OH.istwopair && OH.br == 2 && OH.nstraight == 4 && OH.nstraightcommon == 3 && !WheelDraw() && OH.nsuitedcommon < 3 && OH.nrankedcommon < 3 && OH.rankhiplayer > OH.rankhicommon)) return 2;
            if ((!OH.istwopair && OH.nstraight == 4 && OH.nstraightcommon <= 2 && OH.nsuitedcommon < 3 && OH.nrankedcommon < 3 && !WheelDraw() && OH.nrankedcommon < 3)) return 2;
            if ((!OH.istwopair && OH.br < 4 && OH.nstraight == 4 && OH.nstraightcommon == 3 && !WheelDraw() && OH.rankloplayer > OH.ranklocommon && OH.rankhiplayer > OH.rankhicommon && OH.nrankedcommon < 3)) return 2;



            if ((!OH.istwopair && (OH.nstraight == 2 && OH.nstraightfillcommon == 1 && OH.nsuitedcommon < 3 && OH.nrankedcommon == 1) || (OH.nstraight == 3 && OH.nstraightfillcommon == 1 && OH.nsuitedcommon < 3 && OH.nrankedcommon == 1)) ||
                (OH.nstraightfill == 1 && OH.nsuitedcommon < 3) ||
                (OH.istwopair && OH.nrankedcommon == 1) ||
                (OH.isonepair && OH.nsuitedcommon < 3 && OH.nstraightcommon < 3 && OH.nrankedcommon == 1) ||
                (!OH.istwopair && OH.nsuitedcommon == 3 && OH.nsuited == 4 && (OH.srankloplayer > 6 || OH.srankhiplayer > 6) && OH.nrankedcommon == 1) ||
                (!OH.istwopair && OH.nstraight == 4 && OH.nstraightcommon == 3 && OH.nrankedcommon == 1) ||
                (!OH.istwopair && OH.nstraight == 3 && OH.nstraightcommon == 2 && OH.nstraightfillcommon <= 2 && OH.nrankedcommon == 1) ||
                (WheelDraw() && OH.nrankedcommon == 1))
                return 1;

            return 0;
        }      

        /// <summary>
        /// ##f$wheeldraw##
        /// </summary>
        /// <returns></returns>
        public static bool WheelDraw()
        {
            return (OH.rankbits >> 9 == 60
                    || ((OH.rankbits & 30) == 30));        //check 432A
        }

        /// <summary>
        ///  ##f$safeboard##
        ///  Sostituito con quello di Winngy, quello di gecko era sbagliato
        /// </summary>
        /// <returns></returns>
        public static bool SafeBoard
        {
            get { return   (OH.nrankedcommon < 2 && 
                            OH.nsuitedcommon < 3 && 
                            OH.nstraightcommon < 3 && 
                            OH.nstraightfillcommon>1) &&
                            !(OH.br>2 && OH.rankhicommon==14 && OH.rankhicommon>OH.rankhiplayer); }
        }      


        /// <summary>
        /// ##f$Initiative_Nobody##
        /// </summary>
        /// <returns></returns>
        public static bool NoBetOnRoundBefore
        {
            get
            {
                if (OH.br <= 2) return OH.nbetsround1 <= 1;
                if (OH.br == 3) return OH.nbetsround2 <= 0;
                if (OH.br == 4) return OH.nbetsround3 <= 0;
                return false;
            }
        }

        /// <summary>
        /// ##f$Initiative_Villian##
        /// </summary>
        /// <returns></returns>
        public static bool OpponentRaisedOnRoundBefore
        {
            get
            {
                if (OH.br <= 2) return OH.didcallround1 && OH.nbetsround1 > 1;
                if (OH.br == 3) return !OH.didswaground2 && OH.didcallround2 && OH.nbetsround2 > 0;
                if (OH.br == 4) return !OH.didswaground2 && OH.didcallround3 && OH.nbetsround3 > 0;
                return false;
            }
        }

        /// <summary>
        /// ##f$Initiative_Hero##
        /// </summary>
        /// <returns></returns>
        public static bool IRaisedOnRoundBefore
        {
            get
            {
                if (OH.br <= 2 && OH.nbetsround1 > 1 && !OH.didcallround1) return true;
                if (OH.br == 3 && OH.didswaground2 && !OH.didcallround2) return true;
                if (OH.br == 4 && OH.didswaground3 && !OH.didcallround3) return true;
                return false;
            }
        }

        /// <summary>
        /// ##f$Initiative_GotRaised##
        /// </summary>
        /// <returns></returns>
        public static bool IRaisedGotReraisedAndICalled
        {
            get
            {
                if (OH.br == 3) return OH.didswaground2 && OH.didcallround2 && OH.nbetsround2 > 0;
                if (OH.br == 4) return OH.didswaground3 && OH.didcallround3 && OH.nbetsround3 > 0;
                return false;
            }
        }

        /// <summary>
        /// ##f$actionpostflop_1##
        /// </summary>
        public static bool NoBetsOrMinRaise { get { return OH.call <= OH.bblind; } } // ignora i minraise        

        /// <summary>
        /// ##f$actionpostflop_3##
        /// </summary>
        public static bool StandardRaise { get { return (OH.call > OH.bblind) && (OH.call <= (PotSenzaUltimaBet * 0.8)); } }


        /// <summary>
        /// ##f$actionpostflop_4##
        /// </summary>
        public static bool Overbet
        {
            get { return (OH.call > (PotSenzaUltimaBet * 0.8)) && (OH.call <= (PotSenzaUltimaBet * 1.2)); }
        }


        /// <summary>
        /// ##f$actionpostflop_5##
        /// </summary>
        public static bool BigOverbet
        {
            get { return (OH.call > (PotSenzaUltimaBet * 1.2)); }
        }

        /// <summary>
        ///  ##f$safeboard1##
        /// </summary>
        /// <returns></returns>
        public static bool safeboard1()
        {
            return OH.nrankedcommon == 1 && OH.nsuitedcommon <= 2 && OH.nstraightcommon <= 2;
        }

        /// <summary>
        ///  ##f$committed##
        /// </summary>
        /// <returns></returns>
        public static bool Committed()
        {
            double toCall = Math.Min(OH.call, OH.balance);
            if (toCall * 10 < OH.pot) return OH.PrWin > .30;
            if (toCall * 9 < OH.pot) return OH.PrWin > .40;
            if (toCall * 8 < OH.pot) return OH.PrWin > .50;
            if (toCall * 7 < OH.pot) return OH.PrWin > .60;
            if (toCall * 6 < OH.pot) return OH.PrWin > .75;
            if (toCall * 5 < OH.pot) return OH.PrWin > .80;
            if (toCall * 4 < OH.pot) return OH.PrWin > .85;
            if (toCall * 3 < OH.pot) return OH.PrWin > .95;
            return false;
        }




        
           

       

 }
}
