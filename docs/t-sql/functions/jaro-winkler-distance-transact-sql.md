---
title: "JARO_WINKLER_DISTANCE (Transact-SQL)"
description: JARO_WINKLER_DISTANCE calculates the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length.
author: uc-msft
ms.author: umajay
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 10/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb || >=sql-server-2017 || =fabric"
---

# JARO_WINKLER_DISTANCE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

Calculates the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length.

> [!NOTE]  
> - `JARO_WINKLER_DISTANCE` is generally available starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU11. On earlier SQL Server 2025 builds, the function is in preview. If it isn't enabled by default on your build, enable [PREVIEW_FEATURES](../statements/alter-database-scoped-configuration-transact-sql.md#preview-features) by setting it to `ON`.
> - `JARO_WINKLER_DISTANCE` is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

```syntaxsql
JARO_WINKLER_DISTANCE (
    character_expression
    , character_expression
)
```

## Arguments

#### *character_expression*

An alphanumeric expression of character data. *character_expression* can be a constant, variable, or column. The character expression can't be of type **varchar(max)** or **nvarchar(max)**.

## Return value

**float**

## Remarks

This function implements the Jaro-Winkler edit distance algorithm.

## Examples

### A. Calculate Jaro-Winkler distance between two words

The following example compares two words and returns the `JARO_WINKLER_DISTANCE` value as a column, named `Distance`.

```sql
SELECT 'Colour' AS WordUK,
       'Color' AS WordUS,
       JARO_WINKLER_DISTANCE('Colour', 'Color') AS Distance;
```

Returns:

```output
WordUK WordUS Distance
------ ------ ------------------
Colour Color  0.0333333333333333
```

### B. Compare Jaro-Winkler distance for strings with different prefixes

The following example compares pairs of words that have the same edit distance but different Jaro-Winkler distance values. Jaro-Winkler gives more preference to strings that match from the beginning.

```sql
SELECT Source,
       Target,
       EDIT_DISTANCE(Source, Target) AS EditDistance,
       JARO_WINKLER_DISTANCE(Source, Target) AS JaroWinklerDistance
FROM (VALUES ('DWAYNE', 'DUANE'),
             ('CRATE', 'TRACE')
     ) AS compare(Source, Target);
```

Returns:

```output
Source Target EditDistance JaroWinklerDistance
------ ------ ------------ -------------------
DWAYNE DUANE  2            0.16
CRATE  TRACE  2            0.266666666666667
```

For more examples, see the [Examples](../../relational-databases/fuzzy-string-match/overview.md#examples) section in the [Fuzzy string matching overview](../../relational-databases/fuzzy-string-match/overview.md).

## Related content

- [EDIT_DISTANCE (Transact-SQL)](edit-distance-transact-sql.md)
- [EDIT_DISTANCE_SIMILARITY (Transact-SQL)](edit-distance-similarity-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL)](jaro-winkler-similarity-transact-sql.md)
