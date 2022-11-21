---
description: "SQLSetConfigMode Function"
title: "SQLSetConfigMode Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLSetConfigMode"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetConfigMode"
helpviewer_keywords: 
  - "SQLSetConfigMode function [ODBC]"
ms.assetid: 09eb88ea-b6f6-4eca-b19d-0951cebc6c0a
author: David-Engel
ms.author: v-davidengel
---
# SQLSetConfigMode Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLSetConfigMode** sets the configuration mode that indicates where the Odbc.ini entry listing DSN values is in the system information.  
  
## Syntax  
  
```cpp  
  
BOOL SQLSetConfigMode(  
     UWORD     wConfigMode);  
```  
  
## Arguments  
 *wConfigMode*  
 [Input] The installer configuration mode (see "Comments"). The value in *wConfigMode* can be:  
  
 ODBC_USER_DSN  
  
 ODBC_SYSTEM_DSN  
  
 ODBC_BOTH_DSN  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLSetConfigMode** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_INVALID_PARAM_SEQUENCE|Invalid parameter sequence|The *wConfigMode* argument did not contain ODBC_USER_DSN, ODBC_SYSTEM_DSN, or ODBC_BOTH_DSN.|  
  
## Comments  
 This function is used to set where the Odbc.ini entry listing DSN values is in the system information. If *wConfigMode* is ODBC_USER_DSN, the DSN is a User DSN and the function reads from the Odbc.ini entry in HKEY_CURRENT_USER. If it is ODBC_SYSTEM_DSN, the DSN is a System DSN and the function reads from the Odbc.ini entry in HKEY_LOCAL_MACHINE. If it is ODBC_BOTH_DSN, HKEY_CURRENT_USER is tried, and if it fails, then HKEY_LOCAL_MACHINE is used.  
  
 This function does not affect **SQLCreateDataSource** and **SQLDriverConnect**. The configuration mode has to be set when a driver reads from the registry by calling **SQLGetPrivateProfileString** or writes to the registry by calling **SQLWritePrivateProfileString**. Calls to **SQLGetPrivateProfileString** and **SQLWritePrivateProfileString** use the configuration mode to know which part of the registry to operate on.  
  
> [!CAUTION]  
>  **SQLSetConfigMode** should be called only when necessary; if the mode is improperly set, the ODBC Installer may fail to function properly.  
  
 **SQLSetConfigMode** makes a direct registry modification of the configuration mode. This is apart from the process of modifying the configuration mode by a call to **SQLConfigDataSource**. A call to **SQLConfigDataSource** sets the configuration mode to distinguish user and System DSNs when modifying a DSN. Prior to returning, **SQLConfigDataSource** resets the configuration mode to BOTHDSN.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Creating a data source|[SQLCreateDataSource](../../../odbc/reference/syntax/sqlcreatedatasource-function.md)|  
|Connecting to a data source using a connection string or dialog box|[SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)|  
|Retrieving the configuration mode|[SQLGetConfigMode](../../../odbc/reference/syntax/sqlgetconfigmode-function.md)|
