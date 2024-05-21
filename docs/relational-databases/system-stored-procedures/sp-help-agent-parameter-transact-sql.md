---
title: "sp_help_agent_parameter (Transact-SQL)"
description: Returns all the parameters of a profile from the MSagent_parameters system table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_help_agent_parameter"
  - "sp_help_agent_parameter_TSQL"
helpviewer_keywords:
  - "sp_help_agent_parameter"
dev_langs:
  - "TSQL"
---
# sp_help_agent_parameter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns all the parameters of a profile from the [MSagent_parameters](../system-tables/msagent-parameters-transact-sql.md) system table. This stored procedure is executed at the Distributor where the agent is running, on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_agent_parameter [ [ @profile_id = ] profile_id ]
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID of the profile from the [MSagent_parameters](../system-tables/msagent-parameters-transact-sql.md) table. *@profile_id* is **int**, with a default of `-1`, which returns all parameters.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `profile_id` | **int** | ID of the agent profile. |
| `parameter_name` | **sysname** | Name of the parameter. |
| `value` | **nvarchar(255)** | Value of the parameter. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_agent_parameter` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **replmonitor** fixed database role can execute `sp_help_agent_parameter`.

## Related content

- [Work with Replication Agent Profiles](../replication/agents/work-with-replication-agent-profiles.md)
- [sp_add_agent_parameter (Transact-SQL)](sp-add-agent-parameter-transact-sql.md)
- [sp_drop_agent_parameter (Transact-SQL)](sp-drop-agent-parameter-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
