---
title: "Closing the Cursor"
description: "Closing the Cursor"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "cursors [ODBC], closing"
  - "closing cursors [ODBC]"
---
# Closing the Cursor
When an application has finished using a cursor, it calls **SQLCloseCursor** to close the cursor. For example:  
  
```  
SQLCloseCursor(hstmt);  
```  
  
 Until the application closes the cursor, the statement on which the cursor is opened cannot be used for most other operations, such as executing another SQL statement. For a complete list of functions that can be called while a cursor is open, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).  
  
> [!NOTE]  
>  To close a cursor, an application should call **SQLCloseCursor**, not **SQLCancel**.  
  
 Cursors remain open until they are explicitly closed, except when a transaction is committed or rolled back, in which case some data sources close the cursor. In particular, reaching the end of the result set, when **SQLFetch** returns SQL_NO_DATA, does not close a cursor. Even cursors on empty result sets (result sets created when a statement executed successfully but which returned no rows) must be explicitly closed.
