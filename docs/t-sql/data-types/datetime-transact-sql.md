---
title: "datetime (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/23/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "datetime_TSQL"
  - "datetime"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "time [SQL Server], data types"
  - "date and time [SQL Server], datetime"
  - "dates [SQL Server], data types"
  - "datetime data type [SQL Server]"
  - "data types [SQL Server], date and time"
ms.assetid: 9bd1cc5b-227b-4032-95d6-7581ddcc9924
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# datetime (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

Defines a date that is combined with a time of day with fractional seconds that is based on a 24-hour clock.
  
> [!NOTE]  
>  Use the **time**, **date**, **datetime2** and **datetimeoffset** data types for new work. These types align with the SQL Standard. They are more portable. **time**, **datetime2** and **datetimeoffset** provide more seconds precision. **datetimeoffset** provides time zone support for globally deployed applications.  
  
## datetime Description  
  
|Property|Value|  
|---|---|
|Syntax|**datetime**|  
|Usage|DECLARE \@MyDatetime **datetime**<br /><br /> CREATE TABLE Table1 ( Column1 **datetime** )|  
|Default string literal formats<br /><br /> (used for down-level client)|Not applicable|  
|Date range|January 1, 1753, through December 31, 9999|  
|Time range|00:00:00 through 23:59:59.997|  
|Time zone offset range|None|  
|Element ranges|YYYY is four digits from 1753 through 9999 that represent a year.<br /><br /> MM is two digits, ranging from 01 to 12, that represent a month in the specified year.<br /><br /> DD is two digits, ranging from 01 to 31 depending on the month, that represent a day of the specified month.<br /><br /> hh is two digits, ranging from 00 to 23, that represent the hour.<br /><br /> mm is two digits, ranging from 00 to 59, that represent the minute.<br /><br /> ss is two digits, ranging from 00 to 59, that represent the second.<br /><br /> n* is zero to three digits, ranging from 0 to 999, that represent the fractional seconds.|  
|Character length|19 positions minimum to 23 maximum|  
|Storage size|8 bytes|  
|Accuracy|Rounded to increments of .000, .003, or .007 seconds|  
|Default value|1900-01-01 00:00:00|  
|Calendar|Gregorian (Does include the complete range of years.)|  
|User-defined fractional second precision|No|  
|Time zone offset aware and preservation|No|  
|Daylight saving aware|No|  
  
## Supported String Literal Formats for datetime  
The following tables list the supported string literal formats for **datetime**. Except for ODBC, **datetime** string literals are in single quotation marks ('), for example, 'string_literaL'. If the environment isn't **us_english**, the string literals should be in the format N'string_literaL'.
  
|Numeric|Description|  
|---|---|
|Date formats:<br /><br /> [0]4/15/[19]96 -- (mdy)<br /><br /> [0]4-15-[19]96 -- (mdy)<br /><br /> [0]4.15.[19]96 -- (mdy)<br /><br /> [0]4/[19]96/15 -- (myd)<br /><br /> 15/[0]4/[19]96 -- (dmy)<br /><br /> 15/[19]96/[0]4 -- (dym)<br /><br /> [19]96/15/[0]4 -- (ydm)<br /><br /> [19]96/[0]4/15 -- (ymd)<br /><br /> Time formats:<br /><br /> 14:30<br /><br /> 14:30[:20:999]<br /><br /> 14:30[:20.9]<br /><br /> 4am<br /><br /> 4 PM|You can specify date data with a numeric month specified. For example, 5/20/97 represents the twentieth day of May 1997. When you use numeric date format, specify the month, day, and year in a string that uses slash marks (/), hyphens (-), or periods (.) as separators. This string must appear in the following form:<br /><br /> *number separator number separator number [time] [time]*<br /><br /> <br /><br /> When the language is set to **us_english**, the default order for the date is mdy. You can change the date order by using the [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md) statement.<br /><br /> The setting for SET DATEFORMAT determines how date values are interpreted. If the order doesn't match the setting, the values aren't interpreted as dates, because they are out of range or the values are misinterpreted. For example, 12/10/08 can be interpreted as one of six dates, depending on the DATEFORMAT setting. A four-part year is interpreted as the year.|  
  
|Alphabetical|Description|  
|---|---|
|Apr[il] [15][,] 1996<br /><br /> Apr[il] 15[,] [19]96<br /><br /> Apr[il] 1996 [15]<br /><br /> [15] Apr[il][,] 1996<br /><br /> 15 Apr[il][,][19]96<br /><br /> 15 [19]96 apr[il]<br /><br /> [15] 1996 apr[il]<br /><br /> 1996 APR[IL] [15]<br /><br /> 1996 [15] APR[IL]|You can specify date data with a month specified as the full month name. For example, April or the month abbreviation of Apr specified in the current language; commas are optional and capitalization is ignored.<br /><br /> Here are some guidelines for using alphabetical date formats:<br /><br /> 1) Enclose the date and time data in single quotation marks ('). For languages other than English, use N'<br /><br /> 2) Characters that are enclosed in brackets are optional.<br /><br /> 3) If you specify only the last two digits of the year, values less than the last two digits of the value of the [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) configuration option are in the same century as the cutoff year. Values greater than or equal to the value of this option are in the century that comes before the cutoff year. For example, if **two digit year cutoff** is 2050 (default), 25 is interpreted as 2025 and 50 is interpreted as 1950. To avoid ambiguity, use four-digit years.<br /><br /> 4) If the day is missing, the first day of the month is supplied.<br /><br /> <br /><br /> The SET DATEFORMAT session setting isn't applied when you specify the month in alphabetical form.|  
  
|ISO 8601|Description|  
|---|---|
|YYYY-MM-DDThh:mm:ss[.mmm]<br /><br /> YYYYMMDD[ hh:mm:ss[.mmm]]|Examples:<br /><br /> 1) 2004-05-23T14:25:10<br /><br /> 2) 2004-05-23T14:25:10.487<br /><br /> <br /><br /> To use the ISO 8601 format, you must specify each element in the format. This also includes the **T**, the colons (:), and the period (.) that are shown in the format.<br /><br /> The brackets indicate that the fraction of second component is optional. The time component is specified in the 24-hour format.<br /><br /> The T indicates the start of the time part of the **datetime** value.<br /><br /> The advantage in using the ISO 8601 format is that it is an international standard with unambiguous specification. Also, this format isn't affected by the SET DATEFORMAT or [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md) setting.|  
  
|Unseparated|Description|  
|---|---|
|YYYYMMDD hh:mm:ss[.mmm]||  
  
|ODBC|Description|  
|---|---|
|{ ts '1998-05-02 01:23:56.123' }<br /><br /> { d '1990-10-02' }<br /><br /> { t '13:33:41' }|The ODBC API defines escape sequences to represent date and time values, which ODBC calls timestamp data. This ODBC timestamp format is also supported by the OLE DB language definition (DBGUID-SQL) supported by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Applications that use the ADO, OLE DB, and ODBC-based APIs can use this ODBC timestamp format to represent dates and times.<br /><br /> ODBC timestamp escape sequences are of the format: { *literal_type* '*constant_value*' }:<br /><br /> <br /><br /> - *literal_type* specifies the type of the escape sequence. Timestamps have three *literal_type* specifiers:<br />1) d = date only<br />2) t = time only<br />3) ts = timestamp (time + date)<br /><br /> <br /><br /> - '*constant_value*' is the value of the escape sequence. *constant_value* must follow these formats for each *literal_type*.<br />d : yyyy-mm-dd<br />t : hh:mm:ss[.fff]<br />ts : yyyy-mm-dd hh:mm:ss[.fff]|  
  
## Rounding of datetime Fractional Second Precision  
**datetime** values are rounded to increments of .000, .003, or .007 seconds, as shown in the following table.
  
|User-specified value|System stored value|  
|---|---|
|01/01/98 23:59:59.999|1998-01-02 00:00:00.000|  
|01/01/98 23:59:59.995<br /><br /> 01/01/98 23:59:59.996<br /><br /> 01/01/98 23:59:59.997<br /><br /> 01/01/98 23:59:59.998|1998-01-01 23:59:59.997|  
|01/01/98 23:59:59.992<br /><br /> 01/01/98 23:59:59.993<br /><br /> 01/01/98 23:59:59.994|1998-01-01 23:59:59.993|  
|01/01/98 23:59:59.990<br /><br /> 01/01/98 23:59:59.991|1998-01-01 23:59:59.990|  
  
## ANSI and ISO 8601 Compliance  
**datetime** isn't ANSI or ISO 8601 compliant.
  
##  <a name="_datetime"></a> Converting Date and Time Data  
When you convert to date and time data types, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it can't recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).
  
### Converting Other Date and Time Types to the datetime Data Type 
This section describes what occurs when other date and time data types are converted to the **datetime** data type.  
  
When the conversion is from **date**, the year, month and day are copied. The time component is set to 00:00:00.000. The following code shows the results of converting a `date` value to a `datetime` value.  
  
```sql
DECLARE @date date = '12-21-16';  
DECLARE @datetime datetime = @date;  
  
SELECT @datetime AS '@datetime', @date AS '@date';  
  
--Result  
--@datetime               @date  
------------------------- ----------  
--2016-12-21 00:00:00.000 2016-12-21  
```  
  
When the conversion is from **time(n)**, the time component is copied, and the date component is set to '1900-01-01'. When the fractional precision of the **time(n)** value is greater than three digits, the value will be truncated to fit. The following example shows the results of converting a `time(4)` value to a `datetime` value.  
  
```sql
DECLARE @time time(4) = '12:10:05.1237';  
DECLARE @datetime datetime = @time;  
  
SELECT @datetime AS '@datetime', @time AS '@time';  
  
--Result  
--@datetime               @time  
------------------------- -------------  
--1900-01-01 12:10:05.123 12:10:05.1237  
```  
  
When the conversion is from **smalldatetime**, the hours and minutes are copied. The seconds and fractional seconds are set to 0. The following code shows the results of converting a `smalldatetime` value to a `datetime` value.  
  
```sql
DECLARE @smalldatetime smalldatetime = '12-01-16 12:32';  
DECLARE @datetime datetime = @smalldatetime;  
  
SELECT @datetime AS '@datetime', @smalldatetime AS '@smalldatetime';  
  
--Result  
--@datetime               @smalldatetime  
------------------------- -----------------------  
--2016-12-01 12:32:00.000 2016-12-01 12:32:00  
```  
  
When the conversion is from **datetimeoffset(n)**, the date and time components are copied. The time zone is truncated. When the fractional precision of the **datetimeoffset(n)** value is greater than three digits, the value will be truncated. The following example shows the results of converting a `datetimeoffset(4)` value to a `datetime` value.  
  
```sql
DECLARE @datetimeoffset datetimeoffset(4) = '1968-10-23 12:45:37.1234 +10:0';  
DECLARE @datetime datetime = @datetimeoffset;  
  
SELECT @datetime AS '@datetime', @datetimeoffset AS '@datetimeoffset';  
  
--Result  
--@datetime               @datetimeoffset  
------------------------- ------------------------------  
--1968-10-23 12:45:37.123 1968-10-23 12:45:37.1237 +01:0   
```  
  
When the conversion is from **datetime2(n)**, the date and time are copied. When the fractional precision of the **datetime2(n)** value is greater than three digits, the value will be truncated. The following example shows the results of converting a `datetime2(4)` value to a `datetime` value.  
  
```sql
DECLARE @datetime2 datetime2(4) = '1968-10-23 12:45:37.1237';  
DECLARE @datetime datetime = @datetime2;  
  
SELECT @datetime AS '@datetime', @datetime2 AS '@datetime2';  
  
--Result  
--@datetime               @datetime2  
------------------------- ------------------------  
--1968-10-23 12:45:37.123 1968-10-23 12:45:37.1237  
```  
  
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
  
  
