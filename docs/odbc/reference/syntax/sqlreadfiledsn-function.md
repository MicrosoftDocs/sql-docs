---
description: "SQLReadFileDSN Function"
title: "SQLReadFileDSN Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLReadFileDSN"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLReadFileDSN"
helpviewer_keywords: 
  - "SQLReadFileDSN function [ODBC]"
ms.assetid: ead464aa-cdc3-47dd-a0c0-997711205d31
author: David-Engel
ms.author: v-davidengel
---
# SQLReadFileDSN Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLReadFileDSN** reads information from a File DSN.  
  
## Syntax  
  
```cpp  
  
BOOL SQLReadFileDSN(  
     LPCSTR   lpszFileName,  
     LPCSTR   lpszAppName,  
     LPCSTR   lpszKeyName,  
     LPSTR    lpszString,  
     WORD     cbString,  
     WORD *   pcbString);  
```  
  
## Arguments  
 *lpszFileName*  
 [Input] Pointer to the data buffer containing the name of the .dsn file. A .dsn extension is appended to all file names that do not already have a .dsn extension. The value in *\*lpszFileName* must be a null-terminated string.  
  
 *lpszAppName*  
 [Input] Pointer to the data buffer containing the name of the application. This is "ODBC" for the ODBC section. The value in *\*lpszAppName* must be a null-terminated string.  
  
 *lpszKeyName*  
 [Input] Pointer to the data buffer containing the name of the key to be read. See "Comments" for reserved keywords. The value in *\*lpszAppName* must be a null-terminated string.  
  
 *lpszString*  
 [Output] Pointer to the data buffer containing the string associated with the key to be read.  
  
 If *\*lpszFileName* is a valid .dsn file name but the *lpszAppName* argument is a null pointer and the *lpszKeyName* argument is a null pointer, then *\*lpszString* contains a list of valid applications. If *\*lpszFileName* is a valid .dsn file name and *\*lpszAppName* is a valid application name, but the *lpszKeyName* argument is a null pointer, then *\*lpszString* contains a list of valid reserved keywords in the appropriate section of the DSN file, delimited by semicolons. If *\*lpszFileName* is a valid .dsn file name but *\*lpszAppName* is a null pointer and the *lpszKeyName* argument is a null pointer, then *\*lpszString* contains a list of the sections in the DSN file, delimited by semicolons.  
  
 *cbString*  
 [Input] Length of the *\*lpszString* buffer.  
  
 *pcbString*  
 [Output] Total number of bytes available to return in *\*lpszString*. If the number of bytes available to return is greater than or equal to *cbString*, the output string in *\*lpszString* is truncated to *cbString* minus the null-termination character. The *pcbString* argument can be a null pointer.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLReadFileDSN** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_BUFF_LEN|Invalid buffer length|The *lpszString* argument was NULL.<br /><br /> The *cbString* argument was less than or equal to 0.|  
|ODBC_ERROR_INVALID_PATH|Invalid install path|The path of the file name specified in the *lpszFileName* argument was invalid.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *lpszAppName* argument was NULL, while the *lpszKeyName* argument was valid.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
|ODBC_ERROR_OUTPUT_STRING_TRUNCATED|Output string truncated|The string returned in *\*lpszString* was truncated because the value in *cbString* was less than or equal to the value in *\*pcbString*.|  
|ODBC_ERROR_REQUEST_FAILED|Request failed|The keyword did not exist in the file DSN.|  
  
## Comments  
 ODBC reserves the section name [ODBC] in which to store the connection information. The reserved keywords for this section are the same as those reserved for a connect string in **SQLDriverConnect**. (For more information, see the [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) function description.)  
  
 Applications can use these reserved keywords to read the information in a File DSN. If an applications wants to find out the DSN-less connection string associated with a File DSN, it can call **SQLReadFileDSN** for any of the reserved connection string keywords in the [ODBC] section. The full connection string passed in a DSN-less connection is a combination of all of the keywords (reserved and driver-specific) in the [ODBC] section.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Writing information to a File DSN|[SQLWriteFileDSN](../../../odbc/reference/syntax/sqlwritefiledsn-function.md)|
