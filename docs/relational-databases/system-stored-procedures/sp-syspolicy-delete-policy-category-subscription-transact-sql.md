---
title: "sp_syspolicy_delete_policy_category_subscription (Transact-SQL)"
description: "Deletes a policy category subscription for a specific database."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_delete_policy_category_subscription_TSQL"
  - "sp_syspolicy_delete_policy_category_subscription"
helpviewer_keywords:
  - "sp_syspolicy_delete_policy_category_subscription"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_delete_policy_category_subscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes a policy category subscription for a specific database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_delete_policy_category_subscription
    [ @policy_category_subscription_id = ] policy_category_subscription_id
[ ; ]
```

## Arguments

#### [ @policy_category_subscription_id = ] *policy_category_subscription_id*

The identifier for the policy category subscription. *@policy_category_subscription_id* is **int**.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_delete_policy_category_subscription` in the context of the `msdb` system database.

You can't delete a policy category subscription when the subscription is mandated.

## Permissions

This stored procedure runs in the context of the current owner of the stored procedure.

To obtain values for *@policy_category_subscription_id*, you can use the following query:

```sql
SELECT a.policy_category_subscription_id,
    a.target_object,
    b.name AS category_name
FROM msdb.dbo.syspolicy_policy_category_subscriptions AS a
INNER JOIN msdb.dbo.syspolicy_policy_categories AS b
    ON a.policy_category_id = b.policy_category_id;
```

## Examples

The following example deletes a policy category subscription with an ID of 1.

```sql
EXEC msdb.dbo.sp_syspolicy_delete_policy_category_subscription
    @policy_category_subscription_id = 1;
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_update_policy_category_subscription (Transact-SQL)](sp-syspolicy-update-policy-category-subscription-transact-sql.md)
