---
title: "T-SQL performance issues"
description: "Performance issue rules included with SQL code analysis."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
---

# T-SQL performance issues

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When you analyze the T-SQL code in your database project, one or more warnings might be categorized as performance issues. You should address a performance issue to avoid the following situation:

- A table scan occurs when the code is executed.

In general, you might suppress a performance issue if the table contains so little data that a scan won't cause performance to drop significantly.

The provided rules identify the following performance issues:

- [SR0004: Avoid using columns that don't have indexes as test expressions in IN predicates](#sr0004-avoid-using-columns-that-dont-have-indexes-as-test-expressions-in-in-predicates)
- [SR0005: Avoid using patterns that start with "%" in LIKE predicates](#sr0005-avoid-using-patterns-that-start-with--in-like-predicates)
- [SR0006: Move a column reference to one side of a comparison operator to use a column index](#sr0006-move-a-column-reference-to-one-side-of-a-comparison-operator-to-use-a-column-index)
- [SR0007: Use ISNULL(column, default_value) on nullable columns in expressions](#sr0007-use-isnullcolumn-default_value-on-nullable-columns-in-expressions)
- [SR0015: Extract deterministic function calls from WHERE predicates](#sr0015-extract-deterministic-function-calls-from-where-predicates)

## SR0004: Avoid using columns that don't have indexes as test expressions in IN predicates

You cause a table scan if you use a WHERE clause that references one or more columns that aren't indexed as part of an IN predicate. The table scan reduces performance.

### How to fix violations

To resolve this issue, you must make one of the following changes:

- Change the IN predicate to reference only those columns that have an index.
- Add an index to any column that the IN predicate references and that doesn't already have an index.

### Example

In this example, a simple SELECT statement references a column, [c1], that didn't have an index. The second statement defines an index that you can add to resolve this warning.

```sql
CREATE PROCEDURE [dbo].[Procedure3WithWarnings]
AS
SELECT [Comment]
FROM [dbo].[Table2]
WHERE [c1] IN (1, 2, 3)

CREATE INDEX [IX_Table2_C1]
ON [dbo].[Table2] (c1);
```

## SR0005: Avoid using patterns that start with "%" in LIKE predicates

You could cause a table scan if you use a WHERE clause that contains a LIKE predicate such as '%pattern string' to search for text that can occur anywhere in a column.

### How to fix violations

To resolve this issue, you should change the search string so that it starts with a character that isn't a wildcard (%), or you should create a full-text index.

### Example

In the first example, the SELECT statement causes a table scan because the search string starts with a wildcard character. In the second example, the statement causes an index seek because the search string doesn't start with a wildcard character. An index seek retrieves only the rows that match the WHERE clause.

```sql
SELECT [dbo].[Table2].[ID], [dbo].[Table2].[c1], [dbo].[Table2].[c2], [dbo].[Table2].[c3], [dbo].[Table2].[Comment]
FROM dbo.[Table2]
WHERE Comment LIKE '%pples'

SELECT [dbo].[Table2].[ID], [dbo].[Table2].[c1], [dbo].[Table2].[c2], [dbo].[Table2].[c3], [dbo].[Table2].[Comment]
FROM dbo.[Table2]
WHERE Comment LIKE 'A%'
```

## SR0006: Move a column reference to one side of a comparison operator to use a column index

Your code could cause a table scan if it compares an expression that contains a column reference.

### How to fix violations

To resolve this issue, you must rework the comparison so that the column reference appears alone on one side of the comparison operator, instead of inside an expression. When you run the code that has the column reference alone on one side of the comparison operator, SQL Server can use the column index, and no table scan is performed.

### Example

In the first procedure, a WHERE clause includes column [c1] in an expression as part of a comparison. In the second procedure, the comparison results are identical but never require a table scan.

```sql
CREATE PROCEDURE [dbo].[Procedure3WithWarnings]
@param1 int
AS
SELECT [c1], [c2], [c3], [Comment]
FROM [dbo].[Table2]
WHERE ([c1] + 5 > @param1)

CREATE PROCEDURE [dbo].[Procedure3Fixed]
@param1 int
AS
SELECT [c1], [c2], [c3], [Comment]
FROM [dbo].[Table2]
WHERE ([c1] > (@param1 - 5))
```

## SR0007: Use ISNULL(column, default_value) on nullable columns in expressions

If your code compares two `NULL` values or a `NULL` value with any other value, your code returns an unknown result.

### How to fix violations

You should explicitly indicate how to handle `NULL` values in comparison expressions by wrapping each column that can contain a `NULL` value in an `ISNULL` function.

### Example

This example shows a simple table definition and two stored procedures. The table contains a column, `c2`, which can contain a `NULL` value. The first procedure, `ProcedureWithWarning`, compares `c2` to a constant value. The second procedure fixes the issue by wrapping `c2` with a call to the `ISNULL` function.

```sql
CREATE TABLE [dbo].[Table1]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[c1] INT NOT NULL PRIMARY KEY,
[c2] INT
)
ON [PRIMARY]

CREATE PROCEDURE [dbo].[ProcedureWithWarning]
AS
BEGIN
SELECT COUNT(*) FROM [dbo].[Table1]
 WHERE [c2] > 2;
END

CREATE PROCEDURE [dbo].[ProcedureFixed]
AS
BEGIN
SELECT COUNT(*) FROM [dbo].[Table1]
WHERE ISNULL([c2],0) > 2;
END
```

## SR0015: Extract deterministic function calls from WHERE predicates

In a WHERE predicate, a function call is deterministic if its value doesn't depend on the selected data. Such calls could cause unnecessary table scans, which decrease database performance.

### How to fix violations

To resolve this issue, you can assign the result of the call to a variable that you use in the WHERE predicate.

### Example

In the first example, the stored procedure includes a deterministic function call, `ABS(@param1)`, in the WHERE predicate. In the second example, a temporary variable holds the result of the call.

```sql
CREATE PROCEDURE [dbo].[Procedure2WithWarning]
@param1 INT = 0,
AS
BEGIN
SELECT [c1], [c2], [c3], [SmallString]
FROM [dbo].[Table1]
WHERE [c2] > ABS(@param1)
END

CREATE PROCEDURE [dbo].[Procedure2Fixed]
@param1 INT = 0,
AS
BEGIN
DECLARE @AbsOfParam1 INT
SET @AbsOfParam1 = ABS(@param1)

SELECT [c1], [c2], [c3], [SmallString]
FROM [dbo].[Table1]
WHERE [c2] > @AbsOfParam1
END
```

## Related content

- [SQL code analysis to improve code quality](sql-code-analysis.md)
- [T-SQL naming issues](t-sql-naming-issues.md)
- [T-SQL design issues](t-sql-design-issues.md)
- [Analyze T-SQL code to find defects](../../howto/analyze-t-sql-code-to-find-defects.md)
