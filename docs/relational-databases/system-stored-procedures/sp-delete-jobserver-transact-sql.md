---
title: "sp_delete_jobserver (Transact-SQL)"
description: sp_delete_jobserver removes the specified target server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_jobserver"
  - "sp_delete_jobserver_TSQL"
helpviewer_keywords:
  - "sp_delete_jobserver"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017"
---
# sp_delete_jobserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the specified target server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_jobserver
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    , [ @server_name = ] N'server_name'
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The identification number of the job from which the specified target server will be removed. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job from which the specified target server will be removed. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @server_name = ] N'*server_name*'

The name of the target server to remove from the specified job. *@server_name* is **sysname**, with no default. *@server_name* can be `(LOCAL)` or the name of a remote target server.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

To run this stored procedure, users must be members of the **sysadmin** fixed server role.

## Examples

The following example removes the server `SEATTLE2` from processing the `Weekly Sales Backups` job. This example assumes that the `Weekly Sales Backups` job was created previously.

```sql
USE msdb;
GO

EXEC sp_delete_jobserver
    @job_name = N'Weekly Sales Backups',
    @server_name = N'SEATTLE2';
GO
```

## Related content

- [sp_add_jobserver (Transact-SQL)](sp-add-jobserver-transact-sql.md)
- [sp_help_jobserver (Transact-SQL)](sp-help-jobserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
