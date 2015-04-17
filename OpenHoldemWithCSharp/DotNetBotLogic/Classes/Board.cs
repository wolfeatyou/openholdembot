using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.Classes
{
    class Board
    {
        #region Constants
        const int A = 14;
        const int K = 13;
        const int Q = 12;
        const int J = 11;
        const int T = 10;
        #endregion

        #region Public properties

        public Card FlopFirstCard { get; set; }

        public Card FlopSecondCard { get; set; }

        public Card FlopThirdCard { get; set; }

        public Card TurnCard { get; set; }

        public Card RiverCard { get; set; } 

        public List<int> CardList { get { return new List<int>() { FlopFirstCard.Rank, FlopSecondCard.Rank, FlopThirdCard.Rank, TurnCard.Rank,RiverCard.Rank }; } }

        public bool IsPairedAtFlop { get { return FlopFirstCard.Rank == FlopSecondCard.Rank || FlopFirstCard.Rank == FlopThirdCard.Rank || FlopSecondCard.Rank == FlopThirdCard.Rank; } }

        public bool IsPairedAtTurn { get { return TurnCard.Rank == FlopFirstCard.Rank || TurnCard.Rank == FlopSecondCard.Rank || TurnCard.Rank == FlopThirdCard.Rank; } }

        #endregion

        public Board() 
        {
            FlopFirstCard = new Card();
            FlopSecondCard = new Card();
            FlopThirdCard = new Card();
            TurnCard = new Card();
            RiverCard = new Card();
        }
        

        public void SetBoardCards() 
        {
            FlopFirstCard.Rank = ((MainController.mhs.m_cards[0]) >> 4) & 0x0f;
            FlopFirstCard.Suit = (Symbol)(((MainController.mhs.m_cards[0]) >> 0) & 0x0f);
            FlopSecondCard.Rank = ((MainController.mhs.m_cards[1]) >> 4) & 0x0f;
            FlopSecondCard.Suit = (Symbol)(((MainController.mhs.m_cards[1]) >> 0) & 0x0f);
            FlopThirdCard.Rank = ((MainController.mhs.m_cards[2]) >> 4) & 0x0f;
            FlopThirdCard.Suit = (Symbol)(((MainController.mhs.m_cards[2]) >> 0) & 0x0f);
            TurnCard.Rank = ((MainController.mhs.m_cards[3]) >> 4) & 0x0f;
            TurnCard.Suit = (Symbol)(((MainController.mhs.m_cards[3]) >> 0) & 0x0f);
            RiverCard.Rank = ((MainController.mhs.m_cards[4]) >> 4) & 0x0f;
            RiverCard.Suit = (Symbol)(((MainController.mhs.m_cards[4]) >> 0) & 0x0f);
        }  

        /// <summary>
        /// ##f$openboard##
        /// impossible to have a boat ,flush, or straight; top set is nuts 
        /// </summary>
        public bool OpenBoard{get{ return OH.nrankedcommon==1 && OH.nsuitedcommon<=2 && OH.nstraightfillcommon>=3;}}

        /// <summary>
        /// ##f$draw_flop##
        /// </summary>
        /// <returns></returns>
        public bool draw_flop()
        {
            if (OH.BetRound == Rounds.Flop && (
               FlopFirstCard.Suit == FlopSecondCard.Suit
                || FlopFirstCard.Suit == FlopThirdCard.Suit
                || FlopSecondCard.Suit == FlopThirdCard.Suit
                || (FlopFirstCard.Rank - 1) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank - 1) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank - 1) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank + 1) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank + 1) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank + 1) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank - 2) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank - 2) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank - 2) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank + 2) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank + 2) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank + 2) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank - 3) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank - 3) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank - 3) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank + 3) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank + 3) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank + 3) == FlopThirdCard.Rank)
                )
                return true;
            return false;
        }

        /// <summary>
        /// ##f$draw_turn##
        /// </summary>
        /// <returns></returns>
        public bool draw_turn()
        {
            if (OH.BetRound == Rounds.Turn &&
            (
               FlopFirstCard.Suit == FlopSecondCard.Suit
                || FlopFirstCard.Suit == FlopThirdCard.Suit
                || FlopFirstCard.Suit == TurnCard.Suit
                || FlopSecondCard.Suit == FlopThirdCard.Suit
                || FlopSecondCard.Suit == TurnCard.Suit
                || FlopThirdCard.Suit == TurnCard.Suit
                || (FlopFirstCard.Rank - 1) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank - 1) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank - 1) == TurnCard.Rank
                || (FlopSecondCard.Rank - 1) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank - 1) == TurnCard.Rank
                || (FlopThirdCard.Rank - 1) == TurnCard.Rank
                || (FlopFirstCard.Rank + 1) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank + 1) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank + 1) == TurnCard.Rank
                || (FlopSecondCard.Rank + 1) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank + 1) == TurnCard.Rank
                || (FlopThirdCard.Rank + 1) == TurnCard.Rank
                || (FlopFirstCard.Rank - 2) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank - 2) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank - 2) == TurnCard.Rank
                || (FlopSecondCard.Rank - 2) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank - 2) == TurnCard.Rank
                || (FlopThirdCard.Rank - 2) == TurnCard.Rank
                || (FlopFirstCard.Rank + 2) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank + 2) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank + 2) == TurnCard.Rank
                || (FlopSecondCard.Rank + 2) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank + 2) == TurnCard.Rank
                || (FlopThirdCard.Rank + 2) == TurnCard.Rank
                || (FlopFirstCard.Rank - 3) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank - 3) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank - 3) == TurnCard.Rank
                || (FlopSecondCard.Rank - 3) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank - 3) == TurnCard.Rank
                || (FlopThirdCard.Rank - 3) == TurnCard.Rank
                || (FlopFirstCard.Rank + 3) == FlopSecondCard.Rank
                || (FlopFirstCard.Rank + 3) == FlopThirdCard.Rank
                || (FlopFirstCard.Rank + 3) == TurnCard.Rank
                || (FlopSecondCard.Rank + 3) == FlopThirdCard.Rank
                || (FlopSecondCard.Rank + 3) == TurnCard.Rank
                || (FlopThirdCard.Rank + 3) == TurnCard.Rank))
                return true;
            return false;
        }

        public bool IsPaired { get { return (OH.br == 2 && PairedAtFlop) || (OH.br == 3 && PairedAtTurn) || (OH.br == 3 && PairedAtRiver); } }

        public bool PairedAtFlop { get { return OH.Board.FlopFirstCard.Rank == OH.Board.FlopSecondCard.Rank || OH.Board.FlopFirstCard.Rank == OH.Board.FlopThirdCard.Rank || OH.Board.FlopSecondCard.Rank == OH.Board.FlopThirdCard.Rank; } }

        public bool PairedAtTurn{get{return OH.Board.TurnCard.Rank == OH.Board.FlopFirstCard.Rank || OH.Board.TurnCard.Rank == OH.Board.FlopSecondCard.Rank || OH.Board.TurnCard.Rank == OH.Board.FlopThirdCard.Rank;}}

        public bool PairedAtRiver { get { return RiverCard.Rank == FlopFirstCard.Rank || RiverCard.Rank == FlopSecondCard.Rank || RiverCard.Rank == FlopThirdCard.Rank || RiverCard.Rank == TurnCard.Rank; } }

        /// <summary>
        /// A flop in which the board doesn't contain any flush or straight draws. You usually can play your stronger hands slower on this type of board. 
        /// </summary>
        public bool DryFlop{ get { return OH.nsuitedcommon < 2 && OH.nstraightcommon < 2; } }

        /// <summary>
        ///  ##f$SAFEBOARD##
        /// </summary>
        public bool SAFEBOARD{get{return (OH.nrankedcommon<2 &&  OH.nsuitedcommon<3 &&  OH.nstraightcommon<3 && OH.nstraightfillcommon>1 )
                                            && !(OH.br>2 && OH.rankhicommon==14 && OH.rankhicommon>OH.rankhiplayer);}}

        /// <summary>
        /// ##f$rainbowboard##
        /// The safest board, no pair, no draws to a straight or flush
        /// </summary>
        public bool rainbowboard { get { return OH.nsuitedcommon == 1 && OH.nstraightfillcommon >= 3 && OH.nrankedcommon == 1; } }        
  
        /// <summary>
        /// Board connesso tipo QKJ, QT9 etc...
        /// </summary>
        public bool IsBroadwayFlop { get { return (CardList.Where(x => x > 0).Max() - CardList.Where(x => x > 0).Min() <= 3) && !this.IsPaired; } }

        public int GetOverCards()
        {
            var overcards = CardList.Where(rank => rank > OH.MyHand.FirstCard.Rank).ToList<int>();
            int result = overcards != null ? overcards.Count : 0;
            return result;
        }

        /// <summary>
        /// 225, 244 etc...
        /// </summary>
        public bool IsLowBoard { get { return CardList.Max() < 5; } }
    }
}
