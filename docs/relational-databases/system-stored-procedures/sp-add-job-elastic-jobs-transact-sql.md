---
title: "jobs.sp_add_job (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_add_job adds a new job executed by the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_add_job (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Adds a new job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_add_job` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_add_job (Transact-SQL)](sp-add-job-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax
  
```syntaxsql
[jobs].sp_add_job [ @job_name = ] 'job_name'  
  [ , [ @description = ] 'description' ]
  [ , [ @enabled = ] enabled ]
  [ , [ @schedule_interval_type = ] schedule_interval_type ]  
  [ , [ @schedule_interval_count = ] schedule_interval_count ]
  [ , [ @schedule_start_time = ] schedule_start_time ]
  [ , [ @schedule_end_time = ] schedule_end_time ]
  [ , [ @job_id = ] job_id OUTPUT ]
```

## Arguments

#### @job_name

The name of the job. The name must be unique and cannot contain the percent (`%`) character. *job_name* is nvarchar(128), with no default.

#### @description

The description of the job. *description* is nvarchar(512), with a default of `NULL`. If *description* is omitted, an empty string is used.

#### @enabled

Specifies whether the job's schedule is enabled. *Enabled* is bit, with a default of 0 (disabled). If `0`, the job is not enabled and does not run according to its schedule; however, it can be run manually. If `1`, the job will run according to its schedule, and can also be run manually.

#### @schedule_interval_type

Value indicates when the job is to be executed. *schedule_interval_type* is nvarchar(50), with a default of `Once`, and can be one of the following values:

- `Once`
- `Minutes`
- `Hours`
- `Days`
- `Weeks`
- `Months`

#### @schedule_interval_count

Number of *schedule_interval_count* periods to occur between each execution of the job. *schedule_interval_count* is int, with a default of `1`. The value must be greater than or equal to 1.

#### @schedule_start_time

Date on which job execution can begin. *schedule_start_time* is DATETIME2, with the default of 0001-01-01 00:00:00.0000000.

All times in elastic jobs are in the UTC time zone.

#### @schedule_end_time

Date on which job execution can stop. *schedule_end_time* is DATETIME2, with the default of 9999-12-31 11:59:59.0000000.

All times in elastic jobs are in the UTC time zone.

#### @job_id

The job identification number assigned to the job if created successfully. *job_id* is an output variable of type uniqueidentifier.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

The stored procedure `jobs.sp_add_job` must be run from the job agent database specified when creating the job agent.

After `jobs.sp_add_job` has been executed to add a job, [jobs.sp_add_jobstep](sp-add-jobstep-elastic-jobs-transact-sql.md) can be used to add steps that perform the activities for the job. The job's initial version number is `0`, which is incremented to `1` when the first step is added.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure. Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
