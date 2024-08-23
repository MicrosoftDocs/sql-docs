---
title: "sp_requestpeertopologyinfo (Transact-SQL)"
description: Populates the MSpeer_topologyresponse system table with information about a peer-to-peer transactional replication topology.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_requestpeertopologyinfo"
  - "sp_requestpeertopologyinfo_TSQL"
helpviewer_keywords:
  - "sp_requestpeertopologyinfo"
dev_langs:
  - "TSQL"
---
# sp_requestpeertopologyinfo (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Populates the [MSpeer_topologyresponse](../system-tables/mspeer-topologyresponse-transact-sql.md) system table with information about a peer-to-peer transactional replication topology. Execute [sp_gettopologyinfo](sp-gettopologyinfo-transact-sql.md) to obtain information from the table in XML format.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_requestpeertopologyinfo
    [ @publication = ] N'publication'
    [ , [ @request_id = ] request_id OUTPUT ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication for which to perform a topology-wide status request. *@publication* is **sysname**, with no default.

#### [ @request_id = ] *request_id* OUTPUT

The ID number that is assigned to the topology status request. *@request_id* is an OUTPUT parameter of type **int**. This ID can be used by [sp_gettopologyinfo](sp-gettopologyinfo-transact-sql.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_requestpeertopologyinfo` is used in peer-to-peer transactional replication. Execute `sp_requestpeertopologyinfo` before executing [sp_gettopologyinfo](sp-gettopologyinfo-transact-sql.md). These procedures are used by the Configure Peer-to-Peer Topology Wizard, but they can also be used directly if you require topology information in an XML format. If you prefer tabular results, query the [MSpeer_topologyresponse](../system-tables/mspeer-topologyresponse-transact-sql.md) system table.

## Permissions

Requires membership in the **sysadmin** fixed server role, **db_owner** fixed database role, or execute permission directly on this stored procedure.

## Related content

- [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
