---
description: "INSERT Statement Limitations"
title: "INSERT Statement Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC SQL grammar, INSERT statement limitations"
  - "INSERT statement limitations [ODBC]"
  - "truncation of data [ODBC]"
ms.assetid: dea05698-527a-41ab-8729-bbed85556185
author: David-Engel
ms.author: v-davidengel
---
# INSERT Statement Limitations
Inserted data is truncated on the right without warning if it is too long to fit into the column.  
  
 Attempting to insert a value that is out of the range of a column's data type causes a NULL to be inserted into the column.  
  
 When a dBASE, Microsoft Excel, Paradox, or Textdriver is used, inserting a zero-length string into a column actually inserts a NULL instead.  
  
 When the Microsoft Excel driver is used, if an empty string is inserted into a column, the empty string is converted to a NULL; a searched SELECT statement that is executed with an empty string in the WHERE clause will not succeed on that column.  
  
 A table is not updatable by the Paradox driver under two conditions:  
  
-   When a unique index is not defined on the table. This is not true for an empty table, which can be updated with a single row even if a unique index is not defined on the table. If a single row is inserted in an empty table that does not have a unique index, an application cannot create a unique index or insert additional data after the single row has been inserted.  
  
-   If the Borland Database Engine is not implemented, only read and append statements are allowed on the Paradox table.  
  
 When the Text driver is used, NULL values are represented by a blank-padded string in fixed-length files, but are represented by no spaces in delimited files. For example, in the following row containing three fields, the second field is a NULL value:  
  
```  
"Smith:,, 123  
```  
  
 When the Text driver is used, all column values can be padded with leading spaces. The length of any row must be less than or equal to 65,543 bytes.
