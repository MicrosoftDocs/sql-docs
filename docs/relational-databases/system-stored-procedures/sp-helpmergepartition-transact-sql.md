---
title: "sp_helpmergepartition (Transact-SQL)"
description: "Returns partition information for the specified merge publication."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergepartition"
  - "sp_helpmergepartition_TSQL"
helpviewer_keywords:
  - "sp_helpmergepartition"
dev_langs:
  - "TSQL"
---
# sp_helpmergepartition (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns partition information for the specified merge publication. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergepartition
    [ @publication = ] N'publication'
    [ , [ @suser_sname = ] N'suser_sname' ]
    [ , [ @host_name = ] N'host_name' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @suser_sname = ] N'*suser_sname*'

The [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) value used to define a partition. *@suser_sname* is **sysname**, with a default of `NULL`. Supply this parameter to limit the result set to only partitions where `SUSER_SNAME` resolves to the supplied value.

When *@suser_sname* is supplied, *@host_name* must be `NULL`.

#### [ @host_name = ] N'*host_name*'

The [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) value used to define a partition. *@host_name* is **sysname**, with a default of `NULL`. Supply this parameter to limit the result set to only partitions where `HOST_NAME` resolves to the supplied value.

When *@suser_sname* is supplied, *@host_name* must be `NULL`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `partition` | **int** | Identifies the Subscriber partition. |
| `host_name` | **sysname** | Value used when creating the partition for a subscription that is filtered by the value of the `HOST_NAME` function at the Subscriber. |
| `suser_sname` | **sysname** | Value used when creating the partition for a subscription that is filtered by the value of the `SUSER_SNAME` function at the Subscriber. |
| `dynamic_snapshot_location` | **nvarchar(255)** | Location of the filtered data snapshot for the Subscriber's partition. |
| `date_refreshed` | **datetime** | Last date that the snapshot job was run to generate the filtered data snapshot for the partition. |
| `dynamic_snapshot_jobid` | **uniqueidentifier** | Identifies the job that creates the filtered data snapshot for a partition. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergepartition` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_helpmergepartition`.

## Related content

- [sp_addmergepartition (Transact-SQL)](sp-addmergepartition-transact-sql.md)
- [sp_dropmergepartition (Transact-SQL)](sp-dropmergepartition-transact-sql.md)
