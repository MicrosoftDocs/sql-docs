---
description: "Number of Rows Fetched and Status"
title: "Number of Rows Fetched and Status | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "row status array [ODBC]"
  - "number of rows fetched [ODBC]"
  - "result sets [ODBC], row status array"
ms.assetid: a069b979-5108-4905-932f-8ae8e7905ff2
author: David-Engel
ms.author: v-davidengel
---
# Number of Rows Fetched and Status
If the SQL_ATTR_ROWS_FETCHED_PTR statement attribute has been set, it specifies a buffer that returns the number of rows fetched by the call to **SQLFetch** or **SQLFetchScroll**, and error rows. (This number is a count of all rows that do not have the status SQL_ROW_NO_ROWS.) After a call to **SQLBulkOperations** or **SQLSetPos**, the buffer contains the number of rows that were affected by a bulk operation performed by the function. If the SQL_ATTR_ROW_STATUS_PTR statement attribute has been set, **SQLFetch** or **SQLFetchScroll** returns the *row status array,* which provides the status of each returned row. Both of the buffers pointed to by these fields are allocated by the application and populated by the driver. An application must make sure that these pointers remain valid until the cursor is closed.  
  
 Entries in the row status array state whether each row was fetched successfully, whether it was updated, added, or deleted since it was last fetched, and whether an error occurred while fetching the row. If **SQLFetch** or **SQLFetchScroll** encounters an error while retrieving one row of a multirow rowset, or if **SQLBulkOperations** with an *Operation* argument of SQL_FETCH_BY_BOOKMARK encounters an error while performing a bulk fetch, it sets the corresponding value in the row status array to SQL_ROW_ERROR, continues fetching rows, and returns SQL_SUCCESS_WITH_INFO. For more information about error handling and the row status array, see the [SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md) and [SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md) function descriptions.
