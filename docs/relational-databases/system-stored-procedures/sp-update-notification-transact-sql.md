---
title: "sp_update_notification (Transact-SQL)"
description: Updates the notification method of an alert notification.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_notification_TSQL"
  - "sp_update_notification"
helpviewer_keywords:
  - "sp_updatenotification"
dev_langs:
  - "TSQL"
---
# sp_update_notification (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates the notification method of an alert notification.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_notification
    [ @alert_name = ] N'alert_name'
    , [ @operator_name = ] N'operator_name'
    , [ @notification_method = ] notification_method
[ ; ]
```

## Arguments

#### [ @alert_name = ] N'*alert_name*'

The name of the alert associated with this notification. *@alert_name* is **sysname**, with no default.

#### [ @operator_name = ] N'*operator_name*'

The operator who is notified when the alert occurs. *@operator_name* is **sysname**, with no default.

#### [ @notification_method = ] *notification_method*

The method by which the operator is notified. *@notification_method* is **tinyint**, and can be one or more of these values.

| Value | Description |
| --- | --- |
| `1` | E-mail |
| `2` | Pager |
| `4` | `net send` |
| `7` | All methods |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_update_notification` must be run from the `msdb` database.

You can update a notification for an operator who doesn't have the necessary address information using the specified *notification_method*. If a failure occurs when sending an e-mail message or pager notification, the failure is reported in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent error log.

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Examples

The following example modifies the notification method for notifications sent to `François Ajenstat`for the alert `Test Alert`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_notification
   @alert_name = N'Test Alert',
   @operator_name = N'François Ajenstat',
   @notification_method = 7;
GO
```

## Related content

- [sp_add_notification (Transact-SQL)](sp-add-notification-transact-sql.md)
- [sp_delete_notification (Transact-SQL)](sp-delete-notification-transact-sql.md)
- [sp_help_notification (Transact-SQL)](sp-help-notification-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
