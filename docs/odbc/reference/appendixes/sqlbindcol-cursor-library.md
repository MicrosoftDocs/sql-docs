---
description: "SQLBindCol (Cursor Library)"
title: "SQLBindCol (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLAllocStmt function [ODBC], Cursor Library"
ms.assetid: f4dd546a-0a6c-4397-8ee7-fafa6b9da543
author: David-Engel
ms.author: v-davidengel
---
# SQLBindCol (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLBindCol** function in the cursor library. For general information about **SQLBindCol**, see [SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md).  
  
 An application allocates one or more buffers for the cursor library to return the current rowset in. It calls **SQLBindCol** one or more times to bind these buffers to the result set.  
  
 An application can call **SQLBindCol** to rebind result set columns after it has called **SQLExtendedFetch**, **SQLFetch**, or **SQLFetchScroll**,as long as the C data type, column size, and decimal digits of the bound column remain the same. The application need not close the cursor to rebind columns to different addresses.  
  
 The cursor library supports setting the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute to use bind offsets. (**SQLBindCol** does not have to be called for this rebinding to occur.) If the cursor library is used with an ODBC *3.x* driver, the bind offset is not used when **SQLFetch** is called. The bind offset is used if **SQLFetch** is called when the cursor library is used with an ODBC *2.x* driver because **SQLFetch** is then mapped to **SQLExtendedFetch**.  
  
 The cursor library supports calling **SQLBindCol** to bind the bookmark column.  
  
 When working with an ODBC *2.x* driver, the cursor library returns SQLSTATE HY090 (Invalid string or buffer length) when **SQLBindCol** is called to set the buffer length for a bookmark column to a value not equal to 4. When working with an ODBC *3.x* driver, the cursor library allows the buffer to be any size.
