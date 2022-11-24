---
description: "C to SQL: Numeric"
title: "C to SQL: Numeric | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "numeric data type [ODBC], converting"
  - "data conversions from C to SQL types [ODBC], numeric"
  - "converting data from c to SQL types [ODBC], numeric"
ms.assetid: af4095ff-06c3-4b04-83bf-19f9ee098dc2
author: David-Engel
ms.author: v-davidengel
---
# C to SQL: Numeric
The identifiers for the numeric ODBC C data types are:  
  
 SQL_C_STINYINT  
  
 SQL_C_SLONG  
  
 SQL_C_UTINYINT  
  
 SQL_C_ULONG  
  
 SQL_C_TINYINT  
  
 SQL_C_LONG  
  
 SQL_C_SSHORT  
  
 SQL_C_FLOAT  
  
 SQL_C_USHORT  
  
 SQL_C_DOUBLE  
  
 SQL_C_SHORT  
  
 SQL_C_NUMERIC  
  
 SQL_C_SBIGINT  
  
 SQL_C_UBIGINT  
  
 The following table shows the ODBC SQL data types to which numeric C data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR<br /><br /> SQL_VARCHAR<br /><br /> SQL_LONGVARCHAR|Number of digits <= Column byte length<br /><br /> Number of digits > Column byte length|n/a<br /><br /> 22001|  
|SQL_WCHAR<br /><br /> SQL_WVARCHAR<br /><br /> SQL_WLONGVARCHAR|Number of characters <= Column character length<br /><br /> Number of characters > Column character length|n/a<br /><br /> 22001|  
|SQL_DECIMAL[b]<br /><br /> SQL_NUMERIC[b]<br /><br /> SQL_TINYINT[b]<br /><br /> SQL_SMALLINT[b]<br /><br /> SQL_INTEGER[b]<br /><br /> SQL_BIGINT[b]|Data converted without truncation or with truncated of fractional digits<br /><br /> Data converted with truncation of whole digits|n/a<br /><br /> 22003|  
|SQL_REAL<br /><br /> SQL_FLOAT<br /><br /> SQL_DOUBLE|Data is within the range of the data type to which the number is being converted<br /><br /> Data is outside the range of the data type to which the number is being converted|n/a<br /><br /> 22003|  
|SQL_BIT|Data is 0 or 1<br /><br /> Data is greater than 0, less than 2, and not equal to 1<br /><br /> Data is less than 0 or greater than or equal to 2|n/a<br /><br /> 22001<br /><br /> 22003|  
|SQL_INTERVAL_YEAR[a]<br /><br /> SQL_INTERVAL_MONTH[a]<br /><br /> SQL_INTERVAL_DAY[a]<br /><br /> SQL_INTERVAL_HOUR[a]<br /><br /> SQL_INTERVAL_MINUTE[a]<br /><br /> SQL_INTERVAL_SECOND[a]|Data not truncated.<br /><br /> Data truncated.|n/a<br /><br /> 22015|  
  
 [a]   These conversions are supported only for the exact numeric data types (SQL_C_STINYINT, SQL_C_UTINYINT, SQL_C_SSHORT, SQL_C_USHORT, SQL_C_SLONG, SQL_C_ULONG, or SQL_C_NUMERIC). They are not supported for the approximate numeric data types (SQL_C_FLOAT or SQL_C_DOUBLE). Exact numeric C data types cannot be converted to an interval SQL type whose interval precision is not a single field.  
  
 [b]   For the "n/a" case, a driver may optionally return SQL_SUCCESS_WITH_INFO and 01S07 when there is a fractional truncation.  
  
 The driver ignores the length/indicator value when converting data from the numeric C data types and assumes that the size of the data buffer is the size of the numeric C data type. The length/indicator value is passed in the *StrLen_or_Ind* argument in **SQLPutData** and in the buffer specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**. The data buffer is specified with the *DataPtr* argument in **SQLPutData** and the *ParameterValuePtr* argument in **SQLBindParameter**.
