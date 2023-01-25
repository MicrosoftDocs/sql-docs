---
title: "date (Transact-SQL)"
description: "date (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/23/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "date_TSQL"
  - "date"
helpviewer_keywords:
  - "data types [SQL Server], date"
  - "date and time [SQL Server], date"
  - "dates [SQL Server], data types"
  - "date data type [SQL Server]"
  - "data types [SQL Server], date and time"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# date (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Defines a date in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## date description
  
|Property|Value|  
|--------------|-----------|  
|Syntax|**date**|  
|Usage|DECLARE \@MyDate **date**<br /><br /> CREATE TABLE Table1 ( Column1 **date** )|  
|Default string literal format<br /><br /> (used for down-level client)|YYYY-MM-DD<br /><br /> For more information, see the "Backward Compatibility for Down-level Clients" section that follows.|  
|Range|0001-01-01 through 9999-12-31 (1582-10-15 through 9999-12-31 for Informatica)<br /><br /> January 1, 1 CE (Common Era) through December 31, 9999 CE (October 15, 1582 CE through December 31, 9999 CE for Informatica)|  
|Element ranges|YYYY is four digits from 0001 to 9999 that represent a year. For Informatica, YYYY is limited to the range 1582 to 9999.<br /><br /> MM is two digits from 01 to 12 that represent a month in the specified year.<br /><br /> DD is two digits from 01 to 31, depending on the month, that represents a day of the specified month.|  
|Character length|10 positions|  
|Precision, scale|10, 0|  
|Storage size|3 bytes, fixed|  
|Storage structure|1, 3-byte integer stores date.|  
|Accuracy|One day|  
|Default value|1900-01-01<br /><br /> This value is used for the appended date part for implicit conversion from **time** to **datetime2** or **datetimeoffset**.|  
|Calendar|Gregorian|  
|User-defined fractional second precision|No|  
|Time zone offset aware and preservation|No|  
|Daylight saving aware|No|  
  
## Supported string literal formats for date
The following tables show the valid string literal formats for the **date** data type.
  
|Numeric|Description|  
|-------------|-----------------|  
|mdy<br /><br /> [m]m/dd/[yy]yy<br /><br /> [m]m-dd-[yy]yy<br /><br /> [m]m.dd.[yy]yy<br /><br /> myd<br /><br /> mm/[yy]yy/dd<br /><br /> mm-[yy]yy/dd<br /><br /> [m]m.[yy]yy.dd<br /><br /> dmy<br /><br /> dd/[m]m/[yy]yy<br /><br /> dd-[m]m-[yy]yy<br /><br /> dd.[m]m.[yy]yy<br /><br /> dym<br /><br /> dd/[yy]yy/[m]m<br /><br /> dd-[yy]yy-[m]m<br /><br /> dd.[yy]yy.[m]m<br /><br /> ymd<br /><br /> [yy]yy/[m]m/dd<br /><br /> [yy]yy-[m]m-dd<br /><br /> [yy]yy-[m]m-dd|[m]m, dd, and [yy]yy represent month, day, and year in a string with slash marks (/), hyphens (-), or periods (.) as separators.<br /><br /> Only four- or two-digit years are supported. Use four-digit years whenever possible. To specify an integer from 0001 to 9999 that represents the cutoff year for interpreting two-digit years as four-digit years, use the [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).<br /><br /> **Note:** For Informatica, YYYY is limited to the range 1582 to 9999.<br /><br /> A two-digit year that is less than or equal to the last two digits of the cutoff year is in the same century as the cutoff year. A two-digit year greater than the last two digits of the cutoff year is in the century that comes before the cutoff year. For example, if the two-digit year cutoff is the default 2049, the two-digit year 49 is interpreted as 2049 and the two-digit year 50 is interpreted as 1950.<br /><br /> The default date format is determined by the current language setting. You can change the date format by using the [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md) and [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md) statements.<br /><br /> The **ydm** format isn't supported for **date**.|  
  
|Alphabetical|Description|  
|------------------|-----------------|  
|mon [dd][,] yyyy<br /><br /> mon dd[,] [yy]<br /><br /> mon yyyy [dd]<br /><br /> [dd] mon[,] yyyy<br /><br /> dd mon[,][yy]yy<br /><br /> dd [yy]yy mon<br /><br /> [dd] yyyy mon<br /><br /> yyyy mon [dd]<br /><br /> yyyy [dd] mon|**mon** represents the full month name or the month abbreviation given in the current language. Commas are optional and capitalization is ignored.<br /><br /> To avoid ambiguity, use four-digit years.<br /><br /> If the day is missing, the first day of the month is supplied.|  
  
|ISO 8601|Description|  
|--------------|----------------|  
|YYYY-MM-DD<br /><br /> YYYYMMDD|Same as the SQL standard. This format is the only format defined as an international standard.|  
  
|Unseparated|Description|  
|-----------------|-----------------|  
|[yy]yymmdd<br /><br /> yyyy[mm][dd]|The **date** data can be specified with four, six, or eight digits. A six- or eight-digit string is always interpreted as **ymd**. The month and day must always be two digits. A four-digit string is interpreted as year.|  
  
|ODBC|Description|  
|----------|-----------------|  
|{ d 'yyyy-mm-dd' }|ODBC API specific.|  
  
|W3C XML format|Description|  
|--------------------|-----------------|  
|yyyy-mm-ddTZD|Supported for XML/SOAP usage.<br /><br /> TZD is the time zone designator (Z or +hh:mm or -hh:mm):<br /><br /> -   hh:mm represents the time zone offset. hh is two digits, ranging from 0 to 14, that represent the number of hours in the time zone offset.<br />-   MM is two digits, ranging from 0 to 59, that represent the number of additional minutes in the time zone offset.<br />-   + (plus) or - (minus) the mandatory sign of the time zone offset. This sign indicates that, to obtain the local time, the time zone offset is added or subtracted from the Coordinated Universal Times (UTC) time. The valid range of time zone offset is from -14:00 to +14:00.|  
  
## ANSI and ISO 8601 compliance  
**date** complies with the ANSI SQL standard definition for the Gregorian calendar: "NOTE 85 - Datetime data types will allow dates in the Gregorian format to be stored in the date range 0001-01-01 CE through 9999-12-31 CE."
  
The default string literal format, which is used for down-level clients, complies with the SQL standard form that is defined as YYYY-MM-DD. This format is the same as the ISO 8601 definition for DATE.
  
> [!NOTE]  
>  For Informatica, the range is limited to 1582-10-15 (October 15, 1582 CE) to 9999-12-31 (December 31, 9999 CE).  
  
## Backward compatibility for down-level clients
Some down-level clients don't support the **time**, **date**, **datetime2**, and **datetimeoffset** data types. The following table shows the type mapping between an up-level instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and down-level clients.
  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type|Default string literal format passed to down-level client|Down-level ODBC|Down-level OLEDB|Down-level JDBC|Down-level SQLCLIENT|  
| --- | --- | --- | --- | --- | --- |
|**time**|hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**date**|YYYY-MM-DD|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetime2**|YYYY-MM-DD hh:mm:ss[.nnnnnnn]|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
|**datetimeoffset**|YYYY-MM-DD hh:mm:ss[.nnnnnnn] [+&#124;-]hh:mm|SQL_WVARCHAR or SQL_VARCHAR|DBTYPE_WSTRor DBTYPE_STR|Java.sql.String|String or SqString|  
  
## Converting date and time data
When you convert to date and time data types, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects all values it doesn't recognize as dates or times. For information about using the CAST and CONVERT functions with date and time data, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
### Converting date to other date and time types

This section describes what occurs when you convert a **date** data type to other date and time data types.
  
When the conversion is to **time(n)**, the conversion fails, and error message 206 is raised: "Operand type clash: date is incompatible with time".
  
If the conversion is to **datetime**, date is copied. The following code shows the results of converting a `date` value to a `datetime` value.
  
```sql
DECLARE @date date= '12-10-25';  
DECLARE @datetime datetime= @date;  
  
SELECT @date AS '@date', @datetime AS '@datetime';  
  
--Result  
--@date      @datetime  
------------ -----------------------  
--2025-12-10 2025-12-10 00:00:00.000  
--  
--(1 row(s) affected)  
```  
  
When the conversion is to **smalldatetime**, the **date** value is in the range of a [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md), the date component is copied, and the time component is set to 00:00:00.000. When the **date** value is outside the range of a **smalldatetime** value, error message 242 is raised: "The conversion of a date data type to a smalldatetime data types resulted in an out-of-range value."; and the **smalldatetime** value is set to NULL. The following code shows the results of converting a `date` value to a `smalldatetime` value.
  
```sql
DECLARE @date date= '1912-10-25';  
DECLARE @smalldatetime smalldatetime = @date;  
  
SELECT @date AS '@date', @smalldatetime AS '@smalldatetime';  
  
--Result  
--@date      @smalldatetime  
------------ -----------------------  
--1912-10-25 1912-10-25 00:00:00  
--  
--(1 row(s) affected)  
```  
  
For conversion to **datetimeoffset(n)**, date is copied, and the time is set to 00:00.0000000 +00:00. The following code shows the results of converting a `date` value to a `datetimeoffset(3)` value.
  
```sql
DECLARE @date date = '1912-10-25';  
DECLARE @datetimeoffset datetimeoffset(3) = @date;  
  
SELECT @date AS '@date', @datetimeoffset AS '@datetimeoffset';  
  
--Result  
--@date      @datetimeoffset  
------------ ------------------------------  
--1912-10-25 1912-10-25 00:00:00.000 +00:00  
--  
--(1 row(s) affected)  
```  
  
When the conversion is to **datetime2(n)**, the date component is copied, and the time component is set to 00:00.000000. The following code shows the results of converting a `date` value to a `datetime2(3)` value.
  
```sql
DECLARE @date date = '1912-10-25'  
DECLARE @datetime2 datetime2(3) = @date;  
  
SELECT @date AS '@date', @datetime2 AS '@datetime2(3)';  
  
--Result  
--@date      @datetime2(3)  
------------ -----------------------  
--1912-10-25 1912-10-25 00:00:00.000  
--  
--(1 row(s) affected)  
```  
  
### Converting string literals to date
Conversions from string literals to date and time types are allowed if all parts of the strings are in valid formats. Otherwise, a runtime error is raised. Implicit conversions or explicit conversions that don't specify a style, from date and time types to string literals will be in the default format of the current session. The following table shows the rules for converting a string literal to the **date** data type.
  
|Input string literal|**date**|  
|---|---|
|ODBC DATE|ODBC string literals are mapped to the **datetime** data type. Any assignment operation from ODBC DATETIME literals into a **date** type causes an implicit conversion between **datetime** and the type that the conversion rules define.|  
|ODBC TIME|See previous ODBC DATE rule.|  
|ODBC DATETIME|See previous ODBC DATE rule.|  
|DATE only|Trivial|  
|TIME only|Default values are supplied.|  
|TIMEZONE only|Default values are supplied.|  
|DATE + TIME|The DATE part of the input string is used.|  
|DATE + TIMEZONE|Not allowed.|  
|TIME + TIMEZONE|Default values are supplied.|  
|DATE + TIME + TIMEZONE|The DATE part of local DATETIME will be used.|  
  
## Examples  
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
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
|Data type|Output|  
|---------------|------------|  
|**time**|12:35:29. 1234567|  
|**date**|2007-05-08|  
|**smalldatetime**|2007-05-08 12:35:00|  
|**datetime**|2007-05-08 12:35:29.123|  
|**datetime2**|2007-05-08 12:35:29.1234567|  
|**datetimeoffset**|2007-05-08 12:35:29.1234567 +12:15|  

First introduced in SQL Server 2008.

## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
  
