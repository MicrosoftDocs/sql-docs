---
description: "SQL to C: Time"
title: "SQL to C: Time | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "converting data from SQL to C types [ODBC], time"
  - "time data type [ODBC]"
  - "data conversions from SQL to C types [ODBC], time"
ms.assetid: 6dc59973-7bb5-40f1-87c8-5bf68b3bf2ee
author: David-Engel
ms.author: v-davidengel
---
# SQL to C: Time
The identifier for the time ODBC SQL data type is:  
  
 SQL_TYPE_TIME  
  
 The following table shows the ODBC C data types to which time SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  
  
|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR|*BufferLength* > Character byte length<br /><br /> *9* <= *BufferLength* <= Character byte length<br /><br /> *BufferLength* < 9|Data<br /><br /> Truncated data[a]<br /><br /> Undefined|Length of data in bytes<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_WCHAR|*BufferLength* > Character length<br /><br /> *9* <= *BufferLength* <= Character length<br /><br /> *BufferLength* < 9|Data<br /><br /> Truncated data[a]<br /><br /> Undefined|Length of data in characters<br /><br /> Length of data in characters<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_TYPE_TIME|None[b]|Data|6[d]|n/a|  
|SQL_C_TYPE_TIMESTAMP|None[b]|Data[c]|16[d]|n/a|  
  
 [a]   The fractional seconds of the time are truncated.  
  
 [b]   The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of **TargetValuePtr* is the size of the C data type.  
  
 [c]   The date fields of the timestamp structure are set to the current date, and the fractional seconds field of the timestamp structure is set to zero.  
  
 [d]   This is the size of the corresponding C data type.  
  
 When time SQL data is converted to character C data, the resulting string is in the "*hh*:*mm*:*ss*" format. This format is not affected by the Windows® country/region setting.
