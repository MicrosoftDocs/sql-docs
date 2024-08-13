---
title: sp_check_dynamic_filters (Transact-SQL)
description: sp_check_dynamic_filters displays information on parameterized row filter properties for a publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "dynamic_filters_TSQL"
  - "sp_check_dynamic filter"
  - "check_dynamic_filters_TSQL"
  - "dynamic filters"
  - "check dynamic filters"
  - "sp_check_dynamic filter_TSQL"
helpviewer_keywords:
  - "sp_check_dynamic_filters"
dev_langs:
  - "TSQL"
---
# sp_check_dynamic_filters (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays information on parameterized row filter properties for a publication, in particular the functions used to generate a filtered data partition for a publication and whether the publication qualifies for using precomputed partitions. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_check_dynamic_filters [ @publication = ] N'publication'
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `can_use_partition_groups` | **bit** | Is if the publication qualifies for using precomputed partitions; where `1` means that precomputed partitions can be used, and `0` means that they can't be used. |
| `has_dynamic_filters` | **bit** | Is if at least one parameterized row filter is defined in the publication; where `1` means that one or more parameterized row filters exist, and `0` means that no dynamic filters exist. |
| `dynamic_filters_function_list` | **nvarchar(500)** | List of functions used to filter articles in a publication, where each function is separated by a semi-colon. |
| `validate_subscriber_info` | **nvarchar(500)** | List of functions used to filter articles in a publication, where each function is separated by a plus sign (`+`). |
| `uses_host_name` | **bit** | If the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function is used in parameterized row filters, where `1` means that this function is used for dynamic filtering. |
| `uses_suser_sname` | **bit** | If the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function is used in parameterized row filters, where `1` means that this function is used for dynamic filtering. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_check_dynamic_filters` is used in merge replication.

If a publication is defined to use precomputed partitions, `sp_check_dynamic_filters` checks for any violations of the restrictions of precomputed partitions. If any are found, an error is returned. For more information, see [Parameterized Filters - Optimize for Precomputed Partitions](../replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).

If a publication is defined with parameterized row filters, but no parameterized row filters are found, an error is returned.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_check_dynamic_filters`.

## Related content

- [Manage Partitions for a Merge Publication with Parameterized Filters](../replication/publish/manage-partitions-for-a-merge-publication-with-parameterized-filters.md)
- [sp_check_join_filter (Transact-SQL)](sp-check-join-filter-transact-sql.md)
- [sp_check_subset_filter (Transact-SQL)](sp-check-subset-filter-transact-sql.md)
