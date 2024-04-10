---
title: "jobs.sp_stop_job (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_stop_job instructs the elastic job agent to stop a job execution in the Azure Elastic Jobs service for Azure SQL Database."
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
# jobs.sp_stop_job (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Instructs the elastic job agent to stop a job execution in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_stop_job` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_stop_job (Transact-SQL)](sp-stop-job-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_stop_job [ @job_execution_id = ] ' job_execution_id '
```

## Arguments

#### @job_execution_id

The identification number of the job execution to stop. *job_execution_id* is uniqueidentifier, with default of `NULL`.

## Return Code Values

0 (success) or 1 (failure)

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Remarks

All times in elastic jobs are in the UTC time zone.

To identify the `job_execution_id` of a current job execution, use [jobs.job_executions](../system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql.md).

## Examples

### Identify and stop a job execution

The following example shows how to identify a job execution in [jobs.job_executions](../system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql.md) and then cancel a job execution using the `job_execution_id`, for example `01234567-89ab-cdef-0123-456789abcdef`.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- View all active executions to determine job_execution_id
SELECT job_name
, job_execution_id
, job_version
, step_id
, is_active
, lifecycle
, start_time
, current_attempts
, current_attempt_start_time
, last_message
, target_group_name
, target_server_name
, target_database_name
FROM jobs.job_executions
WHERE is_active = 1 AND job_name = 'ResultPoolsJob'
ORDER BY start_time DESC;
GO

-- Cancel job execution with the specified job_execution_id
EXEC jobs.sp_stop_job '01234567-89ab-cdef-0123-456789abcdef';
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
