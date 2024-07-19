---
title: "ORDER BY clause (Transact-SQL)"
description: The ORDER BY clause sorts data returned by a query in the SQL Server Database Engine.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ORDER_TSQL"
  - "BY"
  - "ORDER_BY_TSQL"
  - "BY_TSQL"
  - "ORDER"
  - "ORDER BY"
helpviewer_keywords:
  - "ad hoc query paging"
  - "OFFSET clause"
  - "SELECT statement [SQL Server], FETCH clause"
  - "clauses [SQL Server], ORDER BY"
  - "SELECT statement [SQL Server], limiting the rows returned"
  - "data [SQL Server], limiting the rows returned"
  - "data [SQL Server], ad hoc query paging"
  - "sort orders [SQL Server]"
  - "SELECT statement [SQL Server], OFFSET clause"
  - "ORDER BY clause [Transact-SQL]"
  - "LIMIT"
  - "limiting result sets"
  - "SELECT statement [SQL Server], ORDER BY clause"
  - "query paging"
  - "data [SQL Server], sorting"
  - "limiting data returned in a query"
  - "sort orders [SQL Server], ORDER BY clause"
  - "FETCH clause"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# SELECT - ORDER BY clause (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Sorts data returned by a query in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use this clause to:

- Order the result set of a query by the specified column list and, optionally, limit the rows returned to a specified range. The order in which rows are returned in a result set aren't guaranteed unless an `ORDER BY` clause is specified.

- Determine the order in which [ranking function](../functions/ranking-functions-transact-sql.md) values are applied to the result set.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!NOTE]  
> `ORDER BY` isn't supported in `SELECT`/`INTO` or `CREATE TABLE AS SELECT` (CTAS) statements in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)].

## Syntax

Syntax for SQL Server and Azure SQL Database.

```syntaxsql
ORDER BY order_by_expression
    [ COLLATE collation_name ]
    [ ASC | DESC ]
    [ , ...n ]
[ <offset_fetch> ]

<offset_fetch> ::=
{
    OFFSET { integer_constant | offset_row_count_expression } { ROW | ROWS }
    [
      FETCH { FIRST | NEXT } { integer_constant | fetch_row_count_expression } { ROW | ROWS } ONLY
    ]
}
```

Syntax for Azure Synapse Analytics and Parallel Data Warehouse.

```syntaxsql
[ ORDER BY
    {
    order_by_expression
    [ ASC | DESC ]
    } [ , ...n ]
]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *order_by_expression*

Specifies a column or expression on which to sort the query result set. A sort column can be specified as a name or column alias, or a non-negative integer representing the position of the column in the select list.

Multiple sort columns can be specified. Column names must be unique. The sequence of the sort columns in the `ORDER BY` clause defines the organization of the sorted result set. That is, the result set is sorted by the first column and then that ordered list is sorted by the second column, and so on.

The column names referenced in the `ORDER BY` clause must correspond to either a column or column alias in the select list or to a column defined in a table specified in the `FROM` clause without any ambiguities. If the `ORDER BY` clause references a column alias from the select list, the column alias must be used on its own, and not as a part of some expression in `ORDER BY` clause, for example:

```sql
SELECT SCHEMA_NAME(schema_id) AS SchemaName
FROM sys.objects
ORDER BY SchemaName; -- correct

SELECT SCHEMA_NAME(schema_id) AS SchemaName
FROM sys.objects
ORDER BY SchemaName + ''; -- wrong
```

#### COLLATE *collation_name*

Specifies that the `ORDER BY` operation should be performed according to the collation specified in *collation_name*, and not according to the collation of the column as defined in the table or view. The *collation_name* can be either a Windows collation name or a SQL collation name. For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md). `COLLATE` is applicable only for columns of type **char**, **varchar**, **nchar**, and **nvarchar**.

#### ASC | DESC

Specifies that the values in the specified column should be sorted in ascending or descending order. `ASC` sorts from the lowest value to highest value. `DESC` sorts from highest value to lowest value. `ASC` is the default sort order. `NULL` values are treated as the lowest possible values.

#### OFFSET { *integer_constant* | *offset_row_count_expression* } { ROW | ROWS }

**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

Specifies the number of rows to skip before it starts to return rows from the query expression. The value can be an integer constant or expression that is greater than or equal to zero.

*offset_row_count_expression* can be a variable, parameter, or constant scalar subquery. When a subquery is used, it can't reference any columns defined in the outer query scope. That is, it can't be correlated with the outer query.

`ROW` and `ROWS` are synonyms and are provided for ANSI compatibility.

In query execution plans, the offset row count value is displayed in the **Offset** attribute of the `TOP` query operator.

#### FETCH { FIRST \| NEXT } { *integer_constant* \| *fetch_row_count_expression* } { ROW \| ROWS } ONLY

**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

Specifies the number of rows to return after the `OFFSET` clause has been processed. The value can be an integer constant or expression that is greater than or equal to one.

*fetch_row_count_expression* can be a variable, parameter, or constant scalar subquery. When a subquery is used, it can't reference any columns defined in the outer query scope. That is, it can't be correlated with the outer query.

`FIRST` and `NEXT` are synonyms and are provided for ANSI compatibility.

`ROW` and `ROWS` are synonyms and are provided for ANSI compatibility.

In query execution plans, the offset row count value is displayed in the **Rows** or **Top** attribute of the `TOP` query operator.

## Best practices

Avoid specifying integers in the `ORDER BY` clause as positional representations of the columns in the select list. For example, although a statement such as `SELECT ProductID, Name FROM Production.Production ORDER BY 2` is valid, the statement isn't as easily understood by others compared with specifying the actual column name. In addition, changes to the select list, such as changing the column order or adding new columns, requires modifying the `ORDER BY` clause in order to avoid unexpected results.

In a `SELECT TOP (<n>)` statement, always use an `ORDER BY` clause. This is the only way to predictably indicate which rows are affected by `TOP`. For more information, see [TOP](top-transact-sql.md).

## Interoperability

When used with a `SELECT...INTO` or `INSERT...SELECT` statement to insert rows from another source, the `ORDER BY` clause doesn't guarantee the rows are inserted in the specified order.

Using `OFFSET` and `FETCH` in a view doesn't change the updateability property of the view.

## Limitations

There's no limit to the number of columns in the `ORDER BY` clause. However, the total size of the columns specified in an `ORDER BY` clause can't exceed 8,060 bytes.

Columns of type **ntext**, **text**, **image**, **geography**, **geometry**, and **xml** can't be used in an `ORDER BY` clause.

An integer or constant can't be specified when *order_by_expression* appears in a ranking function. For more information, see [SELECT - OVER clause](select-over-clause-transact-sql.md).

If a table name is aliased in the `FROM` clause, only the alias name can be used to qualify its columns in the `ORDER BY` clause.

Column names and aliases specified in the `ORDER BY` clause must be defined in the select list if the `SELECT` statement contains one of the following clauses or operators:

- `UNION` operator
- `EXCEPT` operator
- `INTERSECT` operator
- `SELECT DISTINCT`

Additionally, when the statement includes a `UNION`, `EXCEPT`, or `INTERSECT` operator, the column names, or column aliases must be specified in the select list of the first (left-side) query.

In a query that uses `UNION`, `EXCEPT`, or `INTERSECT` operators, `ORDER BY` is allowed only at the end of the statement. This restriction applies only to when you specify `UNION`, `EXCEPT`, and `INTERSECT` in a top-level query and not in a subquery. See the [Examples](#examples) section that follows.

The `ORDER BY` clause isn't valid in views, inline functions, derived tables, and subqueries, unless either the `TOP` or `OFFSET` and `FETCH` clauses are also specified. When `ORDER BY` is used in these objects, the clause is used only to determine the rows returned by the `TOP` clause or `OFFSET` and `FETCH` clauses. The `ORDER BY` clause doesn't guarantee ordered results when these constructs are queried, unless `ORDER BY` is also specified in the query itself.

`OFFSET` and `FETCH` aren't supported in indexed views or in a view that is defined by using the `CHECK OPTION` clause.

`OFFSET` and `FETCH` can be used in any query that allows `TOP` and `ORDER BY` with the following limitations:

- The `OVER` clause doesn't support `OFFSET` and `FETCH`.

- `OFFSET` and `FETCH` can't be specified directly in `INSERT`, `UPDATE`, `MERGE`, and `DELETE` statements, but can be specified in a subquery defined in these statements. For example, in the `INSERT INTO SELECT` statement, `OFFSET` and `FETCH` can be specified in the `SELECT` statement.

- In a query that uses `UNION`, `EXCEPT` or `INTERSECT` operators, `OFFSET` and `FETCH` can only be specified in the final query that specifies the order of the query results.

- `TOP` can't be combined with `OFFSET` and `FETCH` in the same query expression (in the same query scope).

## Use OFFSET and FETCH to limit the rows returned

You should use the `OFFSET` and `FETCH` clauses instead of the `TOP` clause to implement a query paging solution and limit the number of rows sent to a client application.

Using `OFFSET` and `FETCH` as a paging solution requires running the query one time for each *page* of data returned to the client application. For example, to return the results of a query in 10-row increments, you must execute the query one time to return rows 1 to 10 and then run the query again to return rows 11 to 20, and so on. Each query is independent and not related to each other in any way. This means that, unlike using a cursor in which the query is executed once and state is maintained on the server, the client application is responsible for tracking state. To achieve stable results between query requests using `OFFSET` and `FETCH`, the following conditions must be met:

1. The underlying data that is used by the query must not change. That is, either the rows touched by the query aren't updated or all requests for pages from the query are executed in a single transaction using either snapshot or serializable transaction isolation. For more information about these transaction isolation levels, see [SET TRANSACTION ISOLATION LEVEL](../statements/set-transaction-isolation-level-transact-sql.md).

1. The `ORDER BY` clause contains a column or combination of columns that are guaranteed to be unique.

See the example "Running multiple queries in a single transaction" in the Examples section later in this article.

If consistent execution plans are important in your paging solution, consider using the `OPTIMIZE FOR` query hint for the `OFFSET` and `FETCH` parameters. See [Specify expressions for OFFSET and FETCH values](#c-specify-expressions-for-offset-and-fetch-values) in the [Examples](#examples) section later in this article. For more information about `OPTIMIZE FOR`, see [Query hints](hints-transact-sql-query.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

| Category | Featured syntax elements |
| --- | --- |
| [Basic syntax](#basic-syntax) | `ORDER BY` |
| [Specify ascending and descending order](#specify-ascending-and-descending-sort-order) | `DESC` or `ASC` |
| [Specify a collation](#specify-a-collation) | `COLLATE` |
| [Specify a conditional order](#specify-a-conditional-order) | `CASE` expression |
| [Use ORDER BY in a ranking function](#use-order-by-in-a-ranking-function) | Ranking functions |
| [Limit the number of rows returned](#limit-the-number-of-rows-returned) | `OFFSET` and `FETCH` |
| [Use ORDER BY with UNION, EXCEPT, and INTERSECT](#use-order-by-with-union-except-and-intersect) | `UNION` |

### Basic syntax

Examples in this section demonstrate the basic functionality of the `ORDER BY` clause using the minimum required syntax.

#### A. Specify a single column defined in the select list

The following example orders the result set by the numeric `ProductID` column. Because a specific sort order isn't specified, the default (ascending order) is used.

```sql
USE AdventureWorks2022;
GO

SELECT ProductID, Name
FROM Production.Product
WHERE Name LIKE 'Lock Washer%'
ORDER BY ProductID;
```

#### B. Specify a column that isn't defined in the select list

The following example orders the result set by a column that isn't included in the select list, but is defined in the table specified in the `FROM` clause.

```sql
USE AdventureWorks2022;
GO

SELECT ProductID, Name, Color
FROM Production.Product
ORDER BY ListPrice;
```

#### C. Specify an alias as the sort column

The following example specifies the column alias `SchemaName` as the sort order column.

```sql
USE AdventureWorks2022;
GO

SELECT name, SCHEMA_NAME(schema_id) AS SchemaName
FROM sys.objects
WHERE type = 'U'
ORDER BY SchemaName;
```

#### D. Specify an expression as the sort column

The following example uses an expression as the sort column. The expression is defined by using the `DATEPART` function to sort the result set by the year in which employees were hired.

```sql
USE AdventureWorks2022;
GO

SELECT BusinessEntityID, JobTitle, HireDate
FROM HumanResources.Employee
ORDER BY DATEPART(year, HireDate);
```

### Specify ascending and descending sort order

#### A. Specify a descending order

The following example orders the result set by the numeric column `ProductID` in descending order.

```sql
USE AdventureWorks2022;
GO

SELECT ProductID, Name
FROM Production.Product
WHERE Name LIKE 'Lock Washer%'
ORDER BY ProductID DESC;
```

#### B. Specify an ascending order

The following example orders the result set by the `Name` column in ascending order. The characters are sorted alphabetically, not numerically. That is, 10 sorts before 2.

```sql
USE AdventureWorks2022;
GO

SELECT ProductID, Name
FROM Production.Product
WHERE Name LIKE 'Lock Washer%'
ORDER BY Name ASC;
```

#### C. Specify both ascending and descending order

The following example orders the result set by two columns. The query result set is first sorted in ascending order by the `FirstName` column and then sorted in descending order by the `LastName` column.

```sql
USE AdventureWorks2022;
GO

SELECT LastName, FirstName
FROM Person.Person
WHERE LastName LIKE 'R%'
ORDER BY FirstName ASC, LastName DESC;
```

### Specify a collation

The following example shows how specifying a collation in the `ORDER BY` clause can change the order in which the query results are returned. A table is created that contains a column defined by using a case-insensitive, accent-insensitive collation. Values are inserted with various case and accent differences. Because a collation isn't specified in the `ORDER BY` clause, the first query uses the collation of the column when sorting the values. In the second query, a case-sensitive, accent-sensitive collation is specified in the `ORDER BY` clause, which changes the order in which the rows are returned.

```sql
USE tempdb;
GO

CREATE TABLE #t1 (name NVARCHAR(15) COLLATE Latin1_General_CI_AI);
GO

INSERT INTO #t1
VALUES (N'Sánchez'),
    (N'Sanchez'),
    (N'sánchez'),
    (N'sanchez');

-- This query uses the collation specified for the column 'name' for sorting.
SELECT name
FROM #t1
ORDER BY name;

-- This query uses the collation specified in the ORDER BY clause for sorting.
SELECT name
FROM #t1
ORDER BY name COLLATE Latin1_General_CS_AS;
```

### Specify a conditional order

The following examples use the `CASE` expression in an `ORDER BY` clause to conditionally determine the sort order of the rows based on a given column value. In the first example, the value in the `SalariedFlag` column of the `HumanResources.Employee` table is evaluated. Employees that have the `SalariedFlag` set to 1 are returned in order by the `BusinessEntityID` in descending order. Employees that have the `SalariedFlag` set to 0 are returned in order by the `BusinessEntityID` in ascending order. In the second example, the result set is ordered by the column `TerritoryName` when the column `CountryRegionName` is equal to 'United States' and by `CountryRegionName` for all other rows.

```sql
SELECT BusinessEntityID,
    SalariedFlag
FROM HumanResources.Employee
ORDER BY
    CASE SalariedFlag
        WHEN 1 THEN BusinessEntityID
    END DESC,
    CASE 
        WHEN SalariedFlag = 0 THEN BusinessEntityID
    END;
GO
```

```sql
SELECT BusinessEntityID,
    LastName,
    TerritoryName,
    CountryRegionName
FROM Sales.vSalesPerson
WHERE TerritoryName IS NOT NULL
ORDER BY
    CASE CountryRegionName
        WHEN 'United States' THEN TerritoryName
        ELSE CountryRegionName
    END;
```

### Use ORDER BY in a ranking function

The following example uses the `ORDER BY` clause in the ranking functions `ROW_NUMBER`, `RANK`, `DENSE_RANK`, and `NTILE`.

```sql
USE AdventureWorks2022;
GO

SELECT p.FirstName,
    p.LastName,
    ROW_NUMBER() OVER (ORDER BY a.PostalCode) AS "Row Number",
    RANK() OVER (ORDER BY a.PostalCode) AS "Rank",
    DENSE_RANK() OVER (ORDER BY a.PostalCode) AS "Dense Rank",
    NTILE(4) OVER (ORDER BY a.PostalCode) AS "Quartile",
    s.SalesYTD,
    a.PostalCode
FROM Sales.SalesPerson AS s
INNER JOIN Person.Person AS p
    ON s.BusinessEntityID = p.BusinessEntityID
INNER JOIN Person.Address AS a
    ON a.AddressID = p.BusinessEntityID
WHERE TerritoryID IS NOT NULL
    AND SalesYTD <> 0;
```

### Limit the number of rows returned

**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

The following examples use `OFFSET` and `FETCH` to limit the number of rows returned by a query.

#### A. Specify integer constants for OFFSET and FETCH values

The following example specifies an integer constant as the value for the `OFFSET` and `FETCH` clauses. The first query returns all rows sorted by the column `DepartmentID`. Compare the results returned by this query with the results of the two queries that follow it. The next query uses the clause `OFFSET 5 ROWS` to skip the first five rows and return all remaining rows. The final query uses the clause `OFFSET 0 ROWS` to start with the first row and then uses `FETCH NEXT 10 ROWS ONLY` to limit the rows returned to 10 rows from the sorted result set.

```sql
USE AdventureWorks2022;
GO

-- Return all rows sorted by the column DepartmentID.
SELECT DepartmentID, Name, GroupName
FROM HumanResources.Department
ORDER BY DepartmentID;

-- Skip the first 5 rows from the sorted result set and return all remaining rows.
SELECT DepartmentID, Name, GroupName
FROM HumanResources.Department
ORDER BY DepartmentID OFFSET 5 ROWS;

-- Skip 0 rows and return only the first 10 rows from the sorted result set.
SELECT DepartmentID, Name, GroupName
FROM HumanResources.Department
ORDER BY DepartmentID OFFSET 0 ROWS
FETCH NEXT 10 ROWS ONLY;
```

#### B. Specify variables for OFFSET and FETCH values

The following example declares the variables `@RowsToSkip` and `@FetchRows` and specifies these variables in the `OFFSET` and `FETCH` clauses.

```sql
USE AdventureWorks2022;
GO

-- Specifying variables for OFFSET and FETCH values
DECLARE
    @RowsToSkip TINYINT = 2,
    @FetchRows TINYINT = 8;

SELECT DepartmentID, Name, GroupName
FROM HumanResources.Department
ORDER BY DepartmentID ASC OFFSET @RowsToSkip ROWS
FETCH NEXT @FetchRows ROWS ONLY;
```

#### C. Specify expressions for OFFSET and FETCH values

The following example uses the expression `@StartingRowNumber - 1` to specify the `OFFSET` value and the expression `@EndingRowNumber - @StartingRowNumber + 1` to specify the FETCH value. In addition, the query hint, `OPTIMIZE FOR`, is specified. This hint can be used to provide a particular value for a local variable when the query is compiled and optimized. The value is used only during query optimization, and not during query execution. For more information, see [Query hints](hints-transact-sql-query.md).

```sql
USE AdventureWorks2022;
GO

-- Specifying expressions for OFFSET and FETCH values
DECLARE
    @StartingRowNumber TINYINT = 1,
    @EndingRowNumber TINYINT = 8;

SELECT DepartmentID, Name, GroupName
FROM HumanResources.Department
ORDER BY DepartmentID ASC OFFSET @StartingRowNumber - 1 ROWS
FETCH NEXT @EndingRowNumber - @StartingRowNumber + 1 ROWS ONLY
OPTION (OPTIMIZE FOR (@StartingRowNumber = 1, @EndingRowNumber = 20));
```

#### D. Specify a constant scalar subquery for OFFSET and FETCH values

The following example uses a constant scalar subquery to define the value for the `FETCH` clause. The subquery returns a single value from the column `PageSize` in the table `dbo.AppSettings`.

```sql
-- Specifying a constant scalar subquery
USE AdventureWorks2022;
GO

CREATE TABLE dbo.AppSettings (
    AppSettingID INT NOT NULL,
    PageSize INT NOT NULL
);
GO

INSERT INTO dbo.AppSettings
VALUES (1, 10);
GO

DECLARE @StartingRowNumber TINYINT = 1;

SELECT DepartmentID, Name, GroupName
FROM HumanResources.Department
ORDER BY DepartmentID ASC OFFSET @StartingRowNumber ROWS
FETCH NEXT (
    SELECT PageSize
    FROM dbo.AppSettings
    WHERE AppSettingID = 1
) ROWS ONLY;
```

#### E. Run multiple queries in a single transaction

The following example shows one method of implementing a paging solution that ensures stable results are returned in all requests from the query. The query is executed in a single transaction using the snapshot isolation level, and the column specified in the `ORDER BY` clause ensures column uniqueness.

```sql
USE AdventureWorks2022;
GO

-- Ensure the database can support the snapshot isolation level set for the query.
IF (
    SELECT snapshot_isolation_state
    FROM sys.databases
    WHERE name = N'AdventureWorks2022'
) = 0
ALTER DATABASE AdventureWorks2022
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO

-- Set the transaction isolation level  to SNAPSHOT for this query.
SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
GO

-- Beginning the transaction.
BEGIN TRANSACTION;
GO

-- Declare and set the variables for the OFFSET and FETCH values.
DECLARE
    @StartingRowNumber INT = 1,
    @RowCountPerPage INT = 3;

-- Create the condition to stop the transaction after all rows have been returned.
WHILE (
    SELECT COUNT(*)
    FROM HumanResources.Department
) >= @StartingRowNumber
BEGIN
    -- Run the query until the stop condition is met.
    SELECT DepartmentID, Name, GroupName
    FROM HumanResources.Department
    ORDER BY DepartmentID ASC OFFSET @StartingRowNumber - 1 ROWS
    FETCH NEXT @RowCountPerPage ROWS ONLY;

    -- Increment @StartingRowNumber value.
    SET @StartingRowNumber = @StartingRowNumber + @RowCountPerPage;

    CONTINUE
END;
GO

COMMIT TRANSACTION;
GO
```

### Use ORDER BY with UNION, EXCEPT, and INTERSECT

When a query uses the `UNION`, `EXCEPT`, or `INTERSECT` operators, the `ORDER BY` clause must be specified at the end of the statement and the results of the combined queries are sorted. The following example returns all products that are red or yellow and sorts this combined list by the column `ListPrice`.

```sql
USE AdventureWorks2022;
GO

SELECT Name, Color, ListPrice
FROM Production.Product
WHERE Color = 'Red'
-- ORDER BY cannot be specified here.

UNION ALL

SELECT Name, Color, ListPrice
FROM Production.Product
WHERE Color = 'Yellow'
ORDER BY ListPrice ASC;
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example demonstrates ordering of a result set by the numerical `EmployeeKey` column in ascending order.

```sql
-- Uses AdventureWorks
SELECT EmployeeKey, FirstName, LastName
FROM DimEmployee
WHERE LastName LIKE 'A%'
ORDER BY EmployeeKey;
```

The following example orders a result set by the numerical `EmployeeKey` column in descending order.

```sql
-- Uses AdventureWorks
SELECT EmployeeKey, FirstName, LastName
FROM DimEmployee
WHERE LastName LIKE 'A%'
ORDER BY EmployeeKey DESC;
```

The following example orders a result set by the `LastName` column.

```sql
-- Uses AdventureWorks
SELECT EmployeeKey, FirstName, LastName
FROM DimEmployee
WHERE LastName LIKE 'A%'
ORDER BY LastName;
```

The following example orders by two columns. This query first sorts in ascending order by the `FirstName` column, and then sorts common `FirstName` values in descending order by the `LastName` column.

```sql
-- Uses AdventureWorks
SELECT EmployeeKey, FirstName, LastName
FROM DimEmployee
WHERE LastName LIKE 'A%'
ORDER BY LastName, FirstName;
```

## Related content

- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](from-transact-sql.md)
- [Ranking Functions (Transact-SQL)](../functions/ranking-functions-transact-sql.md)
- [TOP (Transact-SQL)](top-transact-sql.md)
- [Query hints (Transact-SQL)](hints-transact-sql-query.md)
- [Set Operators - EXCEPT and INTERSECT (Transact-SQL)](../language-elements/set-operators-except-and-intersect-transact-sql.md)
- [Set Operators - UNION (Transact-SQL)](../language-elements/set-operators-union-transact-sql.md)
- [CASE (Transact-SQL)](../language-elements/case-transact-sql.md)
