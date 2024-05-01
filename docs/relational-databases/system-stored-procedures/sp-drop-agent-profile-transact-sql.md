---
title: "sp_drop_agent_profile (Transact-SQL)"
description: Drops a profile from the MSagent_profiles table. This stored procedure is executed at the Distributor on any database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_drop_agent_profile"
  - "sp_drop_agent_profile_TSQL"
helpviewer_keywords:
  - "sp_drop_agent_profile"
dev_langs:
  - "TSQL"
---
# sp_drop_agent_profile (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a profile from the `MSagent_profiles` table. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_drop_agent_profile [ @profile_id = ] profile_id
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID of the profile to be dropped. *@profile_id* is **int**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_drop_agent_profile` is used in all types of replication.

The parameters of the given profile are also dropped from the `MSagent_parameters` table.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_drop_agent_profile`.

## Related content

- [sp_add_agent_profile (Transact-SQL)](sp-add-agent-profile-transact-sql.md)
- [sp_change_agent_profile (Transact-SQL)](sp-change-agent-profile-transact-sql.md)
- [sp_help_agent_profile (Transact-SQL)](sp-help-agent-profile-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
