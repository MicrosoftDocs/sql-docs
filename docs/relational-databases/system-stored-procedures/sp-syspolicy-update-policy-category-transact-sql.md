---
title: "sp_syspolicy_update_policy_category (Transact-SQL)"
description: "Updates whether a policy category is set to mandate database subscriptions."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_update_policy_category_TSQL"
  - "sp_syspolicy_update_policy_category"
helpviewer_keywords:
  - "sp_syspolicy_update_policy_category"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_update_policy_category (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates whether a policy category is set to mandate database subscriptions. If subscription is mandated, the policy category applies to all databases.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_update_policy_category
    { [ @name = ] N'name' | [ @policy_category_id = ] policy_category_id }
    [ , [ @mandate_database_subscriptions = ] mandate_database_subscriptions ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the policy category. *@name* is **sysname**, and must be specified if *@policy_category_id* is `NULL`.

#### [ @policy_category_id = ] *policy_category_id*

The identifier for the policy category. *@policy_category_id* is **int**, and must be specified if *@name* is `NULL`.

#### [ @mandate_database_subscriptions = ] *mandate_database_subscriptions*

Determines whether database subscription is mandated for the policy category. *@mandate_database_subscriptions* is **bit** value, with a default of `NULL`. You can use either of the following values:

- `0` - Not mandated
- `1` - Mandated

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_update_policy_category` in the context of the `msdb` system database.

You must specify a value for either *@name* or for *@policy_category_id*. Both can't be `NULL`. To obtain these values, query the `msdb.dbo.syspolicy_policy_categories` system view.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example updates the `Finance` category to mandate database subscriptions.

```sql
EXEC msdb.dbo.sp_syspolicy_update_policy_category
    @name = N'Finance',
    @mandate_database_subscriptions = 1;
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_add_policy_category (Transact-SQL)](sp-syspolicy-add-policy-category-transact-sql.md)
- [sp_syspolicy_delete_policy_category (Transact-SQL)](sp-syspolicy-delete-policy-category-transact-sql.md)
- [sp_syspolicy_rename_policy_category (Transact-SQL)](sp-syspolicy-rename-policy-category-transact-sql.md)
