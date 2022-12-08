---
title: SELECT @local_variable (Transact-SQL)
description: "SELECT @local_variable sets a local variable to the value of an expression"
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
monikerRange: "= azuresqldb-current || >= sql-server-2016 || = azure-sqldw-latest || >= sql-server-linux-2017"
---
# SELECT @local_variable (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Sets a local variable to the value of an expression.

For assigning variables, we recommend that you use [SET @local_variable](../../t-sql/language-elements/set-local-variable-transact-sql.md) instead of SELECT @*local_variable*.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SELECT { @local_variable { = | += | -= | *= | /= | %= | &= | ^= | |= } expression }
    [ ,...n ] [ ; ]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### @*local_variable*

A declared variable for which a value is to be assigned.

{ `=` | `+=` | `-=` | `*=` | `/=` | `%=` | `&=` | `^=` | `|=` }  
Assign the value on the right to the variable on the left.

Compound assignment operator:

| Operator | Action |
| -------- | ------ |
| = | Assigns the expression that follows, to the variable. |
| += | Add and assign |
| -= | Subtract and assign |
| \*= | Multiply and assign |
| /= | Divide and assign |
| %= | Modulo and assign |
| &= | Bitwise AND and assign |
| ^= | Bitwise XOR and assign |
| \|= | Bitwise OR and assign |

#### *expression*

Any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md). This includes a scalar subquery.

## Remarks

SELECT @*local_variable* is typically used to return a single value into the variable. However, when *expression* is the name of a column, it can return multiple values. If the SELECT statement returns more than one value, the variable is assigned the last value that is returned.

If the SELECT statement returns no rows, the variable retains its present value. If *expression* is a scalar subquery that returns no value, the variable is set to NULL.

One SELECT statement can initialize multiple local variables.

> [!NOTE]  
> A SELECT statement that contains a variable assignment cannot be used to also perform typical result set retrieval operations.

## Examples

### A. Use SELECT @local_variable to return a single value

In the following example, the variable `@var1` is assigned "Generic Name" as its value. The query against the `Store` table returns no rows because the value specified for `CustomerID` doesn't exist in the table. The variable retains the "Generic Name" value.

This example uses the AdventureWorks2019LT sample database, for more information, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md). The AdventureWorksLT database is used as the `sample` database for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

```sql
-- Uses AdventureWorks2019LT
DECLARE @var1 VARCHAR(30);
SELECT @var1 = 'Generic Name';

SELECT @var1 = [Name]
FROM SalesLT.Product
WHERE ProductID = 1000000; --Value does not exist
SELECT @var1 AS 'ProductName';
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
ProductName
------------------------------
Generic Name
```

### B. Use SELECT @local_variable to return null

In the following example, a subquery is used to assign a value to `@var1`. Because the value requested for `CustomerID` doesn't exist, the subquery returns no value, and the variable is set to `NULL`.

This example uses the AdventureWorks2019LT sample database, for more information, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md). The AdventureWorksLT database is used as the `sample` database for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

```sql
-- Uses AdventureWorks2019
DECLARE @var1 VARCHAR(30);
SELECT @var1 = 'Generic Name';

SELECT @var1 = (SELECT [Name]
FROM SalesLT.Product
WHERE ProductID = 1000000); --Value does not exist

SELECT @var1 AS 'Company Name';
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

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

In this case, it isn't guaranteed that `@Var` would be updated on a row by row basis. For example, `@Var` may be set to initial value of `@Var` for all rows. This is because the order and frequency in which the assignments are processed is nondeterminant. This applies to expressions containing variables string concatenation, as demonstrated below, but also to expressions with non-string variables or += style operators. Use aggregation functions instead for a set-based operation instead of a row-by-row operation.

For string concatenation, instead consider the `STRING_AGG` function, introduced in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], for scenarios where ordered string concatenation is desired. For more information, see [STRING_AGG (Transact-SQL)](../functions/string-agg-transact-sql.md). This example uses the AdventureWorks2016 or AdventureWorks2019 sample database. For more information, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

An example to avoid, where using ORDER BY in attempt to order concatenation causes list to be incomplete:

```sql
DECLARE @List AS nvarchar(max);
SELECT @List = CONCAT(COALESCE(@List + ', ',''), p.LastName)
  FROM Person.Person AS p
  WHERE p.FirstName = 'William'
  ORDER BY p.BusinessEntityID;
SELECT @List;
```

Result set:

```output
(No column name)
---
Walker
```

Instead, consider:

```sql
DECLARE @List AS nvarchar(max);
SELECT @List = STRING_AGG(p.LastName,', ') WITHIN GROUP (ORDER BY p.BusinessEntityID)
  FROM Person.Person AS p
  WHERE p.FirstName = 'William';
SELECT @List;
```

Result set:

```output
(No column name)
---
Vong, Conner, Hapke, Monroe, Richter, Sotelo, Vong, Ngoh, White, Harris, Martin, Thompson, Martinez, Robinson, Clark, Rodriguez, Smith, Johnson, Williams, Jones, Brown, Davis, Miller, Moore, Taylor, Anderson, Thomas, Lewis, Lee, Walker
```

## See also

- [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)
- [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)

## Next steps

- [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md)
