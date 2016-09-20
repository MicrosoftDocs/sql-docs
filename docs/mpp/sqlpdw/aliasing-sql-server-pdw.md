---
title: "Aliasing (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 134e2f7b-c055-4c48-bea9-3d1a695263f9
caps.latest.revision: 17
author: BarbKess
---
# Aliasing (SQL Server PDW)
Aliasing allows the temporary substitution of a short and easy-to-remember string in place of a table or column name in SQL Server PDWSQL queries. Table aliases are often used in JOIN queries because the JOIN syntax requires fully qualified object names when referencing columns.  
  
Aliases must be single words conforming to object naming rules. For more information, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md). Aliases cannot contain blank spaces and cannot be enclosed in either single or double quotes.  
  
## Syntax  
  
```  
object_source [AS ] alias  
```  
  
## Arguments  
*object_source*  
The name of the source table or column.  
  
AS  
An optional alias preposition. When working with range variable aliasing, the AS keyword is prohibited.  
  
*alias*  
The desired temporary reference name for the table or column. Any valid object name can be used. For more information, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
## Examples  
The following example shows a query with multiple joins. Both table and column aliasing are demonstrated in this example.  
  
-   Column Aliasing: Both columns and expressions involving columns in the select list are aliased in this example. `SalesTerritoryRegion AS SalesTR` demonstrates a simple column alias. `Sum(SalesAmountQuota) AS TotalSales` demonstrates  
  
-   Table Aliasing: `dbo.DimSalesTerritory AS st` shows creation of the `st` alias for the `dbo.DimSalesTerritory` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LastName, SUM(SalesAmountQuota) AS TotalSales, SalesTerritoryRegion AS SalesTR,  
    RANK() OVER (PARTITION BY SalesTerritoryRegion ORDER BY SUM(SalesAmountQuota) DESC ) AS RankResult  
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.FactSalesQuota AS sq ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory AS st ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE SalesPersonFlag = 1 AND SalesTerritoryRegion != N'NA'  
GROUP BY LastName, SalesTerritoryRegion;  
```  
  
The AS keyword can be excluded, as shown below, but is often included for readability.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LastName, SUM(SalesAmountQuota) TotalSales, SalesTerritoryRegion SalesTR,  
RANK() OVER (PARTITION BY SalesTerritoryRegion ORDER BY SUM(SalesAmountQuota) DESC ) RankResult  
FROM dbo.DimEmployee e  
INNER JOIN dbo.FactSalesQuota sq ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory st ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE SalesPersonFlag = 1 AND SalesTerritoryRegion != N'NA'  
GROUP BY LastName, SalesTerritoryRegion;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md)  
[INSERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/insert-sql-server-pdw.md)  
[UPDATE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/update-sql-server-pdw.md)  
  
