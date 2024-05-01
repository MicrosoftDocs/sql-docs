---
title: "sp_drop_agent_parameter (Transact-SQL)"
description: sp_drop_agent_parameter drops one or all parameters from a profile in the MSagent_parameters table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_drop_agent_parameter_TSQL"
  - "sp_drop_agent_parameter"
helpviewer_keywords:
  - "sp_drop_agent_parameter"
dev_langs:
  - "TSQL"
---
# sp_drop_agent_parameter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops one or all parameters from a profile in the `MSagent_parameters` table. This stored procedure is executed at the Distributor where the agent is running, on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_drop_agent_parameter
    [ @profile_id = ] profile_id
    [ , [ @parameter_name = ] N'parameter_name' ]
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID of the profile for which a parameter is to be dropped. *@profile_id* is **int**, with no default.

#### [ @parameter_name = ] N'*parameter_name*'

The name of the parameter to be dropped. *@parameter_name* is **sysname**, with a default of `%`. If `%`, all parameters for the specified profile are dropped.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_drop_agent_parameter` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_drop_agent_parameter`.

## Related content

- [sp_add_agent_parameter (Transact-SQL)](sp-add-agent-parameter-transact-sql.md)
- [sp_help_agent_parameter (Transact-SQL)](sp-help-agent-parameter-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
