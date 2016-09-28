---
title: "OPTION (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 72bbce98-305b-42fa-a19f-d89620621ecc
caps.latest.revision: 37
author: BarbKess
---
# OPTION (SQL Server PDW)
The OPTION clause specifies query options for SQL Server PDWSQL statements. The options include a query label and query join hints HASH JOIN, LOOP JOIN, MERGE JOIN, and FORCE ORDER. A query label makes it easy to locate a specific query within the SQL Server PDW Admin Console. Query join hints force SMP SQL Server, running on the Compute nodes, to use a specific join strategy when creating the SQL Server query plan.  
  
In most cases, query join hints are not necessary because SQL Server chooses the best join strategy when it creates the query plan. However, there might be queries for which SQL Server does not generate the most optimal query execution plan, and it is for these cases that it can be beneficial to use join hints to improve query performance.  
  
> [!WARNING]  
> Use query join hints cautiously. Forcing SQL Server to use a suboptimal join strategy can significantly reduce query performance.  
  
For more information about how hash join, loop join, and merge join work in SQL Server, see the following posts from Craig Freedman’s blog on MSDN:  
  
-   [Nested Loops Join](http://blogs.msdn.com/b/craigfr/archive/2006/07/26/679319.aspx)  
  
-   [Hash Join](http://blogs.msdn.com/b/craigfr/archive/2006/08/10/687630.aspx)  
  
-   [Merge Join](http://blogs.msdn.com/b/craigfr/archive/2006/08/03/merge-join.aspx)  
  
-   [Summary of Join Properties](http://blogs.msdn.com/b/craigfr/archive/2006/08/16/702828.aspx)  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OPTION ( <query_option> [ ,...n ] )  
  
<query_option> ::=  
    LABEL = label_name |  
    <query_hint>  
  
<query_hint> ::=  
    HASH JOIN   
    | LOOP JOIN   
    | MERGE JOIN  
    | FORCE ORDER  
    | { FORCE | DISABLE } EXTERNALPUSHDOWN  
```  
  
## Arguments  
*label_name*  
A user-provided label name for the query, where *label_name* is a character string constant enclosed in single quotes. Labels identify queries in the query cache. For more information, see [Constants &#40;SQL Server PDW&#41;](../sqlpdw/constants-sql-server-pdw.md). The string literal can be a Unicode or non-Unicode string literal. For Unicode string literals, use of N prefix is allowed: (N’unicode_string’).  
  
> [!CAUTION]  
> The label option is supported in PDW but is not a supported option in SQL Server.  
  
HASH JOIN  
Add the SQL Server HASH join hint to the query that will be run by SQL Server on the Compute nodes. For more information, see [Hash Join](http://blogs.msdn.com/b/craigfr/archive/2006/08/10/687630.aspx) on MSDN.  
  
MERGE JOIN  
Add the SQL Server MERGE join hint to the query that will be run by SQL Server on the Compute nodes. For more information, see [Merge Join](http://blogs.msdn.com/b/craigfr/archive/2006/08/03/merge-join.aspx) on MSDN.  
  
LOOP JOIN  
Add the SQL Server LOOP join hint to the Transact\-SQL query that will be run by SQL Server on the Compute nodes. For more information, see [Nested Loops Join](http://blogs.msdn.com/b/craigfr/archive/2006/07/26/679319.aspx) on MSDN.  
  
FORCE ORDER  
Run the query by performing joins in the same order as they appear in the query.  
  
{ FORCE | DISABLE } EXTERNALPUSHDOWN  
Force or disable the pushdown of the computation of qualifying expressions or operators in Hadoop. Only applies to queries using PolyBase. Will not pushdown to Azure.  
  
## General Remarks  
Query join hints are supported with the following statements:  
  
-   [SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md) statement  
  
-   SELECT statement within a [CREATE REMOTE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-remote-table-as-select-sql-server-pdw.md) statement  
  
-   SELECT statement within a [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md) statement  
  
-   [INSERT &#40;SQL Server PDW&#41;](../sqlpdw/insert-sql-server-pdw.md)  
  
-   [UPDATE &#40;SQL Server PDW&#41;](../sqlpdw/update-sql-server-pdw.md)  
  
-   [DELETE &#40;SQL Server PDW&#41;](../sqlpdw/delete-sql-server-pdw.md)  
  
When multiple query hints are specified in the same OPTION clause, SQL Server applies the hint that is the most beneficial. For example, in the OPTION clause `OPTION ( HASH JOIN, LOOP JOIN, MERGE JOIN )`, SQL Server will apply either the hash, loop, or merge join hint.  
  
When the SQL Server PDW SELECT statement contains one or more subselect statements and a query hint, SQL Server, running on the Compute nodes, applies the query hint to all SELECT and subselect statements.  
  
When a SQL Server PDW SELECT statement references a view and contains a global query hint, SQL Server PDW first expands the view and then applies the query hint when creating the parallel query plan.  
  
## Limitations and Restrictions  
Option clause:  
  
-   Only one OPTION clause is allowed per query.  
  
-   The OPTION clause must be provided at the global level. For example, it cannot be appended to a subselect statement within a query.  
  
-   When multiple SELECT statements are connected with the UNION operator, the OPTION clause can be used, but must be placed after the last SELECT statement in the UNION. Only one OPTION clause is allowed per query when the UNION operator is used.  
  
Labels:  
  
-   Labels cannot be used in SELECT statements that are subselects of other queries. Labels can be used to apply to entire SELECT statements that contain subselect or UNION statements.  
  
-   Labels are only supported in **SELECT**, **INSERT**, **UPDATE**, and **DELETE** statements.  
  
Query join hints:  
  
-   If the query contains only a single data source, SQL Server PDW ignores query hints that exist in the query.  
  
-   The same join hint cannot be duplicated in the OPTION clause. For example, `OPTION ( HASH JOIN, HASH JOIN)` is invalid.  
  
-   [CREATE VIEW &#40;SQL Server PDW&#41;](../sqlpdw/create-view-sql-server-pdw.md) statements cannot contain query hints. For example, if you try to create a view based on a SELECT statement that contains query hints, the CREATE VIEW statement will not compile.  
  
## Examples  
  
### A. SQL Server PDW SELECT statement with a label in the OPTION clause  
The following example shows a simple SQL Server PDW SELECT statement with a label in the OPTION clause.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT * FROM FactResellerSales  
  OPTION ( LABEL = 'q17' );  
```  
  
### B. SQL Server PDW SELECT statement with a query hint in the OPTION clause  
The following example shows a SELECT statement that uses a HASH JOIN query hint in the OPTION clause.  
  
```  
USE AdventureWorksPDW2012;  
SELECT COUNT (*) FROM dbo.DimCustomer a  
INNER JOIN dbo.FactInternetSales b   
ON (a.CustomerKey = b.CustomerKey)  
OPTION (HASH JOIN);  
```  
  
### C. SQL Server PDW SELECT statement with a label and multiple query hints in the OPTION clause  
The following example is a SQL Server PDW SELECT statement that contains a label and multiple query hints. When the query is run on the Compute nodes, SQL Server will apply a hash join or merge join, according to the strategy that SQL Server decides is the most optimal.  
  
```  
USE AdventureWorksPDW2012;  
SELECT COUNT (*) FROM dbo.DimCustomer a  
INNER JOIN dbo.FactInternetSales b   
ON (a.CustomerKey = b.CustomerKey)  
OPTION ( Label = 'CustJoin', HASH JOIN, MERGE JOIN);  
```  
  
### D. Using a SQL Server PDW query hint when querying a view  
The following example creates a view named CustomerView and then uses a HASH JOIN query hint in a query that references a view and a table.  
  
```  
USE AdventureWorksPDW2012;  
CREATE VIEW CustomerView  
AS  
SELECT CustomerKey, FirstName, LastName FROM AdventureWorksPDW2012..DimCustomer;  
  
SELECT COUNT (*) FROM dbo.CustomerView a  
INNER JOIN dbo.FactInternetSales b  
ON (a.CustomerKey = b.CustomerKey)  
OPTION (HASH JOIN);  
  
DROP VIEW CustomerView;  
```  
  
### E. SQL Server PDW query with a subselect and a query hint  
The following example shows a query that contains both a subselect and a query hint. The query hint is applied globally. Query hints are not allowed to be appended to the subselect statement.  
  
```  
USE AdventureWorksPDW2012;  
CREATE VIEW CustomerView AS  
SELECT CustomerKey, FirstName, LastName FROM AdventureWorksPDW2012..DimCustomer;  
  
SELECT * FROM (  
SELECT COUNT (*) AS a FROM dbo.CustomerView a  
INNER JOIN dbo.FactInternetSales b  
ON ( a.CustomerKey = b.CustomerKey )) AS t  
OPTION (HASH JOIN);  
```  
  
### F. Force the join order to match the order in the query  
The following example uses the FORCE ORDER hint to force the query plan to use the join order specified by the query. This will improve performance on some queries; not all queries.  
  
```  
USE AdventureWorksPDW2012;  
  
-- Obtain partition numbers, boundary values, boundary value types, and rows per boundary  
-- for the partitions in the ProspectiveBuyer table of the AdventureWorksPDW2012 database.  
SELECT sp.partition_number, prv.value AS boundary_value, lower(sty.name) AS boundary_value_type, sp.rows   
FROM sys.tables st JOIN sys.indexes si ON st.object_id = si.object_id AND si.index_id <2  
JOIN sys.partitions sp ON sp.object_id = st.object_id AND sp.index_id = si.index_id  
JOIN sys.partition_schemes ps ON ps.data_space_id = si.data_space_id   
JOIN sys.partition_range_values prv ON prv.function_id = ps.function_id   
JOIN sys.partition_parameters pp ON pp.function_id = ps.function_id   
JOIN sys.types sty ON sty.user_type_id = pp.user_type_id AND prv.boundary_id = sp.partition_number   
WHERE st.object_id = (SELECT object_id FROM sys.objects WHERE name = 'FactResellerSales')   
ORDER BY sp.partition_number  
OPTION ( FORCE ORDER )  
;  
```  
  
### G. Using EXERNALPUSHDOWN  
The following example forces the pushdown of the WHERE clause to the MapReduce job on the external Hadoop table.  
  
```  
SELECT ID FROM External_Table_AS A   
WHERE ID < 1000000  
OPTION (FORCE EXTERNALPUSHDOWN);  
```  
  
The following example prevents the pushdown of the WHERE clause to the MapReduce job on the external Hadoop table. All rows are returned to PDW where the WHERE clause is applied.  
  
```  
SELECT ID FROM External_Table_AS A   
WHERE ID < 10  
OPTION (DISABLE EXTERNALPUSHDOWN);  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
