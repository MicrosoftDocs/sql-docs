---
title: "sys.sp_getagentparameterlist (Transact-SQL)"
description: sp_getagentparameterlist returns a list of all replication agent parameters that can be set in an agent profile for the specified agent type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/19/2026
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_getagentparameterlist"
  - "sp_getagentparameterlist_TSQL"
helpviewer_keywords:
  - "sp_getagentparameterlist"
dev_langs:
  - "TSQL"
---
# sys.sp_getagentparameterlist (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of all replication agent parameters that can be set in an agent profile for the specified agent type. This stored procedure is executed at the Distributor where the agent is running, on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_getagentparameterlist [ @agent_type = ] agent_type
[ ; ]
```

## Arguments

#### [ @agent_type = ] *agent_type*

The replication agent for which the parameter is being added. *@agent_type* is **int**, and can be one of these values:

| Value | Agent |
| --- | --- |
| `1` | Snapshot |
| `2` | Log Reader |
| `3` | Distribution |
| `4` | Merge |
| `9` | Queue Reader |

## Return code values

`0` (success) or `1` (failure).

## Remarks

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_getagentparameter`.

## Related content

- [sys.sp_add_agent_parameter (Transact-SQL)](sp-add-agent-parameter-transact-sql.md)
- [sys.sp_add_agent_profile (Transact-SQL)](sp-add-agent-profile-transact-sql.md)
- [sys.sp_drop_agent_parameter (Transact-SQL)](sp-drop-agent-parameter-transact-sql.md)
- [sys.sp_help_agent_parameter (Transact-SQL)](sp-help-agent-parameter-transact-sql.md)
- [Replication Agent Profiles](../replication/agents/replication-agent-profiles.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
