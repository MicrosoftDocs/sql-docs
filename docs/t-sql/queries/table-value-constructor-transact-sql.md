---
description: "Table Value Constructor (Transact-SQL)"
title: "Table Value Constructor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/23/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "inserting multiple rows"
  - "row value expression"
  - "row constructor [SQL Server]"
  - "table value constructor [SQL Server]"
ms.assetid: e57cd31d-140e-422f-8178-2761c27b9deb
author: VanMSFT
ms.author: vanto
---
# Table Value Constructor (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Specifies a set of row value expressions to be constructed into a table. The [!INCLUDE[tsql](../../includes/tsql-md.md)] table value constructor allows multiple rows of data to be specified in a single DML statement. The table value constructor can be specified either as the VALUES clause of an INSERT ... VALUES statement, or as a derived table in either the USING clause of the MERGE statement or the FROM clause.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
VALUES ( <row value expression list> ) [ ,...n ]   
  
<row value expression list> ::=  
    {<row value expression> } [ ,...n ]  
  
<row value expression> ::=  
    { DEFAULT | NULL | expression }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 VALUES  
 Introduces the row value expression lists. Each list must be enclosed in parentheses and separated by a comma.  
  
 The number of values specified in each list must be the same and the values must be in the same order as the columns in the table. A value for each column in the table must be specified or the column list must explicitly specify the columns for each incoming value.  
  
 DEFAULT  
 Forces the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to insert the default value defined for a column. If a default does not exist for the column and the column allows null values, NULL is inserted. DEFAULT is not valid for an identity column. When specified in a table value constructor, DEFAULT is allowed only in an INSERT statement.  
  
 *expression*  
 Is a constant, a variable, or an expression. The expression cannot contain an EXECUTE statement.  
  
## Limitations and Restrictions  
 When used as a derived table, there is no limit to the number of rows.  
 
 When used as the VALUES clause of an INSERT ... VALUES statement, there is a limit of 1000 rows. Error 10738 is returned if the number of rows exceeds the maximum. To insert more than 1000 rows, use one of the following methods:  
  
- Create multiple INSERT statements  
  
- Use a derived table  
  
- Bulk import the data by using the [**bcp** utility](../../tools/bcp-utility.md), the .NET [SqlBulkCopy class](/dotnet/api/system.data.sqlclient.sqlbulkcopy), [OPENROWSET (BULK ...)](../../t-sql/functions/openrowset-transact-sql.md), or the [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) statement  
  
 Only single scalar values are allowed as a row value expression. A subquery that involves multiple columns is not allowed as a row value expression. For example, the following code results in a syntax error because the third row value expression list contains a subquery with multiple columns.  
  
```sql
USE AdventureWorks2012;  
GO  
CREATE TABLE dbo.MyProducts (Name VARCHAR(50), ListPrice MONEY);  
GO  
-- This statement fails because the third values list contains multiple columns in the subquery.  
INSERT INTO dbo.MyProducts (Name, ListPrice)  
VALUES ('Helmet', 25.50),  
       ('Wheel', 30.00),  
       (SELECT Name, ListPrice FROM Production.Product WHERE ProductID = 720);  
GO  
```  
  
 However, the statement can be rewritten by specifying each column in the subquery separately. The following example successfully inserts three rows into the `MyProducts` table.  
  
```sql
INSERT INTO dbo.MyProducts (Name, ListPrice)  
VALUES ('Helmet', 25.50),  
       ('Wheel', 30.00),  
       ((SELECT Name FROM Production.Product WHERE ProductID = 720),  
        (SELECT ListPrice FROM Production.Product WHERE ProductID = 720));  
GO  
```  
  
## Data Types  
 The values specified in a multi-row INSERT statement follow the data type conversion properties of the UNION ALL syntax. This results in the implicit conversion of unmatched types to the type of higher [precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md). If the conversion is not a supported implicit conversion, an error is returned. For example, the following statement inserts an integer value and a character value into a column of type **char**.  
  
```sql
CREATE TABLE dbo.t (a INT, b CHAR);  
GO  
INSERT INTO dbo.t VALUES (1,'a'), (2, 1);  
GO  
```  
  
 When the INSERT statement is run, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to convert 'a' to an integer because the data type precedence indicates that an integer is of a higher type than a character. The conversion fails and an error is returned. You can avoid the error by explicitly converting values as appropriate. For example, the previous statement can be written as follows.  
  
```sql
INSERT INTO dbo.t VALUES (1,'a'), (2, CONVERT(CHAR,1));  
```  
  
## Examples  
  
### A. Inserting multiple rows of data  
 The following example creates the table `dbo.Departments` and then uses the table value constructor to insert five rows into the table. Because values for all columns are supplied and are listed in the same order as the columns in the table, the column names do not have to be specified in the column list.  
  
```sql
USE AdventureWorks2012;  
GO  
INSERT INTO Production.UnitMeasure  
VALUES (N'FT2', N'Square Feet ', '20080923'), (N'Y', N'Yards', '20080923'),
       (N'Y3', N'Cubic Yards', '20080923');  
GO  
```  
  
### B. Inserting multiple rows with DEFAULT and NULL values  
 The following example demonstrates specifying DEFAULT and NULL when using the table value constructor to insert rows into a table.  
  
```sql
USE AdventureWorks2012;  
GO  
CREATE TABLE Sales.MySalesReason(  
SalesReasonID int IDENTITY(1,1) NOT NULL,  
Name dbo.Name NULL ,  
ReasonType dbo.Name NOT NULL DEFAULT 'Not Applicable' );  
GO  
INSERT INTO Sales.MySalesReason   
VALUES ('Recommendation','Other'), ('Advertisement', DEFAULT), (NULL, 'Promotion');  
  
SELECT * FROM Sales.MySalesReason;  
```  
  
### C. Specifying multiple values as a derived table in a FROM clause  
 The following examples use the table value constructor to specify multiple values in the FROM clause of a SELECT statement.  
  
```sql
SELECT a, b FROM (VALUES (1, 2), (3, 4), (5, 6), (7, 8), (9, 10) ) AS MyTable(a, b);  
GO  
-- Used in an inner join to specify values to return.  
SELECT ProductID, a.Name, Color  
FROM Production.Product AS a  
INNER JOIN (VALUES ('Blade'), ('Crown Race'), ('AWC Logo Cap')) AS b(Name)   
ON a.Name = b.Name;  
```  
  
### D. Specifying multiple values as a derived source table in a MERGE statement  
 The following example uses MERGE to modify the `SalesReason` table by either updating or inserting rows. When the value of `NewName` in the source table matches a value in the `Name` column of the target table, (`SalesReason`), the `ReasonType` column is updated in the target table. When the value of `NewName` does not match, the source row is inserted into the target table. The source table is a derived table that uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] table value constructor to specify multiple rows for the source table.  
  
```sql
USE AdventureWorks2012;  
GO  
-- Create a temporary table variable to hold the output actions.  
DECLARE @SummaryOfChanges TABLE(Change VARCHAR(20));  
  
MERGE INTO Sales.SalesReason AS Target  
USING (VALUES ('Recommendation','Other'), ('Review', 'Marketing'), ('Internet', 'Promotion'))  
       AS Source (NewName, NewReasonType)  
ON Target.Name = Source.NewName  
WHEN MATCHED THEN  
UPDATE SET ReasonType = Source.NewReasonType  
WHEN NOT MATCHED BY TARGET THEN  
INSERT (Name, ReasonType) VALUES (NewName, NewReasonType)  
OUTPUT $action INTO @SummaryOfChanges;  
  
-- Query the results of the table variable.  
SELECT Change, COUNT(*) AS CountPerChange  
FROM @SummaryOfChanges  
GROUP BY Change;  
```  
  
### E. Inserting more than 1000 rows
  The following example demonstrates using the table value constructor as a derived table. This allows for inserting more than 1000 rows from a single table value constructor.
  
```sql
CREATE TABLE dbo.Test ([Value] INT);  
  
INSERT INTO dbo.Test ([Value])  
  SELECT drvd.[NewVal]
  FROM   (VALUES (0), (1), (2), (3), ..., (5000)) drvd([NewVal]);
```  


## See Also  
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)   
 [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)  
  
  
