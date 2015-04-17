#include "dotnetholdem.h"
#include "cs.h"
#include <windows.h>
#include <atlstr.h>
#include <conio.h>

using namespace DotNetBotLogic;
using namespace System;

//card macros
#define RANK(c)			((c>>4)&0x0f)
#define SUIT(c)			((c>>0)&0x0f)
#define ISCARDBACK(c)	(c==0xff)
#define ISUNKNOWN(c)	(c==0)

//consecutive states
holdem_state	m_holdem_state[256];
unsigned char	m_ndx;

holdem_state *state = NULL;

const char* getPlayerName(int chairNumber)
{
	return state->m_player[chairNumber].m_name;
}

double process_query( const char* pquery ) 
{
	if (pquery==NULL)
		return 0;	

	System::String ^query = gcnew String(pquery);
	return cs::csInstance->MainControllerObject->ProcessQuery(query);
}

double process_state( holdem_state* pstate )
{
    System::IntPtr^ href=gcnew System::IntPtr(pstate);
    cs::csInstance->MainControllerObject->UpdateState(*href);
    return 0;
}


#pragma unmanaged

double getsym(const char* name)	
{ 
	bool error_flag;
	return GetSymbolFromDll(0, name, error_flag);
}

/////////////////////////////////////////////////////
// user.dll entry point
/////////////////////////////////////////////////////
USERDLL_API double process_message (const char* pmessage, const void* param) 
{
	if (pmessage==NULL) { return 0; }
	if (param==NULL) { return 0; }

	if (strcmp(pmessage,"state")==0) 
	{ 
		state = (holdem_state*) param;
				CString s; 
		s.Format("<%s>\nPOTS: %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f\nCOMMON: %d %d %d %d %d\n%d %d %d\n<%s> %d %.2f %d", 
			state->m_title,
			state->m_pot[0], state->m_pot[1], state->m_pot[2], state->m_pot[3], state->m_pot[4], 
			state->m_pot[5], state->m_pot[6], state->m_pot[7], state->m_pot[8], state->m_pot[9],
			state->m_cards[0], state->m_cards[1], state->m_cards[2], state->m_cards[3], state->m_cards[4],
			state->m_is_playing, state->m_is_posting, state->m_dealer_chair,
		state->m_player[4].m_name, state->m_player[4].m_name_known, state->m_player[4].m_balance, state->m_player[4].m_balance_known);
//	MessageBox(NULL, s, "state", MB_OK);

		//process_query( "wrere");
		return process_state( (holdem_state*)param ); 
	}

	if (strcmp(pmessage,"query")==0) 
	{ 
		//MessageBox(NULL, (LPCSTR) param, "query", MB_OK);
		return process_query( (const char*)param ); 
	}

	if (strcmp(pmessage,"event")==0 && strcmp((const char *) param, "load")==0) 
	{ 
		//MessageBox(NULL, "event-load", "MESSAGE", MB_OK);

	}

	if (strcmp(pmessage,"event")==0 && strcmp((const char *) param, "unload")==0) 
	{ 
	//	MessageBox(NULL, "event-unload", "MESSAGE", MB_OK);

	}

	return 0;
}
/////////////////////////////////////////////////////

BOOL APIENTRY DllMain(HANDLE hModule, DWORD ul_reason_for_call, LPVOID lpReserved) 
{
    switch (ul_reason_for_call)	
	{
		case DLL_PROCESS_ATTACH:
			AllocConsole();
			break;
		case DLL_THREAD_ATTACH:
			break;
		case DLL_THREAD_DETACH:
			break;
		case DLL_PROCESS_DETACH:
			FreeConsole();
			break;
    }
    return TRUE;
}

#pragma managed