---
title: "sp_syspolicy_set_log_on_success (Transact-SQL)"
description: "Specifies whether successful policy evaluations are logged in the Policy History log for Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_set_log_on_success_TSQL"
  - "sp_syspolicy_set_log_on_success"
helpviewer_keywords:
  - "sp_syspolicy_set_log_on_success"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_set_log_on_success (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies whether successful policy evaluations are logged in the Policy History log for Policy-Based Management.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_set_log_on_success
    [ @value = ] value
[ ; ]
```

## Arguments

#### [ @value = ] *value*

Determines whether successful policy evaluations are logged. *@value* is **sqlvariant**, and can be one of the following values:

- `0` or `false` - Successful policy evaluations aren't logged.
- `1` or `true` - Successful policy evaluations are logged.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_set_log_on_success` in the context of the `msdb` system database.

When *@value* is set to `0` or to `false`, only failed policy evaluations are logged.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example enables the logging of successful policy evaluations.

```sql
EXEC msdb.dbo.sp_syspolicy_set_log_on_success @value = 1;
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_configure (Transact-SQL)](sp-syspolicy-configure-transact-sql.md)
