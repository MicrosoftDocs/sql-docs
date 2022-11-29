---
description: "SQLGetData (Cursor Library)"
title: "SQLGetData (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLGetData function [ODBC], Cursor Library"
ms.assetid: ff40c9c0-b847-4426-a099-1bff47e6e872
author: David-Engel
ms.author: v-davidengel
---
# SQLGetData (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLGetData** function in the cursor library. For general information about **SQLGetData**, see [SQLGetData Function](../../../odbc/reference/syntax/sqlgetdata-function.md).  
  
 The cursor library implements **SQLGetData** by first constructing a **SELECT** statement with a **WHERE** clause that enumerates the values stored in its cache for each bound column in the current row. It then executes the **SELECT** statement to reselect the row and calls **SQLGetData** in the driver to retrieve the data from the data source (as opposed to the cache).  
  
> [!CAUTION]  
>  The **WHERE** clause constructed by the cursor library to identify the current row can fail to identify any rows, identify a different row, or identify more than one row. For more information, see [Constructing Searched Statements](../../../odbc/reference/appendixes/constructing-searched-statements.md).  
  
 If the SQL_ATTR_USE_BOOKMARKS statement attribute is set to SQL_UB_VARIABLE, **SQLGetData** can be called on column 0 to return bookmark data.  
  
 Calls to **SQLGetData** are subject to the following restrictions:  
  
-   **SQLGetData** cannot be called for forward-only cursors.  
  
-   **SQLGetData** can be called only when the following conditions are met: a **SELECT** statement generated the result set; the **SELECT** statement did not contain a join, a **UNION** clause, or a **GROUP BY** clause; and any columns that used an alias or expression in the select list were not bound with **SQLBindCol**.  
  
-   If the driver supports only one active statement, the cursor library fetches the rest of the result set before executing the **SELECT** statement and calling **SQLGetData**.
