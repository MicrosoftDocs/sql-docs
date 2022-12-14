---
description: "Column Size"
title: "Column Size | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], column size"
  - "size of data types [ODBC]"
  - "SQL data types [ODBC], column characteristics"
  - "column size of data types [ODBC]"
ms.assetid: 541b83ab-b16d-4714-bcb2-3c3daa9a963b
author: David-Engel
ms.author: v-davidengel
---
# Column Size
The column (or parameter) size of numeric data types is defined as the maximum number of digits used by the data type of the column or parameter, or the precision of the data. For character types, this is the length in characters of the data; for binary data types, column size is defined as the length in bytes of the data. For the time, timestamp, and all interval data types, this is the number of characters in the character representation of this data. The column size defined for each concise SQL data type is shown in the following table.  
  
|SQL type identifier|Column size|  
|-------------------------|-----------------|  
|All character types[a],[b]|The defined or maximum column size in characters of the column or parameter (as contained in the SQL_DESC_LENGTH descriptor field). For example, the column size of a single-byte character column defined as CHAR(10) is 10.|  
|SQL_DECIMAL SQL_NUMERIC|The defined number of digits. For example, the precision of a column defined as NUMERIC(10,3) is 10.|  
|SQL_BIT[c]|1|  
|SQL_TINYINT[c]|3|  
|SQL_SMALLINT[c]|5|  
|SQL_INTEGER[c]|10|  
|SQL_BIGINT[c]|19 (if signed) or 20 (if unsigned)|  
|SQL_REAL[c]|7|  
|SQL_FLOAT[c]|15|  
|SQL_DOUBLE[c]|15|  
|All binary types[a],[b]|The defined or maximum length in bytes of the column or parameter. For example, the length of a column defined as BINARY(10) is 10.|  
|SQL_TYPE_DATE[c]|10 (the number of characters in the *yyyy-mm-dd* format).|  
|SQL_TYPE_TIME[c]|8 (the number of characters in the *hh-mm-ss* format), or 9 + *s* (the number of characters in the *hh:mm:ss*[.fff...] format, where *s* is the seconds precision).|  
|SQL_TYPE_TIMESTAMP|16 (the number of characters in the *yyyy-mm-dd hh:mm* format)<br /><br /> 19 (the number of characters in the *yyyy-mm-dd* *hh:mm:ss* format)<br /><br /> or<br /><br /> 20 + *s* (the number of characters in the *yyyy-mm-dd hh:mm:ss*[.fff...] format, where *s* is the seconds precision).|  
|SQL_INTERVAL_SECOND|Where *p* is the interval leading precision and *s* is the seconds precision, *p* (if *s*=0) or *p*+*s*+1 (if *s*>0).[d]|  
|SQL_INTERVAL_DAY_TO_SECOND|Where *p* is the interval leading precision and *s* is the seconds precision, 9+*p* (if *s*=0) or 10+*p*+*s* (if *s*>0).[d]|  
|SQL_INTERVAL_HOUR_TO_SECOND|Where *p* is the interval leading precision and *s* is the seconds precision, 6+*p* (if *s*=0) or 7+*p*+*s* (if *s*>0).[d]|  
|SQL_INTERVAL_MINUTE_TO_SECOND|Where *p* is the interval leading precision and *s* is the seconds precision, 3+*p* (if *s*=0) or 4+*p*+*s* (if *s*>0).[d]|  
|SQL_INTERVAL_YEAR  SQL_INTERVAL_MONTH SQL_INTERVAL_DAY SQL_INTERVAL_HOUR SQL_INTERVAL_MINUTE|*p*, where *p* is the interval leading precision.[d]|  
|SQL_INTERVAL_YEAR_TO_MONTH SQL_INTERVAL_DAY_TO_HOUR|3+*p*, where *p* is the interval leading precision.[d]|  
|SQL_INTERVAL_DAY_TO_MINUTE|6+*p*, where *p* is the interval leading precision.[d]|  
|SQL_INTERVAL_HOUR_TO_MINUTE|3+*p*, where *p* is the interval leading precision.[d]|  
|SQL_GUID|36 (the number of characters in the *aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee* format)|  
  
 [a]   For an ODBC 1.0 application calling **SQLSetParam** in an ODBC 2.0 driver, and for an ODBC 2.0 application calling **SQLBindParameter** in an ODBC 1.0 driver, when \**StrLen_or_IndPtr* is SQL_DATA_AT_EXEC for a SQL_LONGVARCHAR or SQL_LONGVARBINARY type, *ColumnSize* must be set to the total length of the data to be sent, not the precision as defined in this table.  
  
 [b]   If the driver cannot determine the column or parameter length for a variable type, it returns SQL_NO_TOTAL.  
  
 [c]   The *ColumnSize* argument of **SQLBindParameter** is ignored for this data type.  
  
 [d]   For general rules about column length in interval data types, see [Interval Data Type Length](../../../odbc/reference/appendixes/interval-data-type-length.md), earlier in this appendix.  
  
 The values returned for the column (or parameter) size do not correspond to the values in any one descriptor field. The values can come from either the SQL_DESC_PRECISION or the SQL_DESC_LENGTH field, depending on the type of data, as shown in the following table.  
  
|SQL type|Descriptor field corresponding to<br /><br /> column or parameter size|  
|--------------|--------------------------------------------------------------------|  
|All character and binary types|LENGTH|  
|All numeric types|PRECISION|  
|All datetime and interval types|LENGTH|  
|SQL_BIT|LENGTH|
