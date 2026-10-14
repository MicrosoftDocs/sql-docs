---
title: "REGEXP_SPLIT_TO_TABLE (Transact-SQL)"
description: Returns a table of strings split, delimited by the regex pattern. If there's no match to the pattern, the function returns the string.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: abhtiwar, wiassaf, randolphwest
ms.date: 10/14/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# REGEXP_SPLIT_TO_TABLE

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `REGEXP_SPLIT_TO_TABLE` table-valued regular expression function returns a table of strings split, delimited by the regex pattern. If there's no match to the pattern, the function returns the string.

```syntaxsql
REGEXP_SPLIT_TO_TABLE
(
    string_expression,
    pattern_expression [ , flags ]
)
```

## Arguments

#### *string_expression*

[!INCLUDE [regexp-string-expression](../../includes/regexp-string-expression.md)]

#### *pattern_expression*

[!INCLUDE [regexp-pattern-expression](../../includes/regexp-pattern-expression.md)]

#### *flags*

[!INCLUDE [regexp-flags-expression](../../includes/regexp-flags-expression.md)]

## Returns

`REGEXP_SPLIT_TO_TABLE` returns the following two-column table:

| Column name | Data type | Description |
| --- | --- | --- |
| `value` | Same type as `string_expression` or **varchar** | If the delimiter is found, it's the matching substring. Otherwise it's the whole expression. |
| `ordinal` | **bigint** | 1-based index value of each substring position from the input expression. |

Return a table split for `the quick brown fox jumps over the lazy dog`.

```sql
SELECT *
FROM REGEXP_SPLIT_TO_TABLE ('the quick brown fox jumps over the lazy dog', '\s+');
```

```output
Value  Ordinal
the    1
quick  2
brown  3
fox    4
jumps  5
over   6
the    7
lazy   8
dog    9
```

## Platform support

Requires database compatibility level 170. To set database compatibility level, review [ALTER DATABASE (Transact-SQL) compatibility level](../statements/alter-database-transact-sql-compatibility-level.md). The compatibility level requirement doesn't apply if the [ALLOW_BUILTIN_TVF_IN_ALL_COMPAT_LEVELS](../statements/alter-database-scoped-configuration-transact-sql.md#allow-builtin-tvf-in-all-compat-levels) database-scoped configuration is enabled.

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- In Fabric Data Warehouse, the regular expression table-valued functions `REGEXP_SPLIT_TO_TABLE` and [REGEXP_MATCHES](regexp-matches-transact-sql.md) aren't supported. T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse.

## Related content

- [Regular expressions functions](../../relational-databases/regular-expressions/overview.md)
