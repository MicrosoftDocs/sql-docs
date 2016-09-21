---
title: "GROUP BY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f0df272c-671a-4c8f-a5ff-568906ea4b2b
caps.latest.revision: 24
author: BarbKess
---
# GROUP BY (SQL Server PDW)
The GROUP BY clause is used within a SELECT statement in SQL Server PDW to group the selected rows into summary rows. One row is returned for each group.  
  
For the SELECT statement, see [SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
GROUP BY <group_by_item> [ ,...n ]  
  
<group_by_item>::=  
    column_name [ WITH (DISTRIBUTED_AGG) ]  
    | column_expression  
```  
  
## Arguments  
*column_name*  
The column name on which the grouping operation is performed.  
  
*expression*  
The expression on which the grouping operation is performed. The expression can contain column names, derived tables, and view names. Columns do not need to appear in the select list of the SELECT clause. Views must be referenced in the FROM clause.  
  
WITH (DISTRIBUTED_AGG)  
The DISTRIBUTED_AGG query hint forces SQL Server PDW to redistribute a table on a specific column before performing an aggregation. Only one column in the GROUP BY clause can have a DISTRIBUTED_AGG query hint. After the query finishes, the redistributed table is dropped. The original table is not changed.  
  
## General Remarks  
GROUP BY:  
  
-   If aggregate functions are included in the SELECT clause <select list>, GROUP BY calculates a summary value for each group.  
  
-   Rows that do not meet the conditions of the WHERE clause are removed before any grouping operation is performed.  
  
-   The [HAVING &#40;SQL Server PDW&#41;](../sqlpdw/having-sql-server-pdw.md) clause is used with the GROUP BY clause to filter groups in the result set.  
  
-   The GROUP BY clause does not order the result set. Use the ORDER BY clause to order the result set.  
  
-   If a grouping column contains null values, all null values are considered equal and they are collected into a single group.  
  
DISTRIBUTED_AGG query hint:  
  
-   The DISTRIBUTED_AGG query hint is provided for backwards compatibility and will not improve performance for most queries. By default, SQL Server PDW already redistributes data as necessary to improve performance for aggregations.  
  
## Examples  
  
### A. Basic use of the GROUP BY clause  
The following example finds the total amount for all sales on each day. One row containing the sum of all sales is returned for each day.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales FROM FactInternetSales  
GROUP BY OrderDateKey ORDER BY OrderDateKey;  
```  
  
### B. Basic use of the DISTRIBUTED_AGG hint  
This example uses the DISTRIBUTED_AGG query hint to force the appliance to shuffle the table on the `CustomerKey` column before performing the aggregation.  
  
```  
USE AdventureWorksPDW2012;  
SELECT CustomerKey, SUM(SalesAmount) AS sas  
FROM FactInternetSales  
GROUP BY CustomerKey WITH (DISTRIBUTED_AGG)  
ORDER BY CustomerKey DESC;  
```  
  
### C. Syntax Variations for GROUP BY  
When the select list has no aggregations, each column in the select list must be included in the GROUP BY list. Computed columns in the select list can be listed, but are not required, in the GROUP BY list. These are examples of syntactically valid SELECT statements:  
  
```  
USE AdventureWorksPDW2012;  
SELECT LastName, FirstName FROM DimCustomer GROUP BY LastName, FirstName;  
SELECT NumberCarsOwned FROM DimCustomer GROUP BY YearlyIncome, NumberCarsOwned;  
SELECT (SalesAmount + TaxAmt + Freight) AS TotalCost FROM FactInternetSales GROUP BY SalesAmount, TaxAmt, Freight;  
SELECT SalesAmount, SalesAmount*1.10 SalesTax FROM FactInternetSales GROUP BY SalesAmount;  
SELECT SalesAmount FROM FactInternetSales GROUP BY SalesAmount, SalesAmount*1.10;  
```  
  
### D. Using a GROUP BY with multiple GROUP BY expressions  
The following example groups results using multiple `GROUP BY` criteria. If, within each `OrderDateKey` group, there are subgroups that can be differentiated by `DueDateKey`, a new grouping will be defined for the result set.  
  
```  
USE AdventureWorksPDW2012;  
SELECT OrderDateKey, DueDateKey, SUM(SalesAmount) AS TotalSales   
FROM FactInternetSalesGROUP BY OrderDateKey, DueDateKey   
ORDER BY OrderDateKey;  
```  
  
### E. Using a GROUP BY clause with a HAVING clause  
The following example uses the `HAVING` clause to specify the groups generated in the `GROUP BY` clause that should be included in the result set. Only those groups with order dates in 2004 or later will be included in the results.  
  
```  
USE AdventureWorksPDW2012;  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales   
FROM FactInternetSales  
GROUP BY OrderDateKey   
HAVING OrderDateKey > 20040000   
ORDER BY OrderDateKey;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md)  
  
