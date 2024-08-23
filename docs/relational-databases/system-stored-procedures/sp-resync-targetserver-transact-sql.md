---
title: "sp_resync_targetserver (Transact-SQL)"
description: sp_resync_targetserver resynchronizes all multiserver jobs in the specified target server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_resync_targetserver"
  - "sp_resync_targetserver_TSQL"
helpviewer_keywords:
  - "sp_resync_targetserver"
dev_langs:
  - "TSQL"
---
# sp_resync_targetserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Resynchronizes all multiserver jobs in the specified target server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_resync_targetserver [ @server_name = ] N'server_name'
[ ; ]
```

## Arguments

#### [ @server_name = ] N'*server_name*'

The name of the server to resynchronize. *@server_name* is **sysname**, with no default. If `all` is specified, all target servers are resynchronized.

## Return code values

`0` (success) or `1` (failure).

## Result set

Reports the result of `sp_post_msx_operation` actions.

## Remarks

`sp_resync_targetserver` deletes the current set of instructions for the target server and posts a new set for the target server to download. The new set consists of an instruction to delete all multiserver jobs, followed by an insert for each job currently targeted at the server.

## Permissions

Permissions to execute this procedure default to members of the **sysadmin** fixed server role.

## Examples

The following example resynchronizes the `SEATTLE1` target server.

```sql
USE msdb;
GO

EXEC dbo.sp_resync_targetserver N'SEATTLE1';
GO
```

## Related content

- [sp_help_downloadlist (Transact-SQL)](sp-help-downloadlist-transact-sql.md)
- [sp_post_msx_operation (Transact-SQL)](sp-post-msx-operation-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
