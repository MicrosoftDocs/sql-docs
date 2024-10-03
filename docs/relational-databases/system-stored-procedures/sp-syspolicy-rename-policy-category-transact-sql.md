---
title: "sp_syspolicy_rename_policy_category (Transact-SQL)"
description: "Renames an existing policy category in Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_rename_policy_category_TSQL"
  - "sp_syspolicy_rename_policy_category"
helpviewer_keywords:
  - "sp_syspolicy_rename_policy_category"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_rename_policy_category (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Renames an existing policy category in Policy-Based Management.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_rename_policy_category
    { [ @name = ] N'name' | [ @policy_category_id = ] policy_category_id }
    , [ @new_name = ] N'new_name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the policy category that you want to rename. *@name* is **sysname**, and must be specified if *@policy_category_id* is `NULL`.

#### [ @policy_category_id = ] *policy_category_id*

The identifier for the policy category that you want to rename. *@policy_category_id* is **int**, and must be specified if *@name* is `NULL`.

#### [ @new_name = ] N'*new_name*'

The new name for the policy category. *@new_name* is **sysname**, and is required. Can't be `NULL` or an empty string.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_rename_policy_category` in the context of the `msdb` system database.

You must specify a value for either *@name* or *@policy_category_id*. Both can't be `NULL`. To obtain these values, query the `msdb.dbo.syspolicy_policy_categories` system view.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example renames a policy category that is named `Test Category 1` to `Test Category 2`.

```sql
EXEC msdb.dbo.sp_syspolicy_rename_policy_category
    @name = N'Test Category 1',
    @new_name = N'Test Category 2';
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_add_policy_category (Transact-SQL)](sp-syspolicy-add-policy-category-transact-sql.md)
- [sp_syspolicy_delete_policy_category (Transact-SQL)](sp-syspolicy-delete-policy-category-transact-sql.md)
- [sp_syspolicy_update_policy_category (Transact-SQL)](sp-syspolicy-update-policy-category-transact-sql.md)
