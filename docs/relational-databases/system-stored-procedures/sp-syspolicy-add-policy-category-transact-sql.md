---
title: "sp_syspolicy_add_policy_category (Transact-SQL)"
description: "Adds a policy category that can be used with Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_add_policy_category"
  - "sp_syspolicy_add_policy_category_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_add_policy_category"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_add_policy_category (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a policy category that can be used with Policy-Based Management. Policy categories enable you to organize policies, and to set policy scope.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_add_policy_category
    [ @name = ] N'name'
    [ , [ @mandate_database_subscriptions = ] mandate_database_subscriptions ]
      , [ @policy_category_id = ] policy_category_id OUTPUT
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the policy category. *@name* is **sysname**, and is required. *@name* can't be NULL or an empty string.

#### [ @mandate_database_subscriptions = ] *mandate_database_subscriptions*

Determines whether database subscription is mandated for the policy category. *@mandate_database_subscriptions* is **bit** value, with a default of `1` (enabled).

#### [ @policy_category_id = ] *policy_category_id*

The identifier for the policy category. *@policy_category_id* is **int**, and is returned as `OUTPUT`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_add_policy_category` in the context of the `msdb` system database.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example creates a policy category where subscription to the category isn't mandated. This means that individual databases can be configured to opt in or opt out of policies in the category.

```sql
DECLARE @policy_category_id INT;

EXEC msdb.dbo.sp_syspolicy_add_policy_category
    @name = N'Table Naming Policies',
    @mandate_database_subscriptions = 0,
    @policy_category_id = @policy_category_id OUTPUT;
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_add_policy_category_subscription (Transact-SQL)](sp-syspolicy-add-policy-category-subscription-transact-sql.md)
- [sp_syspolicy_delete_policy_category (Transact-SQL)](sp-syspolicy-delete-policy-category-transact-sql.md)
