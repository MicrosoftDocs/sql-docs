---
title: "sysmail_help_status_sp (Transact-SQL)"
description: "Displays the status of Database Mail queues."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_help_status_sp"
  - "sysmail_help_status_sp_TSQL"
helpviewer_keywords:
  - "sysmail_help_status_sp"
dev_langs:
  - "TSQL"
---
# sysmail_help_status_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays the status of Database Mail queues. Use `sysmail_start_sp` to start the Database Mail queues and `sysmail_stop_sp` to stop the Database Mail queues.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_help_status_sp
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Status` | **nvarchar(7)** | The status of the Database Mail. Possible values are `STARTED` and `STOPPED`. |

## Permissions

By default, only members of the **sysadmin** fixed server role can access this procedure.

## Examples

The following example displays the status of Database Mail.

```sql
EXEC msdb.dbo.sysmail_help_status_sp;
GO
```

Result set:

```output
Status
-------
STARTED
```

## Related content

- [Database Mail External Program](../database-mail/database-mail-external-program.md)
- [sysmail_start_sp (Transact-SQL)](sysmail-start-sp-transact-sql.md)
- [sysmail_stop_sp (Transact-SQL)](sysmail-stop-sp-transact-sql.md)
