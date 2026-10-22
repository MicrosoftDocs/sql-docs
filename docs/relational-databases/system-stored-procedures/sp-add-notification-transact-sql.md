---
title: "sp_add_notification (Transact-SQL)"
description: "Sets up a notification for an alert."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_notification_TSQL"
  - "sp_add_notification"
helpviewer_keywords:
  - "sp_add_notification"
dev_langs:
  - "TSQL"
---
# sp_add_notification (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets up a notification for an alert.

## Syntax

```syntaxsql
dbo.sp_add_notification
    [ @alert_name = ] N'alert_name'
    , [ @operator_name = ] N'operator_name'
    , [ @notification_method = ] notification_method
[ ; ]
```

## Arguments

#### [ @alert_name = ] N'*alert_name*'

The alert for this notification. *@alert_name* is **sysname**, with no default.

#### [ @operator_name = ] N'*operator_name*'

The operator to be notified when the alert occurs. *@operator_name* is **sysname**, with no default.

#### [ @notification_method = ] *notification_method*

The method by which the operator is notified. *@notification_method* is **tinyint**, with no default. *@notification_method* can be one or more of these values combined with an `OR` logical operator.

| Value | Description |
| --- | --- |
| `1` | E-mail |
| `2` | Pager |
| `4` | `net send` |

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_notification` must be run from the `msdb` database.

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage the entire alerting system. Using [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] is the recommended way to configure your alert infrastructure.

To send a notification in response to an alert, you must first configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to send mail.

If a failure occurs when sending an e-mail message or pager notification, the failure is reported in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service error log.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example adds an e-mail notification for the specified alert (`Test Alert`).

> [!NOTE]  
> This example assumes that `Test Alert` already exists and that `François Ajenstat` is a valid operator name.

```sql
USE msdb;
GO

EXECUTE dbo.sp_add_notification
    @alert_name = N'Test Alert',
    @operator_name = N'François Ajenstat',
    @notification_method = 1;
GO
```

## Related content

- [sp_delete_notification (Transact-SQL)](sp-delete-notification-transact-sql.md)
- [sp_help_notification (Transact-SQL)](sp-help-notification-transact-sql.md)
- [sp_update_notification (Transact-SQL)](sp-update-notification-transact-sql.md)
- [sp_add_operator (Transact-SQL)](sp-add-operator-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
