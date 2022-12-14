---
description: "SQLConfigDataSource Function"
title: "SQLConfigDataSource Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLConfigDataSource"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLConfigDataSource"
helpviewer_keywords: 
  - "SQLConfigDataSource function [ODBC]"
ms.assetid: f8d6e342-c010-434e-b1cd-f5371fb50a14
author: David-Engel
ms.author: v-davidengel
---
# SQLConfigDataSource Function
**Conformance**  
 Version Introduced: ODBC 1.0  
  
 **Summary**  
 **SQLConfigDataSource** adds, modifies, or deletes data sources.  
  
 The functionality of **SQLConfigDataSource** can also be accessed with [ODBCCONF.EXE](../../../odbc/odbcconf-exe.md).  
  
## Syntax  
  
```cpp  
  
BOOL SQLConfigDataSource(  
     HWND     hwndParent,  
     WORD     fRequest,  
     LPCSTR   lpszDriver,  
     LPCSTR   lpszAttributes);  
```  
  
## Arguments  
 *hwndParent*  
 [Input] Parent window handle. The function will not display any dialog boxes if the handle is null.  
  
 *fRequest*  
 [Input] Type of request. The *fRequest* argument must contain one of the following values:  
  
 ODBC_ADD_DSN: Add a new user data source.  
  
 ODBC_CONFIG_DSN: Configure (modify) an existing user data source.  
  
 ODBC_REMOVE_DSN: Remove an existing user data source.  
  
 ODBC_ADD_SYS_DSN: Add a new system data source.  
  
 ODBC_CONFIG_SYS_DSN: Modify an existing system data source.  
  
 ODBC_REMOVE_SYS_DSN: Remove an existing system data source.  
  
 ODBC_REMOVE_DEFAULT_DSN: Remove the default data source specification section from the system information. (It also removes the default driver specification section from the Odbcinst.ini entry in the system information. This *fRequest* performs the same function as the deprecated **SQLRemoveDefaultDataSource** function.) When this option is specified, all of the other parameters in the call to **SQLConfigDataSource** should be NULLs; if they are not NULL, they will be ignored.  
  
 *lpszDriver*  
 [Input] Driver description (usually the name of the associated DBMS) presented to users instead of the physical driver name.  
  
 *lpszAttributes*  
 [Input] A doubly null-terminated list of attributes in the form of keyword-value pairs. For more information, see [ConfigDSN](../../../odbc/reference/syntax/configdsn-function.md).  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails. If no entry exists in the system information when this function is called, the function returns FALSE.  
  
## Diagnostics  
 When **SQLConfigDataSource** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_HWND|Invalid window handle|The *hwndParent* argument was invalid or NULL.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *fRequest* argument was not one of the following:<br /><br /> ODBC_ADD_DSN ODBC_CONFIG_DSN ODBC_REMOVE_DSN ODBC_ADD_SYS_DSN ODBC_CONFIG_SYS_DSN ODBC_REMOVE_SYS_DSN ODBC_REMOVE_DEFAULT_DSN|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszDriver* argument was invalid. It could not be found in the registry.|  
|ODBC_ERROR_INVALID_KEYWORD_VALUE|Invalid keyword-value pairs|The *lpszAttributes* argument contained a syntax error.|  
|ODBC_ERROR_REQUEST_FAILED|*Request* failed|The installer could not perform the operation requested by the *fRequest* argument. The call to **ConfigDSN** failed.|  
|ODBC_ERROR_LOAD_LIBRARY_FAILED|Could not load the driver or translator setup library|The driver setup library could not be loaded.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLConfigDataSource** uses the value of *lpszDriver* to read the full path of the setup DLL for the driver from the system information. It loads the DLL and calls **ConfigDSN** with the same arguments that were passed to it.  
  
 **SQLConfigDataSource** returns FALSE if it is unable to find or load the setup DLL or if the user cancels the dialog box. Otherwise, it returns the status it received from **ConfigDSN**.  
  
 **SQLConfigDataSource** maps the System DSN *fRequest*s to the User DSN *fRequest*s (ODBC_ADD_SYS_DSN to ODBC_ADD_DSN, ODBC_CONFIG_SYS_DSN to ODBC_CONFIG_DSN, and ODBC_REMOVE_SYS_DSN to ODBC_REMOVE_DSN). To distinguish user and System DSNs, **SQLConfigDataSource** sets the installer configuration mode according to the following table. Prior to returning, **SQLConfigDataSource** resets configuration mode to BOTHDSN. **ConfigDSN** (implemented by drivers) should call **SQLWriteDSNToIni** and **SQLWritePrivateProfileString** to support a system DSN. For more information, see [ConfigDSN Function](../../../odbc/reference/syntax/configdsn-function.md).  
  
|*fRequest*|Configuration mode|  
|----------------|------------------------|  
|ODBC_ADD_DSN|USERDSN_ONLY|  
|ODBC_CONFIG_DSN|USERDSN_ONLY|  
|ODBC_REMOVE_DSN|USERDSN_ONLY|  
|ODBC_ADD_SYS_DSN|SYSTEMDSN_ONLY|  
|ODBC_CONFIG_SYS_DSN|SYSTEMDSN_ONLY|  
|ODBC_REMOVE_SYS_DSN|SYSTEMDSN_ONLY|  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a data source|[ConfigDSN](../../../odbc/reference/syntax/configdsn-function.md) (in the setup DLL)|  
|Removing a data source name from the system information|[SQLRemoveDSNFromIni](../../../odbc/reference/syntax/sqlremovedsnfromini-function.md)|  
|Adding a data source name to the system information|[SQLWriteDSNToIni](../../../odbc/reference/syntax/sqlwritedsntoini-function.md)|
