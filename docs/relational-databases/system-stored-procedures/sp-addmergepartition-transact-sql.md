---
title: "sp_addmergepartition (Transact-SQL)"
description: Creates a dynamically filtered partition for a subscription that is filtered.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergepartition"
  - "sp_addmergepartition_TSQL"
helpviewer_keywords:
  - "sp_addmergepartition"
dev_langs:
  - "TSQL"
---
# sp_addmergepartition (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a dynamically filtered partition for a subscription, filtered by the values of [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) or [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) at the Subscriber. This stored procedure is executed at the Publisher on the database that is being published, and is used to manually generate partitions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmergepartition
    [ @publication = ] N'publication'
    [ , [ @suser_sname = ] N'suser_sname' ]
    [ , [ @host_name = ] N'host_name' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The merge publication on which the partition is being created. *@publication* is **sysname**, with no default. If *@suser_sname* is specified, the value of *hostname* must be `NULL`.

#### [ @suser_sname = ] N'*suser_sname*'

The value used when creating the partition for a subscription, filtered by the value of the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function at the Subscriber. *@suser_sname* is **sysname**, with no default.

#### [ @host_name = ] N'*host_name*'

The value used when creating the partition for a subscription, filtered by the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function at the Subscriber. *@host_name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergepartition` is used in merge replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergepartition-tra_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addmergepartition`.

## Related content

- [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)
- [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md)
