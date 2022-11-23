---
description: "Updating Rows by Bookmark with SQLBulkOperations"
title: "Updating Rows by Bookmark with SQLBulkOperations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data updates [ODBC], bookmarks"
  - "SQLBulkOperations function [ODBC], updating rows"
  - "data updates [ODBC], SQLBulkOperations"
  - "bookmarks [ODBC]"
  - "updating data [ODBC], bookmarks"
  - "updating data [ODBC], SQLBulkOperations"
ms.assetid: c9ad82b7-8dba-45b0-bdb9-f4668b37c0d6
author: David-Engel
ms.author: v-davidengel
---
# Updating Rows by Bookmark with SQLBulkOperations
When updating a row by bookmark, **SQLBulkOperations** makes the data source update one or more rows of the table. The rows are identified by the bookmark in a bound bookmark column. The row is updated using data in the application buffers for each bound column (except when the value in the length/indicator buffer for a column is SQL_COLUMN_IGNORE). Unbound columns will not be updated.  
  
 To update rows by bookmark with **SQLBulkOperations**, the application:  
  
1.  Retrieves and caches the bookmarks of all rows to be updated. If there is more than one bookmark and column-wise binding is used, the bookmarks are stored in an array; if there is more than one bookmark and row-wise binding is used, the bookmarks are stored in an array of row structures.  
  
2.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of bookmarks and binds the buffer containing the bookmark value, or the array of bookmarks, to column 0.  
  
3.  Places the new data values in the rowset buffers. For information on how to send long data with **SQLBulkOperations**, see [Long Data and SQLSetPos and SQLBulkOperations](../../../odbc/reference/develop-app/long-data-and-sqlsetpos-and-sqlbulkoperations.md).  
  
4.  Sets the value in the length/indicator buffer of each column as necessary. This is the byte length of the data or SQL_NTS for columns bound to string buffers, the byte length of the data for columns bound to binary buffers, and SQL_NULL_DATA for any columns to be set to NULL.  
  
5.  Sets the value in the length/indicator buffer of those columns that are not to be updated to SQL_COLUMN_IGNORE. Although the application can skip this step and resend existing data, this is inefficient and risks sending values to the data source that were truncated when they were read.  
  
6.  Calls **SQLBulkOperations** with the *Operation* argument set to SQL_UPDATE_BY_BOOKMARK.  
  
 For every row that is sent to the data source as an update, the application buffers should have valid row data. If the application buffers were filled by fetching, if a row status array has been maintained, and if the status value for a row is SQL_ROW_DELETED, SQL_ROW_ERROR, or SQL_ROW_NOROW, invalid data could inadvertently be sent to the data source.
