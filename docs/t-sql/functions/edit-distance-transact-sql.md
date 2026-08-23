---
title: "EDIT_DISTANCE (Transact-SQL)"
description: EDIT_DISTANCE calculates the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 11/18/2025
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

Calculates the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another.

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

The maximum distance that should be computed. *maximum_distance* is an integer. If greater than or equal to zero, then the function returns the actual distance value or a distance value that is greater than *maxiumum_distance* value. If the actual distance is greater than *maximum_distance*, then the function might return a value greater than or equal to *maximum_distance*. If the parameter isn't specified or if *maximum_distance* is negative, then the function returns the actual number of transformations needed. If the value is NULL, then the function returns NULL.

## Return value

**int**

## Remarks

This function implements the Damerau-Levenshtein algorithm. If any of the inputs is `NULL` then the function returns a `NULL` value. Otherwise, the function returns an integer value from 0 to the number of transformations or *maximum_distance* value.

## Examples

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

For additional examples, see [Example *EDIT_DISTANCE()*](../../relational-databases/fuzzy-string-match/overview.md#example-edit_distance).

## Related content

- [EDIT_DISTANCE_SIMILARITY (Transact-SQL) preview](edit-distance-similarity-transact-sql.md)
- [JARO_WINKLER_DISTANCE (Transact-SQL) preview](jaro-winkler-distance-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL) preview](jaro-winkler-similarity-transact-sql.md)
