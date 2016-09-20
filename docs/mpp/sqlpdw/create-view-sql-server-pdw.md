---
title: "CREATE VIEW (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4ce8da71-2a6e-4c77-8fba-431dbf2913d5
caps.latest.revision: 53
author: BarbKess
---
# CREATE VIEW (SQL Server PDW)
Creates a view in SQL Server PDW. A view is a virtual table that contains the results of a query. The query, and not the data, is stored in the database.  
  
-   You can query a view by using the same SQL statements that you would use to query a table.  
  
-   By setting permissions on views, you can allow specific users to read or modify the table data that is in the view.  
  
-   A view does not have to be a simple subset of the rows and columns of one table. You can create a view based on a select statement that queries multiple tables or views from multiple databases.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE VIEW [ schema_name . ] view_name [  ( column_name [ ,...n ] ) ]   
AS <select_statement>   
[;]  
  
select_statement> ::=  
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT <select_criteria>  
```  
  
## Arguments  
[ *schema_name* . ] *view_name*  
The two-part name of the view. View names must follow the rules for identifiers. See [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md) for full details on permitted view names.  
  
*column_name* [**,**...*n*]  
One or more column names for the view.  
  
If column names are not specified, the view columns will use the column names specified in the SELECT statement.  
  
*common_table_expression*  
Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/with-common-table-expression-sql-server-pdw.md).  
  
SELECT <select_criteria>  
The SELECT statement that defines the view. For more information about SELECT statements, see [SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md).  
  
The SELECT statement can contain almost everything that is supported for the SELECT statement. This includes rowstore and columnstore tables, functions, and multiple SELECT statements that are separated by UNION or UNION ALL.  
  
The SELECT statement cannot contain:  
  
-   An ORDER BY clause, unless there is also a TOP  clause in the SELECT statement. The ORDER BY clause is used only to determine the rows that are returned by the TOP clause in the view definition. The ORDER BY clause does not guarantee ordered results when the view is queried, unless ORDER BY is also specified in the query itself. See [TOP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/top-sql-server-pdw.md) for information on the TOP clause.  
  
-   The INTO keyword.  
  
-   An OPTION clause that contains a query hint.  
  
## Permissions  
Requires **CREATE VIEW** permission on the database and **ALTER SCHEMA** permission on the schema that will contain the view. Or requires membership in the **db_ddladmin** fixed database role.  
  
## Limitations and Restrictions  
For information on minimum and maximum constraints on views, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
## Locking  
Takes an exclusive lock on the VIEW. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION objects.  
  
## Metadata  
To see the Data Definition Language (DDL) for a view, you can use the [sys.views](../../mpp/sqlpdw/sys-views-sql-server-pdw.md) metadata table.  
  
## Examples  
  
### A. Creating a simple view  
The following example creates a view by selecting only some of the columns from the source table.  
  
```  
USE AdventureWorksPDW2012;  
CREATE VIEW DimEmployeeBirthDates AS  
SELECT FirstName, LastName, BirthDate   
FROM DimEmployee;  
```  
  
### B. Create a view by joining two tables  
The following example creates a view by using a `SELECT` statement with an `OUTER JOIN`. The results of the join query populate the view.  
  
```  
USE AdventureWorksPDW2012;  
CREATE VIEW view1  
AS SELECT fis.CustomerKey, fis.ProductKey, fis.OrderDateKey, fis.SalesTerritoryKey, dst.SalesTerritoryRegion  
FROM FactInternetSales AS fis   
LEFT OUTER JOIN DimSalesTerritory AS dst   
ON (fis.SalesTerritoryKey=dst.SalesTerritoryKey);  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md)  
[DROP VIEW &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-view-sql-server-pdw.md)  
  
