---
title: "SQLRemoveDriverManager Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLRemoveDriverManager"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLRemoveDriverManager"
helpviewer_keywords: 
  - "SQLRemoveDriverManager function function [ODBC]"
ms.assetid: 3a41511f-6603-4b81-a815-7883874023c4
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLRemoveDriverManager Function
**Conformance**  
 Version Introduced: ODBC 3.0: Deprecated in Windows XP Service Pack 2, Windows Server 2003 Service Pack 1, and later operating systems.  
  
 **Summary**  
 **SQLRemoveDriverManager** changes or removes information about the ODBC core components from the Odbcinst.ini entry in the system information.  
  
## Syntax  
  
```  
  
BOOL SQLRemoveDriverManager(  
     LPDWORD     pdwUsageCount);  
```  
  
## Arguments  
 *pdwUsageCount*  
 [Output] The usage count of the Driver Manager after this function has been called.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails. If no entry exists in the system information when this function is called, the function returns FALSE.  
  
## Diagnostics  
 When **SQLRemoveDriverManager** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_COMPONENT_NOT_FOUND|Component not found in registry|The installer could not remove the Driver Manager information because it either did not exist in the registry or could not be found in the registry.|  
|ODBC_ERROR_USAGE_UPDATE_FAILED|Could not increment or decrement the component usage count|The installer failed to decrement the usage count of the Driver Manager.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLRemoveDriverManager** complements the **SQLInstallDriverManager** function, and updates the component usage count in the system information. This function should be called only from a setup application.  
  
 **SQLRemoveDriverManager** will decrement the core component usage count by 1. If the component usage count goes to 0, the entry system information will be removed. The core component entry is in the following location in the system information, under the title "ODBC Core":  
  
 `HKEY_LOCAL_MACHINE`  
  
 `SOFTWARE`  
  
 `ODBC`  
  
 `Odbcinst.ini`  
  
> [!CAUTION]  
>  An application should not physically remove Driver Manager files when the component usage count and the file usage count reach zero.  
  
 **SQLRemoveDriverManager** does not actually remove any files. The calling program is responsible for deleting files and maintaining the file usage counts. Driver Manager files should not, however, be removed when both the component usage count and the file usage count have reached zero, because these files may be used by other applications that have not incremented the file usage count.  
  
 **SQLRemoveDriverManager** is called as part of the Uninstall process. ODBC core components (which include the Driver Manager, Cursor Library, Installer, Language Library, Administrator, thunking files, and so on) are uninstalled as a whole. The following files are not removed when **SQLRemoveDriverManager** is called as part of the Uninstall process:  
  
|||  
|-|-|  
|ODBC32DLL|ODBCCP32.DLL|  
|ODBCCR32.DLL|ODBC16GT.DLL|  
|ODBCCU32.DLL|ODBC32GT.DLL|  
|ODBCINT.DLL|DS16GT.DLL|  
|ODBCTRAC.DLL|DS32GT.DLL|  
|MSVCRT40.DLL|ODBCAD32.EXE|  
|ODBCCP32.CPL||  
  
 **SQLRemoveDriverManager** is also called as part of an upgrade process. If an application detects that it has to perform an upgrade and it has previously installed the driver, the driver should be removed and then reinstalled.  
  
 **SQLRemoveDriverManager** should first be called to decrement the component usage count. **SQLInstallDriverEx** should then be called to increment the component usage count. The application setup program must replace the old core component files with the new files. The file usage counts will remain the same, and other applications that use the older version core component files will now use the newer version files.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Installing a Driver Manager|[SQLInstallDriverManager](../../../odbc/reference/syntax/sqlinstalldrivermanager-function.md)|
