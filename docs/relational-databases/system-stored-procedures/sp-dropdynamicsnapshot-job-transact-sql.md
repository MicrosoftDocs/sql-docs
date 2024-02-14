---
title: "sp_dropdynamicsnapshot_job (Transact-SQL)"
description: sp_dropdynamicsnapshot_job removes a filtered data snapshot job for a publication with parameterized row filters.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropdynamicsnapshot_job_TSQL"
  - "sp_dropdynamicsnapshot_job"
helpviewer_keywords:
  - "sp_dropdynamicsnapshot_job"
dev_langs:
  - "TSQL"
---
# sp_dropdynamicsnapshot_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a filtered data snapshot job for a publication with parameterized row filters. This stored procedure is executed at the Publisher on the publication database. When the job is deleted, all of the related data is deleted from the [MSdynamicsnapshotjobs](../system-tables/msdynamicsnapshotjobs-transact-sql.md) system table.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropdynamicsnapshot_job
    [ @publication = ] N'publication'
    [ , [ @dynamic_snapshot_jobname = ] N'dynamic_snapshot_jobname' ]
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' ]
    [ , [ @ignore_distributor = ] ignore_distributor ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication from which the filtered data snapshot job is being removed. *@publication* is **sysname**, with no default.

#### [ @dynamic_snapshot_jobname = ] N'*dynamic_snapshot_jobname*'

The name of the filtered data snapshot job being removed. *@dynamic_snapshot_jobname* is **sysname**, with a default of `%`. If this value isn't supplied, it defaults to whatever job name is associated with *@dynamic_snapshot_jobid*.

Only *@dynamic_snapshot_jobid* or *@dynamic_snapshot_jobname* can be specified. If values aren't supplied for either *@dynamic_snapshot_jobid* or *@dynamic_snapshot_jobname*, all dynamic snapshot jobs for the publication are removed.

#### [ @dynamic_snapshot_jobid = ] '*dynamic_snapshot_jobid*'

An identifier for the filtered data snapshot job being removed. *@dynamic_snapshot_jobid* is **uniqueidentifier**, with a default of `NULL`.

Only *@dynamic_snapshot_jobid* or *@dynamic_snapshot_jobname* can be specified. If values aren't supplied for either *@dynamic_snapshot_jobid* or *@dynamic_snapshot_jobname*, all dynamic snapshot jobs for the publication are removed.

#### [ @ignore_distributor = ] *ignore_distributor*

This parameter can be used to drop a dynamic snapshot job without doing cleanup tasks at the Distributor. *@ignore_distributor* is **bit**, with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropdynamicsnapshot` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_dropdynamicsnapshot`.

## Related content

- [sp_adddynamicsnapshot_job (Transact-SQL)](sp-adddynamicsnapshot-job-transact-sql.md)
