---
description: "Updating Data with SQLBulkOperations"
title: "Updating Data with SQLBulkOperations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLBulkOperations function [ODBC], updating data"
  - "data updates [ODBC], SQLBulkOperations"
  - "updating data [ODBC], SQLBulkOperations"
ms.assetid: 7645a704-341e-4267-adbe-061a9fda225b
author: David-Engel
ms.author: v-davidengel
---
# Updating Data with SQLBulkOperations
Applications can perform bulk update, delete, fetch, or insertion operations on the underlying table at the data source with a call to **SQLBulkOperations**. Calling **SQLBulkOperations** is a convenient alternative to constructing and executing an SQL statement. It lets an ODBC driver support positioned updates even when the data source does not support positioned SQL statements. It is part of the paradigm of achieving complete database access by means of function calls.  
  
 **SQLBulkOperations** operates on the current rowset and can be used only after a call to **SQLFetch** or **SQLFetchScroll**. The application specifies the rows to update, delete, or refresh by caching their bookmarks. The driver retrieves the new data for rows to be updated, or the new data to be inserted into the underlying table, from the rowset buffers.  
  
 The rowset size to be used by **SQLBulkOperations** is set by a call to **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_ROW_ARRAY_SIZE. Unlike **SQLSetPos**, which uses a new rowset size only after a call to **SQLFetch** or **SQLFetchScroll**, **SQLBulkOperations** uses the new rowset size after the call to **SQLSetStmtAttr**.  
  
 Because most interaction with relational databases is done through SQL, **SQLBulkOperations** is not widely supported. However, a driver can easily emulate it by constructing and executing an **UPDATE**, **DELETE**, or **INSERT** statement.  
  
 To determine what operations **SQLBulkOperation** supports, an application calls **SQLGetInfo** with the SQL_DYNAMIC_CURSOR_ATTRIBUTES1, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1, SQL_KEYSET_CURSOR_ATTRIBUTES1, or SQL_STATIC_CURSOR_ATTRIBUTES1 information option (depending on the type of the cursor).  
  
 This section contains the following topics.  
  
-   [Updating Rows by Bookmark with SQLBulkOperations](../../../odbc/reference/develop-app/updating-rows-by-bookmark-with-sqlbulkoperations.md)  
  
-   [Deleting Rows by Bookmark with SQLBulkOperations](../../../odbc/reference/develop-app/deleting-rows-by-bookmark-with-sqlbulkoperations.md)  
  
-   [Inserting Rows with SQLBulkOperations](../../../odbc/reference/develop-app/inserting-rows-with-sqlbulkoperations.md)  
  
-   [Fetching Rows with SQLBulkOperations](../../../odbc/reference/develop-app/fetching-rows-with-sqlbulkoperations.md)
