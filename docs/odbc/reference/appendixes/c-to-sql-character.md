---
title: "C to SQL: Character | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "character data type [ODBC]"
  - "data conversions from C to SQL types [ODBC], character"
  - "converting data from c to SQL types [ODBC], character"
ms.assetid: be66188a-ebdb-4c9e-af72-c379886766fa
author: MightyPen
ms.author: genemi
manager: craigg
---
# C to SQL: Character
The identifiers for the character ODBC C data type are:  
  
 SQL_C_CHAR  
  
 SQL_C_WCHAR  
  
 The following table shows the ODBC SQL data types to which C character data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
> [!NOTE]  
>  When character C data is converted to Unicode SQL data, the length of the Unicode data must be an even number.  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR<br /><br /> SQL_VARCHAR<br /><br /> SQL_LONGVARCHAR|Byte length of data <= Column length.<br /><br /> Byte length of data > Column length.|n/a<br /><br /> 22001|  
|SQL_WCHAR<br /><br /> SQL_WVARCHAR<br /><br /> SQL_WLONGVARCHAR|Character length of data <= Column length.<br /><br /> Character length of data > Column length.|n/a<br /><br /> 22001|  
|SQL_DECIMAL<br /><br /> SQL_NUMERIC<br /><br /> SQL_TINYINT<br /><br /> SQL_SMALLINT<br /><br /> SQL_INTEGER SQL_BIGINT|Data converted without truncation<br /><br /> Data converted with truncation of fractional digits[e]<br /><br /> Conversion of data would result in loss of whole (as opposed to fractional) digits[e]<br /><br /> Data value is not a *numeric-literal*|n/a<br /><br /> 22001<br /><br /> 22001<br /><br /> 22018|  
|SQL_REAL<br /><br /> SQL_FLOAT<br /><br /> SQL_DOUBLE|Data is within the range of the data type to which the number is being converted<br /><br /> Data is outside the range of the data type to which the number is being converted<br /><br /> Data value is not a *numeric-literal*|n/a<br /><br /> 22003<br /><br /> 22018|  
|SQL_BIT|Data is 0 or 1<br /><br /> Data is greater than 0, less than 2, and not equal to 1<br /><br /> Data is less than 0 or greater than or equal to 2<br /><br /> Data is not a *numeric-literal*|n/a<br /><br /> 22001<br /><br /> 22003<br /><br /> 22018|  
|SQL_BINARY<br /><br /> SQL_VARBINARY<br /><br /> SQL_LONGVARBINARY|(Byte length of data) / 2 <= column byte length<br /><br /> (Byte length of data) / 2 > column byte length<br /><br /> Data value is not a hexadecimal value|n/a<br /><br /> 22001<br /><br /> 22018|  
|SQL_TYPE_DATE|Data value is a valid *ODBC-date-literal*<br /><br /> Data value is a valid *ODBC-timestamp-literal*; time portion is zero<br /><br /> Data value is a valid *ODBC-timestamp-literal*; time portion is nonzero[a]<br /><br /> Data value is not a valid *ODBC-date-literal* or *ODBC-timestamp-literal*|n/a<br /><br /> n/a<br /><br /> 22008<br /><br /> 22018|  
|SQL_TYPE_TIME|Data value is a valid *ODBC-time-literal*<br /><br /> Data value is a valid *ODBC-timestamp-literal*; fractional seconds portion is zero[b]<br /><br /> Data value is a valid *ODBC-timestamp-literal*; fractional seconds portion is nonzero[b]<br /><br /> Data value is not a valid *ODBC-time-literal* or *ODBC-timestamp-literal*|n/a<br /><br /> n/a<br /><br /> 22008<br /><br /> 22018|  
|SQL_TYPE_TIMESTAMP|Data value is a valid *ODBC-timestamp-literal*; fractional seconds portion not truncated<br /><br /> Data value is a valid *ODBC-timestamp-literal*; fractional seconds portion truncated<br /><br /> Data value is a valid *ODBC-date-literal*[c]<br /><br /> Data value is a valid *ODBC-time-literal*[d]<br /><br /> Data value is not a valid *ODBC-date-literal*, *ODBC-time-literal*, or *ODBC-timestamp-literal*|n/a<br /><br /> 22008<br /><br /> n/a<br /><br /> n/a<br /><br /> 22018|  
|All SQL interval types|Data value is a valid *interval value*; no truncation occurs<br /><br /> Data value is a valid *interval value*; the value in one of the fields is truncated<br /><br /> The data value is not a valid interval literal|n/a<br /><br /> 22015<br /><br /> 22018|  
  
 [a]   The time portion of the timestamp is truncated.  
  
 [b]   The date portion of the timestamp is ignored.  
  
 [c]   The time portion of the timestamp is set to zero.  
  
 [d]   The date portion of the timestamp is set to the current date.  
  
 [e]   The driver/data source effectively waits until the entire string has been received (even if the character data is sent in pieces by calls to **SQLPutData**) before attempting to perform the conversion.  
  
 When character C data is converted to numeric, date, time, or timestamp SQL data, leading and trailing blanks are ignored.  
  
 When character C data is converted to binary SQL data, each two bytes of character data are converted to a single byte (8 bits) of binary data. Each two bytes of character data represent a number in hexadecimal form. For example, "01" is converted to a binary 00000001 and "FF" is converted to a binary 11111111.  
  
 The driver always converts pairs of hexadecimal digits to individual bytes and ignores the null-termination byte. Because of this, if the length of the character string is odd, the last byte of the string (excluding the null-termination byte, if any) is not converted.  
  
> [!NOTE]  
>  Application developers are discouraged from binding character C data to a binary SQL data type. This conversion is usually inefficient and slow.
