---
description: "Datetime Data Types"
title: "Datetime Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "time data type [ODBC]"
  - "datetime data types [ODBC]"
  - "date data type [ODBC]"
  - "data types [ODBC], date"
  - "backward compatibility [ODBC], datetime data types"
  - "timestamp data type [ODBC]"
  - "data types [ODBC], timestamp"
  - "data types [ODBC], backward compatibility"
  - "compatibility [ODBC], datetime data types"
  - "data types [ODBC], time"
ms.assetid: 6b9363c9-04bf-4492-a210-7aa15dea4af8
author: David-Engel
ms.author: v-davidengel
---
# Datetime Data Types
In ODBC *3.x*, the identifiers for date, time, and timestamp SQL data types have changed from SQL_DATE, SQL_TIME, and SQL_TIMESTAMP (with instances of **#define** in the header file of 9, 10, and 11) to SQL_TYPE_DATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP (with instances of **#define** in the header file of 91, 92, and 93), respectively. The corresponding C type identifiers have changed from SQL_C_DATE, SQL_C_TIME, and SQL_C_TIMESTAMP to SQL_C_TYPE_DATE, SQL_C_TYPE_TIME, and SQL_C_TYPE_TIMESTAMP, respectively, and the instances of **#define** have changed accordingly.  
  
 The column size and decimal digits returned for the SQL datetime data types in ODBC *3.x* are the same as the precision and scale returned for them in ODBC *2.x*. These values are different than the values in the SQL_DESC_PRECISION and SQL_DESC_SCALE descriptor fields. (For more information, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) in Appendix D: Data Types.)  
  
 These changes affect **SQLDescribeCol**, **SQLDescribeParam**, and **SQLColAttributes**; **SQLBindCol**, **SQLBindParameter**, and **SQLGetData**; and **SQLColumns**, **SQLGetTypeInfo**, **SQLProcedureColumns**, **SQLStatistics**, and **SQLSpecialColumns**.  
  
 An ODBC *3.x* driver processes the function calls listed in the previous paragraph according to the setting of the SQL_ATTR_ODBC_VERSION environment attribute. For **SQLColumns**, **SQLGetTypeInfo**, **SQLProcedureColumns**, **SQLSpecialColumns**, and **SQLStatistics**, if SQL_ATTR_ODBC_VERSION is set to SQL_OV_ODBC3, the functions return SQL_TYPE_DATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP in the DATA_TYPE field. The COLUMN_SIZE column (in the result set returned by **SQLColumns**, **SQLGetTypeInfo**, **SQLProcedureColumns**, and **SQLSpecialColumns**) contains the binary precision for the approximate numeric type. The NUM_PREC_RADIX column (in the result set returned by **SQLColumns**, **SQLGetTypeInfo**, and **SQLProcedureColumns**) contains a value of 2. If SQL_ATTR_ODBC_VERSION is set to SQL_OV_ODBC2, then the functions return SQL_DATE, SQL_TIME, and SQL_TIMESTAMP in the DATA_TYPE field, the COLUMN_SIZE column contains the decimal precision for the approximate numeric type, and the NUM_PREC_RADIX column contains a value of 10.  
  
 When all data types are requested in a call to **SQLGetTypeInfo**, the result set returned by the function will contain both SQL_TYPE_DATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP as defined in ODBC *3.x*, and SQL_DATE, SQL_TIME, and SQL_TIMESTAMP as defined in ODBC *2.x*.  
  
 Because of how the ODBC *3.x* Driver Manager performs mapping of the date, time, and timestamp data types, ODBC *3.x* drivers need only recognize **#defines** of 91, 92, and 93 for the date, time, and timestamp C data types entered in the *TargetType* arguments of **SQLBindCol** and **SQLGetData** or the *ValueType* argument of **SQLBindParameter**, and need only recognize **#defines** of 91, 92, and 93 for the date, time, and timestamp SQL data types entered in the *ParameterType* argument of **SQLBindParameter** or the *DataType* argument of **SQLGetTypeInfo**. For more information, see [Datetime Data Type Changes](../../../odbc/reference/develop-app/datetime-data-type-changes.md).
