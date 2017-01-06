---
title: "FROM (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d0f868a9-a9a9-4c85-be9d-23feea2959ee
caps.latest.revision: 48
author: BarbKess
---
# FROM (SQL Server PDW)
Specifies the tables, views, and joined tables that are used in the SQL Server PDW DELETE, SELECT, and UPDATE statements for SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
FROM { <table_source> [ ,...n ] }  
  
<table_source> ::=   
{  
    [ database_name . [ schema_name ] . | schema_name . ] table_or_view_name [ AS ] table_or_view_alias  
    | derived_table [ AS ] table_alias [ ( column_alias [ ,...n ] ) ]  
    | <joined_table>  
}  
  
<joined_table> ::=   
{  
    <table_source> <join_type> <table_source> ON search_condition   
    | <table_source> CROSS JOIN <table_source>     | left_table_source { CROSS | OUTER } APPLY right_table_source   
    | [ ( ] <joined_table> [ ) ]   
}  
  
<join_type> ::=   
    [ INNER ] [ <join hint> ] JOIN  
    | LEFT  [ OUTER ] JOIN  
    | RIGHT [ OUTER ] JOIN  
    | FULL  [ OUTER ] JOIN  
  
<join_hint> ::=   
    REDUCE  
    | REPLICATE  
    | REDISTRIBUTE  
```  
  
## Arguments  
<table_source>  
Specifies a table or view (with or without an alias) to use in the SQL statement. Up to 256 table sources can be used in a statement, although the limit varies depending on available memory and the complexity of other expressions in the query. Individual queries may not support up to 256 table sources.  
  
> [!NOTE]  
> Query performance can decrease when many tables are referenced in a query. Query performance is also affected by additional factors, such as the presence of indexes and indexed views on each table source, the size of the select list in the SELECT statement, and whether the query contains complex joins.  
  
*table_source* can be a one, two, or three part name, specifying the table/view, schema, and database. Fully qualifying the object name can slightly improve performance by reducing the need to resolve the schema, can eliminate errors from assuming a default schema, and can make your code more readable.  
  
The order of table sources after the FROM keyword does not affect the result set that is returned. SQL Server PDW returns errors when duplicate names appear in the FROM clause.  
  
*table_or_view_alias*  
The alias used for the table or view name.  
  
*derived _table*  
A subquery that retrieves rows from the database. *derived_table* is used as input to the outer query.  
  
<joined_table>  
A result set that is the product of two or more tables. For multiple joins, use parentheses to change the natural order of the joins.  
  
<join_type>  
Specifies the type of join operation.  
  
[ **INNER** ] [ <join_hint> ] JOIN  
Returns all matching pairs of rows. Discards unmatched rows from both tables. When no join type is specified, this is the default.  
  
FULL [ OUTER ] JOIN  
Returns a row from either the left or right table that does not meet the join condition, and sets to NULL the output columns that correspond to the other table. This is in addition to all rows typically returned by inner join.  
  
LEFT [ OUTER ] JOIN  
Returns all rows from the left table not meeting the join condition. Sets to NULL the output columns from the other table and all rows returned by the inner join.  
  
RIGHT [OUTER] JOIN  
Returns all rows from the right table not meeting the join condition. Sets to NULL the output columns that correspond to the other table and all rows returned by the inner join.  
  
ON *search_condition*  
Specifies the condition on which the join is based. The condition can specify any predicate, although columns and comparison operators are frequently used.  
  
For more information about search conditions and predicates, see [Search Condition &#40;SQL Server PDW&#41;](../sqlpdw/search-condition-sql-server-pdw.md).  
  
CROSS JOIN  
Returns every possible combination of data from all joined tables regardless of relationship. If table1 has 100 rows and table2 has 5 rows, the result set will have 500 rows. Cross-joins are discouraged because the result set can grow unmanageably large.  
  
*left_table_source* { CROSS | OUTER } APPLY *right_table_source*  
Specifies that the *right_table_source* of the APPLY operator is evaluated against every row of the *left_table_source*. This functionality is useful when the *right_table_source* contains a table-valued function that takes column values from the *left_table_source* as one of its arguments.  
  
Either CROSS or OUTER must be specified with APPLY. When CROSS is specified, no rows are produced when the *right_table_source* is evaluated against a specified row of the *left_table_source* and returns an empty result set.  
  
When OUTER is specified, one row is produced for each row of the *left_table_source* even when the *right_table_source* evaluates against that row and returns an empty result set.  
  
For more information, see the Remarks section.  
  
*join_hint*  
These join hints apply to INNER joins on two distribution incompatible columns. They can improve query performance by restricting the amount of data movement that occurs during query processing.  
  
REDUCE  
Reduces the number of rows to be moved for the table on the right side of the join in order to make two distribution incompatible tables compatible. The REDUCE hint is also called a semi-join hint.  
  
REPLICATE  
Causes the values in the joining column from the table on the left side of the join to be replicated to all nodes. The table on the right is joined to the replicated version of those columns.  
  
REDISTRIBUTE  
Forces two data sources to be distributed on columns specified in the JOIN clause. For a distributed table, SQL Server PDW will perform a shuffle move. For a replicated table, SQL Server PDW will perform a trim move. To understand these move types, see the DMS Query Plan Operations section in [Understanding Query Plans &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-plans-sql-server-pdw.md). This hint can improve performance when the query plan is using a broadcast move to resolve a distribution incompatible join.  
  
## Error Handling  
The following error occurs if SQL Server PDW or SQL Server cannot honor the query hint:  
  
`Query processor could not produce a query plan because of the hints defined in this query.`  
  
## General Remarks  
The FROM clause is always required in the SELECT statement, even when the select list contains only constants and arithmetic expressions, and does not contain column names.  
  
To join columns of incompatible data types, use an explicit cast to make the columns compatible.  
  
All of the join tables in the ON clause do not need to be referenced in the SELECT list of the SELECT statement.  
  
For outer joins, the location of the join predicates can cause different query results. This is because join predicates in the ON clause of the join are applied before the join, and join predicates in the WHERE clause are applied to the result of the join.  
  
NULL values are not equal when compared in join predicates. For example, an outer join will return each row with a NULL value in the outer table of the join predicate.  
  
**REDISTRIBUTE:**  
  
The REDISTRIBUTE hint has the following behaviors according to the join predicates in the ON clause.  
  
1.  Distribution compatible join – a join between *two distribution columns*:  
  
    -   REDISTRIBUTE hint is ignored.  
  
2.  Distribution incompatible inner join between *a distribution column and a non-distribution column*:  
  
    -   When there is only one join predicate in the ON clause, the REDISTRIBUTE hint forces a Shuffle move on the non-distribution column.  
  
    -   When the ON clause combines multiple join predicates with the AND operator, the REDISTRIBUTE hint forces a Shuffle move on the non-distribution column in the first expression.  
  
3.  Distribution incompatible inner join between *two non-distribution columns*:  
  
    -   When there is only one join predicate in the ON clause, the REDISTRIBUTE hint forces a Shuffle move on both of the non-distribution columns.  
  
    -   When the ON clause combines multiple join predicates with the AND operator, the REDISTRIBUTE hint forces a Shuffle move on *both* of the non-distribution columns in the first predicate.  
  
## Limitations and Restrictions  
There are limitations for using joins in the UDPATE and DELETE statements. For more information, see [UPDATE &#40;SQL Server PDW&#41;](../sqlpdw/update-sql-server-pdw.md) and [DELETE &#40;SQL Server PDW&#41;](../sqlpdw/delete-sql-server-pdw.md).  
  
Only one of the join hints (REDUCE, REPLICATE, REDISTRIBUTE) is allowed in the ON clause. A join hint and the DISTRIBUTED_AGG query hint can exist together in the same query.  
  
The INNER keyword must be specified when using a REDUCE, REPLICATE, or REDISTRIBUTE join hint.  
  
For the REDUCE or REDISTRIBUTE hints, the join predicate must be an equijoin; it must only use the equality operator (=) to compare values.  
  
For the REDUCE hint, the ON clause must include one and only one column from each data source in the join. To specify additional join predicates, use the WHERE clause.  
  
## Security  
  
### Permissions  
Requires permissions for the DELETE, SELECT, or UPDATE statement.  
  
## Examples  
  
### A. Using a simple FROM clause  
The following example retrieves the `SalesTerritoryID` and `SalesTerritoryRegion` columns from the `DimSalesTerritory` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT SalesTerritoryKey, SalesTerritoryRegion  
FROM DimSalesTerritory  
ORDER BY SalesTerritoryKey;  
```  
  
### B. Using the INNER JOIN syntax  
The following example returns the `SalesOrderNumber`, `ProductKey`, and `EnglishProductName` columns from the `FactInternetSales` and `DimProduct` tables where the join key, `ProductKey`, matches in both tables. The `SalesOrderNumber` and `EnglishProductName` columns each exist in one of the tables only, so it is not necessary to specify the table alias with these columns, as is shown; these aliases are included for readability. The word **AS** before an alias name is not required but is recommended for readability and to conform to the ANSI standard.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis INNER JOIN DimProduct AS dp  
ON dp.ProductKey = fis.ProductKey;  
```  
  
Since the `INNER` keyword is not required for inner joins, this same query could be written as:  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales fis JOIN DimProduct dp  
ON dp.ProductKey = fis.ProductKey;  
```  
  
A `WHERE` clause could also be used with this query to limit results. This example limits results to `SalesOrderNumber` values higher than ‘SO5000’:  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis JOIN DimProduct AS dp  
ON dp.ProductKey = fis.ProductKey  
WHERE fis.SalesOrderNumber > 'SO50000'  
ORDER BY fis.SalesOrderNumber;  
```  
  
### C. Using the LEFT OUTER JOIN and RIGHT OUTER JOIN syntax  
The following example joins the `FactInternetSales` and `DimProduct` tables on the `ProductKey` columns. The left outer join syntax preserves the unmatched rows from the left (`FactInternetSales`) table. Since the `FactInternetSales` table does not contain any `ProductKey` values that do not match the `DimProduct` table, this query returns the same rows as the first inner join example above.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis LEFT OUTER JOIN DimProduct AS dp  
ON dp.ProductKey = fis.ProductKey;  
```  
  
This query could also be written without the `OUTER` keyword.  
  
In right outer joins, the unmatched rows from the right table are preserved. The following example returns the same rows as the left outer join example above.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM DimProduct AS dp RIGHT OUTER JOIN FactInternetSales AS fis  
ON dp.ProductKey = fis.ProductKey;  
```  
  
The following query uses the `DimSalesTerritory` table as the left table in a left outer join. It retrieves the `SalesOrderNumber` values from the `FactInternetSales` table. If there are no orders for a particular `SalesTerritoryKey`, the query will return a NULL for the `SalesOrderNumber` for that row. This query is ordered by the `SalesOrderNumber` column, so that any NULLs in this column will appear at the top of the results.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst LEFT OUTER JOIN FactInternetSales AS fis  
ON dst.SalesTerritoryKey = fis.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
This query could be rewritten with a right outer join to retrieve the same results:  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM FactInternetSales AS fis RIGHT OUTER JOIN DimSalesTerritory AS dst  
ON fis.SalesTerritoryKey = dst.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
### D. Using the FULL OUTER JOIN syntax  
The following example demonstrates a full outer join, which returns all rows from both joined tables but returns NULL for values that do not match from the other table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst FULL OUTER JOIN FactInternetSales AS fis  
ON dst.SalesTerritoryKey = fis.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
This query could also be written without the `OUTER` keyword.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst FULL JOIN FactInternetSales AS fis  
ON dst.SalesTerritoryKey = fis.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
### E. Using the CROSS JOIN syntax  
The following example returns the cross-product of the `FactInternetSales` and `DimSalesTerritory` tables. A list of all possible combinations of `SalesOrderNumber` and  `SalesTerritoryKey` are returned. Notice the absence of the `ON` clause in the cross join query.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT dst.SalesTerritoryKey, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst CROSS JOIN FactInternetSales AS fis  
ORDER BY fis.SalesOrderNumber;  
```  
  
### F. Using a derived table  
The following example uses a derived table (a `SELECT` statement after the `FROM` clause) to return the `CustomerKey` and `LastName` columns of all customers in the `DimCustomer` table with `BirthDate` values later than January 1, 1970 and the last name ‘Smith’.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey, LastName  
FROM  
   (SELECT * FROM DimCustomer  
    WHERE BirthDate > '01/01/1970') AS DimCustomerDerivedTable  
WHERE LastName = 'Smith'  
ORDER BY LastName;  
```  
  
### G. REDUCE join hint example  
The following example uses the `REDUCE` join hint to alter the processing of the derived table within the query. When using the `REDUCE` join hint in this query, the `fis.ProductKey` is projected, replicated and made distinct, and then joined to `DimProduct` during the shuffle of `DimProduct` on `ProductKey`. The resulting derived table is distributed on `fis.ProductKey`.  
  
```  
USE AdventureWorksPDW2012;  
  
EXPLAIN SELECT SalesOrderNumber  
FROM  
   (SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
    FROM DimProduct AS dp   
      INNER REDUCE JOIN FactInternetSales AS fis   
      ON dp.ProductKey = fis.ProductKey  
   ) AS dTable  
ORDER BY SalesOrderNumber;  
```  
  
### H. REPLICATE join hint example  
This next example shows the same query as the previous example, except that a `REPLICATE` join hint is used instead of the `REDUCE` join hint. Use of the `REPLICATE` hint causes the values in the `ProductKey` (joining) column from the `FactInternetSales` table to be replicated to all nodes. The `DimProduct` table is joined to the replicated version of those values.  
  
```  
USE AdventureWorksPDW2012;  
  
EXPLAIN SELECT SalesOrderNumber  
FROM  
   (SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
    FROM DimProduct AS dp   
      INNER REPLICATE JOIN FactInternetSales AS fis  
      ON dp.ProductKey = fis.ProductKey  
   ) AS dTable  
ORDER BY SalesOrderNumber;  
```  
  
### I. Using the REDISTRIBUTE hint to guarantee a Shuffle move for a distribution incompatible join  
The following query uses the REDISTRIBUTE query hint on a distribution incompatible join. This guarantees the query optimizer will use a Shuffle move in the query plan. This also guarantees the query plan will not use a Broadcast move which moves a distributed table to a replicated table.  
  
In the following example, the REDISTRIBUTE hint forces a Shuffle move on the FactInternetSales table because ProductKey is the distribution column for DimProduct, and is not the distribution column for FactInternetSales.  
  
```  
USEAdventureWorksPDW2012;  
EXPLAIN  
SELECT dp.ProductKey, fis.SalesOrderNumber, fis.TotalProductCost  
FROM DimProduct dp INNER REDISTRIBUTE JOIN FactInternetSales fis  
ON dp.ProductKey = fis.ProductKey;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Queries &#40;SQL Server PDW&#41;](../sqlpdw/queries-sql-server-pdw.md)  
  
