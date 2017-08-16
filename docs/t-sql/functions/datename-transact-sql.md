---
title: "DATENAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATENAME_TSQL"
  - "DATENAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dates [SQL Server], functions"
  - "DATENAME function [SQL Server"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "dateparts [SQL Server], datename"
  - "date and time [SQL Server], DATENAME"
  - "comparing dates times [SQL Server]"
  - "dates [SQL Server], dateparts"
ms.assetid: 11855b56-c554-495d-aad4-ba446990153b
caps.latest.revision: 59
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATENAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns a character string that represents the specified *datepart* of the specified *date*
  
For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DATENAME ( datepart , date )  
```  
  
## Arguments  
*datepart*  
Is the part of the *date* to return. The following table lists all valid *datepart* arguments. User-defined variable equivalents are not valid.
  
|*datepart*|Abbreviations|  
|---|---|
|**year**|**yy, yyyy**|  
|**quarter**|**qq, q**|  
|**month**|**mm, m**|  
|**dayofyear**|**dy, y**|  
|**day**|**dd, d**|  
|**week**|**wk, ww**|  
|**weekday**|**dw, w**|  
|**hour**|**hh**|  
|**minute**|**mi, n**|  
|**second**|**ss, s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  
|**nanosecond**|**ns**|  
|**TZoffset**|**tz**|  
|**ISO_WEEK**|**ISOWK, ISOWW**|  
  
*date*  
Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, user-defined variable, or string literal.  
To avoid ambiguity, use four-digit years. For information about two-digit years, see [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).
  
## Return Type  
**nvarchar**
  
## Return Value  
  
-   Each *datepart* and its abbreviations return the same value.  
  
The return value depends on the language environment set by using [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md) and by the [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) of the login. The return value is dependant on [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md) if *date* is a string literal of some formats. SET DATEFORMAT does not affect the return value when the date is a column expression of a date or time data type.
  
When the *date* parameter has a **date** data type argument, the return value depends on the setting specified by using [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md).
  
## TZoffset datepart Argument  
If *datepart* argument is **TZoffset** (**tz**) and the *date* argument has no time zone offset, 0 is returned.
  
## smalldatetime date Argument  
When *date* is [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md), seconds are returned as 00.
  
## Default Returned for a datepart That Is Not in the date Argument  
If the data type of the *date* argument does not have the specified *datepart*, the default for that *datepart* will be returned only when a literal is specified for *date*.
  
For example, the default year-month-day for any **date** data type is 1900-01-01. The following statement has date part arguments for *datepart*, a time argument for *date*, and returns `1900, January, 1, 1, Monday`.
  
```sql
SELECT DATENAME(year, '12:10:30.123')  
    ,DATENAME(month, '12:10:30.123')  
    ,DATENAME(day, '12:10:30.123')  
    ,DATENAME(dayofyear, '12:10:30.123')  
    ,DATENAME(weekday, '12:10:30.123');  
```  
  
If *date* is specified as a variable or table column and the data type for that variable or column does not have the specified *datepart*, error 9810 is returned. The following code example fails because the date part year is not a valid for the **time** data type that is declared for the variable *@t*.
  
```sql
DECLARE @t time = '12:10:30.123';   
SELECT DATENAME(year, @t);  
```  
  
## Remarks  
DATENAME can be used in the select list, WHERE, HAVING, GROUP BY, and ORDER BY clauses.
  
In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], DATENAME implicitly casts string literals as a **datetime2** type. This means that DATENAME does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.
  
## Examples  
The following example returns the date parts for the specified date.
  
`SELECT DATENAME(datepart,'2007-10-30 12:15:32.1234567 +05:10');`
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
|*datepart*|Return value|  
|---|---|
|**year, yyyy, yy**|2007|  
|**quarter, qq, q**|4|  
|**month, mm, m**|October|  
|**dayofyear, dy, y**|303|  
|**day, dd, d**|30|  
|**week, wk, ww**|44|  
|**weekday, dw**|Tuesday|  
|**hour, hh**|12|  
|**minute, n**|15|  
|**second, ss, s**|32|  
|**millisecond, ms**|123|  
|**microsecond, mcs**|123456|  
|**nanosecond, ns**|123456700|  
|**TZoffset, tz**|310|  
|**ISO_WEEK, ISOWK, ISOWW**|44|  
  
[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

The following example returns the date parts for the specified date.
  
```sql
SELECT DATENAME(datepart,'2007-10-30 12:15:32.1234567 +05:10');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
|*datepart*|Return value|  
|---|---|
|**year, yyyy, yy**|2007|  
|**quarter, qq, q**|4|  
|**month, mm, m**|October|  
|**dayofyear, dy, y**|303|  
|**day, dd, d**|30|  
|**week, wk, ww**|44|  
|**weekday, dw**|Tuesday|  
|**hour, hh**|12|  
|**minute, n**|15|  
|**second, ss, s**|32|  
|**millisecond, ms**|123|  
|**microsecond, mcs**|123456|  
|**nanosecond, ns**|123456700|  
|**TZoffset, tz**|310|  
|**ISO_WEEK, ISOWK, ISOWW**|44|  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
  

