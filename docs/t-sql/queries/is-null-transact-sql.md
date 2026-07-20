---
title: "IS NULL (Transact-SQL)"
description: Determines whether a specified expression is null.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/16/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "NULL_TSQL"
  - "IS_[NOT]_NULL_TSQL"
  - "IS_NULL_TSQL"
  - "NULL"
  - "[NOT]_TSQL"
  - "IS"
  - "IS_TSQL"
  - "IS NULL"
  - "IS [NOT] NULL"
  - "[NOT]"
helpviewer_keywords:
  - "verifying nullability"
  - "IS NOT NULL clause"
  - "null values [SQL Server], verifying"
  - "null values [SQL Server], IS [NOT] NULL"
  - "IS [NOT] NULL clause"
  - "testing nullability"
  - "checking nullability"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# IS NULL (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-FabricDW-FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Determines whether a specified expression is `NULL`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression IS [ NOT ] NULL
```

## Arguments

#### *expression*

Any valid [expression](../language-elements/expressions-transact-sql.md).

- `NOT`

  Specifies that the Boolean result is negated. The predicate reverses its return values, returning `TRUE` if the value isn't `NULL`, and `FALSE` if the value is `NULL`.

## Return types

**Boolean**

## Return code values

If the value of *expression* is `NULL`, `IS NULL` returns `TRUE`; otherwise, it returns `FALSE`.

If the value of *expression* is `NULL`, `IS NOT NULL` returns `FALSE`; otherwise, it returns `TRUE`.

## Remarks

To determine whether an expression is `NULL`, use `IS NULL` or `IS NOT NULL` instead of comparison operators (such as `=` or `!=`). Comparison operators return `UNKNOWN` when either or both arguments are `NULL`.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return the name and weight for all products

The following example returns the name and the weight for all products for which either the weight is less than 10 pounds, or the color is unknown, or `NULL`.

```sql
SELECT Name,
       Weight,
       Color
FROM Production.Product
WHERE Weight < 10.00
      OR Color IS NULL
ORDER BY Name;
GO
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### B. Return the full names of all employees with initials

The following example returns the full names of all employees with middle initials.

```sql
SELECT FirstName,
       LastName,
       MiddleName
FROM DIMEmployee
WHERE MiddleName IS NOT NULL
ORDER BY LastName DESC;
```

## Related content

- [CASE (Transact-SQL)](../language-elements/case-transact-sql.md)
- [CREATE PROCEDURE (Transact-SQL)](../statements/create-procedure-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [LIKE (Transact-SQL)](../language-elements/like-transact-sql.md)
- [Operators (Transact-SQL)](../language-elements/operators-transact-sql.md)
- [Logical Operators (Transact-SQL)](../language-elements/logical-operators-transact-sql.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
- [sp_help](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
- [WHERE (Transact-SQL)](where-transact-sql.md)
