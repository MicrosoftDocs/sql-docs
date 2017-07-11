---
title: "DATEADD (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATEADD"
  - "DATEADD_TSQL"
dev_langs: 
  - "TSQL"
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
ms.assetid: 89c5ae32-89c6-47e1-979e-15d97908b9f1
caps.latest.revision: 71
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATEADD (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a specified *date* with the specified *number* interval (signed integer) added to a specified *datepart* of that *date*.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DATEADD (datepart , number , date )  
```  
  
## Arguments  
 *datepart*  
 Is the part of *date* to which an **integer***number* is added. The following table lists all valid *datepart* arguments. User-defined variable equivalents are not valid.  
  
|*datepart*|Abbreviations|  
|----------------|-------------------|  
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
 Is an expression that can be resolved to an [int](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md) that is added to a *datepart* of *date*. User-defined variables are valid.  
  
 If you specify a value with a decimal fraction, the fraction is truncated and not rounded.  
  
 *date*  
 Is an expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, user-defined variable, or string literal. If the expression is a string literal, it must resolve to a **datetime**. To avoid ambiguity, use four-digit years. For information about two-digit years, see [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).  
  
## Return Types  
 The return data type is the data type of the *date* argument, except for string literals.  
  
 The return data type for a string literal is **datetime**. An error will be raised if the string literal seconds scale is more than three positions (.nnn) or contains the time zone offset part.  
  
## Return Value  
  
## datepart Argument  
 **dayofyear**, **day**, and **weekday** return the same value.  
  
 Each *datepart* and its abbreviations return the same value.  
  
 If *datepart* is **month** and the *date* month has more days than the return month and the *date* day does not exist in the return month, the last day of the return month is returned. For example, September has 30 days; therefore, the two following statements return 2006-09-30 00:00:00.000:  
  
 ```
 SELECT DATEADD(month, 1, '2006-08-30');  
 SELECT DATEADD(month, 1, '2006-08-31');
 ```  
  
## number Argument  
 The *number* argument cannot exceed the range of **int**. In the following statements, the argument for *number* exceeds the range of **int** by 1. The following error message is returned: "`Msg 8115, Level 16, State 2, Line 1. Arithmetic overflow error converting expression to data type int."`  
  
```  
SELECT DATEADD(year,2147483648, '2006-07-31');  
SELECT DATEADD(year,-2147483649, '2006-07-31');  
```  
  
## date Argument  
 The *date* argument cannot be incremented to a value outside the range of its data type. In the following statements, the *number* value that is added to the *date* value exceeds the range of the *date* data type. The following error message is returned: "`Msg 517, Level 16, State 1, Line 1 Adding a value to a 'datetime' column caused overflow`."  
  
```  
SELECT DATEADD(year,2147483647, '2006-07-31');  
SELECT DATEADD(year,-2147483647, '2006-07-31');  
```  
  
## Return Values for a smalldatetime date and a second or Fractional Seconds datepart  
 The seconds part of a [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) value is always 00. If *date* is **smalldatetime**, the following apply:  
  
-   If *datepart* is **second** and *number* is between -30 and +29, no addition is performed.  
-   If *datepart* is **second** and *number* is less than-30 or more than +29, addition is performed beginning at one minute.  
-   If *datepart* is **millisecond** and *number* is between -30001 and +29998, no addition is performed.  
-   If *datepart* is **millisecond** and *number* is less than -30001 or more than +29998, addition is performed beginning at one minute.  
  
## Remarks  
 DATEADD can be used in the SELECT \<list>, WHERE, HAVING, GROUP BY and ORDER BY clauses.  
  
## Fractional Seconds Precision  
 Addition for a *datepart* of **microsecond** or **nanosecond** for *date* data types **smalldatetime**, **date**, and **datetime** is not allowed.  
  
 Milliseconds have a scale of 3 (.123), microseconds have a scale of 6 (.123456), And nanoseconds have a scale of 9 (.123456789). The **time**, **datetime2**, and **datetimeoffset** data types have a maximum scale of 7 (.1234567). If *datepart* is **nanosecond**, *number* must be 100 before the fractional seconds of *date* increase. A *number* between 1 and 49 is rounded down to 0 and a number from 50 to 99 is rounded up to 100.  
  
 The following statements add a *datepart* of **millisecond**, **microsecond**, or **nanosecond**.  
  
```  
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
  
## Time Zone Offset  
 Addition is not allowed for time zone offset.  
  
## Examples  


### A. Incrementing datepart by an interval of 1  
 Each of the following statements increments *datepart* by an interval of 1.  
  
```  
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
 Each of the following statements increments *datepart* by a *number* large enough to also increment the next higher *datepart* of *date*.  
  
```  
DECLARE @datetime2 datetime2;  
SET @datetime2 = '2007-01-01 01:01:01.1111111';  
--Statement                                 Result     
-------------------------------------------------------------------   
SELECT DATEADD(quarter,4,@datetime2);     --2008-01-01 01:01:01.110  
SELECT DATEADD(month,13,@datetime2);      --2008-02-01 01:01:01.110  
SELECT DATEADD(dayofyear,365,@datetime2); --2008-01-01 01:01:01.110  
SELECT DATEADD(day,365,@datetime2);       --2008-01-01 01:01:01.110  
SELECT DATEADD(week,5,@datetime2);        --2007-02-05 01:01:01.110  
SELECT DATEADD(weekday,31,@datetime2);    --2007-02-01 01:01:01.110  
SELECT DATEADD(hour,23,@datetime2);       --2007-01-02 00:01:01.110  
SELECT DATEADD(minute,59,@datetime2);     --2007-01-01 02:00:01.110  
SELECT DATEADD(second,59,@datetime2);     --2007-01-01 01:02:00.110  
SELECT DATEADD(millisecond,1,@datetime2); --2007-01-01 01:01:01.110  
```  
  
### C. Using expressions as arguments for the number and date parameters  
 The following examples use different types of expressions as arguments for the *number* and *date* parameters. The examples use the AdventureWorks database.  
  
#### Specifying a column as date  
 The following example adds `2` days to each value in the `OrderDate` column to derive a new column named  `PromisedShipDate`.  
  
```  
SELECT SalesOrderID  
    ,OrderDate   
    ,DATEADD(day,2,OrderDate) AS PromisedShipDate  
FROM Sales.SalesOrderHeader;  
```  
  
 Here is a partial result set.  
  
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
 The following example specifies user-defined variables as arguments for *number* and *date*.  
  
```  
DECLARE @days int = 365,   
        @datetime datetime = '2000-01-01 01:01:01.111'; /* 2000 was a leap year */;  
SELECT DATEADD(day, @days, @datetime);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-----------------------  
2000-12-31 01:01:01.110  
  
(1 row(s) affected)  
```  
  
#### Specifying scalar system function as date  
 The following example specifies `SYSDATETIME` for *date*.  
  
```  
SELECT DATEADD(month, 1, SYSDATETIME());  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------------------------  
2013-02-06 14:29:59.6727944  
  
(1 row(s) affected)  
```  
  
#### Specifying scalar subqueries and scalar functions as number and date  
 The following example uses scalar subqueries, `MAX(ModifiedDate)`, as arguments for *number* and *date*. `(SELECT TOP 1 BusinessEntityID FROM Person.Person)` is an artificial argument for the number parameter to show how to select a *number* argument from a value list.  
  
```  
SELECT DATEADD(month,(SELECT TOP 1 BusinessEntityID FROM Person.Person),  
    (SELECT MAX(ModifiedDate) FROM Person.Person));  
```  
  
#### Specifying numeric expressions and scalar system functions as number and date  
 The following example uses a numeric expression (-`(10/2))`, [unary operators](../../mdx/unary-operators.md) (`-`), an [arithmetic operator](../../mdx/arithmetic-operators.md) (`/`), and scalar system functions (`SYSDATETIME`) as arguments for *number* and *date*.  
  
```  
SELECT DATEADD(month,-(10/2), SYSDATETIME());  
```  
  
#### Specifying ranking functions as number  
 The following example uses a ranking function as arguments for *number*.  
  
```  
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
 The following example uses an aggregate window function as an argument for *number*.  
  
```  
SELECT SalesOrderID, ProductID, OrderQty  
    ,DATEADD(day,SUM(OrderQty)   
        OVER(PARTITION BY SalesOrderID),SYSDATETIME()) AS 'Total'  
FROM Sales.SalesOrderDetail   
WHERE SalesOrderID IN(43659,43664);  
GO  
```  
  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  

