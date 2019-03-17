---
title: "ConfigTranslator Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "ConfigTranslator"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "ConfigTranslator"
helpviewer_keywords: 
  - "ConfigTranslator [ODBC]"
ms.assetid: 7c22f07e-36de-425b-aa67-e32a84afae92
author: MightyPen
ms.author: genemi
manager: craigg
---
# ConfigTranslator Function
**Conformance**  
 Version Introduced: ODBC 2.0  
  
 **Summary**  
 **ConfigTranslator** returns a default translation option for a translator. It can be in the translator DLL or a separate setup DLL.  
  
## Syntax  
  
```  
  
BOOL ConfigTranslator(  
     HWND     hwndParent,  
     DWORD *  pvOption);  
```  
  
## Arguments  
 *hwndParent*  
 [Input] Parent window handle. The function will not display any dialog boxes if the handle is null.  
  
 *pvOption*  
 [Output] A 32-bit translation option.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **ConfigTranslator** returns FALSE, an associated *\*pfErrorCode* value is posted to the installer error buffer by a call to **SQLPostInstallerError** and can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_INVALID_HWND|Invalid window handle|The *hwndParent* argument was invalid or NULL.|  
|ODBC_ERROR_DRIVER_SPECIFIC|Driver- or translator-specific error|A driver-specific error for which there is no defined ODBC installer error. The *SzError* argument in a call to the **SQLPostInstallerError** function should contain the driver-specific error message.|  
|ODBC_ERROR_INVALID_OPTION|Invalid translation option|The *pvOption* argument contained an invalid value.|  
  
## Comments  
 If the translator supports only a single translation option, **ConfigTranslator** returns TRUE and sets *pvOption* to the 32-bit option. Otherwise, it determines the default translation option to use. **ConfigTranslator** can display a dialog box with which a user selects a default translation option.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Getting a translation option|[SQLGetConnectAttr](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
|Selecting a translator|[SQLGetTranslator](../../../odbc/reference/syntax/sqlgettranslator-function.md)|  
|Setting a translation option|[SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|
