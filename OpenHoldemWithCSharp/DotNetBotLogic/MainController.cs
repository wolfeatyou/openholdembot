/* DotNetHoldem 
 * .Net Solution that allows to use OpenHoldem with .Net logic
 * Author: Óscar Andreu
 * oesido at gmail dot com
 * Licensed under GPLv3 - 2012
 */

using System;
using System.Collections.Generic;
using DotNetBotLogic.Classes;
using DotNetBotLogic.Enums;
using System.Runtime.InteropServices;
using DotNetBotLogic.BotLogic;
using DotNetBotLogic.Helpers;
using System.Threading;

namespace DotNetBotLogic
{
    public class MainController
    {
        #region static

        public static managed_holdem_state mhs { get; private set; }

        OH_Wrapper Oh;

        #endregion

        #region Constructor

        public MainController(IntPtr getSymbolFromDll, IntPtr getPlayerName)
        {
            Oh = new OH_Wrapper(getSymbolFromDll, getPlayerName);
        }

        #endregion

        /// <summary>
        /// Process de query message
        /// </summary>
        /// <param name="pquery"></param>
        /// <returns></returns>
        public double ProcessQuery(string pquery)
        {
            try
            {
                double haveToRaise;
                RefreshGlobalVar();

                if (mhs.IsPlaying == true)
                    BeepIfLowBalanceOrHeadsUp();

                switch (pquery)
                {
                    case "dll$swag":
                        haveToRaise = Bot.GetRaiseAction();
                        if (haveToRaise > 0)
                        {
                            double swagResult = Bot.GetRaiseAmount();
                            return swagResult;
                        }
                        break;

                    case "dll$call":
                        double callResult = Bot.getCallAction();
                        return callResult;
                }
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        private void BeepIfLowBalanceOrHeadsUp()
        {
            if (OH.balance < 60 * OH.bblind)    // sono short
                PC.Beep();

            if (OH.nplayersseated <= 2) // sono 1v1
                PC.Beep();
        }
        

        public void UpdateState(IntPtr s)
        {
            mhs = (managed_holdem_state)Marshal.PtrToStructure(s, typeof(managed_holdem_state));

            try
            {
                RefreshGlobalVar();
               // MainController c = new MainController();
               // c.RefreshGlobalVar();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }

        public void RefreshGlobalVar()
        {            
            //Limits
            OH.bblind = Oh.GetSymbol(Symbols.bblind);
            OH.sblind = Oh.GetSymbol(Symbols.sblind);

            //Chairs
            OH.MyChair = (int)Oh.GetSymbol(Symbols.userchair);
            OH.raischair = (int)Oh.GetSymbol(Symbols.raischair);

            OH.IsMyTurn = ((int)Oh.GetSymbol(Symbols.ismyturn) == 7);
            OH.BetRound = (Rounds)Oh.GetSymbol(Symbols.betround);



            OH.pot = Oh.GetSymbol(Symbols.pot);

            //updating card values
            OH.MyHand = new Hand();	//rank 2 ; ... ; 14=A suit 1=C ; 2=D ; 3=H ; 4=S           
            OH.MyHand.SetValues(OH.MyChair);

            OH.Board = new Board();
            OH.Board.SetBoardCards();

            OH.nstraightcommon = (int)Oh.GetSymbol(Symbols.nstraightcommon);
            OH.nstraightfillcommon = (int)Oh.GetSymbol(Symbols.nstraightfillcommon);

            OH.nrankedcommon = (int)Oh.GetSymbol(Symbols.nrankedcommon);
            OH.nsuitedcommon = (int)Oh.GetSymbol(Symbols.nsuitedcommon);
            OH.rankhicommon = (int)Oh.GetSymbol(Symbols.rankhicommon);

            OH.dealposition = (int)Oh.GetSymbol(Symbols.dealposition);
            OH.nplayersdealt = (int)Oh.GetSymbol(Symbols.nplayersdealt);
            OH.betposition = (int)Oh.GetSymbol(Symbols.betposition);
            OH.nplayersplaying = (int)Oh.GetSymbol(Symbols.nplayersplaying);


            OH.br = (int)Oh.GetSymbol(Symbols.br);
            OH.callposition = (int)Oh.GetSymbol(Symbols.callposition);
            OH.nopponentsbetting = (int)Oh.GetSymbol(Symbols.nopponentsbetting);
            OH.nraisbets = (int)Oh.GetSymbol(Symbols.nraisbets);
            OH.nopponentscalling = (int)Oh.GetSymbol(Symbols.nopponentscalling);
            OH.dealpositionrais = (int)Oh.GetSymbol(Symbols.dealpositionrais);
            OH.nopponentsplaying = (int)Oh.GetSymbol(Symbols.nopponentsplaying);

            OH.nbetstocall = (int)Oh.GetSymbol(Symbols.nbetstocall);

            OH.nstraightfill = (int)Oh.GetSymbol(Symbols.nstraightfill);
            OH.nsuited = (int)Oh.GetSymbol(Symbols.nsuited);
            OH.nstraight = (int)Oh.GetSymbol(Symbols.nstraight);
            OH.balance = Oh.GetSymbol(Symbols.balance);
            OH.call = OH.balance > 0 ? Math.Min(OH.balance, Oh.GetSymbol(Symbols.call)) : Oh.GetSymbol(Symbols.call);
            OH.bet = Oh.GetSymbol(Symbols.bet);
            double temp2 = Oh.GetSymbol(Symbols.ncallbets);
            OH.ncallbets = (int)temp2;
            OH.potplayer = (int)Oh.GetSymbol(Symbols.potplayer);

            OH.nchairsdealtleft = (int)(Oh.GetSymbol(Symbols.nchairsdealtleft));
            OH.isroyalflush = Convert.ToBoolean(Oh.GetSymbol(Symbols.isroyalflush));
            OH.isstraightflush = Convert.ToBoolean(Oh.GetSymbol(Symbols.isstraightflush));
            OH.pokervalcommon = (int)Oh.GetSymbol(Symbols.pokervalcommon);
            OH.nhandshi = (int)Oh.GetSymbol(Symbols.nhandshi);
            OH.isfourofakind = Convert.ToBoolean(Oh.GetSymbol(Symbols.isfourofakind));
            OH.isfullhouse = Convert.ToBoolean(Oh.GetSymbol(Symbols.isfullhouse));
            OH.pokerval = (int)Oh.GetSymbol(Symbols.pokerval);
            OH.pcbits = (int)Oh.GetSymbol(Symbols.pcbits);
            OH.trank = (int)Oh.GetSymbol(Symbols.trank);
            OH.isflush = Convert.ToBoolean(Oh.GetSymbol(Symbols.isflush));
            OH.ishiflush = Convert.ToBoolean(Oh.GetSymbol(Symbols.ishiflush));
            OH.nstraightflushfillcommon = (int)Oh.GetSymbol(Symbols.nstraightflushfillcommon);
            OH.srankbits = (int)Oh.GetSymbol(Symbols.srankbits);
            OH.srankhiplayer = (int)Oh.GetSymbol(Symbols.srankhiplayer);
            OH.isstraight = Convert.ToBoolean(Oh.GetSymbol(Symbols.isstraight));
            OH.rankloplayer = (int)Oh.GetSymbol(Symbols.rankloplayer);
            OH.isthreeofakind = Convert.ToBoolean(Oh.GetSymbol(Symbols.isthreeofakind));
            OH.rankhiplayer = (int)Oh.GetSymbol(Symbols.rankhiplayer);
            OH.ranklocommon = (int)Oh.GetSymbol(Symbols.ranklocommon);
            OH.istwopair = Convert.ToBoolean(Oh.GetSymbol(Symbols.istwopair));
            OH.trankcommon = (int)Oh.GetSymbol(Symbols.trankcommon);
            OH.isonepair = Convert.ToBoolean(Oh.GetSymbol(Symbols.isonepair));
            OH.ishipair = Convert.ToBoolean(Oh.GetSymbol(Symbols.ishipair));
            OH.ismidpair = Convert.ToBoolean(Oh.GetSymbol(Symbols.ismidpair));
            OH.islopair = Convert.ToBoolean(Oh.GetSymbol(Symbols.islopair));
            OH.prwinnow = Oh.GetSymbol(Symbols.prwinnow);
            OH.prwin = Oh.GetSymbol(Symbols.prwin);
            OH.prtie = Oh.GetSymbol(Symbols.prtie);
            OH.prlos = Oh.GetSymbol(Symbols.prlos);
            OH.mh_nouts = (int)Oh.GetSymbol(Symbols.mh_nouts);
            OH.callshort = Oh.GetSymbol(Symbols.callshort);
            OH.raisshort = Oh.GetSymbol(Symbols.raisshort);
            OH.ncardsunknown = (int)Oh.GetSymbol(Symbols.ncardsunknown);
            OH.bet4 = Oh.GetSymbol(Symbols.bet4);
            OH.ishandup = Convert.ToBoolean(Oh.GetSymbol(Symbols.ishandup));
            OH.randomround = Oh.GetSymbol(Symbols.randomround);
            OH.nplayersround = (int)Oh.GetSymbol(Symbols.nplayersround);

            OH.nbetsround = (int)Oh.GetSymbol(Symbols.nbetsround);
            OH.nbetsround1 = (int)Oh.GetSymbol(Symbols.nbetsround1);
            OH.nbetsround2 = (int)Oh.GetSymbol(Symbols.nbetsround2);
            OH.nbetsround3 = (int)Oh.GetSymbol(Symbols.nbetsround3);
            OH.nbetsround4 = (int)Oh.GetSymbol(Symbols.nbetsround4);

            OH.nranked = (int)Oh.GetSymbol(Symbols.nranked);
            OH.rankbits = (int)Oh.GetSymbol(Symbols.rankbits);
            OH.pokervalplayer = (int)Oh.GetSymbol(Symbols.pokervalplayer);
            OH.ncurrentbets = (int)Oh.GetSymbol(Symbols.ncurrentbets);
            OH.currentbet0 = Oh.GetSymbol(Symbols.currentbet0);
            OH.currentbet1 = Oh.GetSymbol(Symbols.currentbet1);
            OH.currentbet2 = Oh.GetSymbol(Symbols.currentbet2);
            OH.currentbet3 = Oh.GetSymbol(Symbols.currentbet3);
            OH.currentbet4 = Oh.GetSymbol(Symbols.currentbet4);
            OH.currentbet5 = Oh.GetSymbol(Symbols.currentbet5);
            OH.raisbits1 = (int)Oh.GetSymbol(Symbols.raisbits1);
            OH.raisbits2 = (int)Oh.GetSymbol(Symbols.raisbits2);
            OH.nopponentsraising = (int)Oh.GetSymbol(Symbols.nopponentsraising);

            OH.nopponentsseated = (int)Oh.GetSymbol(Symbols.nopponentsseated);
            OH.srankloplayer = (int)Oh.GetSymbol(Symbols.srankloplayer);
            OH.potcommon = Oh.GetSymbol(Symbols.potcommon);

            OH.dbg = Oh.GetSymbol(Symbols.didswag);
            OH.didswag = Oh.GetSymbol(Symbols.didswag) > 0;
            OH.didswaground1 = Oh.GetSymbol(Symbols.didswaground1) > 0;
            OH.didswaground2 = Oh.GetSymbol(Symbols.didswaground2) > 0;
            OH.didswaground3 = Oh.GetSymbol(Symbols.didswaground3) > 0;

            OH.didchec = Oh.GetSymbol(Symbols.didchec) > 0;
            OH.didraisround1 = Oh.GetSymbol(Symbols.didraisround1) > 0;
            OH.didraisround2 = Oh.GetSymbol(Symbols.didraisround2) > 0;
            OH.didraisround3 = Oh.GetSymbol(Symbols.didraisround3) > 0;

            OH.didcall = Oh.GetSymbol(Symbols.didcall) > 0;
            OH.didcallround1 = Oh.GetSymbol(Symbols.didcallround1) > 0;
            OH.didcallround2 = Oh.GetSymbol(Symbols.didcallround2) > 0;
            OH.didcallround3 = Oh.GetSymbol(Symbols.didcallround3) > 0;

            OH.opponentsplayingbits = (int)Oh.GetSymbol(Symbols.opponentsplayingbits);
            OH.balance0 = Oh.GetSymbol(Symbols.balance0);
            OH.balance1 = Oh.GetSymbol(Symbols.balance1);
            OH.balance2 = Oh.GetSymbol(Symbols.balance2);
            OH.balance3 = Oh.GetSymbol(Symbols.balance3);
            OH.balance4 = Oh.GetSymbol(Symbols.balance4);
            OH.balance5 = Oh.GetSymbol(Symbols.balance5);
            OH.nplayersround2 = (int)Oh.GetSymbol(Symbols.nplayersround2);
            OH.handrank169 = (int)Oh.GetSymbol(Symbols.handrank169);

            OH.strength_flush = (int)Oh.GetSymbol(Symbols.mh_str_flush);
            OH.strength_fullhouse = (int)Oh.GetSymbol(Symbols.mh_str_fullhouse);
            OH.strength_onepair = (int)Oh.GetSymbol(Symbols.mh_str_onepair);
            OH.strength_quads = (int)Oh.GetSymbol(Symbols.mh_str_quads);
            OH.strength_straight = (int)Oh.GetSymbol(Symbols.mh_str_straight);
            OH.strength_straightflush = (int)Oh.GetSymbol(Symbols.mh_str_strflush);
            OH.strength_trips = (int)Oh.GetSymbol(Symbols.mh_str_trips);
            OH.strength_twopair = (int)Oh.GetSymbol(Symbols.mh_str_twopair);

            OH.nplayersseated = (int)Oh.GetSymbol(Symbols.nplayersseated);
            OH.nchairs = (int)Oh.GetSymbol(Symbols.nchairs);


        }
    }
}
