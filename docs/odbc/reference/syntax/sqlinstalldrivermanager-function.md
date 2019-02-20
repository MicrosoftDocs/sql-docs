---
title: "SQLInstallDriverManager Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLInstallDriverManager"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLInstallDriverManager"
helpviewer_keywords: 
  - "SQLInstallDriverManager function [ODBC]"
ms.assetid: aebc439b-fffd-4d98-907a-0163f79aee8d
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLInstallDriverManager Function
**Conformance**  
 Version Introduced: ODBC 1.0: Deprecated in Windows XP Service Pack 2, Windows Server 2003 Service Pack 1, and later operating systems  
  
 **Summary**  
 **SQLInstallDriverManager** returns the path of the target directory for the installation of the ODBC core components. The calling program must actually copy the Driver Manager's files to the target directory.  
  
## Syntax  
  
```  
  
BOOL SQLInstallDriverManager(  
     LPSTR    lpszPath,  
     WORD     cbPathMax,  
     WORD *   pcbPathOut);  
```  
  
## Arguments  
 *lpszPath*  
 [Output] Path of the target directory of the installation.  
  
 *cbPathMax*  
 [Input] Length of *lpszPath*. This must be at least _MAX_PATH bytes.  
  
 *pcbPathOut*  
 [Output] Total number of bytes (excluding the null-termination byte) returned in *lpszPath*. If the number of bytes available to return is greater than or equal to *cbPathMax*, the path in *lpszPath* is truncated to *cbPathMax* minus the null-termination character. The *pcbPathOut* argument can be a null pointer.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLInstallDriverManager** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_BUFF_LEN|Invalid buffer length|The *lpszPath* argument was not large enough to contain the output path. The buffer contains the truncated path.<br /><br /> The *cbPathMax* argument was less than _MAX_PATH.|  
|ODBC_ERROR_USAGE_UPDATE_FAILED|Could not increment or decrement the component usage count|The installer failed to increment the ODBC core component usage count.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLInstallDriverManager** is called to return the path for ODBC core components and increment the component usage count in the system information. If a version of the Driver Manager already exists but the component usage count for the driver does not exist, the new component usage count value is set to 2.  
  
 The application setup program is responsible for physically copying the core component files and maintaining the file usage counts. If a core component file has not previously been installed, the application setup program must copy the file, and create the file usage count. If the file has previously been installed, the setup program merely increments the file usage count.  
  
 If an older version of the Driver Manager was previously installed by the application setup program, the core components should be uninstalled and then reinstalled, so that the core component usage count is valid. **SQLRemoveDriverManager** should first be called to decrement the component usage count. **SQLInstallDriverManager** should then be called to increment the component usage count. The application setup program must replace the old core component files with the new files. The file usage counts will remain the same, and other applications that used the older version core component files will now use the newer version files.  
  
 In a fresh install of the ODBC core components, drivers, and translators, the application setup program should call the following functions in sequence: **SQLInstallDriverManager**, **SQLInstallDriverEx**, **SQLConfigDriver** (with an *fRequest* of ODBC_INSTALL_DRIVER), and then **SQLInstallTranslatorEx**. In an uninstall of the core components, drivers, and translators, the application setup program should call the following functions in sequence: **SQLRemoveTranslator**, **SQLRemoveDriver**, and then **SQLRemoveDriverManager**. These functions must be called in this sequence. In an upgrade of all components, all the uninstall functions should be called in sequence and then all the install functions should be called in sequence.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a driver|[SQLConfigDriver](../../../odbc/reference/syntax/sqlconfigdriver-function.md)|  
|Installing a driver|[SQLInstallDriverEx](../../../odbc/reference/syntax/sqlinstalldriverex-function.md)|  
|Installing a translator|[SQLInstallTranslatorEx](../../../odbc/reference/syntax/sqlinstalltranslatorex-function.md)|  
|Removing a driver|[SQLRemoveDriver](../../../odbc/reference/syntax/sqlremovedriver-function.md)|  
|Removing the Driver Manager|[SQLRemoveDriverManager](../../../odbc/reference/syntax/sqlremovedrivermanager-function.md)|  
|Removing a translator|[SQLRemoveTranslator](../../../odbc/reference/syntax/sqlremovetranslator-function.md)|
