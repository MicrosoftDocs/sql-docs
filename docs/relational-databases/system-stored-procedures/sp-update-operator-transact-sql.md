---
title: "sp_update_operator (Transact-SQL)"
description: Updates information about an operator (notification recipient) for use with alerts and jobs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_operator_TSQL"
  - "sp_update_operator"
helpviewer_keywords:
  - "sp_update_operator"
dev_langs:
  - "TSQL"
---
# sp_update_operator (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates information about an operator (notification recipient) for use with alerts and jobs.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_operator
    [ @name = ] N'name'
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @enabled = ] enabled ]
    [ , [ @email_address = ] N'email_address' ]
    [ , [ @pager_address = ] N'pager_address' ]
    [ , [ @weekday_pager_start_time = ] weekday_pager_start_time ]
    [ , [ @weekday_pager_end_time = ] weekday_pager_end_time ]
    [ , [ @saturday_pager_start_time = ] saturday_pager_start_time ]
    [ , [ @saturday_pager_end_time = ] saturday_pager_end_time ]
    [ , [ @sunday_pager_start_time = ] sunday_pager_start_time ]
    [ , [ @sunday_pager_end_time = ] sunday_pager_end_time ]
    [ , [ @pager_days = ] pager_days ]
    [ , [ @netsend_address = ] N'netsend_address' ]
    [ , [ @category_name = ] N'category_name' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the operator to modify. *@name* is **sysname**, with no default.

#### [ @new_name = ] N'*new_name*'

The new name for the operator. This name must be unique. *@new_name* is **sysname**, with a default of `NULL`.

#### [ @enabled = ] *enabled*

A number indicating the operator's current status (`1` if currently enabled, `0` if not). *@enabled* is **tinyint**, with a default of `NULL`. If not enabled, an operator doesn't receive alert notifications.

#### [ @email_address = ] N'*email_address*'

The e-mail address of the operator. *@email_address* is **nvarchar(100)**, with a default of `NULL`. This string is passed directly to the e-mail system.

#### [ @pager_address = ] N'*pager_address*'

The pager address of the operator. *@pager_address* is **nvarchar(100)**, with a default of `NULL`. This string is passed directly to the e-mail system.

#### [ @weekday_pager_start_time = ] *weekday_pager_start_time*

Specifies the time after which a pager notification can be sent to this operator, from Monday through Friday. *@weekday_pager_start_time* is **int**, with a default of `NULL`, and must be entered in the form `HHmmss` for use with a 24-hour clock.

#### [ @weekday_pager_end_time = ] *weekday_pager_end_time*

Specifies the time after which a pager notification can't be sent to the specified operator, from Monday through Friday. *@weekday_pager_end_time* is **int**, with a default of `NULL`, and must be entered in the form `HHmmss` for use with a 24-hour clock.

#### [ @saturday_pager_start_time = ] *saturday_pager_start_time*

Specifies the time after which a pager notification can be sent to the specified operator on Saturdays. *@saturday_pager_start_time* is **int**, with a default of `NULL`, and must be entered in the form `HHmmss` for use with a 24-hour clock.

#### [ @saturday_pager_end_time = ] *saturday_pager_end_time*

Specifies the time after which a pager notification can't be sent to the specified operator on Saturdays. *@saturday_pager_end_time* is **int**, with a default of `NULL`, and must be entered in the form `HHmmss` for use with a 24-hour clock.

#### [ @sunday_pager_start_time = ] *sunday_pager_start_time*

Specifies the time after which a pager notification can be sent to the specified operator on Sundays. *@sunday_pager_start_time* is **int**, with a default of `NULL`, and must be entered in the form `HHmmss` for use with a 24-hour clock.

#### [ @sunday_pager_end_time = ] *sunday_pager_end_time*

Specifies the time after which a pager notification can't be sent to the specified operator on Sundays. *@sunday_pager_end_time* is **int**, with a default of `NULL`, and must be entered in the form `HHmmss` for use with a 24-hour clock.

#### [ @pager_days = ] *pager_days*

Specifies the days that the operator is available to receive pages (subject to the specified start/end times). *@pager_days* is **tinyint**, with a default of `NULL`, and must be a value from `0` through `127`. *@pager_days* is calculated by adding the individual values for the required days. For example, from Monday through Friday is `2` + `4` + `8` + `16` + `32` = `64`.

| Value | Description |
| --- | --- |
| `1` | Sunday |
| `2` | Monday |
| `4` | Tuesday |
| `8` | Wednesday |
| `16` | Thursday |
| `32` | Friday |
| `64` | Saturday |

#### [ @netsend_address = ] N'*netsend_address*'

The network address of the operator to whom the network message is sent. *@netsend_address* is **nvarchar(100)**, with a default of `NULL`.

#### [ @category_name = ] N'*category_name*'

The name of the category for this alert. *@category_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_update_operator` must be run from the `msdb` database.

## Permissions

Permissions to execute this procedure default to members of the **sysadmin** fixed server role.

## Examples

The following example updates the operator status, setting `@enabled` to `true`, and sets the days (from Monday through Friday, from 8 A.M. through 5 P.M.) when the operator can be paged.

```sql
USE msdb;
GO

EXEC dbo.sp_update_operator
    @name = N'François Ajenstat',
    @enabled = 1,
    @email_address = N'françoisa',
    @pager_address = N'5551290AW@pager.Adventure-Works.com',
    @weekday_pager_start_time = 080000,
    @weekday_pager_end_time = 170000,
    @pager_days = 64;
GO
```

## Related content

- [sp_add_operator (Transact-SQL)](sp-add-operator-transact-sql.md)
- [sp_delete_operator (Transact-SQL)](sp-delete-operator-transact-sql.md)
- [sp_help_operator (Transact-SQL)](sp-help-operator-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
