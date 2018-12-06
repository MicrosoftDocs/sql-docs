---
title: "Inserting Rows with SQLBulkOperations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLBulkOperations function [ODBC], inserting rows"
  - "data updates [ODBC], SQLBulkOperations"
  - "updating data [ODBC], SQLBulkOperations"
ms.assetid: ed585ea7-4d56-4df9-8dc3-53ca82382450
author: MightyPen
ms.author: genemi
manager: craigg
---
# Inserting Rows with SQLBulkOperations
Inserting data with **SQLBulkOperations** is similar to updating data with **SQLBulkOperations** because it uses data from the bound application buffers.  
  
 So that each column in the new row has a value, all bound columns with a length/indicator value of SQL_COLUMN_IGNORE and all unbound columns must either accept NULL values or have a default.  
  
 To insert rows with **SQLBulkOperations**, the application does the following:  
  
1.  Sets the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of rows to insert and places the new data values in the bound application buffers. For information on how to send long data with **SQLBulkOperations**, see [Long Data and SQLSetPos and SQLBulkOperations](../../../odbc/reference/develop-app/long-data-and-sqlsetpos-and-sqlbulkoperations.md).  
  
2.  Sets the value in the length/indicator buffer of each column as necessary. This is the byte length of the data or SQL_NTS for columns bound to string buffers, the byte length of the data for columns bound to binary buffers, and SQL_NULL_DATA for any columns to be set to NULL. The application sets the value in the length/indicator buffer of those columns that are to be set to their default (if one exists) or NULL (if one does not) to SQL_COLUMN_IGNORE.  
  
3.  Calls **SQLBulkOperations** with the *Operation* argument set to SQL_ADD.  
  
 After **SQLBulkOperations** returns, the current row is unchanged. If the bookmark column (column 0) is bound, **SQLBulkOperations** returns the bookmarks of the inserted rows in the rowset buffer bound to that column.
