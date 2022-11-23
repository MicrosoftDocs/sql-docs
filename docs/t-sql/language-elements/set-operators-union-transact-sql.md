---
title: "UNION (Transact-SQL)"
description: "Set Operators - UNION (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "08/07/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "UNION"
  - "UNION_TSQL"
helpviewer_keywords:
  - "UNION queries"
  - "combining query results"
  - "UNION operator [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Set Operators - UNION (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Concatenates the results of two queries into a single result set. You control whether the result set includes duplicate rows:

- **UNION ALL** - Includes duplicates.
- **UNION** - Excludes duplicates.

A **UNION** operation is different from a **[JOIN](../queries/from-transact-sql.md)**:

- A **UNION** concatenates result sets from two queries. But a **UNION** does not create individual rows from columns gathered from two tables.
- A **JOIN** compares columns from two tables, to create result rows composed of columns from two tables.
  
The following are basic rules for combining the result sets of two queries by using **UNION**:  
  
-   The number and the order of the columns must be the same in all queries.  
  
-   The data types must be compatible.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
{ <query_specification> | ( <query_expression> ) }   
{ UNION [ ALL ]   
  { <query_specification> | ( <query_expression> ) } 
  [ ...n ] }
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
\<query_specification> | ( \<query_expression> )
Is a query specification or query expression that returns data to be combined with the data from another query specification or query expression. The definitions of the columns that are part of a UNION operation don't have to be the same, but they must be compatible through implicit conversion. When data types differ, the resulting data type is determined based on the rules for [data type precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md). When the types are the same but differ in precision, scale, or length, the result is based on the same rules for combining expressions. For more information, see [Precision, Scale, and Length (Transact-SQL)](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
Columns of the **xml** data type must be equal. All columns must be either typed to an XML schema or untyped. If typed, they must be typed to the same XML schema collection.  
  
UNION  
Specifies that multiple result sets are to be combined and returned as a single result set.  
  
ALL  
Incorporates all rows into the results, including duplicates. If not specified, duplicate rows are removed.  
  
## Examples  
  
### A. Using a simple UNION  
In the following example, the result set includes the contents of the `ProductModelID` and `Name` columns of both the `ProductModel` and `Gloves` tables.  
 
```sql
-- Uses AdventureWorks  
  
IF OBJECT_ID ('dbo.Gloves', 'U') IS NOT NULL  
DROP TABLE dbo.Gloves;  
GO  
-- Create Gloves table.  
SELECT ProductModelID, Name  
INTO dbo.Gloves  
FROM Production.ProductModel  
WHERE ProductModelID IN (3, 4);  
GO  
  
-- Here is the simple union.  
-- Uses AdventureWorks  
  
SELECT ProductModelID, Name  
FROM Production.ProductModel  
WHERE ProductModelID NOT IN (3, 4)  
UNION  
SELECT ProductModelID, Name  
FROM dbo.Gloves  
ORDER BY Name;  
GO  
```  
  
### B. Using SELECT INTO with UNION  
In the following example, the `INTO` clause in the second `SELECT` statement specifies that the table named `ProductResults` holds the final result set of the union of the selected columns of the `ProductModel` and `Gloves` tables. The `Gloves` table is created in the first `SELECT` statement.  
  
```sql
-- Uses AdventureWorks  
  
IF OBJECT_ID ('dbo.ProductResults', 'U') IS NOT NULL  
DROP TABLE dbo.ProductResults;  
GO  
IF OBJECT_ID ('dbo.Gloves', 'U') IS NOT NULL  
DROP TABLE dbo.Gloves;  
GO  
-- Create Gloves table.  
SELECT ProductModelID, Name  
INTO dbo.Gloves  
FROM Production.ProductModel  
WHERE ProductModelID IN (3, 4);  
GO  
  
-- Uses AdventureWorks  
  
SELECT ProductModelID, Name  
INTO dbo.ProductResults  
FROM Production.ProductModel  
WHERE ProductModelID NOT IN (3, 4)  
UNION  
SELECT ProductModelID, Name  
FROM dbo.Gloves;  
GO  
  
SELECT ProductModelID, Name   
FROM dbo.ProductResults;  
```  
  
### C. Using UNION of two SELECT statements with ORDER BY  
The order of certain parameters used with the UNION clause is important. The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements in which a column is to be renamed in the output.  
  
```sql
-- Uses AdventureWorks  
  
IF OBJECT_ID ('dbo.Gloves', 'U') IS NOT NULL  
DROP TABLE dbo.Gloves;  
GO  
-- Create Gloves table.  
SELECT ProductModelID, Name  
INTO dbo.Gloves  
FROM Production.ProductModel  
WHERE ProductModelID IN (3, 4);  
GO  
  
/* INCORRECT */  
-- Uses AdventureWorks  
  
SELECT ProductModelID, Name  
FROM Production.ProductModel  
WHERE ProductModelID NOT IN (3, 4)  
ORDER BY Name  
UNION  
SELECT ProductModelID, Name  
FROM dbo.Gloves;  
GO  
  
/* CORRECT */  
-- Uses AdventureWorks  
  
SELECT ProductModelID, Name  
FROM Production.ProductModel  
WHERE ProductModelID NOT IN (3, 4)  
UNION  
SELECT ProductModelID, Name  
FROM dbo.Gloves  
ORDER BY Name;  
GO  
```  
  
### D. Using UNION of three SELECT statements to show the effects of ALL and parentheses  
The following examples use `UNION` to combine the results of three tables that all have the same 5 rows of data. The first example uses `UNION ALL` to show the duplicated records, and returns all 15 rows. The second example uses `UNION` without `ALL` to eliminate the duplicate rows from the combined results of the three `SELECT` statements, and returns 5 rows.  
  
The third example uses `ALL` with the first `UNION` and parentheses enclose the second `UNION` that isn't using `ALL`. The second `UNION` is processed first because it's in parentheses, and returns 5 rows because the `ALL` option isn't used and the duplicates are removed. These 5 rows are combined with the results of the first `SELECT` by using the `UNION ALL` keywords. This example doesn't remove the duplicates between the two sets of five rows. The final result has 10 rows.  
  
```sql
-- Uses AdventureWorks  
  
IF OBJECT_ID ('dbo.EmployeeOne', 'U') IS NOT NULL  
DROP TABLE dbo.EmployeeOne;  
GO  
IF OBJECT_ID ('dbo.EmployeeTwo', 'U') IS NOT NULL  
DROP TABLE dbo.EmployeeTwo;  
GO  
IF OBJECT_ID ('dbo.EmployeeThree', 'U') IS NOT NULL  
DROP TABLE dbo.EmployeeThree;  
GO  
  
SELECT pp.LastName, pp.FirstName, e.JobTitle   
INTO dbo.EmployeeOne  
FROM Person.Person AS pp JOIN HumanResources.Employee AS e  
ON e.BusinessEntityID = pp.BusinessEntityID  
WHERE LastName = 'Johnson';  
GO  
SELECT pp.LastName, pp.FirstName, e.JobTitle   
INTO dbo.EmployeeTwo  
FROM Person.Person AS pp JOIN HumanResources.Employee AS e  
ON e.BusinessEntityID = pp.BusinessEntityID  
WHERE LastName = 'Johnson';  
GO  
SELECT pp.LastName, pp.FirstName, e.JobTitle   
INTO dbo.EmployeeThree  
FROM Person.Person AS pp JOIN HumanResources.Employee AS e  
ON e.BusinessEntityID = pp.BusinessEntityID  
WHERE LastName = 'Johnson';  
GO  
-- Union ALL  
SELECT LastName, FirstName, JobTitle  
FROM dbo.EmployeeOne  
UNION ALL  
SELECT LastName, FirstName ,JobTitle  
FROM dbo.EmployeeTwo  
UNION ALL  
SELECT LastName, FirstName,JobTitle   
FROM dbo.EmployeeThree;  
GO  
  
SELECT LastName, FirstName,JobTitle  
FROM dbo.EmployeeOne  
UNION   
SELECT LastName, FirstName, JobTitle   
FROM dbo.EmployeeTwo  
UNION   
SELECT LastName, FirstName, JobTitle   
FROM dbo.EmployeeThree;  
GO  
  
SELECT LastName, FirstName,JobTitle   
FROM dbo.EmployeeOne  
UNION ALL  
(  
SELECT LastName, FirstName, JobTitle   
FROM dbo.EmployeeTwo  
UNION  
SELECT LastName, FirstName, JobTitle   
FROM dbo.EmployeeThree  
);  
GO  
  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### E. Using a simple UNION  
In the following example, the result set includes the contents of the `CustomerKey` columns of both the `FactInternetSales` and `DimCustomer` tables. Since the ALL keyword isn't used, duplicates are excluded from the results.  
  
```sql
-- Uses AdventureWorks  
  
SELECT CustomerKey   
FROM FactInternetSales    
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
```  
  
### F. Using UNION of two SELECT statements with ORDER BY  
 When any SELECT statement in a UNION statement includes an ORDER BY clause, that clause should be placed after all SELECT statements. The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements in which a column is ordered with ORDER BY.  
  
```sql
-- Uses AdventureWorks  
  
-- INCORRECT  
SELECT CustomerKey   
FROM FactInternetSales    
ORDER BY CustomerKey  
UNION   
SELECT CustomerKey   
FROM DimCustomer  
ORDER BY CustomerKey;  
  
-- CORRECT   
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey   
FROM FactInternetSales    
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
```  
  
### G. Using UNION of two SELECT statements with WHERE and ORDER BY  
The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements where WHERE and ORDER BY are needed.  
  
```sql
-- Uses AdventureWorks  
  
-- INCORRECT   
SELECT CustomerKey   
FROM FactInternetSales   
WHERE CustomerKey >= 11000  
ORDER BY CustomerKey   
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
  
-- CORRECT  
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey   
FROM FactInternetSales   
WHERE CustomerKey >= 11000  
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
```  
  
### H. Using UNION of three SELECT statements to show effects of ALL and parentheses  
The following examples use `UNION` to combine the results of **the same table** to demonstrate the effects of ALL and parentheses when using `UNION`.  
  
The first example uses `UNION ALL` to show duplicated records and returns each row in the source table three times. The second example uses `UNION` without `ALL` to eliminate the duplicate rows from the combined results of the three `SELECT` statements and returns only the unduplicated rows from the source table.  
  
The third example uses `ALL` with the first `UNION` and parentheses enclosing the second `UNION` that isn't using `ALL`. The second `UNION` is processed first because it is in parentheses. It returns only the unduplicated rows from the table because the `ALL` option isn't used and duplicates are removed. These rows are combined with the results of the first `SELECT` by using the `UNION ALL` keywords. This example doesn't remove the duplicates between the two sets.  
  
```sql
-- Uses AdventureWorks  
  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION ALL   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION ALL   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer;  
  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer;  
  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION ALL  
(  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
);  
```  
  
## See Also  
[SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md)   
[SELECT Examples (Transact-SQL)](../../t-sql/queries/select-examples-transact-sql.md)  
