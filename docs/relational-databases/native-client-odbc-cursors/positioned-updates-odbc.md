---
description: "Positioned Updates (ODBC)"
title: "Positioned Updates (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, cursors"
  - "SQLSetPos function"
  - "SQLSetCursorName function"
  - "ODBC applications, cursors"
  - "cursors [ODBC], positioned updates"
  - "positioned updates [ODBC]"
  - "ODBC cursors, positioned updates"
ms.assetid: ff404e02-630f-474d-b5d4-06442b756991
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Positioned Updates (ODBC)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  ODBC supports two methods for performing positioned updates in a cursor:  
  
-   **SQLSetPos**  
  
-   WHERE CURRENT OF clause  
  
 The more common approach is to use **SQLSetPos**. It has the following options.  
  
 SQL_POSITION  
 Positions the cursor on a specific row in the current rowset.  
  
 SQL_REFRESH  
 Refreshes program variables bound to the result set columns with the values from the row the cursor is currently positioned on.  
  
 SQL_UPDATE  
 Updates the current row in the cursor with the values stored in the program variables bound to the result set columns.  
  
 SQL_DELETE  
 Deletes the current row in the cursor.  
  
 **SQLSetPos** can be used with any statement result set when the statement handle cursor attributes are set to use server cursors. The result set columns must be bound to program variables. As soon as the application has fetched a row it calls **SQLSetPos**(SQL_POSTION) to position the cursor on the row. The application could then call SQLSetPos(SQL_DELETE) to delete the current row, or it can move new data values into the bound program variables and call SQLSetPos(SQL_UPDATE) to update the current row.  
  
 Applications can update or delete any row in the rowset with **SQLSetPos**. Calling **SQLSetPos** is a convenient alternative to constructing and executing an SQL statement. **SQLSetPos** operates on the current rowset and can be used only after a call to [SQLFetchScroll](../../relational-databases/native-client-odbc-api/sqlfetchscroll.md).  
  
 Rowset size is set by a call to [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) with an attribute argument of SQL_ATTR_ROW_ARRAY_SIZE. **SQLSetPos** uses a new rowset size, but only after a call to **SQLFetch** or **SQLFetchScroll**. For example, if the rowset size is changed, **SQLSetPos** is called and then **SQLFetch** or **SQLFetchScroll** is called. The call to **SQLSetPos** uses the old rowset size, but **SQLFetch** or **SQLFetchScroll** uses the new rowset size.  
  
 The first row in the rowset is row number 1. The RowNumber argument in **SQLSetPos** must identify a row in the rowset; that is, its value must be in the range between 1 and the number of rows that were most recently fetched. This may be less than the rowset size. If RowNumber is 0, the operation applies to every row in the rowset.  
  
 The delete operation of **SQLSetPos** makes the data source delete one or more selected rows of a table. To delete rows with **SQLSetPos**, the application calls **SQLSetPos** with Operation set to SQL_DELETE and RowNumber set to the number of the row to delete. If RowNumber is 0, all rows in the rowset are deleted.  
  
 After **SQLSetPos** returns, the deleted row is the current row and its status is SQL_ROW_DELETED. The row cannot be used in any additional positioned operations, such as calls to [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md) or **SQLSetPos**.  
  
 When you delete all rows of the rowset (RowNumber is equal to 0), the application can prevent the driver from deleting certain rows by using the row operation array just like for the update operation of **SQLSetPos**.  
  
 Every row that is deleted should be a row that exists in the result set. If the application buffers were filled by fetching, and if a row status array has been maintained, its values at each of these row positions should not be SQL_ROW_DELETED, SQL_ROW_ERROR, or SQL_ROW_NOROW.  
  
 Positioned updates can also be performed using the WHERE CURRENT OF clause on UPDATE, DELETE, and INSERT statements. WHERE CURRENT OF requires a cursor name that ODBC will generate when the [SQLGetCursorName](../../relational-databases/native-client-odbc-api/sqlgetcursorname.md) function is called, or which you can specify by calling **SQLSetCursorName**. The following are general steps used to perform a WHERE CURRENT OF update in an ODBC application:  
  
-   Call **SQLSetCursorName** to establish a cursor name for the statement handle.  
  
-   Build a SELECT statement with a FOR UPDATE OF clause and execute it.  
  
-   Call **SQLFetchScroll** to retrieve a rowset or **SQLFetch** to retrieve a row.  
  
-   Call **SQLSetPos** (SQL_POSITION) to position the cursor on the row.  
  
-   Build and execute an UPDATE statement with a WHERE CURRENT OF clause using the cursor name set with **SQLSetCursorName**.  
  
 Alternatively, you could call **SQLGetCursorName** after you execute the SELECT statement instead of calling **SQLSetCursorName** before executing the SELECT statement. **SQLGetCursorName** returns a default cursor name assigned by ODBC if you do not set a cursor name using **SQLSetCursorName**.  
  
 **SQLSetPos** is preferred over WHERE CURRENT OF when you are using server cursors. If you are using a static, updatable cursor with the ODBC cursor library, the cursor library implements WHERE CURRENT OF updates by adding a WHERE clause with the key values for the underlying table. This can cause unintended updates if the keys in the table are not unique.  
  
## See Also  
 [Using Cursors &#40;ODBC&#41;](../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)  
  
  
