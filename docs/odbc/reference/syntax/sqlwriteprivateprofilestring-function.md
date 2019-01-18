---
title: "SQLWritePrivateProfileString Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLWritePrivateProfileString"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLWritePrivateProfileString"
helpviewer_keywords: 
  - "SQLWritePrivateProfileString [ODBC]"
ms.assetid: 526f36a4-92ed-4874-9725-82d27c0b86f9
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLWritePrivateProfileString Function
**Conformance**  
 Version Introduced: ODBC 2.0  
  
 **Summary**  
 **SQLWritePrivateProfileString** writes a value name and data to the Odbc.ini subkey of the system information.  
  
## Syntax  
  
```  
  
BOOL SQLWritePrivateProfileString(  
     LPCSTR     lpszSection,  
     LPCSTR     lpszEntry,  
     LPCSTR     lpszString,  
     LPCSTR     lpszFilename);  
```  
  
## Arguments  
 *lpszSection*  
 [Input] Points to a null-terminated string containing the name of the section to which the string will be copied. If the section does not exist, it is created. The name of the section is case-independent; the string can be any combination of uppercase and lowercase letters.  
  
 *lpszEntry*  
 [Input] Points to a null-terminated string containing the name of the key to be associated with a string. If the key does not exist in the specified section, it is created. If this argument is NULL, the entire section, including all entries within the section, is deleted.  
  
 *lpszString*  
 [Input] Points to a null-terminated string to be written to the file. If this argument is NULL, the key pointed to by the *lpszEntry* argument is deleted.  
  
 *lpszFilename*  
 [Output] Points to a null-terminated string that names the initialization file.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLWritePrivateProfileString** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_REQUEST_FAILED|Request failed|The requested system information could not be written.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLWritePrivateProfileString** is provided as a simple way to port drivers and driver setup DLLs from Microsoft® Windows® to Microsoft Windows NT®/Windows 2000. Calls to **WritePrivateProfileString** that write a profile string to the Odbc.ini file should be replaced with calls to **SQLWritePrivateProfileString**. **SQLWritePrivateProfileString** calls functions in the Win32® API to add the specified value name and data to the Odbc.ini subkey of the system information.  
  
 A configuration mode indicates where the Odbc.ini entry listing DSN values is in the system information. If the DSN is a User DSN (the state variable is USERDSN_ONLY), the function writes to the Odbc.ini entry in HKEY_CURRENT_USER. If the DSN is a System DSN (SYSTEMDSN_ONLY), the function writes to the Odbc.ini entry in HKEY_LOCAL_MACHINE. If the state variable is BOTHDSN, HKEY_CURRENT_USER is tried, and if it fails, HKEY_LOCAL_MACHINE is used.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Getting a value from the system information|[SQLGetPrivateProfileString](../../../odbc/reference/syntax/sqlgetprivateprofilestring-function.md)|
