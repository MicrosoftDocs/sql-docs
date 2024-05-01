---
title: "jobs.target_groups (Azure Elastic Jobs) (Transact-SQL)"
description: "The jobs.target_groups system view contains target groups in Azure Elastic jobs."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/13/2023
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "jobs.target_groups"
helpviewer_keywords:
  - "target_groups catalog view"
  - "jobs.target_groups catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.target_groups (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Contains all target groups for an elastic job agent in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

|Column name|Data type|Description|
|-----|-----|-----|
|**target_group_name**|nvarchar(128)| The name of the target group.
|**target_group_id**|uniqueidentifier| Unique ID of the target group.

## Permissions

Members of the *jobs_reader* role can SELECT from this view. For more information, see [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#elastic-job-database-permissions).

> [!CAUTION]
> You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures on your *job database*.

## Related content

- [jobs.sp_add_target_group (Azure Elastic Jobs) (Transact-SQL)](../system-stored-procedures/sp-add-target-group-elastic-jobs-transact-sql.md)
- [jobs.target_group_members (Azure Elastic Jobs) (Transact-SQL)](jobs-target-group-members-elastic-jobs-transact-sql.md)
- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
