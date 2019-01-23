---
title: "SQLRemoveDriver Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLRemoveDriver"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLRemoveDriver"
helpviewer_keywords: 
  - "SQLRemoveDriver function [ODBC]"
ms.assetid: 9a3b4f8b-982b-44b9-ade6-754ff026dc90
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLRemoveDriver Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLRemoveDriver** changes or removes information about the driver from the Odbcinst.ini entry in the system information.  
  
## Syntax  
  
```  
  
BOOL SQLRemoveDriver(  
     LPCSTR   lpszDriver,  
     BOOL     fRemoveDSN,  
     LPDWORD  lpdwUsageCount);  
```  
  
## Arguments  
 *lpszDriver*  
 [Input] The name of the driver as registered in the Odbcinst.ini key of the system information.  
  
 *fRemoveDSN*  
 [Input] The valid values are:  
  
 TRUE: Remove DSNs associated with the driver specified in *lpszDriver*. FALSE: Do not remove DSNs associated with the driver specified in *lpszDriver*.  
  
 *lpdwUsageCount*  
 [Output] The usage count of the driver after this function has been called.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails. If no entry exists in the system information when this function is called, the function returns FALSE.  
  
## Diagnostics  
 When **SQLRemoveDriver** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_COMPONENT_NOT_FOUND|Component not found in registry|The installer could not remove the driver information because it either did not exist in the registry or could not be found in the registry.|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszDriver* argument was invalid.|  
|ODBC_ERROR_USAGE_UPDATE_FAILED|Could not increment or decrement the component usage count|The installer failed to decrement the usage count of the driver.|  
|ODBC_ERROR_REQUEST_FAILED|Request failed|The *fRemoveDSN* argument was TRUE; however, one or more DSNs could not be removed. The call to **SQLConfigDriver** with the ODBC_REMOVE_DRIVER request failed.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLRemoveDriver** complements the [SQLInstallDriverEx](../../../odbc/reference/syntax/sqlinstalldriverex-function.md) function and updates the component usage count in the system information. This function should be called only from a setup application.  
  
 **SQLRemoveDriver** will decrement the component usage count value by 1. If the component usage count goes to 0, the following will occur:  
  
1.  The **SQLConfigDriver** function with the ODBC_REMOVE_DRIVER option will be called. If the *fRemoveDSN* option is set to TRUE, the **ConfigDSN** function calls **SQLRemoveDSNFromIni** to remove all the data sources associated with the driver specified in *lpszDriver.* If the *fRemoveDSN* option is set to FALSE, the data sources will not be deleted.  
  
2.  The driver entry in the system information will be removed. The driver entry is in the following system information location, under the driver name:  
  
     `HKEY_LOCAL_MACHINE`  
  
     `SOFTWARE`  
  
     `ODBC`  
  
     `Odbcinst.ini`  
  
 **SQLRemoveDriver** does not actually remove any files. The calling program is responsible for deleting files and maintaining the file usage count. Only after both the component usage count and the file usage count have reached zero is a file physically deleted. Some files in a component can be deleted, and others not deleted, depending on whether the files are used by other applications that have incremented the file usage count.  
  
 **SQLRemoveDriver** is also called as part of an upgrade process. If an application detects that it has to perform an upgrade and it has previously installed the driver, the driver should be removed and then reinstalled. **SQLRemoveDriver** should first be called to decrement the component usage count, and then **SQLInstallDriverEx** should be called to increment the component usage count. The application setup program must replace the old files with the new files. The file usage count will remain the same, and other applications that use the older version files will now use the newer version.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a driver|[ConfigDriver](../../../odbc/reference/syntax/configdriver-function.md) (in the Setup DLL)|  
|Adding, modifying, or removing a driver|[SQLConfigDriver](../../../odbc/reference/syntax/sqlconfigdriver-function.md)|  
|Installing a driver|[SQLInstallDriverEx](../../../odbc/reference/syntax/sqlinstalldriverex-function.md)|
