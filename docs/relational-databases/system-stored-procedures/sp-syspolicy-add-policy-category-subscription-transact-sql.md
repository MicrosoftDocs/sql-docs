---
title: "sp_syspolicy_add_policy_category_subscription (Transact-SQL)"
description: "Adds a policy category subscription to the specified database."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_add_policy_category_subscription"
  - "sp_syspolicy_add_policy_category_subscription_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_add_policy_category_subscription"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_add_policy_category_subscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a policy category subscription to the specified database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_add_policy_category_subscription
    [ @target_type = ] N'target_type'
      , [ @target_object = ] N'target_object'
      , [ @policy_category = ] N'policy_category'
    [ , [ @policy_category_subscription_id = ] policy_category_subscription_id OUTPUT ]
[ ; ]
```

## Arguments

#### [ @target_type = ] N'*target_type*'

The target type of the category subscription. *@target_type* is **sysname**, is required, and must be set to `DATABASE`.

#### [ @target_object = ] N'*target_object*'

The name of the database that will subscribe to the category. *@target_object* is **sysname**, and is required.

#### [ @policy_category = ] N'*policy_category*'

The name of the policy category to subscribe to. *@policy_category* is **sysname**, and is required.

To obtain values for *@policy_category*, query the `msdb.dbo.syspolicy_policy_categories` system view.

#### [ @policy_category_subscription_id = ] *policy_category_subscription_id*

The identifier for the category subscription. *@policy_category_subscription_id* is **int**, and is returned as `OUTPUT`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_add_policy_category_subscription` in the context of the `msdb` system database.

If you specify a policy category that doesn't exist, a new policy category is created and the subscription is mandated for all databases when you execute the stored procedure. If you then clear the mandated subscription for the new category, the subscription only applies for the database that you specified as the *target_object*. For more information about how to change a mandated subscription setting, see [sp_syspolicy_update_policy_category](sp-syspolicy-update-policy-category-transact-sql.md).

## Permissions

This stored procedure runs in the context of the current owner of the stored procedure.

## Examples

The following example configures the specified database to subscribe to a policy category that is named `Table Naming Policies`.

```sql
EXEC msdb.dbo.sp_syspolicy_add_policy_category_subscription
    @target_type = N'DATABASE',
    @target_object = N'AdventureWorks2022',
    @policy_category = N'Table Naming Policies';
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_update_policy_category_subscription (Transact-SQL)](sp-syspolicy-update-policy-category-subscription-transact-sql.md)
- [sp_syspolicy_unsubscribe_from_policy_category (Transact-SQL)](sp-syspolicy-unsubscribe-from-policy-category-transact-sql.md)
