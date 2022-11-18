---
title: "DATEADD (Transact-SQL)"
description: "Transact-SQL reference for the DATEADD function. This function returns a date that has been modified by the specified date part."
author: markingmyname
ms.author: maghan
ms.date: "07/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATEADD"
  - "DATEADD_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "add interval to date or time [SQL Server]"
  - "subtract interval from date or time [SQL Server]"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], intervals"
  - "date and time [SQL Server], DATEADD"
  - "DATEADD function [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATEADD (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function adds a *number* (a signed integer) to a *datepart* of an input *date*, and returns a modified date/time value. For example, you can use this function to find the date that is 7000 minutes from today: *number* = 7000, *datepart* = minute, *date* = today.

See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATEADD (datepart , number , date )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*datepart*  
The part of *date* to which `DATEADD` adds an **integer** *number*. This table lists all valid *datepart* arguments.

> [!NOTE]
> `DATEADD` does not accept user-defined variable equivalents for the *datepart* arguments. 
  
|*datepart*|Abbreviations|  
|---|---|
|**year**|**yy**, **yyyy**|  
|**quarter**|**qq**, **q**|  
|**month**|**mm**, **m**|  
|**dayofyear**|**dy**, **y**|  
|**day**|**dd**, **d**|  
|**week**|**wk**, **ww**|  
|**weekday**|**dw**, **w**|  
|**hour**|**hh**|  
|**minute**|**mi**, **n**|  
|**second**|**ss**, **s**|  
|**millisecond**|**ms**|  
|**microsecond**|**mcs**|  
|**nanosecond**|**ns**|  
  
*number*  
An expression that can resolve to an [int](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md) that `DATEADD` adds to a *datepart* of *date*. `DATEADD` accepts user-defined variable values for *number*. `DATEADD` will truncate a specified *number* value that has a decimal fraction. It will not round the *number* value in this situation.
  
*date*  
An expression that can resolve to one of the following values: 

+ **date**
+ **datetime**
+ **datetimeoffset**
+ **datetime2** 
+ **smalldatetime**
+ **time**

For *date*, `DATEADD` will accept a column expression, expression, string literal, or user-defined variable. A string literal value must resolve to a **datetime**. Use four-digit years to avoid ambiguity issues. See [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) for information about two-digit years.
  
## Return types

The return value data type for this method is dynamic. The return type depends on the argument supplied for `date`. If the value for `date` is a string literal date, `DATEADD` returns a **datetime** value. If another valid input data type is supplied for `date`, `DATEADD` returns the same data type. `DATEADD` raises an error if the string literal seconds scale exceeds three decimal place positions (.nnn) or if the string literal contains the time zone offset part.
  
## Return Value  
  
## datepart Argument
 
**dayofyear**, **day**, and **weekday** return the same value.
  
Each *datepart* and its abbreviations return the same value.
  
If the following are true:

+ *datepart* is **month**
+ the *date* month has more days than the return month
+ the *date* day does not exist in the return month

Then, `DATEADD` returns the last day of the return month. For example, September has 30 (thirty) days; therefore, these statements return 2006-09-30 00:00:00.000:
  
```sql
SELECT DATEADD(month, 1, '20060830');
SELECT DATEADD(month, 1, '2006-08-31');
```
  
## number Argument

The *number* argument cannot exceed the range of **int**. In the following statements, the argument for *number* exceeds the range of **int** by 1. These statements both return the following error message: "`Msg 8115, Level 16, State 2, Line 1. Arithmetic overflow error converting expression to data type int."`
  
```sql
SELECT DATEADD(year,2147483648, '20060731');  
SELECT DATEADD(year,-2147483649, '20060731');  
```  
  
## date Argument

`DATEADD` will not accept a *date* argument incremented to a value outside the range of its data type. In the following statements, the *number* value added to the *date* value exceeds the range of the *date* data type. `DATEADD` returns the following error message: "`Msg 517, Level 16, State 1, Line 1 Adding a value to a 'datetime' column caused overflow`."
  
```sql
SELECT DATEADD(year,2147483647, '20060731');  
SELECT DATEADD(year,-2147483647, '20060731');  
```  
  
## Return Values for a smalldatetime date and a second or Fractional Seconds datepart  

The seconds part of a [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) value is always 00. For a **smalldatetime** *date* value, the following apply: 

- For a *datepart* of **second**, and a *number* value between -30 and +29, `DATEADD` makes no changes.  
- For a *datepart* of **second**, and a *number* value less than -30, or more than +29, `DATEADD` performs its addition beginning at one minute.  
- For a *datepart* of **millisecond** and a *number* value between -30001 and +29998, `DATEADD` makes no changes.  
- For a *datepart* of **millisecond** and a *number* value less than -30001, or more than +29998, `DATEADD` performs its addition beginning at one minute.  
  
## Remarks

Use `DATEADD` in the following clauses:

+ GROUP BY
+ HAVING
+ ORDER BY
+ SELECT \<list>
+ WHERE
  
## Fractional seconds precision

`DATEADD` does not allow addition for a *datepart* of **microsecond** or **nanosecond** for *date* data types **smalldatetime**, **date**, and **datetime**.
  
Milliseconds have a scale of 3 (.123), microseconds have a scale of 6 (.123456), and nanoseconds have a scale of 9 (.123456789). The **time**, **datetime2**, and **datetimeoffset** data types have a maximum scale of 7 (.1234567). For a *datepart* of **nanosecond**, *number* must be 100 before the fractional seconds of *date* increase. A *number* between 1 and 49 will round down to 0, and a number from 50 to 99 rounds up to 100.
  
These statements add a *datepart* of **millisecond**, **microsecond**, or **nanosecond**.
  
```sql
DECLARE @datetime2 datetime2 = '2007-01-01 13:10:10.1111111';  
SELECT '1 millisecond', DATEADD(millisecond,1,@datetime2)  
UNION ALL  
SELECT '2 milliseconds', DATEADD(millisecond,2,@datetime2)  
UNION ALL  
SELECT '1 microsecond', DATEADD(microsecond,1,@datetime2)  
UNION ALL  
SELECT '2 microseconds', DATEADD(microsecond,2,@datetime2)  
UNION ALL  
SELECT '49 nanoseconds', DATEADD(nanosecond,49,@datetime2)  
UNION ALL  
SELECT '50 nanoseconds', DATEADD(nanosecond,50,@datetime2)  
UNION ALL  
SELECT '150 nanoseconds', DATEADD(nanosecond,150,@datetime2);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
1 millisecond     2007-01-01 13:10:10.1121111  
2 milliseconds    2007-01-01 13:10:10.1131111  
1 microsecond     2007-01-01 13:10:10.1111121  
2 microseconds    2007-01-01 13:10:10.1111131  
49 nanoseconds    2007-01-01 13:10:10.1111111  
50 nanoseconds    2007-01-01 13:10:10.1111112  
150 nanoseconds   2007-01-01 13:10:10.1111113  
```  
  
## Time zone offset

`DATEADD` does not allow addition for time zone offset.
  
## Examples  

### A. Incrementing datepart by an interval of 1  

Each of these statements increments *datepart* by an interval of 1:
  
```sql
DECLARE @datetime2 datetime2 = '2007-01-01 13:10:10.1111111';  
SELECT 'year', DATEADD(year,1,@datetime2)  
UNION ALL  
SELECT 'quarter',DATEADD(quarter,1,@datetime2)  
UNION ALL  
SELECT 'month',DATEADD(month,1,@datetime2)  
UNION ALL  
SELECT 'dayofyear',DATEADD(dayofyear,1,@datetime2)  
UNION ALL  
SELECT 'day',DATEADD(day,1,@datetime2)  
UNION ALL  
SELECT 'week',DATEADD(week,1,@datetime2)  
UNION ALL  
SELECT 'weekday',DATEADD(weekday,1,@datetime2)  
UNION ALL  
SELECT 'hour',DATEADD(hour,1,@datetime2)  
UNION ALL  
SELECT 'minute',DATEADD(minute,1,@datetime2)  
UNION ALL  
SELECT 'second',DATEADD(second,1,@datetime2)  
UNION ALL  
SELECT 'millisecond',DATEADD(millisecond,1,@datetime2)  
UNION ALL  
SELECT 'microsecond',DATEADD(microsecond,1,@datetime2)  
UNION ALL  
SELECT 'nanosecond',DATEADD(nanosecond,1,@datetime2);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Year         2008-01-01 13:10:10.1111111  
quarter      2007-04-01 13:10:10.1111111  
month        2007-02-01 13:10:10.1111111  
dayofyear    2007-01-02 13:10:10.1111111  
day          2007-01-02 13:10:10.1111111  
week         2007-01-08 13:10:10.1111111  
weekday      2007-01-02 13:10:10.1111111  
hour         2007-01-01 14:10:10.1111111  
minute       2007-01-01 13:11:10.1111111  
second       2007-01-01 13:10:11.1111111  
millisecond  2007-01-01 13:10:10.1121111  
microsecond  2007-01-01 13:10:10.1111121  
nanosecond   2007-01-01 13:10:10.1111111  
```  
  
### B. Incrementing more than one level of datepart in one statement

Each of these statements increments *datepart* by a *number* large enough to additionally increment the next higher *datepart* of *date*:
  
```sql
DECLARE @datetime2 datetime2;  
SET @datetime2 = '2007-01-01 01:01:01.1111111';  
--Statement                                 Result     
-------------------------------------------------------------------   
SELECT DATEADD(quarter,4,@datetime2);     --2008-01-01 01:01:01.1111111  
SELECT DATEADD(month,13,@datetime2);      --2008-02-01 01:01:01.1111111  
SELECT DATEADD(dayofyear,365,@datetime2); --2008-01-01 01:01:01.1111111  
SELECT DATEADD(day,365,@datetime2);       --2008-01-01 01:01:01.1111111  
SELECT DATEADD(week,5,@datetime2);        --2007-02-05 01:01:01.1111111  
SELECT DATEADD(weekday,31,@datetime2);    --2007-02-01 01:01:01.1111111  
SELECT DATEADD(hour,23,@datetime2);       --2007-01-02 00:01:01.1111111  
SELECT DATEADD(minute,59,@datetime2);     --2007-01-01 02:00:01.1111111  
SELECT DATEADD(second,59,@datetime2);     --2007-01-01 01:02:00.1111111  
SELECT DATEADD(millisecond,1,@datetime2); --2007-01-01 01:01:01.1121111  
```  
  
### C. Using expressions as arguments for the number and date parameters
  
These examples use different types of expressions as arguments for the *number* and *date* parameters. The examples use the AdventureWorks database.
  
#### Specifying a column as date

This example adds `2` (two) days to each value in the `OrderDate` column, to derive a new column named `PromisedShipDate`:
  
```sql
SELECT SalesOrderID  
    ,OrderDate   
    ,DATEADD(day,2,OrderDate) AS PromisedShipDate  
FROM Sales.SalesOrderHeader;  
```  
  
A partial result set:
  
```
SalesOrderID OrderDate               PromisedShipDate  
------------ ----------------------- -----------------------  
43659        2005-07-01 00:00:00.000 2005-07-03 00:00:00.000  
43660        2005-07-01 00:00:00.000 2005-07-03 00:00:00.000  
43661        2005-07-01 00:00:00.000 2005-07-03 00:00:00.000  
...  
43702        2005-07-02 00:00:00.000 2005-07-04 00:00:00.000  
43703        2005-07-02 00:00:00.000 2005-07-04 00:00:00.000  
43704        2005-07-02 00:00:00.000 2005-07-04 00:00:00.000  
43705        2005-07-02 00:00:00.000 2005-07-04 00:00:00.000  
43706        2005-07-03 00:00:00.000 2005-07-05 00:00:00.000  
...  
43711        2005-07-04 00:00:00.000 2005-07-06 00:00:00.000  
43712        2005-07-04 00:00:00.000 2005-07-06 00:00:00.000  
...  
43740        2005-07-11 00:00:00.000 2005-07-13 00:00:00.000  
43741        2005-07-12 00:00:00.000 2005-07-14 00:00:00.000  
  
```  
  
#### Specifying user-defined variables as number and date
 
This example specifies user-defined variables as arguments for *number* and *date*:
  
```sql
DECLARE @days INT = 365,   
        @datetime DATETIME = '2000-01-01 01:01:01.111'; /* 2000 was a leap year */;  
SELECT DATEADD(day, @days, @datetime);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
-----------------------  
2000-12-31 01:01:01.110  
  
(1 row(s) affected)  
```  
  
#### Specifying scalar system function as date
  
This example specifies `SYSDATETIME` for *date*. The exact value returned depends on the
day and time of statement execution:
  
```sql
SELECT DATEADD(month, 1, SYSDATETIME());  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
---------------------------  
2013-02-06 14:29:59.6727944  
  
(1 row(s) affected)  
```  
  
#### Specifying scalar subqueries and scalar functions as number and date
 
This example uses scalar subqueries, `MAX(ModifiedDate)`, as arguments for *number* and *date*. `(SELECT TOP 1 BusinessEntityID FROM Person.Person)` serves as an artificial argument for the number parameter, to show how to select a *number* argument from a value list.
  
```sql
SELECT DATEADD(month,(SELECT TOP 1 BusinessEntityID FROM Person.Person),  
    (SELECT MAX(ModifiedDate) FROM Person.Person));  
```  
  
#### Specifying numeric expressions and scalar system functions as number and date

This example uses a numeric expression (-`(10/2))`, [unary operators](../../mdx/unary-operators.md) (`-`), an [arithmetic operator](../../mdx/arithmetic-operators.md) (`/`), and scalar system functions (`SYSDATETIME`) as arguments for *number* and *date*.
  
```sql
SELECT DATEADD(month,-(10/2), SYSDATETIME());  
```  
  
#### Specifying ranking functions as number
 
This example uses a ranking function as an argument for *number*.
  
```sql
SELECT p.FirstName, p.LastName  
    ,DATEADD(day,ROW_NUMBER() OVER (ORDER BY  
        a.PostalCode),SYSDATETIME()) AS 'Row Number'  
FROM Sales.SalesPerson AS s   
    INNER JOIN Person.Person AS p   
        ON s.BusinessEntityID = p.BusinessEntityID  
    INNER JOIN Person.Address AS a   
        ON a.AddressID = p.BusinessEntityID  
WHERE TerritoryID IS NOT NULL   
    AND SalesYTD <> 0;  
```  
  
#### Specifying an aggregate window function as number

This example uses an aggregate window function as an argument for *number*.
  
```sql
SELECT SalesOrderID, ProductID, OrderQty  
    ,DATEADD(day,SUM(OrderQty)   
        OVER(PARTITION BY SalesOrderID),SYSDATETIME()) AS 'Total'  
FROM Sales.SalesOrderDetail   
WHERE SalesOrderID IN(43659,43664);  
GO  
```  
  

## See also

[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
