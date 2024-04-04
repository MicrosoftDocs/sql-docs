---
title: "sp_add_agent_parameter (Transact-SQL)"
description: "sp_add_agent_parameter (Transact-SQL)"
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_add_agent_parameter_TSQL"
  - "sp_add_agent_parameter"
helpviewer_keywords:
  - "sp_add_agent_parameter"
dev_langs:
  - "TSQL"
---
# sp_add_agent_parameter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a new parameter and its value to an agent profile. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_agent_parameter [ @profile_id = ] profile_id
        , [ @parameter_name = ] 'parameter_name'
        , [ @parameter_value = ] 'parameter_value'
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID of the profile from the `MSagent_profiles` table in the `msdb` database. *@profile_id* is **int**, with no default.

To find out what agent type this *@profile_id* represents, find the *@profile_id* in the [MSagent_profiles (Transact-SQL)](../system-tables/msagent-profiles-transact-sql.md) table, and note the `agent_type` field value. The values are as follows:

| Value | Description |
| --- | --- |
| `1` | Snapshot Agent |
| `2` | Log Reader Agent |
| `3` | Distribution Agent |
| `4` | Merge Agent |
| `9` | Queue Reader Agent |

#### [ @parameter_name = ] '*parameter_name*'

The name of the parameter. *@parameter_name* is **sysname**, with no default. For a list of parameters already defined in system profiles, see [Replication Agent Profiles](../replication/agents/replication-agent-profiles.md). For a complete list of valid parameters for each agent, see the following topics:

- [Replication Snapshot Agent](../replication/agents/replication-snapshot-agent.md)
- [Replication Log Reader Agent](../replication/agents/replication-log-reader-agent.md)
- [Replication Distribution Agent](../replication/agents/replication-distribution-agent.md)
- [Replication Merge Agent](../replication/agents/replication-merge-agent.md)
- [Replication Queue Reader Agent](../replication/agents/replication-queue-reader-agent.md)

#### [ @parameter_value = ] '*parameter_value*'

The value to be assigned to the parameter. *@parameter_value* is **nvarchar(255)**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_add_agent_parameter` is used in snapshot replication, transactional replication, and merge replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_add_agent_parameter`.

## Related content

- [Work with Replication Agent Profiles](../replication/agents/work-with-replication-agent-profiles.md)
- [Replication Agent Profiles](../replication/agents/replication-agent-profiles.md)
- [sp_add_agent_profile (Transact-SQL)](sp-add-agent-profile-transact-sql.md)
- [sp_change_agent_profile (Transact-SQL)](sp-change-agent-profile-transact-sql.md)
- [sp_change_agent_parameter (Transact-SQL)](sp-change-agent-parameter-transact-sql.md)
- [sp_drop_agent_parameter (Transact-SQL)](sp-drop-agent-parameter-transact-sql.md)
- [sp_help_agent_parameter (Transact-SQL)](sp-help-agent-parameter-transact-sql.md)
