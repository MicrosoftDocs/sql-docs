---
title: sp_check_join_filter (Transact-SQL)
description: sp_check_join_filter verifies a join filter between two tables to determine if the join filter clause is valid.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_check_join_filter"
  - "sp_check_join_filter_TSQL"
helpviewer_keywords:
  - "sp_check_join_filter"
dev_langs:
  - "TSQL"
---
# sp_check_join_filter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Verifies a join filter between two tables to determine if the join filter clause is valid. This stored procedure also returns information about the supplied join filter, including if it can be used with precomputed partitions for the given table. This stored procedure is executed at the Publisher on the publication. For more information, see [Parameterized Filters - Optimize for Precomputed Partitions](../replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_check_join_filter
    [ @filtered_table = ] N'filtered_table'
    , [ @join_table = ] N'join_table'
    , [ @join_filterclause = ] N'join_filterclause'
[ ; ]
```

## Arguments

#### [ @filtered_table = ] N'*filtered_table*'

The name of a filtered table. *@filtered_table* is **nvarchar(400)**, with no default.

#### [ @join_table = ] N'*join_table*'

The name of a table being joined to *@filtered_table*. *@join_table* is **nvarchar(400)**, with no default.

#### [ @join_filterclause = ] N'*join_filterclause*'

The join filter clause being tested. *@join_filterclause* is **nvarchar(1000)**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `can_use_partition_groups` | **bit** | Is if the publication qualifies for precomputed partitions; where `1` means that precomputed partitions can be used, and `0` means that they can't be used. |
| `has_dynamic_filters` | **bit** | Is if the supplied filter clause includes at least one parameterized filtering function; where `1` means that a parameterized filtering function is used, and `0` means that such a function isn't used. |
| `dynamic_filters_function_list` | **nvarchar(500)** | List of the functions in the filter clause that define a parameterized filter for an article, where each function is separated by a semi-colon. |
| `uses_host_name` | **bit** | If the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function is used in the filter clause, where `1` means that this function is present. |
| `uses_suser_sname` | **bit** | If the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function is used in the filter clause, where `1` means that this function is present. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_check_join_filter` is used in merge replication.

`sp_check_join_filter` can be executed against any related tables even if they aren't published. This stored procedure can be used to verify a join filter clause before defining a join filter between two articles.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_check_join_filter`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
