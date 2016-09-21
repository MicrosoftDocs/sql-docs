---
title: "DATEDIFF (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 61393c3b-8c81-4a63-bd5c-846a2947f6b3
caps.latest.revision: 16
author: BarbKess
---
# DATEDIFF (SQL Server PDW)
Returns the count (signed integer) of the specified *datepart* boundaries crossed between the specified *startdate* and *enddate* in SQL Server PDW. Use this function in the select list, WHERE, HAVING, GROUP BY and ORDER BY clauses to subtract **datetime** values and return their difference.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATEDIFF (datepart ,startdate ,enddate )  
```  
  
## Arguments  
*datepart*  
The part of *startdate* and *enddate* that specifies the type of boundary crossed. The following table lists all valid *datepart* arguments.  
  
|*datepart*|Abbreviations|  
|--------------|-----------------|  
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
An [expressions](../sqlpdw/expressions-sql-server-pdw.md) that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. *date* can be an expression, column expression, or string literal. *startdate* is subtracted from *enddate*.  
  
To avoid ambiguity, use four-digit years.  
  
*enddate*  
See *startdate*.  
  
## Return Type  
**int**  
  
## Return Value  
Each *datepart* and its abbreviations return the same value.  
  
If the return value is out of range for **int** (-2,147,483,648 to +2,147,483,647), an error is returned. For **millisecond**, the maximum difference between *startdate* and *enddate* is 24 days, 20 hours, 31 minutes and 23.647 seconds. For **second**, the maximum difference is 68 years.  
  
If *startdate* and *enddate* are both assigned only a time value and the *datepart* is not a time *datepart*, 0 is returned.  
  
A time zone offset component of *startdate* or *endate* is not used in calculating the return value.  
  
Because **smalldatetime** is accurate only to the minute, when a **smalldatetime** value is used for *startdate* or *enddate*, seconds and milliseconds are always set to 0 in the return value.  
  
If only a time value is assigned to a variable of a date data type, the value of the missing date part is set to the default value: 1900-01-01. If only a date value is assigned to a variable of a time or date data type, the value of the missing time part is set to the default value: 00:00:00. If either *startdate* or *enddate* have only a time part and the other only a date part, the missing time and date parts are set to the default values.  
  
If *startdate* and *enddate* are of different date data types and one has more time parts or fractional seconds precision than the other, the missing parts of the other are set to 0.  
  
## datepart Boundaries  
The following statements have the same *startdate* and the same *endate*. Those dates are adjacent and differ in time by .0000001 second. The difference between the *startdate* and *endate* in each statement crosses one calendar or time boundary of its *datepart*. Each statement returns 1. If different years are used for this example and if both *startdate* and *endate* are in the same calendar week, the return value for **week** would be 0.  
  
```  
USE AdventureWorksPDW2012;  
```  
  
<pre>SELECT TOP(1) DATEDIFF(year, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(quarter, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(month, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(dayofyear, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(day, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(week, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(hour, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(minute, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(second, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;  
SELECT TOP(1) DATEDIFF(millisecond, '2005-12-31 23:59:59.9999999'  
, '2006-01-01 00:00:00.0000000') FROM DimCustomer;</pre>  
  
## Examples  
The following examples use different types of expressions as arguments for the *startdate* and *enddate* parameters.  
  
### A. Specifying columns for startdate and enddate  
The following example calculates the number of day boundaries that are crossed between dates in two columns in a table.  
  
```  
CREATE TABLE dbo.Duration (  
    startDate datetime2  
    ,endDate datetime2  
    );  
INSERT INTO dbo.Duration(startDate,endDate)  
    VALUES('2007-05-06 12:10:09','2007-05-07 12:10:09');  
SELECT TOP(1) DATEDIFF(day,startDate,endDate) AS Duration  
FROM dbo.Duration;  
-- Returns: 1  
```  
  
### B. Specifying scalar subqueries and scalar functions for startdate and enddate  
The following example uses scalar subqueries and scalar functions as arguments for *startdate* and *enddate*.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT TOP(1) DATEDIFF(day,(SELECT MIN(HireDate) FROM dbo.DimEmployee),  
    (SELECT MAX(HireDate) FROM dbo.DimEmployee))   
FROM dbo.DimEmployee;  
```  
  
### C. Specifying constants for startdate and enddate  
The following example uses character constants as arguments for *startdate* and *enddate*.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT TOP(1) DATEDIFF(day, '2007-05-07 09:53:01.0376635'  
    , '2007-05-08 09:53:01.0376635') FROM DimCustomer;  
```  
  
### D. Specifying ranking functions for startdate  
The following example uses a ranking function as an argument for *startdate*.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT FirstName, LastName  
,DATEDIFF(day,ROW_NUMBER() OVER (ORDER BY   
        DepartmentName),SYSDATETIME()) AS RowNumber  
FROM dbo.DimEmployee;  
```  
  
### E. Specifying an aggregate window function for startdate  
The following example uses an aggregate window function as an argument for *startdate*.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT FirstName, LastName, DepartmentName  
    ,DATEDIFF(year,MAX(HireDate)  
             OVER (PARTITION BY DepartmentName),SYSDATETIME()) AS SomeValue  
FROM dbo.DimEmployee  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DATEADD &#40;SQL Server PDW&#41;](../sqlpdw/dateadd-sql-server-pdw.md)  
[DATEPART &#40;SQL Server PDW&#41;](../sqlpdw/datepart-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
  
