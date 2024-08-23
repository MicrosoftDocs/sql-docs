---
title: "sp_add_jobserver (Transact-SQL)"
description: "Targets the specified job at the specified server."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_jobserver"
  - "sp_add_jobserver_TSQL"
helpviewer_keywords:
  - "sp_add_jobserver"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017"
---
# sp_add_jobserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Targets the specified job at the specified server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_jobserver
    [ @job_id = ] job_id
        | [ @job_name = ] 'job_name'
    [ , [ @server_name = ] 'server' ]
[ ; ]
```

## Arguments

#### [ @job_id = ] *job_id*

The identification number of the job. *job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] '*job_name*'

The name of the job. *job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @server_name = ] N'*server*'

The name of the server at which to target the job. *@server_name* is **nvarchar(30)**, with a default of `(LOCAL)`. *@server_name* can be either `(LOCAL)` for a local server, or the name of an existing target server.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

*@automatic_post* exists in `sp_add_jobserver`, but isn't listed under Arguments. *@automatic_post* is reserved for internal use.

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of the **sysadmin** fixed server role can execute `sp_add_jobserver` for jobs that involve multiple servers.

## Examples

### A. Assign a job to the local server

The following example assigns the job `NightlyBackups` to run on the local server.

> [!NOTE]  
> This example assumes that the `NightlyBackups` job already exists.

```sql
USE msdb;
GO

EXEC dbo.sp_add_jobserver @job_name = N'NightlyBackups';
GO
```

### B. Assign a job to run on a different server

The following example assigns the multiserver job `Weekly Sales Backups` to the server `SEATTLE2`.

> [!NOTE]  
> This example assumes that the `Weekly Sales Backups` job already exists and that `SEATTLE2` is registered as a target server for the current instance.

```sql
USE msdb;
GO

EXEC dbo.sp_add_jobserver @job_name = N'Weekly Sales Backups',
    @server_name = N'SEATTLE2';
GO
```

## Related content

- [sp_apply_job_to_targets (Transact-SQL)](sp-apply-job-to-targets-transact-sql.md)
- [sp_delete_jobserver (Transact-SQL)](sp-delete-jobserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
