---
title: "sp_gettopologyinfo (Transact-SQL)"
description: sp_gettopologyinfo returns information about a peer-to-peer transactional replication topology.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_gettopologyinfo_TSQL"
  - "sp_gettopologyinfo"
helpviewer_keywords:
  - "sp_gettopologyinfo"
dev_langs:
  - "TSQL"
---
# sp_gettopologyinfo (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about a peer-to-peer transactional replication topology. Execute [sp_requestpeertopologyinfo](sp-requestpeertopologyinfo-transact-sql.md) to collect information before you execute this procedure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_gettopologyinfo [ [ @request_id = ] request_id ]
[ ; ]
```

## Arguments

#### [ @request_id = ] *request_id*

The ID of a topology status request. *@request_id* is **int**, with a default of `NULL`. To obtain an ID, use the *@request_id* OUTPUT parameter from [sp_requestpeertopologyinfo](sp-requestpeertopologyinfo-transact-sql.md), or query the [MSpeer_topologyrequest](../system-tables/mspeer-topologyrequest-transact-sql.md) system table.

## Result set

`sp_gettopologyinfo` returns a result set that's a single XML column. The data in the XML column is the same as the data in the [MSpeer_topologyresponse](../system-tables/mspeer-topologyresponse-transact-sql.md) system table.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_gettopologyinfo` is used in peer-to-peer transactional replication. Execute [sp_requestpeertopologyinfo](sp-requestpeertopologyinfo-transact-sql.md) before you execute `sp_gettopologyinfo`. These procedures are used by the Configure Peer-to-Peer Topology Wizard, but they can also be used directly if you require topology information in XML format. If you prefer tabular results, query the [MSpeer_topologyresponse](../system-tables/mspeer-topologyresponse-transact-sql.md) system table.

## Permissions

Requires membership in the **sysadmin** fixed server role, or **db_owner** fixed database role.

## Related content

- [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
