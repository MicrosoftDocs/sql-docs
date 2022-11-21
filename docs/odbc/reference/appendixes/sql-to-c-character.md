---
description: "SQL to C: Character"
title: "SQL to C: Character | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "converting data from SQL to C types [ODBC], character"
  - "character data type [ODBC]"
  - "data conversions from SQL to C types [ODBC], character"
ms.assetid: 7fdb7f38-b64d-48f2-bcb4-1ca96b2bbdb6
author: David-Engel
ms.author: v-davidengel
---
# SQL to C: Character

The identifiers for the character ODBC SQL data types are the following:

- SQL_CHAR
- SQL_VARCHAR
- SQL_LONGVARCHAR
- SQL_WCHAR
- SQL_WVARCHAR
- SQL_WLONGVARCHAR

The following table shows the ODBC C data types to which character SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  

|C type identifier|Test|TargetValuePtr|StrLen_or_IndPtr|SQLSTATE|
|:----------------|:---|:-------------|:---------------|:-------|
|SQL_C_CHAR|Byte length of data < *BufferLength*<br /><br /> Byte length of data >= *BufferLength*|Data<br /><br /> Truncated data|Length of data in bytes<br /><br /> Length of data in bytes|n/a<br /><br /> 01004|  
|SQL_C_WCHAR|Character length of data < *BufferLength*<br /><br /> Character length of data >= *BufferLength*|Data<br /><br /> Truncated data|Length of data in characters<br /><br /> Length of data in characters|n/a<br /><br /> 01004|  
|SQL_C_STINYINT SQL_C_UTINYINT SQL_C_TINYINT  SQL_C_SBIGINT SQL_C_UBIGINT SQL_C_SSHORT SQL_C_USHORT SQL_C_SHORT  SQL_C_SLONG SQL_C_ULONG SQL_C_LONG  SQL_C_NUMERIC|Data converted without truncation[b]<br /><br /> Data converted with truncation of fractional digits[a]<br /><br /> Conversion of data would result in loss of whole (as opposed to fractional) digits[a]<br /><br /> Data is not a *numeric-literal*[b]|Data<br /><br /> Truncated data<br /><br /> Undefined<br /><br /> Undefined|Number of bytes of the C data type<br /><br /> Number of bytes of the C data type<br /><br /> Undefined<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22003<br /><br /> 22018|  
|SQL_C_FLOAT SQL_C_DOUBLE|Data is within the range of the data type to which the number is being converted[a]<br /><br /> Data is outside the range of the data type to which the number is being converted[a]<br /><br /> Data is not a *numeric-literal*[b]|Data<br /><br /> Undefined<br /><br /> Undefined|Size of the C data type<br /><br /> Undefined<br /><br /> Undefined|n/a<br /><br /> 22003<br /><br /> 22018|  
|SQL_C_BIT|Data is 0 or 1<br /><br /> Data is greater than 0, less than 2, and not equal to 1<br /><br /> Data is less than 0 or greater than or equal to 2<br /><br /> Data is not a *numeric-literal*|Data<br /><br /> Truncated data<br /><br /> Undefined<br /><br /> Undefined|1[b]<br /><br /> 1[b]<br /><br /> Undefined<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22003<br /><br /> 22018|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Truncated data|Length of data in bytes<br /><br /> Length of data|n/a<br /><br /> 01004|  
|SQL_C_TYPE_DATE|Data value is a valid *date-value*[a]<br /><br /> Data value is a valid *timestamp-value*; time portion is zero[a]<br /><br /> Data value is a valid *timestamp-value*; time portion is nonzero[a],[c]<br /><br /> Data value is not a valid *date-value* or *timestamp-value*[a]|Data<br /><br /> Data<br /><br /> Truncated data<br /><br /> Undefined|6[b]<br /><br /> 6[b]<br /><br /> 6[b]<br /><br /> Undefined|n/a<br /><br /> n/a<br /><br /> 01S07<br /><br /> 22018|  
|SQL_C_TYPE_TIME|Data value is a valid *time-value and the fractional seconds value is 0*[a]<br /><br /> Data value is a valid *timestamp-value or a valid time-value*; fractional seconds portion is zero[a],[d]<br /><br /> Data value is a valid *timestamp-value*; fractional seconds portion is nonzero[a],[d],[e]<br /><br /> Data value is not a valid *time-value* or *timestamp-value*[a]|Data<br /><br /> Data<br /><br /> Truncated data<br /><br /> Undefined|6[b]<br /><br /> 6[b]<br /><br /> 6[b]<br /><br /> Undefined|n/a<br /><br /> n/a<br /><br /> 01S07<br /><br /> 22018|  
|SQL_C_TYPE_TIMESTAMP|Data value is a valid *timestamp-value or a valid time-value*; fractional seconds portion not truncated[a]<br /><br /> Data value is a valid *timestamp-value or a valid time-value*; fractional seconds portion truncated[a]<br /><br /> Data value is a valid *date-value*[a]<br /><br /> Data value is a valid *time-value*[a]<br /><br /> Data value is not a valid *date-value*, *time-value*, or *timestamp-value*[a]|Data<br /><br /> Truncated data<br /><br /> Data[f]<br /><br /> Data[g]<br /><br /> Undefined|16[b]<br /><br /> 16[b]<br /><br /> 16[b]<br /><br /> 16[b]<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> n/a<br /><br /> n/a<br /><br /> 22018|  
|All C interval types|Data value is a valid *interval value*; no truncation<br /><br /> Data value is a valid *interval value*; truncation of one or more trailing fields<br /><br /> Data is valid interval; leading field significant precision is lost<br /><br /> The data value is not a valid interval value|Data<br /><br /> Truncated data<br /><br /> Undefined<br /><br /> Undefined|Length of data in bytes<br /><br /> Length of data in bytes<br /><br /> Undefined<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22015<br /><br /> 22018|  

 [a]   The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of  **TargetValuePtr* is the size of the C data type.  
  
 [b]   This is the size of the corresponding C data type.  
  
 [c]   The time portion of the *timestamp-value* is truncated.  
  
 [d]   The date portion of the *timestamp-value* is ignored.  
  
 [e]   The fractional seconds portion of the timestamp is truncated.  
  
 [f]   The time fields of the timestamp structure are set to zero.  
  
 [g]   The date fields of the timestamp structure are set to the current date.  

**Extra spaces**

Leading and trailing spaces are ignored when SQL character data is converted to any of the following types:

- date
- numeric
- time
- timestamp
- interval C data
