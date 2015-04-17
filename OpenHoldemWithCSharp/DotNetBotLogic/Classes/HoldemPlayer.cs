/* DotNetHoldem 
 * .Net Solution that allows to use OpenHoldem with .Net logic
 * Author: Óscar Andreu
 * oesido at gmail dot com
 * Licensed under GPLv3 - 2012
 * http://www.maxinmontreal.com/forums/viewtopic.php?f=174&t=8628
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DotNetBotLogic.Classes
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct managed_holdem_player
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string m_name;

        public double m_balance;
        public double m_currentbet;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] m_cards;

        public byte m_is_known;
        public byte m_fillerbyte;

        public bool NameKnown { get { return (m_is_known & 0x01) == 0x01; } }
        public bool BalanceKnown { get { return (m_is_known & 0x02) == 0x02; } }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct managed_holdem_state
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_title;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public double[] m_pot;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] m_cards;

        public byte m_playing_bits;

        public byte m_fillerbyte;
        public byte m_dealer_chair;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 10)]
        public managed_holdem_player[] m_player;

        public bool IsPlaying { get { return (m_playing_bits & 0x01) == 0x01; } }
        public bool IsPosting { get { return (m_playing_bits & 0x02) == 0x02; } }
    }
}
