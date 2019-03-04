---
title: "DATEDIFF (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/13/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATEDIFF_TSQL"
  - "DATEDIFF"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dates [SQL Server], functions"
  - "DATEDIFF function [SQL Server]"
  - "time [SQL Server], crossed boundaries"
  - "differences in date and time [SQL Server]"
  - "counting crossed date time boundaries [SQL Server]"
  - "date and time [SQL Server], DATEDIFF"
  - "dates [SQL Server], crossed boundaries"
  - "boundary differences date and time [SQL Server]"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "interval dates [SQL Server]"
  - "time [SQL Server], functions"
  - "crossing date time boundaries [SQL Server]"
  - "calculating dates times [SQL Server]"
ms.assetid: eba979f2-1a8d-4cce-9d75-b74f9b519b37
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DATEDIFF (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the count (as a signed integer value) of the specified datepart boundaries crossed between the specified *startdate* and *enddate*.
  
See [DATEDIFF_BIG &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-big-transact-sql.md) for a function that handles larger differences between the *startdate* and *enddate* values. See [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATEDIFF ( datepart , startdate , enddate )  
```  
  
## Arguments  
*datepart*  
The part of *startdate* and *enddate* that specifies the type of boundary crossed. `DATEDIFF` will not accept user-defined variable equivalents. This table lists all valid *datepart* arguments.
  
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
  
Use four-digit years to avoid ambiguity. See [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md) for information about two-digit year values.
  
*enddate*  
See *startdate*.
  
## Return Type  
 **int**  
  
## Return Value  
  
Each specific *datepart* and the abbreviations for that *datepart* will return the same value.  
  
For a return value out of range for **int** (-2,147,483,648 to +2,147,483,647), `DATEDIFF` returns an error.  For **millisecond**, the maximum difference between *startdate* and *enddate* is 24 days, 20 hours, 31 minutes and 23.647 seconds. For **second**, the maximum difference is 68 years, 19 days, 3 hours, 14 minutes and 7 seconds.
  
If *startdate* and *enddate* are both assigned only a time value, and the *datepart* is not a time *datepart*, `DATEDIFF` returns 0.
  
`DATEDIFF` does not use a time zone offset component of *startdate* or *enddate* to calculate the return value.
  
Because [smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md) is accurate only to the minute, seconds and milliseconds are always set to 0 in the return value when *startdate* or *enddate* have a **smalldatetime** value.
  
If only a time value is assigned to a date data type variable, `DATEDIFF` sets the value of the missing date part to the default value: `1900-01-01`. If only a date value is assigned to a variable of a time or date data type, `DATEDIFF` sets the value of the missing time part to the default value: `00:00:00`. If either *startdate* or *enddate* have only a time part and the other only a date part, `DATEDIFF` sets the missing time and date parts to the default values.
  
If *startdate* and *enddate* have different date data types, and one has more time parts or fractional seconds precision than the other, `DATEDIFF` sets the missing parts of the other to 0.
  
## datepart boundaries  
The following statements have the same *startdate* and the same *enddate* values. Those dates are adjacent and they differ in time by one microsecond (.0000001 second). The difference between the *startdate* and *enddate* in each statement crosses one calendar or time boundary of its *datepart*. Each statement returns 1. If *startdate* and *enddate* have different year values but they have the same calendar week values, `DATEDIFF` will return 0 for *datepart* **week**.
  
```sql
SELECT DATEDIFF(year,        '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(quarter,     '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(month,       '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(dayofyear,   '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(day,         '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(week,        '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(hour,        '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(minute,      '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(second,      '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
SELECT DATEDIFF(millisecond, '2005-12-31 23:59:59.9999999', '2006-01-01 00:00:00.0000000');
```
  
## Remarks  
Use `DATEDIFF` in the `SELECT <list>`, `WHERE`, `HAVING`, `GROUP BY` and `ORDER BY` clauses.
  
`DATEDIFF` implicitly casts string literals as a **datetime2** type. This means that `DATEDIFF` does not support the format YDM when the date is passed as a string. You must explicitly cast the string to a **datetime** or **smalldatetime** type to use the YDM format.
  
Specifying `SET DATEFIRST` has no effect on `DATEDIFF`. `DATEDIFF` always uses Sunday as the first day of the week to ensure the function operates in a deterministic way.

`DATEDIFF` may overflow with a precision of **minute** or higher if the difference between *enddate* and *startdate* returns a value that is out of range for **int**.
  
## Examples  
These examples use different types of expressions as arguments for the *startdate* and *enddate* parameters.
  
### A. Specifying columns for startdate and enddate  
This example calculates the number of day boundaries crossed between dates in two columns in a table.
  
```sql
CREATE TABLE dbo.Duration  
    (startDate datetime2, endDate datetime2);  
    
INSERT INTO dbo.Duration(startDate, endDate)  
    VALUES ('2007-05-06 12:10:09', '2007-05-07 12:10:09');  
    
SELECT DATEDIFF(day, startDate, endDate) AS 'Duration'  
    FROM dbo.Duration;  
-- Returns: 1  
```  
  
### B. Specifying user-defined variables for startdate and enddate  
In this example, user-defined variables serve as arguments for *startdate* and *enddate*.
  
```sql
DECLARE @startdate datetime2 = '2007-05-05 12:10:09.3312722';  
DECLARE @enddate   datetime2 = '2007-05-04 12:10:09.3312722';   
SELECT DATEDIFF(day, @startdate, @enddate);  
```  
  
### C. Specifying scalar system functions for startdate and enddate  
This example uses scalar system functions as arguments for *startdate* and *enddate*.
  
```sql
SELECT DATEDIFF(millisecond, GETDATE(), SYSDATETIME());  
```  
  
### D. Specifying scalar subqueries and scalar functions for startdate and enddate  
This example uses scalar subqueries and scalar functions as arguments for *startdate* and *enddate*.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT DATEDIFF(day,
    (SELECT MIN(OrderDate) FROM Sales.SalesOrderHeader),  
    (SELECT MAX(OrderDate) FROM Sales.SalesOrderHeader));  
```  
  
### E. Specifying constants for startdate and enddate  
This example uses character constants as arguments for *startdate* and *enddate*.
  
```sql
SELECT DATEDIFF(day,
   '2007-05-07 09:53:01.0376635',
   '2007-05-08 09:53:01.0376635');  
```  
  
### F. Specifying numeric expressions and scalar system functions for enddate  
This example uses a numeric expression, `(GETDATE() + 1)`, and scalar system functions `GETDATE` and `SYSDATETIME`, as arguments for *enddate*.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT DATEDIFF(day, '2007-05-07 09:53:01.0376635', GETDATE() + 1)   
    AS NumberOfDays  
    FROM Sales.SalesOrderHeader;  
GO  
USE AdventureWorks2012;  
GO  
SELECT DATEDIFF(day, '2007-05-07 09:53:01.0376635', DATEADD(day, 1, SYSDATETIME())) AS NumberOfDays  
    FROM Sales.SalesOrderHeader;  
GO  
```  
  
### G. Specifying ranking functions for startdate  
This example uses a ranking function as an argument for *startdate*.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT p.FirstName, p.LastName  
    ,DATEDIFF(day, ROW_NUMBER() OVER (ORDER BY   
        a.PostalCode), SYSDATETIME()) AS 'Row Number'  
FROM Sales.SalesPerson s   
    INNER JOIN Person.Person p   
        ON s.BusinessEntityID = p.BusinessEntityID  
    INNER JOIN Person.Address a   
        ON a.AddressID = p.BusinessEntityID  
WHERE TerritoryID IS NOT NULL   
    AND SalesYTD <> 0;  
```  
  
### H. Specifying an aggregate window function for startdate  
This example uses an aggregate window function as an argument for *startdate*.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT soh.SalesOrderID, sod.ProductID, sod.OrderQty, soh.OrderDate,
    DATEDIFF(day, MIN(soh.OrderDate)   
        OVER(PARTITION BY soh.SalesOrderID), SYSDATETIME()) AS 'Total'  
FROM Sales.SalesOrderDetail sod  
    INNER JOIN Sales.SalesOrderHeader soh  
        ON sod.SalesOrderID = soh.SalesOrderID  
WHERE soh.SalesOrderID IN(43659, 58918);  
GO  
```  

### I. Finding difference between startdate and enddate as date parts strings

```sql
DECLARE @date1 DATETIME, @date2 DATETIME, @result VARCHAR(100)
DECLARE @years INT, @months INT, @days INT, @hours INT, @minutes INT, @seconds INT, @milliseconds INT

SET @date1 = '1900-01-01 00:00:00.000'
SET @date2 = '2018-12-12 07:08:01.123'

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
118 years, 11 months, 11 days, 7 hours, 8 minutes and 1.123 seconds
```
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
These examples use different types of expressions as arguments for the *startdate* and *enddate* parameters.
  
### J. Specifying columns for startdate and enddate  
This example calculates the number of day boundaries crossed between dates in two columns in a table.
  
```sql
CREATE TABLE dbo.Duration 
    (startDate datetime2, endDate datetime2);
    
INSERT INTO dbo.Duration (startDate, endDate)  
    VALUES ('2007-05-06 12:10:09', '2007-05-07 12:10:09');  
    
SELECT TOP(1) DATEDIFF(day, startDate, endDate) AS Duration  
    FROM dbo.Duration;  
-- Returns: 1  
```  
  
### K. Specifying scalar subqueries and scalar functions for startdate and enddate  
This example uses scalar subqueries and scalar functions as arguments for *startdate* and *enddate*.
  
```sql
-- Uses AdventureWorks  
  
SELECT TOP(1) DATEDIFF(day, (SELECT MIN(HireDate) FROM dbo.DimEmployee),  
    (SELECT MAX(HireDate) FROM dbo.DimEmployee))   
FROM dbo.DimEmployee;  
  
```  
  
### L. Specifying constants for startdate and enddate  
This example uses character constants as arguments for *startdate* and *enddate*.
  
```sql
-- Uses AdventureWorks  
  
SELECT TOP(1) DATEDIFF(day,
    '2007-05-07 09:53:01.0376635',
    '2007-05-08 09:53:01.0376635') FROM DimCustomer;  
```  
  
### M. Specifying ranking functions for startdate  
This example uses a ranking function as an argument for *startdate*.
  
```sql
-- Uses AdventureWorks  
  
SELECT FirstName, LastName,
    DATEDIFF(day, ROW_NUMBER() OVER (ORDER BY   
        DepartmentName), SYSDATETIME()) AS RowNumber  
FROM dbo.DimEmployee;  
```  
  
### N. Specifying an aggregate window function for startdate  
This example uses an aggregate window function as an argument for *startdate*.
  
```sql
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, DepartmentName,
    DATEDIFF(year, MAX(HireDate)  
        OVER (PARTITION BY DepartmentName), SYSDATETIME()) AS SomeValue  
FROM dbo.DimEmployee  
```  
  
## See also
[DATEDIFF_BIG &#40;Transact-SQL&#41;](../../t-sql/functions/datediff-big-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
