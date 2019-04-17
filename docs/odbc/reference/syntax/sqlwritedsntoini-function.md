---
title: "SQLWriteDSNToIni Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLWriteDSNToIni"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLWriteDSNToIni"
helpviewer_keywords: 
  - "SQLWriteDSNToIni [ODBC]"
ms.assetid: dc7018b2-18d4-4657-96d0-086479a47474
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLWriteDSNToIni Function
**Conformance**  
 Version Introduced: ODBC 1.0  
  
 **Summary**  
 **SQLWriteDSNToIni** adds a data source to the system information.  
  
## Syntax  
  
```  
  
BOOL SQLWriteDSNToIni(  
     LPCSTR   lpszDSN,  
     LPCSTR   lpszDriver);  
```  
  
## Arguments  
 *lpszDSN*  
 [Input] Name of the data source to add.  
  
 *lpszDriver*  
 [Input] Driver description (usually the name of the associated DBMS) presented to users instead of the physical driver name.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLWriteDSNToIni** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_DSN|Invalid DSN|The *lpszDSN* argument contained a string that was invalid for a DSN.|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszDriver* argument was invalid.|  
|ODBC_ERROR_REQUEST_FAILED|Request failed|The installer failed to create a DSN in the registry.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLWriteDSNToIni** adds the data source to the [ODBC Data Sources] section of the system information. It then creates a specification section for the data source and adds a single keyword (**Driver**) with the name of the driver DLL as its value. If the data source specification section already exists, **SQLWriteDSNToIni** removes the old section before creating the new one.  
  
 The caller of this function must add any driver-specific keywords and values to the data source specification section of the system information.  
  
 If the name of the data source is Default, **SQLWriteDSNToIni** also creates the default driver specification section in the system information.  
  
 This function should be called only from a setup DLL.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a data source|[ConfigDSN](../../../odbc/reference/syntax/configdsn-function.md)(in the Setup DLL)|  
|Adding, modifying, or removing a data source|[SQLConfigDataSource](../../../odbc/reference/syntax/sqlconfigdatasource-function.md)|  
|Removing a data source name from the system information|[SQLRemoveDSNFromIni](../../../odbc/reference/syntax/sqlremovedsnfromini-function.md)|
