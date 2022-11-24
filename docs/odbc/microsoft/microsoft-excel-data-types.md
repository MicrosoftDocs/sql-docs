---
description: "Microsoft Excel Data Types"
title: "Microsoft Excel Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], Excel driver"
  - "Excel data types [ODBC]"
  - "Jet-based ODBC drivers [ODBC], Excel driver"
  - "desktop database drivers [ODBC], Excel driver"
  - "ODBC desktop database drivers [ODBC], Excel driver"
  - "Excel driver [ODBC], data types"
ms.assetid: 7b44c8e5-0bc3-4912-8a5d-56f4d5562fe6
author: David-Engel
ms.author: v-davidengel
---
# Microsoft Excel Data Types
The following table shows how Microsoft Excel driver data types are mapped to ODBC SQL data types. The Microsoft Excel driver assigns these data types to columns in Microsoft Excel tables based on the data in the column.  
  
|Microsoft Excel data type|ODBC data type|  
|-------------------------------|--------------------|  
|CURRENCY|SQL_NUMERIC|  
|DATETIME|SQL_TIMESTAMP|  
|LOGICAL|SQL_BIT|  
|NUMBER|SQL_DOUBLE|  
|TEXT|SQL_VARCHAR|  
  
> [!NOTE]  
>  **SQLGetTypeInfo** returns ODBC SQL data types. All conversions in Appendix D of the *ODBC Programmer's Reference* are supported for the ODBC SQL data types listed earlier in this topic.  
  
 The following table shows limitations on Microsoft Excel data types.  
  
|Data type|Description|  
|---------------|-----------------|  
|Encrypted data|The Microsoft Excel driver cannot read encrypted data.|  
|Error Strings|The Microsoft Excel driver cannot return a character string for the Microsoft Excel error values (#N/A!, #VALUE!, #REF!, #DIV/0!, #NUM!, #NAME?, and #NULL!), but returns a NULL instead.|  
|LOGICAL|The value in a LOGICAL column is returned in a SQL_C_CHAR buffer as either 0 or 1.|  
|NUMBER|If an integer column is created, numbers that are too big for the integer data type can be entered, and data containing non-integer values can be inserted, with the result that the column may be converted to SQL_DOUBLE.|  
|TEXT|When the rows of a column contain more than one Microsoft Excel data type, the ODBC Microsoft Excel driver assigns the SQL_VARCHAR data type to the column. There is one exception to this: if the column contains only two or three of the datetime data types (DATE, TIME, and DATETIME), the ODBC Microsoft Excel driver assigns the SQL_TIMESTAMP data type to the column.<br /><br /> Creating a TEXT column of zero or unspecified length actually returns a 255-byte column.<br /><br /> A character string literal can contain any ANSI character (1-255 decimal). Use two consecutive single quotation marks (") to represent one single quotation mark (').<br /><br /> Inserting a NULL into a column with a data type other than SQL_VARCHAR will cause the data type of the column to change to SQL_VARCHAR.|  
  
 More limitations on data types can be found in [Data Type Limitations](../../odbc/microsoft/data-type-limitations.md).
