---
title: "SQLRemoveDefaultDataSource Function"
description: "SQLRemoveDefaultDataSource Function"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
f1_keywords:
  - "SQLRemoveDefaultDataSource"
helpviewer_keywords:
  - "SQLRemoveDefaultDataSource function [ODBC]"
apilocation: "sqlsrv32.dll"
apiname: "SQLRemoveDefaultDataSource"
apitype: "dllExport"
---
# SQLRemoveDefaultDataSource Function
**Conformance**  
 Version Introduced: ODBC 1.0, Deprecated  
  
 **Summary**  
 In ODBC 3.0, the **SQLRemoveDefaultDataSource** function has been replaced by a call to [SQLConfigDataSource](../../../odbc/reference/syntax/sqlconfigdatasource-function.md) with an *fRequest* argument of ODBC_REMOVE_DEFAULT_DSN. If an ODBC 2*.x* installation program calls this function, the ODBC installer will map it to the following **SQLConfigDataSource** call:  
  
```cpp  
SQLConfigDataSource (NULL, ODBC_REMOVE_DEFAULT_DSN, NULL, NULL)  
```
