---
title: "datetimeoffset (Transact-SQL)"
description: "datetimeoffset (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/23/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "datetimeoffset_TSQL"
  - "datetimeoffset"
helpviewer_keywords:
  - "time [SQL Server], data types"
  - "date and time [SQL Server], datetimeoffset"
  - "datetimeoffset data type [SQL Server]"
  - "dates [SQL Server], data types"
  - "data types [SQL Server], date and time"
  - "time zones [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# datetimeoffset (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Defines a date that is combined with a time of a day based on a 24-hour clock like [datetime2](../../t-sql/data-types/datetime2-transact-sql.md), and adds time zone awareness based on UTC (Universal Time Coordinate or Greenwich Mean Time). 

## datetimeoffset description
  
|Property|Value|  
|---|---|
|Syntax|**datetimeoffset** [ (*fractional seconds precision*) ]|  
|Usage|DECLARE \@MyDatetimeoffset **datetimeoffset(7)**<br /><br /> CREATE TABLE Table1 ( Column1 **datetimeoffset(7)** )|  
|Default string literal formats (used for down-level client)|YYYY-MM-DD hh:mm:ss[.nnnnnnn] [{+&#124;-}hh:mm]<br /><br /> For more information, see the "Backward Compatibility for Down-level Clients" section that follows.|  
|Date range|0001-01-01 through 9999-12-31<br /><br /> January 1, 1 CE through December 31, 9999 CE|  
|Time range|00:00:00 through 23:59:59.9999999|  
|Time zone offset range|-14:00 through +14:00|  
|Element ranges|YYYY is four digits, ranging from 0001 through 9999, that represent a year.<br /><br /> MM is two digits, ranging from 01 to 12, that represent a month in the specified year.<br /><br /> DD is two digits, ranging from 01 to 31 depending on the month, that represent a day of the specified month.<br /><br /> hh is two digits, ranging from 00 to 23, that represent the hour.<br /><br /> mm is two digits, ranging from 00 to 59, that represent the minute.<br /><br /> ss is two digits, ranging from 00 to 59, that represent the second.<br /><br /> n* is zero to seven digits, ranging from 0 to 9999999, that represent the fractional seconds.<br /><br /> hh is two digits that range from -14 to +14. <br /><br /> mm is two digits that range from 00 to 59.|  
|Character length|26 positions minimum (YYYY-MM-DD hh:mm:ss {+&#124;-}hh:mm) to 34 maximum (YYYY-MM-DD hh:mm:ss.nnnnnnn {+&#124;-}hh:mm)|  
|Precision, scale|See the table below.|  
|Storage size|10 bytes, fixed is the default with the default of 100ns fractional second precision.|  
|Accuracy|100 nanoseconds|  
|Default value|1900-01-01 00:00:00 00:00|  
|Calendar|Gregorian|  
|User-defined fractional second precision|Yes|  
|Time zone offset aware and preservation|Yes|  
|Daylight saving aware|No|  
  
|Specified scale|Result (precision, scale)|Column length (bytes)|Fractional seconds precision|  
|---|---|---|---|
|**datetimeoffset**|(34,7)|10|7|  
|**datetimeoffset(0)**|(26,0)|8|0-2|  
|**datetimeoffset(1)**|(28,1)|8|0-2|  
|**datetimeoffset(2)**|(29,2)|8|0-2|  
|**datetimeoffset(3)**|(30,3)|9|3-4|  
|**datetimeoffset(4)**|(31,4)|9|3-4|  
|**datetimeoffset(5)**|(32,5)|10|5-7|  
|**datetimeoffset(6)**|(33,6)|10|5-7|  
|**datetimeoffset(7)**|(34,7)|10|5-7|  
  
## Supported string literal formats for datetimeoffset
The following table lists the supported ISO 8601 string literal formats for **datetimeoffset**. For information about alphabetical, numeric, unseparated and time formats for the date and time parts of **datetimeoffset**, see [date &#40;Transact-SQL&#41;](../../t-sql/data-types/date-transact-sql.md) and [time &#40;Transact-SQL&#41;](../../t-sql/data-types/time-transact-sql.md).
  
|ISO 8601|Description|  
|---|---|
|YYYY-MM-DDThh:mm:ss[.nnnnnnn][{+&#124;-}hh:mm]|These two formats are not affected by the SET LANGUAGE and SET DATEFORMAT session locale settings. Spaces are not allowed between the **datetimeoffset** and the **datetime** parts.|  
|YYYY-MM-DDThh:mm:ss[.nnnnnnn]Z (UTC)|This format by ISO definition indicates the **datetime** portion should be expressed in Coordinated Universal Time (UTC). For example, 1999-12-12 12:30:30.12345 -07:00 should be represented as 1999-12-12 19:30:30.12345Z.|  
  
## Time zone offset
A time zone offset specifies the zone offset from UTC for a **time** or **datetime** value. The time zone offset can be represented as [+|-] hh:mm:
-   hh is two digits that range from 00 to 14 and represent the number of hours in the time zone offset.  
-   mm is two digits, ranging from 00 to 59, that represent the number of additional minutes in the time zone offset.  
-   \+ (plus) or - (minus) is the mandatory sign for a time zone offset. This indicates whether the time zone offset is added or subtracted from the UTC time to obtain the local time. The valid range of time zone offset is from -14:00 to +14:00.  
  
The time zone offset range follows the W3C XML standard for XSD schema definition and is slightly different from the SQL 2003 standard definition, 12:59 to +14:00.
  
The optional type parameter *fractional seconds precision* specifies the number of digits for the fractional part of the seconds. This value can be an integer with 0 to 7 (100 nanoseconds). The default *fractional seconds precision* is 100ns (seven digits for the fractional part of the seconds).
  
The data is stored in the database and processed, compared, sorted, and indexed in the server as in UTC. The time zone offset will be preserved in the database for retrieval.
  
The given time zone offset will be assumed to be daylight saving time (DST) aware and adjusted for any given **datetime** that is in the DST period.
  
For **datetimeoffset** type, both UTC and local (to the persistent or converted time zone offset) **datetime** value will be validated during insert, update, arithmetic, convert, or assign operations. The detection of any invalid UTC or local (to the persistent or converted time zone offset) **datetime** value will raise an invalid value error. For example, 9999-12-31 10:10:00 is valid in UTC, but overflow in local time to the time zone offset +13:50.
  
To convert a date to a corresponding **datetimeoffset** value in a target time zone, see [AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md).
  
## ANSI and ISO 8601 compliance  
The ANSI and ISO 8601 Compliance sections of the [date](../../t-sql/data-types/date-transact-sql.md) and [time](../../t-sql/data-types/time-transact-sql.md) topics apply to **datetimeoffset**.
  
## Backward compatibility for down-level clients
Some down-level clients do not support the **time**, **date**, **datetime2** and **datetimeoffset** data types. The following table shows the type mapping between an up-level instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and down-level clients.
  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type|Default string literal format passed to down-level client|Down-level ODBC|Down-level OLEDB|Down-level JDBC|Down-level SQLCLIENT|  
|---|---|---|---|---|---|
|**time**|hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**date**|YYYY-MM-DD|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetime2**|YYYY-MM-DD hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetimeoffset**|YYYY-MM-DD hh:mm:ss[.nnnnnnn] [+&#124;-]hh:mm|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
  
## Converting date and time data
When you convert to date and time data types, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it cannot recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
### Converting datetimeoffset data type to other date and time types
This section describes what occurs when a **datetimeoffset** data type is converted to other date and time data types.
  
When converting to  **date**, the year, month, and day are copied. The following code shows the results of converting a `datetimeoffset(4)` value to a `date` value.  
  
```sql
DECLARE @datetimeoffset datetimeoffset(4) = '12-10-25 12:32:10 +01:00';  
DECLARE @date date= @datetimeoffset;  
  
SELECT @datetimeoffset AS '@datetimeoffset ', @date AS 'date';  
  
--Result  
--@datetimeoffset                date  
-------------------------------- ----------  
--2025-12-10 12:32:10.0000 +01:0 2025-12-10  
--  
--(1 row(s) affected)  
  
```  
  
If the conversion is to **time(n)**, the hour, minute, second, and fractional seconds are copied. The time zone value is truncated. When the precision of the **datetimeoffset(n)** value is greater than the precision of the **time(n)** value, the value is rounded up. The following code shows the results of converting a `datetimeoffset(4)` value to a `time(3)` value.
  
```sql
DECLARE @datetimeoffset datetimeoffset(4) = '12-10-25 12:32:10.1237 +01:0';  
DECLARE @time time(3) = @datetimeoffset;  
  
SELECT @datetimeoffset AS '@datetimeoffset ', @time AS 'time';  
  
--Result  
--@datetimeoffset                time  
-------------------------------- ------------  
-- 2025-12-10 12:32:10.1237 +01:00    12:32:10.124  
  
--  
--(1 row(s) affected)  
  
```  
  
When converting to**datetime**, the date and time values are copied, and the time zone is truncated. When the fractional precision of the **datetimeoffset(n)** value is greater than three digits, the value is truncated. The following code shows the results of converting a `datetimeoffset(4)` value to a `datetime` value.
  
```sql
DECLARE @datetimeoffset datetimeoffset(4) = '12-10-25 12:32:10.1237 +01:0';  
DECLARE @datetime datetime = @datetimeoffset;  
  
SELECT @datetimeoffset AS '@datetimeoffset ', @datetime AS 'datetime';  
  
--Result  
--@datetimeoffset                datetime  
-------------------------------- -----------------------  
--2025-12-10 12:32:10.1237 +01:0 2025-12-10 12:32:10.123  
--  
--(1 row(s) affected)  
```  
  
For conversions to  **smalldatetime**, the date and hours are copied. The minutes are rounded up with respect to the seconds value and seconds are set to 0. The following code shows the results of converting a `datetimeoffset(3)` value to a `smalldatetime` value.  
  
```sql
DECLARE @datetimeoffset datetimeoffset(3) = '1912-10-25 12:24:32 +10:0';  
DECLARE @smalldatetime smalldatetime = @datetimeoffset;  
  
SELECT @datetimeoffset AS '@datetimeoffset', @smalldatetime AS '@smalldatetime';  
  
--Result  
--@datetimeoffset                @smalldatetime  
-------------------------------- -----------------------  
--1912-10-25 12:24:32.000 +10:00 1912-10-25 12:25:00  
--  
--(1 row(s) affected)  
```  
  
If the conversion is to **datetime2(n)**, the date and time are copied to the **datetime2** value, and the time zone is truncated. When the precision of the **datetime2(n)** value is greater than the precision of the **datetimeoffset(n)** value, the fractional seconds are truncated to fit. The following code shows the results of converting a `datetimeoffset(4)` value to a `datetime2(3)` value.
  
```sql
DECLARE @datetimeoffset datetimeoffset(4) = '1912-10-25 12:24:32.1277 +10:0';  
DECLARE @datetime2 datetime2(3)=@datetimeoffset;  
  
SELECT @datetimeoffset AS '@datetimeoffset', @datetime2 AS '@datetime2';  
  
--Result  
@datetimeoffset                    @datetime2  
---------------------------------- ----------------------  
1912-10-25 12:24:32.1277 +10:00    1912-10-25 12:24:32.12  
  
--(1 row(s) affected)  
```  
  
### Converting string literals to datetimeoffset
Conversions from string literals to date and time types are permitted if all parts of the strings are in valid formats. Otherwise, a runtime error is raised. Implicit conversions or explicit conversions that do not specify a style, from date and time types to string literals will be in the default format of the current session. The following table shows the rules for converting a string literal to the **datetimeoffset** data type.
  
|Input string literal|**datetimeoffset(n)**|  
|---|---|
|ODBC DATE|ODBC string literals are mapped to the **datetime** data type. Any assignment operation from ODBC DATETIME literals into **datetimeoffset** types will cause an implicit conversion between **datetime** and this type as defined by the conversion rules.|  
|ODBC TIME|See previous ODBC DATE rule.|  
|ODBC DATETIME|See previous ODBC DATE rule.|  
|DATE only|The TIME part defaults to 00:00:00. The TIMEZONE defaults to +00:00.|  
|TIME only|The DATE part defaults to 1900-1-1. The TIMEZONE will default to +00:00.|  
|TIMEZONE only|Default values are supplied|  
|DATE + TIME|The TIMEZONE defaults to +00:00.|  
|DATE + TIMEZONE|Not allowed|  
|TIME + TIMEZONE|The DATE part defaults to 1900-1-1.|  
|DATE + TIME + TIMEZONE|Trivial|  
  
## Examples  
The following example compares the results of casting a string to each **date** and **time** data type.
  
```sql
SELECT   
     CAST('2007-05-08 12:35:29. 1234567 +12:15' AS time(7)) AS 'time'   
    ,CAST('2007-05-08 12:35:29. 1234567 +12:15' AS date) AS 'date'   
    ,CAST('2007-05-08 12:35:29.123' AS smalldatetime) AS   
        'smalldatetime'   
    ,CAST('2007-05-08 12:35:29.123' AS datetime) AS 'datetime'   
    ,CAST('2007-05-08 12:35:29.1234567+12:15' AS datetime2(7)) AS   
        'datetime2'  
    ,CAST('2007-05-08 12:35:29.1234567 +12:15' AS datetimeoffset(7)) AS   
        'datetimeoffset'  
    ,CAST('2007-05-08 12:35:29.1234567+12:15' AS datetimeoffset(7)) AS  
        'datetimeoffset IS08601';  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
|Data type|Output|  
|---|---|
|**Time**|12:35:29. 1234567|  
|**Date**|2007-05-08|  
|**Smalldatetime**|2007-05-08 12:35:00|  
|**Datetime**|2007-05-08 12:35:29.123|  
|**datetime2**|2007-05-08 12:35:29. 1234567|  
|**Datetimeoffset**|2007-05-08 12:35:29.1234567 +12:15|  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)
  
  
