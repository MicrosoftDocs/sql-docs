---
description: "SQLInstallTranslatorEx Function"
title: "SQLInstallTranslatorEx Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLInstallTranslatorEx"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLInstallTranslatorEx"
helpviewer_keywords: 
  - "SQLInstallTranslatorEx function [ODBC]"
ms.assetid: a0630602-53c1-4db0-98ce-70d160aedf8d
author: David-Engel
ms.author: v-davidengel
---
# SQLInstallTranslatorEx Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLInstallTranslatorEx** adds information about a translator to the Odbcinst.ini section of the system information (HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\ODBC Translators registry key).  
  
 The functionality of **SQLInstallTranslatorEx** can also be accessed with [ODBCCONF.EXE](../../../odbc/odbcconf-exe.md).  
  
## Syntax  
  
```cpp  
  
BOOL SQLInstallTranslatorEx(  
     LPCSTR    lpszTranslator,  
     LPCSTR    lpszPathIn,  
     LPSTR     lpszPathOut,  
     WORD      cbPathOutMax,  
     WORD *    pcbPathOut,  
     WORD      fRequest,  
     LPDWORD   lpdwUsageCount);  
```  
  
## Arguments  
 *lpszTranslator*  
 [Input] This must contain a doubly null-terminated list of keyword-value pairs describing the translator. For more information about keyword-value pair syntax, see [Translator Specification Subkeys](../../../odbc/reference/install/translator-specification-subkeys.md).  
  
 The **Translator** and **Setup** keywords must be included in the *lpszTranslator* string. The translation DLL is listed with the **Translator** keyword, and the translator setup DLL is listed with the **Setup** keyword. Each pair is terminated with a NULL byte, and the entire list is terminated with a NULL byte. (That is, two NULL bytes mark the end of the list.) The format of *lpszTranslator* is as follows:  
  
 \0Translator=*translator-DLL-filename*\0[Setup=*setup-DLL-filename*\0]\0  
  
 *lpszPathIn*  
 [Input] Full path of where the translator is to be installed or a null pointer. If *lpszPath* is a null pointer, the translators will be installed in the System directory.  
  
 *lpszPathOut*  
 [Output] The path of the target directory where the translator should be installed. If the translator has never been installed, *lpszPathOut* is the same as *lpszPathIn*. If there exists a prior installation of the translator, *lpszPathOut* is the path of the prior installation.  
  
 *cbPathOutMax*  
 [Input] Length of *lpszPathOut.*  
  
 *pcbPathOut*  
 [Output] Total number of bytes available to return in *lpszPathOut*. If the number of bytes available to return is greater than or equal to *cbPathOutMax*, the output path in *lpszPathOut* is truncated to *pcbPathOutMax* minus the null-termination character. The *pcbPathOut* argument can be a null pointer.  
  
 *fRequest*  
 [Input] Type of request. *fRequest* must contain one of the following values:  
  
 ODBC_INSTALL_INQUIRY: Inquire about where a translator can be installed.  
  
 ODBC_INSTALL_COMPLETE: Complete the installation request.  
  
 *lpdwUsageCount*  
 [Output] The usage count of the translator after this function has been called.  
  
 Applications should not set the usage count. ODBC will maintain this count.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLInstallTranslatorEx** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_BUFF_LEN|Invalid buffer length|The *lpszPathOut* argument was not large enough to contain the output path. The buffer contains the truncated path.<br /><br /> The *cbPathOutMax* argument was 0, and the *fRequest* argument was ODBC_INSTALL_COMPLETE.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *fRequest* argument was not one of the following:<br /><br /> ODBC_INSTALL_INQUIRY ODBC_INSTALL_COMPLETE|  
|ODBC_ERROR_INVALID_KEYWORD_VALUE|Invalid keyword-value pairs|The *lpszTranslator* argument contained a syntax error.|  
|ODBC_ERROR_INVALID_PATH|Invalid install path|The *lpszPathIn* argument contained an invalid path.|  
|ODBC_ERROR_INVALID_PARAM_SEQUENCE|Invalid parameter sequence|The *lpszTranslator* argument did not contain a list of keyword-value pairs.|  
|ODBC_ERROR_USAGE_UPDATE_FAILED|Could not increment or decrement the registry's component usage count|The installer failed to increment the translator's usage count.|  
  
## Comments  
 **SQLInstallTranslatorEx** provides a mechanism to install just the translator. This function does not actually copy any files. The calling program is responsible for copying the translator files.  
  
 **SQLInstallTranslatorEx** increments the component usage count for the installed translator by 1. If a version of the translator already exists but the component usage count for the translator does not exist, the new component usage count value is set to 2.  
  
 The application setup program is responsible for physically copying the translator file and maintaining the file usage count. If the translator file has not previously been installed, the application setup program must copy the file or files and create the file or files usage count. If the file has previously been installed, the setup program simply increments the file usage count.  
  
 If an older version of the translator was previously installed by the application, the translator should be uninstalled and then reinstalled, so that the translator component usage count is valid. **SQLRemoveTranslator** should be called to decrement the component usage count, and then **SQLInstallTranslatorEx** should be called to increment the component usage count. The application setup program must replace the old file or files with the new file. The file usage count will remain the same, and other applications that used the older version file will now use the newer version.  
  
 The length of the path in *lpszPathOut* in **SQLInstallTranslatorEx** allows for a two-phase install process, so an application can determine what *cbPathOutMax* should be by calling **SQLInstallTranslatorEx** with an *fRequest* of ODBC_INSTALL_INQUIRY mode. This will return the total number of bytes available in the *pcbPathOut* buffer. **SQLInstallTranslatorEx** can then be called with an *fRequest* of ODBC_INSTALL_COMPLETE and the *cbPathOutMax* argument set to the value in the *pcbPathOut* buffer, plus the null-termination character.  
  
 If you choose not to use the two-phase model for **SQLInstallTranslatorEx**, you must set *cbPathOutMax*, which defines the size of the storage for the path of the target directory, to the value _MAX_PATH, as defined in Stdlib.h, to prevent truncation.  
  
 When *fRequest* is ODBC_INSTALL_COMPLETE, **SQLInstallTranslatorEx** does not allow *lpszPathOut* to be NULL (or *cbPathOutMax* to be 0). If *fRequest* is ODBC_INSTALL_COMPLETE, FALSE is returned when the number of bytes available to return is greater than or equal to *cbPathOutMax*, with the result that truncation occurs.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning a default translation option|[ConfigTranslator](../../../odbc/reference/syntax/configtranslator-function.md)|  
|Selecting translators|[SQLGetTranslator](../../../odbc/reference/syntax/sqlgettranslator-function.md)|  
|Removing translators|[SQLRemoveTranslator](../../../odbc/reference/syntax/sqlremovetranslator-function.md)|
