---
title: "Microsoft Access Data Types | Microsoft Docs"
ms.custom: ""
ms.date: 01/19/2019
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC desktop database drivers [ODBC], Access driver"
  - "Jet-based ODBC drivers [ODBC], Access driver"
  - "desktop database drivers [ODBC], Access driver"
  - "Access driver [ODBC], data types"
  - "access data types [ODBC]"
  - "data types [ODBC], Access driver"
ms.assetid: b537348a-bea0-4bd6-84a4-52a75292957f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft Access Data Types
The following table shows the Microsoft Access data types, data types used to create tables, and ODBC SQL data types.  
  
|Microsoft Access data type|Data type (CREATETABLE)|ODBC SQL data type|  
|--------------------------------|-------------------------------|------------------------|  
|BIGBINARY[1]|LONGBINARY|SQL_LONGVARBINARY|  
|BINARY|BINARY|SQL_BINARY|  
|BIT|BIT|SQL_BIT|  
|COUNTER|COUNTER|SQL_INTEGER|  
|CURRENCY|CURRENCY|SQL_NUMERIC|  
|DATE/TIME|DATETIME|SQL_TIMESTAMP|  
|GUID|GUID|SQL_GUID|  
|LONG BINARY|LONGBINARY|SQL_LONGVARBINARY|  
|LONG TEXT|LONGTEXT|SQL_LONGVARCHAR[2] SQL_WLONGVARCHAR[3]|  
|MEMO|LONGTEXT|SQL_LONGVARCHAR[2] SQL_WLONGVARCHAR[3]|  
|NUMBER (FieldSize= SINGLE)|SINGLE|SQL_REAL|  
|NUMBER (FieldSize= DOUBLE)|DOUBLE|SQL_DOUBLE|  
|NUMBER (FieldSize= BYTE)|UNSIGNED BYTE|SQL_TINYINT|  
|NUMBER (FieldSize= INTEGER)|SHORT|SQL_SMALLINT|  
|NUMBER (FieldSize= LONG INTEGER)|LONG|SQL_INTEGER|  
|NUMERIC|NUMERIC|SQL_NUMERIC|  
|OLE|LONGBINARY|SQL_LONGVARBINARY|  
|TEXT|VARCHAR|SQL_VARCHAR[1] SQL_WVARCHAR[2]|  
|VARBINARY|VARBINARY|SQL_VARBINARY|  
  
 [1]   Access 4.0 applications only. Maximum length of 4000 bytes. Behavior similar to LONGBINARY.  
  
 [2]   ANSI applications only.  
  
 [3]   Unicode and Access 4.0 applications only.  
  
> [!NOTE]  
>  **SQLGetTypeInfo** returns ODBC data types. It will not return all Microsoft Access data types if more than one Microsoft Access type is mapped to the same ODBC SQL data type. All conversions in Appendix D of the *ODBC Programmer's Reference* are supported for the SQL data types listed in the previous table.  
  
 The following table shows limitations on Microsoft Access data types.  
  
|Data type|Description|  
|---------------|-----------------|  
|BINARY, VARBINARY, and VARCHAR|Creating a BINARY, VARBINARY, or VARCHAR column of zero or unspecified length actually returns a 510-byte column.|  
|BYTE|Even though a Microsoft Access NUMBER field with a FieldSize equal to BYTE is unsigned, a negative number can be inserted into the field when using the Microsoft Access driver.|  
|CHAR, LONGVARCHAR, and VARCHAR|A character string literal can contain any ANSI character (1-255 decimal). Use two consecutive single quotation marks ('') to represent one single quotation mark (').<br /><br /> Procedures should be used to pass character data when using any special character in a character data type column.|  
|DATE|Date values must be either delimited according to the ODBC canonical date format or delimited by the datetime delimiter ("#"). Otherwise, Microsoft Access will treat the value as an arithmetic expression and will not raise a warning or error.<br /><br /> For example, the date "March 5, 1996" must be represented as {d '1996-03-05'} or #03/05/1996#; otherwise, if only 03/05/1993 is submitted, Microsoft Access will evaluate this as 3 divided by 5 divided by 1996. This value rounds up to the integer 0, and since the zero day maps to 1899-12-31, this is the date used.<br /><br /> A pipe character (&#124;) cannot be used in a date value, even if enclosed in back quotes.|  
|GUID|Data type limited to Microsoft Access 4.0.|  
|NUMERIC|Data type limited to Microsoft Access 4.0.|  
  
 More limitations on data types can be found in [Data Type Limitations](../../odbc/microsoft/data-type-limitations.md).
