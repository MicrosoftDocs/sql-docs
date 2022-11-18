---
description: "Fetch and Update Rowsets (ODBC)"
title: "Fetch and Update Rowsets (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "rowsets [ODBC]"
ms.assetid: cf0eb3b4-8b72-49fc-a845-95edc360cf93
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Fetch and Update Rowsets (ODBC)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

    
### To fetch and update rowsets  
  
1.  Optionally, call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) with SQL_ROW_ARRAY_SIZE to change the number of rows (R) in the rowset.  
  
2.  Call [SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md) or [SQLFetchScroll](../../../relational-databases/native-client-odbc-api/sqlfetchscroll.md) to get a rowset.  
  
3.  If bound columns are used, use the data values and data lengths now available in the bound column buffers for the rowset.  
  
     If unbound columns are used, for each row call [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md) with SQL_POSITION to set the cursor position; then, for each unbound column:  
  
    -   Call [SQLGetData](../../../relational-databases/native-client-odbc-api/sqlgetdata.md) one or more times to get the data for unbound columns after the last bound column of the rowset. Calls to [SQLGetData](../../../relational-databases/native-client-odbc-api/sqlgetdata.md) should be in order of increasing column number.  
  
    -   Call [SQLGetData](../../../relational-databases/native-client-odbc-api/sqlgetdata.md) multiple times to get data from a text or image column.  
  
4.  Set up any data-at-execution text or image columns.  
  
5.  Call [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md) or [SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md) to set the cursor position, refresh, update, delete, or add row(s) within the rowset.  
  
     If data-at-execution text or image columns are used for an update or add operation, handle them.  
  
6.  Optionally, execute a positioned UPDATE or DELETE statement, specifying the cursor name (available from [SQLGetCursorName](../../../relational-databases/native-client-odbc-api/sqlgetcursorname.md)) and using a different statement handle on the same connection.  
  
## See Also  
 [Using Cursors How-to Topics &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-how-to/cursors/using-cursors-how-to-topics-odbc.md)  
  
