---
title: "DATEPART (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/29/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATEPART_TSQL"
  - "DATEPART"
dev_langs: 
  - "TSQL"
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
ms.assetid: 15f1a5bc-4c0c-4c48-848d-8ec03473e6c1
caps.latest.revision: 57
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATEPART (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns an integer that represents the specified *datepart* of the specified *date*.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DATEPART ( datepart , date )  
```  
  
## Arguments  
 *datepart*  
 Is the part of *date* (a date or time value) for which an **integer** will be returned. The following table lists all valid *datepart* arguments. User-defined variable equivalents are not valid.  
  
|*datepart*|Abbreviations|  
|----------------|-------------------|  
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
|**TZoffset**|**tz**|  
|**ISO_WEEK**|**isowk**, **isoww**|  
  
 *date*  
 Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, user-defined variable, or string literal.  
  
 To avoid ambiguity, use four-digit years. For information about two digits years, see [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).  
  
## Return Type  
 **int**  
  
## Return Value  
 Each *datepart* and its abbreviations return the same value.  
  
 The return value depends on the language environment set by using [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md) and by the [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) of the login. If *date* is a string literal for some formats, the return value depends on the format specified by using [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md). SET DATEFORMAT does not affect the return value when the date is a column expression of a date or time data type.  
  
 The following table lists all *datepart* arguments with corresponding return values for the statement `SELECT DATEPART(datepart,'2007-10-30 12:15:32.1234567 +05:10')`. The data type of the *date* argument is **datetimeoffset(7)**. The **nanosecond***datepart* return value has a scale of 9 (.123456700) and the last two positions are always 00.  
  
|*datepart*|Return value|  
|----------------|------------------|  
|**year, yyyy, yy**|2007|  
|**quarter, qq, q**|4|  
|**month, mm, m**|10|  
|**dayofyear, dy, y**|303|  
|**day, dd, d**|30|  
|**week, wk, ww**|45|  
|**weekday, dw**|1|  
|**hour, hh**|12|  
|**minute, n**|15|  
|**second, ss, s**|32|  
|**millisecond, ms**|123|  
|**microsecond, mcs**|123456|  
|**nanosecond, ns**|123456700|  
|**TZoffset, tz**|310|  
  
## week and weekday datepart Arguments  
 When *datepart* is **week** (**wk**, **ww**) or **weekday** (**dw**), the return value depends on the value that is set by using [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md).  
  
 January 1 of any year defines the starting number for the **week***datepart*, for example: DATEPART (**wk**, 'Jan 1, *xxx*x') = 1, where *xxxx* is any year.  
  
 The following table lists the return value for **week** and **weekday***datepart* for '2007-04-21 ' for each SET DATEFIRST argument. January 1 is a Monday in the year 2007. April 21 is a Saturday in the year 2007. SET DATEFIRST 7, Sunday, is the default for U.S. English.  
  
|SET DATEFIRST<br /><br /> argument|week<br /><br /> returned|weekday<br /><br /> returned|  
|--------------------------------|-----------------------|--------------------------|  
|1|16|6|  
|2|17|5|  
|3|17|4|  
|4|17|3|  
|5|17|2|  
|6|17|1|  
|7|16|7|  
  
## year, month, and day datepart Arguments  
 The values that are returned for DATEPART (**year**, *date*), DATEPART (**month**, *date*), and DATEPART (**day**, *date*) are the same as those returned by the functions [YEAR](../../t-sql/functions/year-transact-sql.md), [MONTH](../../t-sql/functions/month-transact-sql.md), and [DAY](../../t-sql/functions/day-transact-sql.md), f respectively.  
  
## ISO_WEEK datepart  
 ISO 8601 includes the ISO week-date system, a numbering system for weeks. Each week is associated with the year in which Thursday occurs. For example, week 1 of 2004 (2004W01) ran from Monday 29 December 2003 to Sunday, 4 January 2004. The highest week number in a year might be 52 or 53. This style of numbering is typically used in European countries/regions, but rare elsewhere.  
  
 The numbering system in different countries/regions might not comply with the ISO standard. There are at least six possibilities as shown in the following table  
  
|First day of week|First week of year contains|Weeks assigned two times|Used by/in|  
|-----------------------|---------------------------------|------------------------------|-----------------|  
|Sunday|1 January,<br /><br /> First Saturday,<br /><br /> 1–7 days of year|Yes|United States|  
|Monday|1 January,<br /><br /> First Sunday,<br /><br /> 1–7 days of year|Yes|Most of Europe and the United Kingdom|  
|Monday|4 January,<br /><br /> First Thursday,<br /><br /> 4–7 days of year|No|ISO 8601, Norway, and Sweden|  
|Monday|7 January,<br /><br /> First Monday,<br /><br /> 7 days of year|No||  
|Wednesday|1 January,<br /><br /> First Tuesday,<br /><br /> 1–7 days of year|Yes||  
|Saturday|1 January,<br /><br /> First Friday,<br /><br /> 1–7 days of year|Yes||  
  
## TZoffset  
 The **TZoffset** (**tz**) is returned as the number of minutes (signed). The following statement returns a time zone offset of 310 minutes.  
  
```  
SELECT DATEPART (TZoffset, '2007-05-10  00:00:01.1234567 +05:10');  
```  
  
- For datetimeoffset and datetime2, TZoffset returns the time offset in minutes, where the offset for datetime2 is always 0 minutes.
- For data types that can be implicitly converted to datetimeoffset or datetime2, with the exception of the other date/time data types, it returns the time offset in minutes.
- Parameters of all other types result in an error.
  
  
## smalldatetime date Argument  
 When *date* is [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md), seconds are returned as 00.  
  
## Default Returned for a datepart That Is Not in a date Argument  
 If the data type of the *date* argument does not have the specified *datepart*, the default for that *datepart* will be returned only when a literal is specified for *date*.  
  
 For example, the default year-month-day for any **date** data type is 1900-01-01. The following statement has date part arguments for *datepart*, a time argument for *date*, and returns `1900, 1, 1, 1, 2`.  
  
```  
SELECT DATEPART(year, '12:10:30.123')  
    ,DATEPART(month, '12:10:30.123')  
    ,DATEPART(day, '12:10:30.123')  
    ,DATEPART(dayofyear, '12:10:30.123')  
    ,DATEPART(weekday, '12:10:30.123');  
```  
  
 If *date* is specified as a variable or table column and the data type for that variable or column does not have the specified *datepart*, error 9810 is returned. The following code example fails because the date part year is not a valid for the **time** data type that is declared for the variable *@t*.  
  
```  
DECLARE @t time = '12:10:30.123';   
SELECT DATEPART(year, @t);  
  
```  
  
## Fractional Seconds  
 Fractional seconds are returned as shown in the following statements:  
  
```  
SELECT DATEPART(millisecond, '00:00:01.1234567'); -- Returns 123  
SELECT DATEPART(microsecond, '00:00:01.1234567'); -- Returns 123456  
SELECT DATEPART(nanosecond,  '00:00:01.1234567'); -- Returns 123456700  
```  
  
## Remarks  
 DATEPART can be used in the select list, WHERE, HAVING, GROUP BY and ORDER BY clauses.  
  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], DATEPART implicitly casts string literals as a **datetime2** type. This means that DATEPART does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.  
  
## Examples  
 The following example returns the base year. The base year is useful for date calculations. In the example, the date is specified as a number. Notice that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] interprets 0 as January 1, 1900.  
  
```  
SELECT DATEPART(year, 0), DATEPART(month, 0), DATEPART(day, 0);  
-- Returns: 1900    1    1 */  
```  
  
 The following example returns the day part of the date `12/20/1974`.  
  
```  
-- Uses AdventureWorks  
  
SELECT TOP(1) DATEPART (day,'12/20/1974') FROM dbo.DimCustomer;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `--------`  
  
 `20`  
  
 The following example returns the year part of the date `12/20/1974`.  
  
```  
-- Uses AdventureWorks  
  
SELECT TOP(1) DATEPART (year,'12/20/1974') FROM dbo.DimCustomer;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `--------`  
  
 `1974`  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  
