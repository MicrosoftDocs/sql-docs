---
title: EDIT_DISTANCE_SIMILARITY (Transact-SQL)
description: EDIT_DISTANCE_SIMILARITY calculates a similarity value ranging from 0 (indicating no match) to 100 (indicating full match).
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

# EDIT_DISTANCE_SIMILARITY (Transact-SQL) preview

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

[!INCLUDE [preview](../../includes/preview.md)]

Calculates a similarity score between two strings based on their *edit distance*. The score ranges from 0 (no similarity) to 100 (an exact match).

> [!NOTE]  
>
> - `EDIT_DISTANCE_SIMILARITY` is currently in preview.
> - SQL Server support for `EDIT_DISTANCE_SIMILARITY` introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].
> - `EDIT_DISTANCE_SIMILARITY` is available in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

```syntaxsql
EDIT_DISTANCE_SIMILARITY (
    character_expression
    , character_expression
)
```

## Arguments

#### *character_expression*

An alphanumeric expression of character data. *character_expression* can be a constant, variable, or column. The character expression can't be of type **varchar(max)** or **nvarchar(max)**.

## Return types

**int**

## Remarks

This function implements the Damerau-Levenshtein (Optimal String Alignment) algorithm. If any of the inputs is `NULL` then the function returns a `NULL` value. Otherwise, the function returns an integer value from `0` to `100`. The similarity value is computed as `(1 - (edit_distance / greatest(len(string1), len(string2)))) * 100`.

## Examples

### A. Calculate edit distance similarity between two words

The following example compares two words and returns the `EDIT_DISTANCE_SIMILARITY()` value as a column, named `Similarity`.

```sql
SELECT 'Colour' AS WordUK,
       'Color' AS WordUS,
       EDIT_DISTANCE_SIMILARITY('Colour', 'Color') AS Similarity;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
WordUK WordUS Similarity
------ ------ -----------
Colour Color  83
```

For additional examples, see [Example *EDIT_DISTANCE_SIMILARITY()*](../../relational-databases/fuzzy-string-match/overview.md#example-edit_distance_similarity).

## Related content

- [EDIT_DISTANCE (Transact-SQL) preview](edit-distance-transact-sql.md)
- [JARO_WINKLER_DISTANCE (Transact-SQL) preview](jaro-winkler-distance-transact-sql.md)
- [JARO_WINKLER_SIMILARITY (Transact-SQL) preview](jaro-winkler-similarity-transact-sql.md)
