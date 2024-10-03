---
title: "table (Transact-SQL)"
description: "table (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 04/05/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "table data type [SQL Server]"
  - "table variables [SQL Server]"
dev_langs:
  - "TSQL"
---
# table (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

**table** is a special data type used to store a result set for processing at a later time. **table** is primarily used for temporarily storing a set of rows that are returned as the table-valued function result set. Functions and variables can be declared to be of type **table**. **table** variables can be used in functions, stored procedures, and batches. To declare variables of type **table**, use [DECLARE @local_variable](../../t-sql/language-elements/declare-local-variable-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
table_type_definition ::=
    TABLE ( { <column_definition> | <table_constraint> } [ , ...n ] )

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
     { { PRIMARY KEY | UNIQUE } ( column_name [ , ...n ] )
     | CHECK ( logical_expression )
     }
```

## Arguments

#### *table_type_definition*

The same subset of information that is used to define a table in CREATE TABLE. The table declaration includes column definitions, names, data types, and constraints. The only constraint types allowed are PRIMARY KEY, UNIQUE KEY, and NULL.

For more information about the syntax, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md), [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md), and [DECLARE @local_variable (Transact-SQL)](../../t-sql/language-elements/declare-local-variable-transact-sql.md).

#### *collation_definition*

The collation of the column that is made up of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows locale and a comparison style, a Windows locale, and the binary notation, or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation. If *collation_definition* isn't specified, the column inherits the collation of the current database. Or if the column is defined as a common language runtime (CLR) user-defined type, the column inherits the collation of the user-defined type.

## Remarks

**table** Reference variables by name in a batch's FROM clause, as shown the following example:

```sql
SELECT Employee_ID, Department_ID FROM @MyTableVar;
```

Outside a FROM clause, **table** variables must be referenced by using an alias, as shown in the following example:

```sql
SELECT EmployeeID,
    DepartmentID
FROM @MyTableVar m
INNER JOIN Employee
    ON m.EmployeeID = Employee.EmployeeID
    AND m.DepartmentID = Employee.DepartmentID;
```

**table** variables provide the following benefits over temporary tables for small-scale queries that have query plans that don't change and when recompilation concerns are dominant:

- A **table** variable behaves like a local variable. It has a well-defined scope. This variable can be used in the function, stored procedure, or batch in which it's declared.

  Within its scope, a **table** variable can be used like a regular table. It may be applied anywhere a table or table expression is used in SELECT, INSERT, UPDATE, and DELETE statements. However, **table** can't be used in the following statement:

```sql
SELECT select_list INTO table_variable;
```

**table** variables are automatically cleaned up at the end of the function, stored procedure, or batch in which they're defined.

- **table** variables that are used in stored procedures cause fewer stored procedure recompilations than when temporary tables are used when there are no cost-based choices that affect performance.

  Table variables are completely isolated to the batch that creates them so no *re-resolution* has to occur when a CREATE or ALTER statement takes place, which may occur with a temporary table. Temporary tables need this re-resolution so the table can be referenced from a nested stored procedure. Table variables avoid this step completely, so stored procedures can use plan that is already compiled, thus saving resources to process the stored procedure.

- Transactions involving **table** variables last only for the duration of an update on the **table** variable. As such, **table** variables require fewer locking and logging resources.

## Limitations and restrictions

**table** variables don't have distribution statistics. They don't trigger recompiles. In many cases, the optimizer builds a query plan on the assumption that the table variable has no rows. For this reason, you should be cautious about using a table variable if you expect a larger number of rows (greater than 100). Temp tables may be a better solution in this case. For queries that join the table variable with other tables, use the RECOMPILE hint, which causes the optimizer to use the correct cardinality for the table variable.

**table** variables aren't supported in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] optimizer's cost-based reasoning model. As such, they shouldn't be used when cost-based choices are required to achieve an efficient query plan. Temporary tables are preferred when cost-based choices are required. This plan typically includes queries with joins, parallelism decisions, and index selection choices.

Queries that modify **table** variables don't generate parallel query execution plans. Performance can be affected when large **table** variables, or **table** variables in complex queries, are modified. Consider using temporary tables instead in situations where **table** variables are modified. For more information, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md). Queries that read **table** variables without modifying them can still be parallelized.

> [!IMPORTANT]  
> Database compatibility level 150 improves the performance of table variables with the introduction of **table variable deferred compilation**.  For more information, see [Table variable deferred compilation](../../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation).

Indexes can't be created explicitly on **table** variables, and no statistics are kept on **table** variables. Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], new syntax was introduced which allows you to create certain index types inline with the table definition.  Using this new syntax, you can create indexes on **table** variables as part of the table definition. In some cases, performance may improve by using temporary tables instead, which provide full index support and statistics. For more information about temporary tables and inline index creation, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).

CHECK constraints, DEFAULT values, and computed columns in the **table** type declaration can't call user-defined functions.
Assignment operation between **table** variables isn't supported.
Because **table** variables have limited scope and aren't part of the persistent database, transaction rollbacks don't affect them.
Table variables can't be altered after creation.

Tables variables can't be used as the target of the `INTO` clause in a `SELECT ... INTO` statement.

You can't use the EXEC statement or the `sp_executesql` stored procedure to run a dynamic SQL Server query that refers a table variable, if the table variable was created outside the EXEC statement or the `sp_executesql` stored procedure. Because table variables can be referenced in their local scope only, an EXEC statement and a `sp_executesql` stored procedure would be outside the scope of the table variable. However, you can create the table variable and perform all processing inside the EXEC statement or the `sp_executesql` stored procedure because then the table variables local scope is in the EXEC statement or the `sp_executesql` stored procedure.

A table variable isn't a memory-only structure. Because a table variable might hold more data than can fit in memory, it has to have a place on disk to store data. Table variables are created in the `tempdb` database similar to temporary tables. If memory is available, both table variables and temporary tables are created and processed while in memory (data cache).

## Table variables vs temporary tables

Choosing between table variables and temporary tables depends on these factors:

- The number of rows that are inserted to the table.
- The number of recompilations the query is saved from.
- The type of queries and their dependency on indexes and statistics for performance.

In some situations, breaking a stored procedure with temporary tables into smaller stored procedures so that recompilation takes place on smaller units is helpful.

In general, you use table variables whenever possible except when there is a significant volume of data and there is repeated use of the table. In that case, you can create indexes on the temporary table to increase query performance. However, each scenario may be different. Microsoft recommends that you test if table variables are more helpful than temporary tables for a particular query or stored procedure.

## Examples

### A. Declare a variable of type table

The following example creates a **table** variable that stores the values specified in the OUTPUT clause of the UPDATE statement. Two `SELECT` statements follow, which return the values in `@MyTableVar` and the results of the update operation in the `Employee` table. Results in the `INSERTED.ModifiedDate` column differ from the values in the `ModifiedDate` column in the `Employee` table. This difference is because the `AFTER UPDATE` trigger, which updates the value of `ModifiedDate` to the current date, is defined on the `Employee` table. However, the columns returned from `OUTPUT` reflect the data before triggers are fired. For more information, see [OUTPUT Clause (Transact-SQL)](../../t-sql/queries/output-clause-transact-sql.md).

```sql
USE AdventureWorks2022;
GO
DECLARE @MyTableVar TABLE (
    EmpID INT NOT NULL,
    OldVacationHours INT,
    NewVacationHours INT,
    ModifiedDate DATETIME
);

UPDATE TOP (10) HumanResources.Employee
SET VacationHours = VacationHours * 1.25
OUTPUT INSERTED.BusinessEntityID,
    DELETED.VacationHours,
    INSERTED.VacationHours,
    INSERTED.ModifiedDate
INTO @MyTableVar;

--Display the result set of the table variable.
SELECT EmpID,
    OldVacationHours,
    NewVacationHours,
    ModifiedDate
FROM @MyTableVar;
GO

--Display the result set of the table.
--Note that ModifiedDate reflects the value generated by an
--AFTER UPDATE trigger.
SELECT TOP (10) BusinessEntityID,
    VacationHours,
    ModifiedDate
FROM HumanResources.Employee;
GO
```

### B. Create an inline table-valued function

The following example returns an inline table-valued function. It returns three columns `ProductID`, `Name`, and the aggregate of year-to-date totals by store as `YTD Total` for each product sold to the store.

```sql
USE AdventureWorks2022;
GO
IF OBJECT_ID (N'Sales.ufn_SalesByStore', N'IF') IS NOT NULL
    DROP FUNCTION Sales.ufn_SalesByStore;
GO
CREATE FUNCTION Sales.ufn_SalesByStore (@storeid int)
RETURNS TABLE
AS
RETURN
(
    SELECT P.ProductID,
        P.Name,
        SUM(SD.LineTotal) AS 'Total'
    FROM Production.Product AS P
    INNER JOIN Sales.SalesOrderDetail AS SD
        ON SD.ProductID = P.ProductID
    INNER JOIN Sales.SalesOrderHeader AS SH
        ON SH.SalesOrderID = SD.SalesOrderID
    INNER JOIN Sales.Customer AS C
        ON SH.CustomerID = C.CustomerID
    WHERE C.StoreID = @storeid
    GROUP BY P.ProductID,
        P.Name
);
GO
```

To invoke the function, run this query.

```sql
SELECT * FROM Sales.ufn_SalesByStore (602);
```

## See also

- [COLLATE (Transact-SQL)](../statements/collations.md)
- [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md)
- [User-Defined Functions](../../relational-databases/user-defined-functions/user-defined-functions.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [Use Table-Valued Parameters (Database Engine)](../../relational-databases/tables/use-table-valued-parameters-database-engine.md)
- [Query Hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
