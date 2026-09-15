---
title: EDIT_DISTANCE (Transact-SQL)
description: EDIT_DISTANCE calculates the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: abhtiwar, wiassaf
ms.date: 09/04/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb || >=sql-server-2017 || =fabric"
---

# EDIT_DISTANCE (Transact-SQL) preview

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

[!INCLUDE [preview](../../includes/preview.md)]

Calculates the *edit distance* between two strings, which is the minimum number of insertions, deletions, substitutions, and transpositions needed to transform one string into the other.

> [!NOTE]  
> - `EDIT_DISTANCE` is in preview.
> - `EDIT_DISTANCE` is available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
> - `EDIT_DISTANCE` is available in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

```syntaxsql
EDIT_DISTANCE (
    character_expression
    , character_expression [ , maximum_distance ]
)
```

## Arguments

#### *character_expression*

An alphanumeric expression of character data. *character_expression* can be a constant, variable, or column. The character expression can't be of type **varchar(max)** or **nvarchar(max)**.

#### *maximum_distance*

An optional value that specifies the maximum edit distance to calculate. *maximum_distance* is an integer. When *maximum_distance* is greater than or equal to `0`, the function might stop processing once it determines that the edit distance exceeds the specified value.

If the actual edit distance is less than or equal to *maximum_distance*, the function returns the actual distance. Otherwise, the function returns *maximum_distance* + `1`.

If *maximum_distance* isn't specified, or if it's negative, the function returns the actual edit distance. If *maximum_distance* is `NULL`, the function returns `NULL`.

## Return value

**int**

This function implements the Damerau-Levenshtein (Optimal String Alignment) algorithm to return the distance between the two *character_expressions*, or *maximum_distance* value if that is smaller.

If any of the inputs is `NULL` then the function returns a `NULL` value.

## Remarks

If the actual distance is greater than *maximum_distance*, then the function returns *maximum_distance* + `1`.

## Examples

### A. Calculate edit distance between two words

The following example compares two words and returns the `EDIT_DISTANCE()` value as a column, named `Distance`.

```sql
SELECT 'Colour' AS WordUK,
       'Color' AS WordUS,
       EDIT_DISTANCE('Colour', 'Color') AS Distance;
```

Returns:

```output
WordUK WordUS Distance
------ ------ -----------
Colour Color  1
```

### B. Calculate edit distance between two words limited by a maximum value

The following example compares two words and returns the `EDIT_DISTANCE()` limited to a maximum value.

```sql
SELECT Source,
       Target,
       EDIT_DISTANCE(Source, Target) AS ActualDistance,
       EDIT_DISTANCE(Source, Target, 2) AS LimitedDistance
FROM (VALUES ('Chocolate', 'Sweets')) AS compare(Source, Target);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Source    Target    ActualDistance LimitedDistance
--------- --------- -------------- ---------------
Chocolate Sweets    8              3
```

For more examples, see the [EDIT_DISTANCE example](../../relational-databases/fuzzy-string-match/overview.md#example-edit_distance) in the [Fuzzy string matching overview](../../relational-databases/fuzzy-string-match/overview.md).

## Related content

- [EDIT_DISTANCE_SIMILARITY (Transact-SQL) preview](edit-distance-similarity-transact-sql.md)
- [JARO_WINKLER_DISTANCE (Transact-SQL) preview](jaro-winkler-distance-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL) preview](jaro-winkler-similarity-transact-sql.md)
