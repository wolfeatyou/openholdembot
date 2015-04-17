/* DotNetHoldem 
 * .Net Solution that allows to use OpenHoldem with .Net logic
 * Author: Óscar Andreu
 * oesido at gmail dot com
 * Licensed under GPLv3 - 2012
 */


namespace DotNetBotLogic.Enums
{
    /// <summary>
    /// Most common OH symbols, must be completed
    /// </summary>
    internal enum Symbols
    {
        ismanual, //true if you're in manual mode, false otherwise
        isppro, //true if you're connected to a ppro server
        site, //0=user/ppro 1=scraped
        nchairs, //the integer value for the Table Map symbol s$nchairs
        isbring, //true if OpenHoldem is attached to a bring client window
        session, //the current logging instance (0-9)
        handnumber, //the site hand number if available
        version, //returns the version number of OpenHoldem that is currently running
        swagdelay, //autoplayer delay in milliseconds between swag keystrokes and button click
        allidelay, //autoplayer delay in milliseconds between alli slider jam and button click
        swagtextmethod, //the site interpretation for swag edit text (Table Map symbol) 1=f$srai 2=f$srai+call 3=f$srai+call+currentbet
        rake, //percentage amount added/subtracted to/from the pot
        nit, //number of iterations used to calculate the prwin/prtie/prlos and associated symbols. For an analysis of how many iterations to use, go here: OpenHoldem:EndUserDocumentation:Iterations and Std Deviation
        bankroll, //the user defined real world bankroll
        bblind, //the big blind amount
        sblind, //the small blind amount
        ante, //the current pre-deal ante requirement
        lim, //the current table limit 0=NL 1=PL 2=FL
        isnl, //(lim==0)
        ispl, //(lim==1)
        isfl, //(lim==2)
        sraiprev, //the difference between the two largest unique wagers
        sraimin, //Scraped - (currentbet+call); PokerPro - the greater of bet and the current raise
        sraimax, //balance-call
        istournament, //true if a tournament table is detected
        handrank, //one of the following based on your selected option
        handrank169, //your pocket holdem hand rank 1-169 (See: http://www.winholdem.net/handrank.txt)
        handrank2652, //your pocket holdem hand rank 12-2652
        handrank1326, //your pocket holdem hand rank 6-1326 (handrank2652/2)
        handrank1000, //your pocket holdem hand rank 4-1000 (1000*handrank2652/2652)
        handrankp, //2652 / (1+nopponents)
        chair, //your chair number 0-9 ... 0 is usually top right
        userchair, //user chair number (0-9)
        dealerchair, //dealer chair number (0-9)
        raischair, //raising chair number (0-9)
        betround, //betting round (1-4) 1=preflop, 2=flop, 3=turn, 4=river
        br, //abbreviation for betround
        betposition, //your bet position (1=sblind,2=bblind,...,nplayersdealt=dealer). Betposition will change as players fold in front of you.
        dealposition, //your deal position (1=sblind,2=bblind ... nplayersdealt=dealer). Dealposition will not change as players fold.
        originaldealposition, // a memory of deal position which retains its value through the hand, even if the user folds (OH 1.8.3)
        callposition, //your numbered offset from the raising player (who is 0)
        seatposition, //your seat position relative to the dealer
        dealpositionrais, //the deal position of the raising player (1-10)
        betpositionrais, //the bet position of the raising player (1-10)
        prwin, //the probability of winning this hand (0.000 - 1.000)
        prlos, //the probability of losing this hand (0.000 - 1.000)
        prtie, //the probability of pushing this hand (0.000 - 1.000)
        prwinnow, //probability that all opponents have a lower hand right now
        prlosnow, //probability that any opponents have a higher hand right now
        random, //random number between (0.000-1.000). Value is recalculated each time symbol appears in formula.
        randomhand, //random number between (0.000-1.000) for the hand. Value is calculated only once per hand.
        randomround, //random number between (0.000-1.000) for the current round. Value is calculated only once in current round.
        defcon, //defense level used in P formula. Determines the number of analyzer opponents (0.000=maxoffense 1.000=maxdefense) (the defense level dialog uses values 0-10)
        isdefmode, //true when defcon is at max
        isaggmode, //true when defcon is at min
        balance, //your balance
        currentbet, //, //your current amount of chips in play
        call, //the amount you need to call
        bet, //the amount of a single initial bet or raise for current round
        pot, //the total amount of chips in play including player bets
        potcommon, //the total amount of chips in the middle
        potplayer, //the total amount of chips in front of all players
        callshort, //total amount that will be added to the pot if all players call
        raisshort, //callshort + bet * nplayersplaying
        nbetstocall, //total number of additional bets required to call.
        nbetstorais, //total number of additional bets required to raise.
        ncurrentbets, //total number of bets currently in front of you.
        ncallbets, //total number of bets you would have on the table if you call
        nraisbets, //total number of bets you would have on the table if you raise
        islistcall, //true if your hand is in list 0
        islistrais, //true if your hand is in list 1
        islistalli, //true if your hand is in list 7
        isemptylistcall, //true if the call list is empty
        isemptylistrais, //true if the rais list is empty
        isemptylistalli, //true if the alli list is empty
        nlistmax, //highest list number in which your hand is listed
        nlistmin, //lowest list number in which your hand is listed
        pokerval, //absolute poker value for your 5 card hand
        pokervalplayer, //absolute poker value for your 2 card pocket hand only.
        pokervalcommon, //absolute poker value for the common cards
        pcbits, //bit list of where your pocket cards are used in your 5 card hand
        npcbits, //number (0-2) of your pocket cards used in your 5 card hand
        hicard, //1<< 0 (2 ** 0)
        onepair, //1<<24 (2 ** 24)
        twopair, //1<<25 (2 ** 25)
        threeofakind, //1<<26 (2 ** 26)
        straight, //1<<27 (2 ** 27)
        flush, //1<<28 (2 ** 28)
        fullhouse, //1<<29 (2 ** 29)
        fourofakind, //1<<30 (2 ** 30)
        straightflush, //1<<31 (2 ** 31)
        royalflush, //0x800edcba
        fiveofakind, //0xff000000
        ishandup, //true if your hand has gone up a level (i.e. from 1 pair to 2 pair)
        ishandupcommon, //true if common hand has gone up a level (i.e. from 1 pair to 2 pair)
        ishicard, //true when you have hicard hand
        isonepair, //true when you have one pair
        istwopair, //true when you have two pair
        isthreeofakind, //true when you have three of a kind
        isstraight, //true when you have a straight
        isflush, //true when you have a flush
        isfullhouse, //true when you have a full house
        isfourofakind, //true when you have four of a kind
        isstraightflush, //true when you have a straight flush
        isroyalflush, //true when you have a royal flush
        isfiveofakind, //true when you have a five of a kind
        ispair, //true when your two dealt pocket cards are rank equal (0-1)
        issuited, //true when your two dealt pocket cards are suit equal (0-1)
        isconnector, //true when your two dealt pocket cards are rank adjacent (0-1)
        ishipair, //true when you have hi pair (0-1)
        islopair, //true when you have lo pair (0-1)
        ismidpair, //true when you have mid pair (0-1)
        ishistraight, //true when you have the highest straight possible
        ishiflush, //true when you have the highest flush possible
        nopponents, //P formula value for the userchair iterator
        nopponentsmax, //maximum allowable value for nopponents (1-22 default=9)
        nplayersseated, //number of players seated (including you) (0-10)
        nplayersactive, //number of players active (including you) (0-10)
        nplayersdealt, //number of players dealt (including you) (0-10)
        nplayersplaying, //number of players playing (including you) (0-10)
        nplayersblind, //number of players blind (including you) (0-10)
        nfriendsseated, //1 if you are seated, 0 otherwise (0-1)
        nfriendsactive, //1 if you are active, 0 otherwise (0-1)
        nfriendsdealt, //1 if you are dealt, 0 otherwise (0-1)
        nfriendsplaying, //1 if you are playing, 0 otherwise (0-1)
        nfriendsblind, //1 if you are in a blind, 0 otherwise (0-1)
        nopponentsseated, //number of opponents seated (not including you) (0-9)
        nopponentsactive, //number of opponents active (not including you) (0-9)
        nopponentsdealt, //number of opponents dealt (not including you) (0-9)
        nopponentsplaying, //number of opponents playing (not including you) (0-9)
        nopponentsblind, //number of opponents blind (not including you) (0-9)
        nopponentschecking, //number of opponents playing with a zero current bet equal to the previous bettor (0-9)
        nopponentscalling, //number of opponents playing with a non-zero current bet equal to the previous bettor (0-9)
        nopponentsraising, //number of opponents playing with a current bet greater than the previous bettor (0-9)
        nopponentsbetting, //number of opponents playing with a non zero current bet (0-9)
        nopponentsfolded, //number of opponents that have folded this hand (0-9)
        nplayerscallshort, //number of players that must call to stay in the hand
        nchairsdealtright, //number of chairs dealt before your chair
        nchairsdealtleft, //number of chairs dealt after your chair
        playersseatedbits, //bits 9-0: 1=seated 0=unseated
        playersactivebits, //bits 9-0: 1=active 0=inactive
        playersdealtbits, //bits 9-0: 1=dealt 0=notdealt
        playersplayingbits, //bits 9-0: 1=playing 0=notplaying
        playersblindbits, //bits 9-0: 1=blind 0=notblind
        opponentsseatedbits, //bits 9-0: 1=seated 0=unseated
        opponentsactivebits, //bits 9-0: 1=active 0=inactive
        opponentsdealtbits, //bits 9-0: 1=dealt 0=notdealt
        opponentsplayingbits, //bits 9-0: 1=playing 0=notplaying
        opponentsblindbits, //bits 9-0: 1=blind 0=notblind
        friendsseatedbits, //bits 9-0: 1=seated 0=unseated, you only
        friendsactivebits, //bits 9-0: 1=active 0=inactive, you only
        friendsdealtbits, //bits 9-0: 1=dealt 0=notdealt, you only
        friendsplayingbits, //bits 9-0: 1=playing 0=notplaying, you only
        friendsblindbits, //bits 9-0: 1=blind 0=notblind, you only
        fmax, //highest numbered flag button pressed
        fbits, //flag button bits 9-0 - 1=pressed 0=notpressed
        ncommoncardspresent, //number of common cards present (normal or highlighted)
        ncommoncardsknown, //number of common cards known (normal not highlighted)
        nflopc, //short for ncommoncardsknown
        nouts, //the total number of unseen single cards that if dealt to the board might put your hand in the lead. to be counted as an out, the card must be able to bump your level and your new level must be higher than the resulting common level
        ncardsknown, //total number of cards you can see (yours and commons)
        ncardsunknown, //total number of cards you cannot see (deck and opponents)
        ncardsbetter, //total number of single unknown cards that can beat you, e.g. if the board is four suited in hearts, and you have two spades, then ncardsbetter will be at least 9, because of the possible flush.
        nhands, //total possible number of two-card hands using the unseen cards (nhandshi+nhandslo+nhandsti)
        nhandshi, //number of hands that can beat you in a showdown right now
        nhandslo, //number of hands that you can beat in a showdown right now
        nhandsti, //number of hands that can tie you in a showdown right now
        nsuited, //total number of same suited cards you have (1-7)
        nsuitedcommon, //total number of same suited cards in the middle (1-5)
        tsuit, //specific card suit for nsuited (1-4)
        tsuitcommon, //specific card suit for nsuitedcommon (1-4)
        nranked, //total number of same ranked cards you have (1-4)
        nrankedcommon, //total number of same ranked cards in the middle (1-4)
        trank, //specific card rank for nranked (2-14)
        trankcommon, //specific card rank for nrankedcommon (2-14)
        nstraight, //total number of connected cards you have (1-7)
        nstraightcommon, //total number of connected common cards (1-5)
        nstraightfill, //total number of cards needed to fill a straight (0-5)
        nstraightfillcommon, //total number of cards needed to fill a common straight (0-5)
        nstraightflush, //total number of suited connected cards you have (1-7)
        nstraightflushcommon, //total number of suited connected common cards (1-5)
        nstraightflushfill, //total number of cards needed to fill a straightflush (0-5)
        nstraightflushfillcommon, //total number of cards needed to fill a common straightflush (0-5)
        rankbits, //bit list of card ranks (yours and commons)
        nbetsround1, //bit list of card ranks (commons)
        rankbitsplayer, //bit list of card ranks (yours)
        rankbitspoker, //bit list of card ranks (pokerval)
        srankbits, //bit list of suited card ranks (yours and commons tsuit)
        rankbitscommon,
        srankbitscommon, //bit list of suited card ranks (commons tsuitcommon)
        srankbitsplayer, //bit list of suited card ranks (yours tsuit)
        srankbitspoker, //bit list of suited card ranks (pokerval tsuit)
        rankhi, //highest card rank (14-2) (yours and commons)
        rankhicommon, //highest card rank (14-2) (commons)
        rankhiplayer, //highest card rank (14-2) (yours)
        rankhipoker, //highest card rank (14-2) (pokerval)
        srankhi, //highest suited card rank (14-2) (yours and commons tsuit)
        srankhicommon, //highest suited card rank (14-2) (commons tsuitcommon)
        srankhiplayer, //highest suited card rank (14-2) (yours tsuit)
        srankhipoker, //highest suited card rank (14-2) (pokerval tsuit)
        ranklo, //lowest card rank (14-2) (yours and commons)
        ranklocommon, //lowest card rank (14-2) (commons)
        rankloplayer, //lowest card rank (14-2) (yours)
        ranklopoker, //lowest card rank (14-2) (pokerval)
        sranklo, //lowest suited card rank (14-2) (yours and commons tsuit)
        sranklocommon, //lowest suited card rank (14-2) (commons tsuitcommon)
        srankloplayer, //lowest suited card rank (14-2) (yours tsuit)
        sranklopoker, //lowest suited card rank (14-2) (pokerval tsuit)
        elapsed, //time in seconds since sitting down
        elapsedhand, //time in seconds since end of previous hand
        elapsedauto, //time in seconds since autoplayer took action
        elapsedtoday, //time in seconds since midnight GMT
        elapsed1970, //time in seconds since 1970-01-01 00:00:00 GMT (THURSDAY)
        clocks, //number of cpu clocks since the last scrape
        nclockspersecond, //number of cpu clocks per second
        ncps, //number of cpu clocks per second
        myturnbits, //bits 43210 correspond to buttons KARCF (check alli rais call fold).
        ismyturn, //(myturnbits & 7) (rais or call/chec or fold)
        issittingin, //true when you are not being dealt out
        issittingout, //true when you are being dealt out
        isautopost, //true when you are autoposting
        isfinalanswer, //true when autoplayer preparing to act; false any other time.
        nplayersround, //number of players that began the current betting round
        prevaction, //record of previously attempted autoplayer action. (-1=fold 0=chec 1=call 2=rais 3=swag 4=alli)
        didchec, //the number of times the autoplayer has checked during the current round (*)
        didcall, //the number of times the autoplayer has called during the current round (*)
        didswag, //the number of times the autoplayer has raised during the current round (*)
        didrais, //the number of times the autoplayer has swag'd during the current round (*)
        nbetsround, //the largest number of bets in front of any player right now
        oppdealt, //Trailing indicator for nopponentsdealt
        ac_aggressor, //which chair was aggressor (might be from previous round)
        ac_agchair_after, //does the aggressor chair act after me?
        ac_preflop_pos, //preflop position of the userchair (SB=1 BB=2 Early=3 Middle=4 Late=5 Dealer=6)
        ac_prefloprais_pos, //preflop position of the raiser (SB=1 BB=2 Early=3 Middle=4 Late=5 Dealer=6)
        ac_postflop_pos, //postflop position of the userchair (first=1 early=2 middle=3 late=4 last=5)
        ac_pf_bets,
        ac_first_into_pot, //returns true if you are first into the pot (first to act or checked to you)
        mh_3straightxy, //(x=1 for wheel, 0 not, y=1 for broadway, 0 not) - returns true if the board has a wheel straight draw or broadway straight draw, given the wheel/broadway parameters
        mh_bottomsd, //true if I have a bottom straight draw (if you are contributing a single card to an open-ended straight draw and that card is the smallest, this symbol is true e.g. hole: T2 common: 345K)
        mh_nsuitedbetter, //number of missing suited cards that are higher than my best suited card
        mh_kickerbetter, //number of cards that can beat your kicker
        mh_kickerrank, //rank of your kicker (returns 0 if kicker is shared [board] and thus useless)
        mh_nouts, //number of outs (HTC's formula)
        mh_str_strflush, //0-5 (5 best) of the relative strength of your straight flush
        mh_str_quads, //0-5 (5 best) of the relative strength of your four of a kind
        mh_str_fullhouse, //0-5 (5 best) of the relative strength of your full house
        mh_str_flush, //0-5 (5 best) of the relative strength of your flush
        mh_str_straight, //0-5 (5 best) of the relative strength of your straight
        mh_str_trips, //0-5 (5 best) of the relative strength of your three of a kind
        mh_str_twopair, //0-5 (5 best) of the relative strength of your two pair
        mh_str_onepair, //0-5 (5 best) of the relative strength of your one pair
        floppct, //percentage of players seeing the flop for the last y minutes
        turnpct, //percentage of players seeing the turn for the last y minutes
        riverpct, //percentage of players seeing the river for the last y minutes
        avgbetspf, //average number of bets preflop for the last y minutes
        tablepfr, //pfr percentage preflop for the last y minutes
        maxbalance, //my highest balance during the session
        handsplayed, //number of hands played this session
        didraisround1,
        didraisround2,
        didcallround1,
        didcallround2,
        didraisround3,
        didcallround3,
        nbetsround2,
        nbetsround3,
        nbetsround4,
        currentbet0,
        currentbet1,
        currentbet2,
        currentbet3,
        currentbet4,
        currentbet5,
        currentbet6,
        currentbet7,
        currentbet8,
        currentbet9,
        raisbits1, 
        raisbits2,
        didswaground1,
        didswaground2,
        didswaground3,
        balance0,
        balance1,
        balance2,
        balance3,
        balance4,
        balance5,
        nplayersround2,
        bet4
    }
}
