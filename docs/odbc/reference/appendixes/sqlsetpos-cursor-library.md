---
description: "SQLSetPos (Cursor Library)"
title: "SQLSetPos (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLSetPos function [ODBC], Cursor Library"
ms.assetid: 574399c3-2bb2-4d19-829c-7c77bd82858d
author: David-Engel
ms.author: v-davidengel
---
# SQLSetPos (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetPos** function in the cursor library. For general information about **SQLSetPos**, see [SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
 The cursor library supports the SQL_POSITION operation only for the *Operation* argument in **SQLSetPos**. It supports the SQL_LOCK_NO_CHANGE value only for the *LockType* argument.  
  
 If the driver does not support bulk operations, the cursor library returns SQLSTATE HYC00 (Driver not capable) when **SQLSetPos** is called with *RowNumber* equal to 0. This driver behavior is not recommended.  
  
 The cursor library does not support the SQL_UPDATE and SQL_DELETE operations in a call to **SQLSetPos**. The cursor library implements a positioned update or delete SQL statement by creating a searched update or delete statement with a WHERE clause that enumerates the values stored in its cache for each bound column. For more information, see [Processing Positioned Update and Delete Statements](../../../odbc/reference/appendixes/processing-positioned-update-and-delete-statements.md).  
  
 If the driver does not support static cursors, an application working with the cursor library should call **SQLSetPos** only on a rowset fetched by **SQLExtendedFetch** or **SQLFetchScroll**, not by **SQLFetch**. The cursor library implements **SQLExtendedFetch** and **SQLFetchScroll** by making repeated calls of **SQLFetch** (with a rowset size of 1) in the driver. The cursor library passes calls to **SQLFetch**, on the other hand, through to the driver. If **SQLSetPos** is called on a multirow rowset fetched by **SQLFetch** when the driver does not support static cursors, the call will fail because **SQLSetPos** does not work with forward-only cursors. This will occur even if an application has successfully called **SQLSetStmtAttr** to set SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_STATIC, which the cursor library supports even if the driver does not support static cursors.
