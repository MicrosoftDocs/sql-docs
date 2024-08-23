---
title: "sp_replication_agent_checkup (Transact-SQL)"
description: Checks each distribution database for running replication agents with no logged history in the specified heartbeat interval.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replication_agent_checkup_TSQL"
  - "sp_replication_agent_checkup"
helpviewer_keywords:
  - "sp_replication_agent_checkup"
dev_langs:
  - "TSQL"
---
# sp_replication_agent_checkup (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Checks each distribution database for replication agents that are running but haven't logged history within the specified heartbeat interval. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replication_agent_checkup [ [ @heartbeat_interval = ] heartbeat_interval ]
[ ; ]
```

## Arguments

#### [ @heartbeat_interval = ] *heartbeat_interval*

The maximum number of minutes that an agent can go without logging a progress message. *@heartbeat_interval* is **int**, with a default of `10` minutes.

## Return code values

`sp_replication_agent_checkup` raises error 14151 for each agent it detects as suspect. It also logs a failure history message about the agents.

## Remarks

`sp_replication_agent_checkup` is used in snapshot replication, transactional replication, and merge replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_replication_agent_checkup`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
