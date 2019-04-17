---
title: "Use Rowset Binding (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "rowset binding [ODBC]"
ms.assetid: a7be05f0-6b11-4b53-9fbc-501e591eef09
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use Rowset Binding (ODBC)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

    
### To use column-wise binding  
  
1.  For each bound column, do the following:  
  
    -   Allocate an array of R (or more) column buffers to store data values, where R is number of rows in the rowset.  
  
    -   Optionally, allocate an array of R (or more) column buffers to store data lengths.  
  
    -   Call [SQLBindCol](../../../relational-databases/native-client-odbc-api/sqlbindcol.md) to bind the column's data value and data length arrays to the column of the rowset.  
  
2.  Call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
    -   Set SQL_ATTR_ROW_ARRAY_SIZE to the number of rows in the rowset (R).  
  
    -   Set SQL_ATTR_ROW_BIND_TYPE to SQL_BIND_BY_COLUMN.  
  
    -   Set the SQL_ATTR_ROWS FETCHED_PTR attribute to point to a SQLUINTEGER variable to hold the number of rows fetched.  
  
    -   Set SQL_ATTR_ROW_STATUS_PTR to point to an array[R] of SQLUSSMALLINT variables to hold the row-status indicators.  
  
3.  Execute the statement.  
  
4.  Each call to [SQLFetch](https://go.microsoft.com/fwlink/?LinkId=58401) or [SQLFetchScroll](../../../relational-databases/native-client-odbc-api/sqlfetchscroll.md) retrieves R rows and transfers the data into the bound columns.  
  
### To use row-wise binding  
  
1.  Allocate an array[R] of structures, where R is the number of rows in the rowset. The structure has one element for each column, and each element has two parts:  
  
    -   The first part is a variable of the appropriate data type to hold the column data.  
  
    -   The second part is a SQLINTEGER variable to hold the column status indicator.  
  
2.  Call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
    -   Set SQL_ATTR_ROW_ARRAY_SIZE to the number of rows in the rowset (R).  
  
    -   Set SQL_ATTR_ROW_BIND_TYPE to the size of the structure allocated in Step 1.  
  
    -   Set the SQL_ATTR_ROWS_FETCHED_PTR attribute to point to a SQLUINTEGER variable to hold the number of rows fetched.  
  
    -   Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[R] of SQLUSSMALLINT variables to hold the row-status indicators.  
  
3.  For each column in the result set, call [SQLBindCol](../../../relational-databases/native-client-odbc-api/sqlbindcol.md) to point the data value and data length pointer of the column to their variables in the first element of the array of structures allocated in Step 1.  
  
4.  Execute the statement.  
  
5.  Each call to [SQLFetch](https://go.microsoft.com/fwlink/?LinkId=58401) or [SQLFetchScroll](../../../relational-databases/native-client-odbc-api/sqlfetchscroll.md) retrieves R rows and transfers the data into the bound columns.  
  
## See Also  
 [Using Cursors How-to Topics &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-how-to/cursors/using-cursors-how-to-topics-odbc.md)   
 [How Cursors Are Implemented](../../../relational-databases/native-client-odbc-cursors/implementation/how-cursors-are-implemented.md)   
 [Use Cursors &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-how-to/cursors/use-cursors-odbc.md)  
  
  
