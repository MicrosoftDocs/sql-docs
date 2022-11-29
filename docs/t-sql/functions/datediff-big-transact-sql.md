---
title: DATEDIFF_BIG (Transact-SQL)
description: "DATEDIFF_BIG (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "01/12/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATEDIFF_BIG"
  - "DATEDIFF_BIG_TSQL"
helpviewer_keywords:
  - "DATEDIFF_BIG function [SQL Server]"
  - "dates [SQL Server]. functions"
  - "date and time [SQL Server], DATEDIFF_BIG"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
dev_langs:
  - "TSQL"
---

# DATEDIFF_BIG (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

This function returns the count (as a signed big integer value) of the specified *datepart* boundaries crossed between the specified *startdate* and *enddate*.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATEDIFF_BIG ( datepart , startdate , enddate )  
```  

## Arguments
*datepart*  
The part of *startdate* and *enddate* that specifies the type of boundary crossed.

> [!NOTE]
> `DATEDIFF_BIG` will not accept *datepart* values from user-defined variables or as quoted strings.

This table lists all valid *datepart* argument names and abbreviations.
  
|*datepart* name| *datepart* abbreviation|  
|---|---|
|**year**|**yy, yyyy**|  
|**quarter**|**qq, q**|  
|**month**|**mm, m**|  
|**dayofyear**|**dy, y**|  
|**day**|**dd, d**|  
|**week**|**wk, ww**|  
|**hour**|**hh**|  
|**minute**|**mi, n**|  
|**second**|**ss, s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  
|**nanosecond**|**ns**|  

> [!NOTE]
> Each specific *datepart* name and abbreviations for that *datepart* name will return the same value.

*startdate*  
An expression that can resolve to one of the following values:

+ **date**
+ **datetime**
+ **datetimeoffset**
+ **datetime2** 
+ **smalldatetime**
+ **time**

For *date*, `DATEDIFF_BIG` will accept a column expression, expression, string literal, or user-defined variable. A string literal value must resolve to a **datetime**. Use four-digit years to avoid ambiguity issues. `DATEDIFF_BIG` subtracts *startdate* from *enddate*. To avoid ambiguity, use four-digit years. See [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) for information about two-digit years.
  
*enddate*  
See *startdate*.
  
## Return Type  
Signed **bigint**  
  
## Return Value  
Returns the **bigint** difference between the *startdate* and *enddate*, expressed in the boundary set by *datepart*.
  
For a return value out of range for **bigint** (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807), `DATEDIFF_BIG` returns an error. Unlike  , which returns an **int** and therefore may overflow a **minute** or higher, `DATEDIFF_BIG` can only overflow if using **nanosecond** precision where the difference between *enddate* and *startdate* is more than 292 years, 3 months, 10 days, 23 hours, 47 minutes, and 16.8547758 seconds.
  
If *startdate* and *enddate* are both assigned only a time value, and the *datepart* isn't a time *datepart*, `DATEDIFF_BIG` returns 0.
  
`DATEDIFF_BIG` does use a time zone offset component of *startdate* or *enddate* to calculate the return value.
  
For a **smalldatetime** value used for *startdate* or *enddate*, `DATEDIFF_BIG` always sets seconds and milliseconds to 0 in the return value because [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) only has accuracy to the minute.
  
If only a time value is assigned to a date data type variable, `DATEDIFF_BIG` sets the value of the missing date part to the default value: `1900-01-01`. If only a date value is assigned to a variable of a time or date data type, `DATEDIFF_BIG` sets the value of the missing time part to the default value: `00:00:00`. If either *startdate* or *enddate* have only a time part and the other only a date part, `DATEDIFF_BIG` sets the missing time and date parts to the default values.
  
If *startdate* and *enddate* have different date data types, and one has more time parts or fractional seconds precision than the other, `DATEDIFF_BIG` sets the missing parts of the other to 0.
  
## datepart boundaries
The following statements have the same *startdate* and the same *enddate* values. Those dates are adjacent and they differ in time by one hundred nanoseconds (.0000001 second). The difference between the *startdate* and *enddate* in each statement crosses one calendar or time boundary of its *datepart*. Each statement returns 1. If *startdate* and *enddate* have different year values but they have the same calendar week values, `DATEDIFF_BIG` will return 0 for *datepart* **week**.

```sql
SELECT DATEDIFF_BIG(year,        '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(quarter,     '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(month,       '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(dayofyear,   '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(day,         '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(week,        '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(hour,        '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(minute,      '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(second,      '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF_BIG(millisecond, '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
```
  
## Remarks  
Use `DATEDIFF_BIG` in the `SELECT <list>`, `WHERE`, `HAVING`, `GROUP BY` and `ORDER BY` clauses.
  
`DATEDIFF_BIG` implicitly casts string literals as a **datetime2** type. This means that `DATEDIFF_BIG` doesn't support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.
  
Specifying `SET DATEFIRST` has no effect on `DATEDIFF_BIG`. `DATEDIFF_BIG` always uses Sunday as the first day of the week to ensure the function operates in a deterministic way.

`DATEDIFF_BIG` may overflow with a **nanosecond** if the difference between *enddate* and *startdate* returns a value that is out of range for **bigint**.
  
## Examples 
  
### Specifying columns for startdate and enddate  
This example uses different types of expressions as arguments for the *startdate* and *enddate* parameters. It calculates the number of day boundaries crossed between dates in two columns of a table.
  
```sql
CREATE TABLE dbo.Duration  
    (startDate datetime2, endDate datetime2);  
    
INSERT INTO dbo.Duration(startDate,endDate)  
    VALUES('2007-05-06 12:10:09', '2007-05-07 12:10:09');  
    
SELECT DATEDIFF_BIG(day, startDate, endDate) AS 'Duration'  
    FROM dbo.Duration;  
-- Returns: 1  
```  

### Finding difference between startdate and enddate as date parts strings

```sql
DECLARE @date1 DATETIME2, @date2 DATETIME2, @result VARCHAR(100)
DECLARE @years BIGINT, @months BIGINT, @days BIGINT, @hours BIGINT, @minutes BIGINT, @seconds BIGINT, @milliseconds BIGINT

SET @date1 = '0001-01-01 00:00:00.00000000'
SET @date2 = '2018-12-12 07:08:01.12345678'

SELECT @years = DATEDIFF(yy, @date1, @date2)
IF DATEADD(yy, -@years, @date2) < @date1 
SELECT @years = @years-1
SET @date2 = DATEADD(yy, -@years, @date2)

SELECT @months = DATEDIFF(mm, @date1, @date2)
IF DATEADD(mm, -@months, @date2) < @date1 
SELECT @months=@months-1
SET @date2= DATEADD(mm, -@months, @date2)

SELECT @days=DATEDIFF(dd, @date1, @date2)
IF DATEADD(dd, -@days, @date2) < @date1 
SELECT @days=@days-1
SET @date2= DATEADD(dd, -@days, @date2)

SELECT @hours=DATEDIFF(hh, @date1, @date2)
IF DATEADD(hh, -@hours, @date2) < @date1 
SELECT @hours=@hours-1
SET @date2= DATEADD(hh, -@hours, @date2)

SELECT @minutes=DATEDIFF(mi, @date1, @date2)
IF DATEADD(mi, -@minutes, @date2) < @date1 
SELECT @minutes=@minutes-1
SET @date2= DATEADD(mi, -@minutes, @date2)

SELECT @seconds=DATEDIFF(s, @date1, @date2)
IF DATEADD(s, -@seconds, @date2) < @date1 
SELECT @seconds=@seconds-1
SET @date2= DATEADD(s, -@seconds, @date2)

SELECT @milliseconds=DATEDIFF(ms, @date1, @date2)

SELECT @result= ISNULL(CAST(NULLIF(@years,0) AS VARCHAR(10)) + ' years,','')
     + ISNULL(' ' + CAST(NULLIF(@months,0) AS VARCHAR(10)) + ' months,','')    
     + ISNULL(' ' + CAST(NULLIF(@days,0) AS VARCHAR(10)) + ' days,','')
     + ISNULL(' ' + CAST(NULLIF(@hours,0) AS VARCHAR(10)) + ' hours,','')
     + ISNULL(' ' + CAST(@minutes AS VARCHAR(10)) + ' minutes and','')
     + ISNULL(' ' + CAST(@seconds AS VARCHAR(10)) 
          + CASE WHEN @milliseconds > 0 THEN '.' + CAST(@milliseconds AS VARCHAR(10)) 
               ELSE '' END 
          + ' seconds','')

SELECT @result
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)] 

```
2017 years, 11 months, 11 days, 7 hours, 8 minutes and 1.123 seconds
```   

See more closely related examples in [DATEDIFF &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-transact-sql.md).
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[DATEDIFF &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-transact-sql.md)
  
  
