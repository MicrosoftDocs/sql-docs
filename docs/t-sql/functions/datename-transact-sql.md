---
title: "DATENAME (Transact-SQL)"
description: "DATENAME (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATENAME_TSQL"
  - "DATENAME"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "DATENAME function [SQL Server"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "dateparts [SQL Server], datename"
  - "date and time [SQL Server], DATENAME"
  - "comparing dates times [SQL Server]"
  - "dates [SQL Server], dateparts"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATENAME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns a character string representing the specified *datepart* of the specified *date*.

See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATENAME ( datepart , date )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*datepart*  
The specific part of the *date* argument that `DATENAME` will return. This table lists all valid *datepart* arguments.

> [!NOTE]
> `DATENAME` does not accept user-defined variable equivalents for the *datepart* arguments.
  
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

An expression that can resolve to one of the following data types: 

+ **date**
+ **datetime**
+ **datetimeoffset**
+ **datetime2** 
+ **smalldatetime**
+ **time**

For *date*, `DATENAME` will accept a column expression, expression, string literal, or user-defined variable. Use four-digit years to avoid ambiguity issues. See [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) for information about two-digit years.
  
## Return Type  
**nvarchar**
  
## Return Value  
  
-   Each *datepart* and its abbreviations return the same value.  
  
The return value depends on the language environment set by using [SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md), and by the [Configure the default language Server Configuration Option](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) of the login. The return value depends on [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md) if *date* is a string literal of some formats. SET DATEFORMAT does not change the return value when the date is a column expression of a date or time data type.
  
When the *date* parameter has a **date** data type argument, the return value depends on the setting specified by [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md).
  
## TZoffset datepart Argument  
If the *datepart* argument is **TZoffset** (**tz**) and the *date* argument has no time zone offset, `DATEADD` returns 0.
  
## smalldatetime date Argument  
When *date* is [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md), `DATENAME` returns seconds as 00.
  
## Default Returned for a datepart That Is Not in the date Argument  
If the data type of the *date* argument does not have the specified *datepart*, `DATENAME` will return the default for that *datepart* only if the *date* argument has a literal .
  
For example, the default year-month-day for any **date** data type is 1900-01-01. This statement has date part arguments for *datepart*, a time argument for *date*, and `DATENAME` returns `1900, January, 1, 1, Monday`.
  
```sql
SELECT DATENAME(year, '12:10:30.123')  
    ,DATENAME(month, '12:10:30.123')  
    ,DATENAME(day, '12:10:30.123')  
    ,DATENAME(dayofyear, '12:10:30.123')  
    ,DATENAME(weekday, '12:10:30.123');  
```  
  
If *date* is specified as a variable or table column, and the data type for that variable or column does not have the specified *datepart*, `DATENAME` will return error 9810. In this example, variable *\@t* has a **time** data type. The example fails because the date part year is invalid for the **time** data type:
  
```sql
DECLARE @t time = '12:10:30.123';   
SELECT DATENAME(year, @t);  
```  
  
## Remarks  

Use `DATENAME` in the following clauses:

+ GROUP BY
+ HAVING
+ ORDER BY
+ SELECT \<list>
+ WHERE
  
In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], DATENAME implicitly casts string literals as a **datetime2** type. In other words, `DATENAME` does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.
  
## Examples  
This example returns the date parts for the specified date. Substitute a *datepart* value from the table for the `datepart` argument in the SELECT statement:
  
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
|**TZoffset, tz**|+05:10|  
|**ISO_WEEK, ISOWK, ISOWW**|44|  
  
[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

This example returns the date parts for the specified date. Substitute a *datepart* value from the table for the `datepart` argument in the SELECT statement:
  
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
|**TZoffset, tz**|+05:10|  
|**ISO_WEEK, ISOWK, ISOWW**|44|  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
  

