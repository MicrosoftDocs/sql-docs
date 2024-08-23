---
title: "sp_syspolicy_delete_policy_execution_history (Transact-SQL)"
description: "Deletes execution history for policies in Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_delete_policy_execution_history"
  - "sp_syspolicy_delete_policy_execution_history_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_delete_policy_execution_history"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_delete_policy_execution_history (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes execution history for policies in Policy-Based Management. You can use this stored procedure to delete execution history for a specific policy or for all policies, and to delete execution history before a specific date.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_delete_policy_execution_history
    [ @policy_id = ] policy_id
    , [ @oldest_date = ] 'oldest_date'
[ ; ]
```

## Arguments

#### [ @policy_id = ] *policy_id*

The identifier of the policy for which you want to delete the execution history. *@policy_id* is **int**, and is required. Can be `NULL`.

#### [ @oldest_date = ] '*oldest_date*'

The oldest date for which you want to keep policy execution history. Any execution history earlier than this date is deleted. *@oldest_date* is **datetime**, and is required. Can be `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_delete_policy_execution_history` in the context of the `msdb` system database.

To obtain values for *@policy_id*, and to view execution history dates, you can use the following query:

```sql
SELECT a.name AS N'policy_name',
    b.policy_id,
    b.start_date,
    b.end_date
FROM msdb.dbo.syspolicy_policies AS a
INNER JOIN msdb.dbo.syspolicy_policy_execution_history AS b
    ON a.policy_id = b.policy_id;
```

The following behavior applies if you specify `NULL` for one or both values:

- To delete all policy execution history, specify `NULL` for both *@policy_id* and for *@oldest_date*.

- To delete all policy execution history for a specific policy, specify a policy identifier for *@policy_id*, and specify `NULL` as *@oldest_date*.

- To delete policy execution history for all policies before a specific date, specify `NULL` for *@policy_id*, and specify a date for *@oldest_date*.

To archive policy execution history, you can open the Policy History log, in Object Explorer, and export the execution history to a file. To access the Policy History log, expand **Management**, right-click **Policy Management**, and then select **View History**.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example deletes policy execution history before a specific date for a policy with an ID of `7`.

```sql
EXEC msdb.dbo.sp_syspolicy_delete_policy_execution_history
    @policy_id = 7,
    @oldest_date = '2019-02-16 16:00:00.000';
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_set_config_history_retention (Transact-SQL)](sp-syspolicy-set-config-history-retention-transact-sql.md)
- [sp_syspolicy_purge_history (Transact-SQL)](sp-syspolicy-purge-history-transact-sql.md)
