---
title: "Use Cursors (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "cursors [ODBC], how to topics"
ms.assetid: c502736f-bca0-45c3-ae25-d2ad52d296bf
author: MightyPen
ms.author: genemi
manager: craigg
---
# Use Cursors (ODBC)
    
### To use cursors  
  
1.  Call [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md) to set the desired cursor attributes:  
  
     Set the SQL_ATTR_CURSOR_TYPE and SQL_ATTR_CONCURRENCY attributes (this is the preferred option).  
  
     Or  
  
     Set the SQL_CURSOR_SCROLLABLE and SQL_CURSOR_SENSITIVITY attributes.  
  
2.  Call [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md) to set the rowset size by using the SQL_ATTR_ROW_ARRAY_SIZE attribute.  
  
3.  Optionally, call [SQLSetCursorName](https://go.microsoft.com/fwlink/?LinkId=58406) to set a cursor name if positioned updates will be done by using the WHERE CURRENT OF clause.  
  
4.  Execute the SQL statement.  
  
5.  Optionally, call [SQLGetCursorName](../../native-client-odbc-api/sqlgetcursorname.md) to get the cursor name if positioned updates will be done by using the WHERE CURRENT OF clause and a cursor name was not supplied with [SQLSetCursorName](https://go.microsoft.com/fwlink/?LinkId=58406) in Step 3.  
  
6.  Call [SQLNumResultCols](../../native-client-odbc-api/sqlnumresultcols.md) to get the number of columns (C) in the rowset.  
  
     Use column-wise binding.  
  
     \- or -  
  
     Use row-wise binding.  
  
7.  Fetch rowsets from the cursor as desired.  
  
8.  Call [SQLMoreResults](../../native-client-odbc-api/sqlmoreresults.md) to determine if another result set is available.  
  
    -   If it returns SQL_SUCCESS, another result set is available.  
  
    -   If it returns SQL_NO_DATA, no more result sets are available.  
  
    -   If it returns SQL_SUCCESS_WITH_INFO or SQL_ERROR, call [SQLGetDiagRec](https://go.microsoft.com/fwlink/?LinkId=58402) to determine if the output from a PRINT or RAISERROR statement is available.  
  
     If bound statement parameters are used for output parameters or the return value of a stored procedure, use the data now available in the bound parameter buffers.  
  
     When bound parameters are used, each call to [SQLExecute](https://go.microsoft.com/fwlink/?LinkId=58400) or [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) will have executed the SQL statement S times, where S is the number of elements in the array of bound parameters. This means that there will be S sets of results to process, where each set of results comprises all of the result sets, output parameters, and return codes usually returned by a single execution of the SQL statement.  
  
     Note that when a result set contains compute rows, each compute row is made available as a separate result set. These compute result sets are interspersed within the normal rows and break normal rows into multiple result sets.  
  
9. Optionally, call [SQLFreeStmt](../../native-client-odbc-api/sqlfreestmt.md) with SQL_UNBIND to release any bound column buffers.  
  
10. If another result set is available, go to Step 6.  
  
     In Step 9, calling [SQLMoreResults](../../native-client-odbc-api/sqlmoreresults.md) on a partially processed result set clears the remainder of the result set. Another way to clear a partially processed result set is to call [SQLCloseCursor](../../native-client-odbc-api/sqlclosecursor.md).  
  
     You can control the type of cursor used by setting either SQL_ATTR_CURSOR_TYPE and SQL_ATTR_CONCURRENCY, or by setting SQL_ATTR_CURSOR_SENSITIVITY and SQL_ATTR_CURSOR_SCROLLABLE. You should not mix the two methods of specifying cursor behavior.  
  
## See Also  
 [Using Cursors How-to Topics &#40;ODBC&#41;](using-cursors-how-to-topics-odbc.md)  
  
  
