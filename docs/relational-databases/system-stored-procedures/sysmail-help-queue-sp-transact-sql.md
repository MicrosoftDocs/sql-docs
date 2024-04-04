---
title: "sysmail_help_queue_sp (Transact-SQL)"
description: "Allows viewing the state of the mail or status queues."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_queue_sp"
  - "sysmail_help_queue_sp_TSQL"
helpviewer_keywords:
  - "sysmail_help_queue_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_queue_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

There are two queues in Database Mail: the mail queue and status queue. The mail queue stores mail items that are waiting to be sent. The status queue stores the status of items that have already been sent. This stored procedure allows viewing the state of the mail or status queues. If the parameter *@queue_type* isn't specified, the stored procedure returns one row for each of the queues.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_queue_sp [ @queue_type = ] 'queue_type'
[ ; ]
```

## Arguments

#### [ @queue_type = ] '*queue_type*'

Optional argument deletes e-mails of the type specified as the *@queue_type*. *@queue_type* is **nvarchar(6)** with no default. Valid entries are `mail` and *`status`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `queue_type` | **nvarchar(6)** | The type of queue. Possible values are `mail` and `status`. |
| `length` | **int** | The number of mail items in the specified queue. |
| `state` | **nvarchar(64)** | The state of the monitor. Possible values are `INACTIVE` (queue is inactive), `NOTIFIED` (queue has been notified receipt to occur), and `RECEIVES_OCCURRING` (queue is receiving). |
| `last_empty_rowset_time` | **datetime** | The date and time that the queue was last empty. In military time format and GMT time zone. |
| `last_activated_time` | **datetime** | The date and time the queue was last activated. In military time format and GMT time zone. |

## Remarks

When troubleshooting Database Mail, use `sysmail_help_queue_sp` to see how many items are in the queue, the status of the queue, and when it was last activated.

## Permissions

By default, only members of the **sysadmin** fixed server role can access this procedure.

## Examples

The following example returns both the mail and status queues.

```sql
EXEC msdb.dbo.sysmail_help_queue_sp;
GO
```

This is a sample result set has been edited for length.

```output
queue_type length      state              last_empty_rowset_time  last_activated_time
---------- -------- ------------------ ----------------------- -----------------------
mail       0        RECEIVES_OCCURRING 2005-10-07 21:14:47.010 2005-10-10 20:52:51.517
status     0        INACTIVE           2005-10-07 21:04:47.003 2005-10-10 21:04:47.003

(2 row(s) affected)
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
