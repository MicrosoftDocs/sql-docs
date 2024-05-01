---
title: "sp_syspolicy_repair_policy_automation (Transact-SQL)"
description: "Repairs policy automation in Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_repair_policy_automation"
  - "sp_syspolicy_repair_policy_automation_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_repair_policy_automation"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_repair_policy_automation (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Repairs policy automation in Policy-Based Management. For example, you can use this stored procedure to repair triggers and jobs that are associated with policies that are configured to use "On schedule" or "On change" evaluation modes.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_repair_policy_automation
[ ; ]
```

## Arguments

This stored procedure has no parameters.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_repair_policy_automation` in the context of the `msdb` system database.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example repairs policy automation.

```sql
EXEC msdb.dbo.sp_syspolicy_repair_policy_automation;

GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
