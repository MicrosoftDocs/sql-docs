---
title: "SQL to C: Date | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "converting data from SQL to C types [ODBC], date"
  - "date data type [ODBC]"
  - "data conversions from SQL to C types [ODBC], date"
ms.assetid: 703c7960-9cf4-4d7a-9920-53b29c184f97
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C: Date
The identifier for the date ODBC SQL data type is:  
  
 SQL_TYPE_DATE  
  
 The following table shows the ODBC C data types to which date SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  
  
|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR|*BufferLength* > Character byte length<br /><br /> 11 <= *BufferLength* <= Character byte length<br /><br /> *BufferLength* < 11|Data<br /><br /> Truncated data<br /><br /> Undefined|10<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_WCHAR|*BufferLength* > Character length<br /><br /> 11 <= *BufferLength* <= Character length<br /><br /> *BufferLength* < 11|Data<br /><br /> Truncated data<br /><br /> Undefined|10<br /><br /> Length of data in characters<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_TYPE_DATE|None[a]|Data|6[c]|n/a|  
|SQL_C_TYPE_TIMESTAMP|None[a]|Data[b]|16[c]|n/a|  
  
 [a]   The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of **TargetValuePtr* is the size of the C data type.  
  
 [b]   The time fields of the timestamp structure are set to zero.  
  
 [c]   This is the size of the corresponding C data type.  
  
 When date SQL data is converted to character C data, the resulting string is in the "*yyyy*-*mm*-*dd*" format. This format is not affected by the Windows® country setting.
