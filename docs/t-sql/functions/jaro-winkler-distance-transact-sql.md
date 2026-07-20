---
title: "JARO_WINKLER_DISTANCE (Transact-SQL)"
description: JARO_WINKLER_DISTANCE calculates the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length.
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

# JARO_WINKLER_DISTANCE (Transact-SQL) preview

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

[!INCLUDE [preview](../../includes/preview.md)]

Calculates the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length.

> [!NOTE]  
> - `JARO_WINKLER_DISTANCE` is currently in preview in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
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

For additional examples, see [Example *JARO_WINKLER_DISTANCE*](../../relational-databases/fuzzy-string-match/overview.md#example-jaro_winkler_distance).

## Related content

- [EDIT_DISTANCE (Transact-SQL)](edit-distance-transact-sql.md)
- [EDIT_DISTANCE_SIMILARITY (Transact-SQL)](edit-distance-similarity-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL)](jaro-winkler-similarity-transact-sql.md)
