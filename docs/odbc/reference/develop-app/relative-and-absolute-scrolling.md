---
description: "Relative and Absolute Scrolling"
title: "Relative and Absolute Scrolling | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "absolute scrolling [ODBC]"
  - "relative scrolling [ODBC]"
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], scrollable"
ms.assetid: 3d0ff48d-fef5-4c01-bb1d-a583e6269b66
author: David-Engel
ms.author: v-davidengel
---
# Relative and Absolute Scrolling
Most of the scrolling options in **SQLFetchScroll** position the cursor relative to the current position or to an absolute position. **SQLFetchScroll** supports fetching the next, prior, first, and last rowsets, as well as relative fetching (fetch the rowset *n* rows from the start of the current rowset) and absolute fetching (fetch the rowset starting at row *n*). If *n* is negative in an absolute fetch, rows are counted from the end of the result set. Thus, an absolute fetch of row -1 means to fetch the rowset that starts with the last row in the result set.  
  
 Dynamic cursors detect rows inserted into and deleted from the result set, so there is no easy way for dynamic cursors to retrieve the row at a particular number other than reading from the start of the result set, which is likely to be slow. Furthermore, absolute fetching is not very useful in dynamic cursors because row numbers change as rows are inserted and deleted; therefore, successively fetching the same row number can yield different rows.  
  
 Applications that use **SQLFetchScroll** only for its block cursor capabilities, such as reports, are likely to pass through the result set a single time, using only the option to fetch the next rowset. Screen-based applications, on the other hand, can take advantage of all the capabilities of **SQLFetchScroll**. If the application sets the rowset size to the number of rows displayed on the screen and binds the screen buffers to the result set, it can translate scroll bar operations directly to calls to **SQLFetchScroll**.  
  
|Scroll bar operation|SQLFetchScroll scrolling option|  
|--------------------------|-------------------------------------|  
|Page up|SQL_FETCH_PRIOR|  
|Page down|SQL_FETCH_NEXT|  
|Line up|SQL_FETCH_RELATIVE with *FetchOffset* equal to -1|  
|Line down|SQL_FETCH_RELATIVE with *FetchOffset* equal to 1|  
|Scroll box at top|SQL_FETCH_FIRST|  
|Scroll box at bottom|SQL_FETCH_LAST|  
|Random scroll box position|SQL_FETCH_ABSOLUTE|  
  
 Such applications also need to position the scroll box after a scrolling operation, which requires the current row number and the number of rows. For the current row number, applications can either keep track of the current row number or call **SQLGetStmtAttr** with the SQL_ATTR_ROW_NUMBER attribute to retrieve it.  
  
 The number of rows in the cursor, which is the size of the result set, is available as the SQL_DIAG_CURSOR_ROW_COUNT field of the diagnostic header. The value in this field is defined only after **SQLExecute**, **SQLExecDirect**, or **SQLMoreResult** has been called. This count can be either an approximate count or an exact count, depending on the capabilities of the driver. The driver's support can be determined by calling **SQLGetInfo** with the cursor attributes information types and checking whether the SQL_CA2_CRC_APPROXIMATE or SQL_CA2_CRC_EXACT bit is returned for the type of cursor.  
  
 An exact row count is never supported for a dynamic cursor. For other types of cursors, the driver can support either exact or approximate row counts, but not both. If the driver supports neither exact nor approximate row counts for a specific cursor type, the SQL_DIAG_CURSOR_ROW_COUNT field contains the number of rows that have been fetched so far. Regardless of what the driver supports, **SQLFetchScroll** with an *Operation* of SQL_FETCH_LAST will cause the SQL_DIAG_CURSOR_ROW_COUNT field to contain the exact row count.
