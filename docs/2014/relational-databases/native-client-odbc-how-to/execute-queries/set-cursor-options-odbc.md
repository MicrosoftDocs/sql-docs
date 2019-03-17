---
title: "Set Cursor Options (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "cursors [ODBC], options"
ms.assetid: 0e72b48a-fc5a-4656-8cf5-39f57d8c1565
author: MightyPen
ms.author: genemi
manager: craigg
---
# Set Cursor Options (ODBC)
  To set cursor options, Call [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md) to set or [SQLGetStmtAttr](../../native-client-odbc-api/sqlgetstmtattr.md) to get the statement options that control cursor behavior.  
  
|*Attribute*|Specifies|  
|-----------------|---------------|  
|SQL_ATTR_CURSOR_TYPE|Cursor type of forward-only, static, dynamic, or keyset-driven|  
|SQL_ATTR_CONCURRENCY|Concurrency control option of read-only, locking, optimistic using timestamps, or optimistic using values|  
|SQL_ATTR_ROW_ARRAY_SIZE|Number of rows retrieved in each fetch|  
|SQL_ATTR_CURSOR_SENSITIVITY|Cursor that does or does not show updates to cursor rows made by other connections|  
|SQL_ATTR_CURSOR_SCROLLABLE|Cursor that can be scrolled forward and backward|  
  
 The default values for these attributes (forward-only, read-only, rowset size of 1) do not use server cursors. To use server cursors, at least one of these attributes must be set to a value other than the default, and the statement being executed must be a single SELECT statement or a stored procedure that contains a single SELECT statement. When using server cursors, SELECT statements cannot use clauses not supported by server cursors: COMPUTE, COMPUTE BY, FOR BROWSE, and INTO.  
  
 You can control the type of cursor used either by setting SQL_ATTR_CURSOR_TYPE and SQL_ATTR_CONCURRENCY, or by setting SQL_ATTR_CURSOR_SENSITIVITY and SQL_ATTR_CURSOR_SCROLLABLE. You should not mix the two methods of specifying cursor behavior.  
  
## Example  
 The following sample allocates a statement handle, sets a dynamic cursor type with row versioning optimistic concurrency, and then executes a SELECT.  
  
```  
retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc1, &hstmt1);  
retcode = SQLSetStmtAttr(hstmt1, SQL_ATTR_CURSOR_TYPE, (SQLPOINTER)SQL_CURSOR_DYNAMIC, SQL_IS_INTEGER);  
retcode = SQLSetStmtAttr(hstmt1, SQL_ATTR_CONCURRENCY, SQLPOINTER)SQL_CONCUR_ROWVER, SQL_IS_INTEGER);  
retcode = SQLExecDirect(hstmt1, SELECT au_lname FROM authors", SQL_NTS);  
```  
  
## Example  
 The following sample allocates a statement handle, sets a scrollable, sensitive cursor, and then executes a SELECT  
  
```  
retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc1, &hstmt1);  
  
// Set the cursor options and execute the statement.  
retcode = SQLSetStmtAttr(hstmt1, SQL_ATTR_CURSOR_SCROLLABLE, SQLPOINTER)SQL_SCROLLABLE, SQL_IS_INTEGER);  
retcode = SQLSetStmtAttr(hstmt1, SQL_ATTR_CURSOR_SENSITIVITY, SQLPOINTER)SQL_INSENSITIVE, SQL_IS_INTEGER);  
retcode = SQLExecDirect(hstmt1, select au_lname from authors", SQL_NTS);  
```  
  
## See Also  
 [Executing Queries How-to Topics &#40;ODBC&#41;](executing-queries-how-to-topics-odbc.md)  
  
  
