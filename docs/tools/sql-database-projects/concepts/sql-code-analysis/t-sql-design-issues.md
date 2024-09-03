---
title: "T-SQL design issues"
description: "Design issue rules included with SQL code analysis."
author: dzsquared
ms.author: drskwier
ms.date: 08/30/2024
ms.reviewer: maghan, randolphwest
ms.service: sql
ms.topic: concept-article
---

# T-SQL design issues

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When you analyze the T-SQL code in your database project, one or more warnings might be categorized as design issues. You should address design issues to avoid the following situations:

- Subsequent changes to your database might break applications that depend on it.
- The code might not produce the expected result.
- The code might break if you run it with future releases of SQL Server.

In general, you shouldn't suppress a design issue because it might break your application, either now or in the future.

The provided rules identify the following design issues:

- [SR0001: Avoid SELECT * in stored procedures, views, and table-valued functions](#sr0001-avoid-select--in-stored-procedures-views-and-table-valued-functions)
- [SR0008: Consider using SCOPE_IDENTITY instead of @@IDENTITY](#sr0008-consider-using-scope_identity-instead-of-identity)
- [SR0009: Avoid using types of variable length that are size 1 or 2](#sr0009-avoid-using-types-of-variable-length-that-are-size-1-or-2)
- [SR0010: Avoid using deprecated syntax when you join tables or views](#sr0010-avoid-using-deprecated-syntax-when-you-join-tables-or-views)
- [SR0013: Output parameter (parameter) is not populated in all code paths](#sr0013-output-parameter-parameter-isnt-populated-in-all-code-paths)
- [SR0014: Data loss might occur when casting from {Type1} to {Type2}](#sr0014-data-loss-might-occur-when-casting-from-type1-to-type2)

## SR0001: Avoid SELECT * in stored procedures, views, and table-valued functions

If you use a wildcard character in a stored procedure, view, or table-valued function to select all columns in a table or view, the number or shape of returned columns might change if the underlying table or view changes. The shape of a column is a combination of its type and size. This variance could cause problems in applications that consume the stored procedure, view, or table-valued function because those consumers will expect a different number of columns.

### How to fix violations

You can protect consumers of the stored procedure, view, or table-valued function from schema changes by replacing the wildcard character with a fully qualified list of column names.

### Example

The following example first defines a table that is named [Table2] and then defines two stored procedures. The first procedure contains a `SELECT *`, which violates rule SR0001. The second procedure avoids `SELECT *` and explicitly lists the columns in the SELECT statement.

```sql
CREATE TABLE [dbo].[Table2]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[c1] INT NOT NULL,
[Comment] NVARCHAR (50)
)
ON [PRIMARY]

CREATE PROCEDURE [dbo].[procWithWarning]
AS
BEGIN
-- Contains code that breaks rule SR0001
SELECT *
FROM [dbo].[Table2]
END

CREATE PROCEDURE [dbo].[procFixed]
AS
BEGIN
-- Explicitly lists the column names in a SELECT statement
SELECT [dbo].[Table2].[ID], [dbo].[Table2].[c1], [dbo].[Table2].[Comment]
FROM [dbo].[Table2]
END
```

## SR0008: Consider using SCOPE_IDENTITY instead of @@IDENTITY

Because @@IDENTITY is a global identity value, it might have been updated outside the current scope and obtained an unexpected value. Triggers, including nested triggers used by replication, can update @@IDENTITY outside your current scope.

### How to fix violations

To resolve this issue you must replace references to @@IDENTITY with SCOPE_IDENTITY, which returns the most recent identity value in the scope of the user statement.

### Example

In the first example, @@IDENTITY is used in a stored procedure that inserts data into a table. The table is then published for merge replication, which adds triggers to tables that are published. Therefore, @@IDENTITY can return the value from the insert operation into a replication system table instead of the insert operation into a user table.

The `Sales.Customer` table has a maximum identity value of 29483. If you insert a row into the table, @@IDENTITY and SCOPE_IDENTITY() return different values. SCOPE_IDENTITY() returns the value from the insert operation into the user table, but @@IDENTITY returns the value from the insert operation into the replication system table.

The second example shows how you can use SCOPE_IDENTITY() to access the inserted identity value and resolve the warning.

```sql
CREATE PROCEDURE [dbo].[ProcedureWithWarning]
@param1 INT,
@param2 NCHAR(1),
@Param3 INT OUTPUT
AS
BEGIN
INSERT INTO Sales.Customer ([TerritoryID],[CustomerType]) VALUES (@param1,@param2);

SELECT @Param3 = @@IDENTITY
END

CREATE PROCEDURE [dbo].[ProcedureFixed]
@param1 INT,
@param2 NCHAR(1),
@param3 INT OUTPUT
AS
BEGIN
INSERT INTO Sales.Customer ([TerritoryID],[CustomerType]) VALUES (@param1,@param2);

SELECT @Param3 = SCOPE_IDENTITY()
END
```

## SR0009: Avoid using types of variable length that are size 1 or 2

When you use data types of variable length such as VARCHAR, NVARCHAR, and VARBINARY, you incur an additional storage cost to track the length of the value stored in the data type. In addition, columns of variable length are stored after all columns of fixed length, which can have performance implications. You'll also receive a warning if you declare a type of variable length, such as VARCHAR, but you specify no length. This warning occurs because, if unspecified, the default length is 1.

### How to fix violations

If the length of the type will be very small (size 1 or 2) and consistent, declare them as a type of fixed length, such as CHAR, NCHAR, and BINARY.

### Example

This example shows definitions for two tables. The first table declares a string of variable length to have length 2. The second table declares a string of fixed length instead, which avoids the warning.

```sql
CREATE TABLE [dbo].[TableWithWarning]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[c1] INT NOT NULL PRIMARY KEY,
[c2] INT,
[c3] INT,
[SmallString] VARCHAR(2)
)
ON [PRIMARY]

CREATE TABLE [dbo].[FixedTable]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[c1] INT NOT NULL PRIMARY KEY,
[c2] INT,
[c3] INT,
[SmallString] CHAR(2)
)
ON [PRIMARY]
```

Data for types of variable length is physically stored after data for types of fixed length. Therefore, you'll cause data movement if you change a column from variable to fixed length in a table that isn't empty.

## SR0010: Avoid using deprecated syntax when you join tables or views

Joins that use the deprecated syntax fall into two categories:

- **Inner Join:** For an inner join, the values in the columns that are being joined are compared by using a comparison operator such as =, <, >=, and so forth. Inner joins return rows only if at least one row from each table matches the join condition.
- **Outer Join:** Outer joins return all rows from at least one of the tables or views specified in the FROM clause, as long as those rows meet any WHERE or HAVING search condition. If you use *= or =* to specify an outer join, you're using deprecated syntax.

### How to fix violations

To fix a violation in an inner join, use the `INNER JOIN` syntax.

To fix a violation in an outer join, use the appropriate `OUTER JOIN` syntax. You have the following options:

- LEFT OUTER JOIN or LEFT JOIN
- RIGHT OUTER JOIN or RIGHT JOIN

Examples of the deprecated syntax and the updated syntax are provided in the following examples. More information on joins can be found at [Joins](../../../../relational-databases/performance/joins.md).

### Examples

The six examples demonstrate the following options:

1. Example 1 demonstrates the deprecated syntax for an inner join.
1. Example 2 demonstrates how you can update Example 1 to use current syntax.
1. Example 3 demonstrates the deprecated syntax for a left outer join.
1. Example 4 demonstrates how you can update Example 2 to use current syntax.
1. Example 5 demonstrates the deprecated syntax for a right outer join.
1. Example 6 demonstrates how you can update Example 5 to use current syntax.

```sql
-- Example 1: Deprecated syntax for an inner join
SELECT [T2].[c3], [T1].[c3]
FROM [dbo].[Table2] T2, [dbo].[Table1] T1
WHERE [T1].[ID] = [T2].[ID]

-- Example 2: Current syntax for an inner join
SELECT [T2].[c3], [T1].[c3]
FROM [dbo].[Table2] AS T2
INNER JOIN [dbo].[Table1] as T1
ON [T2].[ID] = [T2].[ID]

-- Example 3: Deprecated syntax for a left outer join
SELECT [T2].[c3], [T1].[c3]
FROM [dbo].[Table2] T2, [dbo].[Table1] T1
WHERE [T1].[ID] *= [T2].[ID]

-- Example 4: Fixed syntax for a left outer join
SELECT [T2].[c3], [T1].[c3]
FROM [dbo].[Table2] AS T2
LEFT OUTER JOIN [dbo].[Table1] as T1
ON [T2].[ID] = [T2].[ID]

-- Example 5: Deprecated syntax for a right outer join
SELECT [T2].[c3], [T1].[c3]
FROM [dbo].[Table2] T2, [dbo].[Table1] T1
WHERE [T1].[ID] =* [T2].[ID]

-- Example 6: Fixed syntax for a right outer join
SELECT [T2].[c3], [T1].[c3]
FROM [dbo].[Table2] AS T2
RIGHT OUTER JOIN [dbo].[Table1] as T1
ON [T2].[ID] = [T2].[ID]
```

## SR0013: Output parameter (parameter) isn't populated in all code paths

This rule identifies code in which the output parameter isn't set to a value in one or more code paths through the stored procedure or function. This rule doesn't identify in which paths the output parameter should be set. If multiple output parameters have this problem, one warning appears for each parameter.

### How to fix violations

You can correct this issue in one of two ways. You can fix this issue most easily if you initialize the output parameters to a default value at the start of the procedure body. As an alternative, you can also set the output parameter to a value in the specific code paths in which the parameter isn't set. However, you might overlook an uncommon code path in a complex procedure.

> [!IMPORTANT]  
> Specifying a value within the procedure declaration, such as `CREATE PROC MyProcedure (@param1 INT = 10 OUTPUT)` will not resolve the issue. You must assign a value to the output parameter within the procedure body.

### Example

The following example shows two simple procedures. The first procedure doesn't set the value of the output parameter, `@Sum`. The second procedure initializes the `@Sum` parameter at the start of the procedure, which ensures that the value will be set in all code paths.

```sql
CREATE PROCEDURE [dbo].[procedureHasWarning]
(
@Value1 BIGINT,
@Value2 INT,
@Value3 INT,
@Sum INT OUTPUT
)
AS
BEGIN
-- No initialization of the output parameter
--
-- Additional statements here.
--
RETURN 0;
END
--
CREATE PROCEDURE [dbo].[procedureFixed]
(
@Value1 BIGINT,
@Value2 INT,
@Value3 INT,
@Sum INT OUTPUT
)
AS
BEGIN
-- Initialize the out parameter
SET @Sum = 0;
--
-- Additional statements here
--
RETURN 0;
END
```

## SR0014: Data loss might occur when casting from {Type1} to {Type2}

If data types are inconsistently assigned to columns, variables, or parameters, they're implicitly converted when the Transact-SQL code that contains those objects is run. This type of conversion not only reduces performance but also, in some cases, causes subtle loss of data. For example, a table scan might run if every column in a WHERE clause must be converted. Worse, data might be lost if a Unicode string is converted to an ASCII string that uses a different code page.

This rule does NOT:

- Check the type of a computed column because the type isn't known until run-time.
- Analyze anything inside a CASE statement. It also doesn't analyze the return value of a CASE statement.
- Analyze the input parameters or return value of a call to ISNULL

This table summarizes the checks covered by the rule SR0014:

<table>
<tr><th>Language construct</th><th>What is Checked</th><th>Example</th></tr>
<tr><td>Default value of parameters</td><td>Parameter data type</td><td>

```sql
CREATE PROCEDURE p1(@p1 INT = 1)
AS
BEGIN
END
```

</td></tr>
<tr><td>CREATE INDEX predicate</td><td>Predicate is Boolean</td><td>

```sql
CREATE INDEX index1 ON table1 (column1)
WHERE column1 > 10
```

</td></tr>
<tr><td>Arguments of LEFT or RIGHT functions</td><td>String argument type and length</td><td>

```sql
SET @v = LEFT('abc', 2)
```

</td></tr>
<tr><td>Arguments of CAST and CONVERT functions</td><td>Expression and types are valid</td><td>

```sql
SET @v = CAST('abc' AS CHAR(10))
```

</td></tr>
<tr><td>SET statement</td><td>Left side and right side have compatible types</td><td>

```sql
SET @v1 = 'xyz'
SELECT @v1 = c1 FROM t1
```

</td></tr>
<tr><td>IF statement predicate</td><td>Predicate is Boolean</td><td>

```sql
IF (@v > 10)
```

</td></tr>
<tr><td>WHILE statement predicate</td><td>Predicate is Boolean</td><td>

```sql
WHILE (@v > 10)
```

</td></tr>
<tr><td>INSERT statement</td><td>Values and columns are correct</td><td>

```sql
INSERT INTO t1(c1, c2) VALUES (99, 'xyz')
INSERT INTO t1 SELECT c1 FROM t2.
```

</td></tr>
<tr><td>SELECT WHERE predicate</td><td>Predicate is Boolean</td><td>

```sql
SELECT * FROM t1 WHERE c1 > 10
```

</td></tr>
<tr><td>SELECT TOP expression</td><td>Expression is an Integer or Float type</td><td>

```sql
SELECT TOP 4 * FROM t1
SELECT TOP 1.5 PERCENT * FROM t1
```

</td></tr>
<tr><td>UPDATE statement</td><td>Expression and column have compatible types</td><td>

```sql
UPDATE t1 SET c1 = 100
```

</td></tr>
<tr><td>UPDATE predicate</td><td>Predicate is Boolean</td><td>

```sql
UPDATE t1 SET c1 = 100
WHERE c1 > 100
```

</td></tr>
<tr><td>UPDATE TOP expression</td><td>Expression is an Integer or Float type</td><td>

```sql
UPDATE TOP 4 table1
```

</td></tr>
<tr><td>DELETE PREDICATE</td><td>Predicate is Boolean</td><td>

```sql
DELETE t1 WHERE c1 > 10
```

</td></tr>
<tr><td>DELETE TOP expression</td><td>Expression is an Integer or Float type</td><td>

```sql
DELETE TOP 2 FROM t1
```

</td></tr>
<tr><td>DECLARE variable declaration</td><td>Initial value and data type are compatible</td><td>

```sql
DECLARE @v INT = 10
```

</td></tr>
<tr><td>EXECUTE statement arguments and return type</td><td>Parameters and arguments</td><td>

```sql
CREATE PROCEDURE p1 (@p1 INT) AS
GO
EXECUTE p1 100
EXECUTE @v1 = p1 100
```

</td></tr>
<tr><td>RETURN statement</td><td>RETURN expression has a compatible data type</td><td>

```sql
CREATE FUNCTION f1() RETURNS INT
AS
BEGIN
  RETURN 100
END
```

</td></tr>
<tr><td>MERGE statement conditions</td><td>Condition is Boolean</td><td>

```sql
MERGE t1 USING t2
ON t1.c1 = t2.c2
WHEN t1.c1 > 10 THEN DELETE
```

</td></tr>
</table>

### How to fix violations

You can avoid and resolve these issues by assigning data types consistently and by explicitly converting types where they're needed. For more information about how to explicitly convert data types, see this page on the Microsoft Web site: CAST and CONVERT (Transact-SQL).

### Example

This example shows two stored procedures that insert data into a table. The first procedure, procWithWarning, will cause an implicit conversion of a data type. The second procedure, procFixed, shows how you can add an explicit conversion to maximize performance and retain all data.

```sql
CREATE TABLE [dbo].[Table2]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[c1] INT NOT NULL,
[c2] INT NOT NULL,
[c3] BIGINT NOT NULL,
[Comment] VARCHAR (25)
)
ON [PRIMARY]

CREATE PROCEDURE [dbo].[procWithWarning]
(
@Value1 INT,
@Value2 INT,
@Value3 BIGINT,
@Comment CHAR(30)
)
AS
BEGIN
INSERT INTO [Table2] ([c1], [c2], [c3], Comment)
VALUES (@Value1, @Value2, @Value3, @Comment)

END

CREATE PROCEDURE [dbo].[procFixed]
(
@Value1 INT,
@Value2 INT,
@Value3 BIGINT,
@Comment CHAR(10)
)
AS
BEGIN
INSERT INTO [Table2] ([c1], [c2], [c3], Comment)
VALUES (@Value1, @Value2, @Value3, CAST(@Comment AS VARCHAR(25)))

END
```

## Related content

- [SQL code analysis to improve code quality](sql-code-analysis.md)
- [T-SQL naming issues](t-sql-naming-issues.md)
- [T-SQL performance issues](t-sql-performance-issues.md)
- [Analyze T-SQL code to find defects](../../howto/analyze-t-sql-code-to-find-defects.md)
