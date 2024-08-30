---
title: "sp_syspolicy_configure (Transact-SQL)"
description: "Configures settings for Policy-Based Management, such as whether Policy-Based Management is enabled."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syspolicy_configure"
  - "sp_syspolicy_configure_TSQL"
helpviewer_keywords:
  - "sp_syspolicy_configure"
dev_langs:
  - "TSQL"
---
# sp_syspolicy_configure (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Configures settings for Policy-Based Management, such as whether Policy-Based Management is enabled.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syspolicy_configure
    [ @name = ] N'name'
    , [ @value = ] value
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the setting that you want to configure. *@name* is **sysname**, is required, and can't be `NULL` or an empty string.

*@name* can be any of the following values:

- `Enabled` - Determines whether Policy-Based Management is enabled.

- `HistoryRetentionInDays` - Specifies the number of days that policy evaluation history should be retained. If set to `0`, the history isn't automatically removed.

- `LogOnSuccess` - Specifies whether Policy-Based Management logs successful policy evaluations.

#### [ @value = ] *value*

The value that is associated with the specified value for *@name*. *@value* is **sql_variant**, and is required.

- If you specify 'Enabled' for *@name*, you can use either of the following values:

  - `0` - Disables Policy-Based Management.
  - `1` - Enables Policy-Based Management.

- If you specify `HistoryRententionInDays` for *@name*, specify the number of days as an integer value.

- If you specify `LogOnSuccess` for *@name*, you can use either of the following values:

  - `0` - Logs only failed policy evaluations.
  - `1` - Logs both successful and failed policy evaluations.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must run `sp_syspolicy_configure` in the context of the `msdb` system database.

To view current values for these settings, query the `msdb.dbo.syspolicy_configuration` system view.

## Permissions

Requires membership in the **PolicyAdministratorRole** fixed database role.

[!INCLUDE [policy-administrator-role](includes/policy-administrator-role.md)]

## Examples

The following example enables Policy-Based Management.

```sql
EXEC msdb.dbo.sp_syspolicy_configure
    @name = N'Enabled',
    @value = 1;
GO
```

The following example sets the policy history retention to 14 days.

```sql
EXEC msdb.dbo.sp_syspolicy_configure
    @name = N'HistoryRetentionInDays',
    @value = 14;
GO
```

The following example configures Policy-Based Management to log both successful and failed policy evaluations.

```sql
EXEC msdb.dbo.sp_syspolicy_configure
    @name = N'LogOnSuccess',
    @value = 1;
GO
```

## Related content

- [Policy-Based Management stored procedures (Transact-SQL)](policy-based-management-stored-procedures-transact-sql.md)
- [sp_syspolicy_set_config_enabled (Transact-SQL)](sp-syspolicy-set-config-enabled-transact-sql.md)
- [sp_syspolicy_set_config_history_retention (Transact-SQL)](sp-syspolicy-set-config-history-retention-transact-sql.md)
- [sp_syspolicy_set_log_on_success (Transact-SQL)](sp-syspolicy-set-log-on-success-transact-sql.md)
