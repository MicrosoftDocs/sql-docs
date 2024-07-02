---
title: "SQLCloseCursor_ODBC"
description: "SQLCloseCursor_ODBC"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLCloseCursor function [ODBC], ODBC"
---
# SQLCloseCursor_ODBC
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLCloseCursor** function in the cursor library. For general information about **SQLCloseCursor**, see [SQLCloseCursor Function](../../../odbc/reference/syntax/sqlclosecursor-function.md).  
  
 The cursor library does not support calling **SQLCloseCursor** without an open cursor. Attempting this will return SQLSTATE 24000 (Invalid cursor state). Calling **SQLFreeStmt** with an *Option* of SQL_CLOSE when no cursor is open is supported by the cursor library.
