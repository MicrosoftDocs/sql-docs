---
title: "sp_help_targetserver (Transact-SQL)"
description: sp_help_targetserver lists all target servers.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_targetserver_TSQL"
  - "sp_help_targetserver"
helpviewer_keywords:
  - "sp_help_targetserver"
dev_langs:
  - "TSQL"
---
# sp_help_targetserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists all target servers.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_targetserver [ [ @server_name = ] N'server_name' ]
[ ; ]
```

## Arguments

#### [ @server_name = ] N'*server_name*'

The name of the server for which to return information. *@server_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

If *@server_name* isn't specified, `sp_help_targetserver` returns this result set.

| Column name | Data type | Description |
| --- | --- | --- |
| `server_id` | **int** | Server identification number. |
| `server_name` | **nvarchar(30)** | Server name. |
| `location` | **nvarchar(200)** | Location of the specified server. |
| `time_zone_adjustment` | **int** | Time zone adjustment, in hours, from Greenwich mean time (GMT). |
| `enlist_date` | **datetime** | Date of the specified server's enlistment. |
| `last_poll_date` | **datetime** | Date the server was last polled for jobs. |
| `status` | **int** | Status of the specified server. |
| `unread_instructions` | **int** | Specifies whether the server has unread instructions. If all rows are downloaded, this column is `0`. |
| `local_time` | **datetime** | Local date and time on the target server, which is based on the local time on the target server as of the last poll of the master server. |
| `enlisted_by_nt_user` | **nvarchar(100)** | Microsoft Windows user that enlisted the target server. |
| `poll_interval` | **int** | Frequency in seconds with which the target server polls the master SQLServerAgent service in order to download jobs and upload job status. |

## Permissions

To execute this stored procedure, a user must be a member of the **sysadmin** fixed server role.

## Examples

### A. List information for all registered target servers

The following example lists information for all registered target servers.

```sql
USE msdb;
GO

EXEC dbo.sp_help_targetserver;
GO
```

### B. List information for a specific target server

The following example lists information for the target server `SEATTLE2`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_targetserver N'SEATTLE2';
GO
```

## Related content

- [sp_add_targetservergroup (Transact-SQL)](sp-add-targetservergroup-transact-sql.md)
- [sp_delete_targetserver (Transact-SQL)](sp-delete-targetserver-transact-sql.md)
- [sp_delete_targetservergroup (Transact-SQL)](sp-delete-targetservergroup-transact-sql.md)
- [sp_update_targetservergroup (Transact-SQL)](sp-update-targetservergroup-transact-sql.md)
- [dbo.sysdownloadlist (Transact-SQL)](../system-tables/dbo-sysdownloadlist-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
