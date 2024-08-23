---
title: "sp_add_job (Transact-SQL)"
description: "sp_add_job creates a new job to be executed by the SQL Server Agent service."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_job_TSQL"
  - "sp_add_job"
helpviewer_keywords:
  - "sp_add_job"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_add_job (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a new job executed by the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

## Syntax

```syntaxsql
sp_add_job
         [ @job_name = ] N'job_name'
     [ , [ @enabled = ] enabled ]
     [ , [ @description = ] N'description' ]
     [ , [ @start_step_id = ] step_id ]
     [ , [ @category_name = ] 'category' ]
     [ , [ @category_id = ] category_id ]
     [ , [ @owner_login_name = ] 'login' ]
     [ , [ @notify_level_eventlog = ] eventlog_level ]
     [ , [ @notify_level_email = ] email_level ]
     [ , [ @notify_level_netsend = ] netsend_level ]
     [ , [ @notify_level_page = ] page_level ]
     [ , [ @notify_email_operator_name = ] 'email_name' ]
     [ , [ @notify_netsend_operator_name = ] 'netsend_name' ]
     [ , [ @notify_page_operator_name = ] 'page_name' ]
     [ , [ @delete_level = ] delete_level ]
     [ , [ @job_id = ] job_id OUTPUT ]
[ ; ]
```

## Arguments

#### @job_name

The name of the job. The name must be unique and can't contain the percent (`%`) character. *@job_name* is **nvarchar(128)**, with no default. Required.

#### @enabled

Indicates the status of the added job. *enabled* is **tinyint**, with a default of `1` (enabled). If `0`, the job isn't enabled and doesn't run according to its schedule; however, it can be run manually.

#### @description

The description of the job. *@description* is **nvarchar(512)**, with a default of `NULL`. If *@description* is omitted, `N'No description available'` is used.

#### @start_step_id

The identification number of the first step to execute for the job. *@start_step_id* is **int**, with a default of `1`.

#### @category_name

The category for the job. *@category_name* is **sysname**, with a default of `NULL`.

#### @category_id

A language-independent mechanism for specifying a job category. *@category_id* is **int**, with a default of `NULL`.

#### @owner_login_name

The name of the login that owns the job. *@owner_login_name* is **sysname**, with a default of `NULL`, which is interpreted as the current login name. Only members of the **sysadmin** fixed server role can set or change the value for *@owner_login_name*. If users who aren't members of the **sysadmin** role set or change the value of *@owner_login_name*, execution of this stored procedure fails and an error is returned.

#### @notify_level_eventlog

A value indicating when to place an entry in the Microsoft Windows application log for this job. *@notify_level_eventlog* is **int**, and can be one of these values:

| Value | Description |
| --- | --- |
| `0` | Never |
| `1` | On success |
| `2` (default) | On failure |
| `3` | Always |

#### @notify_level_email

A value that indicates when to send an e-mail upon the completion of this job. *@notify_level_email* is **int**, with a default of `0`, which indicates never. *@notify_level_email* uses the same values as *@notify_level_eventlog*.

#### @notify_level_netsend

A value that indicates when to send a network message upon the completion of this job. *@notify_level_netsend* is **int**, with a default of `0`, which indicates never. *@notify_level_netsend* uses the same values as *@notify_level_eventlog*.

#### @notify_level_page

A value that indicates when to send a page upon the completion of this job. *@notify_level_page* is **int**, with a default of `0`, which indicates never. *@notify_level_page* uses the same values as *@notify_level_eventlog*.

#### @notify_email_operator_name

The e-mail name of the person to send e-mail to when *@notify_email_operator_name* is reached. *@notify_email_operator_name* is **sysname**, with a default of `NULL`.

#### @notify_netsend_operator_name

The name of the operator to whom the network message is sent upon completion of this job. *@notify_netsend_operator_name* is **sysname**, with a default of `NULL`.

#### @notify_page_operator_name

The name of the person to page upon completion of this job. *@notify_page_operator_name* is **sysname**, with a default of `NULL`.

#### @delete_level

A value that indicates when to delete the job. *delete_value* is **int**, with a default of `0`, which means never. *@delete_level* uses the same values as *@notify_level_eventlog*.

> [!NOTE]  
> When *@delete_level* is `3`, the job is executed only once, regardless of any schedules defined for the job. Furthermore, if a job deletes itself, all history for the job is also deleted.

#### @job_id OUTPUT

The job identification number assigned to the job if created successfully. *@job_id* is an output variable of type **uniqueidentifier**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

*@originating_server* exists in `sp_add_job`, but isn't listed under Arguments. *@originating_server* is reserved for internal use.

After `sp_add_job` has been executed to add a job, `sp_add_jobstep` can be used to add steps that perform the activities for the job. `sp_add_jobschedule` can be used to create the schedule that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service uses to execute the job.

Use `sp_add_jobserver` to set the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance where the job executes, and `sp_delete_jobserver` to remove the job from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. If the job executes on one or more target servers in a multiserver environment, use `sp_apply_job_to_targets` to set the target servers or target server groups for the job. To remove jobs from target servers or target server groups, use `sp_remove_job_from_targets`. The [Multi Server Administration (MSX/TSX) feature is not supported on [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent).

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

This stored procedure shares the name of `sp_add_job` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_add_job (Azure Elastic Jobs)](sp-add-job-elastic-jobs-transact-sql.md?view=azuresql-db&preserve-view=true).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of the **sysadmin** fixed server role can set or change the value for *@owner_login_name*. If users who aren't members of the **sysadmin** role set or change the value of *@owner_login_name*, execution of this stored procedure fails and an error is returned.

## Examples

### A. Add a job

This example adds a new job named `NightlyBackups`.

```sql
USE msdb;
GO

EXEC dbo.sp_add_job @job_name = N'NightlyBackups';
GO
```

### B. Add a job with pager, e-mail, and net send information

This example creates a job named `Ad hoc Sales Data Backup` that notifies `François Ajenstat` (by pager, e-mail, or network pop-up message) if the job fails, and deletes the job upon successful completion.

> [!NOTE]  
> This example assumes that an operator named `François Ajenstat` and a login named `françoisa` already exist.

```sql
USE msdb;
GO

EXEC dbo.sp_add_job
    @job_name = N'Ad hoc Sales Data Backup',
    @enabled = 1,
    @description = N'Ad hoc backup of sales data',
    @owner_login_name = N'françoisa',
    @notify_level_eventlog = 2,
    @notify_level_email = 2,
    @notify_level_netsend = 2,
    @notify_level_page = 2,
    @notify_email_operator_name = N'François Ajenstat',
    @notify_netsend_operator_name = N'François Ajenstat',
    @notify_page_operator_name = N'François Ajenstat',
    @delete_level = 1;
GO
```

## Related content

- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_add_jobstep (Transact-SQL)](sp-add-jobstep-transact-sql.md)
- [sp_add_jobserver (Transact-SQL)](sp-add-jobserver-transact-sql.md)
- [sp_apply_job_to_targets (Transact-SQL)](sp-apply-job-to-targets-transact-sql.md)
- [sp_delete_job (Transact-SQL)](sp-delete-job-transact-sql.md)
- [sp_delete_jobserver (Transact-SQL)](sp-delete-jobserver-transact-sql.md)
- [sp_remove_job_from_targets (Transact-SQL)](sp-remove-job-from-targets-transact-sql.md)
- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
- [sp_help_jobstep (Transact-SQL)](sp-help-jobstep-transact-sql.md)
- [sp_update_job (Transact-SQL)](sp-update-job-transact-sql.md)
