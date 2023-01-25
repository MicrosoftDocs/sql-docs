---
description: "SQLRemoveDefaultDataSource Function"
title: "SQLRemoveDefaultDataSource Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLRemoveDefaultDataSource"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLRemoveDefaultDataSource"
helpviewer_keywords: 
  - "SQLRemoveDefaultDataSource function [ODBC]"
ms.assetid: db803266-57df-4864-a41b-901247549c1f
author: David-Engel
ms.author: v-davidengel
---
# SQLRemoveDefaultDataSource Function
**Conformance**  
 Version Introduced: ODBC 1.0, Deprecated  
  
 **Summary**  
 In ODBC 3.0, the **SQLRemoveDefaultDataSource** function has been replaced by a call to [SQLConfigDataSource](../../../odbc/reference/syntax/sqlconfigdatasource-function.md) with an *fRequest* argument of ODBC_REMOVE_DEFAULT_DSN. If an ODBC 2*.x* installation program calls this function, the ODBC installer will map it to the following **SQLConfigDataSource** call:  
  
```cpp  
SQLConfigDataSource (NULL, ODBC_REMOVE_DEFAULT_DSN, NULL, NULL)  
```
