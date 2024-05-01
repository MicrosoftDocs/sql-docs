---
title: "sp_delete_notification (Transact-SQL)"
description: Removes a SQL Server Agent notification definition for a specific alert and operator.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_notification_TSQL"
  - "sp_delete_notification"
helpviewer_keywords:
  - "sp_delete_notification"
dev_langs:
  - "TSQL"
---
# sp_delete_notification (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent notification definition for a specific alert and operator.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_notification
    [ @alert_name = ] N'alert_name'
    , [ @operator_name = ] N'operator_name'
[ ; ]
```

## Arguments

#### [ @alert_name = ] N'*alert_name*'

The name of the alert. *@alert_name* is **sysname**, with no default.

#### [ @operator_name = ] N'*operator_name*'

The name of the operator. *@operator_name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Removing a notification removes only the notification; the alert and the operator are left intact.

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Examples

The following example removes the notification sent to operator `François Ajenstat` when alert `Test Alert` occurs.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_notification
    @alert_name = 'Test Alert',
    @operator_name = 'François Ajenstat' ;
GO
```

## Related content

- [sp_add_alert (Transact-SQL)](sp-add-alert-transact-sql.md)
- [sp_add_notification (Transact-SQL)](sp-add-notification-transact-sql.md)
- [sp_add_operator (Transact-SQL)](sp-add-operator-transact-sql.md)
- [sp_delete_alert (Transact-SQL)](sp-delete-alert-transact-sql.md)
- [sp_help_alert (Transact-SQL)](sp-help-alert-transact-sql.md)
- [sp_help_notification (Transact-SQL)](sp-help-notification-transact-sql.md)
- [sp_help_operator (Transact-SQL)](sp-help-operator-transact-sql.md)
- [sp_update_notification (Transact-SQL)](sp-update-notification-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
