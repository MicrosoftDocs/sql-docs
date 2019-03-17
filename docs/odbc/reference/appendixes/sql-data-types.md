---
title: "SQL Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL data types [ODBC]"
  - "SQL data types [ODBC], about SQL data types"
  - "data types [ODBC], SQL data types"
ms.assetid: 1b22f985-f5e4-4779-87eb-e43329a442b1
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL Data Types
Each DBMS defines its own SQL types. Each ODBC driver exposes only those SQL data types that the associated DBMS defines. Information about how a driver maps DBMS SQL types to the ODBC-defined SQL type identifiers and how a driver maps DBMS SQL types to its own driver-specific SQL type identifiers is returned through a call to **SQLGetTypeInfo**. A driver also returns the SQL data types when describing the data types of columns and parameters through calls to **SQLColAttribute**, **SQLColumns**, **SQLDescribeCol**, **SQLDescribeParam**, **SQLProcedureColumns**, and **SQLSpecialColumns**.  
  
> [!NOTE]  
>  The SQL data types are contained in the SQL_DESC_ CONCISE_TYPE, SQL_DESC_TYPE, and SQL_DESC_DATETIME_INTERVAL_CODE fields of the implementation descriptors. Characteristics of the SQL data types are contained in the SQL_DESC_PRECISION, SQL_DESC_SCALE, SQL_DESC_LENGTH, and SQL_DESC_OCTET_LENGTH fields of the implementation descriptors. For more information, see [Data Type Identifiers and Descriptors](../../../odbc/reference/appendixes/data-type-identifiers-and-descriptors.md) later in this appendix.  
  
 A given driver and data source do not necessarily support all the SQL data types that are defined in this appendix. A driver's support for SQL data types depends on the level of SQL-92 that the driver complies with. To determine the level of SQL-92 grammar supported by the driver, an application calls **SQLGetInfo** with the SQL_SQL_CONFORMANCE information type. Additionally, a given driver and data source may support additional, driver-specific SQL data types. To determine which data types a driver supports, an application calls **SQLGetTypeInfo**. For information about driver-specific SQL data types, see the driver's documentation. For information about the data types in a specific data source, see the documentation for that data source.  
  
> [!IMPORTANT]  
>  The tables throughout this appendix are only guidelines and show typically used names, ranges, and limits of SQL data types. A given data source might support only some of the listed data types, and the characteristics of the supported data types can differ from those listed.  
  
 The following table lists valid SQL type identifiers for all SQL data types. The table also lists the name and description of the corresponding data type from SQL-92 (if one exists).  
  
|SQL type identifier[1]|Typical SQL data<br /><br /> type[2]|Typical type description|  
|------------------------------|------------------------------------|------------------------------|  
|SQL_CHAR|CHAR(*n*)|Character string of fixed string length *n*.|  
|SQL_VARCHAR|VARCHAR(*n*)|Variable-length character string with a maximum string length *n*.|  
|SQL_LONGVARCHAR|LONG VARCHAR|Variable length character data. Maximum length is data source-dependent.[9]|  
|SQL_WCHAR|WCHAR(*n*)|Unicode character string of fixed string length *n*|  
|SQL_WVARCHAR|VARWCHAR(*n*)|Unicode variable-length character string with a maximum string length *n*|  
|SQL_WLONGVARCHAR|LONGWVARCHAR|Unicode variable-length character data. Maximum length is data source-dependent|  
|SQL_DECIMAL|DECIMAL(*p*,*s*)|Signed, exact, numeric value with a precision of at least *p* and scale *s.* (The maximum precision is driver-defined.) (1 <= *p* <= 15; *s* <= *p*).[4]|  
|SQL_NUMERIC|NUMERIC(*p*,*s*)|Signed, exact, numeric value with a precision *p* and scale *s* (1 <= *p* <= 15; *s* <= *p*).[4]|  
|SQL_SMALLINT|SMALLINT|Exact numeric value with precision 5 and scale 0  (signed:  -32,768 <= *n* <= 32,767, unsigned:  0 <= *n* <= 65,535)[3].|  
|SQL_INTEGER|INTEGER|Exact numeric value with precision 10 and scale 0  (signed:  -2[31] <= *n* <= 2[31] - 1, unsigned:  0 <= *n* <= 2[32] - 1)[3].|  
|SQL_REAL|REAL|Signed, approximate, numeric value with a binary precision 24 (zero or absolute value 10[-38] to 10[38]).|  
|SQL_FLOAT|FLOAT(*p*)|Signed, approximate, numeric value with a binary precision of at least *p*. (The maximum precision is driver-defined.)[5]|  
|SQL_DOUBLE|DOUBLE PRECISION|Signed, approximate, numeric value with a binary precision 53 (zero or absolute value 10[-308] to 10[308]).|  
|SQL_BIT|BIT|Single bit binary data.[8]|  
|SQL_TINYINT|TINYINT|Exact numeric value with precision 3 and scale 0  (signed:  -128 <= *n* <= 127,  unsigned:  0 <= *n* <= 255)[3].|  
|SQL_BIGINT|BIGINT|Exact numeric value with precision 19 (if signed) or 20 (if unsigned) and scale 0  (signed:  -2[63] <= *n* <= 2[63] - 1,  unsigned: 0 <= *n* <= 2[64] - 1)[3],[9].|  
|SQL_BINARY|BINARY(*n*)|Binary data of fixed length *n*.[9]|  
|SQL_VARBINARY|VARBINARY(*n*)|Variable length binary data of maximum length *n*. The maximum is set by the user.[9]|  
|SQL_LONGVARBINARY|LONG VARBINARY|Variable length binary data. Maximum length is data source-dependent.[9]|  
|SQL_TYPE_DATE[6]|DATE|Year, month, and day fields, conforming to the rules of the Gregorian calendar. (See [Constraints of the Gregorian Calendar](../../../odbc/reference/appendixes/constraints-of-the-gregorian-calendar.md), later in this appendix.)|  
|SQL_TYPE_TIME[6]|TIME(*p*)|Hour, minute, and second fields, with valid values for hours of 00 to 23, valid values for minutes of 00 to 59, and valid values for seconds of 00 to 61. Precision *p* indicates the seconds precision.|  
|SQL_TYPE_TIMESTAMP[6]|TIMESTAMP(*p*)|Year, month, day, hour, minute, and second fields, with valid values as defined for the DATE and TIME data types.|  
|SQL_TYPE_UTCDATETIME|UTCDATETIME|Year, month, day, hour, minute, second, utchour, and utcminute fields. The utchour and utcminute fields have 1/10 microsecond precision.|  
|SQL_TYPE_UTCTIME|UTCTIME|Hour, minute, second, utchour, and utcminute fields. The utchour and utcminute fields have 1/10 microsecond precision..|  
|SQL_INTERVAL_MONTH[7]|INTERVAL MONTH(*p*)|Number of months between two dates; *p* is the interval leading precision.|  
|SQL_INTERVAL_YEAR[7]|INTERVAL YEAR(*p*)|Number of years between two dates; *p* is the interval leading precision.|  
|SQL_INTERVAL_YEAR_TO_MONTH[7]|INTERVAL YEAR(*p*) TO MONTH|Number of years and months between two dates; *p* is the interval leading precision.|  
|SQL_INTERVAL_DAY[7]|INTERVAL DAY(*p*)|Number of days between two dates; *p* is the interval leading precision.|  
|SQL_INTERVAL_HOUR[7]|INTERVAL HOUR(*p*)|Number of hours between two date/times; *p* is the interval leading precision.|  
|SQL_INTERVAL_MINUTE[7]|INTERVAL MINUTE(*p*)|Number of minutes between two date/times; *p* is the interval leading precision.|  
|SQL_INTERVAL_SECOND[7]|INTERVAL SECOND(*p*,*q*)|Number of seconds between two date/times; *p* is the interval leading precision and *q* is the interval seconds precision.|  
|SQL_INTERVAL_DAY_TO_HOUR[7]|INTERVAL DAY(*p*) TO HOUR|Number of days/hours between two date/times; *p* is the interval leading precision.|  
|SQL_INTERVAL_DAY_TO_MINUTE[7]|INTERVAL DAY(*p*) TO MINUTE|Number of days/hours/minutes between two date/times; *p* is the interval leading precision.|  
|SQL_INTERVAL_DAY_TO_SECOND[7]|INTERVAL DAY(*p*) TO SECOND(*q*)|Number of days/hours/minutes/seconds between two date/times; *p* is the interval leading precision and *q* is the interval seconds precision.|  
|SQL_INTERVAL_HOUR_TO_MINUTE[7]|INTERVAL HOUR(*p*) TO MINUTE|Number of hours/minutes between two date/times; *p* is the interval leading precision.|  
|SQL_INTERVAL_HOUR_TO_SECOND[7]|INTERVAL HOUR(*p*) TO SECOND(*q*)|Number of hours/minutes/seconds between two date/times; *p* is the interval leading precision and *q* is the interval seconds precision.|  
|SQL_INTERVAL_MINUTE_TO_SECOND[7]|INTERVAL MINUTE(*p*) TO SECOND(*q*)|Number of minutes/seconds between two date/times; *p* is the interval leading precision and *q* is the interval seconds precision.|  
|SQL_GUID|GUID|Fixed length GUID.|  
  
 [1]   This is the value returned in the DATA_TYPE column by a call to **SQLGetTypeInfo**.  
  
 [2]   This is the value returned in the NAME and CREATE PARAMS column by a call to **SQLGetTypeInfo**. The NAME column returns the designation-for example, CHAR-whereas the CREATE PARAMS column returns a comma-separated list of creation parameters such as precision, scale, and length.  
  
 [3]   An application uses **SQLGetTypeInfo** or **SQLColAttribute** to determine whether a particular data type or a particular column in a result set is unsigned.  
  
 [4]   SQL_DECIMAL and SQL_NUMERIC data types differ only in their precision. The precision of a DECIMAL(*p*,*s*) is an implementation-defined decimal precision that is no less than *p*, whereas the precision of a NUMERIC(*p*,*s*) is exactly equal to *p*.  
  
 [5]   Depending on the implementation, the precision of SQL_FLOAT can be either 24 or 53: if it is 24, the SQL_FLOAT data type is the same as SQL_REAL; if it is 53, the SQL_FLOAT data type is the same as SQL_DOUBLE.  
  
 [6]   In ODBC 3*.x*, the SQL date, time, and timestamp data types are SQL_TYPE_DATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP, respectively; in ODBC 2.*x*, the data types are SQL_DATE, SQL_TIME, and SQL_TIMESTAMP.  
  
 [7]   For more information about the interval SQL data types, see the [Interval Data Types](../../../odbc/reference/appendixes/interval-data-types.md) section, later in this appendix.  
  
 [8]   The SQL_BIT data type has different characteristics than the BIT type in SQL-92.  
  
 [9]   This data type has no corresponding data type in SQL-92.  
  
 This section provides the following example.  
  
-   [Example SQLGetTypeInfo Result Set](../../../odbc/reference/appendixes/example-sqlgettypeinfo-result-set.md)
