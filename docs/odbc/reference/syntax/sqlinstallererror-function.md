---
title: "SQLInstallerError Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLInstallerError"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLInstallerError"
helpviewer_keywords: 
  - "SQLInstallerError [ODBC]"
ms.assetid: e6474b79-4d55-458f-81ce-abfafe357f83
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLInstallerError Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLInstallerError** returns error or status information for the ODBC installer functions.  
  
## Syntax  
  
```  
  
RETCODE SQLInstallerError(  
     WORD      iError,  
     DWORD *   pfErrorCode,  
     LPSTR     lpszErrorMsg,  
     WORD      cbErrorMsgMax,  
     WORD *    pcbErrorMsg);  
```  
  
## Arguments  
 *iError*  
 [Input] Error record number. Valid numbers are from 1 through 8.  
  
 *pfErrorCode*  
 [Output] Installer error code. (For more information, see "Comments.")  
  
 *lpszErrorMsg*  
 [Output] Pointer to storage for the error message text.  
  
 *cbErrorMsgMax*  
 [Input] Maximum length of the *szErrorMsg* buffer. This must be less than or equal to SQL_MAX_MESSAGE_LENGTH minus the null-termination character.  
  
 *cbErrorMsgMax*  
 [Input] Maximum length of the *szErrorMsg* buffer. This must be less than or equal to SQL_MAX_MESSAGE_LENGTH minus the null-termination character.  
  
 *pcbErrorMsg*  
 [Output] Pointer to the total number of bytes (excluding the null-termination character) available to return in *lpszErrorMsg*. If the number of bytes available to return is greater than or equal to *cbErrorMsgMax*, the error message text in *lpszErrorMsg* is truncated to *cbErrorMsgMax* minus the null-termination character bytes. The *pcbErrorMsg* argument can be a null pointer.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, or SQL_ERROR.  
  
## Diagnostics  
 **SQLInstallerError** does not post error values for itself. **SQLInstallerError** returns SQL_NO_DATA when it is unable to retrieve any error information (in which case *pfErrorCode* is undefined). If **SQLInstallerError** cannot access error values for any reason that would normally return SQL_ERROR, **SQLInstallerError** returns SQL_ERROR but does not post any error values. If you do not know the length of the warning string (*lpszErrorMsg*), you can set *lpszErrorMsg* to NULL and call **SQLInstallerError**. **SQLInstallerError** will then return the length of the warning string in *cbErrorMsgMax*. If the buffer for the error message is too short, **SQLInstallerError** returns SQL_SUCCESS_WITH_INFO and returns the correct *pfErrorCode* value for **SQLInstallerError**.  
  
 To determine whether a truncation occurred in the error message, an application can compare the value in the *cbErrorMsgMax* argument to the actual length of the message text written to the *pcbErrorMsg* argument. If truncation does occur, the correct buffer length should be allocated for *lpszErrorMsg* and **SQLInstallerError** should be called again with the corresponding *iError* record.  
  
## Comments  
 An application calls **SQLInstallerError** when a previous call to the ODBC installer function returns FALSE. ODBC installer and driver or translator setup functions post zero or more errors only when the function fails (returns FALSE); therefore, an application calls **SQLInstallerError** only after an ODBC installer function fails.  
  
 The ODBC installer error queue is flushed each time a new installer function is called. Therefore, an application cannot expect to retrieve errors for functions other than from the last installer function call.  
  
 To retrieve multiple errors for a function call, an application calls **SQLInstallerError** multiple times.  
  
 When there is no additional information, **SQLInstallerError** returns SQL_NO_DATA, the *pfErrorCode* argument is undefined, the *pcbErrorMsg* argument equals 0, and the *lpszErrorMsg* argument contains a single null-termination character (unless the *cbErrorMsgMax* argument is equal to 0).
