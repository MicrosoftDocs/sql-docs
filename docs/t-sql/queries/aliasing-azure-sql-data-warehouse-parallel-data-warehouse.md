---
title: "Aliasing"
titleSuffix: Azure Synapse Analytics
description: "Aliasing in Azure Synapse Analytics and Parallel Data Warehouse."
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
monikerRange: "=azure-sqldw-latest"
---

# Aliasing (Azure Synapse Analytics)

[!INCLUDE [applies-to-version/asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/asa-pdw-fabricse-fabricdw.md)]

Aliasing allows the temporary substitution of a short and easy-to-remember string in place of a table or column name in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] [!INCLUDE [DWsql](../../includes/dwsql-md.md)] queries. Table aliases are often used in `JOIN` queries because the `JOIN` syntax requires fully qualified object names when referencing columns.  

Aliases must be single words conforming to object naming rules. For more information, see [Database identifiers](../../relational-databases/databases/database-identifiers.md). Aliases cannot contain blank spaces and cannot be enclosed in either single or double quotes.  

## Syntax

```syntaxsql
object_source [ AS ] alias
```

## Arguments

#### *object_source*  
The name of the source table or column.  

#### AS
An optional alias preposition. When working with range variable aliasing, the AS keyword is prohibited.  

#### *alias*

The desired temporary reference name for the table or column. Any valid object name can be used.

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

The following example shows a query with multiple joins. Both table and column aliasing are demonstrated in this example. These examples use the [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md). 

- Column Aliasing: Both columns and expressions involving columns in the select list are aliased in this example. `SalesTerritoryRegion AS SalesTR` demonstrates a simple column alias. `Sum(SalesAmountQuota) AS TotalSales` demonstrates  

- Table Aliasing: `dbo.DimSalesTerritory AS st` shows creation of the `st` alias for the `dbo.DimSalesTerritory` table.  

```sql
-- Uses AdventureWorks

SELECT LastName, SUM(SalesAmountQuota) AS TotalSales, SalesTerritoryRegion AS SalesTR,  
    RANK() OVER (PARTITION BY SalesTerritoryRegion ORDER BY SUM(SalesAmountQuota) DESC ) AS RankResult  
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.FactSalesQuota AS sq ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory AS st ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE SalesPersonFlag = 1 AND SalesTerritoryRegion != N'NA'  
GROUP BY LastName, SalesTerritoryRegion;  
```

The `AS` keyword can be excluded, but is often included for readability.  

```sql
-- Uses AdventureWorks

SELECT LastName, SUM(SalesAmountQuota) TotalSales, SalesTerritoryRegion SalesTR,  
RANK() OVER (PARTITION BY SalesTerritoryRegion ORDER BY SUM(SalesAmountQuota) DESC ) RankResult  
FROM dbo.DimEmployee e  
INNER JOIN dbo.FactSalesQuota sq ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory st ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE SalesPersonFlag = 1 AND SalesTerritoryRegion != N'NA'  
GROUP BY LastName, SalesTerritoryRegion;  
```

## Related content

- [SELECT (Transact-SQL)](select-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
