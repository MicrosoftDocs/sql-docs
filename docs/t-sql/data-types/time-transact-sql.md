---
title: "time (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 06/07/2017
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "time_TSQL"
  - "time"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "time [SQL Server], data types"
  - "time [SQL Server]"
  - "date and time [SQL Server], time"
  - "data types [SQL Server], date and time"
  - "time data type [SQL Server]"
ms.assetid: 30a6c681-8190-48e4-94d0-78182290a402
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# time (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Defines a time of a day. The time is without time zone awareness and is based on a 24-hour clock.  
  
  > [!NOTE]  
  > Informatica information is provided for PDW customers using the Informatica Connector. 
  
## time Description  
  
|Property|Value|  
|--------------|-----------|  
|Syntax|**time** [ (*fractional second scale*) ]|  
|Usage|DECLARE \@MyTime **time(7)**<br /><br /> CREATE TABLE Table1 ( Column1 **time(7)** )|  
|*fractional seconds scale*|Specifies the number of digits for the fractional part of the seconds.<br /><br /> This can be an integer from 0 to 7. For Informatica, this can be an integer from 0 to 3.<br /><br /> The default fractional scale is 7 (100ns).|  
|Default string literal format<br /><br /> (used for down-level client)|hh:mm:ss[.nnnnnnn] for Informatica)<br /><br /> For more information, see the [Backward Compatibility for Down-level Clients](#BackwardCompatibilityforDownlevelClients) section.|  
|Range|00:00:00.0000000 through 23:59:59.9999999 (00:00:00.000 through 23:59:59.999 for Informatica)|  
|Element ranges|hh is two digits, ranging from 0 to 23, that represent the hour.<br /><br /> mm is two digits, ranging from 0 to 59, that represent the minute.<br /><br /> ss is two digits, ranging from 0 to 59, that represent the second.<br /><br /> n\* is zero to seven digits, ranging from 0 to 9999999, that represent the fractional seconds. For Informatica, n\* is zero to three digits, ranging from 0 to 999.|  
|Character length|8 positions minimum (hh:mm:ss) to 16 maximum (hh:mm:ss.nnnnnnn). For Informatica, the maximum is 12 (hh:mm:ss.nnn).|  
|Precision, scale<br /><br /> (user specifies scale only)|See the table below.|  
|Storage size|5 bytes, fixed, is the default with the default of 100ns fractional second precision. In Informatica, the default is 4 bytes, fixed, with the default of 1ms fractional second precision.|  
|Accuracy|100 nanoseconds (1 millisecond in Informatica)|  
|Default value|00:00:00<br /><br /> This value is used for the appended time part for implicit conversion from **date** to **datetime2** or **datetimeoffset**.|  
|User-defined fractional second precision|Yes|  
|Time zone offset aware and preservation|No|  
|Daylight saving aware|No|  
  
|Specified scale|Result (precision, scale)|Column length (bytes)|Fractional<br /><br /> seconds<br /><br /> precision|  
|---------------------|---------------------------------|-----------------------------|------------------------------------------|  
|**time**|(16,7) [(12,3) in Informatica]|5 (4 in Informatica)|7 (3 in Informatica)|  
|**time(0)**|(8,0)|3|0-2|  
|**time(1)**|(10,1)|3|0-2|  
|**time(2)**|(11,2)|3|0-2|  
|**time(3)**|(12,3)|4|3-4|  
|**time(4)**<br /><br /> Not supported in Informatica.|(13,4)|4|3-4|  
|**time(5)**<br /><br /> Not supported in Informatica.|(14,5)|5|5-7|  
|**time(6)**<br /><br /> Not supported in Informatica.|(15,6)|5|5-7|  
|**time(7)**<br /><br /> Not supported in Informatica.|(16,7)|5|5-7|  
  
## Supported String Literal Formats for time  
 The following table shows the valid string literal formats for the **time** data type.  
  
|SQL Server|Description|  
|----------------|-----------------|  
|hh:mm[:ss][:fractional seconds][AM][PM]<br /><br /> hh:mm[:ss][.fractional seconds][AM][PM]<br /><br /> hhAM[PM]<br /><br /> hh AM[PM]|The hour value of 0 represents the hour after midnight (AM), regardless of whether AM is specified. PM cannot be specified when the hour equals 0.<br /><br /> Hour values from 01 through 11 represent the hours before noon if neither AM nor PM is specified. The values represent the hours before noon when AM is specified. The values represent hours after noon if PM is specified.<br /><br /> The hour value 12 represents the hour that starts at noon if neither AM nor PM is specified. If AM is specified, the value represents the hour that starts at midnight. If PM is specified, the value represents the hour that starts at noon. For example, 12:01 is 1 minute after noon, as is 12:01 PM; and 12:01 AM is one minute after midnight. Specifying 12:01 AM is the same as specifying 00:01 or 00:01 AM.<br /><br /> Hour values from 13 through 23 represent hours after noon if AM or PM is not specified. The values also represent the hours after noon when PM is specified. AM cannot be specified when the hour value is from 13 through 23.<br /><br /> An hour value of 24 is not valid. To represent midnight, use 12:00 AM or 00:00.<br /><br /> Milliseconds can be preceded by either a colon (:) or a period (.). If a colon is used, the number means thousandths-of-a-second. If a period is used, a single digit means tenths-of-a-second, two digits mean hundredths-of-a-second, and three digits mean thousandths-of-a-second. For example, 12:30:20:1 indicates 20 and one-thousandth seconds past 12:30; 12:30:20.1 indicates 20 and one-tenth seconds past 12:30.|  
  
|ISO 8601|Notes|  
|--------------|-----------|  
|hh:mm:ss<br /><br /> hh:mm[:ss][.fractional seconds]|hh is two digits, ranging from 0 to 14, that represent the number of hours in the time zone offset.<br /><br /> mm is two digits, ranging from 0 to 59, that represent the number of additional minutes in the time zone offset.|  
  
|ODBC|Notes|  
|----------|-----------|  
|{t 'hh:mm:ss[.fractional seconds]'}|ODBC API specific.|  
  
## Compliance with ANSI and ISO 8601 Standards  
 Using hour 24 to represent midnight and leap second over 59 as defined by ISO 8601 (5.3.2 and 5.3) are not supported to be backward compatible and consistent with the existing date and time types.  
  
 The default string literal format (used for down-level client) will align with the SQL standard form, which is defined as hh:mm:ss[.nnnnnnn]. This format resembles the ISO 8601 definition for TIME excluding fractional seconds.  
  
##  <a name="BackwardCompatibilityforDownlevelClients"></a> Backward Compatibility for Down-level Clients  
 Some down-level clients do not support the **time**, **date**, **datetime2** and **datetimeoffset** data types. The following table shows the type mapping between an up-level instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and down-level clients.  
  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type|Default string literal format passed to down-level client|Down-level ODBC|Down-level OLEDB|Down-level JDBC|Down-level SQLCLIENT|  
|-----------------------------------------|----------------------------------------------------------------|----------------------|-----------------------|----------------------|---------------------------|  
|**time**|hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**date**|YYYY-MM-DD|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetime2**|YYYY-MM-DD hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetimeoffset**|YYYY-MM-DD hh:mm:ss[.nnnnnnn] [+&#124;-]hh:mm|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
  
## Converting Date and Time Data  
 When you convert to date and time data types, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it cannot recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
### Converting time(n) Data Type to Other Date and Time Types  
 This section describes what occurs when a **time** data type is converted to other date and time data types.  
  
 When the conversion is to **time(n)**, the hour, minute, and seconds are copied. When the destination precision is less than the source precision, the fractional seconds is rounded up to fit the destination precision. The following example shows the results of converting a `time(4)` value to a `time(3)` value.  
  
```  
DECLARE @timeFrom time(4) = '12:34:54.1237';  
DECLARE @timeTo time(3) = @timeFrom;  
  
SELECT @timeTo AS 'time(3)', @timeFrom AS 'time(4)';  
  
--Results  
--time(3)      time(4)  
-------------- -------------  
--12:34:54.124 12:34:54.1237  
--  
--(1 row(s) affected)  
```  
  
 If the conversion is to **date**, the conversion fails, and error message 206 is raised: "Operand type clash: date is incompatible with time".  
  
 When the conversion is to **datetime**, hour, minute, and second values are copied; and the date component is set to '1900-01-01'. When the fractional seconds precision of the **time(n)** value is greater than three digits, the **datetime** result will be truncated. The following code shows the results of converting a `time(4)` value to a `datetime` value.  
  
```  
DECLARE @time time(4) = '12:15:04.1237';  
DECLARE @datetime datetime= @time;  
SELECT @time AS '@time', @datetime AS '@datetime';  
  
--Result  
--@time         @datetime  
--------------- -----------------------  
--12:15:04.1237 1900-01-01 12:15:04.123  
--  
--(1 row(s) affected)  
  
```  
  
 When the conversion is to **smalldatetime**, the date is set to '1900-01-01', and the hour and minute values are rounded up. The seconds and fractional seconds are set to 0. The following code shows the results of converting a `time(4)` value to a `smalldatetime` value.  
  
```  
-- Shows rounding up of the minute value.  
DECLARE @time time(4) = '12:15:59.9999';   
DECLARE @smalldatetime smalldatetime= @time;    
SELECT @time AS '@time', @smalldatetime AS '@smalldatetime';   
  
--Result  
@time            @smalldatetime  
---------------- -----------------------  
12:15:59.9999    1900-01-01 12:16:00--  
--(1 row(s) affected)  
  
-- Shows rounding up of the hour value.  
DECLARE @time time(4) = '12:59:59.9999';   
DECLARE @smalldatetime smalldatetime= @time;    
  
SELECT @time AS '@time', @smalldatetime AS '@smalldatetime';  
@time            @smalldatetime  
---------------- -----------------------  
12:59:59.9999    1900-01-01 13:00:00  
  
(1 row(s) affected)  
  
```  
  
 If the conversion is to **datetimeoffset(n)**, the date is set to '1900-01-01', and the time is copied. The time zone offset is set to +00:00. When the fractional seconds precision of the **time(n)** value is greater than the precision of the **datetimeoffset(n)** value, the value is rounded up to fit. The following example shows the results of converting a `time(4)` value to a `datetimeoffset(3)` type.  
  
```  
DECLARE @time time(4) = '12:15:04.1237';  
DECLARE @datetimeoffset datetimeoffset(3) = @time;  
  
SELECT @time AS '@time', @datetimeoffset AS '@datetimeoffset';  
  
--Result  
--@time         @datetimeoffset  
--------------- ------------------------------  
--12:15:04.1237 1900-01-01 12:15:04.124 +00:00  
--  
--(1 row(s) affected)  
  
```  
  
 When converting to **datetime2(n)**, the date is set to '1900-01-01', the time component is copied, and the time zone offset is set to 00:00. When the fractional seconds precision of the **datetime2(n)** value is greater than the **time(n)** value, the value will be rounded up to fit.  The following example shows the results of converting a `time(4)` value to a `datetime2(2)` value.  
  
```  
DECLARE @time time(4) = '12:15:04.1237';  
DECLARE @datetime2 datetime2(3) = @time;  
  
SELECT @datetime2 AS '@datetime2', @time AS '@time';  
  
--Result  
--@datetime2              @time  
------------------------- -------------  
--1900-01-01 12:15:04.124 12:15:04.1237  
--  
--(1 row(s) affected)  
```  
  
### Converting String Literals to time(n)  
 Conversions from string literals to date and time types are permitted if all parts of the strings are in valid formats. Otherwise, a runtime error is raised. Implicit conversions or explicit conversions that do not specify a style, from date and time types to string literals will be in the default format of the current session. The following table shows the rules for converting a string literal to the **time** data type.  
  
|Input string literal|Conversion Rule|  
|--------------------------|---------------------|  
|ODBC DATE|ODBC string literals are mapped to the **datetime** data type. Any assignment operation from ODBC DATETIME literals into **time**types will cause an implicit conversion between **datetime** and this type as defined by the conversion rules.|  
|ODBC TIME|See ODBC DATE rule above.|  
|ODBC DATETIME|See ODBC DATE rule above.|  
|DATE only|Default values are supplied.|  
|TIME only|Trivial|  
|TIMEZONE only|Default values are supplied.|  
|DATE + TIME|The TIME part of the input string is used.|  
|DATE + TIMEZONE|Not allowed.|  
|TIME + TIMEZONE|The TIME part of the input string is used.|  
|DATE + TIME + TIMEZONE|The TIME part of local DATETIME will be used.|  
  
## Examples  
  
### A. Comparing date and time Data Types  
 The following example compares the results of casting a string to each **date** and **time** data type.  
  
```  
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
|---------------|------------|  
|**time**|12:35:29. 1234567|  
|**date**|2007-05-08|  
|**smalldatetime**|2007-05-08 12:35:00|  
|**datetime**|2007-05-08 12:35:29.123|  
|**datetime2**|2007-05-08 12:35:29. 1234567|  
|**datetimeoffset**|2007-05-08 12:35:29.1234567 +12:15|  
  
###  <a name="ExampleB"></a> B. Inserting Valid Time String Literals into a time(7) Column  
 The following table lists different string literals that can be inserted into a column of data type **time(7)** with the values that are then stored in that column.  
  
|String literal format type|Inserted string literal|time(7) value that is stored|Description|  
|--------------------------------|-----------------------------|------------------------------------|-----------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|'01:01:01:123AM'|01:01:01.1230000|When a colon (:) comes before fractional seconds precision, scale cannot exceed three positions or an error will be raised.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|'01:01:01.1234567 AM'|01:01:01.1234567|When AM or PM is specified, the time is stored in 24-hour format without the literal AM or PM|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|'01:01:01.1234567 PM'|13:01:01.1234567|When AM or PM is specified, the time is stored in 24-hour format without the literal AM or PM|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|'01:01:01.1234567PM'|13:01:01.1234567|A space before AM or PM is optional.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|'01AM'|01:00:00.0000000|When only the hour is specified, all other values are 0.|  
|SQL Server|'01 AM'|01:00:00.0000000|A space before AM or PM is optional.|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|'01:01:01'|01:01:01.0000000|When fractional seconds precision is not specified, each position that is defined by the data type is 0.|  
|ISO 8601|'01:01:01.1234567'|01:01:01.1234567|To comply with ISO 8601, use 24-hour format, not AM or PM.|  
|ISO 8601|'01:01:01.1234567 +01:01'|01:01:01.1234567|The optional time zone difference (TZD) is allowed in the input but is not stored.|  
  
### C. Inserting Time String Literal into Columns of Each date and time Date Type  
 In the following table the first column shows a time string literal to be inserted into a database table column of the date or time data type shown in the second column. The third column shows the value that will be stored in the database table column.  
  
|Inserted string literal|Column data type|Value that is stored in column|Description|  
|-----------------------------|----------------------|------------------------------------|-----------------|  
|'12:12:12.1234567'|**time(7)**|12:12:12.1234567|If the fractional seconds precision exceeds the value specified for the column, the string will be truncated without error.|  
|'2007-05-07'|**date**|NULL|Any time value will cause the INSERT statement to fail.|  
|'12:12:12'|**smalldatetime**|1900-01-01 12:12:00|Any fractional seconds precision value will cause the INSERT statement to fail.|  
|'12:12:12.123'|**datetime**|1900-01-01 12:12:12.123|Any second precision longer than three positions will cause the INSERT statement to fail.|  
|'12:12:12.1234567'|**datetime2(7)**|1900-01-01 12:12:12.1234567|If the fractional seconds precision exceeds the value specified for the column, the string will be truncated without error.|  
|'12:12:12.1234567'|**datetimeoffset(7)**|1900-01-01 12:12:12.1234567 +00:00|If the fractional seconds precision exceeds the value specified for the column, the string will be truncated without error.|  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  
