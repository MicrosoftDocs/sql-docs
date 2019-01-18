---
title: "SQLRemoveTranslator Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLRemoveTranslator"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLRemoveTranslator"
helpviewer_keywords: 
  - "SQLRemoveTranslator function [ODBC]"
ms.assetid: c6feda49-0359-4224-8de9-77125cf2397b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLRemoveTranslator Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLRemoveTranslator** removes information about a translator from the Odbcinst.ini section of the system information and decrements the translator's component usage count by 1.  
  
## Syntax  
  
```  
  
BOOL SQLRemoveTranslator(  
     LPCSTR    lpszTranslator,  
     LPDWORD   lpdwUsageCount);  
```  
  
## Arguments  
 *lpszTranslator*  
 [Input] The name of the translator as registered in the Odbcinst.ini key of the system information.  
  
 *lpdwUsageCount*  
 [Output] The usage count of the translator after this function has been called.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails. If no entry exists in the system information when this function is called, the function returns FALSE.  
  
## Diagnostics  
 When **SQLRemoveTranslator** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_COMPONENT_NOT_FOUND|Component not found in registry|The installer could not remove the translator information because it either did not exist in the registry or could not be found in the registry.|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszTranslator* argument was invalid.|  
|ODBC_ERROR_USAGE_UPDATE_FAILED|Could not increment or decrement the component usage count|The installer failed to decrement the usage count of the driver.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLRemoveTranslator** complements the [SQLInstallTranslatorEx](../../../odbc/reference/syntax/sqlinstalltranslatorex-function.md) function and updates the component usage count in the system information. This function should be called only from a setup application.  
  
 **SQLRemoveTranslator** will decrement the component usage count by 1. If the component usage count goes to 0, the translator entry in the system information will be removed. The translator entry is in the following location in the system information, under the translator name:  
  
 `HKEY_LOCAL_MACHINE`  
  
 `SOFTWARE`  
  
 `ODBC`  
  
 `Odbcinst.ini`  
  
 **SQLRemoveTranslator** does not actually remove any files. The calling program is responsible for deleting files, and maintaining the file usage count. Only after both the component usage count and the file usage count have reached zero is a file physically deleted. Some files in a component can be deleted, and others not deleted, depending on whether the files are used by other applications that have incremented the file usage count.  
  
 **SQLRemoveTranslator** is also called as part of an upgrade process. If an application detects that it has to perform an upgrade and it has previously installed the driver, the driver should be removed and then reinstalled. **SQLRemoveTranslator** should first be called to decrement the component usage count, and then **SQLInstallTranslatorEx** should be called to increment the component usage count. The application setup program must physically replace the old files with the new files. The file usage count will remain the same, and other applications that use the older version files will now use the newer version.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Installing a translator|[SQLInstallTranslatorEx](../../../odbc/reference/syntax/sqlinstalltranslatorex-function.md)|
