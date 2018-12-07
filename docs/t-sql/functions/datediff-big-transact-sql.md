---
title: "DATEDIFF_BIG (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
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
ms.assetid: 19ac1693-3cfa-400d-bf83-20a9cb46599a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# DATEDIFF_BIG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

This function returns the count (as a signed big integer value) of the specified *datepart* boundaries crossed between the specified *startdate* and *enddate*.
  
See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
  
DATEDIFF_BIG ( datepart , startdate , enddate )  
```  
  
## Arguments  
*datepart*  
The part of *startdate* and *enddate* that specifies the type of boundary crossed. `DATEDIFF_BIG` will not accept user-defined variable equivalents. This table lists all valid *datepart* arguments.

> [!NOTE]
> `DATEDIFF_BIG` does not accept user-defined variable equivalents for the *datepart* arguments.
  
|*datepart*|Abbreviations|  
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
Returns the count (as a signed bigint value) of the specified datepart boundaries crossed between the specified startdate and enddate.
-   Each specific *datepart* and the abbreviations for that *datepart* will return the same value.  
  
For a return value out of range for **bigint** (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807), `DATEDIFF_BIG` returns an error. For **nanosecond**, the maximum difference between *enddate* and *startdate* is approximately 292.27 years.
  
If *startdate* and *enddate* are both assigned only a time value, and the *datepart* is not a time *datepart*, `DATEDIFF_BIG` returns 0.
  
`DATEDIFF_BIG` does not use a time zone offset component of *startdate* or *enddate* to calculate the return value.
  
For a **smalldatetime** value used for *startdate* or *enddate*, `DATEDIFF_BIG` always sets seconds and milliseconds to 0 in the return value because [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) only has accuracy to the minute.
  
If only a time value is assigned to a date data type variable, `DATEDIFF_BIG` sets the value of the missing date part to the default value: 1900-01-01. If only a date value is assigned to a variable of a time or date data type, `DATEDIFF_BIG` sets the value of the missing time part to the default value: 00:00:00. If either *startdate* or *enddate* have only a time part and the other only a date part, `DATEDIFF_BIG` sets the missing time and date parts to the default values.
  
If *startdate* and *enddate* have different date data types, and one has more time parts or fractional seconds precision than the other, `DATEDIFF_BIG` sets the missing parts of the other to 0.
  
## datepart boundaries
The following statements have the same *startdate* and the same *enddate* values. Those dates are adjacent and they differ in time by .0000001 second. The difference between the *startdate* and *enddate* in each statement crosses one calendar or time boundary of its *datepart*. Each statement returns 1. If *startdate* and *enddate* have different year values but they have the same calendar week values, `DATEDIFF_BIG` will return 0 for *datepart* **week**.

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
Use `DATEDIFF_BIG` in the SELECT <list>, WHERE, HAVING, GROUP BY and ORDER BY clauses.
  
`DATEDIFF_BIG` implicitly casts string literals as a **datetime2** type. This means that `DATEDIFF_BIG` does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.
  
Specifying SET DATEFIRST has no effect on `DATEDIFF_BIG`. `DATEDIFF_BIG` always uses Sunday as the first day of the week to ensure the function operates in a deterministic way.
  
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

See more closely related examples in [DATEDIFF &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-transact-sql.md).
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[DATEDIFF &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-transact-sql.md)
  
  
