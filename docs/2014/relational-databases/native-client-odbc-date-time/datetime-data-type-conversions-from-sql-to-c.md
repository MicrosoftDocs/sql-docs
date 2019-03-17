---
title: "Conversions from SQL to C | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "conversions [ODBC], SQL to C"
ms.assetid: 059431e2-a65c-4587-ba4a-9929a1611e96
author: MightyPen
ms.author: genemi
manager: craigg
---
# Conversions from SQL to C
  The following table lists issues to consider when you convert from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date/time types to C types.  
  
## Conversions  
  
||||||||||  
|-|-|-|-|-|-|-|-|-|  
||SQL_C_DATE|SQL_C_TIME|SQL_C_TIMESTAMP|SQL_C_SS_TIME2|SQL_C_SS_TIMESTAMPOFFSET|SQL_C_BINARY|SQL_C_CHAR|SQL_C_WCHAR|  
|SQL_CHAR|2,3,4,5|2,3,6,7,8|2,3,9,10,11|2,3,6,7|2,3,9,10,11|1|1|1|  
|SQL_WCHAR|2,3,4,5|2,3,6,7,8|2,3,9,10,11|2,3,6,7|2,3,9,10,11|1|1|1|  
|SQL_TYPE_DATE|OK|12|13|12|13,23|14|16|16|  
|SQL_SS_TIME2|12|8|15|OK|10,23|17|16|16|  
|SQL_TYPE_TIMESTAMP|18|7,8|OK|7|23|19|16|16|  
|SQL_SS_TIMESTAMPOFFSET|18,22|7,8,20|20|7,20|OK|21|16|16|  
  
## Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|OK|No conversion issues.|  
|1|Rules prior to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] apply.|  
|2|Leading and trailing spaces are ignored.|  
|3|The string is parsed into a date, time, timezone, or timezoneoffset, and allows up to 9 digits for fractional seconds. If a timezoneoffset is parsed, the time is converted to the client timezone. If an error occurs during this conversion, a diagnostic record is generated with SQLSTATE 22018 and the message "Datetime field overflow".|  
|4|If the value is not a valid date, timestamp, or timestampoffset value, a diagnostic record is generated with SQLSTATE 22018 and the message "Invalid character value for cast specification".|  
|5|If the time is non-zero, a diagnostic record is generated with SQLSTATE 01S07 and the message "Fractional truncation".|  
|6|If the value is not a valid time, timestamp, or timestampoffset value, a diagnostic record is generated with SQLSTATE 22018 and the message "Invalid character value for cast specification".|  
|7|The date component is ignored.|  
|8|If the fractional seconds are non-zero, a diagnostic record is generated with SQLSTATE 01S07 and the message "Fractional truncation".|  
|9|If the value is not a valid date, time, timestamp, or timestampoffset value, a diagnostic record is generated with SQLSTATE 22018 and the message "Invalid character value for cast specification".|  
|10|If the value is a valid time, the date component is set to current client date.|  
|11|If the value is a valid date, the time is set to zero.|  
|12|A diagnostic record is generated with SQLSTATE 07006 and the message "Restricted data type attribute violation".|  
|13|The time is set to zero.|  
|14|If the buffer is not large enough to accommodate a SQL_DATE_STRUCT, a diagnostic record is generated with SQLSTATE 22003 and the message "Numeric value out of range".|  
|15|The date is set to the current client date.|  
|16|If the buffer is not large enough to accommodate the converted string value but only fractional seconds, truncation occurs and a diagnostic record is generated with SQLSTATE 01004 and the message "String data, right truncated". If the buffer is not large enough to accommodate the string value without truncation of date, time, or offset components, a diagnostic record is generated with SQLSTATE 22003 and the message "Numeric value out of range". Note that for date and timestampoffset, SQLSTATE 01004 is not possible because the rightmost part of the converted string does not contain fractional seconds. Therefore, any truncation causes data loss.|  
|17|If the buffer is not large enough to accommodate a SQL_SS_TIME2_STRUCT, a diagnostic record is generated with SQLSTATE 22003 and the message "Numeric value out of range".|  
|18|If the time is non-zero, a diagnostic record is generated with SQLSTATE 01S07 and the message "Fractional truncation".|  
|19|If the server type is datetime or smalldatetime, the value corresponds to the TDS wire format and will be a 4-byte value for smalldatetime and an 8-byte value for datetime.<br /><br /> If the server type is datetime2, the value is returned as a SQL_TIMESTAMP_STRUCT. If the buffer is not large enough to accommodate the returned value, a diagnostic record is generated with SQLSTATE 22003 and the message "Numeric value out of range".|  
|20|The time is converted to the client timezone. If an error occurs during this conversion, a diagnostic record is generated with SQLSTATE 22008 and the message "Datetime field overflow".|  
|21|If the buffer is large enough to accommodate a SQL_SS_TIMESTAMPOFFSET_STRUCT, the value is returned as a SQL_SS_TIMESTAMPOFFSET_STRUCT. Otherwise, a diagnostic record is generated with SQLSTATE 22003 and the message "Numeric value out of range".|  
|22|The value is converted to the client timezone before the date is extracted. This provides consistency with other conversions with timestampoffset types. If an error occurs during this conversion, a diagnostic record is generated with SQLSTATE 22008 and the message "Datetime field overflow". This might result in a date that differs from the value obtained by simple truncation.|  
  
 The table in this topic describes conversions between the type returned to the client and the type in the binding. For output parameters, if the server type specified in SQLBindParameter does not match the actual type on the server, an implicit conversion will be performed by the server and the type returned to the client will match the type specified through SQLBindParameter. This can lead to unexpected conversion results when the server's conversion rules are different from those listed in the preceding table. For example, when a default date must be provided, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses 1900-1-1, rather than the current date.  
  
## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](date-and-time-improvements-odbc.md)  
  
  
