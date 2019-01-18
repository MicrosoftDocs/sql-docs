---
title: "SQLSetStmtAttr (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetStmtAttr function [ODBC], Cursor Library"
ms.assetid: 6018a733-c2c8-4047-92ec-92cf85031767
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetStmtAttr (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetStmtAttr** function in the cursor library. For general information about **SQLSetStmtAttr**, see [SQLSetStmtAttr Function](../../../odbc/reference/syntax/sqlsetstmtattr-function.md).  
  
 The cursor library supports the following statement attributes with **SQLSetStmtAttr**:  
  
|||  
|-|-|  
|SQL_ATTR_CONCURRENCY|SQL_ATTR_ROW_BIND_OFFSET_PTR|  
|SQL_ATTR_CURSOR_TYPE|SQL_ATTR_ROW_BIND_TYPE|  
|SQL_ATTR_FETCH_BOOKMARK_PTR|SQL_ATTR_ROWSET_ARRAY_SIZE|  
|SQL_ATTR_PARAM_BIND_OFFSET_PTR|SQL_ATTR_SIMULATE_CURSOR|  
|SQL_ATTR_PARAM_BIND_TYPE|SQL_ATTR_USE_BOOKMARKS|  
  
 The cursor library supports only the SQL_CURSOR_FORWARD_ONLY and SQL_CURSOR_STATIC values of the SQL_ATTR_CURSOR_TYPE statement attribute.  
  
 For forward-only cursors, the cursor library supports the SQL_CONCUR_READ_ONLY value of the SQL_ATTR_CONCURRENCY statement attribute. For static cursors, the cursor library supports the SQL_CONCUR_READ_ONLY and SQL_CONCUR_VALUES values of the SQL_ATTR_CONCURRENCY statement attribute.  
  
 The cursor library supports only the SQL_SC_NON_UNIQUE value of the SQL_ATTR_SIMULATE_CURSOR statement attribute.  
  
 Although the ODBC specification supports calls to **SQLSetStmtAttr** with the SQL_ATTR_PARAM_BIND_TYPE or SQL_ATTR_ROW_BIND_TYPE attributes after **SQLFetch** or **SQLFetchScroll** has been called, the cursor library does not. Before it can change the binding type in the cursor library, the application must close the cursor. The cursor library supports changing the SQL_ATTR_ROW_BIND_OFFSET_PTR, SQL_ATTR_PARAM_BIND_OFFSET_PTR, SQL_ATTR_ROWS_FETCHED_PTR, and SQL_ATTR_PARAMS_PROCESSED_PTR statement attributes when a cursor is open.  
  
 An application can call **SQLSetStmtAttr** with an **Attribute** of SQL_ATTR_ROW_ARRAY_SIZE to change the rowset size while a cursor is open. The new rowset size will take effect the next time **SQLFetchScroll** or **SQLFetch** is called.  
  
 The cursor library supports setting the SQL_ATTR_PARAM_BIND_OFFSET_PTR or SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute to enable binding offsets. The binding offset will not be used for calls to **SQLFetch** when the cursor library is used with an ODBC 2.*x* driver.  
  
 The cursor library supports setting the SQL_ATTR_USE_BOOKMARKS statement attribute to SQL_UB_VARIABLE.
