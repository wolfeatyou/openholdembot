using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.Classes
{
    class Hand
    {
        #region Constants
        const int A = 14;
        const int K = 13;
        const int Q = 12;
        const int J = 11;
        const int T = 10;         
        #endregion

        #region Hand values

        // pocket pairs
        public bool IsAA { get { return FirstCard.Rank >= A && this.ispair; } }
        public bool IsKK { get { return FirstCard.Rank >= K && this.ispair; } }
        public bool IsQQ { get { return FirstCard.Rank >= Q && this.ispair; } }
        public bool IsJJ { get { return FirstCard.Rank >= J && this.ispair; } }
        public bool IsTT { get { return FirstCard.Rank >= T && this.ispair; } }
        public bool Is99 { get { return FirstCard.Rank >= 9 && this.ispair; } }
        public bool Is88 { get { return FirstCard.Rank >= 8 && this.ispair; } }
        public bool Is77 { get { return FirstCard.Rank >= 7 && this.ispair; } }
        public bool Is66 { get { return FirstCard.Rank >= 6 && this.ispair; } }

        // aces
        public bool IsAK { get { return FirstCard.Rank == A && SecondCard.Rank >= K; } }
        public bool IsAQ { get { return FirstCard.Rank == A && SecondCard.Rank >= Q; } }
        public bool IsAJ { get { return FirstCard.Rank == A && SecondCard.Rank >= J; } }
        public bool IsAT { get { return FirstCard.Rank == A && SecondCard.Rank >= T; } }
        public bool IsA9 { get { return FirstCard.Rank == A && SecondCard.Rank >= 9; } }
        public bool IsA8 { get { return FirstCard.Rank == A && SecondCard.Rank >= 8; } }
        public bool IsA7 { get { return FirstCard.Rank == A && SecondCard.Rank >= 7; } }

        public bool IsAx { get { return FirstCard.Rank == 14; } }

        // kings
        public bool IsKQ { get { return FirstCard.Rank == K && SecondCard.Rank >= Q; } }
        public bool IsKJ { get { return FirstCard.Rank == K && SecondCard.Rank >= J; } }
        public bool IsKT { get { return FirstCard.Rank == K && SecondCard.Rank >= T; } }
        public bool IsK9 { get { return FirstCard.Rank == K && SecondCard.Rank >= 9; } }

        public bool isKxs { get { return FirstCard.Rank == K && this.issuited; } }

        // queens
        public bool IsQJ { get { return FirstCard.Rank == Q && SecondCard.Rank >=  J; } }
        public bool IsQT { get { return FirstCard.Rank == Q && SecondCard.Rank >=  T; } }
        public bool IsQ9 { get { return FirstCard.Rank == Q && SecondCard.Rank >=  9; } }
        public bool IsQ8 { get { return FirstCard.Rank == Q && SecondCard.Rank >=  8; } }

        // jacks
        public bool IsJT { get { return FirstCard.Rank == J && SecondCard.Rank >= T; } }
        public bool IsJ9 { get { return FirstCard.Rank == T && SecondCard.Rank >= 9; } }
        public bool IsJ8 { get { return FirstCard.Rank == 9 && SecondCard.Rank >= 8; } }        

        // tens
        public bool IsT9 { get { return FirstCard.Rank == T && SecondCard.Rank >= 9; } }
        public bool IsT8 { get { return FirstCard.Rank == T && SecondCard.Rank >= 8; } }

        // connectors
        public bool Is98 { get { return FirstCard.Rank == 9 && SecondCard.Rank >= 8; } }
        public bool Is87 { get { return FirstCard.Rank == 8 && SecondCard.Rank >= 7; } }        

        public int Sum { get { return FirstCard.Rank + SecondCard.Rank; } }

        #endregion

        #region Public properties
        
        public Card FirstCard { get; set; }

        public Card SecondCard { get; set; }

        public bool issuited { get { return FirstCard.Suit == SecondCard.Suit; } }

        public bool ispair { get { return FirstCard.Rank == SecondCard.Rank; } }

        public bool isconnector { get { return (FirstCard.Rank - SecondCard.Rank) == 1; } }       

        #endregion

        #region Constructor
        
        public Hand()
        {
            FirstCard = new Card();
            SecondCard = new Card();
        } 

        #endregion

        #region Public methods
        public void SetValues(int myChair)
        {
            int rank1 = ((MainController.mhs.m_player[myChair].m_cards[0]) >> 4) & 0x0f;
            int rank2 = ((MainController.mhs.m_player[myChair].m_cards[1]) >> 4) & 0x0f;

            if (rank1 >= rank2)
            {
                FirstCard.Rank = ((MainController.mhs.m_player[myChair].m_cards[0]) >> 4) & 0x0f;
                FirstCard.Suit = (Symbol)((int)((MainController.mhs.m_player[myChair].m_cards[0]) >> 0) & 0x0f);
                SecondCard.Rank = ((MainController.mhs.m_player[myChair].m_cards[1]) >> 4) & 0x0f;
                SecondCard.Suit = (Symbol)((int)((MainController.mhs.m_player[myChair].m_cards[1]) >> 0) & 0x0f);
            }
            else
            {
                SecondCard.Rank = ((MainController.mhs.m_player[myChair].m_cards[0]) >> 4) & 0x0f;
                SecondCard.Suit = (Symbol)((int)((MainController.mhs.m_player[myChair].m_cards[0]) >> 0) & 0x0f);
                FirstCard.Rank = ((MainController.mhs.m_player[myChair].m_cards[1]) >> 4) & 0x0f;
                FirstCard.Suit = (Symbol)((int)((MainController.mhs.m_player[myChair].m_cards[1]) >> 0) & 0x0f);
            }
        }
       
        #endregion

        #region Hand ranges

        /// <summary>
        /// 22+ A7+ KT+ QT+ J9+ T9 A2s+ Q9s+ J8s+ 98s+
        /// </summary>
        /// <returns></returns>
        public bool RangeAperturaBTNconLimpers()
        {            
            if (this.ispair)
                return true;

            // A7+ KT+ QT+ J9+ T9 
            if (this.IsAT || this.IsKT || this.IsQT || this.IsJ9)
                return true;

            // A2s+ Q9s+ J8s+ 98s+
            if (this.issuited && (this.IsAx || this.IsQ9 || this.IsJ8 || this.IsT9 || this.Is98))
                return true;

            return false;    
        }

        /// <summary>
        /// 22+ A7+ KT+ Q9+ J9+ T9 98 A2s+ Q8s+ J8s+ T8s+ 87s
        /// </summary>
        /// <returns></returns>
        public bool RangeAperturaBTN()
        {
            // tutte le coppie
            if (this.ispair)
                return true;

            // A7+ KT+ Q9+ J9+ T9 98 
            if (this.IsA7 || this.IsKT || this.IsQ9 || this.IsJ9 || this.IsT9 || this.Is98)
                return true;

            // A2s+ Q8s+ J8s+ T8s+ 87s
            if (this.issuited && (this.IsAx ||this.IsK9 || this.IsQ8 || this.IsJ8 || this.IsT8 || this.IsT9 || this.Is98 || this.Is87))
                return true;

            return false;    
        }

        /// <summary>
        /// 66+ AJ+ KQ+ KJs+
        /// </summary>
        /// <returns></returns>
        public bool RangeAperturaUTG()
        {
            // 66+
            if (this.Is77)
                return true;

            // AJ+ KQ
            if (this.IsAJ || this.IsKQ)
                return true;

            // KJs
            if (this.issuited && this.IsKJ)
                return true;

            return false;
        }

        /// <summary>
        ///  22+ A9+ KT+ Q9+ JT+ A2s+ Q8s+ J9s+ T9s+ 98s
        /// </summary>
        public bool RangeAperturaCO()
        {
            // tutte le coppie
            if (this.ispair)
                return true;

            // A9+ KT+ Q9+ JT
            if (this.IsA9 || this.IsKT || this.IsQ9 || this.IsJT)
                return true;

            // A2s+ Q8s+ J9s+ T9s+ 98s
            if (this.issuited && (this.IsA7 || this.IsQ9 || this.IsJ9 || (this.Sum >= 9 + 8 && this.isconnector)))
                return true;

            return false;    
        }      

        /// <summary>
        /// Range di re-raise
        /// </summary>
        /// <returns></returns>
        public bool RangeNutsPreflop()
        {
            if (this.IsQQ)
                return true;

            if (this.IsAK)
                return true;

            return false;
        }       

        /// <summary>
        /// 22+ AQ
        /// </summary>
        /// <returns></returns>
        public bool RangeCallTribet()
        {
            if (this.ispair)
                return true;

            if (this.IsAQ)
                return true;

            return false;

        }

        public bool TribetMinRaise() 
        {
            if (this.Is99 || this.IsAJ)
                return true;
            return false;        
        }

        /// <summary>
        /// pairs 22+ KQ+ QJs JTs
        /// </summary>
        /// <returns></returns>
        public bool RangeCallBtnCo() 
        {
            if (this.ispair) return true;
            if (this.IsAJ) return true;
            if (this.IsKQ) return true;
            if (this.issuited && ( this.IsJT || this.IsAT)) return true;
            return false;
        }        
        
       


        #endregion
    }
}
