---
title: "CAST and CONVERT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1d6df410-932e-4904-bce4-31271db8b040
caps.latest.revision: 24
author: BarbKess
---
# CAST and CONVERT (SQL Server PDW)
Converts an expression of one data type to another data type in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
--Syntax for CAST  
CAST ( expression AS data_type [ (length ) ])  
```  
  
```  
--Syntax for CONVERT  
CONVERT ( data_type [ ( length ) ] , expression [ , style ] )  
```  
  
## Arguments  
*expression*  
A valid expression.  
  
*data_type*  
The target data type.  
  
*length*  
An optional integer that specifies the length of the target data type. The default value is 30.  
  
*style*  
An integer expression that specifies how the CONVERT function is to translate *expression*. If style is NULL, NULL is returned. The range is determined by *data_type*.  
  
## Return Types  
Returns *expression* translated to *data_type*.  
  
## Data Types  
  
### Date and Time  
When *expression* is a date or time data type, *style* can be one of the values shown in the following table. Other values are processed as 0. SQL Server PDW supports the date format in Arabic style by using the Kuwaiti algorithm.  
  
|Without century (yy) (<sup>1</sup>)|With century (yyyy)|Standard|Input/Output (<sup>3</sup>)|  
|-------------------------------------------|-------------------------|------------|----------------------------------|  
|-|**0** or **100** (<sup>1,</sup><sup>2</sup>)|Default for datetime and smalldatetime|mon dd yyyy hh:miAM (or PM)|  
|**1**|**101**|U.S.|mm/dd/yyyy|  
|**2**|**102**|ANSI|yy.mm.dd|  
|**3**|**103**|British/French|dd/mm/yyyy|  
|**4**|**104**|German|dd.mm.yy|  
|**5**|**105**|Italian|dd-mm-yy|  
|**6**|**106**<sup>(1)</sup>|-|dd mon yy|  
|**7**|**107**<sup>(1)</sup>|-|mon dd, yy|  
|**8**|**108**|-|hh:mi:ss|  
|-|**9** or **109** (<sup>1,</sup><sup>2</sup>)|Default + milliseconds|mon dd yyyy hh:mi:ss:mmmAM (or PM)|  
|**10**|**110**|USA|mm-dd-yy|  
|**11**|**111**|JAPAN|yy/mm/dd|  
|**12**|**112**|ISO|yymmdd<br /><br />yyyymmdd|  
|-|**13** or **113** (<sup>1,</sup><sup>2</sup>)|Europe default + milliseconds|dd mon yyyy hh:mi:ss:mmm(24h)|  
|**14**|**114**|-|hh:mi:ss:mmm(24h)|  
|-|**20** or **120** (<sup>2</sup>)|ODBC canonical|yyyy-mm-dd hh:mi:ss(24h)|  
|-|**21** or **121** (<sup>2</sup>)|ODBC canonical (with milliseconds) default for time, date, datetime2, and datetimeoffset|yyyy-mm-dd hh:mi:ss.mmm(24h)|  
|-|**126** (<sup>4</sup>)|ISO8601|yyyy-mm-ddThh:mi:ss.mmm (no spaces)|  
|-|**127**(<sup>6, 7</sup>)|ISO8601 with time zone Z.|yyyy-mm-ddThh:mi:ss.mmmZ<br /><br />(no spaces)|  
|-|**130** (<sup>1,</sup><sup>2</sup>)|Hijri (<sup>5</sup>)|dd mon yyyy hh:mi:ss:mmmAM|  
|-|**131** (<sup>2</sup>)|Hijri (<sup>5</sup>)|dd/mm/yy hh:mi:ss:mmmAM|  
  
<sup>1</sup> These style values return nondeterministic results. Includes all (yy) (without century) styles and a subset of (yyyy) (with century) styles.  
  
<sup>2</sup> The default values (*style***0** or **100**, **9** or **109**, **13** or **113**, **20** or **120**, and **21** or **121**) always return the century (yyyy).  
  
<sup>3</sup> Input when you convert to **datetime**; output when you convert to character data.  
  
<sup>4</sup> For conversion from **datetime** to character data, the output format is as described in the previous table.  
  
<sup>5</sup> Hijri is a calendar system with several variations. SQL Server PDW uses the Kuwaiti algorithm.  
  
> [!IMPORTANT]  
> By default, SQL Server PDW interprets two-digit years based on a cutoff year of 2049. That is, the two-digit year 49 is interpreted as 2049 and the two-digit year 50 is interpreted as 1950. Many client applications, such as those based on Automation objects, use a cutoff year of 2030. SQL Server PDW provides the **two digit year cutoff** configuration option that changes the cutoff year used by SQL Server PDW and allows for the consistent treatment of dates. We recommend specifying four-digit years.  
  
<sup>6</sup> Only supported when casting from character data to **datetime**. When character data that represents only date or only time components is cast to the **datetime** data types, the unspecified time component is set to 00:00:00.000, and the unspecified date component is set to 1900-01-01.  
  
<sup>7</sup>The optional time zone indicator, Z, is used to make it easier to map XML **datetime** values that have time zone information to SQL Server PDW **datetime** values that have no time zone. Z is the indicator for time zone UTC-0. Other time zones are indicated with HH:MM offset in the + or - direction. For example: `2006-12-12T23:45:12-08:00`.  
  
You can truncate unwanted date parts when you convert from **datetime** values by using an appropriate **char** or **varchar** data type length.  
  
### float  
When *expression* is **float**, *style* can be one of the values shown in the following table. Other values are processed as 0.  
  
|Value|Output|  
|---------|----------|  
|**0** (default)|A maximum of 6 digits. Use in scientific notation, when appropriate.|  
|**1**|Always 8 digits. Always use in scientific notation.|  
|**2**|Always 16 digits. Always use in scientific notation.|  
  
## General Remarks  
When you convert character expressions (**char**, **nchar**, **nvarchar**, or **varchar**) to an expression of a different data type, data can be truncated, only partially displayed, or an error is returned because the result is too short to display. Conversions to **char, nchar, nvarchar** or **varchar** are truncated, except for the conversions shown in the following table.  
  
|From data type|To data type|Result|  
|------------------|----------------|----------|  
|**int**, **smallint**, or **tinyint**|**char**|*|  
||**varchar**|*|  
||**nchar**|E|  
||**nvarchar**|E|  
|**decimal** or **float**|**char**|E|  
||**varchar**|E|  
||**nchar**|E|  
||**nvarchar**|E|  
  
\* = Result length too short to display. E = Error returned because result length is too short to display.  
  
SQL Server PDW guarantees that only "roundtrip conversions"--conversions that convert a data type from its original data type and back again--will yield the same values from version to version. The following example shows such a roundtrip conversion:  
  
```  
SELECT TOP(1) CAST(CAST('193.57' AS varchar(20)) AS decimal(10,5)) FROM dbo.DimCustomer;  
```  
  
or  
  
```  
SELECT TOP(1) CONVERT(decimal(10,5), CONVERT(varchar(20), '193.57')) FROM dbo.DimCustomer;  
```  
  
The following example shows a resulting expression that is too small to display.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName,  
       CAST(SickLeaveHours AS char(1)) AS SickLeave  
FROM dbo.DimEmployee  
WHERE NOT EmployeeKey > 5  
AND EndDate IS NULL;  
```  
  
Here is the result set.  
  
<pre>FirstName        LastName         Sick Leave  
---------------  ---------------  ----------  
Guy              Gilbert          *  
Kevin            Brown            *  
Roberto          Tamburello       *  
Rob              Walters          *</pre>  
  
When you convert data types that differ in decimal places, sometimes the result value is truncated and at other times it is rounded. The following table shows the behavior.  
  
|From|To|Behavior|  
|--------|------|------------|  
|**float**|**int**|Truncate|  
|**float**|**datetime**|Round|  
|**datetime**|**int**|Round|  
  
For example, the result of the following conversion is `10`:  
  
`SELECT TOP(1) CAST(10.6496 AS int) FROM dbo.DimCustomer;`  
  
When you convert data types in which the target data type has fewer decimal places than the source data type, the value is rounded. For example, the result of the following conversion is `10.3497`:  
  
`SELECT TOP(1) CAST(10.3496847 AS decimal (10, 4)) FROM dbo.DimCustomer;`  
  
SQL Server PDW returns an error message when nonnumeric **char**, **nchar**, **varchar**, or **nvarchar** data is converted to **int**, **float**, or **decimal**.  
  
### Implicit Conversions  
Implicit conversions are those conversions that occur without specifying the CAST or CONVERT functions. Explicit conversions are those conversions that require the CAST or CONVERT function to be specified. See "Data Type Conversion" in [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md) for information on all explicit and implicit data type conversions that are allowed for SQL Server PDW data types.  
  
## Examples  
  
### A. Using CAST and CONVERT  
This example retrieves the name of the product for those products that have a `3` in the first digit of their list price and converts their `ListPrice` to **int**.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EnglishProductName AS ProductName, ListPrice  
FROM dbo.DimProduct  
WHERE CAST(ListPrice AS int) LIKE '3%';  
```  
  
This example shows the same query, using CONVERT instead of CAST.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EnglishProductName AS ProductName, ListPrice  
FROM dbo.DimProduct  
WHERE CONVERT(int, ListPrice) LIKE '3%';  
```  
  
### B. Using CAST with arithmetic operators  
The following example calculates a single column computation by dividing the product unit price (`UnitPrice`) by the discount percentage (`UnitPriceDiscountPct`). This result is converted to an `int` data type after being rounded to the nearest whole number.  
  
```  
USE AdventureWorksPDW2012   
  
SELECT ProductKey, UnitPrice,UnitPriceDiscountPct,  
       CAST(ROUND (UnitPrice*UnitPriceDiscountPct,0) AS int) AS DiscountPrice  
FROM dbo.FactResellerSales  
WHERE SalesOrderNumber = 'SO47355'   
      AND UnitPriceDiscountPct > .02  
```  
  
Here is the result set.  
  
<pre>ProductKey  UnitPrice  UnitPriceDiscountPct  DiscountPrice  
----------  ---------  --------------------  -------------  
323         430.6445   0.05                  22  
213         18.5043    0.05                  1  
456         37.4950    0.10                  4  
456         37.4950    0.10                  4  
216         18.5043    0.05                  1</pre>  
  
### C. Using CAST to concatenate  
The following example concatenates noncharacter expressions by using CAST.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT 'The list price is ' + CAST(ListPrice AS varchar(12)) AS ListPrice  
FROM dbo.DimProduct  
WHERE ListPrice BETWEEN 350.00 AND 400.00;  
```  
  
Here is the result set.  
  
<pre>ListPrice  
------------------------  
The list price is 357.06  
The list price is 364.09  
The list price is 364.09  
The list price is 364.09  
The list price is 364.09</pre>  
  
### D. Using CAST to produce more readable text  
The following example uses CAST in the SELECT list to convert the `Name` column to a **char(10)** column.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT DISTINCT CAST(EnglishProductName AS char(10)) AS Name, ListPrice  
FROM dbo.DimProduct  
WHERE EnglishProductName LIKE 'Long-Sleeve Logo Jersey, M';  
```  
  
Here is the result set.  
  
<pre>Name        UnitPrice  
----------  ---------  
Long-Sleev  31.2437  
Long-Sleev  32.4935  
Long-Sleev  49.99</pre>  
  
### E. Using CAST with the LIKE clause  
The following example converts the **money** column `ListPrice` to an **int** type and then to a **char(20)** type so that it can be used with the LIKE clause.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT EnglishProductName AS Name, ListPrice  
FROM dbo.DimProduct  
WHERE CAST(CAST(ListPrice AS int) AS char(20)) LIKE '2%';  
```  
  
### G. Using CAST and CONVERT with datetime data  
The following example displays the current date and time, uses CAST to change the current date and time to a character data type, and then uses CONVERT display the date and time in the ISO 8601 format.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT TOP(1)  
   SYSDATETIME() AS UnconvertedDateTime,  
   CAST(SYSDATETIME() AS nvarchar(30)) AS UsingCast,  
   CONVERT(nvarchar(30), SYSDATETIME(), 126) AS UsingConvertTo_ISO8601  
FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>UnconvertedDateTime         UsingCast                      UsingConvertTo_ISO8601  
-----------------------     ------------------------------ ------------------------------  
07/20/2010 1:44:31 PM       2010-07-20 13:44:31.5879025    2010-07-20T13:44:31.5879025</pre>  
  
The following example is approximately the opposite of the previous example. The example displays a date and time as character data, uses CAST to change the character data to the **datetime** data type, and then uses CONVERT to change the character data to the **datetime** data type.  
  
```  
USE AdventureWorksPDW2012;   
  
SELECT TOP(1)   
   '2010-07-25T13:50:38.544' AS UnconvertedText,  
CAST('2010-07-25T13:50:38.544' AS datetime) AS UsingCast,  
   CONVERT(datetime, '2010-07-25T13:50:38.544', 126) AS UsingConvertFrom_ISO8601  
FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>UnconvertedText         UsingCast               UsingConvertFrom_ISO8601  
----------------------- ----------------------- ------------------------  
2010-07-25T13:50:38.544 07/25/2010 1:50:38 PM   07/25/2010 1:50:38 PM</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
  
