---
title: "Datetime Data Type Changes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "time data type [ODBC]"
  - "datetime data types [ODBC]"
  - "date data type [ODBC]"
  - "backward compatibility [ODBC], datetime data types"
  - "timestamp data type [ODBC]"
  - "compatibility [ODBC], datetime data types"
ms.assetid: c38c79f9-8bb0-4633-ac86-542366c09a95
author: MightyPen
ms.author: genemi
manager: craigg
---
# Datetime Data Type Changes
In ODBC 3.*x*, the identifiers for date, time, and timestamp SQL data types have changed from SQL_DATE, SQL_TIME, and SQL_TIMESTAMP (with instances of **#define** in the header file of 9, 10, and 11) to SQL_TYPE_DATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP (with instances of **#define** in the header file of 91, 92, and 93), respectively. The corresponding C type identifiers have changed from SQL_C_DATE, SQL_C_TIME, and SQL_C_TIMESTAMP to SQL_C_TYPE_DATE, SQL_C_TYPE_TIME, and SQL_C_TYPE_TIMESTAMP, respectively.  
  
 The column size and decimal digits returned for the SQL datetime data types in ODBC 3.*x* are the same as the precision and scale returned for them in ODBC 2.*x*. These values are different than the values in the SQL_DESC_PRECISION and SQL_DESC_SCALE descriptor fields. (For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md).)  
  
 These changes affect **SQLDescribeCol**, **SQLDescribeParam**, and **SQLColAttribute**; **SQLBindCol**, **SQLBindParameter**, and **SQLGetData**; and **SQLColumns**, **SQLGetTypeInfo**, **SQLProcedureColumns**, **SQLStatistics**, and **SQLSpecialColumns**.  
  
 The following table shows how the ODBC 3*.x* Driver Manager performs mapping of the date, time, and timestamp C data types entered in the *TargetType* arguments of **SQLBindCol** and **SQLGetData** or in the *ValueType* argument of **SQLBindParameter**.  
  
|Data type<br /><br /> code entered|2.*x* app to<br /><br /> 2.*x* driver|2.*x* app to<br /><br /> 3.*x* driver|3.*x* app to<br /><br /> 2.*x* driver|3.*x* app to<br /><br /> 3.*x* driver|  
|--------------------------------|-----------------------------------|-----------------------------------|-----------------------------------|-----------------------------------|  
|SQL_C_DATE (9)|No mapping|SQL_C_TYPE_DATE (91)|No mapping[1]|SQL_C_TYPE_DATE (91)|  
|SQL_C_TYPE_DATE (91)|Error (from DM)|Error (from DM)|SQL_C_DATE (9)|No mapping[2]|  
|SQL_C_TIME (10)|No mapping|SQL_C_TYPE_TIME (92)|No mapping[1]|SQL_C_TYPE_TIME (92)|  
|SQL_C_TYPE_TIME (92)|Error (from DM)|Error (from DM)|SQL_C_TIME (10)|No mapping[2]|  
|SQL_C_TIMESTAMP (11)|No mapping|SQL_C_TYPE_TIMESTAMP (93)|No mapping[1]|SQL_C_TYPE_TIMESTAMP (93)|  
|SQL_C_TYPE_TIMESTAMP (93)|Error (from DM)|Error (from DM)|SQL_C_TIMESTAMP (11)|No mapping[2]|  
  
 [1]   As a result of this, an ODBC 3.*x* application working with an ODBC 2.*x* driver can use the date, time, or timestamp codes returned in the result sets that are returned by the catalog functions.  
  
 [2]   As a result of this, an ODBC 3.*x* application working with an ODBC 3.*x* driver can use the date, time, or timestamp codes returned in the result sets that are returned by the catalog functions.  
  
 The following table shows how the ODBC 3*.x* Driver Manager performs mapping of the date, time, and timestamp SQL data types entered in the *ParameterType* argument of **SQLBindParameter** or in the *DataType* argument of **SQLGetTypeInfo**.  
  
|Data type<br /><br /> code entered|2.*x* app to<br /><br /> 2.*x* driver|2.*x* app to<br /><br /> 3.*x* driver|3.*x* app to<br /><br /> 2.*x* driver|3.*x* app to<br /><br /> 3.*x* driver|  
|--------------------------------|-----------------------------------|-----------------------------------|-----------------------------------|-----------------------------------|  
|SQL_DATE (9)|No mapping|SQL_TYPE_DATE (91)|No mapping[1]|SQL_TYPE_DATE (91)|  
|SQL_TYPE_DATE (91)|Error (from DM)|Error (from DM)|SQL_DATE (9)|No mapping[2]|  
|SQL_TIME (10)|No mapping|SQL_TYPE_TIME (92)|No mapping[1]|SQL_TYPE_TIME (92)|  
|SQL_TYPE_TIME (92)|Error (from DM)|Error (from DM)|SQL_TIME (10)|No mapping[2]|  
|SQL_TIMESTAMP (11)|No mapping|SQL_TYPE_TIMESTAMP (93)|No mapping[1]|SQL_TYPE_TIMESTAMP (93)|  
|SQL_TYPE_TIMESTAMP (93)|Error (from DM)|Error (from DM)|SQL_TIMESTAMP (11)|No mapping[2]|  
  
 [1]   As a result of this, an ODBC 3.*x* application working with an ODBC 2.*x* driver can use the date, time, or timestamp codes returned in the result sets that are returned by the catalog functions.  
  
 [2]   As a result of this, an ODBC 3.*x* application working with an ODBC 3.*x* driver can use the date, time, or timestamp codes returned in the result sets that are returned by the catalog functions.
