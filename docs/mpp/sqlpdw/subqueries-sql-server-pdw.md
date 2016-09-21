---
title: "Subqueries (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e1da7806-bddc-44a4-92bc-c75fd07e9dbd
caps.latest.revision: 33
author: BarbKess
---
# Subqueries (SQL Server PDW)
This topic gives examples of using subqueries in SQL Server PDW.  
  
For the SELECT statement, see [SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
  
## Contents  
  
-   [Basics](#Basics)  
  
-   [Examples](#Examples)  
  
## <a name="Basics"></a>Basics  
Subquery  
A subquery is a query that is nested inside a SELECT, INSERT, UPDATE, or DELETE statement, or inside another subquery. This is also called an inner query or inner select.  
  
Outer query  
The statement that contains the subquery. This is also called an outer select.  
  
Correlated subquery  
A subquery that refers to a table in the outer query.  
  
## <a name="Examples"></a>Examples  
This section provides examples of subqueries supported in SQL Server PDW.  
  
### A. TOP and ORDER BY in a subquery  
  
```  
SELECT * FROM tblA  
WHERE col1 IN  
    (SELECT TOP 100 col1 FROM tblB ORDER BY col1);  
```  
  
### B. HAVING clause with a correlated subquery  
  
```  
SELECT dm.EmployeeKey, dm.FirstName, dm.LastName   
FROM DimEmployee AS dm   
GROUP BY dm.EmployeeKey, dm.FirstName, dm.LastName  
HAVING 5000 <=   
(SELECT sum(OrderQuantity)  
FROM FactResellerSales AS frs  
WHERE dm.EmployeeKey = frs.EmployeeKey)  
ORDER BY EmployeeKey;  
```  
  
### C. Correlated subqueries with analytics  
  
```  
SELECT * FROM ReplA AS A   
WHERE A.ID IN   
    (SELECT sum(B.ID2) OVER() FROM ReplB AS B WHERE A.ID2 = B.ID);  
```  
  
### D. Correlated union statements in a subquery  
  
```  
SELECT * FROM RA   
WHERE EXISTS   
    (SELECT 1 FROM RB WHERE RB.b1 = RA.a1   
     UNION ALL SELECT 1 FROM RC);  
```  
  
### E. Join predicates in a subquery  
  
```  
SELECT * FROM RA INNER JOIN RB   
    ON RA.a1 = (SELECT COUNT(*) FROM RC);  
```  
  
### F. Correlated join predicates in a subquery  
  
```  
SELECT * FROM RA   
    WHERE RA.a2 IN   
    (SELECT 1 FROM RB INNER JOIN RC ON RA.a1=RB.b1+RC.c1);  
```  
  
### G. Correlated subselects as data sources  
  
```  
SELECT * FROM RA   
    WHERE 3 = (SELECT COUNT(*)   
        FROM (SELECT b1 FROM RB WHERE RB.b1 = RA.a1) X);  
```  
  
### H. Correlated subqueries in the data values  used with aggregates  
  
```  
SELECT Rb.b1, (SELECT RA.a1 FROM RA WHERE RB.b1 = RA.a1) FROM RB GROUP BY RB.b1;  
```  
  
### I. Using IN with a correlated subquery  
The following example uses `IN` in a correlated, or repeating, subquery. This is a query that depends on the outer query for its values. The inner query is run repeatedly, one time for each row that may be selected by the outer query. This query retrieves one instance of the `EmployeeKey` plus first and last name of each employee for which the `OrderQuantity` in the `FactResellerSales` table is `5` and for which the employee identification numbers match in the `DimEmployee` and `FactResellerSales` tables.  
  
```  
SELECT DISTINCT dm.EmployeeKey, dm.FirstName, dm.LastName   
FROM DimEmployee AS dm   
WHERE 5 IN   
    (SELECT OrderQuantity  
    FROM FactResellerSales AS frs  
    WHERE dm.EmployeeKey = frs.EmployeeKey)  
ORDER BY EmployeeKey;  
```  
  
### J. Using EXISTS versus IN with a subquery  
The following example shows queries that are semantically equivalent to illustrate the difference between using the `EXISTS` keyword and the `IN` keyword. Both are examples of a subquery that retrieves one instance of each product name for which the product subcategory is `Road Bikes`. `ProductSubcategoryKey` matches between the `DimProduct` and `DimProductSubcategory` tables.  
  
```  
SELECT DISTINCT EnglishProductName  
FROM DimProduct AS dp   
WHERE EXISTS  
    (SELECT *  
     FROM DimProductSubcategory AS dps   
     WHERE dp.ProductSubcategoryKey = dps.ProductSubcategoryKey  
           AND dps.EnglishProductSubcategoryName = 'Road Bikes')  
ORDER BY EnglishProductName;  
```  
  
Or  
  
```  
SELECT DISTINCT EnglishProductName  
FROM DimProduct AS dp   
WHERE dp.ProductSubcategoryKey IN  
    (SELECT ProductSubcategoryKey  
     FROM DimProductSubcategory   
     WHERE EnglishProductSubcategoryName = 'Road Bikes')  
ORDER BY EnglishProductName;  
```  
  
### K. Using multiple correlated subqueries  
This example uses two correlated subqueries to find the names of employees who have sold a particular product.  
  
```  
SELECT DISTINCT LastName, FirstName, e.EmployeeKey  
FROM DimEmployee e JOIN FactResellerSales s ON e.EmployeeKey = s.EmployeeKey  
WHERE ProductKey IN  
(SELECT ProductKey FROM DimProduct WHERE ProductSubcategoryKey IN  
(SELECT ProductSubcategoryKey FROM DimProductSubcategory   
 WHERE EnglishProductSubcategoryName LIKE '%Bikes'))  
ORDER BY LastName  
;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
