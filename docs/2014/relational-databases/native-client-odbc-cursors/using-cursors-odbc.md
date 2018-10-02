---
title: "Using Cursors (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, cursors"
  - "ODBC cursors, about ODBC cursors"
  - "ODBC applications, cursors"
  - "cursors [ODBC]"
  - "ODBC cursors"
ms.assetid: 51322f92-0d76-44c9-9c33-9223676cf1d3
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Cursors (ODBC)
  ODBC supports a cursor model that allows:  
  
-   Several types of cursors.  
  
-   Scrolling and positioning within a cursor.  
  
-   Several concurrency options.  
  
-   Positioned updates.  
  
 ODBC applications rarely declare and open cursors or use any cursor-related [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. ODBC automatically opens a cursor for every result set returned from an SQL statement. The characteristics of the cursors are controlled by statement attributes set with [SQLSetStmtAttr](../native-client-odbc-api/sqlsetstmtattr.md) before the SQL statement is executed. The ODBC API functions for processing result sets support the full range of cursor functionality, including fetching, scrolling, and positioned updates.  
  
 This is a comparison of how [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts and ODBC applications work with cursors.  
  
|Action|[!INCLUDE[tsql](../../includes/tsql-md.md)]|ODBC|  
|------------|------------------------|----------|  
|Define cursor behavior|Specify through DECLARE CURSOR parameters|Set cursor attributes by using [SQLSetStmtAttr](../native-client-odbc-api/sqlsetstmtattr.md)|  
|Open a cursor|DECLARE CURSOR OPEN *cursor_name*|**SQLExecDirect** or **SQLExecute**|  
|Fetch rows|FETCH|**SQLFetch** or [SQLFetchScroll](../native-client-odbc-api/sqlfetchscroll.md)|  
|Positioned update|WHERE CURRENT OF clause on UPDATE or DELETE|**SQLSetPos**|  
|Close a cursor|CLOSE *cursor_name* DEALLOCATE|[SQLCloseCursor](../native-client-odbc-api/sqlclosecursor.md)|  
  
 The server cursors implemented in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support the functionality of the ODBC cursor model. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client driver uses server cursors to support the cursor functionality of the ODBC API.  
  
## In This Section  
  
-   [How Cursors Are Implemented](implementation/how-cursors-are-implemented.md)  
  
-   [Cursor Types](cursor-types.md)  
  
-   [Cursor Behaviors](cursor-behaviors.md)  
  
-   [Cursor Properties](properties/cursor-properties.md)  
  
-   [Cursor Programming Details &#40;ODBC&#41;](programming/cursor-programming-details-odbc.md)  
  
-   [Scrolling and Fetching Rows](../native-client-ole-db-rowsets/fetching-rows.md)  
  
-   [Positioned Updates &#40;ODBC&#41;](positioned-updates-odbc.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../native-client/odbc/sql-server-native-client-odbc.md)   
 [CLOSE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/close-transact-sql)   
 [Cursors](../../relational-databases/cursors.md)   
 [DEALLOCATE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/deallocate-transact-sql)   
 [DECLARE CURSOR &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/declare-cursor-transact-sql)   
 [FETCH &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/fetch-transact-sql)   
 [OPEN &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/open-transact-sql)  
  
  
