---
title: "SQLSetScrollOptions (Cursor Library)"
description: "SQLSetScrollOptions (Cursor Library)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSetScrollOptions function [ODBC], Cursor Library"
---
# SQLSetScrollOptions (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetScrollOptions** function in the cursor library. For general information about **SQLSetScrollOptions**, see [SQLSetScrollOptions Function](../../../odbc/reference/syntax/sqlsetscrolloptions-function.md).  
  
 The cursor library supports **SQLSetScrollOptions** only for backward compatibility; applications should use the SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_TYPE, and SQL_ATTR_ROW_ARRAY_SIZE statement attributes instead.
