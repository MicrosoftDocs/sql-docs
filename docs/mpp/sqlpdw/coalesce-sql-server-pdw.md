---
title: "COALESCE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 83fe2ec8-4dca-4d77-ad99-f08b777a9fe5
caps.latest.revision: 19
author: BarbKess
---
# COALESCE (SQL Server PDW)
Returns the first non-null expression among its arguments in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
COALESCE ( expression [ ,...n ] )  
```  
  
## Arguments  
*expression*  
An expression of any type.  
  
## Return Types  
Returns the data type of *expression* with the highest data type precedence. If all expressions are non-nullable, the result is typed as non-nullable. If all arguments are null, COALESCE returns null. At least one of the null values must be a typed null.  
  
## General Remarks  
COALESCE(*expression1,...n*) is equivalent to the following [CASE](../../mpp/sqlpdw/case-sql-server-pdw.md) expression:  
  
```  
CASE  
   WHEN (expression1 IS NOT NULL) THEN expression1  
   ...  
   WHEN (expressionN IS NOT NULL) THEN expressionN  
   ELSE NULL  
END  
```  
  
[ISNULL](../../mpp/sqlpdw/isnull-sql-server-pdw.md) and COALESCE, though closely related, can behave differently. An expression involving ISNULL with non-null parameters is considered to be NOT NULL, while expressions involving COALESCE with non-null parameters is considered to be NULL.  
  
## Examples  
  
### Simple Example  
The following example demonstrates how COALESCE selects the data from the first column that has a non-null value. Assume for this example that the `Products` table contains this data:  
  
<pre>Name         Color      ProductNumber  
------------ ---------- -------------  
Socks, Mens  NULL       PN1278  
Socks, Mens  Blue       PN1965  
NULL         White      PN9876</pre>  
  
We then run the following COALESCE query:  
  
```  
SELECT Name, Color, ProductNumber, COALESCE(Color, ProductNumber) AS FirstNotNull   
FROM Products ;  
```  
  
Here is the result set.  
  
<pre>Name         Color      ProductNumber  FirstNotNull  
------------ ---------- -------------  ------------  
Socks, Mens  NULL       PN1278         PN1278  
Socks, Mens  Blue       PN1965         Blue  
NULL         White      PN9876         White</pre>  
  
Notice that in the first row, the `FirstNotNull` value is “PN1278”, not “Socks, Mens”. This is because the `Name` column was not specified as a parameter for COALESCE in the example.  
  
### Complex Example  
The following example uses COALESCE to compare the values in three columns  and return only the non-null value found in the columns.  
  
```  
CREATE TABLE dbo.wages  
(  
    emp_id        tinyint   NULL,  
    hourly_wage   decimal   NULL,  
    salary        decimal   NULL,  
    commission    decimal   NULL,  
    num_sales     tinyint   NULL  
);  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (1, 10.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (2, 20.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (3, 30.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (4, 40.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (5, NULL, 10000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (6, NULL, 20000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (7, NULL, 30000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (8, NULL, 40000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (9, NULL, NULL, 15000, 3);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (10,NULL, NULL, 25000, 2);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (11, NULL, NULL, 20000, 6);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (12, NULL, NULL, 14000, 4);  
  
SELECT CAST(COALESCE(hourly_wage * 40 * 52,   
   salary,   
   commission * num_sales) AS decimal(10,2)) AS TotalSalary   
FROM dbo.wages  
ORDER BY TotalSalary;  
```  
  
Here is the result set.  
  
<pre>Total Salary  
------------  
10000.00  
20000.00  
20800.00  
30000.00  
40000.00  
41600.00  
45000.00  
50000.00  
56000.00  
62400.00  
83200.00  
120000.00</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[CASE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/case-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md)  
  
