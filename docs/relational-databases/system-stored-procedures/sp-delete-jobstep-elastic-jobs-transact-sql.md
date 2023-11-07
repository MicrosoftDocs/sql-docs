---
title: "jobs.sp_delete_jobstep (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_delete_jobstep removes an existing job step from an existing job created for the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_delete_jobstep (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Removes an existing job step from an existing job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_delete_jobstep` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the SQL Agent service. For information about the SQL Agent version, see [sp_delete_jobstep (Transact-SQL)](sp-delete-jobstep-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_delete_jobstep [ @job_name = ] 'job_name'
     [ , [ @step_id = ] step_id ]
     [ , [ @step_name = ] 'step_name' ]
     [ , [ @job_version = ] job_version OUTPUT ]
```

## Arguments

#### @job_name

The name of the job from which the step will be removed. *job_name* is nvarchar(128), with no default.

#### @step_id  

The identification number for the job step to be deleted. Either *step_id* or *step_name* must be specified. *step_id* is an int.

#### @step_name  

The name of the step to be deleted. Either *step_id* or *step_name* must be specified. *step_name* is nvarchar(128).

#### @job_version OUTPUT  

Output parameter that will be assigned the new job version number. *job_version* is int.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

To remove an entire job, use [jobs.sp_delete_job (Azure Elastic Jobs) (Transact-SQL)](sp-delete-job-elastic-jobs-transact-sql.md).

Any in-progress executions of the job will not be affected.

The other job steps will be automatically renumbered to fill the gap left by the deleted job step.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Related content

- [Elastic jobs in Azure SQL Database (preview)](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs (preview)](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL (preview)](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)