---
title: "UPDATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 677f5cf5-7a56-476c-832e-c980ee2b84d6
caps.latest.revision: 50
author: BarbKess
---
# UPDATE (SQL Server PDW)
Changes existing data in a SQL Server PDW table.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
UPDATE [ database_name . [ schema_name ] . | schema_name . ] table_name   
SET { column_name = { expression | NULL } } [ ,...n ]  
[ FROM from_clause ]  
[ WHERE <search_condition> ]   
[ OPTION ( LABEL = label_name ) ]  
[;]  
```  
  
## Arguments  
*database_name*  
The name of the database that is to receive the data. The default is the current database.  
  
*schema_name*  
The schema of the table or view.  
  
*table_name*  
The name of the table to update.  
  
*from_clause*  
Specifies a source table or view for the update. For more information, see [FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md).  
  
*column_name*  
A column that contains the data to be changed. *column_name* must exist in *table_name*.  
  
*expression*  
A literal value, expression, or a subselect statement (enclosed within parentheses) that returns a single value. The value returned by *expression* replaces the existing value in *column_name*.  
  
*search_condition*  
Specifies the condition to be met for the rows to be updated. The search condition cannot contain a join. There is no limit to the number of predicates that can be included in a search condition. For more information about predicates and search conditions, see [Search Condition &#40;SQL Server PDW&#41;](../sqlpdw/search-condition-sql-server-pdw.md).  
  
*label_name*  
A user-provided label name for the query, where *label_name* is a character string constant enclosed in single quotes. Labels identify queries in the query cache. For more information, see [Constants &#40;SQL Server PDW&#41;](../sqlpdw/constants-sql-server-pdw.md). The string literal can be a Unicode or non-Unicode string literal. For Unicode string literals, use of N prefix is allowed: (N’unicode_string’).  
  
## General Remarks  
If an update to a row violates a constraint for the column, or if the new value is an incompatible data type, the statement is canceled, an error is returned, and no records are updated.  
  
The UPDATE statement cannot be used to modify the Distribution Key on a distributed table. See [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md) for information on Distribution Keys.  
  
When an UPDATE statement encounters an arithmetic error (overflow, divide by zero, or a domain error) during expression evaluation, the update is not performed. The rest of the batch is not run, and an error message is returned.  
  
For distributed tables, SQL Server PDW performs updates in parallel across the Compute nodes, and sequentially across the distributions within each Compute node. This provides the ability to rollback updates in case an update operation fails.  
  
All **char** and **nchar** columns are right-padded to the defined length.  
  
The TOP clause cannot be used with the UPDATE statement.  For more information, see [TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md)  
  
## Limitations and Restrictions  
UPDATE statements cannot contain the TOP clause.  For more information, see [TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md).  
  
[SET ROWCOUNT &#40;SQL Server PDW&#41;](../sqlpdw/set-rowcount-sql-server-pdw.md) has no effect on this statement.  
  
UPDATE statements cannot contain a FROM clause and a subquery. For example, the following statement is not allowed:  
  
```  
UPDATE A SET a2=2 FROM B WHERE EXISTS ( SELECT 1 FROM C );  
```  
  
UPDATE statements cannot contain explicit joins, or more than one table, in the FROM clause. For example, the following statement is not allowed:  
  
```  
UPDATE dbo.Table2  
SET dbo.Table2.ColB = dbo.Table2.ColB + dbo.Table1.ColB  
FROM dbo.Table2  
INNER JOIN dbo.Table1  
ON ( dbo.Table2.ColA = dbo.Table1.ColA );  
```  
  
The above query can be rewritten as an implicit join, and with only one table in the FROM clause. The following statement is allowed:  
  
```  
UPDATE dbo.Table2  
SET dbo.Table2.ColB = dbo.Table2.ColB + dbo.Table1.ColB  
FROM dbo.Table1  
WHERE dbo.Table2.ColA = dbo.Table1.ColA;  
```  
  
## Permissions  
Requires **UPDATE** permissions on the target table or membership in the **db_datawriter** fixed database role. **SELECT** permissions are also required for the table being updated if the **UPDATE** statement contains a **WHERE** clause, or if *expression* in the **SET** clause uses a column in the table. **UPDATE** permission is denied to members of the **db_denydatawriter** fixed database role.  
  
## Locking  
Takes an exclusive lock at some level on the table being changed.  
  
## Examples  
  
### A. Using a simple UPDATE statement  
The following examples show how all rows can be affected when a WHERE clause is not used to specify the row (or rows) to update.  
  
This example updates the values in the `EndDate` and `CurrentFlag` columns for all rows in the `DimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
UPDATE DimEmployee  
SET EndDate = '2010-12-31', CurrentFlag='False';  
```  
  
You can also use computed values in an UPDATE statement. The following example doubles the value in the `ListPrice` column for all rows in the `Product` table.  
  
```  
USE AdventureWorksPDW2012;  
  
UPDATE DimEmployee  
SET BaseRate = BaseRate * 2;  
```  
  
### B. Using the UPDATE statement with a WHERE clause  
The following example uses the WHERE clause to specify which rows to update.  
  
```  
USE AdventureWorksPDW2012;  
  
UPDATE DimEmployee  
SET FirstName = 'Gail'  
WHERE EmployeeKey = 500;  
```  
  
### C. Using the UPDATE statement with label  
The following example shows use of a LABEL for the UPDATE statement.  
  
```  
USE AdventureWorksPDW2012;  
  
UPDATE DimProduct  
SET ProductSubcategoryKey = 2   
WHERE ProductKey = 313  
OPTION (LABEL = N'label1');  
```  
  
### D. Using the UPDATE statement with information from another table  
This example creates a table to store total sales by year. It updates the total sales for the year 2004 by running a SELECT statement against the FactInternetSales table.  
  
```  
USE AdventureWorksPDW2012;  
  
CREATE TABLE YearlyTotalSales (  
    YearlySalesAmount money NOT NULL,  
    Year smallint NOT NULL )  
WITH ( DISTRIBUTION = REPLICATE );  
  
INSERT INTO YearlyTotalSales VALUES (0, 2004);  
INSERT INTO YearlyTotalSales VALUES (0, 2005);  
INSERT INTO YearlyTotalSales VALUES (0, 2006);  
  
UPDATE YearlyTotalSales  
SET YearlySalesAmount=  
(SELECT SUM(SalesAmount) FROM FactInternetSales WHERE OrderDateKey >=20040000 AND OrderDateKey < 20050000)  
WHERE Year=2004;  
  
SELECT * FROM YearlyTotalSales;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[INSERT &#40;SQL Server PDW&#41;](../sqlpdw/insert-sql-server-pdw.md)  
[DELETE &#40;SQL Server PDW&#41;](../sqlpdw/delete-sql-server-pdw.md)  
  
