---
title: "jobs.job_versions (Azure Elastic Jobs) (Transact-SQL)"
description: "The jobs.job_versions system view contains job version history in Azure Elastic jobs."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/13/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "jobs.job_versions"
  - "job_versions"
helpviewer_keywords:
  - "job_versions catalog view"
  - "jobs.job_versions catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.job_versions (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Contains version history about jobs in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

|Column name|Data type|Description|
|------|------|-------|
|**job_name**|nvarchar(128)|Name of the job.|
|**job_id**|uniqueidentifier|Unique ID of the job.|
|**job_version**|int|Version of the job (automatically updated each time the job is modified).|

## Permissions

Members of the *jobs_reader* role can SELECT from this view. For more information, see [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#elastic-job-database-permissions).

> [!CAUTION]
> You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures on your *job database*.

## Remarks

All times in elastic jobs are in the UTC time zone.

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
