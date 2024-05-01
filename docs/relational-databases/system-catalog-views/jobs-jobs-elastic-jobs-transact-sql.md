---
title: "jobs.jobs (Azure Elastic Jobs) (Transact-SQL)"
description: "The jobs.jobs system view contains configuration information about Azure Elastic jobs."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/13/2023
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "jobs.jobs"
helpviewer_keywords:
  - "jobs catalog view"
  - "jobs.jobs catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.jobs (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Contains jobs configurations in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

|Column name | Data type |Description|
|------|------|-------|
|**job_name** | nvarchar(128) | Name of the job.|
|**job_id**| uniqueidentifier | Unique ID of the job.|
|**job_version** |int | Version of the job (automatically updated each time the job is modified).|
|**description** |nvarchar(512)| Description for the job. |
|**enabled** | bit | Indicates whether the job is enabled or disabled. `1` indicates enabled jobs, and `0` indicates disabled jobs.|
|**schedule_interval_type**|nvarchar(50) | Value indicating when the job is to be executed:<br /><br /> - `Once`<br />- `Minutes`<br />- `Hours`<br />- `Days`<br />- `Weeks`<br />- `Months` |
|**schedule_interval_count**|int| Number of `schedule_interval_type` periods to occur between each execution of the job.|
|**schedule_start_time**|datetime2(7)| Date and time the job was last started execution.|
|**schedule_end_time**|datetime2(7)| Date and time the job was last completed execution.|

## Permissions

Members of the *jobs_reader* role can SELECT from this view. For more information, see [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#elastic-job-database-permissions).

> [!CAUTION]
> You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures on your *job database*.

## Remarks

All times in elastic jobs are in the UTC time zone.

## Examples

### View all enabled jobs in an elastic job agent

This sample retrieves only enabled jobs in the job agent. Connect to the job database specified when creating the job agent to run this sample.

```sql
--Connect to the job database specified when creating the job agent

-- View all enabled jobs
SELECT * FROM [jobs].[jobs]
WHERE [enabled] = 1;
```

## Related content

- [jobs.sp_add_job (Azure Elastic Jobs) (Transact-SQL)](../system-stored-procedures/sp-add-job-elastic-jobs-transact-sql.md)
- [jobs.sp_update_job (Azure Elastic Jobs) (Transact-SQL)](../system-stored-procedures/sp-update-job-elastic-jobs-transact-sql.md)
- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
