---
title: "SQL to C: Year-Month Intervals | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "converting data from SQL to c types [ODBC], about converting"
  - "data conversions from SQL to C types [ODBC], year-month intervals"
  - "intervals [ODBC], converting"
  - "year-month intervals [ODBC]"
ms.assetid: 1233634b-8214-420f-b872-3b2630105ba4
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C: Year-Month Intervals

The identifiers for the year-month interval ODBC SQL data types are the following:

- SQL_INTERVAL_MONTH
- SQL_INTERVAL_YEAR
- SQL_INTERVAL_YEAR_TO_MONTH

The following table shows the ODBC C data types to which year-month interval SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  

|C type identifier|Test|TargetValuePtr|StrLen_or_IndPtr|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_INTERVAL_MONTH[a]<br /><br /> SQL_C_INTERVAL_YEAR[a]<br /><br /> SQL_C_INTERVAL_YEAR_TO_MONTH[a]|Trailing fields portion not truncated<br /><br /> Trailing fields portion truncated<br /><br /> Leading precision of target is not big enough to hold data from source|Data<br /><br /> Truncated data<br /><br /> Undefined|Length of data in bytes<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22015|  
|SQL_C_STINYINT[b]<br /><br /> SQL_C_UTINYINT[b]<br /><br /> SQL_C_USHORT[b]<br /><br /> SQL_C_SHORT[b]<br /><br /> SQL_C_SLONG[b]<br /><br /> SQL_C_ULONG[b]<br /><br /> SQL_C_NUMERIC[b]<br /><br /> SQL_C_BIGINT[b]|Interval precision was a single field and the data was converted without truncation<br /><br /> Interval precision was a single field and truncated whole<br /><br /> Interval precision was not a single field|Data<br /><br /> Truncated  data<br /><br /> Undefined|Size of the C data type<br /><br /> Length of data in bytes<br /><br /> Size of the C data type|n/a<br /><br /> 22003<br /><br /> 22015|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_CHAR|Character byte length < *BufferLength*<br /><br /> Number of whole  (as opposed to fractional) digits < *BufferLength*<br /><br /> Number of whole  (as opposed to fractional) digits >= *BufferLength*|Data<br /><br /> Truncated data<br /><br /> Undefined|Size of the C data type<br /><br /> Size of the C data type<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_WCHAR|Character length < *BufferLength*<br /><br /> Number of whole  (as opposed to fractional) digits < *BufferLength*<br /><br /> Number of whole  (as opposed to fractional) digits >= *BufferLength*|Data<br /><br /> Truncated data<br /><br /> Undefined|Size of the C data type<br /><br /> Size of the C data type<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
  
 [a]   A year-month interval SQL type can be converted to any year-month interval C type.  
  
 [b]   If the interval precision is a single field (one of YEAR or MONTH), the interval SQL type can be converted to any exact numeric (SQL_C_STINYINT, SQL_C_UTINYINT, SQL_C_USHORT, SQL_C_SHORT, SQL_C_SLONG, SQL_C_ULONG, or SQL_C_NUMERIC).  

## Default conversions

The default conversion of an interval SQL type is to the corresponding C interval data type. The application then binds the column or parameter (or sets the SQL_DESC_DATA_PTR field in the appropriate record of the ARD) to point to the initialized SQL_INTERVAL_STRUCT structure (or passes a pointer to the SQL_ INTERVAL_STRUCT structure as the *TargetValuePtr* argument in a call to **SQLGetData**).
