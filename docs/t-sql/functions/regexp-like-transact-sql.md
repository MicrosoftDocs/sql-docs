---
title: "REGEXP_LIKE (Transact-SQL)"
description: REGEXP_LIKE Returns a Boolean value that indicates whether the text input matches the regex pattern.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 11/21/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# REGEXP_LIKE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Indicates if the regular expression pattern matches in a string.

```syntaxsql
REGEXP_LIKE
(
    string_expression,
    pattern_expression [ , flags ]
)
```

`REGEXP_LIKE` requires database compatibility level 170 and above. If the database compatibility level is lower than 170, `REGEXP_LIKE` isn't available. Other [regular expression scalar functions](regular-expressions-functions-transact-sql.md) are available at all compatibility levels.

You can check the compatibility level in the `sys.databases` view or in database properties. You can change the compatibility level of a database with the following command:

```sql
ALTER DATABASE [DatabaseName]
    SET COMPATIBILITY_LEVEL = 170;
```

> [!NOTE]  
> Regular expressions are available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *flags*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

## Return value

Boolean value. `true` or `false`.

## Remarks

### Cardinality estimation

To enhance the accuracy of [cardinality estimation](../../relational-databases/performance/cardinality-estimation-sql-server.md) for the `REGEXP_LIKE` function, use the `ASSUME_FIXED_MIN_SELECTIVITY_FOR_REGEXP` and `ASSUME_FIXED_MAX_SELECTIVITY_FOR_REGEXP` query hints to adjust the default selectivity values. For more information, see [Query hints](../queries/hints-transact-sql-query.md#use_hint).

These query hints also integrate with [Cardinality estimation (CE) feedback](../../relational-databases/performance/intelligent-query-processing-cardinality-estimation-feedback.md). The CE feedback model automatically identifies queries that use the `REGEXP_LIKE` function where there's a significant difference between estimated and actual row counts. It then applies the appropriate selectivity hint at the query level to improve plan quality without requiring manual input.

To disable the automatic feedback behavior, enable trace flag 16268.

### SARGable pattern support

`REGEXP_LIKE` is *SARGable* only when the pattern begins with the anchor `^`. In addition, the anchored pattern can include:

- A quantifier: `*`, `+`, `?`, `{n}`, `{n,}`, or `{n,m}`. For example, `^ab+` or `^ab*`.
- Range characters, such as `[0-9A-Za-z]`.

To escape a metacharacter, use the backslash (`\`).

These conditions let the query optimizer use index seek operations to improve query performance.

Regular expressions don't honor collation rules. Their behavior might be different from other string comparison functions, such as `LIKE`. This difference is most important on indexed columns that have language-specific collations.

For example, in Turkish collation, the characters `i` and `I` are treated distinctly even in the case-insensitive collation due to language-specific rules. For more information, see example [F. Compare SARGable and non-SARGable pattern matching with Turkish collation](#sargable-example).

> [!NOTE]  
> [!INCLUDE [search-argument](../../includes/paragraph-content/search-argument.md)]

## Examples

### A. Match values that start and end with specific characters

Select all records from the `Employees` table where the first name starts with `A` and ends with `Y`:

```sql
SELECT *
FROM Employees
WHERE REGEXP_LIKE (FIRST_NAME, '^A.*Y$');
```

### B. Perform a case-insensitive pattern match

Select all records from the `Employees` table where the first name starts with `A` and ends with `Y`, using case-insensitive mode:

```sql
SELECT *
FROM Employees
WHERE REGEXP_LIKE (FIRST_NAME, '^A.*Y$', 'i');
```

### C. Match dates using a regular expression pattern

Select all records from the `Orders` table where the order date is in February 2020:

```sql
SELECT *
FROM Orders
WHERE REGEXP_LIKE (ORDER_DATE, '2020-02-\d\d');
```

### D. Match repeated character patterns

Select all records from the `Products` table where the product name contains at least three consecutive vowels:

```sql
SELECT *
FROM Products
WHERE REGEXP_LIKE (PRODUCT_NAME, '[AEIOU]{3,}');
```

### E. Enforce data validation with CHECK constraints

Create an employees table with `CHECK` constraints for the `Email` and `Phone_Number` columns:

```sql
DROP TABLE IF EXISTS Employees;
CREATE TABLE Employees
(
    ID INT IDENTITY (101, 1),
    [Name] VARCHAR (150),
    Email VARCHAR (320)
        CHECK (REGEXP_LIKE (Email, '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$')),
    Phone_Number NVARCHAR (20)
        CHECK (REGEXP_LIKE (Phone_Number, '^(\d{3})-(\d{3})-(\d{4})$'))
);
```

<a id="sargable-example"></a>

### F. Compare SARGable and non-SARGable pattern matching with Turkish collation

This example demonstrates SARGable and non-SARGable use of the `REGEXP_LIKE` function with Turkish collation.

```sql
-- Create a temporary table with Turkish collation and and an index
CREATE TABLE #Users
(
    Username NVARCHAR (100) COLLATE Turkish_100_CI_AS_SC_UTF8 NOT NULL,
    INDEX idx_username (Username)
);

-- Insert sample data
INSERT INTO #Users (Username)
VALUES (N'i'), -- lowercase i
       (N'I'), -- uppercase dotless I
       (N'İ'), -- uppercase dotted İ
       (N'abc');

-- SARGable pattern: starts with ^ and uses quantifier
-- This will use index seek if applicable, but REGEXP_LIKE ignores collation
-- So 'i' and 'I' are treated as different characters
SELECT 'SARGable' AS PatternType,
       *
FROM #Users
WHERE REGEXP_LIKE (Username, '^i');

-- Non-SARGable pattern: does not start with ^.
-- REGEXP_LIKE performs full scan, and matches are
-- case-insensitive since 'i' flag is supplied,
-- so both 'i' and 'I' match.
SELECT 'Non-SARGable' AS PatternType,
       *
FROM #Users
WHERE REGEXP_LIKE (Username, 'i', 'i');

-- Cleanup
DROP TABLE #Users;
```

## Related content

- [Regular expressions](../../relational-databases/regular-expressions/overview.md)
- [Regular expressions functions (Transact-SQL)](regular-expressions-functions-transact-sql.md)
