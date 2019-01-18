---
title: "Data Type Identifiers and Descriptors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], identifiers"
  - "identifiers [ODBC], data types"
  - "descriptors [ODBC], data types"
  - "verbose data types [ODBC]"
  - "data types [ODBC], descriptors"
  - "concise data types [ODBC]"
ms.assetid: f0077c9b-8eb2-4b5f-8c4c-7436fdef37ab
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Type Identifiers and Descriptors
The data types listed in the [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) and [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) sections earlier in this appendix are "concise" data types: Each identifier refers to a single data type. There is a one-to-one correspondence between the identifier and the data type. Descriptors, however, do not in all cases use a single value to identify data types. In some cases, they use a "verbose" data type and a type subcode. For all data types except datetime and interval data types, the verbose type identifier is the same as the concise type identifier and the value in SQL_DESC_DATETIME_INTERVAL_CODE is equal to 0. For datetime and interval data types, however, a verbose type (SQL_DATETIME or SQL_INTERVAL) is stored in SQL_DESC_TYPE, a concise type is stored in SQL_DESC_CONCISE_TYPE, and a subcode for each concise type is stored in SQL_DESC_DATETIME_INTERVAL_CODE. Setting one of these fields affects the others. For more information about these fields, see the [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md) function description.  
  
 When the SQL_DESC_TYPE or SQL_DESC_CONCISE_TYPE field is set for some data types, the SQL_DESC_DATETIME_INTERVAL_PRECISION, SQL_DESC_LENGTH, SQL_DESC_PRECISION, and SQL_DESC_SCALE fields are automatically set to default values, as applicable for the data type. For more information, see the description of the SQL_DESC_TYPE field in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md). If any of the default values set is not appropriate, the application should explicitly set the descriptor field through a call to **SQLSetDescField**.  
  
 The following table shows the concise type identifier, verbose type identifier, and type subcode for each datetime and interval SQL and C type identifier. As this table indicates, for datetime and interval data types, the SQL_DESC_TYPE and SQL_DESC_DATETIME_INTERVAL_CODE fields have the same manifest constants both for SQL data types (in implementation descriptors) and for C data types (in application descriptors).  
  
|Concise SQL type|Concise C type|Verbose type|DATETIME_INTERVAL_CODE|  
|----------------------|--------------------|------------------|------------------------------|  
|SQL_TYPE_DATE|SQL_C_TYPE_DATE|SQL_DATETIME|SQL_CODE_DATE|  
|SQL_TYPE_TIME|SQL_C_TYPE_TIME|SQL_DATETIME|SQL_CODE_TIME|  
|SQL_TYPE_TIMESTAMP|SQL_C_TYPE_TIMESTAMP|SQL_DATETIME|SQL_CODE_TIMESTAMP|  
|SQL_INTERVAL_MONTH|SQL_C_INTERVAL_MONTH|SQL_INTERVAL|SQL_CODE_MONTH|  
|SQL_INTERVAL_YEAR|SQL_C_INTERVAL_YEAR|SQL_INTERVAL|SQL_CODE_YEAR|  
|SQL_INTERVAL_YEAR_TO_MONTH|SQL_C_INTERVAL_YEAR_TO_MONTH|SQL_INTERVAL|SQL_CODE_YEAR_TO_MONTH|  
QL_INTERVAL_DAY|SQL_C_INTERVAL_DAY|SQL_INTERVAL|SQL_CODE_DAY|  
|SQL_INTERVAL_HOUR|SQL_C_INTERVAL_HOUR|SQL_INTERVAL|SQL_CODE_HOUR|  
|SQL_INTERVAL_MINUTE|SQL_C_INTERVAL_MINUTE|SQL_INTERVAL|SQL_CODE_MINUTE|  
|SQL_INTERVAL_SECOND|SQL_C_INTERVAL_SECOND|SQL_INTERVAL|SQL_CODE_SECOND|  
|SQL_INTERVAL_DAY_TO_HOUR|SQL_C_INTERVAL_DAY_TO_HOUR|SQL_INTERVAL|SQL_CODE_DAY_TO_HOUR|  
|SQL_INTERVAL_DAY_TO_MINUTE|SQL_C_INTERVAL_DAY_TO_MINUTE|SQL_INTERVAL|SQL_CODE_DAY_TO_MINUTE|  
QL_INTERVAL_DAY_TO_SECOND|SQL_C_INTERVAL_DAY_TO_SECOND|SQL_INTERVAL|SQL_CODE_DAY_TO_SECOND|  
|SQL_INTERVAL_HOUR_TO_MINUTE|SQL_C_INTERVAL_HOUR_TO_MINUTE|SQL_INTERVAL|SQL_CODE_HOUR_TO_MINUTE|  
|SQL_INTERVAL_HOUR_TO_SECOND|SQL_C_INTERVAL_HOUR_TO_SECOND|SQL_INTERVAL|SQL_CODE_HOUR_TO_SECOND|  
|SQL_INTERVAL_MINUTE_TO_SECOND|SQL_C_INTERVAL_MINUTE_TO_SECOND|SQL_INTERVAL|SQL_CODE_MINUTE_TO_SECOND|
