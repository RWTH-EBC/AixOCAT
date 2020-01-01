///   ///   ///   ///   ///   ///   ///   ///   ///   
/// Beckhoff ADS driver (header-only).
///
/// @author		Georg Ferdinand Schneider <georg.schneider@ibp.fraunhofer.de>
/// @version	    2015-11-02 12:00:00
/// @since		2015-10-31
/// @copyright  Fraunhofer-Institute for Building Physics, Fuerther Strasse 250, 90429 Nuernberg, Germany
/// error codes for ADS-DLL fin in link below
/// http://infosys.beckhoff.de/content/1031/tc3_adscommon/html/ads_returncodes.htm?id=17776
 
#ifndef _ADSHEADERFILE_H_
#define _ADSHEADERFILE_H_

#include "ModelicaUtilities.h" // Use Modelica utilities

#if defined(_MSC_VER)
/// /// /// PRELIMINARIES /// /// ///
/// Standard header files
#include <windows.h>
#include <string.h>

/// ADS headers for TwinCAT 3
#include "C:\TwinCAT\AdsApi\TcAdsDll\Include\TcAdsDef.h"
#include "C:\TwinCAT\AdsApi\TcAdsDll\Include\TcAdsAPI.h"

/// Definitions
#define DEBUG_FLAG 0 // 1 == True, 0 == False
 
/// Function prototypes
// Intialize and end communication
int funAdsConstructor(int portNumber,int AmsNetID1,int AmsNetID2,int AmsNetID3,int AmsNetID4,int AmsNetID5,int AmsNetID6); 
int funAdsDestructor(void);
// Perform communication by handles


/// Global variables
AmsAddr   Addr; // Structure to store Addr
PAmsAddr  pAddr = &Addr; // Pointer to address
   


/* +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
/// /// /// FUNCTION /// /// ///
// Establish ADS connection
int funAdsConstructor(int portNumber,
					  int AmsNetID1,
					  int AmsNetID2,
					  int AmsNetID3,
					  int AmsNetID4,
					  int AmsNetID5,
					  int AmsNetID6)
{
	long nPort;
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Call AdsPortOpen()!\n");
	// ADS COMMUNICATION: Open communication port on the ADS router
	nPort = AdsPortOpen();
  			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Call AdsPortOpen() sucessful! Port used is: %i\n",nPort);
  			
	// Set port number and AMS net ID:
	pAddr->port = portNumber;
	 (*pAddr).netId.b[0] = AmsNetID1;
	 (*pAddr).netId.b[1] = AmsNetID2;
	 (*pAddr).netId.b[2] = AmsNetID3;
	 (*pAddr).netId.b[3] = AmsNetID4;
	 (*pAddr).netId.b[4] = AmsNetID5;
	 (*pAddr).netId.b[5] = AmsNetID6;
	 
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Call of funAdsConstructor() Sucessful!\n");
	return(0);
}

/// /// /// FUNCTION /// /// ///
// End connection
int funAdsDestructor(void)
{
	long nErr; // Variable for error codes from ADS 
	
	// Close communication port
	nErr = AdsPortClose();
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Call of AdsPortClose(), done!\n");
  	if (nErr != 0)
		{// Error checking
		ModelicaFormatMessage("Error: AdsPortClose(): %i\n",nErr);
		return(1);
		}
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("AdsPortClosed successful!\n");
	
return(0);
}  

/// /// /// FUNCTION /// /// ///
// Send double
int funAdsSendReal(double sendData,char* ptr_varName)
{
	long	nErr; // Variable for error handling
	ULONG	lHdlVar; // Variable to save handle
	double *p_SendData = &sendData; // Pointer to variable which contains data to be send
	unsigned long varNameLen = strlen(ptr_varName)+1; // Length of string of variable name + 1 because C strings end with a \0 character 
	

	// Step 1: Get variable handle
	nErr = AdsSyncReadWriteReq(pAddr,
							   ADSIGRP_SYM_HNDBYNAME,
							   0x0, 
							   sizeof(lHdlVar), 
							   &lHdlVar, 
							   varNameLen, 
							   ptr_varName);
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to fetch handle called!\n");
	if (nErr != 0)
		{
			ModelicaFormatMessage("\n");
			ModelicaFormatMessage("Error: Function to fetch handle failed: %i\n",nErr);
			ModelicaFormatMessage("Error: Variable name given is %s\n",ptr_varName);
			ModelicaFormatMessage("Error: Length of variable name given is %i\n",varNameLen);
			return(1);
		}
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function call to fetch handle sucessful!\n");
	
		// Write the sendData to the PLC
	nErr = AdsSyncWriteReq(pAddr, 
							ADSIGRP_SYM_VALBYHND,			// IndexGroup 
							lHdlVar,						// IndexOffset
							sizeof(sendData),				// Size of data to send
							p_SendData);					// Data to be send
	
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to send data called!\n");
	if (nErr != 0)
		{
			ModelicaFormatMessage("Error: Function to send data failed with code: %i\n",nErr);
			return(1);
		}
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to send data sucessful!\n");
	//Release handle of PLC variable
	nErr = AdsSyncWriteReq(pAddr,
							ADSIGRP_SYM_RELEASEHND,
							0,
							sizeof(lHdlVar),
							&lHdlVar); 
			if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to release handle called!\n");
	if (nErr != 0)
		{
			ModelicaFormatMessage("Error: Function to release handle failed with code: %i\n",nErr);
			return(1);
		}
		if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to release handle sucessful!\n");
		if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to write by handle in total successful! the data send is:\n");
		if (DEBUG_FLAG != 0) ModelicaFormatMessage("The send data is %f\n",sendData);
return(0);
} 

/// /// /// FUNCTION /// /// ///
// Receive double
int funAdsReceiveReal(double *p_RecvData,char* ptr_varName)
{
	long	nErr; // Variable for error handling
	ULONG	lHdlVar; // lHdlVar
	unsigned long varNameLen = strlen(ptr_varName)+1; // Length of string of variable name + 1 because C strings end with a \0 character 

	// Step 1: Get variable handle
	nErr = AdsSyncReadWriteReq(pAddr,
							   ADSIGRP_SYM_HNDBYNAME,
							   0x0, 
							   sizeof(lHdlVar), 
							   &lHdlVar, 
							   varNameLen, 
							   ptr_varName);
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to fetch handle called!\n");
	if (nErr != 0)
		{
		ModelicaFormatMessage("\n");
		ModelicaFormatMessage("Error: Function to fetch handle failed: %i\n",nErr);
		ModelicaFormatMessage("Error: Variable name given is is %s\n",ptr_varName);
		return(1);
		}
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function call to fetch handle sucessful!\n");
	
	// Read the recvData from the PLC
		nErr = AdsSyncReadReq(pAddr, 
							  ADSIGRP_SYM_VALBYHND,		// IndexGroup 
								lHdlVar,				// IndexOffset
								sizeof(*p_RecvData),	// Size of data to received
								p_RecvData);			// Data to be received
	
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to receive data called!\n");
	if (nErr != 0)
		{
		ModelicaFormatMessage("Error: Function to receive data failed with code: %i\n",nErr);
		return(1);
		}
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to receive data sucessful!\n");
	//Release handle of plc variable
	nErr = AdsSyncWriteReq(pAddr, ADSIGRP_SYM_RELEASEHND, 0, sizeof(lHdlVar), &lHdlVar); 
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to release handle called!\n");
	if (nErr != 0)
		{
		ModelicaFormatMessage("Error: Function to release handle failed with code: %i\n",nErr);
		return(1);
		}
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to release handle sucessful!\n");

	if (DEBUG_FLAG != 0) ModelicaFormatMessage("Function to receive/right by handle in total successful! the data read is:\n");
	if (DEBUG_FLAG != 0) ModelicaFormatMessage("The read data is %f\n",*p_RecvData);
return(0);
} 

/// /// /// FUNCTION /// /// ///
// Dummy function to check functionality
int add2(double a, double b, double *c)
{
	//return(a+b);
	*c = a+b;
	ModelicaFormatMessage("Call of add2() successful\n");
	return 0;
} 

/// /// /// FUNCTION /// /// ///
// Dummy function to check functionality of strings
int stringTester(const char *ptr_varName, int varNameLen)
{
	char      szVar []={"MAIN.myInputVar"}; // Specify variable which should be written
	unsigned long szVarLen = sizeof(szVar);
	
	unsigned long varNameLenCorrect = (unsigned long) varNameLen;
	
	
	ModelicaFormatMessage("Das ist szVar mit s: %s\n", szVar);
	ModelicaFormatMessage("Das ist szVar mit i: %i\n", szVar);
	ModelicaFormatMessage("Das ist szVar mit c: %c\n", szVar);
	ModelicaFormatMessage("Das ist sizeof(szVar) mit i: %i in Bytes\n",sizeof(szVar));
	ModelicaFormatMessage("Das ist szVarLen: %i\n", szVarLen);
	ModelicaFormatMessage("\n");
	
	ModelicaFormatMessage("Das ist ptr_varName mit s: %s\n", ptr_varName);
	ModelicaFormatMessage("Das ist ptr_varName mit i: %i\n", ptr_varName);
	ModelicaFormatMessage("Das ist ptr_varName mit c: %c\n", ptr_varName);
	ModelicaFormatMessage("Das ist sizeof(ptr_varName) mit i: %i in Bytes\n", strlen(ptr_varName));
	ModelicaFormatMessage("Das ist varNameLen mit i: %i\n", varNameLen);
	ModelicaFormatMessage("Das ist varNameLenCorrect mit i: %i\n", varNameLenCorrect);
	ModelicaFormatMessage("hello this is stringTester()");
	
	return 0;
} 

#endif /* defined(_MSC_VER) */
#endif /* _ADSHEADERFILE_H_ */