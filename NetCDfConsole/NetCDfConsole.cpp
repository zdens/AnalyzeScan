// NetCDfConsole.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <netcdf.h>

int main()
{
	int status, ncid;
	char filePath[] = "d:\\Xcalibur\\data\\Dina\\capillar\\08.08\\cdf\\capilar orb S4 V3_3.cdf";

	status = nc_open(filePath, NC_NOWRITE, &ncid);

    return 0;
}

