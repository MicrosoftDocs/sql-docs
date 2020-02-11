---
title: "Scrolling and Fetching Rows | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "scrollable cursors [SQL Server]"
  - "SQL Server Native Client ODBC driver, cursors"
  - "SQLFetchScroll function"
  - "ODBC applications, cursors"
  - "cursors [ODBC], fetching rows"
  - "ODBC cursors, fetching rows"
  - "cursors [ODBC], scrolling rows"
  - "fetching [ODBC]"
  - "ODBC cursors, scrolling rows"
ms.assetid: 9109f10d-326b-4a6d-8c97-831f60da8c4c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Scrolling and Fetching Rows
  To use a scrollable cursor, an ODBC application must:  
  
-   Set the cursor capabilities using [SQLSetStmtAttr](../native-client-odbc-api/sqlsetstmtattr.md).  
  
-   Open the cursor using **SQLExecute** or **SQLExecDirect**.  
  
-   Scroll and fetch rows using **SQLFetch** or [SQLFetchScroll](../native-client-odbc-api/sqlfetchscroll.md).  
  
 Both **SQLFetch** and **SQLFetchSroll** can fetch blocks of rows at a time. The number of rows returned is specified by using **SQLSetStmtAttr** to set the SQL_ATTR_ROW_ARRAY_SIZE parameter.  
  
 ODBC applications can use **SQLFetch** to fetch through a forward-only cursor.  
  
 **SQLFetchScroll** is used to scroll around a cursor. **SQLFetchScroll** supports fetching the next, prior, first, and last rowsets in addition to relative fetching (fetch the rowset *n* rows from the start of the current rowset) and absolute fetching (fetch the rowset starting at row *n*). If *n* is negative in an absolute fetch, rows are counted from the end of the result set. An absolute fetch of row -1 means to fetch the rowset that starts with the last row in the result set.  
  
 Applications that use **SQLFetchScroll** only for its block cursor capabilities, such as reports, are likely to pass through the result set a single time, using only the option to fetch the next rowset. Screen-based applications, on the other hand, can take advantage of all the capabilities of **SQLFetchScroll**. If the application sets the rowset size to the number of rows displayed on the screen and binds the screen buffers to the result set, it can translate scroll bar operations directly to calls to **SQLFetchScroll**.  
  
|Scroll bar operation|SQLFetchScroll scrolling option|  
|--------------------------|-------------------------------------|  
|Page up|SQL_FETCH_PRIOR|  
|Page down|SQL_FETCH_NEXT|  
|Line up|SQL_FETCH_RELATIVE with FetchOffset equal to -1|  
|Line down|SQL_FETCH_RELATIVE with FetchOffset equal to 1|  
|Scroll box to top|SQL_FETCH_FIRST|  
|Scroll box to bottom|SQL_FETCH_LAST|  
|Random scroll box position|SQL_FETCH_ABSOLUTE|  
  
## In This Section  
  
-   [Bookmarking Rows in ODBC](scrolling-and-fetching-rows-bookmarking-rows-in-odbc.md)  
  
## See Also  
 [Using Cursors &#40;ODBC&#41;](using-cursors-odbc.md)  
  
  
