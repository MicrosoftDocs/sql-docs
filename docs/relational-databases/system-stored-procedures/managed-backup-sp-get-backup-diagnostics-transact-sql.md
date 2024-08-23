---
title: "managed_backup.sp_get_backup_diagnostics (Transact-SQL)"
description: "Returns the Extended Events logged by Smart Admin."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_get_backup_diagnostics_TSQL"
  - "sp_get_backup_diagnostics"
  - "smart_admin.sp_get_backup_diagnostics_TSQL"
  - "smart_admin.sp_get_backup_diagnostics"
helpviewer_keywords:
  - "sp_get_backup_diagnostics"
  - "smart_admin.sp_get_backup_diagnostics"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_get_backup_diagnostics (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Returns the Extended Events logged by Smart Admin.

Use this stored procedure to monitor Extended Events logged by Smart Admin. [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] events are logged in this system, and can be reviewed and monitored using this stored procedure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
managed_backup.sp_get_backup_diagnostics
    [ [ @xevent_channel = ] 'event_type'
    [ , [ @begin_time = ] 'time1' ]
    [ , [ @end_time = ] 'time2' ] ]
[ ; ]
```

## Arguments

#### [ @xevent_channel = ] '*event_type*'

The type of Extended Event. The default value is set to return all events logged for the previous 30 minutes. The events logged depend on the type of Extended Events enabled. You can use this parameter to filter the stored procedure to show only events of a certain type. You can either specify the full event name or specify a substring such as: `Admin`, `Analytic`, `Operational`, and `Debug`. *@event_channel* is **varchar(255)**.

To get a list of event types currently enabled use the `managed_backup.fn_get_current_xevent_settings` function.

#### [ @begin_time = ] '*time1*'

The start of the time period from which the events should be displayed. *@begin_time* is **datetime** with a default value of `NULL`. If this isn't specified, then the events from the past 30 minutes are displayed.

#### [ @end_time = ] '*time2*'

The end of the time period up to which the events should be displayed. *@end_time* is **datetime** with a default value of `NULL`. If this isn't specified, then the events up to the current time is displayed.

## Table returned

This stored procedure returns a table with the following information:

| Column name | Data type | Description |
| --- | --- | --- |
| `event_type` | **nvarchar(512)** | Type of Extended Event |
| `Event` | **nvarchar(512)** | Summary of the event logs |
| `Timestamp` | **timestamp** | Timestamp of the event that shows when the event was raised |

## Permissions

Requires EXECUTE permissions on the stored procedure. It also requires VIEW SERVER STATE permissions since it internally calls other system objects that require this permission.

## Examples

The following example returns all the events logged for the past 30 minutes.

```sql
USE msdb;
GO
EXEC managed_backup.sp_get_backup_diagnostics;
```

The following example returns all the events logged for a specific time range.

```sql
USE msdb;
GO
EXEC managed_backup.sp_get_backup_diagnostics @xevent_channel = 'Admin',
  @begin_time = '2022-06-01', @end_time = '2022-06-10';
```

The following example returns all the analytical events logged for the past 30 minutes

```sql
USE msdb;
GO
EXEC managed_backup.sp_get_backup_diagnostics @xevent_channel = 'Analytic';
```
