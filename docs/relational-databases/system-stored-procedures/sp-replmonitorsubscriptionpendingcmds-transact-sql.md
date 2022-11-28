---
title: "sp_replmonitorsubscriptionpendingcmds (T-SQL)"
description: Describes the sp_replmonitorsubscriptionpendingcmds stored procedure that returns information on the number of pending commands for a subscription to a transactional publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/24/2022
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ms.custom: seo-lt-2019
f1_keywords:
  - "sp_replmonitorsubscriptionpendingcmds_TSQL"
  - "sp_replmonitorsubscriptionpendingcmds"
helpviewer_keywords:
  - "sp_replmonitorsubscriptionpendingcmds"
dev_langs:
  - "TSQL"
---
# sp_replmonitorsubscriptionpendingcmds (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information on the number of pending commands for a subscription to a transactional publication and a rough estimate of how much time it takes to process them. This stored procedure returns one row for each returned subscription. This stored procedure, which is used to monitor replication, is executed at the Distributor on the distribution database.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replmonitorsubscriptionpendingcmds [ @publisher = ] 'publisher'
    , [ @publisher_db = ] 'publisher_db'
    , [ @publication = ] 'publication'
    , [ @subscriber = ] 'subscriber'
    , [ @subscriber_db = ] 'subscriber_db'
    , [ @subscription_type = ] subscription_type
    , [ @subdb_version = ] subdb_version
```

## Arguments

#### [ @publisher = ] 'publisher'

The name of the Publisher. *publisher* is **sysname**, with no default.

#### [ @publisher_db = ] 'publisher_db'

The name of the published database. *publisher_db* is **sysname**, with no default.

#### [ @publication = ] 'publication'

The name of the publication. *publication* is **sysname**, with no default.

#### [ @subscriber = ] 'subscriber'

The name of the Subscriber. *subscriber* is **sysname**, with no default.

#### [ @subscriber_db = ] 'subscriber_db'

The name of the subscription database. *subscriber_db* is **sysname**, with no default.

#### [ @subscription_type = ] subscription_type

The type of subscription. *subscription_type* is **int**, with no default and can be one of these values.

|Value|Description|
|-----------|-----------------|
|**0**|Push subscription|
|**1**|Pull subscription|

#### [ @subdb_version = ] subdb_version

The `dbversion` of the subscription database. *subdb_version* is an optional parameter of type **int**, with a default value of 0.

## Result sets

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`pendingcmdcount`|**int**|The number of commands that are pending for the subscription.|
|`estimatedprocesstime`|**int**|Estimate of the number of seconds required to deliver all of the pending commands to the subscriber.|

## Return code values

**0** (success) or **1** (failure)

## Remarks

`sp_replmonitorsubscriptionpendingcmds` is used with transactional replication.

Prior to [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU17, `sp_replmonitorsubscriptionpendingcmds` wasn't supported with peer-to-peer replication, and returned an incorrect number of pending commands when used to query peer-to-peer replication topology. In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 17, support was added to make `sp_replmonitorsubscriptionpendingcmds` compatible with peer-to-peer publications.

However, even with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU17 or later, `sp_replmonitorsubscriptionpendingcmds` could report an incorrect number of pending commands when used with peer-to-peer replication if the table `MSrepl_originators` contains a stale entry of an incorrect version of the subscription database. To correct the problem, either delete all the stale entries from `MSrepl_originators` or pass the correct `dbversion` of the subscription database when using the `subdb_version` argument for the `sp_replmonitorsubscriptionpendingcmds` stored procedure.

See [KB5017009](https://support.microsoft.com/help/5017009) for details on how to determine `dbversion`.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role in the distribution database can execute `sp_replmonitorsubscriptionpendingcmds`. Members of the publication access list for a publication that uses the distribution database can execute `sp_replmonitorsubscriptionpendingcmds` to return pending commands for that publication.

## See also

- [Programmatically monitor replication](../replication/monitor/programmatically-monitor-replication.md)
