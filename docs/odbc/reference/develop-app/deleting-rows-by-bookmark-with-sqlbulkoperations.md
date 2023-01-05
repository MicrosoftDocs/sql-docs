---
description: "Deleting Rows by Bookmark with SQLBulkOperations"
title: "Deleting Rows by Bookmark with SQLBulkOperations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data updates [ODBC], SQLBulkOperations"
  - "SQLBulkOperations function [ODBC], deleting rows"
  - "updating data [ODBC], SQLBulkOperations"
ms.assetid: 46139ec9-7095-481a-bf45-20200a2fdc03
author: David-Engel
ms.author: v-davidengel
---
# Deleting Rows by Bookmark with SQLBulkOperations
When deleting a row by bookmark, **SQLBulkOperations** makes the data source delete one or more selected rows of the table. The rows are identified by the bookmark in a bound bookmark column.  
  
 To delete rows by bookmark with **SQLBulkOperations**, the application does the following:  
  
1.  Retrieves and caches the bookmarks of all rows to be deleted. If there is more than one bookmark and column-wise binding is used, the bookmarks are stored in an array; if there is more than one bookmark and row-wise binding is used, the bookmarks are stored in an array of row structures.  
  
2.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of bookmarks and binds the buffer containing the bookmark value, or the array of bookmarks, to column 0.  
  
3.  Calls **SQLBulkOperations** with *Operation* set to SQL_DELETE_BY_BOOKMARK.
