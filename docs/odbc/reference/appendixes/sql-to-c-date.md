---
title: "SQL to C: Date"
description: "SQL to C: Date"
author: David-Engel
ms.author: davidengel
ms.date: 01/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "converting data from SQL to C types [ODBC], date"
  - "date data type [ODBC]"
  - "data conversions from SQL to C types [ODBC], date"
---
# SQL to C: Date

The identifier for the date ODBC SQL data type is:

SQL_TYPE_DATE
  
 The following table shows the ODBC C data types to which the driver can convert date SQL data. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).

> [!NOTE]
> For character conversions, *BufferLength* must include space for the null terminator. A date string is 10 characters long (yyyy-mm-dd), so *BufferLength* must be at least 11 bytes to avoid truncation.

|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR|*BufferLength* > Character byte length<br /><br /> 11 <= *BufferLength* <= Character byte length<br /><br /> *BufferLength* < 11|Data<br /><br /> Truncated data<br /><br /> Undefined|10<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_WCHAR|*BufferLength* > Character length<br /><br /> 11 <= *BufferLength* <= Character length<br /><br /> *BufferLength* < 11|Data<br /><br /> Truncated data<br /><br /> Undefined|10<br /><br /> Length of data in characters<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_TYPE_DATE|None<sup>1</sup>|Data|6<sup>3</sup>|n/a|
|SQL_C_TYPE_TIMESTAMP|None<sup>1</sup>|Data<sup>2</sup>|16<sup>3</sup>|n/a|

<sup>1</sup> The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of **TargetValuePtr* is the size of the C data type.

<sup>2</sup> The driver sets the time fields of the timestamp structure to zero.

<sup>3</sup> This is the size of the corresponding C data type.
  
 When the driver converts date SQL data to character C data, the resulting string is in the "*yyyy*-*mm*-*dd*" format. This format isn't affected by the Windows country/region setting.

## Related content

- [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md)
- [SQL to C: Timestamp](../../../odbc/reference/appendixes/sql-to-c-timestamp.md)
- [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md)