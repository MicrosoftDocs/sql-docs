---
title: "jobs.sp_delete_job (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_delete_job updates a job created for the Azure Elastic Jobs service for Azure SQL Database."
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
# jobs.sp_delete_job (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Deletes an existing job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_delete_job` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the SQL Agent service. For information about the SQL Agent version, see [sp_delete_job (Transact-SQL)](sp-delete-job-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_delete_job [ @job_name = ] 'job_name'
  [ , [ @force = ] force ]
```

## Arguments

#### @job_name

The name of the job to be deleted. *job_name* is nvarchar(128).

#### @force

*force* is bit.

- When `1`, forces the job to be deleted, even if executions are currently in progress, canceling any executions currently in progress.
- When `0`, fails if any job executions are in progress.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

Job history is automatically deleted when a job is deleted.

To remove only a single job step from an existing job, use [jobs.sp_delete_jobstep (Azure Elastic Jobs) (Transact-SQL)](sp-delete-jobstep-elastic-jobs-transact-sql.md).

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure. Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Related content

- [Elastic jobs in Azure SQL Database (preview)](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs (preview)](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL (preview)](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
