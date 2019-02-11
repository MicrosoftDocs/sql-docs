---
title: "datetime2 (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/23/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "datetime2"
  - "datetime2_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "time [SQL Server], data types"
  - "dates [SQL Server], data types"
  - "date and time [SQL Server], datetime2"
  - "data types [SQL Server], date and time"
  - "datetime2 data type [SQL Server]"
ms.assetid: 868017f3-214f-43ef-8536-cc1632a2288f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# datetime2 (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Defines a date that is combined with a time of day that is based on 24-hour clock. **datetime2** can be considered as an extension of the existing **datetime** type that has a larger date range, a larger default fractional precision, and optional user-specified precision.
  
## datetime2 description
  
|Property|Value|  
|--------------|-----------|  
|Syntax|**datetime2** [ (*fractional seconds precision*) ]|  
|Usage|DECLARE \@MyDatetime2 **datetime2(7)**<br /><br /> CREATE TABLE Table1 ( Column1 **datetime2(7)** )|  
|Default string literal format<br /><br /> (used for down-level client)|YYYY-MM-DD hh:mm:ss[.fractional seconds]<br /><br /> For more information, see the "Backward Compatibility for Down-level Clients" section that follows.|  
|Date range|0001-01-01 through 9999-12-31<br /><br /> January 1,1 CE through December 31, 9999 CE|  
|Time range|00:00:00 through 23:59:59.9999999|  
|Time zone offset range|None|  
|Element ranges|YYYY is a four-digit number, ranging from 0001 through 9999, that represents a year.<br /><br /> MM is a two-digit number, ranging from 01 to 12, that represents a month in the specified year.<br /><br /> DD is a two-digit number, ranging from 01 to 31 depending on the month, that represents a day of the specified month.<br /><br /> hh is a two-digit number, ranging from 00 to 23, that represents the hour.<br /><br /> mm is a two-digit number, ranging from 00 to 59, that represents the minute.<br /><br /> ss is a two-digit number, ranging from 00 to 59, that represents the second.<br /><br /> n* is a zero- to seven-digit number from 0 to 9999999 that represents the fractional seconds. In Informatica, the fractional seconds will be truncated when n > 3.|  
|Character length|19 positions minimum (YYYY-MM-DD hh:mm:ss ) to 27 maximum (YYYY-MM-DD hh:mm:ss.0000000)|  
|Precision, scale|0 to 7 digits, with an accuracy of 100ns. The default precision is 7 digits.|  
|Storage size|6 bytes for precisions less than 3; 7 bytes for precisions 3 and 4. All other precisions require 8 bytes.|  
|Accuracy|100 nanoseconds|  
|Default value|1900-01-01 00:00:00|  
|Calendar|Gregorian|  
|User-defined fractional second precision|Yes|  
|Time zone offset aware and preservation|No|  
|Daylight saving aware|No|  
  
For data type metadata, see [sys.systypes &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-systypes-transact-sql.md) or [TYPEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/typeproperty-transact-sql.md). Precision and scale are variable for some date and time data types. To obtain the precision and scale for a column, see [COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md), [COL_LENGTH &#40;Transact-SQL&#41;](../../t-sql/functions/col-length-transact-sql.md), or [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md).
  
## Supported string literal formats for datetime2
The following tables list the supported ISO 8601 and ODBC string literal formats for **datetime2**. For information about alphabetical, numeric, unseparated, and time formats for the date and time parts of **datetime2**, see [date &#40;Transact-SQL&#41;](../../t-sql/data-types/date-transact-sql.md) and [time &#40;Transact-SQL&#41;](../../t-sql/data-types/time-transact-sql.md).
  
|ISO 8601|Descriptions|  
|---|---|
|YYYY-MM-DDThh:mm:ss[.nnnnnnn]<br /><br /> YYYY-MM-DDThh:mm:ss[.nnnnnnn]|This format is not affected by the SET LANGUAGE and SET DATEFORMAT session locale settings. The **T**, the colons (:) and the period (.) are included in the string literal, for example '2007-05-02T19:58:47.1234567'.|  
  
|ODBC|Description|  
|---|---|
|{ ts 'yyyy-mm-dd hh:mm:ss[.fractional seconds]' }|ODBC API specific:<br /><br /> The number of digits to the right of the decimal point, which represents the fractional seconds, can be specified from 0 up to 7 (100 nanoseconds).|  
  
## ANSI and ISO 8601 compliance  
The ANSI and ISO 8601 compliance of [date](../../t-sql/data-types/date-transact-sql.md) and [time](../../t-sql/data-types/time-transact-sql.md) apply to **datetime2**.
  
##  Backward Compatibility for Down-level Clients  
Some down-level clients do not support the **time**, **date**, **datetime2** and **datetimeoffset** data types. The following table shows the type mapping between an up-level instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and down-level clients.
  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type|Default string literal format passed to down-level client|Down-level ODBC|Down-level OLEDB|Down-level JDBC|Down-level SQLCLIENT|  
| --- | --- | --- | --- | --- | --- |
|**time**|hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**date**|YYYY-MM-DD|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetime2**|YYYY-MM-DD hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetimeoffset**|YYYY-MM-DD hh:mm:ss[.nnnnnnn] [+&#124;-]hh:mm|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
  
## Converting date and time data
When you convert to date and time data types, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it cannot recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
### Converting other date and time types to the datetime2 data type
This section describes what occurs when other date and time data types are converted to the **datetime2** data type.  
  
When the conversion is from **date**, the year, month and day are copied.  The time component is set to 00:00:00.0000000.  The following code shows the results of converting a `date` value to a `datetime2` value.  
  
```sql
DECLARE @date date = '12-21-16';
DECLARE @datetime2 datetime2 = @date;

SELECT @datetime2 AS '@datetime2', @date AS '@date';
  
--Result  
--@datetime2                  @date
----------------------------- ----------
--2016-12-21 00:00:00.0000000 2016-12-21
```  
  
When the conversion is from **time(n)**, the time component is copied, and the date component is set to '1900-01-01'. The following example shows the results of converting a `time(7)` value to a `datetime2` value.  
  
```sql
DECLARE @time time(7) = '12:10:16.1234567';
DECLARE @datetime2 datetime2 = @time;

SELECT @datetime2 AS '@datetime2', @time AS '@time';
  
--Result  
--@datetime2                  @time
----------------------------- ----------------
--1900-01-01 12:10:16.1234567 12:10:16.1234567
```  
  
When the conversion is from **smalldatetime**, the hours and minutes are copied. The seconds and fractional seconds are set to 0. The following code shows the results of converting a `smalldatetime` value to a `datetime2` value.  
  
```sql
DECLARE @smalldatetime smalldatetime = '12-01-16 12:32';
DECLARE @datetime2 datetime2 = @smalldatetime;

SELECT @datetime2 AS '@datetime2', @smalldatetime AS '@smalldatetime'; 
  
--Result  
--@datetime2                  @smalldatetime
----------------------------- -----------------------
--2016-12-01 12:32:00.0000000 2016-12-01 12:32:00 
```  
  
When the conversion is from **datetimeoffset(n)**, the date and time components are copied. The time zone is truncated. The following example shows the results of converting a `datetimeoffset(7)` value to a `datetime2` value.  
  
```sql
DECLARE @datetimeoffset datetimeoffset(7) = '2016-10-23 12:45:37.1234567 +10:0';
DECLARE @datetime2 datetime2 = @datetimeoffset;

SELECT @datetime2 AS '@datetime2', @datetimeoffset AS '@datetimeoffset'; 
  
--Result  
--@datetime2                  @datetimeoffset
----------------------------- ----------------------------------
--2016-10-23 12:45:37.1234567 2016-10-23 12:45:37.1234567 +10:00
```  

When the conversion is from **datetime**, the date and time are copied. The fractional precision is extended to 7 digits. The following example shows the results of converting a `datetime` value to a `datetime2` value.

```sql
DECLARE @datetime datetime = '2016-10-23 12:45:37.333';
DECLARE @datetime2 datetime2 = @datetime;

SELECT @datetime2 AS '@datetime2', @datetime AS '@datetime';
   
--Result  
--@datetime2                  @datetime
------------------------- ---------------------------
--2016-10-23 12:45:37.3333333 2016-10-23 12:45:37.333
```  

> [!NOTE]
> Under database compatibility level 130, implicit conversions from datetime to datetime2 data types show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values, as seen in the example above. Use explicit casting to datetime2 datatype whenever a mixed comparison scenario between datetime and datetime2 datatypes exists. For more information, refer to this [Microsoft Support Article](https://support.microsoft.com/help/4010261).

### Converting String Literals to datetime2  
Conversions from string literals to date and time types are permitted if all parts of the strings are in valid formats. Otherwise, a runtime error is raised. Implicit conversions or explicit conversions that do not specify a style, from date and time types to string literals will be in the default format of the current session. The following table shows the rules for converting a string literal to the **datetime2** data type.
  
|Input string literal|**datetime2(n)**|  
|---|---|
|ODBC DATE|ODBC string literals are mapped to the **datetime** data type. Any assignment operation from ODBC DATETIME literals into **datetime2** types will cause an implicit conversion between **datetime** and this type as defined by the conversion rules.|  
|ODBC TIME|See previous ODBC DATE rule.|  
|ODBC DATETIME|See previous ODBC DATE rule.|  
|DATE only|The TIME part defaults to 00:00:00.|  
|TIME only|The DATE part defaults to 1900-1-1.|  
|TIMEZONE only|Default values are supplied.|  
|DATE + TIME|Trivial|  
|DATE + TIMEZONE|Not allowed.|  
|TIME + TIMEZONE|The DATE part defaults to 1900-1-1. TIMEZONE input is ignored.|  
|DATE + TIME + TIMEZONE|The local DATETIME will be used.|  
  
## Examples  
The following example compares the results of casting a string to each **date** and **time** data type.
  
```sql
SELECT   
     CAST('2007-05-08 12:35:29. 1234567 +12:15' AS time(7)) AS 'time'   
    ,CAST('2007-05-08 12:35:29. 1234567 +12:15' AS date) AS 'date'   
    ,CAST('2007-05-08 12:35:29.123' AS smalldatetime) AS   
        'smalldatetime'   
    ,CAST('2007-05-08 12:35:29.123' AS datetime) AS 'datetime'   
    ,CAST('2007-05-08 12:35:29. 1234567 +12:15' AS datetime2(7)) AS   
        'datetime2'  
    ,CAST('2007-05-08 12:35:29.1234567 +12:15' AS datetimeoffset(7)) AS   
        'datetimeoffset';  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
|Data type|Output|  
|---|---|
|**time**|12:35:29. 1234567|  
|**date**|2007-05-08|  
|**smalldatetime**|2007-05-08 12:35:00|  
|**datetime**|2007-05-08 12:35:29.123|  
|**datetime2**|2007-05-08 12:35:29. 1234567|  
|**datetimeoffset**|2007-05-08 12:35:29.1234567 +12:15|  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
  
