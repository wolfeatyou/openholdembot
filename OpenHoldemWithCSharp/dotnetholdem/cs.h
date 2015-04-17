#pragma once

#include "dotnetholdem.h"

using namespace DotNetBotLogic;

	/// <summary>
	/// Summary for cs
	/// </summary>
	public ref class cs
	{
		cs(void)
		{
			MainControllerObject = gcnew MainController(System::IntPtr(getsym), System::IntPtr(getPlayerName));
		}

	public:
		
		MainController^ MainControllerObject;
		static cs^ csInstance = gcnew cs(); 

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~cs()
		{
			
		}
		

	};

