---
title: "smalldatetime (Transact-SQL)"
description: "smalldatetime (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "smalldatetime_TSQL"
  - "smalldatetime"
helpviewer_keywords:
  - "time [SQL Server], data types"
  - "smalldatetime data type [SQL Server]"
  - "dates [SQL Server], data types"
  - "date and time [SQL Server], smalldatetime"
  - "data types [SQL Server], date and time"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# smalldatetime (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Defines a date that is combined with a time of day. The time is based on a 24-hour day, with seconds always zero (:00) and without fractional seconds.
  
> [!NOTE]  
>  Use the **time**, **date**, **datetime2** and **datetimeoffset** data types for new work. These types align with the SQL Standard. They are more portable. **time**, **datetime2** and **datetimeoffset** provide more seconds precision. **datetimeoffset** provides time zone support for globally deployed applications.  
  
## smalldatetime description
  
|Property|Value|
|--------|-----|
|Syntax|**smalldatetime**|  
|Usage|DECLARE \@MySmalldatetime **smalldatetime**<br /><br /> CREATE TABLE Table1 ( Column1 **smalldatetime** )|  
|Default string literal formats<br /><br /> (used for down-level client)|Not applicable|  
|Date range|1900-01-01 through 2079-06-06<br /><br /> January 1, 1900, through June 6, 2079|  
|Time range|00:00:00 through 23:59:59<br /><br /> 2007-05-09 23:59:59 will round to<br /><br /> 2007-05-10 00:00:00|  
|Element ranges|YYYY is four digits, ranging from 1900, to 2079, that represent a year.<br /><br /> MM is two digits, ranging from 01 to 12, that represent a month in the specified year.<br /><br /> DD is two digits, ranging from 01 to 31 depending on the month, that represent a day of the specified month.<br /><br /> hh is two digits, ranging from 00 to 23, that represent the hour.<br /><br /> mm is two digits, ranging from 00 to 59, that represent the minute.<br /><br /> ss is two digits, ranging from 00 to 59, that represent the second. Values that are 29.998 seconds or less are rounded down to the nearest minute. Values of 29.999 seconds or more are rounded up to the nearest minute.|  
|Character length|19 positions maximum|  
|Storage size|4 bytes, fixed.|  
|Accuracy|One minute|  
|Default value|1900-01-01 00:00:00|  
|Calendar|Gregorian<br /><br /> (Doesn't include the complete range of years.)|  
|User-defined fractional second precision|No|  
|Time zone offset aware and preservation|No|  
|Daylight saving aware|No|  
  
## ANSI and ISO 8601 Compliance  
**smalldatetime** isn't ANSI or ISO 8601 compliant.
  
## Converting date and time data
When you convert to date and time data types, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it can't recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).
  
### Converting smalldatetime to other date and time types
This section describes what occurs when a **smalldatetime** data type is converted to other date and time data types.
  
For a conversion to **date**, the  year, month, and day are copied. The following code shows the results of converting a `smalldatetime` value to a `date` value.
  
```sql
DECLARE @smalldatetime smalldatetime = '1955-12-13 12:43:10';  
DECLARE @date date = @smalldatetime  
  
SELECT @smalldatetime AS '@smalldatetime', @date AS 'date';  
  
--Result  
--@smalldatetime          date  
------------------------- ----------  
--1955-12-13 12:43:00     1955-12-13  
--  
--(1 row(s) affected)  
```  
  
When the conversion is to **time(n)**, the hours, minutes, and seconds are copied. The fractional seconds are set to 0. The following code shows the results of converting a `smalldatetime` value to a `time(4)` value.
  
```sql
DECLARE @smalldatetime smalldatetime = '1955-12-13 12:43:10';  
DECLARE @time time(4) = @smalldatetime;  
  
SELECT @smalldatetime AS '@smalldatetime', @time AS 'time';  
  
--Result  
--@smalldatetime          time  
------------------------- -------------  
--1955-12-13 12:43:00     12:43:00.0000  
--  
--(1 row(s) affected)  
```  
  
When the conversion is to **datetime**, the **smalldatetime** value is copied to the **datetime** value. The fractional seconds are set to 0. The following code shows the results of converting a `smalldatetime` value to a `datetime` value.
  
```sql
DECLARE @smalldatetime smalldatetime = '1955-12-13 12:43:10';  
DECLARE @datetime datetime = @smalldatetime;  
  
SELECT @smalldatetime AS '@smalldatetime', @datetime AS 'datetime';  
  
--Result  
--@smalldatetime          datetime  
------------------------- -----------------------  
--1955-12-13 12:43:00     1955-12-13 12:43:00.000  
--  
--(1 row(s) affected)  
```  
  
For a conversion to **datetimeoffset(n)**, the **smalldatetime** value is copied to the **datetimeoffset(n)** value. The fractional seconds are set to 0, and the time zone offset is set to +00:0. The following code shows the results of converting a `smalldatetime` value to a `datetimeoffset(4)` value.
  
```sql
DECLARE @smalldatetime smalldatetime = '1955-12-13 12:43:10';  
DECLARE @datetimeoffset datetimeoffset(4) = @smalldatetime;  
  
SELECT @smalldatetime AS '@smalldatetime', @datetimeoffset AS 'datetimeoffset(4)';  
  
--Result  
--@smalldatetime          datetimeoffset(4)  
------------------------- ------------------------------  
--1955-12-13 12:43:00     1955-12-13 12:43:00.0000 +00:0  
--  
--(1 row(s) affected)  
```  
  
For the conversion to **datetime2(n)**, the **smalldatetime** value is copied to the **datetime2(n)** value. The fractional seconds are set to 0. The following code shows the results of converting a `smalldatetime` value to a `datetime2(4)` value.
  
```sql
DECLARE @smalldatetime smalldatetime = '1955-12-13 12:43:10';  
DECLARE @datetime2 datetime2(4) = @smalldatetime;  
  
SELECT @smalldatetime AS '@smalldatetime', @datetime2 AS ' datetime2(4)';  
  
--Result  
--@smalldatetime           datetime2(4)  
------------------------- ------------------------  
--1955-12-13 12:43:00     1955-12-13 12:43:00.0000  
--  
--(1 row(s) affected)  
```  
  
## Examples  
  
### A. Casting string literals with seconds to smalldatetime  
The following example compares the conversion of seconds in string literals to `smalldatetime`.
  
```sql
SELECT   
     CAST('2007-05-08 12:35:29'     AS smalldatetime)  
    ,CAST('2007-05-08 12:35:30'     AS smalldatetime)  
    ,CAST('2007-05-08 12:59:59.998' AS smalldatetime);  
```  
  
|Input|Output|  
|---|---|
|2007-05-08 12:35:29|2007-05-08 12:35:00|  
|2007-05-08 12:35:30|2007-05-08 12:36:00|  
|2007-05-08 12:59:59.998|2007-05-08 13:00:00|  
  
### B. Comparing date and time data types  
The following example compares the results of casting a string to each date and time data type.
  
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
  
