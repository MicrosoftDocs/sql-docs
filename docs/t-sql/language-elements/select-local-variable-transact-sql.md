---
title: "SELECT @local_variable (Transact-SQL)"
description: Sets a local variable to the value of an expression.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "variable_TSQL"
  - "@local_variable"
  - "@local"
  - "variable"
  - "@local_variable_TSQL"
  - "@local_TSQL"
helpviewer_keywords:
  - "variables [SQL Server], assigning"
  - "SELECT statement [SQL Server], @local_variable"
  - "@local_variable"
  - "local variables [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || =azure-sqldw-latest || >=sql-server-linux-2017 || =fabric || =fabric-sqldb"
---

# SELECT @local_variable (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

Sets a local variable to the value of an expression.

For assigning variables, use [SET @local_variable](set-local-variable-transact-sql.md) instead of `SELECT @local_variable`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SELECT { @local_variable { = | += | -= | *= | /= | %= | &= | ^= | |= } expression }
    [ , ...n ] [ ; ]
```

## Arguments

#### *@local_variable*

A declared variable for which you assign a value.

#### { `=` | `+=` | `-=` | `*=` | `/=` | `%=` | `&=` | `^=` | `|=` }

Assign the value on the right to the variable on the left.

Compound assignment operator:

| Operator | Action |
| --- | --- |
| = | Assigns the expression that follows, to the variable. |
| += | Add and assign |
| -= | Subtract and assign |
| \*= | Multiply and assign |
| /= | Divide and assign |
| %= | Modulo and assign |
| &= | Bitwise `AND` and assign |
| ^= | Bitwise `XOR` and assign |
| &#124;= | Bitwise `OR` and assign |

#### *expression*

Any valid [expression](expressions-transact-sql.md). This term includes a scalar subquery.

## Remarks

Use `SELECT @local_variable` to return a single value into the variable. However, when *expression* is the name of a column, it can return multiple values. If the `SELECT` statement returns more than one value, the variable gets the last value that the query returns.

If the `SELECT` statement returns no rows, the variable keeps its current value. If *expression* is a scalar subquery that returns no value, the variable is set to `NULL`.

One `SELECT` statement can initialize multiple local variables.

> [!NOTE]  
> You can't use a `SELECT` statement that contains a variable assignment to also perform typical result set retrieval operations.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The `AdventureWorksLT` database is used as the sample database for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

### A. Use SELECT @local_variable to return a single value

In the following example, the variable `@var1` gets the value `'Generic Name'`. The query against the `Store` table returns no rows because the value specified for `CustomerID` doesn't exist in the table. The variable keeps the "Generic Name" value.

```sql
DECLARE @var1 AS VARCHAR (30);

SELECT @var1 = 'Generic Name';

SELECT @var1 = [Name]
FROM SalesLT.Product
WHERE ProductID = 1000000;

SELECT @var1 AS 'ProductName';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ProductName
------------------------------
Generic Name
```

### B. Use SELECT @local_variable to return null

In the following example, a subquery assigns a value to `@var1`. Because the value requested for `CustomerID` doesn't exist, the subquery returns no value, and the variable is set to `NULL`.

```sql
DECLARE @var1 AS VARCHAR (30);

SELECT @var1 = 'Generic Name';

SELECT @var1 = (SELECT [Name]
                FROM SalesLT.Product
                WHERE ProductID = 1000000);

SELECT @var1 AS 'Company Name';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Company Name
----------------------------
NULL
```

### C. Antipattern use of recursive variable assignment

Avoid the following pattern for recursive use of variables and expressions:

```syntaxsql
SELECT @Var = <expression containing @Var>
FROM
...
```

In this case, it isn't guaranteed that `@Var` is updated on a row by row basis. For example, `@Var` might be set to initial value of `@Var` for all rows. This behavior occurs because the order and frequency in which the assignments are processed is nondeterminant. This rule applies to expressions containing variables string concatenation, as demonstrated in the following example, but also to expressions with non-string variables or `+=` style operators. Use aggregation functions instead for a set-based operation instead of a row-by-row operation.

For string concatenation, consider the `STRING_AGG` function, introduced in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], for scenarios where ordered string concatenation is desired. For more information, see [STRING_AGG](../functions/string-agg-transact-sql.md).

The following example shows an antipattern to avoid. Using `ORDER BY` in an attempt to order concatenation causes the list to be incomplete:

```sql
DECLARE @List AS NVARCHAR (MAX);

SELECT @List = CONCAT(COALESCE (@List + ', ', ''), p.LastName)
FROM Person.Person AS p
WHERE p.FirstName = 'William'
ORDER BY p.BusinessEntityID;

SELECT @List;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
(No column name)
---
Walker
```

Instead, consider:

```sql
DECLARE @List AS NVARCHAR (MAX);

SELECT @List = STRING_AGG(p.LastName, ', ') WITHIN GROUP (ORDER BY p.BusinessEntityID)
FROM Person.Person AS p
WHERE p.FirstName = 'William';

SELECT @List;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
(No column name)
---
Vong, Conner, Hapke, Monroe, Richter, Sotelo, Vong, Ngoh, White, Harris, Martin, Thompson, Martinez, Robinson, Clark, Rodriguez, Smith, Johnson, Williams, Jones, Brown, Davis, Miller, Moore, Taylor, Anderson, Thomas, Lewis, Lee, Walker
```

## Related content

- [DECLARE @local_variable (Transact-SQL)](declare-local-variable-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md)
