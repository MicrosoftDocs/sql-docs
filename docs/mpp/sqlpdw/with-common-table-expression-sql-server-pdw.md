---
title: "WITH common_table_expression (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2353fe35-e951-4212-977f-dab2f5b6dbdf
caps.latest.revision: 9
author: BarbKess
---
# WITH common_table_expression (SQL Server PDW)
Specifies a temporary named result set, known as a common table expression (CTE). This is derived from a simple query and defined within the execution scope of a single **SELECT** statement. This clause can also be used in a **CREATE VIEW**, **CREATE TABLE AS SELECT**, **CREATE EXTERNAL TABLE AS SELECT**, or **CREATE REMOTE TABLE AS SELECT** statement as part of its defining **SELECT** statement.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[ WITH <common_table_expression> [ ,...n ] ]  
  
<common_table_expression>::=  
    expression_name [ (column_name [ ,...n ] ) ]  
    AS  
    (CTE_query_definition)  
```  
  
## Arguments  
*expression_name*  
Is a valid identifier for the common table expression. *expression_name* must be different from the name of any other common table expression defined in the same **WITH** <common_table_expression> clause, but *expression_name* can be the same as the name of a base table or view. Any reference to *expression_name* in the query uses the common table expression and not the base object.  
  
*column_name*  
Specifies a column name in the common table expression. Duplicate names within a single CTE definition are not allowed. The number of column names specified must match the number of columns in the result set of the *CTE_query_definition*. The list of column names is optional only if distinct names for all resulting columns are supplied in the query definition.  
  
*CTE_query_definition*  
Specifies a **SELECT** statement whose result set populates the common table expression. The **SELECT** statement for *CTE_query_definition* must meet the same requirements as for creating a view, except a CTE cannot define another CTE.  
  
If more than one *CTE_query_definition* is defined, the query definitions must be joined by one of these set operators: **UNION ALL**, **UNION**, or **EXCEPT**.  
  
## Remarks  
  
## Features and Limitations of Common Table Expressions  
The current implementation of CTEs in SQL Server PDW has the following features and limitations.  
  
-   A CTE can be specified in a **SELECT** statement.  
  
-   A CTE can be specified in a **CREATE VIEW** statement.  
  
-   A CTE can be specified in a **CREATE TABLE AS SELECT** (CTAS) statement.  
  
-   A CTE can be specified in a **CREATE REMOTE TABLE AS SELECT** (CRTAS) statement.  
  
-   A CTE can be specified in a **CREATE EXTERNAL TABLE AS SELECT** (CETAS) statement.  
  
-   A remote table can be referenced from a CTE.  
  
-   An external table can be referenced from a CTE.  
  
-   Multiple CTE query definitions can be defined in a CTE.  
  
-   A CTE must be followed by a single **SELECT** statement. **INSERT**, **UPDATE**, **DELETE**, and **MERGE** statements are not supported.  
  
-   A common table expression that includes references to itself (a recursive common table expression) is not supported.  
  
-   Specifying more than one **WITH** clause in a CTE is not allowed. For example, if a CTE_query_definition contains a subquery, that subquery cannot contain a nested **WITH** clause that defines another CTE.  
  
-   An **ORDER BY** clause cannot be used in the CTE_query_definition, except when a **TOP** clause is specified.  
  
-   When a CTE is used in a statement that is part of a batch, the statement before it must be followed by a semicolon.  
  
-   When used in statements prepared by **sp_prepare**, CTEs will behave the same way as other **SELECT** statements in PDW. However, if CTEs are used as part of CETAS prepared by **sp_prepare**, the behavior can defer from SQL Server and other PDW statements because of the way binding is implemented for **sp_prepare**. If **SELECT** that references CTE is using a wrong column that does not exist in CTE, the **sp_prepare** will pass without detecting the error, but the error will be thrown during **sp_execute** instead.  
  
For standard **Guidelines for Creating and Using Common Table Expressions** and **Guidelines for Defining and Using Recursive Common Table Expressions**, see those sections of SQL Server Books Online at [WITH common_table_expression (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms175972.aspx).  
  
## Examples  
  
### A. Creating a simple common table expression  
The following example shows the total number of sales orders per year for each sales representative at Adventure Works Cycles.  
  
```  
USE AdventureWorks2012;  
GO  
-- Define the CTE expression name and column list.  
WITH Sales_CTE (SalesPersonID, SalesOrderID, SalesYear)  
AS  
-- Define the CTE query.  
(  
    SELECT SalesPersonID, SalesOrderID, YEAR(OrderDate) AS SalesYear  
    FROM Sales.SalesOrderHeader  
    WHERE SalesPersonID IS NOT NULL  
)  
-- Define the outer query referencing the CTE name.  
SELECT SalesPersonID, COUNT(SalesOrderID) AS TotalSales, SalesYear  
FROM Sales_CTE  
GROUP BY SalesYear, SalesPersonID  
ORDER BY SalesPersonID, SalesYear;  
GO  
```  
  
### B. Using a common table expression to limit counts and report averages  
The following example shows the average number of sales orders for all years for the sales representatives.  
  
```  
WITH Sales_CTE (SalesPersonID, NumberOfOrders)  
AS  
(  
    SELECT SalesPersonID, COUNT(*)  
    FROM Sales.SalesOrderHeader  
    WHERE SalesPersonID IS NOT NULL  
    GROUP BY SalesPersonID  
)  
SELECT AVG(NumberOfOrders) AS "Average Sales Per Person"  
FROM Sales_CTE;  
GO  
```  
  
### C. Using a common table expression within a CTAS statement  
The following example creates a new table containing the total number of sales orders per year for each sales representative at Adventure Works Cycles.  
  
```  
USE AdventureWorks2012;  
GO  
  
CREATE TABLE SalesOrdersPerYear  
WITH  
(  
    DISTRIBUTION = HASH(SalesPersonID)  
)  
AS  
    -- Define the CTE expression name and column list.  
    WITH Sales_CTE (SalesPersonID, SalesOrderID, SalesYear)  
    AS  
    -- Define the CTE query.  
    (  
        SELECT SalesPersonID, SalesOrderID, YEAR(OrderDate) AS SalesYear  
        FROM Sales.SalesOrderHeader  
        WHERE SalesPersonID IS NOT NULL  
    )  
    -- Define the outer query referencing the CTE name.  
    SELECT SalesPersonID, COUNT(SalesOrderID) AS TotalSales, SalesYear  
    FROM Sales_CTE  
    GROUP BY SalesYear, SalesPersonID  
    ORDER BY SalesPersonID, SalesYear;  
GO  
```  
  
### D. Using a common table expression within a CETAS statement  
The following example creates a new external table containing the total number of sales orders per year for each sales representative at Adventure Works Cycles.  
  
```  
USE AdventureWorks2012;  
GO  
  
CREATE EXTERNAL TABLE SalesOrdersPerYear  
WITH  
(  
    LOCATION = 'hdfs://xxx.xxx.xxx.xxx:5000/files/Customer',  
    FORMAT_OPTIONS ( FIELD_TERMINATOR = '|' )   
)  
AS  
    -- Define the CTE expression name and column list.  
    WITH Sales_CTE (SalesPersonID, SalesOrderID, SalesYear)  
    AS  
    -- Define the CTE query.  
    (  
        SELECT SalesPersonID, SalesOrderID, YEAR(OrderDate) AS SalesYear  
        FROM Sales.SalesOrderHeader  
        WHERE SalesPersonID IS NOT NULL  
    )  
    -- Define the outer query referencing the CTE name.  
    SELECT SalesPersonID, COUNT(SalesOrderID) AS TotalSales, SalesYear  
    FROM Sales_CTE  
    GROUP BY SalesYear, SalesPersonID  
    ORDER BY SalesPersonID, SalesYear;  
GO  
```  
  
### E. Using multiple comma separated CTEs in a statement  
The following example demonstrates including two CTEs in a single statement. The CTEs cannot be nested (no recursion).  
  
```  
WITH   
 CountDate (TotalCount, TableName) AS  
    (  
     SELECT COUNT(datekey), 'DimDate' FROM DimDate  
    ) ,  
 CountCustomer (TotalAvg, TableName) AS  
    (  
     SELECT COUNT(CustomerKey), 'DimCustomer' FROM DimCustomer  
    )  
SELECT TableName, TotalCount FROM CountDate  
UNION ALL  
SELECT TableName, TotalAvg FROM CountCustomer;  
```  
  
## See Also  
[CREATE VIEW &#40;SQL Server PDW&#41;](../sqlpdw/create-view-sql-server-pdw.md)  
[UNION &#40;SQL Server PDW&#41;](../sqlpdw/union-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md)  
[CREATE REMOTE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-remote-table-as-select-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md)  
  
