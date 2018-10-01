---
title: "Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "forward-only cursors [ODBC]"
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], about cursors"
  - "cursors [ODBC]"
  - "fetches [ODBC], cursors"
  - "result sets [ODBC], fetching"
  - "block cursors [ODBC]"
ms.assetid: 0b114352-3c63-4d33-9220-182ede90e4aa
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cursors
An application fetches data with a *cursor*. A cursor is different from a result set: A result set is the set of rows that matches particular search criteria, whereas a cursor is the software that returns those rows to the application. The name *cursor,* as it applies to databases, probably originated from the blinking cursor on a computer terminal. Just as that cursor indicates the current position on the screen and where the typed words will appear next, a cursor on a result set indicates the current position in the result set and what row will be returned next.  
  
 The cursor model in ODBC is based on the cursor model in embedded SQL. One notable difference between these models is the way cursors are opened. In embedded SQL, a cursor must be explicitly declared and opened before it can be used. In ODBC, a cursor is implicitly opened when a statement that creates a result set is executed. When the cursor is opened, it is positioned before the first row of the result set. In both embedded SQL and ODBC, a cursor must be closed after the application has finished using it.  
  
 Different cursors have different characteristics. The most common type of cursor, which is called a *forward-only cursor,* can only move forward through the result set. To return to a previous row, the application must close and reopen the cursor and then read rows from the beginning of the result set until it reaches the required row. Forward-only cursors provide a fast mechanism for making a single pass through a result set.  
  
 Forward-only cursors are less useful for screen-based applications, in which the user scrolls backward and forward through the data. Such applications can use a forward-only cursor by reading the result set once, caching the data locally, and performing scrolling themselves. However, this works well only with small amounts of data. A better solution is to use a *scrollable cursor,* which provides random access to the result set. Such applications can also increase performance by fetching more than one row of data at a time, using what is called a *block cursor.* For more information about block cursors, see [Using Block Cursors](../../../odbc/reference/develop-app/using-block-cursors.md).  
  
 The forward-only cursor is the default cursor type in ODBC and is discussed in the following sections. For more information about block cursors and scrollable cursors, see [Block Cursors](../../../odbc/reference/develop-app/block-cursors.md) and [Scrollable Cursors](../../../odbc/reference/develop-app/scrollable-cursors.md).  
  
> [!IMPORTANT]  
>  Committing or rolling back a transaction, either by explicitly calling **SQLEndTran** or by operating in auto-commit mode, causes some data sources to close all the cursors on all statements on a connection. For more information, see the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR attributes in the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.
