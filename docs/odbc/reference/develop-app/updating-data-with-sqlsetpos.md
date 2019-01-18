---
title: "Updating Data with SQLSetPos | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "updating data [ODBC], SQLSetPos"
  - "data updates [ODBC], SQLSetPos"
  - "SQLSetPos function [ODBC], updating data"
ms.assetid: e9625b59-06a0-4883-b155-b932ba7528d9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Updating Data with SQLSetPos
Applications can update or delete any row in the rowset with **SQLSetPos**. Calling **SQLSetPos** is a convenient alternative to constructing and executing an SQL statement. It lets an ODBC driver support positioned updates even when the data source does not support positioned SQL statements. It is part of the paradigm of achieving complete database access by means of function calls.  
  
 **SQLSetPos** operates on the current rowset and can be used only after a call to **SQLFetchScroll**. The application specifies the number of the row to update, delete, or insert, and the driver retrieves the new data for that row from the rowset buffers. **SQLSetPos** can also be used to designate a specified row as the current row, or to refresh a particular row in the rowset from the data source.  
  
 Rowset size is set by a call to **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_ROW_ARRAY_SIZE. **SQLSetPos** uses a new rowset size, however, only after a call to **SQLFetch** or **SQLFetchScroll**. For example, if the rowset size is changed, **SQLSetPos** is called and then **SQLFetch** or **SQLFetchScroll** is called, and the call to **SQLSetPos** uses the old rowset size while **SQLFetch** or **SQLFetchScroll** uses the new rowset size.  
  
 The first row in the rowset is row number 1. The *RowNumber* argument in **SQLSetPos** must identify a row in the rowset; that is, its value must be in the range between 1 and the number of rows that were most recently fetched (which may be less than the rowset size). If *RowNumber* is 0, the operation applies to every row in the rowset.  
  
 Because most interaction with relational databases is done through SQL, **SQLSetPos** is not widely supported. However, a driver can easily emulate it by constructing and executing an **UPDATE** or **DELETE** statement.  
  
 To determine what operations **SQLSetPos** supports, an application calls **SQLGetInfo** with the SQL_DYNAMIC_CURSOR_ATTRIBUTES1, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1, SQL_KEYSET_CURSOR_ATTRIBUTES1, or SQL_STATIC_CURSOR_ATTRIBUTES1 information option (depending on the type of the cursor).  
  
 This section contains the following topics.  
  
-   [Updating Rows in the Rowset with SQLSetPos](../../../odbc/reference/develop-app/updating-rows-in-the-rowset-with-sqlsetpos.md)  
  
-   [Deleting Rows in the Rowset with SQLSetPos](../../../odbc/reference/develop-app/deleting-rows-in-the-rowset-with-sqlsetpos.md)
