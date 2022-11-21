---
description: "Processing Positioned Update and Delete Statements"
title: "Processing Positioned Update and Delete Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "positioned deletes [ODBC]"
  - "ODBC cursor library [ODBC], statement processing"
  - "cursor library [ODBC], positioned update or delete"
  - "positioned updates [ODBC]"
  - "SQL statements [ODBC], cursor library"
  - "ODBC cursor library [ODBC], positioned update or delete"
  - "cursor library [ODBC], statement processing"
ms.assetid: 2975dd97-48e6-4d0a-a9c7-40759a7d94c8
author: David-Engel
ms.author: v-davidengel
---
# Processing Positioned Update and Delete Statements
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 The cursor library supports positioned update and delete statements by replacing the **WHERE CURRENT OF** clause in such statements with a **WHERE** clause that enumerates the values stored in its cache for each bound column. The cursor library passes the newly constructed **UPDATE** and **DELETE** statements to the driver for execution. For positioned update statements, the cursor library then updates its cache from the values in the rowset buffers and sets the corresponding value in the row status array to SQL_ROW_UPDATED. For positioned delete statements, it sets the corresponding value in the row status array to SQL_ROW_DELETED.  
  
> [!CAUTION]  
>  The **WHERE** clause constructed by the cursor library to identify the current row can fail to identify any rows, identify a different row, or identify more than one row. For more information, see [Constructing Searched Statements](../../../odbc/reference/appendixes/constructing-searched-statements.md), later in this appendix.  
  
 Positioned update and delete statements are subject to the following restrictions:  
  
-   Positioned update and delete statements can be used only in the following cases: when a **SELECT** statement generated the result set; when the **SELECT** statement did not contain a join, a **UNION** clause, or a **GROUP BY** clause; and when any columns that used an alias or expression in the select list were not bound with **SQLBindCol**.  
  
-   If an application prepares a positioned update or delete statement, it must do so after it has called **SQLFetch** or **SQLFetchScroll**. Although the cursor library submits the statement to the driver for preparation, it closes the statement and executes it directly when the application calls **SQLExecute**.  
  
-   If the driver supports only one active statement, the cursor library fetches the rest of the result set and then refetches the current rowset from its cache before it executes a positioned update or delete statement. If the application then calls a function that returns metadata in a result set (for example, **SQLNumResultCols** or **SQLDescribeCol**), the cursor library returns an error.  
  
-   If a positioned update or delete statement is performed on a column of a table that includes a timestamp column that is automatically updated every time an update is performed, all subsequent positioned update or delete statements will fail if the timestamp column is bound. This occurs because the searched update or delete statement that the cursor library creates will not accurately identify the row to update. The value in the searched statement for the timestamp column will not match the automatically updated value of the timestamp column.
