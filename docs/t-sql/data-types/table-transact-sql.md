---
title: "table (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/23/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "table data type [SQL Server]"
  - "table variables [SQL Server]"
ms.assetid: 1ef0b60e-a64c-4e97-847b-67930e3973ef
caps.latest.revision: 48
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# table (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Is a special data type that can be used to store a result set for processing at a later time. **table** is primarily used for temporary storage of a set of rows returned as the result set of a table-valued function. Functions and variables can be declared to be of type **table**. **table** variables can be used in functions, stored procedures, and batches. To declare variables of type **table**, use [DECLARE @local_variable](../../t-sql/language-elements/declare-local-variable-transact-sql.md).
  

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
table_type_definition ::=   
    TABLE ( { <column_definition> | <table_constraint> } [ ,...n ] )   
  
<column_definition> ::=   
    column_name scalar_data_type   
    [ COLLATE <collation_definition> ]   
    [ [ DEFAULT constant_expression ] | IDENTITY [ ( seed , increment ) ] ]   
    [ ROWGUIDCOL ]   
    [ column_constraint ] [ ...n ]   
  
<column_constraint> ::=   
    { [ NULL | NOT NULL ]   
    | [ PRIMARY KEY | UNIQUE ]   
    | CHECK ( logical_expression )   
    }   
  
<table_constraint> ::=   
     { { PRIMARY KEY | UNIQUE } ( column_name [ ,...n ] )  
     | CHECK ( logical_expression )   
     }   
```  
  
## Arguments  
*table_type_definition*  
Is the same subset of information that is used to define a table in CREATE TABLE. The table declaration includes column definitions, names, data types, and constraints. The only constraint types allowed are PRIMARY KEY, UNIQUE KEY, and NULL.  
For more information about the syntax, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md), [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md), and [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md).
  
*collation_definition*  
Is the collation of the column that is made up of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows locale and a comparison style, a Windows locale and the binary notation, or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation. If *collation_definition* is not specified, the column inherits the collation of the current database. Or if the column is defined as a common language runtime (CLR) user-defined type, the column inherits the collation of the user-defined type.
  
## Remarks  
**table** variables can be referenced by name in the FROM clause of a batch, as shown the following example:
  
```sql
SELECT Employee_ID, Department_ID FROM @MyTableVar;  
```  
  
Outside a FROM clause, **table** variables must be referenced by using an alias, as shown in the following example:
  
```sql
SELECT EmployeeID, DepartmentID   
FROM @MyTableVar m  
JOIN Employee on (m.EmployeeID =Employee.EmployeeID AND  
   m.DepartmentID = Employee.DepartmentID);  
```  
  
**table** variables provide the following benefits for small-scale queries that have query plans that do not change and when recompilation concerns are dominant:
-   A **table** variable behaves like a local variable. It has a well-defined scope. This is the function, stored procedure, or batch that it is declared in.  
     Within its scope, a **table** variable can be used like a regular table. It may be applied anywhere a table or table expression is used in SELECT, INSERT, UPDATE, and DELETE statements. However, **table** cannot be used in the following statement:  
  
```sql
SELECT select_list INTO table_variable;
```
  
**table** variables are automatically cleaned up at the end of the function, stored procedure, or batch in which they are defined.
  
-   **table** variables used in stored procedures cause fewer recompilations of the stored procedures than when temporary tables are used when there are no cost-based choices that affect performance.  
-   Transactions involving **table** variables last only for the duration of an update on the **table** variable. Therefore, **table** variables require less locking and logging resources.  
  
## Limitations and restrictions
**Table** variables does not have distribution statistics, theywill not trigger recompiles. Therefore, in many cases, the optimizer will build a query plan on the assumption that the table variable has no rows. For this reason, you should be cautious about using a table variable if you expect a larger number of rows (greater than 100). Temp tables may be a better solution in this case. Alternatively, for queries that join the table variable with other tables, use the RECOMPILE hint, which will cause the optimizer to use the correct cardinality for the table variable.
  
**table** variables are not supported in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] optimizer's cost-based reasoning model. Therefore, they should not be used when cost-based choices are required to achieve an efficient query plan. Temporary tables are preferred when cost-based choices are required. This typically includes queries with joins, parallelism decisions, and index selection choices.
  
Queries that modify **table** variables do not generate parallel query execution plans. Performance can be affected when very large **table** variables, or **table** variables in complex queries, are modified. In these situations, consider using temporary tables instead. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md). Queries that read **table** variables without modifying them can still be parallelized.
  
Indexes cannot be created explicitly on **table** variables, and no statistics are kept on **table** variables. In some cases, performance may improve by using temporary tables instead, which support indexes and statistics. For more information about temporary tables, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).
  
CHECK constraints, DEFAULT values and computed columns in the **table** type declaration cannot call user-defined functions.
  
Assignment operation between **table** variables is not supported.
  
Because **table** variables have limited scope and are not part of the persistent database, they are not affected by transaction rollbacks.
  
Table variables cannot be altered after creation.
  
## Examples  
  
### A. Declaring a variable of type table  
The following example creates a `table` variable that stores the values specified in the OUTPUT clause of the UPDATE statement. Two `SELECT` statements follow that return the values in `@MyTableVar` and the results of the update operation in the `Employee` table. Note that the results in the `INSERTED.ModifiedDate` column differ from the values in the `ModifiedDate` column in the `Employee` table. This is because the `AFTER UPDATE` trigger, which updates the value of `ModifiedDate` to the current date, is defined on the `Employee` table. However, the columns returned from `OUTPUT` reflect the data before triggers are fired. For more information, see [OUTPUT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/output-clause-transact-sql.md).
  
```sql
USE AdventureWorks2012;  
GO  
DECLARE @MyTableVar table(  
    EmpID int NOT NULL,  
    OldVacationHours int,  
    NewVacationHours int,  
    ModifiedDate datetime);  
UPDATE TOP (10) HumanResources.Employee  
SET VacationHours = VacationHours * 1.25   
OUTPUT INSERTED.BusinessEntityID,  
       DELETED.VacationHours,  
       INSERTED.VacationHours,  
       INSERTED.ModifiedDate  
INTO @MyTableVar;  
--Display the result set of the table variable.  
SELECT EmpID, OldVacationHours, NewVacationHours, ModifiedDate  
FROM @MyTableVar;  
GO  
--Display the result set of the table.  
--Note that ModifiedDate reflects the value generated by an  
--AFTER UPDATE trigger.  
SELECT TOP (10) BusinessEntityID, VacationHours, ModifiedDate  
FROM HumanResources.Employee;  
GO  
```  
  
### B. Creating an inline table-valued function  
The following example returns an inline table-valued function. It returns three columns `ProductID`, `Name` and the aggregate of year-to-date totals by store as `YTD Total` for each product sold to the store.
  
```sql
USE AdventureWorks2012;  
GO  
IF OBJECT_ID (N'Sales.ufn_SalesByStore', N'IF') IS NOT NULL  
    DROP FUNCTION Sales.ufn_SalesByStore;  
GO  
CREATE FUNCTION Sales.ufn_SalesByStore (@storeid int)  
RETURNS TABLE  
AS  
RETURN   
(  
    SELECT P.ProductID, P.Name, SUM(SD.LineTotal) AS 'Total'  
    FROM Production.Product AS P   
    JOIN Sales.SalesOrderDetail AS SD ON SD.ProductID = P.ProductID  
    JOIN Sales.SalesOrderHeader AS SH ON SH.SalesOrderID = SD.SalesOrderID  
    JOIN Sales.Customer AS C ON SH.CustomerID = C.CustomerID  
    WHERE C.StoreID = @storeid  
    GROUP BY P.ProductID, P.Name  
);  
GO  
```  
  
To invoke the function, run this query.
  
```sql
SELECT * FROM Sales.ufn_SalesByStore (602);  
```  
  
## See also
[COLLATE &#40;Transact-SQL&#41;](http://msdn.microsoft.com/library/4ba6b7d8-114a-4f4e-bb38-fe5697add4e9)  
[CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)  
[User-Defined Functions](../../relational-databases/user-defined-functions/user-defined-functions.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[Use Table-Valued Parameters &#40;Database Engine&#41;](../../relational-databases/tables/use-table-valued-parameters-database-engine.md)  
[Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md)
  
  
