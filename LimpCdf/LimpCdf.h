// LimpCdf.h : main header file for the LimpCdf DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols
#include "netcdf.h"

// CLimpCdfApp
// See LimpCdf.cpp for the implementation of this class
//

class CLimpCdfApp : public CWinApp
{
public:
	CLimpCdfApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
