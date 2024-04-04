---
title: "sysmail_delete_log_sp (Transact-SQL)"
description: "Deletes events from the Database Mail log."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_delete_log_sp_TSQL"
  - "sysmail_delete_log_sp"
helpviewer_keywords:
  - "sysmail_delete_log_sp"
dev_langs:
  - "TSQL"
---
# sysmail_delete_log_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes events from the Database Mail log. Deletes all events in the log or those events meeting a date or type criteria.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_delete_log_sp [ [ @logged_before = ] 'logged_before' ]
    [ , [ @event_type = ] 'event_type' ]
[ ; ]
```

## Arguments

#### [ @logged_before = ] '*logged_before*'

Deletes entries up to the date and time specified by the *@logged_before* argument. *@logged_before* is **datetime** with NULL as default. NULL indicates all dates.

#### [ @event_type = ] '*event_type*'

Deletes log entries of the type specified as the *@event_type*. *@event_type* is **varchar(15)** with no default. Valid entries are:

- `success`
- `warning`
- `error`
- `informational`

NULL indicates all event types.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Use the `sysmail_delete_log_sp` stored procedure to permanently delete entries from the Database Mail log. An optional argument allows you to delete only the older records by providing a date and time. Events older than that argument will be deleted. An optional argument allows you to delete only events of a certain type, specified as the *@event_type* argument.

Deleting entries in the Database Mail log doesn't delete the e-mails entries from the Database Mail tables. Use [sysmail_delete_mailitems_sp](sysmail-delete-mailitems-sp-transact-sql.md) to delete e-mail from the Database Mail tables.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. Delete all events

The following example deletes all events in the Database Mail log.

```sql
EXEC msdb.dbo.sysmail_delete_log_sp;
GO
```

### B. Delete the oldest events

The following example deletes events in the Database Mail log that are older than October 9, 2022.

```sql
EXEC msdb.dbo.sysmail_delete_log_sp
    @logged_before = 'October 9, 2022';
GO
```

### C. Delete all events of a certain type

The following example deletes success messages in the Database Mail log.

```sql
EXEC msdb.dbo.sysmail_delete_log_sp
    @event_type = 'success' ;
GO
```

## Related content

- [sysmail_event_log (Transact-SQL)](../system-catalog-views/sysmail-event-log-transact-sql.md)
- [sysmail_delete_mailitems_sp (Transact-SQL)](sysmail-delete-mailitems-sp-transact-sql.md)
- [Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../database-mail/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md)
