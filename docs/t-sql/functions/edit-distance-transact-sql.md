---
title: "EDIT_DISTANCE (Transact-SQL)"
description: EDIT_DISTANCE calculates the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb || >=sql-server-2016"
---

# EDIT_DISTANCE (Transact-SQL) preview

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

[!INCLUDE [preview](../../includes/preview.md)]

Calculates the distance that is the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another.

> [!NOTE]  
>
> - `EDIT_DISTANCE` is in preview.
> - `EDIT_DISTANCE` currently doesn't support transpositions.
> - SQL Server support for `EDIT_DISTANCE` introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
> - `EDIT_DISTANCE` is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

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

The maximum distance that should be computed. *maximum_distance* is an integer. If greater than or equal to zero, then the function stops calculating the distance when the *maximum_distance* is reached.

## Return value

**int**

Returns the distance between the two *character_expressions* using Damerau-Levenshtein algorithm, or *maximum_distance* value if that is smaller.
If any of the inputs is `NULL` then the function returns a `NULL` value.

## Remarks

If the actual distance is greater than *maximum_distance*, then the function might return a value greater than or equal to *maximum_distance*.

## Examples

### Example 1

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

### Example 2

The following example compares two words and returns the `EDIT_DISTANCE()` limited to a maximum value.

```sql
SELECT Source, Target,
	   EDIT_DISTANCE(Source, Target) AS ActualDistance,
       EDIT_DISTANCE(Source, Target,2) AS LimitedDistance
FROM (VALUES('Chocolate', 'Sweets')) compare(Source, Target) ;

```

Returns:

```output
Source    Target    ActualDistance LimitedDistance
--------- --------- -------------- ---------------
Chocolate Sweets    8              2
```

For additional examples, see [Example *EDIT_DISTANCE()*](../../relational-databases/fuzzy-string-match/overview.md#example-edit_distance).

## Related content

- [EDIT_DISTANCE_SIMILARITY (Transact-SQL)](edit-distance-similarity-transact-sql.md)
- [JARO_WINKLER_DISTANCE (Transact-SQL)](jaro-winkler-distance-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL)](jaro-winkler-similarity-transact-sql.md)
