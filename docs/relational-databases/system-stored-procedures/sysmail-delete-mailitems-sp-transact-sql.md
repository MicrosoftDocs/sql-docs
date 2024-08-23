---
title: "sysmail_delete_mailitems_sp (Transact-SQL)"
description: "Permanently deletes e-mail messages from the Database Mail internal tables."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_delete_mailitems_sp_TSQL"
  - "sysmail_delete_mailitems_sp"
helpviewer_keywords:
  - "sysmail_delete_mailitems_sp"
dev_langs:
  - "TSQL"
---
# sysmail_delete_mailitems_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Permanently deletes e-mail messages from the Database Mail internal tables.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_delete_mailitems_sp [ [ @sent_before = ] 'sent_before' ]
    [ , [ @sent_status = ] 'sent_status' ]
[ ; ]
```

## Arguments

#### [ @sent_before = ] '*sent_before*'

Deletes e-mails up to the date and time provided as the *@sent_before* argument. *@sent_before* is **datetime** with `NULL` as default. `NULL` indicates all dates.

#### [ @sent_status = ] '*sent_status*'

Deletes e-mails of the type specified by *@sent_status*. *@sent_status* is **varchar(8)** with no default. Valid entries are:

- `sent`
- `unsent`
- `retrying`
- `failed`.

NULL indicates all statuses.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Database Mail messages and their attachments are stored in the `msdb` database. Messages should be periodically deleted to prevent `msdb` from growing larger than expected and to comply with your organizations document retention program. Use the `sysmail_delete_mailitems_sp` stored procedure to permanently delete e-mail messages from the Database Mail tables. An optional argument allows you to delete only older e-mails by providing a date and time. E-mails older than that argument will be deleted. Another optional argument allows you to delete only e-mails of a certain type, specified as the *@sent_status* argument. You must provide an argument either for *@sent_before* or *@sent_status*. To delete all messages, use `@sent_before = GETDATE();`.

Deleting e-mail also deletes attachments related to those messages. Deleting e-mail doesn't delete the corresponding entries in `sysmail_event_log`. Use [sysmail_delete_log_sp](sysmail-delete-log-sp-transact-sql.md) to delete items from the log.

## Permissions

By default, this stored procedure is granted for execution to members off the **sysadmin** fixed server role and **DatabaseMailUserRole**. Members of the **sysadmin** fixed server role can execute this procedure to delete e-mails sent by all users. Members of **DatabaseMailUserRole** can only delete e-mails sent by that user.

## Examples

### A. Delete all e-mails

The following example deletes all e-mails in the Database Mail system.

```sql
DECLARE @GETDATE DATETIME;

SET @GETDATE = GETDATE();

EXECUTE msdb.dbo.sysmail_delete_mailitems_sp @sent_before = @GETDATE;
GO
```

### B. Delete the oldest e-mails

The following example deletes e-mails in the Database Mail log that are older than October 9, 2022.

```sql
EXEC msdb.dbo.sysmail_delete_mailitems_sp
    @sent_before = 'October 9, 2022';
GO
```

### C. Delete all e-mails of a certain type

The following example deletes all failed e-mails in the Database Mail log.

```sql
EXEC msdb.dbo.sysmail_delete_mailitems_sp
    @sent_status = 'failed';
GO
```

## Related content

- [sysmail_allitems (Transact-SQL)](../system-catalog-views/sysmail-allitems-transact-sql.md)
- [sysmail_event_log (Transact-SQL)](../system-catalog-views/sysmail-event-log-transact-sql.md)
- [sysmail_mailattachments (Transact-SQL)](../system-catalog-views/sysmail-mailattachments-transact-sql.md)
- [Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../database-mail/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md)
