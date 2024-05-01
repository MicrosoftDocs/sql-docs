---
title: "sp_syspolicy_update_policy_category_subscription (Transact-SQL)"
description: "Updates a policy category subscription for a specified database."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_update_policy_category_subscription_TSQL"
  - "sp_syspolicy_update_policy_category_subscription"
helpviewer_keywords:
  - "sp_syspolicy_update_policy_category_subscription"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_update_policy_category_subscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates a policy category subscription for a specified database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_update_policy_category_subscription
    [ @policy_category_subscription_id = ] policy_category_subscription_id
    [ , [ @target_type = ] N'target_type' ]
    [ , [ @target_object = ] N'target_object' ]
      , [ @policy_category = ] N'policy_category'
[ ; ]
```

## Arguments

#### [ @policy_category_subscription_id = ] *policy_category_subscription_id*

The identifier for the policy category subscription that you want to update. *@policy_category_subscription_id* is **int**, and is required.

#### [ @target_type = ] N'*target_type*'

The target type of the category subscription. *@target_type* is **sysname**, with a default of `NULL`.

If you specify *@target_type*, the value must be set to `DATABASE`.

#### [ @target_object = ] N'*target_object*'

The name of the database that will subscribe to the policy category. *@target_object* is **sysname**, with a default of `NULL`.

#### [ @policy_category = ] N'*policy_category*'

The name of the policy category that you want the database to subscribe to. *@policy_category* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_update_policy_category_subscription` in the context of the `msdb` system database.

To obtain values for *@policy_category_subscription_id* and for *@policy_category*, you can use the following query:

```sql
SELECT a.policy_category_subscription_id,
    a.target_type,
    a.target_object,
    b.name AS policy_category
FROM msdb.dbo.syspolicy_policy_category_subscriptions AS a
INNER JOIN msdb.dbo.syspolicy_policy_categories AS b
    ON a.policy_category_id = b.policy_category_id;
```

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example updates an existing policy category subscription so that the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database subscribes to the `Finance` policy category.

```sql
EXEC msdb.dbo.sp_syspolicy_update_policy_category_subscription
    @policy_category_subscription_id = 1,
    @target_object = 'AdventureWorks2022',
    @policy_category = 'Finance';
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_add_policy_category_subscription (Transact-SQL)](sp-syspolicy-add-policy-category-subscription-transact-sql.md)
- [sp_syspolicy_delete_policy_category_subscription (Transact-SQL)](sp-syspolicy-delete-policy-category-subscription-transact-sql.md)
