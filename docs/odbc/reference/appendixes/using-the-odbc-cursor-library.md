---
title: "Using the ODBC Cursor Library | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], using cursor library"
  - "cursor library [ODBC], using cursor library"
ms.assetid: 9653f2f8-ccfc-4220-99ef-601dc0fa641c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using the ODBC Cursor Library
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 To use the ODBC cursor library, an application:  
  
1.  Calls **SQLSetConnectAttr** with an *Attribute* of SQL_ATTR_ODBC_CURSORS to specify how the cursor library should be used with a particular connection. The cursor library can be always used (SQL_CUR_USE_ODBC), used only if driver does not support scrollable cursors (SQL_CUR_USE_IF_NEEDED), or never used (SQL_CUR_USE_DRIVER).  
  
2.  Calls **SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect** to connect to the data source.  
  
3.  Calls **SQLSetStmtAttr** to specify the cursor type (SQL_ATTR_CURSOR_TYPE), concurrency (SQL_ATTR_CONCURRENCY), and rowset size (SQL_ATTR_ROW_ARRAY_SIZE). The cursor library supports forward-only and static cursors. Forward-only cursors must be read-only, while static cursors can be read-only or can use optimistic concurrency control comparing values.  
  
4.  Allocates one or more rowset buffers and calls **SQLBindCol** one or more times to bind these buffers to result set columns.  
  
5.  Generates a result set by executing a **SELECT** statement or a procedure, or by calling a catalog function. If the application will execute positioned update statements, it should execute a **SELECT FOR UPDATE** statement to generate the result set.  
  
6.  Calls **SQLFetch** or **SQLFetchScroll** one or more times to scroll through the result set.  
  
 The application can change data values in the rowset buffers. To refresh the rowset buffers with data from the cursor library's cache, an application calls **SQLFetchScroll** with the *FetchOrientation* argument set to SQL_FETCH_RELATIVE and the *FetchOffset* argument set to 0.  
  
 To retrieve data from an unbound column, the application calls **SQLSetPos** to position the cursor on the desired row. It then calls **SQLGetData** to retrieve the data.  
  
 To determine the number of rows that have been retrieved from the data source, the application calls **SQLRowCount**.
