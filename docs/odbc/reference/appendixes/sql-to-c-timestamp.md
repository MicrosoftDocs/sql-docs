---
description: "SQL to C: Timestamp"
title: "SQL to C: Timestamp | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "timestamp data type [ODBC]"
  - "converting data from SQL to C types [ODBC], timestamp"
  - "data conversions from SQL to C types [ODBC], timestamp"
ms.assetid: 6a0617cf-d8c0-4316-8bb4-e6ddb45d7bf1
author: David-Engel
ms.author: v-davidengel
---
# SQL to C: Timestamp

The identifier for the timestamp ODBC SQL data type is the following:

- SQL_TYPE_TIMESTAMP  

The following table shows the ODBC C data types to which timestamp SQL data can be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  

|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR|*BufferLength* > Character byte length<br /><br /> 20 <= *BufferLength* <= Character byte length<br /><br /> *BufferLength* < 20|Data<br /><br /> Truncated data[b]<br /><br /> Undefined|Length of data in bytes<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_WCHAR|*BufferLength* > Character length<br /><br /> 20 <= *BufferLength* <= Character length<br /><br /> *BufferLength* < 20|Data<br /><br /> Truncated data[b]<br /><br /> Undefined|Length of data in characters<br /><br /> Length of data in characters<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_TYPE_DATE|Time portion of timestamp is zero[a]<br /><br /> Time portion of timestamp is nonzero[a]|Data<br /><br /> Truncated data[c]|6[f]<br /><br /> 6[f]|n/a<br /><br /> 01S07|  
|SQL_C_TYPE_TIME|Fractional seconds portion of timestamp is zero[a]<br /><br /> Fractional seconds portion of timestamp is nonzero[a]|Data[d]<br /><br /> Truncated data[d], [e]|6[f]<br /><br /> 6[f]|n/a<br /><br /> 01S07|  
|SQL_C_TYPE_TIMESTAMP|Fractional seconds portion of timestamp is not truncated[a]<br /><br /> Fractional seconds portion of timestamp is truncated[a]|Data[e]<br /><br /> Truncated data[e]|16[f]<br /><br /> 16[f]|n/a<br /><br /> 01S07|  

 [a]   The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of **TargetValuePtr* is the size of the C data type.  
  
 [b]   The fractional seconds of the timestamp are truncated.  
  
 [c]   The time portion of the timestamp is truncated.  
  
 [d]   The date portion of the timestamp is ignored.  
  
 [e]   The fractional seconds portion of the timestamp is truncated.  
  
 [f]   This is the size of the corresponding C data type.  

When timestamp SQL data is converted to character C data, the resulting string is in the "*yyyy*-*mm*-*dd* *hh*:*mm*:*ss*[.*f...*]" format, where up to nine digits can be used for fractional seconds. This format is not affected by the Windows® country/region setting. (Except for the decimal point and fractional seconds, the entire format must be used, regardless of the precision of the timestamp SQL data type.)
