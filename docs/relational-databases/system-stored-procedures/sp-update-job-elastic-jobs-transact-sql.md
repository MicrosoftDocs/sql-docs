---
title: "jobs.sp_update_job (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_update_job updates a job created for the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 04/03/2024
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_update_job (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Updates a job created in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_update_job` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_update_job (Transact-SQL)](sp-update-job-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_update_job [ @job_name = ] 'job_name'  
  [ , [ @new_name = ] 'new_name' ]
  [ , [ @description = ] 'description' ]
  [ , [ @enabled = ] enabled ]
  [ , [ @schedule_interval_type = ] schedule_interval_type ]  
  [ , [ @schedule_interval_count = ] schedule_interval_count ]
  [ , [ @schedule_start_time = ] schedule_start_time ]
  [ , [ @schedule_end_time = ] schedule_end_time ]
```

## Arguments

#### @job_name

The name of the job to be updated. *job_name* is nvarchar(128).

#### @new_name

The new name of the job. *new_name* is nvarchar(128).

#### @description  

The description of the job. The *description* argument is nvarchar(512).

#### @enabled  

Specifies whether the job's schedule is enabled (1) or not enabled (0). *@enabled* is bit.

#### @schedule_interval_type

Value indicates when the job is to be executed. *schedule_interval_type* is nvarchar(50) and can be one of the following values:

- 'Once',
- 'Minutes',
- 'Hours',
- 'Days',
- 'Weeks',
- 'Months'

#### @schedule_interval_count

Number of *schedule_interval_count* periods to occur between each execution of the job. *schedule_interval_count* is int, with a default of `1`. The value must be greater than or equal to `1`.

#### @schedule_start_time

Date on which job execution can begin. *schedule_start_time* is DATETIME2, with the default of `0001-01-01 00:00:00.0000000`.

All times in elastic jobs are in the UTC time zone.

#### @schedule_end_time

Date on which job execution can stop. *schedule_end_time* is DATETIME2, with the default of `9999-12-31 11:59:59.0000000`.

All times in elastic jobs are in the UTC time zone.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

After `jobs.sp_add_job` is executed to add a job, use [jobs.sp_add_jobstep](sp-add-jobstep-elastic-jobs-transact-sql.md) to add steps that perform the activities for the job.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure. Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
