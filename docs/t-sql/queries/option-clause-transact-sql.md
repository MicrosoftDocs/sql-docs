---
description: "OPTION Clause (Transact-SQL)"
title: "OPTION Clause (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "OPTION clause"
  - "OPTION_TSQL"
  - "OPTION"
  - "OPTION_clause_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "clauses [SQL Server], OPTION"
  - "OPTION clause"
ms.assetid: f47e2f3f-9302-4711-9d66-16b1a2a7ffe3
author: VanMSFT
ms.author: vanto
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# OPTION Clause (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Specifies that the indicated query hint should be used throughout the entire query. Each query hint can be specified only one time, although multiple query hints are permitted. Only one OPTION clause can be specified with the statement.  
  
 This clause can be specified in the SELECT, DELETE, UPDATE and MERGE statements.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
### Syntax for [!INCLUDE[ssnoversion-md.md](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure_md.md](../../includes/ssazure_md.md)].

```syntaxsql
[ OPTION ( <query_hint> [ ,...n ] ) ]   
```  
  
### Syntax for [!INCLUDE[sssdw-md.md](../../includes/sssdw-md.md)] and [!INCLUDE[sspdw-md.md](../../includes/sspdw-md.md)]

```syntaxsql
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
  
### Syntax for [!INCLUDE[sssodfull-md.md](../../includes/sssodfull-md.md)]

```syntaxsql
OPTION ( <query_option> [ ,...n ] )

<query_option> ::=
    LABEL = label_name
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *query_hint*  
 Keywords that indicate which optimizer hints are used to customize the way the Database Engine processes the statement. For more information, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  
  
## Examples  
  
### A. Using an OPTION clause with a GROUP BY clause  
 The following example shows how the `OPTION` clause is used with a `GROUP BY` clause.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT ProductID, OrderQty, SUM(LineTotal) AS Total  
FROM Sales.SalesOrderDetail  
WHERE UnitPrice < $5.00  
GROUP BY ProductID, OrderQty  
ORDER BY ProductID, OrderQty  
OPTION (HASH GROUP, FAST 10);  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. SELECT statement with a label in the OPTION clause  
 The following example shows a simple [!INCLUDE[ssDW](../../includes/ssdw-md.md)] SELECT statement with a label in the OPTION clause.  
  
```sql
-- Uses AdventureWorks  
  
SELECT * FROM FactResellerSales  
  OPTION ( LABEL = 'q17' );  
```  
  
### C. SELECT statement with a query hint in the OPTION clause  
 The following example shows a SELECT statement that uses a HASH JOIN query hint in the OPTION clause.  
  
```sql
-- Uses AdventureWorks  
  
SELECT COUNT (*) FROM dbo.DimCustomer a  
INNER JOIN dbo.FactInternetSales b   
ON (a.CustomerKey = b.CustomerKey)  
OPTION (HASH JOIN);  
```  
  
### D. SELECT statement with a label and multiple query hints in the OPTION clause  
 The following example is a [!INCLUDE[ssDW](../../includes/ssdw-md.md)] SELECT statement that contains a label and multiple query hints. When the query is run on the Compute nodes, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will apply a hash join or merge join, according to the strategy that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] decides is the most optimal.  
  
```sql
-- Uses AdventureWorks  
  
SELECT COUNT (*) FROM dbo.DimCustomer a  
INNER JOIN dbo.FactInternetSales b   
ON (a.CustomerKey = b.CustomerKey)  
OPTION ( Label = 'CustJoin', HASH JOIN, MERGE JOIN);  
```  
  
### E. Using a query hint when querying a view  
 The following example creates a view named CustomerView and then uses a HASH JOIN query hint in a query that references a view and a table.  
  
```sql
-- Uses the AdventureWorks sample database
  
CREATE VIEW CustomerView  
AS  
SELECT CustomerKey, FirstName, LastName FROM ssawPDW..DimCustomer;  
GO
SELECT COUNT (*) FROM dbo.CustomerView a  
INNER JOIN dbo.FactInternetSales b  
ON (a.CustomerKey = b.CustomerKey)  
OPTION (HASH JOIN);  
GO
DROP VIEW CustomerView;
GO
```  
  
### F. Query with a subselect and a query hint  
 The following example shows a query that contains both a subselect and a query hint. The query hint is applied globally. Query hints are not allowed to be appended to the subselect statement.  
  
```sql
-- Uses the AdventureWorks sample database
CREATE VIEW CustomerView AS  
SELECT CustomerKey, FirstName, LastName FROM ssawPDW..DimCustomer;  
GO
SELECT * FROM (  
SELECT COUNT (*) AS a FROM dbo.CustomerView a  
INNER JOIN dbo.FactInternetSales b  
ON ( a.CustomerKey = b.CustomerKey )) AS t  
OPTION (HASH JOIN);  
```  
  
### G. Force the join order to match the order in the query  
 The following example uses the FORCE ORDER hint to force the query plan to use the join order specified by the query. This will improve performance on some queries; not all queries.  
  
```sql
-- Uses AdventureWorks  
  
-- Obtain partition numbers, boundary values, boundary value types, and rows per boundary  
-- for the partitions in the ProspectiveBuyer table of the ssawPDW database.  
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
  
### H. Using EXTERNALPUSHDOWN  
 The following example forces the pushdown of the WHERE clause to the MapReduce job on the external Hadoop table.  
  
```sql
SELECT ID FROM External_Table_AS A   
WHERE ID < 1000000  
OPTION (FORCE EXTERNALPUSHDOWN);  
```  
  
 The following example prevents the pushdown of the WHERE clause to the MapReduce job on the external Hadoop table. All rows are returned to PDW where the WHERE clause is applied.  
  
```sql
SELECT ID FROM External_Table_AS A   
WHERE ID < 10  
OPTION (DISABLE EXTERNALPUSHDOWN);  
```  
  
## See Also  
 [Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)  
  
  

