/* DotNetHoldem 
 * .Net Solution that allows to use OpenHoldem with .Net logic
 * Author: Óscar Andreu
 * oesido at gmail dot com
 * Licensed under GPLv3 - 2012
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DotNetBotLogic.Enums;

namespace DotNetBotLogic.Classes
{

    internal class OH_Wrapper
    {
        #region OH function delegates


        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        private delegate double GetSymbolFromDllDelegate(string symbol);
        GetSymbolFromDllDelegate getSymbolFromDll;

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        private delegate string GetPlayerNameDelegate(int chair);
        GetPlayerNameDelegate getPlayerName;

        #endregion

        public OH_Wrapper(IntPtr getSymbolFromDll, IntPtr getPlayerName)
        {
            this.getSymbolFromDll = (GetSymbolFromDllDelegate)Marshal.GetDelegateForFunctionPointer(getSymbolFromDll, typeof(GetSymbolFromDllDelegate));
            this.getPlayerName = (GetPlayerNameDelegate)Marshal.GetDelegateForFunctionPointer(getPlayerName, typeof(GetPlayerNameDelegate));
        }
      
        internal string GetPlayerName(int chair)
        {
            return this.getPlayerName(chair);
        }

        internal double GetSym(string symbolName)
        {
            return this.getSymbolFromDll(symbolName);
        }        

        internal double GetSymbol(Symbols symbol)
        {
            return GetSym(symbol.ToString());
        }

        
    }

}
