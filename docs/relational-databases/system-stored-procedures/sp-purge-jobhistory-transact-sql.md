---
title: "sp_purge_jobhistory (Transact-SQL)"
description: sp_purge_jobhistory removes the history records for a job in the SQL Server Agent service.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_purge_jobhistory_TSQL"
  - "sp_purge_jobhistory"
helpviewer_keywords:
  - "sp_purge_jobhistory"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_purge_jobhistory (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Removes the history records for a job in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_purge_jobhistory
    [ [ @job_name = ] N'job_name' ]
    [ , [ @job_id = ] 'job_id' ]
    [ , [ @oldest_date = ] oldest_date ]
[ ; ]
```

## Arguments

#### [ @job_name = ] N'*job_name*'

The name of the job for which to delete the history records. *@job_name* is **sysname**, with a default of `NULL`. Either *@job_id* or *@job_name* must be specified, but both can't be specified.

Members of the **sysadmin** fixed server role or members of the **SQLAgentOperatorRole** fixed database role can execute `sp_purge_jobhistory` without specifying a *@job_name* or *@job_id*. When **sysadmin** users don't specify these arguments, the job history for all local and multiserver jobs is deleted within the time specified by *@oldest_date*. When **SQLAgentOperatorRole** users don't specify these arguments, the job history for all local jobs is deleted within the time specified by *@oldest_date*.

#### [ @job_id = ] '*job_id*'

The job identification number of the job for the records to be deleted. *@job_id* is **uniqueidentifier**, with a default of `NULL`. Either *@job_id* or *@job_name* must be specified, but both can't be specified.

See the note in the description of *@job_name* for information about how **sysadmin** or **SQLAgentOperatorRole** users can use this argument.

#### [ @oldest_date = ] *oldest_date*

The oldest record to retain in the history. *@oldest_date* is **datetime**, with a default of `NULL`. When *oldest_date* is specified, `sp_purge_jobhistory` only removes records that are older than the value specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

When `sp_purge_jobhistory` completes successfully, a message is returned.

This stored procedure shares the name of `sp_purge_jobhistory` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_purge_jobhistory (Azure Elastic Jobs)](sp-purge-jobhistory-elastic-jobs-transact-sql.md?view=azuresql-db&preserve-view=true).

## Permissions

By default, only members of the **sysadmin** fixed server role or the **SQLAgentOperatorRole** fixed database role can execute this stored procedure. Members of **sysadmin** can purge the job history for all local and multiserver jobs. Members of **SQLAgentOperatorRole** can purge the job history for all local jobs only.

Other users, including members of **SQLAgentUserRole** and members of **SQLAgentReaderRole**, must explicitly be granted the EXECUTE permission on `sp_purge_jobhistory`. After being granted EXECUTE permission on this stored procedure, these users can only purge the history for jobs that they own.

The **SQLAgentUserRole**, **SQLAgentReaderRole**, and **SQLAgentOperatorRole** fixed database roles are in the `msdb` database. For details about their permissions, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

## Examples

### A. Remove history for a specific job

The following example removes the history for a job named `NightlyBackups`.

```sql
USE msdb;
GO

EXEC dbo.sp_purge_jobhistory
    @job_name = N'NightlyBackups';
GO
```

### B. Remove history for all jobs

Only members of the **sysadmin** fixed server role and members of the **SQLAgentOperatorRole** can remove history for all jobs. When **sysadmin** users execute this stored procedure with no parameters, the job history for all local and multiserver jobs is purged. When **SQLAgentOperatorRole** users execute this stored procedure with no parameters, only the job history for all local jobs is purged.

The following example executes the procedure with no parameters to remove all history records.

```sql
USE msdb;
GO

EXEC dbo.sp_purge_jobhistory;
GO
```

## Related content

- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
- [sp_help_jobhistory (Transact-SQL)](sp-help-jobhistory-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [GRANT object permissions (Transact-SQL)](../../t-sql/statements/grant-object-permissions-transact-sql.md)
