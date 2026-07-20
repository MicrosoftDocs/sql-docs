---
title: "COALESCE (Transact-SQL)"
description: "Transact-SQL reference for COALESCE, which returns the value of the first expression that doesn't evaluate to NULL."
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/28/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "COALESCE"
  - "COALESCE_TSQL"
helpviewer_keywords:
  - "expressions [SQL Server], nonnull"
  - "COALESCE function"
  - "first nonnull expressions [SQL Server]"
  - "nonnull expressions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# COALESCE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Evaluates the arguments in order and returns the current value of the first expression that initially doesn't evaluate to `NULL`. The following example returns the third value because the third value is the first value that isn't null.

```sql
SELECT COALESCE(NULL, NULL, 'third_value', 'fourth_value');
```

> [!NOTE]  
> If you want to concatenate strings, use [STRING_AGG](../functions/string-agg-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
COALESCE ( expression [ , ...n ] )
```

## Arguments

#### *expression*

An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type.

## Return types

Returns the data type of *expression* with the highest data type precedence. If all expressions are non-nullable, the result is typed as non-nullable.

## Remarks

If all arguments are `NULL`, `COALESCE` returns `NULL`. At least one of the null values must be a typed `NULL`.

## Compare COALESCE and CASE

The `COALESCE` expression is a syntactic shortcut for the `CASE` expression. That is, the code `COALESCE(<expression1>, ...n)` is rewritten by the query optimizer as the following `CASE` expression:

```sql
CASE
WHEN (expression1 IS NOT NULL) THEN expression1
WHEN (expression2 IS NOT NULL) THEN expression2
...
ELSE expressionN
END
```

As such, the input values (*expression1*, *expression2*, *expressionN*, and so on) are evaluated multiple times. A value expression that contains a subquery is considered nondeterministic and the subquery is evaluated twice. This result is in compliance with the SQL standard. In either case, different results can be returned between the first evaluation and upcoming evaluations.

For example, when the code `COALESCE((subquery), 1)` is executed, the subquery is evaluated twice. As a result, you can get different results depending on the isolation level of the query. For example, the code can return `NULL` under the `READ COMMITTED` isolation level in a multi-user environment. To ensure stable results are returned, use the `SNAPSHOT ISOLATION` isolation level, or replace `COALESCE` with the `ISNULL` function. As an alternative, you can rewrite the query to push the subquery into a subselect as shown in the following example:

```sql
SELECT CASE WHEN x IS NOT NULL THEN x ELSE 1 END
FROM (SELECT (SELECT Nullable
              FROM Demo
              WHERE SomeCol = 1) AS x) AS T;
```

## Compare COALESCE and ISNULL

The `ISNULL` function and the `COALESCE` expression have a similar purpose but can behave differently.

1. Because `ISNULL` is a function, it's evaluated only once. As described previously, the input values for the `COALESCE` expression can be evaluated multiple times.

1. Data type determination of the resulting expression is different. `ISNULL` uses the data type of the first parameter, and `COALESCE` follows the `CASE` expression rules to return the data type of value with the highest precedence.

1. The NULLability of the result expression is different for `ISNULL` and `COALESCE`. The `ISNULL` return value is always considered not nullable (assuming the return value is a non-nullable one). By contrast,`COALESCE` with non-null parameters is considered to be `NULL`. So the expressions `ISNULL(NULL, 1)` and `COALESCE(NULL, 1)`, although equal, have different nullability values. These values make a difference if you're using these expressions in computed columns, creating key constraints, or making the return value of a scalar user-defined function (UDF) deterministic, so that it can be indexed as shown in the following example:

    ```sql
    USE tempdb;
    GO

    -- This statement fails because the PRIMARY KEY cannot accept NULL values
    -- and the nullability of the COALESCE expression for col2
    -- evaluates to NULL.
    CREATE TABLE #Demo
    (
        col1 INT NULL,
        col2 AS COALESCE (col1, 0) PRIMARY KEY,
        col3 AS ISNULL(col1, 0)
    );
    -- This statement succeeds because the nullability of the
    -- ISNULL function evaluates AS NOT NULL.

    CREATE TABLE #Demo
    (
        col1 INT NULL,
        col2 AS COALESCE (col1, 0),
        col3 AS ISNULL(col1, 0) PRIMARY KEY
    );
    ```

1. Validations for `ISNULL` and `COALESCE` are also different. For example, a `NULL` value for `ISNULL` is converted to **int** though for `COALESCE`, you must provide a data type.

1. `ISNULL` takes only two parameters. By contrast `COALESCE` takes a variable number of parameters.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return data from the first column that has a non-null value

The following example demonstrates how `COALESCE` selects the data from the first column that has a non-null value. Assume for this example that the `Products` table contains this data:

```output
Name         Color      ProductNumber
------------ ---------- -------------
Socks, Mens  NULL       PN1278
Socks, Mens  Blue       PN1965
NULL         White      PN9876
```

We then run the following `COALESCE` query:

```sql
SELECT Name,
       Color,
       ProductNumber,
       COALESCE (Color, ProductNumber) AS FirstNotNull
FROM Products;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Name         Color      ProductNumber  FirstNotNull
------------ ---------- -------------  ------------
Socks, Mens  NULL       PN1278         PN1278
Socks, Mens  Blue       PN1965         Blue
NULL         White      PN9876         White
```

In the first row, the `FirstNotNull` value is `PN1278`, not `Socks, Mens`. This value is this way because the `Name` column wasn't specified as a parameter for `COALESCE` in the example.

### B. Return the non-null value in a wages table

In the following example, the `wages` table includes three columns that contain information about the yearly wages of the employees: the hourly wage, salary, and commission. However, an employee receives only one type of pay. To determine the total amount paid to all employees, use `COALESCE` to receive only the non-null value found in `hourly_wage`, `salary`, and `commission`.

```sql
SET NOCOUNT ON;
GO

USE tempdb;

IF OBJECT_ID('dbo.wages') IS NOT NULL
    DROP TABLE wages;
GO

CREATE TABLE dbo.wages
(
    emp_id TINYINT IDENTITY,
    hourly_wage DECIMAL NULL,
    salary DECIMAL NULL,
    commission DECIMAL NULL,
    num_sales TINYINT NULL
);
GO

INSERT dbo.wages (hourly_wage, salary, commission, num_sales)
VALUES (10.00, NULL, NULL, NULL),
    (20.00, NULL, NULL, NULL),
    (30.00, NULL, NULL, NULL),
    (40.00, NULL, NULL, NULL),
    (NULL, 10000.00, NULL, NULL),
    (NULL, 20000.00, NULL, NULL),
    (NULL, 30000.00, NULL, NULL),
    (NULL, 40000.00, NULL, NULL),
    (NULL, NULL, 15000, 3),
    (NULL, NULL, 25000, 2),
    (NULL, NULL, 20000, 6),
    (NULL, NULL, 14000, 4);
GO

SET NOCOUNT OFF;
GO

SELECT CAST (COALESCE (hourly_wage * 40 * 52, salary, commission * num_sales) AS MONEY) AS 'Total Salary'
FROM dbo.wages
ORDER BY 'Total Salary';
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Total Salary
------------
10000.00
20000.00
20800.00
30000.00
40000.00
41600.00
45000.00
50000.00
56000.00
62400.00
83200.00
120000.00
```

## Related content

- [ISNULL (Transact-SQL)](../functions/isnull-transact-sql.md)
- [CASE (Transact-SQL)](case-transact-sql.md)
- [STRING_AGG (Transact-SQL)](../functions/string-agg-transact-sql.md)
