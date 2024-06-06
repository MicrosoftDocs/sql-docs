---
title: "sp_help_peerconflictdetection (Transact-SQL)"
description: Returns information about the conflict detection settings for a publication involved in a peer-to-peer transactional replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_help_peerconflictdetection"
  - "sp_help_peerconflictdetection_TSQL"
helpviewer_keywords:
  - "sp_help_peerconflictdetection"
dev_langs:
  - "TSQL"
---
# sp_help_peerconflictdetection (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the conflict detection settings for a publication that is involved in a peer-to-peer transactional replication topology.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_peerconflictdetection
    [ @publication = ] N'publication'
    [ , [ @timeout = ] timeout ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication for which to return information. *@publication* is **sysname**, with no default.

#### [ @timeout = ] *timeout*

Specifies the amount of time, in seconds, after which the procedure times out while waiting for response from every node in the topology. *@timeout* is **int**, with a default of `60`. If there's a read-only Subscriber in the topology, specifying a time-out value isn't valid. Read-only Subscribers never respond to a call from this procedure.

## Result set

`sp_help_peerconflictdetection` returns three result sets. These results are documented in the following articles:

- [MSpeer_conflictdetectionconfigrequest (Transact-SQL)](../system-tables/mspeer-conflictdetectionconfigrequest-transact-sql.md)
- [MSpeer_conflictdetectionconfigresponse (Transact-SQL)](../system-tables/mspeer-conflictdetectionconfigresponse-transact-sql.md)
- [MSpeer_originatorid_history (Transact-SQL)](../system-tables/mspeer-originatorid-history-transact-sql.md)

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_peerconflictdetection` is used in peer-to-peer transactional replication.

## Permissions

Requires membership in the **sysadmin** fixed server role, or **db_owner** fixed database role.

## Related content

- [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md)
- [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
