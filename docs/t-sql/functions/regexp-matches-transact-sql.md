---
title: "REGEXP_MATCHES (Transact-SQL)"
description: Returns a table of captured substrings that match a regular expression pattern to a string. If no match is found, the function returns no row.
author: uc-msft
ms.author: umajay
ms.reviewer: abhtiwar, wiassaf
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

# REGEXP_MATCHES (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `REGEXP_MATCHES` table-valued regular expression function returns a table of captured substrings that match a regular expression pattern to a string. If no match is found, the function returns no row.

```syntaxsql
REGEXP_MATCHES
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

Returns a tabular result as follows:

| Column name | Data type | Description |
| --- | --- | --- |
| `match_id` | **bigint** | The sequence of matching words. |
| `start_position` | **int** | Starting index position. |
| `end_position` | **int** | Ending index position. |
| `match_value` | Same type as `string_expression` or **varchar** | Matching expression. |
| `substring_matches` | **json** | JSON document describing match. |

## Platform support

Requires database compatibility level 170. To set database compatibility level, review [ALTER DATABASE (Transact-SQL) compatibility level](../statements/alter-database-transact-sql-compatibility-level.md). The compatibility level requirement doesn't apply if the [ALLOW_BUILTIN_TVF_IN_ALL_COMPAT_LEVELS](../statements/alter-database-scoped-configuration-transact-sql.md#allow-builtin-tvf-in-all-compat-levels) database-scoped configuration is enabled.

- In Azure SQL Managed Instance, T-SQL regular expression functions are available with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy) only.
- T-SQL regular expression functions are available in Azure SQL Database, SQL database in Fabric, and starting in SQL Server 2025.
- T-SQL regular expression functions aren't available in SQL Server 2022 or earlier versions.
- In Fabric Data Warehouse, the regular expression table-valued functions `REGEXP_MATCHES` and [REGEXP_SPLIT_TO_TABLE](regexp-split-to-table-transact-sql.md) aren't supported. T-SQL regular expression scalar functions are available as a preview feature in Fabric Data Warehouse.

## Examples

Return tabular results from `'Learning #AzureSQL #AzureSQLDB'` that start with a `#` character followed by one or more alphanumeric characters (`A-Z, a-z, 0-9`) or underscores (`_`).

```sql
SELECT *
FROM REGEXP_MATCHES ('Learning #AzureSQL #AzureSQLDB', '#([A-Za-z0-9_]+)');
```

```output
match_id start_position end_position match_value substring_matches
1        10             18           #AzureSQL   [{"value":"AzureSQL","start":11,"length":8}]
2        20             30           #AzureSQLDB [{"value":"AzureSQLDB","start":21,"length":10}]
```

Return strings from `ABC` that match strings that start with the letter `A` followed by exactly two characters.

```sql
SELECT *
FROM REGEXP_MATCHES ('ABC', '^(A)(..)$');
```

```output
match_id start_position end_position match_value substring_matches
1        1              3            ABC         [{"value":"A","start":1,"length":1},{"value":"BC","start":2,"length":2}]
```

## Related content

- [Regular expressions functions](../../relational-databases/regular-expressions/overview.md)
