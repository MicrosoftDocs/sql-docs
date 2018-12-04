---
title: "Conversions from C to SQL | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "conversions [ODBC], C to SQL"
ms.assetid: 7ac098db-9147-4883-8da9-a58ab24a0d31
author: MightyPen
ms.author: genemi
manager: craigg
---
# Conversions from C to SQL
  This topic lists issues to consider when you convert from C types to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date/time types.  
  
 The conversions described in the following table apply to conversions made on the client. In cases where the client specifies fractional second precision for a parameter that differs from that defined on the server, the client conversion might succeed but the server will return an error when `SQLExecute` or `SQLExecuteDirect` is called. In particular, ODBC treats any truncation of fractional seconds as an error, whereas the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] behavior is to round; for example, rounding occurs when you go from `datetime2(6)` to `datetime2(2)`. Datetime column values are rounded to 1/300th of a second and smalldatetime columns have seconds set to zero by the server.  
  
|||||||||  
|-|-|-|-|-|-|-|-|  
||SQL_TYPE_DATE|SQL_TYPE_TIME|SQL_SS_TIME2|SQL_TYPE_TIMESTAMP|SQL_SS_TIMSTAMPOFFSET|SQL_CHAR|SQL_WCHAR|  
|SQL_C_DATE|1|-|-|1,6|1,5,6|1,13|1,13|  
|SQL_C_TIME|-|1|1|1,7|1,5,7|1,13|1,13|  
|SQL_C_SS_TIME2|-|1,3|1,10|1,7|1,5,7|1,13|1,13|  
|SQL_C_BINARY(SQL_SS_TIME2_STRUCT)|N/A|N/A|1,10,11|N/A|N/A|N/A|N/A|  
|SQL_C_TYPE_TIMESTAMP|1,2|1,3,4|1,4,10|1,10|1,5,10|1,13|1,13|  
|SQL_C_SS_TIMESTAMPOFFSET|1,2,8|1,3,4,8|1,4,8,10|1,8,10|1,10|1,13|1,13|  
|SQL_C_BINARY(SQL_SS_TIMESTAMPOFFSET_STRUCT)|N/A|N/A|N/A|N/A|1,10,11|N/A|N/A|  
|SQL_C_CHAR/SQL_WCHAR (date)|9|9|9|9,6|9,5,6|N/A|N/A|  
|SQL_C_CHAR/SQL_WCHAR (time2)|9|9,3|9,10|9,7,10|9,5,7,10|N/A|N/A|  
|SQL_C_CHAR/SQL_WCHAR (datetime)|9,2|9,3,4|9,4,10|9,10|9,5,10|N/A|N/A|  
|SQL_C_CHAR/SQL_WCHAR (datetimeoffset)|9,2,8|9,3,4,8|9,4,8,10|9,8,10|9,10|N/A|N/A|  
|SQL_C_BINARY(SQL_DATE_STRUCT)|1,11|N/A|N/A|N/A|N/A|N/A|N/A|  
|SQL_C_BINARY(SQL_TIME_STRUCT)|N/A|N/A|N/A|N/A|N/A|N/A|N/A|  
|SQL_C_BINARY(SQL_TIMESTAMP_STRUCT)|N/A|N/A|N/A|N/A|N/A|N/A|N/A|  
  
## Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|-|No conversion is supported. A diagnostic record is generated with SQLSTATE 07006 and the message "Restricted data type attribute violation".|  
|1|If the data supplied is not valid, a diagnostic record is generated with SQLSTATE 22007 and the message "Invalid datetime format".|  
|2|Time fields must be zero or a diagnostic record is generated with SQLSTATE 22008 and the message "Fractional truncation".|  
|3|Fractional seconds must be zero or a diagnostic record is generated with SQLSTATE 22008 and the message "Fractional truncation".|  
|4|The date component is ignored.|  
|5|The timezone is set to the client's timezone setting.|  
|6|The time is set to zero.|  
|7|The date is set to the current date.|  
|8|The time is converted from the client's timezone to UTC. If an error occurs during this conversion, a diagnostic record is generated with SQLSTATE 22008 and the message "Datetime field overflow".|  
|9|The string is parsed and converted to a date, datetime, datetimeoffset, or time value, depending on the first punctuation character encountered and the presence of remaining components. The string is then converted to the target type, following the rules in the preceding table for the source type discovered by this process. If an error is detected while parsing the data, a diagnostic record is generated with SQLSTATE 22018 and the message "Invalid character value for cast specification". For datetime and smalldatetime parameters, if the year is outside the range supported by these types, a diagnostic record is generated with SQLSTATE 22007 and the message "Invalid datetime format".<br /><br /> For datetimeoffset, the value must be within range after conversion to UTC, even if no conversion to UTC is requested. This is because TDS and the server always normalize the time in datetimeoffset values for UTC, so the client must verify that time components are within the range supported after conversion to UTC. If the value is not within the supported UTC range, a diagnostic record is generated with SQLSTATE 22007 and the message "Invalid datetime format".|  
|10|If truncation with data loss occurs, a diagnostic record is generated with SQLSTATE 22008 and the message "Invalid time format". This error also occurs if the value falls outside the range that can be represented by the UTC range used by the server.|  
|11|If the byte length of the data does not equal the size of the struct required by the SQL type, a diagnostic record is generated with SQLSTATE 22003 and the message "Numeric value out of range".|  
|12|If the byte length of the data is 4 or 8, the data is sent to the server in raw TDS smalldatetime or datetime format. If the byte length of the data exactly matches the size of SQL_TIMESTAMP_STRUCT, the data is converted to the TDS format for datetime2.|  
|13|If truncation with data loss occurs, a diagnostic record is generated with SQLSTATE 22001 and the message "String data, right truncated".<br /><br /> The number of fractional seconds digits (the scale) is determined from the destination column's size according to the following:<br /><br /> **Type:** SQL_C_TYPE_TIMESTAMP<br /><br /> Implied Scale<br /><br /> 0<br /><br /> 19<br /><br /> Implied Scale<br /><br /> 1..9<br /><br /> 21..29<br /><br /> However, for SQL_C_TYPE_TIMESTAMP, if the fractional seconds can be represented with three digits without data loss and the column size is 23 or larger, then exactly three fractional seconds' digits are generated. This behavior ensures backwards compatibility for applications developed using older ODBC drivers.<br /><br /> For column sizes larger than the range in the table, a scale of 9 is implied. This conversion should allow for up to nine fractional seconds digits, the maximum allowed by ODBC.<br /><br /> A column size of zero implies unlimited size for variable length character types in ODBC (9 digits, unless the 3-digit rule for SQL_C_TYPE_TIMESTAMP applies). Specifying a column size of zero with a fixed length character type is an error.|  
|N/A|Existing [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and earlier behavior is maintained.|  
  
## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](date-and-time-improvements-odbc.md)  
  
  
