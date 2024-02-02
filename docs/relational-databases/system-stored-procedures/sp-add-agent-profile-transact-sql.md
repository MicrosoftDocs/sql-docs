---
title: "sp_add_agent_profile (Transact-SQL)"
description: "sp_add_agent_profile (Transact-SQL)"
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_add_agent_profile"
  - "sp_add_agent_profile_TSQL"
helpviewer_keywords:
  - "sp_add_agent_profile"
dev_langs:
  - "TSQL"
---
# sp_add_agent_profile (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a new profile for a replication agent. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_agent_profile [ [ @profile_id = ] profile_id OUTPUT ]
      , [ @profile_name = ] 'profile_name'
      , [ @agent_type = ] agent_type
    [ , [ @profile_type = ] profile_type ]
    [ , [ @description = ] N'description' ]
    [ , [ @default = ] default ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID associated with the newly inserted profile. *@profile_id* is **int** and is an optional OUTPUT parameter. If specified, the value is set to the new profile ID.

#### [ @profile_name = ] '*profile_name*'

The name of the profile. *@profile_name* is **sysname**, with no default.

#### [ @agent_type = ] *agent_type*

The type of replication agent. *@agent_type* is **int**, with no default, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Snapshot Agent |
| `2` | Log Reader Agent |
| `3` | Distribution Agent |
| `4` | Merge Agent |
| `9` | Queue Reader Agent |

#### [ @profile_type = ] *profile_type*

The type of profile.*profile_type* is **int**, with a default of `1`.

`0` indicates a system profile. `1` indicates a custom profile. Only custom profiles can be created using this stored procedure; therefore the only valid value is `1`. Only [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates system profiles.

#### [ @description = ] N'*description*'

A description of the profile. *@description* is **nvarchar(3000)**, with no default.

#### [ @default = ] *default*

Indicates whether the profile is the default for *@agent_type*. *@default* is **bit**, with a default of `0`. `1` indicates that the profile being added will become the new default profile for the agent specified by *@agent_type*.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_add_agent_profile` is used in snapshot replication, transactional replication, and merge replication.

Custom agent profiles are added with the default agent parameter values. Use [sp_change_agent_parameter (Transact-SQL)](sp-change-agent-parameter-transact-sql.md) to change these default values or [sp_add_agent_parameter (Transact-SQL)](sp-add-agent-parameter-transact-sql.md) to add additional parameters.

When `sp_add_agent_profile` is executed, a row is added for the new custom profile in the [MSagent_profiles (Transact-SQL)](../system-tables/msagent-profiles-transact-sql.md) table and the associated default parameters for this profile are added to the [MSagent_parameters (Transact-SQL)](../system-tables/msagent-parameters-transact-sql.md) table.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_add_agent_profile`.

## Related content

- [Work with Replication Agent Profiles](../replication/agents/work-with-replication-agent-profiles.md)
- [Replication Agent Profiles](../replication/agents/replication-agent-profiles.md)
- [sp_add_agent_parameter (Transact-SQL)](sp-add-agent-parameter-transact-sql.md)
- [sp_change_agent_parameter (Transact-SQL)](sp-change-agent-parameter-transact-sql.md)
- [sp_change_agent_profile (Transact-SQL)](sp-change-agent-profile-transact-sql.md)
- [sp_drop_agent_parameter (Transact-SQL)](sp-drop-agent-parameter-transact-sql.md)
- [sp_drop_agent_profile (Transact-SQL)](sp-drop-agent-profile-transact-sql.md)
- [sp_help_agent_parameter (Transact-SQL)](sp-help-agent-parameter-transact-sql.md)
- [sp_help_agent_profile (Transact-SQL)](sp-help-agent-profile-transact-sql.md)
