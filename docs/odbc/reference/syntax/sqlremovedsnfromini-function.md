---
title: "SQLRemoveDSNFromIni Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLRemoveDSNFromIni"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLRemoveDSNFromIni"
helpviewer_keywords: 
  - "SQLRemoveDSNFromIni function [ODBC]"
ms.assetid: bb2e8273-7b61-4113-bfc8-f7ccc607c811
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLRemoveDSNFromIni Function
**Conformance**  
 Version Introduced: ODBC 1.0  
  
 **Summary**  
 **SQLRemoveDSNFromIni** removes a data source from the system information.  
  
## Syntax  
  
```  
  
BOOL SQLRemoveDSNFromIni(  
     LPCSTR   lpszDSN);  
```  
  
## Arguments  
 *lpszDSN*  
 [Input] Name of the data source to remove.  
  
## Returns  
 The function returns TRUE if it removes the data source or the data source was not in the Odbc.ini file. It returns FALSE if it fails to remove the data source.  
  
## Diagnostics  
 When **SQLRemoveDSNFromIni** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_DSN|Invalid DSN|The *lpszDSN* argument was invalid.|  
|ODBC_ERROR_REQUEST_FAILED|Request failed|The installer could not remove the DSN info from the registry.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLRemoveDSNFromIni** removes the data source name from the [ODBC Data Sources] section of the system information. It also removes the data source specification section from the system information.  
  
 This function should be called only from a driver setup library.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a data source|[ConfigDSN](../../../odbc/reference/syntax/configdsn-function.md)|  
|Adding, modifying, or removing a data source|[SQLConfigDataSource](../../../odbc/reference/syntax/sqlconfigdatasource-function.md)|  
|Removing the default data source|[SQLRemoveDefaultDataSource](../../../odbc/reference/syntax/sqlremovedefaultdatasource-function.md)|  
|Adding a data source name to the system information|[SQLWriteDSNToIni](../../../odbc/reference/syntax/sqlwritedsntoini-function.md)|
