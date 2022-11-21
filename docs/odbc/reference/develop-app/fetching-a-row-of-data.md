---
description: "Fetching a Row of Data"
title: "Fetching a Row of Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLFetch function [ODBC], fetching a row of data"
  - "cursors [ODBC], fetching rows"
  - "result sets [ODBC], fetching"
  - "fetches [ODBC], row of data"
ms.assetid: 16d4a380-0d83-456b-aeee-f10738944e86
author: David-Engel
ms.author: v-davidengel
---
# Fetching a Row of Data
To fetch a row of data, an application calls **SQLFetch**. **SQLFetch** can be called with any kind of cursor, but it only moves the rowset cursor in a forward-only direction. **SQLFetch** advances the cursor to the next row and returns the data for any columns that were bound with calls to **SQLBindCol**. When the cursor reaches the end of the result set, **SQLFetch** returns SQL_NO_DATA. For examples of calling **SQLFetch**, see [Using SQLBindCol](../../../odbc/reference/develop-app/using-sqlbindcol.md).  
  
 Exactly how **SQLFetch** is implemented is driver-specific, but the general pattern is for the driver to retrieve the data for any bound columns from the data source, convert it according to the types of the bound variables, and place the converted data in those variables. If the driver cannot convert any data, **SQLFetch** returns an error. The application can continue fetching rows, but the data for the current row is lost. What happens to the data for unbound columns depends on the driver, but most drivers either retrieve and discard it or never retrieve it at all.  
  
 The driver also sets the values of any length/indicator buffers that have been bound. If the data value for a column is NULL, the driver sets the corresponding length/indicator buffer to SQL_NULL_DATA. If the data value is not NULL, the driver sets the length/indicator buffer to the byte length of the data after conversion. If this length cannot be determined, as is sometimes the case with long data that is retrieved by more than one function call, the driver sets the length/indicator buffer to SQL_NO_TOTAL. For fixed-length data types, such as integers and date structures, the byte length is the size of the data type.  
  
 For variable-length data, such as character and binary data, the driver checks the byte length of the converted data against the byte length of the buffer bound to the column; the buffer's length is specified in the *BufferLength* argument in **SQLBindCol**. If the byte length of the converted data is greater than the byte length of the buffer, the driver truncates the data to fit in the buffer, returns the untruncated length in the length/indicator buffer, returns SQL_SUCCESS_WITH_INFO, and places SQLSTATE 01004 (Data truncated) in the diagnostics. The only exception to this is if a variable-length bookmark is truncated when returned by **SQLFetch**, which returns SQLSTATE 22001 (String data, right truncated).  
  
 Fixed-length data is never truncated, because the driver assumes that the size of the bound buffer is the size of the data type. Data truncation tends to be rare, because the application usually binds a buffer large enough to hold the entire data value; it determines the necessary size from the metadata. However, the application might explicitly bind a buffer it knows to be too small. For example, it might retrieve and display the first 20 characters of a part description or the first 100 characters of a long text column.  
  
 Character data must be null-terminated by the driver before it is returned to the application, even if it has been truncated. The null-termination character is not included in the returned byte length but does require space in the bound buffer. For example, suppose an application uses strings composed of character data in the ASCII character set, a driver has 50 characters of data to return, and the application's buffer is 25 bytes long. In the application's buffer, the driver returns the first 24 characters followed by a null-termination character. In the length/indicator buffer, it returns a byte length of 50.  
  
 The application can restrict the number of rows in the result set by setting the SQL_ATTR_MAX_ROWS statement attribute before executing the statement that creates the result set. For example, the preview mode in an application used to format reports needs only enough data to display the first page of the report. By restricting the size of the result set, such a feature would run faster. This statement attribute is intended to reduce network traffic and might not be supported by all drivers.
