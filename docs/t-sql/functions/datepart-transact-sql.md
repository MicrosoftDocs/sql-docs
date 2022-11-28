---
title: "DATEPART (Transact-SQL)"
description: "Transact-SQL reference for the DATEPART function. This function returns an integer corresponding to the datepart of a specified date."
author: markingmyname
ms.author: maghan
ms.reviewer: "derekw"
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATEPART_TSQL"
  - "DATEPART"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "date and time [SQL Server], DATEPART"
  - "dateparts [SQL Server]"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dateparts [SQL Server], datepart"
  - "comparing dates times [SQL Server]"
  - "DATEPART function [SQL Server]"
  - "dates [SQL Server], dateparts"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# DATEPART (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns an integer representing the specified *datepart* of the specified *date*.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATEPART ( datepart , date )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*datepart*  
The specific part of the *date* argument for which `DATEPART` will return an **integer**. This table lists all valid *datepart* arguments.

> [!NOTE]
> `DATEPART` does not accept user-defined variable equivalents for the *datepart* arguments.
  
|*datepart*|Abbreviations|  
|---|---|
|**year**|**yy**, **yyyy**|  
|**quarter**|**qq**, **q**|  
|**month**|**mm**, **m**|  
|**dayofyear**|**dy**, **y**|  
|**day**|**dd**, **d**|  
|**week**|**wk**, **ww**|  
|**weekday**|**dw**|  
|**hour**|**hh**|  
|**minute**|**mi, n**|  
|**second**|**ss**, **s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  
|**nanosecond**|**ns**|  
|**tzoffset**|**tz**|  
|**iso_week**|**isowk**, **isoww**|  
  
*date*  
An expression that resolves to one of the following data types: 

- **date**
- **datetime**
- **datetimeoffset**
- **datetime2**
- **smalldatetime**
- **time**

For *date*, `DATEPART` will accept a column expression, expression, string literal, or user-defined variable. Use four-digit years to avoid ambiguity issues. See [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) for information about two-digit years.
  
## Return Type

#### int  
  
## Return Value

Each *datepart* and its abbreviations return the same value.
  
The return value depends on the language environment set by using [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md), and by the [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) of the login. The return value depends on [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md) if *date* is a string literal of some formats. SET DATEFORMAT doesn't change the return value when the date is a column expression of a date or time data type.
  
This table lists all *datepart* arguments, with corresponding return values, for the statement `SELECT DATEPART(datepart,'2007-10-30 12:15:32.1234567 +05:10')`. The *date* argument has a **datetimeoffset(7)** data type. The last two positions of the **nanosecond** *datepart* return value are always `00` and this value has a scale of 9:

**.123456700**  
  
|*datepart*|Return value|  
|---|---|
|**year, yyyy, yy**|2007|  
|**quarter, qq, q**|4|  
|**month, mm, m**|10|  
|**dayofyear, dy, y**|303|  
|**day, dd, d**|30|  
|**week, wk, ww**|44|  
|**weekday, dw**|3|  
|**hour, hh**|12|  
|**minute, n**|15|  
|**second, ss, s**|32|  
|**millisecond, ms**|123|  
|**microsecond, mcs**|123456|  
|**nanosecond, ns**|123456700|  
|**tzoffset, tz**|310|  
|**iso_week, isowk, isoww**|44|  
  
## Week and weekday datepart arguments

For a **week** (**wk**, **ww**) or **weekday** (**dw**) *datepart*, the `DATEPART` return value depends on the value set by [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md).
  
January 1 of any year defines the starting number for the **week** _datepart_. For example:

DATEPART (**wk**, 'Jan 1, *xxx*x') = 1

where *xxxx* is any year.
  
This table shows the return value for the **week** and **weekday** *datepart* for '2007-04-21 ' for each SET DATEFIRST argument. 
January 1, 2007 falls on a Monday. April 21, 2007 falls on a Saturday. For U.S. English,

`SET DATEFIRST 7 -- ( Sunday )`

serves as the default. After setting DATEFIRST, use this suggested SQL statement for the datepart table values:

`SELECT DATEPART(week, '2007-04-21 '), DATEPART(weekday, '2007-04-21 ')`
  
|SET DATEFIRST<br /><br /> argument|week<br /><br /> returned|weekday<br /><br /> returned|  
|---|---|---|
|1|16|6|  
|2|17|5|  
|3|17|4|  
|4|17|3|  
|5|17|2|  
|6|17|1|  
|7|16|7|  
  
## year, month, and day datepart Arguments

The values that are returned for DATEPART (**year**, *date*), DATEPART (**month**, *date*), and DATEPART (**day**, *date*) are the same as those returned by the functions [YEAR](../../t-sql/functions/year-transact-sql.md), [MONTH](../../t-sql/functions/month-transact-sql.md), and [DAY](../../t-sql/functions/day-transact-sql.md), respectively.
  
## iso_week datepart

ISO 8601 includes the ISO week-date system, a numbering system for weeks. Each week is associated with the year in which Thursday occurs. For example, week 1 of 2004 (2004W01) covered Monday, 29 December 2003 to Sunday, 4 January 2004. European countries/regions typically use this style of numbering. Non-European countries/regions typically don't use it.

Note: the highest week number in a year could be either 52 or 53.
  
The numbering systems of different countries/regions might not comply with the ISO standard. This table shows six possibilities:
  
|First day of week|First week of year contains|Weeks assigned two times|Used by/in|  
|---|---|---|---|
|Sunday|1 January,<br /><br /> First Saturday,<br /><br /> 1-7 days of year|Yes|United States|  
|Monday|1 January,<br /><br /> First Sunday,<br /><br /> 1-7 days of year|Yes|Most of Europe and the United Kingdom|  
|Monday|4 January,<br /><br /> First Thursday,<br /><br /> 4-7 days of year|No|ISO 8601, Norway, and Sweden|  
|Monday|7 January,<br /><br /> First Monday,<br /><br /> Seven days of year|No||  
|Wednesday|1 January,<br /><br /> First Tuesday,<br /><br /> 1-7 days of year|Yes||  
|Saturday|1 January,<br /><br /> First Friday,<br /><br /> 1-7 days of year|Yes||  
  
## tzoffset

`DATEPART` returns the **tzoffset** (**tz**) value as the number of minutes (signed). This statement returns a time zone offset of 310 minutes:
  
```sql
SELECT DATEPART (tzoffset, '2007-05-10  00:00:01.1234567 +05:10');  
```

`DATEPART` renders the tzoffset value as follows:

- For datetimeoffset and datetime2, tzoffset returns the time offset in minutes, where the offset for datetime2 is always 0 minutes.
- For data types that can implicitly convert to **datetimeoffset** or **datetime2**, `DATEPART` returns the time offset in minutes. Exception: other date / time data types.
- Parameters of all other types result in an error.
  
## smalldatetime date Argument  

For a [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) *date* value, `DATEPART` returns seconds as 00.
  
## Default Returned for a datepart That Isn't in a date Argument

If the *date* argument data type doesn't have the specified *datepart*, `DATEPART` will return the default for that *datepart* only when a literal is specified for *date*.
  
For example, the default year-month-day for any **date** data type is 1900-01-01. This statement has date part arguments for *datepart*, a time argument for *date*, and it returns `1900, 1, 1, 1, 2`.
  
```sql
SELECT DATEPART(year, '12:10:30.123')  
    ,DATEPART(month, '12:10:30.123')  
    ,DATEPART(day, '12:10:30.123')  
    ,DATEPART(dayofyear, '12:10:30.123')  
    ,DATEPART(weekday, '12:10:30.123');  
```  
  
If *date* is specified as a variable or table column, and the data type for that variable or column doesn't have the specified *datepart*, `DATEPART` will return error 9810. In this example, variable *\@t* has a **time** data type. The example fails because the date part year is invalid for the **time** data type:
  
```sql
DECLARE @t time = '12:10:30.123';   
SELECT DATEPART(year, @t);  
```

## Fractional seconds

These statements show that `DATEPART` returns fractional seconds:
  
```sql
SELECT DATEPART(millisecond, '00:00:01.1234567'); -- Returns 123  
SELECT DATEPART(microsecond, '00:00:01.1234567'); -- Returns 123456  
SELECT DATEPART(nanosecond,  '00:00:01.1234567'); -- Returns 123456700  
```

## Remarks

`DATEPART` can be used in the select list, WHERE, HAVING, GROUP BY, and ORDER BY clauses.
  
DATEPART implicitly casts string literals as a **datetime2** type in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later. This means that DATENAME doesn't support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.
  
## Examples

This example returns the base year. The base year helps with date calculations. In the example, a number specifies the date. Notice that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets 0 as January 1, 1900.
  
```sql
SELECT DATEPART(year, 0), DATEPART(month, 0), DATEPART(day, 0);  

-- Returns: 1900    1    1 
```  
  
This example returns the day part of the date `12/20/1974`.
  
```sql
-- Uses AdventureWorks  
  
SELECT TOP(1) DATEPART (day,'12/20/1974') FROM dbo.DimCustomer;  

-- Returns: 20
```  
  
This example returns the year part of the date `12/20/1974`.
  
```sql
-- Uses AdventureWorks  
  
SELECT TOP(1) DATEPART (year,'12/20/1974') FROM dbo.DimCustomer;  

-- Returns: 1974
```  
  
## See also

[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
[DATETRUNC](../../t-sql/functions/datetrunc-transact-sql.md)
