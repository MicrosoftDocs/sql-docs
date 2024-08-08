---
title: "jobs.jobsteps (Azure Elastic Jobs) (Transact-SQL)"
description: "The jobs.jobsteps system view contains all steps in the current version of a job in Azure Elastic jobs. "
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/13/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "jobs.jobsteps"
helpviewer_keywords:
  - "jobsteps catalog view"
  - "jobs.jobsteps catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.jobsteps (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Contains all steps in the current version of jobs in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

|Column name|Data type|Description|
|------|------|-------|
|**job_name**|nvarchar(128)|Name of the job.|
|**job_id**|uniqueidentifier|Unique ID of the job.|
|**job_version**|int|Version of the job (automatically updated each time the job is modified).|
|**step_id**|int|Unique (for this job) identifier for the step.|
|**step_name**|nvarchar(128)|Unique (for this job) name for the step.|
|**command_type**|nvarchar(50)|Type of command to execute in the job step. The value must equal to and defaults to `TSql`.|
|**command_source**|nvarchar(50)|Location of the command. `Inline` is the default and only accepted value.|
|**command**|nvarchar(max)|The commands to be executed by elastic jobs through `command_type`.|
|**credential_name**|nvarchar(128)|Name of the database-scoped credential used to execution the job.|
|**target_group_name**|nvarchar(128)|Name of the target group.|
|**target_group_id**|uniqueidentifier|Unique ID of the target group.|
|**initial_retry_interval_seconds**|int|The delay before the first retry attempt. Default value is 1.|
|**maximum_retry_interval_seconds**|int|The maximum delay between retry attempts. If the delay between retries would grow larger than this value, it is capped to this value instead. Default value is 120.|
|**retry_interval_backoff_multiplier**|real|The multiplier to apply to the retry delay if multiple job step execution attempts fail. Default value is 2.0.|
|**retry_attempts**|int|The number of retry attempts to use if this step fails. Default of 10, which indicates no retry attempts.|
|**step_timeout_seconds**|int|The amount of time in minutes between retry attempts. The default is 0, which indicates a 0-minute interval.|
|**output_type**|nvarchar(11)|Location of the command. `Inline` is the default and only accepted value.|
|**output_credential_name**|nvarchar(128)|Name of the credentials to be used to connect to the destination server to store the results set.|
|**output_subscription_id**|uniqueidentifier|Unique ID of the subscription of the destination server\database for the results set from the query execution.|
|**output_resource_group_name**|nvarchar(128)|Resource group name where the destination server resides.|
|**output_server_name**|nvarchar(256)|Name of the destination server for the results set.|
|**output_database_name**|nvarchar(128)|Name of the destination database for the results set.|
|**output_schema_name**|nvarchar(max)|Name of the destination schema. Defaults to `dbo`, if not specified.|
|**output_table_name**|nvarchar(max)|Name of the table to store the results set from the query results. Table will be created automatically based on the schema of the results set if it doesn't already exist. Schema must match the schema of the results set.|
|**max_parallelism**|int|The maximum number of databases per elastic pool that the job step will be run on at a time. The default is `NULL`, meaning no limit. |

## Permissions

Members of the *jobs_reader* role can SELECT from this view. For more information, see [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#elastic-job-database-permissions).

## Remarks

All times in elastic jobs are in the UTC time zone.

To view version history of job steps, use [jobs.jobstep_versions](jobs-jobstep-versions-elastic-jobs-transact-sql.md).

> [!CAUTION]
> You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures on your *job database*.

## Examples

### View steps from current version of all jobs

```sql
-- View the steps of the current version of all jobs
SELECT js.* 
FROM jobs.jobsteps AS js
INNER JOIN jobs.jobs AS j
  ON j.job_id = js.job_id AND j.job_version = js.job_version;
```

## Related content

- [jobs.sp_add_jobstep (Azure Elastic Jobs) (Transact-SQL)](../system-stored-procedures/sp-add-jobstep-elastic-jobs-transact-sql.md)
- [jobs.sp_update_jobstep (Azure Elastic Jobs) (Transact-SQL)](../system-stored-procedures/sp-update-jobstep-elastic-jobs-transact-sql.md)
- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
