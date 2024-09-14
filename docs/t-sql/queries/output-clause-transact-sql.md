---
title: "OUTPUT clause (Transact-SQL)"
description: Returns information from, or expressions based on, each row affected by an INSERT, UPDATE, DELETE, or MERGE statement.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 09/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OUTPUT_TSQL"
  - "OUTPUT"
helpviewer_keywords:
  - "displaying updated rows"
  - "INSERT statement [SQL Server], OUTPUT clause"
  - "outputs [SQL Server]"
  - "OUTPUT clause"
  - "row additions [SQL Server], OUTPUT clause"
  - "viewing updated rows"
  - "row deletions [SQL Server], OUTPUT clause"
  - "viewing deleted rows"
  - "DELETE statement [SQL Server], OUTPUT clause"
  - "row updates [SQL Server]"
  - "displaying inserted rows"
  - "viewing inserted rows"
  - "displaying deleted rows"
  - "UPDATE statement [SQL Server], OUTPUT clause"
dev_langs:
  - "TSQL"
---
# OUTPUT clause (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns information from, or expressions based on, each row affected by an `INSERT`, `UPDATE`, `DELETE`, or `MERGE` statement. These results can be returned to the processing application for use in such things as confirmation messages, archiving, and other such application requirements. The results can also be inserted into a table or table variable. Additionally, you can capture the results of an `OUTPUT` clause in a nested `INSERT`, `UPDATE`, `DELETE`, or `MERGE` statement, and insert those results into a target table or view.

> [!NOTE]  
> An `UPDATE`, `INSERT`, or `DELETE` statement that has an `OUTPUT` clause will return rows to the client even if the statement encounters errors and is rolled back. The result shouldn't be used if any error occurs when you run the statement.

**Used in:**

- [DELETE](../statements/delete-transact-sql.md)
- [INSERT](../statements/insert-transact-sql.md)
- [UPDATE](update-transact-sql.md)
- [MERGE](../statements/merge-transact-sql.md)

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
<OUTPUT_CLAUSE> ::=
{
    [ OUTPUT <dml_select_list> INTO { @table_variable | output_table } [ ( column_list ) ] ]
    [ OUTPUT <dml_select_list> ]
}
<dml_select_list> ::=
{ <column_name> | scalar_expression } [ [ AS ] column_alias_identifier ]
    [ , ...n ]

<column_name> ::=
{ DELETED | INSERTED | from_table_name } . { * | column_name }
    | $action
```

## Arguments

#### *@table_variable*

Specifies a **table** variable that the returned rows are inserted into instead of being returned to the caller. *@table_variable* must be declared before the `INSERT`, `UPDATE`, `DELETE`, or `MERGE` statement.

If *column_list* isn't specified, the **table** variable must have the same number of columns as the `OUTPUT` result set. The exceptions are identity and computed columns, which must be skipped. If *column_list* is specified, any omitted columns must either allow null values or have default values assigned to them.

For more information about **table** variables, see [table](../data-types/table-transact-sql.md).

#### *output_table*

Specifies a table that the returned rows are inserted into instead of being returned to the caller. *output_table* might be a temporary table.

If *column_list* isn't specified, the table must have the same number of columns as the `OUTPUT` result set. The exceptions are identity and computed columns, which must be skipped. If *column_list* is specified, any omitted columns must either allow null values or have default values assigned to them.

*output_table* can't:

- Have enabled triggers defined on it.
- Participate on either side of a `FOREIGN KEY` constraint.
- Have `CHECK` constraints or enabled rules.

#### *column_list*

An optional list of column names on the target table of the `INTO` clause. It's analogous to the column list allowed in the [INSERT](../statements/insert-transact-sql.md) statement.

#### *scalar_expression*

Any combination of symbols and operators that evaluates to a single value. Aggregate functions aren't permitted in *scalar_expression*.

Any reference to columns in the table being modified must be qualified with the `INSERTED` or `DELETED` prefix.

#### *column_alias_identifier*

An alternative name used to reference the column name.

#### DELETED

A column prefix that specifies the value deleted by the update or delete operation, and any existing values that don't change with the current operation. Columns prefixed with `DELETED` reflect the value before the `UPDATE`, `DELETE`, or `MERGE` statement is completed.

`DELETED` can't be used with the `OUTPUT` clause in the `INSERT` statement.

#### INSERTED

A column prefix that specifies the value added by the insert or update operation, and any existing values that don't change with the current operation. Columns prefixed with `INSERTED` reflect the value after the `UPDATE`, `INSERT`, or `MERGE` statement is completed but before triggers are executed.

`INSERTED` can't be used with the `OUTPUT` clause in the `DELETE` statement.

#### *from_table_name*

A column prefix that specifies a table included in the `FROM` clause of a `DELETE`, `UPDATE`, or `MERGE` statement that is used to specify the rows to update or delete.

If the table being modified is also specified in the `FROM` clause, any reference to columns in that table must be qualified with the `INSERTED` or `DELETED` prefix.

#### `*`

The asterisk (`*`) specifies that all columns affected by the delete, insert, or update action are returned in the order in which they exist in the table.

For example, `OUTPUT DELETED.*` in the following `DELETE` statement returns all columns deleted from the `ShoppingCartItem` table:

```sql
DELETE Sales.ShoppingCartItem
    OUTPUT DELETED.*;
```

#### *column_name*

An explicit column reference. Any reference to the table being modified must be correctly qualified by either the `INSERTED` or the `DELETED` prefix as appropriate, for example: `INSERTED.<column_name>`.

#### $action

Available only for the `MERGE` statement. Specifies a column of type **nvarchar(10)** in the `OUTPUT` clause in a `MERGE` statement that returns one of three values for each row: `INSERT`, `UPDATE`, or `DELETE`, according to the action that was performed on that row.

## Remarks

The `OUTPUT <dml_select_list>` clause and the `OUTPUT <dml_select_list> INTO { @table_variable | output_table }` clause can be defined in a single `INSERT`, `UPDATE`, `DELETE`, or `MERGE` statement.

> [!NOTE]  
> Unless specified otherwise, references to the `OUTPUT` clause refer to both the `OUTPUT` clause and the `OUTPUT INTO` clause.

The `OUTPUT` clause might be useful to retrieve the value of identity or computed columns after an `INSERT` or `UPDATE` operation.

When a computed column is included in the `<dml_select_list>`, the corresponding column in the output table or table variable isn't a computed column. The values in the new column are the values that were computed at the time the statement was executed.

The order in which the changes are applied to the table, and the order in which the rows are inserted into the output table or table variable, aren't guaranteed to correspond.

If parameters or variables are modified as part of an `UPDATE` statement, the `OUTPUT` clause always returns the value of the parameter or variable as it was before the statement executed instead of the modified value.

You can use `OUTPUT` with an `UPDATE` or `DELETE` statement positioned on a cursor that uses `WHERE CURRENT OF` syntax.

The `OUTPUT` clause isn't supported in the following statements:

- DML statements that reference local partitioned views, distributed partitioned views, or remote tables.

- `INSERT` statements that contain an `EXECUTE` statement.

- Full-text predicates aren't allowed in the `OUTPUT` clause when the database compatibility level is set to 100.

- The `OUTPUT INTO` clause can't be used to insert into a view, or rowset function.

- A user-defined function can't be created if it contains an `OUTPUT INTO` clause that has a table as its target.

To prevent nondeterministic behavior, the `OUTPUT` clause can't contain the following references:

- Subqueries or user-defined functions that perform user or system data access, or are assumed to perform such access. User-defined functions are assumed to perform data access if they aren't schema-bound.

- A column from a view or inline table-valued function when that column is defined by one of the following methods:

  - A subquery.

  - A user-defined function that performs user or system data access, or is assumed to perform such access.

  - A computed column that contains a user-defined function that performs user or system data access in its definition.

  When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] detects such a column in the `OUTPUT` clause, error 4186 is raised.

## Insert data returned from an OUTPUT clause into a table

When you're capturing the results of an `OUTPUT` clause in a nested `INSERT`, `UPDATE`, `DELETE`, or `MERGE` statement and inserting those results into a target table, keep the following information in mind:

- The whole operation is atomic. Either both the `INSERT` statement and the nested DML statement that contains the `OUTPUT` clause execute, or the whole statement fails.

- The following restrictions apply to the target of the outer `INSERT` statement:

  - The target can't be a remote table, view, or common table expression.

  - The target can't have a `FOREIGN KEY` constraint, or be referenced by a `FOREIGN KEY` constraint.

  - Triggers can't be defined on the target.

  - The target can't participate in merge replication or updatable subscriptions for transactional replication.

- The following restrictions apply to the nested DML statement:

  - The target can't be a remote table or partitioned view.

  - The source itself can't contain a `<dml_table_source>` clause.

- The `OUTPUT INTO` clause isn't supported in `INSERT` statements that contain a `<dml_table_source>` clause.

- `@@ROWCOUNT` returns the rows inserted only by the outer `INSERT` statement.

- `@@IDENTITY`, `SCOPE_IDENTITY`, and `IDENT_CURRENT` return identity values generated only by the nested DML statement, and not values generated by the outer `INSERT` statement.

- Query notifications treat the statement as a single entity, and the type of any message that is created is the type of the nested DML, even if the significant change is from the outer `INSERT` statement itself.

- In the `<dml_table_source>` clause, the `SELECT` and `WHERE` clauses can't include subqueries, aggregate functions, ranking functions, full-text predicates, user-defined functions that perform data access, or the `TEXTPTR()` function.

## Parallelism

An `OUTPUT` clause that returns results to the client, or table variable, always uses a serial plan.

In the context of a database set to compatibility level 130 or higher, if an `INSERT...SELECT` operation uses a `WITH (TABLOCK)` hint for the `SELECT` statement and also uses `OUTPUT...INTO` to insert into a temporary or user table, then the target table for the `INSERT...SELECT` is eligible for parallelism depending on the subtree cost. The target table referenced in the `OUTPUT INTO` clause isn't eligible for parallelism.

## Triggers

Columns returned from `OUTPUT` reflect the data as it's after the `INSERT`, `UPDATE`, or `DELETE` statement completes, but before triggers are executed.

For `INSTEAD OF` triggers, the returned results are generated as if the `INSERT`, `UPDATE`, or `DELETE` had actually occurred, even if no modifications take place as the result of the trigger operation. If a statement that includes an `OUTPUT` clause is used inside the body of a trigger, table aliases must be used to reference the trigger inserted and deleted tables to avoid duplicating column references with the `INSERTED` and `DELETED` tables associated with `OUTPUT`.

If the `OUTPUT` clause is specified without also specifying the `INTO` keyword, the target of the DML operation can't have any enabled trigger defined on it for the given DML action. For example, if the `OUTPUT` clause is defined in an `UPDATE` statement, the target table can't have any enabled `UPDATE` triggers.

If the `sp_configure` option disallow results from triggers is set, an `OUTPUT` clause without an `INTO` clause causes the statement to fail when it's invoked from within a trigger.

## Data types

The `OUTPUT` clause supports the large object data types: **nvarchar(max)**, **varchar(max)**, **varbinary(max)**, **text**, **ntext**, **image**, and **xml**. When you use the `.WRITE` clause in the `UPDATE` statement to modify an **nvarchar(max)**, **varchar(max)**, or **varbinary(max)** column, the full before and after images of the values are returned if they're referenced. The `TEXTPTR()` function can't appear as part of an expression on a **text**, **ntext**, or **image** column in the `OUTPUT` clause.

## Queues

You can use `OUTPUT` in applications that use tables as queues, or to hold intermediate result sets. That is, the application is constantly adding or removing rows from the table. The following example uses the `OUTPUT` clause in a `DELETE` statement to return the deleted row to the calling application.

```sql
USE AdventureWorks2022;
GO

DELETE TOP(1) dbo.DatabaseLog WITH (READPAST)
OUTPUT DELETED.*
WHERE DatabaseLogID = 7;
GO
```

This example removes a row from a table used as a queue and returns the deleted values to the processing application in a single action. Other semantics might also be implemented, such as using a table to implement a stack. However, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't guarantee the order in which rows are processed and returned by DML statements using the `OUTPUT` clause. It's up to the application to include an appropriate `WHERE` clause that can guarantee the desired semantics, or understand that when multiple rows might qualify for the DML operation, there's no guaranteed order. The following example uses a subquery and assumes uniqueness is a characteristic of the `DatabaseLogID` column in order to implement the desired ordering semantics.

```sql
USE tempdb;
GO

CREATE TABLE dbo.table1
(
    id INT,
    employee VARCHAR(32)
);
GO

INSERT INTO dbo.table1
VALUES (1, 'Fred'),
    (2, 'Tom'),
    (3, 'Sally'),
    (4, 'Alice');
GO

DECLARE @MyTableVar TABLE (
    id INT,
    employee VARCHAR(32)
);

PRINT 'table1, before delete';

SELECT *
FROM dbo.table1;

DELETE
FROM dbo.table1
OUTPUT DELETED.*
INTO @MyTableVar
WHERE id = 4
    OR id = 2;

PRINT 'table1, after delete';

SELECT *
FROM dbo.table1;

PRINT '@MyTableVar, after delete';

SELECT *
FROM @MyTableVar;

DROP TABLE dbo.table1;
```

Here are the results:

```output
table1, before delete
id          employee
----------- ------------------------------
1           Fred
2           Tom
3           Sally
4           Alice

table1, after delete
id          employee
----------- ------------------------------
1           Fred
3           Sally

@MyTableVar, after delete
id          employee
----------- ------------------------------
2           Tom
4           Alice
```

> [!NOTE]  
> Use the `READPAST` table hint in `UPDATE` and `DELETE` statements if your scenario allows for multiple applications to perform a destructive read from one table. This prevents locking issues that can come up if another application is already reading the first qualifying record in the table.

## Permissions

`SELECT` permissions are required on any columns retrieved through `<dml_select_list>` or used in `<scalar_expression>`.

`INSERT` permissions are required on any tables specified in `<output_table>`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use OUTPUT INTO with an INSERT statement

The following example inserts a row into the `ScrapReason` table and uses the `OUTPUT` clause to return the results of the statement to the `@MyTableVar` table variable. Because the `ScrapReasonID` column is defined with an IDENTITY property, a value isn't specified in the `INSERT` statement for that column. However, the value generated by the [!INCLUDE [ssDE](../../includes/ssde-md.md)] for that column is returned in the `OUTPUT` clause in the column `INSERTED.ScrapReasonID`.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTableVar TABLE (
    NewScrapReasonID SMALLINT,
    Name VARCHAR(50),
    ModifiedDate DATETIME
);

INSERT Production.ScrapReason
    OUTPUT INSERTED.ScrapReasonID, INSERTED.Name, INSERTED.ModifiedDate
        INTO @MyTableVar
VALUES (N'Operator error', GETDATE());

--Display the result set of the table variable.
SELECT NewScrapReasonID, Name, ModifiedDate FROM @MyTableVar;
--Display the result set of the table.
SELECT ScrapReasonID, Name, ModifiedDate
FROM Production.ScrapReason;
GO
```

### B. Use OUTPUT with a DELETE statement

The following example deletes all rows in the `ShoppingCartItem` table. The clause `OUTPUT DELETED.*` specifies that the results of the `DELETE` statement, that is, all columns in the deleted rows, are returned to the calling application. The `SELECT` statement that follows verifies the results of the delete operation on the `ShoppingCartItem` table.

```sql
USE AdventureWorks2022;
GO

DELETE Sales.ShoppingCartItem
OUTPUT DELETED.*
WHERE ShoppingCartID = 20621;

--Verify the rows in the table matching the WHERE clause have been deleted.
SELECT COUNT(*) AS [Rows in Table] FROM Sales.ShoppingCartItem WHERE ShoppingCartID = 20621;
GO
```

### C. Use OUTPUT INTO with an UPDATE statement

The following example updates the `VacationHours` column in the `Employee` table by 25 percent for the first 10 rows. The `OUTPUT` clause returns the `VacationHours` value that exists before applying the `UPDATE` statement in the column `DELETED.VacationHours`, and the updated value in the column `INSERTED.VacationHours` to the `@MyTableVar` table variable.

Two `SELECT` statements follow, that return the values in `@MyTableVar` and the results of the update operation in the `Employee` table.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTableVar TABLE (
    EmpID INT NOT NULL,
    OldVacationHours INT,
    NewVacationHours INT,
    ModifiedDate DATETIME);

UPDATE TOP (10) HumanResources.Employee
SET VacationHours = VacationHours * 1.25,
    ModifiedDate = GETDATE()
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
SELECT TOP (10) BusinessEntityID, VacationHours, ModifiedDate
FROM HumanResources.Employee;
GO
```

### D. Use OUTPUT INTO to return an expression

The following example builds on example C by defining an expression in the `OUTPUT` clause as the difference between the updated `VacationHours` value and the `VacationHours` value before the update was applied. The value of this expression is returned to the `@MyTableVar` table variable in the column `VacationHoursDifference`.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTableVar TABLE (
    EmpID INT NOT NULL,
    OldVacationHours INT,
    NewVacationHours INT,
    VacationHoursDifference INT,
    ModifiedDate DATETIME);

UPDATE TOP (10) HumanResources.Employee
SET VacationHours = VacationHours * 1.25,
    ModifiedDate = GETDATE()
OUTPUT INSERTED.BusinessEntityID,
       DELETED.VacationHours,
       INSERTED.VacationHours,
       INSERTED.VacationHours - DELETED.VacationHours,
       INSERTED.ModifiedDate
INTO @MyTableVar;

--Display the result set of the table variable.
SELECT EmpID, OldVacationHours, NewVacationHours,
    VacationHoursDifference, ModifiedDate
FROM @MyTableVar;
GO
SELECT TOP (10) BusinessEntityID, VacationHours, ModifiedDate
FROM HumanResources.Employee;
GO
```

### E. Use OUTPUT INTO with from_table_name in an UPDATE statement

The following example updates the `ScrapReasonID` column in the `WorkOrder` table for all work orders with a specified `ProductID` and `ScrapReasonID`. The `OUTPUT INTO` clause returns values from the table being updated (`WorkOrder`) and also from the `Product` table. The `Product` table is used in the `FROM` clause to specify the rows to update. Because the `WorkOrder` table has an `AFTER UPDATE` trigger defined on it, the `INTO` keyword is required.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTestVar TABLE (
    OldScrapReasonID INT NOT NULL,
    NewScrapReasonID INT NOT NULL,
    WorkOrderID INT NOT NULL,
    ProductID INT NOT NULL,
    ProductName NVARCHAR(50)NOT NULL);

UPDATE Production.WorkOrder
SET ScrapReasonID = 4
OUTPUT DELETED.ScrapReasonID,
       INSERTED.ScrapReasonID,
       INSERTED.WorkOrderID,
       INSERTED.ProductID,
       p.Name
    INTO @MyTestVar
FROM Production.WorkOrder AS wo
    INNER JOIN Production.Product AS p
    ON wo.ProductID = p.ProductID
    AND wo.ScrapReasonID= 16
    AND p.ProductID = 733;

SELECT OldScrapReasonID, NewScrapReasonID, WorkOrderID,
    ProductID, ProductName
FROM @MyTestVar;
GO
```

### F. Use OUTPUT INTO with from_table_name in a DELETE statement

The following example deletes rows in the `ProductProductPhoto` table based on search criteria defined in the `FROM` clause of `DELETE` statement. The `OUTPUT` clause returns columns from the table being deleted (`DELETED.ProductID`, `DELETED.ProductPhotoID`) and columns from the `Product` table. This table is used in the `FROM` clause to specify the rows to delete.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTableVar TABLE (
    ProductID INT NOT NULL,
    ProductName NVARCHAR(50)NOT NULL,
    ProductModelID INT NOT NULL,
    PhotoID INT NOT NULL);

DELETE Production.ProductProductPhoto
OUTPUT DELETED.ProductID,
       p.Name,
       p.ProductModelID,
       DELETED.ProductPhotoID
    INTO @MyTableVar
FROM Production.ProductProductPhoto AS ph
JOIN Production.Product as p
    ON ph.ProductID = p.ProductID
    WHERE p.ProductModelID BETWEEN 120 and 130;

--Display the results of the table variable.
SELECT ProductID, ProductName, ProductModelID, PhotoID
FROM @MyTableVar
ORDER BY ProductModelID;
GO
```

### G. Use OUTPUT INTO with a large object data type

The following example updates a partial value in `DocumentSummary`, an **nvarchar(max)** column in the `Production.Document` table, by using the `.WRITE` clause. The word `components` is replaced by the word `features` by specifying the replacement word, the beginning location (offset) of the word to be replaced in the existing data, and the number of characters to be replaced (length). The example uses the `OUTPUT` clause to return the before and after images of the `DocumentSummary` column to the `@MyTableVar` table variable. The full before and after images of the `DocumentSummary` column are returned.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTableVar TABLE (
    SummaryBefore NVARCHAR(MAX),
    SummaryAfter NVARCHAR(MAX)
);

UPDATE Production.Document
SET DocumentSummary.WRITE(N'features', 28, 10)
OUTPUT DELETED.DocumentSummary,
       INSERTED.DocumentSummary
    INTO @MyTableVar
WHERE Title = N'Front Reflector Bracket Installation';

SELECT SummaryBefore, SummaryAfter
FROM @MyTableVar;
GO
```

### H. Use OUTPUT in an INSTEAD OF trigger

The following example uses the `OUTPUT` clause in a trigger to return the results of the trigger operation. First, a view is created on the `ScrapReason` table, and then an `INSTEAD OF INSERT` trigger is defined on the view that lets only the `Name` column of the base table to be modified by the user. Because the column `ScrapReasonID` is an `IDENTITY` column in the base table, the trigger ignores the user-supplied value. This allows the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to automatically generate the correct value. Also, the value supplied by the user for `ModifiedDate` is ignored and is set to the current date. The `OUTPUT` clause returns the values actually inserted into the `ScrapReason` table.

```sql
USE AdventureWorks2022;
GO

IF OBJECT_ID('dbo.vw_ScrapReason', 'V') IS NOT NULL
    DROP VIEW dbo.vw_ScrapReason;
GO

CREATE VIEW dbo.vw_ScrapReason
AS
SELECT ScrapReasonID,
    Name,
    ModifiedDate
FROM Production.ScrapReason;
GO

CREATE TRIGGER dbo.io_ScrapReason ON dbo.vw_ScrapReason
INSTEAD OF INSERT
AS
BEGIN
    --ScrapReasonID is not specified in the list of columns to be inserted
    --because it is an IDENTITY column.
    INSERT INTO Production.ScrapReason (
        Name,
        ModifiedDate
    )
    OUTPUT INSERTED.ScrapReasonID,
        INSERTED.Name,
        INSERTED.ModifiedDate
    SELECT Name, GETDATE()
    FROM INSERTED;
END
GO

INSERT vw_ScrapReason (
    ScrapReasonID,
    Name,
    ModifiedDate
)
VALUES (
    99,
    N'My scrap reason',
    '20030404'
);
GO
```

Here is the result set generated on April 12, 2004 ('`2004-04-12'`). The `ScrapReasonIDActual` and `ModifiedDate` columns reflect the values generated by the trigger operation instead of the values provided in the `INSERT` statement.

```output
ScrapReasonID  Name             ModifiedDate
-------------  ---------------- -----------------------
17             My scrap reason  2004-04-12 16:23:33.050
```

### I. Use OUTPUT INTO with identity and computed columns

The following example creates the `EmployeeSales` table and then inserts several rows into it using an `INSERT` statement with a `SELECT` statement to retrieve data from source tables. The `EmployeeSales` table contains an identity column (`EmployeeID`) and a computed column (`ProjectedSales`).

```sql
USE AdventureWorks2022;
GO

IF OBJECT_ID('dbo.EmployeeSales', 'U') IS NOT NULL
    DROP TABLE dbo.EmployeeSales;
GO

CREATE TABLE dbo.EmployeeSales (
    EmployeeID INT IDENTITY(1, 5) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    FirstName NVARCHAR(20) NOT NULL,
    CurrentSales MONEY NOT NULL,
    ProjectedSales AS CurrentSales * 1.10
);
GO

DECLARE @MyTableVar TABLE (
    EmployeeID INT NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    FirstName NVARCHAR(20) NOT NULL,
    CurrentSales MONEY NOT NULL,
    ProjectedSales MONEY NOT NULL
);

INSERT INTO dbo.EmployeeSales (
    LastName,
    FirstName,
    CurrentSales
)
OUTPUT INSERTED.EmployeeID,
    INSERTED.LastName,
    INSERTED.FirstName,
    INSERTED.CurrentSales,
    INSERTED.ProjectedSales
INTO @MyTableVar
SELECT c.LastName,
    c.FirstName,
    sp.SalesYTD
FROM Sales.SalesPerson AS sp
INNER JOIN Person.Person AS c
    ON sp.BusinessEntityID = c.BusinessEntityID
WHERE sp.BusinessEntityID LIKE '2%'
ORDER BY c.LastName,
    c.FirstName;

SELECT EmployeeID,
    LastName,
    FirstName,
    CurrentSales,
    ProjectedSales
FROM @MyTableVar;
GO

SELECT EmployeeID,
    LastName,
    FirstName,
    CurrentSales,
    ProjectedSales
FROM dbo.EmployeeSales;
GO
```

### J. Use OUTPUT and OUTPUT INTO in a single statement

The following example deletes rows in the `ProductProductPhoto` table based on search criteria defined in the `FROM` clause of `DELETE` statement. The `OUTPUT INTO` clause returns columns from the table being deleted (`DELETED.ProductID`, `DELETED.ProductPhotoID`) and columns from the `Product` table to the `@MyTableVar` table variable. The `Product` table is used in the `FROM` clause to specify the rows to delete. The `OUTPUT` clause returns the `DELETED.ProductID`, `DELETED.ProductPhotoID` columns, and the date and time the row was deleted from the `ProductProductPhoto` table to the calling application.

```sql
USE AdventureWorks2022;
GO

DECLARE @MyTableVar TABLE (
    ProductID INT NOT NULL,
    ProductName NVARCHAR(50) NOT NULL,
    ProductModelID INT NOT NULL,
    PhotoID INT NOT NULL
);

DELETE Production.ProductProductPhoto
OUTPUT DELETED.ProductID,
    p.Name,
    p.ProductModelID,
    DELETED.ProductPhotoID
INTO @MyTableVar
OUTPUT DELETED.ProductID,
    DELETED.ProductPhotoID,
    GETDATE() AS DeletedDate
FROM Production.ProductProductPhoto AS ph
INNER JOIN Production.Product AS p
    ON ph.ProductID = p.ProductID
WHERE p.ProductID BETWEEN 800
        AND 810;

--Display the results of the table variable.
SELECT ProductID,
    ProductName,
    PhotoID,
    ProductModelID
FROM @MyTableVar;
GO
```

### K. Insert data returned from an OUTPUT clause

The following example captures data returned from the `OUTPUT` clause of a `MERGE` statement, and inserts that data into another table. The `MERGE` statement updates the `Quantity` column of the `ProductInventory` table daily, based on orders that are processed in the `SalesOrderDetail` table. It also deletes rows for products whose inventories drop to `0` or fewer. The example captures the rows that are deleted and inserts them into another table, `ZeroInventory`, which tracks products with no inventory.

```sql
USE AdventureWorks2022;
GO

IF OBJECT_ID(N'Production.ZeroInventory', N'U') IS NOT NULL
    DROP TABLE Production.ZeroInventory;
GO

--Create ZeroInventory table.
CREATE TABLE Production.ZeroInventory (
    DeletedProductID INT,
    RemovedOnDate DATETIME
    );
GO

INSERT INTO Production.ZeroInventory (
    DeletedProductID,
    RemovedOnDate
)
SELECT ProductID,
    GETDATE()
FROM (
    MERGE Production.ProductInventory AS pi
    USING (
        SELECT ProductID,
            SUM(OrderQty)
        FROM Sales.SalesOrderDetail AS sod
        INNER JOIN Sales.SalesOrderHeader AS soh
            ON sod.SalesOrderID = soh.SalesOrderID
                AND soh.OrderDate = '20070401'
        GROUP BY ProductID
        ) AS src(ProductID, OrderQty)
        ON (pi.ProductID = src.ProductID)
    WHEN MATCHED
        AND pi.Quantity - src.OrderQty <= 0
        THEN
            DELETE
    WHEN MATCHED
        THEN
            UPDATE
            SET pi.Quantity = pi.Quantity - src.OrderQty
    OUTPUT $ACTION,
        DELETED.ProductID
    ) AS Changes(Action, ProductID)
WHERE Action = 'DELETE';

IF @@ROWCOUNT = 0
    PRINT 'Warning: No rows were inserted';
GO

SELECT DeletedProductID,
    RemovedOnDate
FROM Production.ZeroInventory;
GO
```

## Related content

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
- [table (Transact-SQL)](../data-types/table-transact-sql.md)
- [CREATE TRIGGER (Transact-SQL)](../statements/create-trigger-transact-sql.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
