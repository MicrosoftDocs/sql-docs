---
title: "sp_help_agent_profile (Transact-SQL)"
description: Displays the profile of a specified agent. This stored procedure is executed at the Distributor on any database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_help_agent_profile"
  - "sp_help_agent_profile_TSQL"
helpviewer_keywords:
  - "sp_help_agent_profile"
dev_langs:
  - "TSQL"
---
# sp_help_agent_profile (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays the profile of a specified agent. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_agent_profile
    [ [ @agent_type = ] agent_type ]
    [ , [ @profile_id = ] profile_id ]
[ ; ]
```

## Arguments

#### [ @agent_type = ] *agent_type*

The type of agent. *@agent_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` (default) | All agent types |
| `1` | Snapshot Agent |
| `2` | Log Reader Agent |
| `3` | Distribution Agent |
| `4` | Merge Agent |
| `9` | Queue Reader Agent |

#### [ @profile_id = ] *profile_id*

The ID of the profile to be displayed. *@profile_id* is **int**, with a default of `-1`, which returns all the profiles in the `MSagent_profiles` table.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `profile_id` | **int** | ID of the profile. |
| `profile_name` | **sysname** | Unique for agent type. |
| `agent_type` | **int** | `1` = Snapshot Agent<br /><br />`2` = Log Reader Agent<br /><br />`3` = Distribution Agent<br /><br />`4` = Merge Agent<br /><br />`9` = Queue Reader Agent |
| `type` | **int** | `0` = System<br /><br />`1` = Custom |
| `description` | **varchar(3000)** | Description of the profile. |
| `def_profile` | **bit** | Specifies whether this profile is the default for this agent type. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_agent_profile` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **replmonitor** fixed database role can execute `sp_help_agent_profile`.

## Related content

- [Work with Replication Agent Profiles](../replication/agents/work-with-replication-agent-profiles.md)
- [sp_add_agent_profile (Transact-SQL)](sp-add-agent-profile-transact-sql.md)
- [sp_drop_agent_profile (Transact-SQL)](sp-drop-agent-profile-transact-sql.md)
- [sp_help_agent_parameter (Transact-SQL)](sp-help-agent-parameter-transact-sql.md)
