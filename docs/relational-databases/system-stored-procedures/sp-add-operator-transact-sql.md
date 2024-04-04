---
title: "sp_add_operator (Transact-SQL)"
description: "Creates an operator (notification recipient) for use with alerts and jobs."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_operator"
  - "sp_add_operator_TSQL"
helpviewer_keywords:
  - "sp_add_operator"
dev_langs:
  - "TSQL"
---
# sp_add_operator (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates an operator (notification recipient) for use with alerts and jobs.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_operator
    [ @name = ] 'name'
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
    [ , [ @category_name = ] 'category' ]
[ ; ]
```

## Arguments

#### [ @name = ] '*name*'

The name of an operator (notification recipient). This name must be unique and can't contain the percent (`%`) character. *@name* is **sysname**, with no default.

#### [ @enabled = ] *enabled*

Indicates the current status of the operator. *@enabled* is **tinyint**, with a default of `1` (enabled). If `0`, the operator isn't enabled and doesn't receive notifications.

#### [ @email_address = ] N'*email_address*'

The e-mail address of the operator. This string is passed directly to the e-mail system. *@email_address* is **nvarchar(100)**, with a default of `NULL`.

You can specify either a physical e-mail address or an alias for *@email_address*. For example:

`fatmir.bregu` or `fatmir.bregu@contoso.com`

> [!NOTE]  
> You must use the e-mail address for Database Mail.

#### [ @pager_address = ] N'*pager_address*'

The pager address of the operator. This string is passed directly to the e-mail system. *@pager_address* is **nvarchar(100)**, with a default of `NULL`.

#### [ @weekday_pager_start_time = ] *weekday_pager_start_time*

The time after which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent sends pager notification to the specified operator on the weekdays, from Monday through Friday. *@weekday_pager_start_time* is **int**, with a default of `090000`, which indicates 9:00 A.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @weekday_pager_end_time = ] *weekday_pager_end_time*

The time after which [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service no longer sends pager notification to the specified operator on the weekdays, from Monday through Friday. *weekday_pager_end_time* is **int**, with a default of `180000`, which indicates 6:00 P.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @saturday_pager_start_time = ] *saturday_pager_start_time*

The time after which [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service sends pager notification to the specified operator on Saturdays. *saturday_pager_start_time* is **int**, with a default of `090000`, which indicates 9:00 A.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @saturday_pager_end_time = ] *saturday_pager_end_time*

The time after which [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service no longer sends pager notification to the specified operator on Saturdays. *@saturday_pager_end_time* is **int**, with a default of `180000`, which indicates 6:00 P.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @sunday_pager_start_time = ] *sunday_pager_start_time*

The time after which [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service sends pager notification to the specified operator on Sundays. *@sunday_pager_start_time* is **int**, with a default of `090000`, which indicates 9:00 A.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @sunday_pager_end_time = ] *sunday_pager_end_time*

The time after which [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service no longer sends pager notification to the specified operator on Sundays. *@sunday_pager_end_time* is **int**, with a default of `180000`, which indicates 6:00 P.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @pager_days = ] *pager_days*

A number that indicates the days that the operator is available for pages (subject to the specified start/end times). *@pager_days* is **tinyint**, with a default of `0` indicating the operator is never available to receive a page. Valid values are from `0` through `127`. *@pager_days* is calculated by adding the individual values for the required days. For example, from Monday through Friday is `2 + 4 + 8 + 16 + 32 = 62`. The following table lists the value for each day of the week.

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

#### [ @category_name = ] '*category*'

The name of the category for this operator. *@category_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_operator` must be run from the `msdb` database.

Your e-mail system must have an e-mail-to-pager capability if you want to use paging.

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example sets up the operator information for `danwi`. The operator is enabled. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent sends notifications by pager from Monday through Friday from 8 A.M. to 5 P.M.

```sql
USE msdb;
GO

EXEC dbo.sp_add_operator @name = N'Dan Wilson',
    @enabled = 1,
    @email_address = N'danwi',
    @pager_address = N'5551290AW@pager.adventure-works.com',
    @weekday_pager_start_time = 080000,
    @weekday_pager_end_time = 170000,
    @pager_days = 62;
GO
```

## Related content

- [sp_delete_operator (Transact-SQL)](sp-delete-operator-transact-sql.md)
- [sp_help_operator (Transact-SQL)](sp-help-operator-transact-sql.md)
- [sp_update_operator (Transact-SQL)](sp-update-operator-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
