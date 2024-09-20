---
title: "SQLPostInstallerError Function"
description: "SQLPostInstallerError Function"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
f1_keywords:
  - "SQLPostInstallerError"
helpviewer_keywords:
  - "SQLPostInstallerError function [ODBC]"
apilocation: "sqlsrv32.dll"
apiname: "SQLPostInstallerError"
apitype: "dllExport"
---
# SQLPostInstallerError Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLPostInstallerError** provides a mechanism for a driver or translator setup library to report errors for the **ConfigDriver**, **ConfigDSN**, and **ConfigTranslator** functions to the installer error queue. Applications do not use this API; they use **SQLInstallerError** to retrieve the error.  
  
## Syntax  
  
```cpp  
  
RETCODE SQLPostInstallerError(  
     DWORD    fErrorCode,  
     LPSTR    szErrorMsg);  
```  
  
## Arguments  
 *fErrorCode*  
 [Input] Installer error code.  
  
 *szErrorMsg*  
 [Input] Error message text.  
  
## Returns  
 SQL_SUCCESS or SQL_ERROR.  
  
## Diagnostics  
 **SQLPostInstallerError** does not post error values for itself. If the error was successfully posted to the installer error queue (retrievable using **SQLInstallerError**), SQL_SUCCESS is returned. SQL_ERROR will be returned if the value in the *dwErrorCode* argument is not one of the specified installer error codes.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a driver|[ConfigDriver](../../../odbc/reference/syntax/configdriver-function.md)|  
|Adding, modifying, or removing data sources|[ConfigDSN](../../../odbc/reference/syntax/configdsn-function.md)|  
|Setting a translation option|[ConfigTranslator](../../../odbc/reference/syntax/configtranslator-function.md)|
