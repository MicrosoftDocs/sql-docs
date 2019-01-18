---
title: "SQL to C: Day-Time Intervals | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "converting data from SQL to C types [ODBC], day-time intervals"
  - "day-time intervals [ODBC]"
  - "data conversions from SQL to C types [ODBC], day-time intervals"
  - "intervals [ODBC], converting"
ms.assetid: 8ea84d69-2292-4128-89a0-f184f68abb98
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C: Day-Time Intervals
The identifiers for the day-time interval ODBC SQL data types are:  
  
 SQL_INTERVAL_DAY  
  
 SQL_INTERVAL_DAY_TO_MINUTE  
  
 SQL_INTERVAL_HOUR  
  
 SQL_INTERVAL_DAY_TO_SECOND  
  
 SQL_INTERVAL_MINUTE  
  
 SQL_INTERVAL_HOUR_TO_MINUTE  
  
 SQL_INTERVAL_SECOND  
  
 SQL_INTERVAL_HOUR_TO_SECOND  
  
 SQL_INTERVAL_DAY_TO_HOUR  
  
 SQL_INTERVAL_MINUTE_TO_SECOND  
  
 The following table shows the ODBC C data types to which day-time interval SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  
  
|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|All day-time C interval types|Trailing fields portion not truncated<br /><br /> Trailing fields portion truncated<br /><br /> Leading precision of target is not big enough to hold data from source|Data<br /><br /> Truncated data<br /><br /> Undefined|Length of data<br /><br /> Length of data<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22015|  
|SQL_C_STINYINT[b] SQL_C_UTINYINT[b] SQL_C_USHORT[b] SQL_C_SHORT[b] SQL_C_SLONG[b] SQL_C_ULONG[b] SQL_C_NUMERIC[b] SQL_C_BIGINT[b]|Interval precision was a single field and the data was converted without truncation<br /><br /> Interval precision was a single field and truncated fractional<br /><br /> Interval precision was a single field and truncated whole<br /><br /> Interval precision was not a single field|Data<br /><br /> Truncated  data<br /><br /> Truncated  data<br /><br /> Undefined|Size of the C data type<br /><br /> Length of data<br /><br /> Length of data<br /><br /> Size of the C data type|n/a<br /><br /> 01S07<br /><br /> 22003<br /><br /> 07006|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_CHAR|Character byte length < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits >= *BufferLength*|Data<br /><br /> Truncated data<br /><br /> Undefined|Size of the C data type<br /><br /> Size of the C data type<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
_C_WCHAR|Character length < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits >= *BufferLength*|Data<br /><br /> Truncated data<br /><br /> Undefined|Size of the C data type<br /><br /> Size of the C data type<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
  
 [a]   A day-time interval SQL type can be converted to any day-time interval C type.  
  
 [b]   If the interval precision is a single field (one of DAY, HOUR, MINUTE, or SECOND), the interval SQL type can be converted to any exact numeric (SQL_C_STINYINT, SQL_C_UTINYINT, SQL_C_USHORT, SQL_C_SHORT, SQL_C_SLONG, SQL_C_ULONG, or SQL_C_NUMERIC).  
  
 The default conversion of an interval SQL type is to the corresponding C interval data type. The application then binds the column or parameter (or sets the SQL_DESC_DATA_PTR field in the appropriate record of the ARD) to point to the initialized SQL_INTERVAL_STRUCT structure (or passes a pointer to the SQL_ INTERVAL_STRUCT structure as the *TargetValuePtr* argument in a call to **SQLGetData**).  
  
 The following example demonstrates how to transfer data from a column of type SQL_INTERVAL_DAY_TO_MINUTE into the SQL_INTERVAL_STRUCT structure such that it comes back as a DAY_TO_HOUR interval.  
  
```  
SQL_INTERVAL_STRUCT is;  
SQLINTEGER    cbValue;  
SQLUINTEGER   days, hours;  
  
// Execute a select statement; "interval_column" is a column  
// whose data type is SQL_INTERVAL_DAY_TO_MINUTE.  
SQLExecDirect(hstmt, "SELECT interval_column FROM table", SQL_NTS);  
  
// Bind  
SQLBindCol(hstmt, 1, SQL_C_INTERVAL_DAY_TO_MINUTE, &is, sizeof(SQL_INTERVAL_STRUCT), &cbValue);  
  
// Fetch  
SQLFetch(hstmt);  
  
// Process data  
days = is.intval.day_second.day;  
hours = is.intval.day_second.hour;  
```
