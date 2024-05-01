---
title: "sp_syspolicy_set_config_enabled (Transact-SQL)"
description: "Enables or disables Policy-Based Management."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_set_config_enabled"
  - "sp_syspolicy_set_config_enabled_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_set_config_enabled"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_set_config_enabled (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Enables or disables Policy-Based Management.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_set_config_enabled [ @value = ] value
[ ; ]
```

## Arguments

#### [ @value = ] *value*

Determines whether Policy-Based Management is enabled. *@value* is **sqlvariant**, and can be one of the following values:

- 0 or `false` - Disabled
- 1 or `true` - Enabled

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_set_config_enabled` in the context of the `msdb` system database.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example enables Policy-Based Management.

```sql
EXEC msdb.dbo.sp_syspolicy_set_config_enabled @value = 1;

GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_configure (Transact-SQL)](sp-syspolicy-configure-transact-sql.md)
