---
title: "EXCEPT and INTERSECT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "INTERSECT_TSQL"
  - "EXCEPT_TSQL"
  - "INTERSECT"
  - "EXCEPT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "EXCEPT operator [Transact-SQL]"
  - "queries [SQL Server], comparing"
  - "comparing queries"
  - "INTERSECT operator"
ms.assetid: b1019300-171a-4a1a-854f-e1e751de3565
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Set Operators - EXCEPT and INTERSECT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns distinct rows by comparing the results of two queries.  
  
EXCEPT returns distinct rows from the left input query that aren't output by the right input query.  
 
INTERSECT returns distinct rows that are output by both the left and right input queries operator.  
  
To combine the result sets of two queries that use EXCEPT or INTERSECT, the basic rules are:  
  
-   The number and the order of the columns must be the same in all queries.  
  
-   The data types must be compatible.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
{ <query_specification> | ( <query_expression> ) }   
{ EXCEPT | INTERSECT }  
{ <query_specification> | ( <query_expression> ) }  
```  
  
## Arguments  
\<_query\_specification_> | ( \<_query\_expression_> )  
Is a query specification or query expression that returns data to be compared with the data from another query specification or query expression. The definitions of the columns that are part of an EXCEPT or INTERSECT operation don't have to be the same. But, they must be comparable through implicit conversion. When data types differ, the rules for [data type precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md) determine the data type that is run for comparison.  
  
The result is based on the same rules for combining expressions when the types are the same but differ in precision, scale, or length. For more information, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
The query specification or expression can't return **xml**, **text**, **ntext**, **image**, or nonbinary CLR user-defined type columns because these data types aren't comparable.  
  
EXCEPT  
Returns any distinct values from the query left of the EXCEPT operator. Those values return as long the right query doesn't return those values as well.  
  
INTERSECT  
Returns any distinct values that are returned by both the query on the left and right sides of the INTERSECT operator.  
  
## Remarks  
The data types of comparable columns are returned by the queries left and right of the EXCEPT or INTERSECT operators. These data types can include character data types with different collations. When they do, the required comparison is run according to the rules of [collation precedence](../../t-sql/statements/collation-precedence-transact-sql.md). If you can't run this conversion, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] returns an error.  
  
When comparing column values for determining DISTINCT rows, two NULL values are considered equal.  
  
EXCEPT and INTERSECT return the result set's column names that are the same as the column names that the query on the operator's left side returns.  
  
Column names or aliases in ORDER BY clauses must reference column names returned by the left-side query.  
  
The nullability of any column in the result set returned by EXCEPT or INTERSECT is the same as the nullability of the corresponding column that is returned by the query on the operator's left side.  
  
If EXCEPT or INTERSECT is used together with other operators in an expression, it's evaluated in the context of the following precedence:  
  
1.  Expressions in parentheses  
  
2.  The INTERSECT operator  
  
3.  EXCEPT and UNION evaluated from left to right based on their position in the expression  
  
You can use EXCEPT or INTERSECT to compare more than two sets of queries. When you do, data type conversion is determined by comparing two queries at a time, and following the previously mentioned rules of expression evaluation.  
  
EXCEPT and INTERSECT can't be used in distributed partitioned view definitions, query notifications.  
 
EXCEPT and INTERSECT may be used in distributed queries, but are only executed on the local server and not pushed to the linked server. As such, using EXCEPT and INTERSECT in distributed queries may affect performance.  
  
You can use fast forward-only and static cursors in the result set when they're used with an EXCEPT or INTERSECT operation. You can also use a keyset-driven or dynamic cursor together with an EXCEPT or INTERSECT operation. When you do, the cursor of the operation result set is converted to a static cursor.  
  
When an EXCEPT operation is displayed by using the Graphical Showplan feature in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the operation appears as a [left anti semi join](../../relational-databases/showplan-logical-and-physical-operators-reference.md), and an INTERSECT operation appears as a [left semi join](../../relational-databases/showplan-logical-and-physical-operators-reference.md).  
  
## Examples  
The following examples show using the `INTERSECT` and `EXCEPT` operators. The first query returns all values from the `Production.Product` table for comparison to the results with `INTERSECT` and `EXCEPT`.  
  
```  
-- Uses AdventureWorks  
  
SELECT ProductID   
FROM Production.Product ;  
--Result: 504 Rows  
```  
  
The following query returns any distinct values that are returned by both the query on the left and right sides of the `INTERSECT` operator.  
  
```  
-- Uses AdventureWorks  
  
SELECT ProductID   
FROM Production.Product  
INTERSECT  
SELECT ProductID   
FROM Production.WorkOrder ;  
--Result: 238 Rows (products that have work orders)  
```  
  
The following query returns any distinct values from the query left of the `EXCEPT` operator that aren't also found on the right query.  
  
```  
-- Uses AdventureWorks  
  
SELECT ProductID   
FROM Production.Product  
EXCEPT  
SELECT ProductID   
FROM Production.WorkOrder ;  
--Result: 266 Rows (products without work orders)  
```  
  
The following query returns any distinct values from the query left of the `EXCEPT` operator that aren't also found on the right query. The tables are reversed from the previous example.  
  
```  
-- Uses AdventureWorks  
  
SELECT ProductID   
FROM Production.WorkOrder  
EXCEPT  
SELECT ProductID   
FROM Production.Product ;  
--Result: 0 Rows (work orders without products)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
The following examples show how to use the `INTERSECT` and `EXCEPT` operators. The first query returns all values from the `FactInternetSales` table for comparison to the results with `INTERSECT` and `EXCEPT`.  
  
```  
-- Uses AdventureWorks  
  
SELECT CustomerKey   
FROM FactInternetSales;  
--Result: 60398 Rows  
```  
  
The following query returns any distinct values that are returned by both the query on the left and right sides of the `INTERSECT` operator.  
  
```  
-- Uses AdventureWorks  
  
SELECT CustomerKey   
FROM FactInternetSales    
INTERSECT   
SELECT CustomerKey   
FROM DimCustomer   
WHERE DimCustomer.Gender = 'F'  
ORDER BY CustomerKey;  
--Result: 9133 Rows (Sales to customers that are female.)  
```  
  
The following query returns any distinct values from the query left of the `EXCEPT` operator that aren't also found on the right query.  
  
```  
-- Uses AdventureWorks  
  
SELECT CustomerKey   
FROM FactInternetSales    
EXCEPT   
SELECT CustomerKey   
FROM DimCustomer   
WHERE DimCustomer.Gender = 'F'  
ORDER BY CustomerKey;  
--Result: 9351 Rows (Sales to customers that are not female.)  
```  