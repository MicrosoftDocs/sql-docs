---
title: "sp_configure_peerconflictdetection (Transact-SQL)"
description: sp_configure_peerconflictdetection configures conflict detection for a publication that is involved in a peer-to-peer transactional replication topology.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_configure_peerconflictdetection_TSQL"
  - "sp_configure_peerconflictdetection"
helpviewer_keywords:
  - "sp_configure_peerconflictdetection"
dev_langs:
  - "TSQL"
---
# sp_configure_peerconflictdetection (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Configures conflict detection for a publication that is involved in a peer-to-peer transactional replication topology. For more information, see [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md). This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> You can't use `sp_configure_peerconflictdetection` to enable `lastwriter`. To change the conflict resolution of an existing replication topology, drop the publication and recreate it.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_configure_peerconflictdetection
    [ @publication = ] N'publication'
    , [ @action = ] N'action'
    [ , [ @originator_id = ] originator_id ]
    [ , [ @conflict_retention = ] conflict_retention ]
    [ , [ @continue_onconflict = ] N'continue_onconflict' ]
    [ , [ @local = ] N'local' ]
    [ , [ @timeout = ] timeout ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication for which to configure conflict detection. *@publication* is **sysname**, with no default.

#### [ @action = ] N'*action*'

Specifies whether to enable or disable conflict detection for a publication. *@action* is **nvarchar(32)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `enable` | Enables conflict detection for a publication. |
| `disable` | Disables conflict detection for a publication. |
| `NULL` (default) | |

#### [ @originator_id = ] *originator_id*

Specifies an ID for a node in a peer-to-peer topology. *@originator_id* is **int**, with a default of `NULL`. This ID is used for conflict detection if *@action* is set to `enable`. Specify a positive, nonzero ID that hasn't been used in the topology. For a list of IDs that were already used, query the [MSpeer_originatorid_history](../system-tables/mspeer-originatorid-history-transact-sql.md) system table.

#### [ @conflict_retention = ] *conflict_retention*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @continue_onconflict = ] N'*continue_onconflict*'

Determines whether the Distribution Agent continues to process changes after a conflict is detected. *@continue_onconflict* is **nvarchar(5)**, with a default of `false`.

> [!CAUTION]  
> We recommend that you use the default value of `false`. When this option is set to `true`, the Distribution Agent tries to converge data in the topology by applying the conflicting row from the node that's the highest originator ID. This method doesn't guarantee convergence. You should make sure that the topology is consistent after a conflict is detected. For more information, see [Handling Conflicts](../replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md#handling-conflicts).

#### [ @local = ] N'*local*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @timeout = ] *timeout*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_configure_peerconflictdetection` is used in peer-to-peer transactional replication. To use conflict detection, all nodes must be running [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] or later versions; and detection must be enabled for all nodes.

## Permissions

Requires membership in the **sysadmin** fixed server role, or **db_owner** fixed database role.

## Related content

- [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md)
- [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
