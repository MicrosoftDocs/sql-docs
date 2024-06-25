---
title: "SQLFreeStmt (Cursor Library)"
description: "SQLFreeStmt (Cursor Library)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLFreeStmt function [ODBC], Cursor Library"
---
# SQLFreeStmt (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLFreeStmt** function in the cursor library. For general information about **SQLFreeStmt**, see [SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md).  
  
 If an application calls **SQLFreeStmt** with the SQL_UNBIND option after it calls **SQLExtendedFetch**, **SQLFetch**, or **SQLFetchScroll**, the cursor library returns an error. Before it can unbind result set columns, an application must call **SQLCloseCursor** or **SQLFreeStmt** with the SQL_CLOSE option.
