---
description: "SQLWriteFileDSN Function"
title: "SQLWriteFileDSN Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLWriteFileDSN"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLWriteFileDSN"
helpviewer_keywords: 
  - "SQLWriteFileDSN [ODBC]"
ms.assetid: 9e18f56f-1061-416b-83d4-ffeec42ab5a9
author: David-Engel
ms.author: v-davidengel
---
# SQLWriteFileDSN Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLWriteFileDSN** writes information to a File DSN.  
  
## Syntax  
  
```cpp  
  
BOOL SQLWriteFileDSN(  
     LPCSTR     lpszFileName,  
     LPCSTR     lpszAppName,  
     LPCSTR     lpszKeyName,  
     LPCSTR     lpszString);  
```  
  
## Arguments  
 *lpszFileName*  
 [Input] Pointer to the name of the File DSN. A DSN extension is appended to all file names that do not already have a DSN extension.  
  
 *lpszAppName*  
 [Input] Pointer to the name of the application. This is "ODBC" for the ODBC section.  
  
 *lpszKeyName*  
 [Input] Pointer to the name of the key to be read. See "Comments" for reserved keywords.  
  
 *lpszString*  
 [Output] Pointed to the string associated with the key to be written. The maximum length of the string pointed to by this argument is 32,767 bytes.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLWriteFileDSN** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_PATH|Invalid install path|The path of the file name specified in the *lpszFileName* argument was invalid.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *lpszAppName*, *lpszKeyName*, or *lpszString* argument was NULL.|  
  
## Comments  
 ODBC reserves the section name [ODBC] in which to store the connection information. The reserved keywords for this section are the same as those reserved for a connect string in **SQLDriverConnect**. (For more information, see the [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) function description.)  
  
 Applications can use these reserved keywords to write information directly to a File DSN. If an application wants to create or modify the DSN-less connection string associated with a File DSN, it can call **SQLWriteFileDSN** for any of the reserved connection string keywords in the [ODBC] section.  
  
 If the *lpszString* argument is a null pointer, the keyword pointed to by the *lpszKeyName* argument will be deleted from the .dsn file. If the *lpszString* and *lpszKeyName* arguments are both null pointers, the section pointed to by the *lpszAppName* argument will be deleted from the .dsn file.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Reading information from File DSNs|[SQLReadFileDSN](../../../odbc/reference/syntax/sqlreadfiledsn-function.md)|
