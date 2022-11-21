---
description: "SQLGetPrivateProfileString Function"
title: "SQLGetPrivateProfileString Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLGetPrivateProfileString"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetPrivateProfileString"
helpviewer_keywords: 
  - "SQLGetPrivateProfileString function [ODBC]"
ms.assetid: b72ca065-4d67-48df-baac-e18379a8320a
author: David-Engel
ms.author: v-davidengel
---
# SQLGetPrivateProfileString Function
**Conformance**  
 Version Introduced: ODBC 2.0  
  
 **Summary**  
 **SQLGetPrivateProfileString** gets a list of names of values or data corresponding to a value of the system information.  
  
## Syntax  
  
```cpp  
  
int SQLGetPrivateProfileString(  
     LPCSTR   lpszSection,  
     LPCSTR   lpszEntry,  
     LPCSTR   lpszDefault,  
     LPCSTR   RetBuffer,  
     INT      cbRetBuffer,  
     LPCSTR   lpszFilename);  
```  
  
## Arguments  
 *lpszSection*  
 [Input] Points to a null-terminated string that specifies the section containing the key name. If this argument is NULL, the function copies all section names in the file to the supplied buffer.  
  
 *lpszEntry*  
 [Input] Points to the null-terminated string containing the key name whose associated string is to be retrieved. If this argument is NULL, all key names in the section specified by the *lpszSection* argument are copied to the buffer specified by the *RetBuffer* argument.  
  
 *lpszDefault*  
 [Input] Points to a null-terminated string that specifies the default value for the given key if the key cannot be found in the initialization file. This argument cannot be NULL.  
  
 *RetBuffer*  
 [Output] Points to the buffer that receives the retrieved string.  
  
 *cbRetBuffer*  
 [Input] Specifies the size, in characters, of the buffer pointed to by the *RetBuffer* argument.  
  
 *lpszFilename*  
 [Input] Points to a null-terminated string that names the initialization file. If this argument does not contain a full path to the file, the default directory is searched.  
  
## Returns  
 **SQLGetPrivateProfileString** returns an integer value that indicates the number of characters read.  
  
## Diagnostics  
 When a call to **SQLGetPrivateProfileString** fails, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLGetPrivateProfileString** is provided as a simple way to port drivers and driver setup DLLs from Microsoft® Windows® to Microsoft Windows NT®/Windows 2000. Calls to **GetPrivateProfileString** that retrieve a profile string from the Odbc.ini file should be replaced with calls to **SQLGetPrivateProfileString**. **SQLGetPrivateProfileString** calls functions in the Win32® API to retrieve the requested names of values or data corresponding to a value of the Odbc.ini subkey of the system information.  
  
 The configuration mode (as set by **SQLSetConfigMode**) indicates where the Odbc.ini entry listing DSN values is in the system information. If the DSN is a User DSN (the configuration mode is USERDSN_ONLY), the function reads from the Odbc.ini entry in HKEY_CURRENT_USER. If the DSN is a System DSN (SYSTEMDSN_ONLY), the function reads from the Odbc.ini entry in HKEY_LOCAL_MACHINE. If the configuration mode is BOTHDSN, HKEY_CURRENT_USER is tried, and if it fails, HKEY_LOCAL_MACHINE is used.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Writing a value to the system information|[SQLWritePrivateProfileString](../../../odbc/reference/syntax/sqlwriteprivateprofilestring-function.md)|
