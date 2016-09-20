---
title: "ISNULL (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9875dc23-9352-44a1-890c-136b469dda47
caps.latest.revision: 23
author: BarbKess
---
# ISNULL (SQL Server PDW)
Replaces NULL with the specified replacement value in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ISNULL (check_expression ,replacement_value )  
```  
  
## Arguments  
*check_expression*  
The expression to be checked for NULL. *check_expression* can be of any type.  
  
*replacement_value*  
The expression to be returned if *check_expression* is NULL. *replacement_value* must be of a type that is implicitly convertible to the type of *check_expresssion*.  
  
## Return Types  
Returns the same type as *check_expression*.  
  
## Remarks  
The value of *check_expression* is returned if it is not NULL; otherwise, *replacement_value* is returned after it is implicitly converted to the type of *check_expression*, if the types are different.  
  
Do not use ISNULL to find NULL values. Use IS NULL instead.  
  
## Examples  
  
### A. Using ISNULL with AVG  
The following example finds the average of the weight of all products in a sample table. It substitutes the value `50` for all NULL entries in the `Weight` column of the `Product` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT AVG(ISNULL(Weight, 50))  
FROM dbo.DimProduct;  
```  
  
Here is the result set.  
  
```  
--------------------------   
52.88  
```  
  
### B. Using ISNULL  
The following example uses ISNULL to test for NULL values in the column `MinPaymentAmount` and display the value `0.00` for those rows.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ResellerName,   
       ISNULL(MinPaymentAmount,0) AS MinimumPayment  
FROM dbo.DimReseller  
ORDER BY ResellerName;  
```  
  
Here is a partial result set.  
  
<pre>ResellerName               MinimumPayment  
-------------------------  --------------  
A Bicycle Association         0.0000  
A Bike Store                  0.0000  
A Cycle Shop                  0.0000  
A Great Bicycle Company       0.0000  
A Typical Bike Shop         200.0000  
Acceptable Sales & Service    0.0000</pre>  
  
### C. Using IS NULL to test for NULL in a WHERE clause  
The following example finds all products that have `NULL` in the `Weight` column. Note the space between `IS` and `NULL`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EnglishProductName, Weight  
FROM dbo.DimProduct  
WHERE Weight IS NULL;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[NULLIF &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/nullif-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/where-sql-server-pdw.md)  
  
