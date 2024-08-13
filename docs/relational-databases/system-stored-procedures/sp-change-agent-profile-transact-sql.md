---
title: "sp_change_agent_profile (Transact-SQL)"
description: Changes a parameter of a replication agent profile stored in the MSagent_profiles table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_change_agent_profile"
  - "sp_change_agent_profile_TSQL"
helpviewer_keywords:
  - "sp_change_agent_profile"
dev_langs:
  - "TSQL"
---
# sp_change_agent_profile (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes a parameter of a replication agent profile stored in the [MSagent_profiles](../system-tables/msagent-profiles-transact-sql.md) table. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_change_agent_profile
    [ @profile_id = ] profile_id
    , [ @property = ] N'property'
    , [ @value = ] N'value'
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID of the profile. *@profile_id* is **int**, with no default.

#### [ @property = ] N'*property*'

The name of the property. *@property* is **sysname**, with no default.

#### [ @value = ] N'*value*'

The new value of the property. *@value* is **nvarchar(3000)**, with no default.

This table describes the profile properties that can be changed.

| Property | Description |
| --- | --- |
| `description` | Description for the profile. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_change_agent_profile` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_change_agent_profile`.

## Related content

- [sp_add_agent_profile (Transact-SQL)](sp-add-agent-profile-transact-sql.md)
- [sp_drop_agent_profile (Transact-SQL)](sp-drop-agent-profile-transact-sql.md)
- [sp_help_agent_profile (Transact-SQL)](sp-help-agent-profile-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
