---
title: "jobs.job_executions (Azure Elastic Jobs) (Transact-SQL)"
description: "The jobs.job_executions system view contains information about Azure Elastic job execution history."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 04/03/2024
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "JOBS.JOB_EXECUTIONS"
  - "JOB_EXECUTIONS"
helpviewer_keywords:
  - "job_executions catalog view"
  - "jobs.job_executions catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.job_executions (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Contains job execution status and history for jobs in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

|Column name | Data type | Description |
|---------|---------|---------|
|**job_execution_id** | uniqueidentifier | Unique ID of an instance of a job execution.
|**job_name** | nvarchar(128) | Name of the job.
|**job_id** | uniqueidentifier | Unique ID of the job.
|**job_version** | int | Version of the job (automatically updated each time the job is modified).
|**step_id** |int | Unique (for this job) identifier for the step. `NULL` indicates this execution is the parent job execution.
|**is_active** | bit | Indicates whether information is active or inactive. `1` indicates active jobs, and `0` indicates inactive.
|**lifecycle** | nvarchar(50) | Value indicating the status of the job. See [Lifecycle](#lifecycle) table for possible values. |
|**create_time**| datetime2(7) | Date and time the job was created.
|**start_time** | datetime2(7) | Date and time the job started execution. `NULL` if the job has not yet been executed.
|**end_time** | datetime2(7) | Date and time the job finished execution. `NULL` if the job has not yet been executed or has not yet completed execution.
|**current_attempts** | int | Number of times the step was retried. Parent job is `0`, child job executions will be `1` or greater, based on the execution policy.
|**current_attempt_start_time** | datetime2(7) | Date and time the job started execution. `NULL` indicates this execution is the parent job execution.
|**next_attempt_start_time** | datetime2(7) | Date and time the job will start next execution. `NULL` indicates this execution is the parent job execution.
|**last_message** | nvarchar(max) | Job or step history message.
|**target_type** | nvarchar(128) | Type of target database or collection of databases including all databases in a server, all databases in an elastic pool or a database. Valid values for `target_type` are `SqlServer`, `SqlElasticPool`, or `SqlDatabase`. `NULL` indicates this execution is the parent job execution.
|**target_id** | uniqueidentifier | Unique ID of the target group member.  `NULL` indicates this execution is the parent job execution.
|**target_group_name** | nvarchar(128) | Name of the target group. `NULL` indicates this execution is the parent job execution.
|**target_server_name** | nvarchar(256)  | Name of the server contained in the target group. Specified only if `target_type` is `SqlServer`. `NULL` indicates this execution is the parent job execution.
|**target_database_name** | nvarchar(128) | Name of the database contained in the target group. Specified only when `target_type` is `SqlDatabase`. `NULL` indicates this execution is the parent job execution.

<a id="lifecycle"></a>

The following table lists the possible job execution states in `lifecycle`:

|State|Description|
|:---|:---|
|**Created** | The job execution was just created and is not yet in progress.|
|**InProgress** | The job execution is currently in progress.|
|**WaitingForRetry** | The job execution wasn't able to complete its action and is waiting to retry.|
|**Succeeded** | The job execution completed successfully.|
|**SucceededWithSkipped** | The job execution completed successfully, but some of its children were skipped.|
|**Failed** | The job execution failed and exhausted its retries.|
|**TimedOut** | The job execution timed out.|
|**Canceled** | The job execution was canceled.|
|**Skipped** | The job execution was skipped because another execution of the same job step was already running on the same target.|
|**WaitingForChildJobExecutions** | The job execution is waiting for its child executions to complete.|

## Permissions

Members of the *jobs_reader* role can SELECT from this view. For more information, see [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#elastic-job-database-permissions).

> [!CAUTION]
> You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures on your *job database*.

## Remarks

All times in elastic jobs are in the UTC time zone.

### Monitor job execution status

The following example shows how to view execution status details for all jobs.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

--View top-level execution status for the job named 'ResultsPoolJob'
SELECT * FROM jobs.job_executions
WHERE job_name = 'ResultsPoolsJob' and step_id IS NULL
ORDER BY start_time DESC;

--View all top-level execution status for all jobs
SELECT * FROM jobs.job_executions WHERE step_id IS NULL
ORDER BY start_time DESC;

--View all execution statuses for job named 'ResultsPoolsJob'
SELECT * FROM jobs.job_executions
WHERE job_name = 'ResultsPoolsJob'
ORDER BY start_time DESC;

-- View all active executions
SELECT * FROM jobs.job_executions
WHERE is_active = 1
ORDER BY start_time DESC;
```

### Run a job and monitor status

The following example shows how to start an elastic job immediately as a manual, unplanned action.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Execute the latest version of a job and receive the execution id
DECLARE @je uniqueidentifier;
EXEC jobs.sp_start_job 'CreateTableTest', @job_execution_id = @je output;
SELECT @je;

-- Monitor progress

SELECT * FROM jobs.job_executions WHERE job_execution_id = @je;
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
