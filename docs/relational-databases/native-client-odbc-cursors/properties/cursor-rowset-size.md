---
title: "Cursor Rowset Size | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "cursors [ODBC], rowset size"
  - "ODBC cursors, rowset size"
  - "rowsets [ODBC]"
ms.assetid: 2febe2ae-fdc1-490e-a79f-c516bc8e7c3f
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Cursor Rowset Size
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  ODBC cursors are not limited to fetching one row at a time. They can retrieve multiple rows in each call to **SQLFetch** or [SQLFetchScroll](../../../relational-databases/native-client-odbc-api/sqlfetchscroll.md). When you are working with a client/server database such as Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], it is more efficient to fetch several rows at a time. The number of rows returned on a fetch is called the rowset size and is specified by using the SQL_ATTR_ROW_ARRAY_SIZE of [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md).  
  
```  
SQLUINTEGER uwRowsize;  
SQLSetStmtAttr(m_hstmt, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER)uwRowsetSize, SQL_IS_UINTEGER);  
```  
  
 Cursors with a rowset size greater than 1 are called block cursors.  
  
 There are two options for binding result set columns for block cursors:  
  
-   Column-wise binding  
  
     Each column is bound to an array of variables. Each array has the same number of elements as the rowset size.  
  
-   Row-wise binding  
  
     An array is built using structures that hold the data and indicators for all the columns in a row. The array has the same number of structures as the rowset size.  
  
 When either column-wise or row-wise binding is used, each call to **SQLFetch** or **SQLFetchScroll** fills the bound arrays with data from the rowset retrieved.  
  
 [SQLGetData](../../../relational-databases/native-client-odbc-api/sqlgetdata.md) can also be used to retrieve column data from a block cursor. Because **SQLGetData** works one row at a time, **SQLSetPos** must be called to set a specific row in the rowset as the current row before calling **SQLGetData**.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver offers an optimization using rowsets to retrieve a whole result set quickly. To use this optimization, set the cursor attributes to their defaults (forward-only, read-only, rowset size = 1) at the time **SQLExecDirect** or **SQLExecute** is called. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver sets up a default result set. This is more efficient than server cursors when transferring results to the client without scrolling. After the statement has been executed, increase the rowset size and use either column-wise or row-wise binding. This lets [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] use a default result set to send result rows efficiently to the client, while the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver continuously pulls rows from the network buffers on the client.  
  
## See Also  
 [Cursor Properties](../../../relational-databases/native-client-odbc-cursors/properties/cursor-properties.md)  
  
  
