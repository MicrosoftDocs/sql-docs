---
title: "sp_update_job (Transact-SQL)"
description: "sp_update_job (Transact-SQL) update the attributes of an existing job created in the SQL Server Agent service."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, wiassaf
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_job"
  - "sp_update_job_TSQL"
helpviewer_keywords:
  - "sp_update_job"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_update_job (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Updates the attributes of an existing job created in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_job
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @enabled = ] enabled ]
    [ , [ @description = ] N'description' ]
    [ , [ @start_step_id = ] start_step_id ]
    [ , [ @category_name = ] N'category_name' ]
    [ , [ @owner_login_name = ] N'owner_login_name' ]
    [ , [ @notify_level_eventlog = ] notify_level_eventlog ]
    [ , [ @notify_level_email = ] notify_level_email ]
    [ , [ @notify_level_netsend = ] notify_level_netsend ]
    [ , [ @notify_level_page = ] notify_level_page ]
    [ , [ @notify_email_operator_name = ] N'notify_email_operator_name' ]
    [ , [ @notify_netsend_operator_name = ] N'notify_netsend_operator_name' ]
    [ , [ @notify_page_operator_name = ] N'notify_page_operator_name' ]
    [ , [ @delete_level = ] delete_level ]
    [ , [ @automatic_post = ] automatic_post ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The identification number of the job to be updated. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @new_name = ] N'*new_name*'

The new name for the job. *@new_name* is **sysname**, with a default of `NULL`.

#### [ @enabled = ] *enabled*

Specifies whether the job is enabled (`1`) or not enabled (`0`). *@enabled* is **tinyint**, with a default of `NULL`.

#### [ @description = ] N'*description*'

The description of the job. *@description* is **nvarchar(512)**, with a default of `NULL`.

#### [ @start_step_id = ] *start_step_id*

The identification number of the first step to execute for the job. *@start_step_id* is **int**, with a default of `NULL`.

#### [ @category_name = ] N'*category_name*'

The category of the job. *@category_name* is **sysname**, with a default of `NULL`.

#### [ @owner_login_name = ] N'*owner_login_name*'

The name of the login that owns the job. *@owner_login_name* is **sysname**, with a default of `NULL`. Only members of the **sysadmin** fixed server role can change job ownership.

#### [ @notify_level_eventlog = ] *notify_level_eventlog*

Specifies when to place an entry in the Microsoft Windows application log for this job. *@notify_level_eventlog* is **int**, and can be one of these values.

| Value | Description (action) |
| --- | --- |
| `0` | Never |
| `1` | On success |
| `2` | On failure |
| `3` | Always |

#### [ @notify_level_email = ] *notify_level_email*

Specifies when to send an e-mail upon the completion of this job. *@notify_level_email* is **int**, with a default of `NULL`. *@notify_level_email* uses the same values as *@notify_level_eventlog*.

#### [ @notify_level_netsend = ] *notify_level_netsend*

Specifies when to send a network message upon the completion of this job. *@notify_level_netsend* is **int**, with a default of `NULL`. *@notify_level_netsend* uses the same values as *@notify_level_eventlog*.

#### [ @notify_level_page = ] *notify_level_page*

Specifies when to send a page upon the completion of this job. *@notify_level_page* is **int**, with a default of `NULL`. *@notify_level_page* uses the same values as *@notify_level_eventlog*.

#### [ @notify_email_operator_name = ] N'*notify_email_operator_name*'

The name of the operator to whom the e-mail is sent when *email_level* is reached. *@notify_email_operator_name* is **sysname**, with a default of `NULL`.

#### [ @notify_netsend_operator_name = ] N'*notify_netsend_operator_name*'

The name of the operator to whom the network message is sent. *@notify_netsend_operator_name* is **sysname**, with a default of `NULL`.

#### [ @notify_page_operator_name = ] N'*notify_page_operator_name*'

The name of the operator to whom a page is sent. *@notify_page_operator_name* is **sysname**, with a default of `NULL`.

#### [ @delete_level = ] *delete_level*

Specifies when to delete the job. *@delete_level* is **int**, with a default of `NULL`. *@delete_level* uses the same values as *@notify_level_eventlog*.

#### [ @automatic_post = ] *automatic_post*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_update_job` must be run from the `msdb` database.

`sp_update_job` changes only those settings for which parameter values are supplied. If a parameter is omitted, the current setting is retained.

This stored procedure shares the name of `sp_update_job` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_update_job (Azure Elastic Jobs) (Transact-SQL)](sp-update-job-elastic-jobs-transact-sql.md?view=azuresqldb-current&preserve-view=true).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Examples

The following example changes the name, description, and enabled status of the job `NightlyBackups`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_job
    @job_name = N'NightlyBackups',
    @new_name = N'NightlyBackups -- Disabled',
    @description = N'Nightly backups disabled during server migration.',
    @enabled = 0;
GO
```

## Related content

- [sp_add_job (Transact-SQL)](sp-add-job-transact-sql.md)
- [sp_delete_job (Transact-SQL)](sp-delete-job-transact-sql.md)
- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
