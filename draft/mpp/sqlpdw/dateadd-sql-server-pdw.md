---
title: "DATEADD (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 40f60889-3c87-4fe9-af81-d550c92e2ff4
caps.latest.revision: 28
author: BarbKess
---
# DATEADD (SQL Server PDW)
Returns a specified date with the specified number interval (signed int) added to a specified date part of that date in SQL Server PDW. Use this function in the SELECT <list>, WHERE, HAVING, GROUP BY and ORDER BY clauses to add a specified value to a part of a date column or expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATEADD (datepart ,number, date )  
```  
  
## Arguments  
*datepart*  
The part of *date* to which an **int** *number* is added. The following table lists all valid *datepart* arguments. User-defined variable equivalents are not valid.  
  
|*datepart*|Abbreviations|  
|--------------|-----------------|  
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
An expression that can be resolved to an **int** that is added to a *datepart* of *date*. *number* cannot exceed the range of **int**. If you specify a value with a decimal fraction, the fraction is truncated and not rounded.  
  
*date*  
An expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, or string literal. If the expression is a string literal, it must resolve to a **datetime**. An error will be raised if the string literal seconds scale is more than three positions (.nnn) or contains the time zone offset part.  
  
To avoid ambiguity, use four-digit years when specifying *date*.  
  
## Return Types  
The return data type is the data type of the *date* argument, except for string literals, which must resolve to and return a **datetime**.  
  
## Return Value  
  
## datepart Argument  
**dayofyear**, **day**, and **weekday** return the same value.  
  
Each *datepart* and its abbreviations return the same value.  
  
If *datepart* is **month** and the *date* month has more days than the return month and the *date* day does not exist in the return month, the last day of the return month is returned. For example, September has 30 days; therefore, the two following statements return `9/30/2006 12:00:00 AM`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) DATEADD(month, 1, '2006-08-30')   
FROM DimCustomer;  
SELECT TOP(1)DATEADD(month, 1, '2006-08-31')   
FROM DimCustomer;  
```  
  
## date Argument  
The *date* argument cannot be incremented to a value outside the range of its data type. In the following statements, the *number* value that is added to the *date* value exceeds the range of the *date* data type. The following error message is returned: "Adding a value to a 'datetime' column caused an overflow."  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) DATEADD(year,2147483647, '2006-07-31') FROM DimCustomer;  
SELECT TOP(1) DATEADD(year,-2147483647, '2006-07-31')FROM DimCustomer;  
```  
  
## Remarks  
  
## Fractional Seconds Precision  
Addition for a *datepart* of **microsecond** or **nanosecond** for *date* data types **date** and **datetime** or string literals is not allowed.  
  
Milliseconds have a scale of 3 (.123). microseconds have a scale of 6 (.123456). nanoseconds have a scale of 9 (.123456789). The **time** and **datetime2** data types have a maximum scale of 7 (.1234567). If *datepart* is **nanosecond**, *number* must be 100 before the fractional seconds of *date* increase. A *number* between 1 and 49 is rounded down to 0 and a number from 50 to 99 is rounded up to 100.  
  
The following statements add a *datepart* of **millisecond**, **microsecond**, or **nanosecond**.  
  
```  
CREATE TABLE Customer (  
  Id INT,  
  CustomerDate datetime2);  
  
INSERT INTO Customer VALUES (1, '2007-01-01 13:10:10.1111111');  
  
SELECT '1 millisecond' ,DATEADD(millisecond,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT '2 milliseconds', DATEADD(millisecond,2, CustomerDate) FROM Customer  
UNION ALL  
SELECT '1 microsecond', DATEADD(microsecond,1, CustomerDate) FROM Customer  
UNION ALL  
SELECT '2 microseconds', DATEADD(microsecond,2, CustomerDate) FROM Customer  
UNION ALL  
SELECT '49 nanoseconds', DATEADD(nanosecond,49, CustomerDate) FROM Customer  
UNION ALL  
SELECT '50 nanoseconds', DATEADD(nanosecond,50, CustomerDate) FROM Customer  
UNION ALL  
SELECT '150 nanoseconds', DATEADD(nanosecond,150, CustomerDate) FROM Customer;  
```  
  
Here is the result set.  
  
<pre>-----------------  ---------------------------  
1 millisecond      01/01/2007 13:10:10.1121111  
2 milliseconds     01/01/2007 1:10:10.1131111  
1 microsecond      01/01/2007 1:10:10.1111121  
2 microseconds     01/01/2007 1:10:10.1111131  
49 nanoseconds     01/01/2007 1:10:10.1111111  
50 nanoseconds     01/01/2007 1:10:10.1111112  
150 nanoseconds    01/01/2007 1:10:10.1111113</pre>  
  
## Time Zone Offset  
Addition is not allowed for time zone offset.  
  
## Examples  
  
### A. Incrementing datepart by an interval of 1  
Each of the following statements increments *datepart* by an interval of 1.  
  
```  
CREATE TABLE Customer (  
  ID INTEGER,  
  CustomerDate datetime2);  
  
INSERT INTO Customer VALUES (1, '2007-01-01 13:10:10.1111111');  
  
SELECT 'year', DATEADD(year,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'quarter',DATEADD(quarter,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'month',DATEADD(month,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'dayofyear',DATEADD(dayofyear,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'day',DATEADD(day,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'week',DATEADD(week,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'weekday',DATEADD(weekday,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'hour',DATEADD(hour,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'minute',DATEADD(minute,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'second',DATEADD(second,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'millisecond',DATEADD(millisecond,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'microsecond',DATEADD(microsecond,1,CustomerDate) FROM Customer  
UNION ALL  
SELECT 'nanosecond',DATEADD(nanosecond,1,CustomerDate) FROM Customer;  
```  
  
<pre>--------------  --------------------------  
Year            01/01/2008 1:10:10.1111111  
Quarter         04/01/2007 1:10:10.1111111  
Month           02/01/2007 1:10:10.1111111  
Dayofyear       01/02/2007 1:10:10.1111111  
Day             01/02/2007 1:10:10.1111111  
Week            01/08/2007 1:10:10.1111111  
Weekday         01/02/2007 1:10:10.1111111  
Hour            01/01/2007 2:10:10.1111111  
Minute          01/01/2007 1:11:10.1111111  
Second          01/01/2007 1:10:11.1111111  
Millisecond     01/01/2007 1:10:10.1121111  
Microsecond     01/01/2007 1:10:10.1111121  
Nanosecond      01/01/2007 1:10:10.1111111</pre>  
  
### B. Using expressions as arguments for the number and date parameters  
The following examples use different types of expressions as arguments for the *number* and *date* parameters.  
  
#### Specifying column as date  
The following example adds `60` days to each `HireDate` to calculate the date that the employee is eligible for company benefits.  
  
```  
USE AdventureWorksPDW2012   
  
SELECT EmployeeKey  
    ,HireDate  
    ,DATEADD(day,60,HireDate) AS BenefitsDate  
FROM DimEmployee;  
```  
  
#### Specifying scalar system function as date  
The following example specifies `SYSDATETIME` for *date*.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP (1) DATEADD(month, 1, SYSDATETIME()) FROM DimCustomer;  
```  
  
#### Specifying scalar subqueries and scalar functions as number and date  
The following example uses scalar subqueries and scalar functions (`MAX(HireDate))`, as arguments for *number* and *date*. `(SELECT (TOP(1) EmployeeKey FROM dbo.DimEmployee)` is an artificial argument for the *number* parameter to show how to select a *number* argument from a value list.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) DATEADD (month, (SELECT TOP(1) EmployeeKey FROM dbo.DimEmployee),  
    (SELECT MAX(HireDate) FROM dbo.DimEmployee))  
FROM dbo.DimCustomer;  
```  
  
#### Specifying constants as number and date  
The following example uses numeric and character constants as arguments for *number* and *date*.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP (1) DATEADD(minute, 1, '2007-05-07 09:53:01.123') FROM DimCustomer;  
```  
  
#### Specifying numeric expressions and scalar system functions as number and date  
The following example uses a numeric expressions (-`(10/2))`, and a scalar system function (`SYSDATETIME`) as arguments for *number* and *date*.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP (1) DATEADD(month,10/2, SYSDATETIME()) FROM DimCustomer;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[DATEPART &#40;SQL Server PDW&#41;](../sqlpdw/datepart-sql-server-pdw.md)  
[DATEDIFF &#40;SQL Server PDW&#41;](../sqlpdw/datediff-sql-server-pdw.md)  
  
