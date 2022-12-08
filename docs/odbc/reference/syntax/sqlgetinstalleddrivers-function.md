---
description: "SQLGetInstalledDrivers Function"
title: "SQLGetInstalledDrivers Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLGetInstalledDrivers"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetInstalledDrivers"
helpviewer_keywords: 
  - "SQLGetInstalledDrivers function [ODBC]"
ms.assetid: a1983a2e-0edf-422e-bd1b-ec5db40a34bc
author: David-Engel
ms.author: v-davidengel
---
# SQLGetInstalledDrivers Function
**Conformance**  
 Version Introduced: ODBC 1.0  
  
 **Summary**  
 **SQLGetInstalledDrivers** reads the [ODBC Drivers] section of the system information and returns a list of descriptions of the installed drivers.  
  
## Syntax  
  
```cpp  
  
BOOL SQLGetInstalledDrivers(  
     LPSTR   lpszBuf,  
     WORD    cbBufMax,  
     WORD *  pcbBufOut);  
```  
  
## Arguments  
 *lpszBuf*  
 [Output] List of descriptions of the installed drivers. For information about the list structure, see "Comments."  
  
 *cbBufMax*  
 [Input] Length of *lpszBuf*.  
  
 *pcbBufOut*  
 [Output] Total number of bytes (excluding the null-termination byte) returned in *lpszBuf*. If the number of bytes available to return is greater than or equal to *cbBufMax*, the list of driver descriptions in *lpszBuf* is truncated to *cbBufMax* minus the null-termination character. The *pcbBufOut* argument can be a null pointer.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLGetInstalledDrivers** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_BUFF_LEN|Invalid buffer length|The *lpszBuf* argument was NULL or invalid, or the *cbBufMax* argument was less than or equal to 0.|  
|ODBC_ERROR_COMPONENT_NOT_FOUND|Component not found in registry|The installer could not find the [ODBC Drivers] section in the registry.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 Each driver description is terminated with a null byte, and the entire list is terminated with a null byte. (That is, two null bytes mark the end of the list.) If the allocated buffer is not large enough to hold the entire list, the list is truncated without error. An error is returned if a null pointer is passed in as *lpszBuf*.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning driver descriptions and attributes|[SQLDrivers](../../../odbc/reference/syntax/sqldrivers-function.md)|
