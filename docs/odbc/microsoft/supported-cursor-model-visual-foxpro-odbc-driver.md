---
description: "Supported Cursor Model (Visual FoxPro ODBC Driver)"
title: "Supported Cursor Model (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual FoxPro ODBC driver [ODBC], cursors"
  - "cursors [ODBC], Visual FoxPro ODBC driver"
  - "FoxPro ODBC driver [ODBC], cursors"
  - "static cursors [ODBC]"
  - "block cursors [ODBC]"
  - "rowset cursors [ODBC]"
ms.assetid: be95bbb2-6886-491e-a5a7-f58028d19c1e
author: David-Engel
ms.author: v-davidengel
---
# Supported Cursor Model (Visual FoxPro ODBC Driver)
The Visual FoxPro ODBC Driver supports both *block* (*rowset*) and *static* cursors. Static cursors are supported for any driver that conforms to Level 1 ODBC compliance. The driver does not support dynamic, keyset-driven, or mixed (keyset and dynamic) cursors.  
  
 Your application can call [SQLSetStmtOption](../../odbc/microsoft/sqlsetstmtoption-visual-foxpro-odbc-driver.md) with a SQL_CURSOR_TYPE option of SQL_CURSOR_FORWARD_ONLY (block cursor) or SQL_CURSOR_STATIC (static cursor).  
  
> [!NOTE]  
>  If you call **SQLSetStmtOption** with a SQL_CURSOR_TYPE option other than SQL_CURSOR_FORWARD_ONLY or SQL_CURSOR_STATIC, the function returns SQL_SUCCESS_WITH_INFO with a SQLSTATE of 01S02 (Option value changed). The driver sets all unsupported cursor modes to SQL_CURSOR_STATIC.  
  
 For more information about cursor types and about **SQLSetStmtOption**, see the [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md).  
  
## block cursor  
 A forward-scrolling, read-only result set returned to the client, who is responsible for maintaining storage for the data.  
  
## static cursor  
 A snapshot of a data set defined by the query. Static cursors do not reflect real-time changes of the underlying data by other users. The cursor's memory buffer is maintained by the ODBC cursor library, which allows forward and backward scrolling.  
  
## rowset  
 Blocks of data stored in a cursor, representing rows retrieved from a data source.
