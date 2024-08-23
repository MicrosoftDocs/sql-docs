---
title: "sp_syspolicy_rename_condition (Transact-SQL)"
description: "Renames an existing condition in Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_rename_condition"
  - "sp_syspolicy_rename_condition_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_rename_condition"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_rename_condition (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Renames an existing condition in Policy-Based Management.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_rename_condition
    { [ @name = ] N'name' | [ @condition_id = ] condition_id }
    , [ @new_name = ] N'new_name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the condition that you want to rename. *@name* is **sysname**, and must be specified if *@condition_id* is `NULL`.

#### [ @condition_id = ] *condition_id*

The identifier for the condition that you want to rename. *@condition_id* is **int**, and must be specified if *@name* is `NULL`.

#### [ @new_name = ] N'*new_name*'

The new name of the condition. *@new_name* is **sysname**, and is required. Can't be `NULL` or an empty string.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_rename_condition` in the context of the `msdb` system database.

You must specify a value for either *@name* or *@condition_id*. Both can't be `NULL`. To obtain these values, query the `msdb.dbo.syspolicy_conditions` system view.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example renames a condition that is named `Change Tracking Enabled`.

```sql
EXEC msdb.dbo.sp_syspolicy_rename_condition
    @name = N'Change Tracking Enabled',
    @new_name = N'Verify Change Tracking Enabled';
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
