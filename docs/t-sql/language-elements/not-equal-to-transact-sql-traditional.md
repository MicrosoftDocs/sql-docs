---
title: "&lt;&gt; (Not Equal To) (Transact-SQL)"
description: Compares two expressions and returns TRUE if the left operand isn't equal to the right operand.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "<>"
  - "<>_TSQL"
helpviewer_keywords:
  - "not equal to operator (<>)"
  - "<> (not equal to operator)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# Not equal to (Transact-SQL) - traditional

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Compares two expressions (a comparison operator). When you compare non-null expressions, the result is `TRUE` if the left operand isn't equal to the right operand. Otherwise, the result is `FALSE`. If either or both operands are `NULL`, see [SET ANSI_NULLS](../statements/set-ansi-nulls-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression <> expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md). Both expressions must have implicitly convertible data types. The conversion depends on the rules of [data type precedence](../data-types/data-type-precedence-transact-sql.md).

## Return types

**Boolean**

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use <> in a simple query

The following example returns all rows in the `Production.ProductCategory` table that don't have a value in `ProductCategoryID` that equals 3 or 2.

```sql
SELECT ProductCategoryID,
       Name
FROM Production.ProductCategory
WHERE ProductCategoryID <> 3
      AND ProductCategoryID <> 2;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ProductCategoryID  Name
------------------ ----------------------
1                  Bikes
4                  Accessories
```

## Related content

- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [Comparison Operators (Transact-SQL)](comparison-operators-transact-sql.md)
