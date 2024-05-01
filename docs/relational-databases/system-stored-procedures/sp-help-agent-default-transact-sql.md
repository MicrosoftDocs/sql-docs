---
title: "sp_help_agent_default (Transact-SQL)"
description: "Retrieves the ID of the default configuration for the agent type passed as parameter. This stored procedure is executed at Distributor on any database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_help_agent_default"
  - "sp_help_agent_default_TSQL"
helpviewer_keywords:
  - "sp_help_agent_default"
dev_langs:
  - "TSQL"
---
# sp_help_agent_default (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Retrieves the ID of the default configuration for the agent type passed as parameter. This stored procedure is executed at Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_agent_default [ @profile_id = ] profile_id OUTPUT
        , [ @agent_type = ] agent_type
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id* OUTPUT

The ID of the default configuration for the type of agent. *@profile_id* is **int**, with no default. *@profile_id* is also an `OUTPUT` parameter and returns the ID of the default configuration for the type of agent.

#### [ @agent_type = ] '*agent_type*'

The type of agent. *@agent_type* is **int**, with no default, and can be one of the following values:

| Value | Description |
| --- | --- |
| `1` | Snapshot Agent |
| `2` | Log Reader Agent |
| `3` | Distribution Agent |
| `4` | Merge Agent |
| `9` | Queue Reader Agent |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_agent_default` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **replmonitor** fixed database role can execute `sp_help_agent_default`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
