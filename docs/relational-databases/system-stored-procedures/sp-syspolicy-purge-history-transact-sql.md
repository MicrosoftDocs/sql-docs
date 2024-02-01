---
title: "sp_syspolicy_purge_history (Transact-SQL)"
description: "Removes the policy evaluation history according to the history retention interval setting."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_purge_history_TSQL"
  - "sp_syspolicy_purge_history"
helpviewer_keywords:
  - "sp_syspolicy_purge_history"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_purge_history (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the policy evaluation history according to the history retention interval setting.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_purge_history
[ ; ]
```

## Arguments

This stored procedure has no parameters.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_purge_history` in the context of the `msdb` system database.

To view the history retention interval, you can use the following query:

```sql
SELECT current_value
FROM msdb.dbo.syspolicy_configuration
WHERE name = N'HistoryRetentionInDays';
GO
```

If the history retention interval is set to `0`, policy evaluation history isn't removed.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example removes the policy evaluation history.

```sql
EXEC msdb.dbo.sp_syspolicy_purge_history;
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_set_config_history_retention (Transact-SQL)](sp-syspolicy-set-config-history-retention-transact-sql.md)
- [sp_syspolicy_delete_policy_execution_history (Transact-SQL)](sp-syspolicy-delete-policy-execution-history-transact-sql.md)
