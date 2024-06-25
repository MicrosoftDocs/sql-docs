---
title: "SQLNativeSql (Cursor Library)"
description: "SQLNativeSql (Cursor Library)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLNativeSql function [ODBC], Cursor Library"
---
# SQLNativeSql (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLNativeSql** function in the cursor library. For general information about **SQLNativeSql**, see [SQLNativeSql Function](../../../odbc/reference/syntax/sqlnativesql-function.md).  
  
 If the driver supports this function, the cursor library calls **SQLNativeSql** in the driver and passes it the SQL statement. For positioned update, positioned delete, and **SELECT FOR UPDATE** statements, the cursor library modifies the statement before passing it to the driver.  
  
> [!NOTE]  
>  The cursor library incorrectly returns SQLSTATE 34000 (Invalid cursor name) if the cursor name is invalid in a positioned update or delete statement that is passed in the *InStatementText* argument of **SQLNativeSql**. **SQLNativeSql** is not intended to return syntax errors, which are returned only upon statement preparation or execution.
