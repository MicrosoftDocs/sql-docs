---
title: "jobs.target_group_members (Azure Elastic Jobs) (Transact-SQL)"
description: "The jobs.target_group_members system view contains target group members in a target group in Azure Elastic jobs."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/13/2023
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "jobs.target_group_members"
helpviewer_keywords:
  - "target_group_members catalog view"
  - "jobs.target_group_members catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.target_group_members (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Contains all target group members for a target group in an elastic job agent in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

|Column name|Data type|Description|
|-----|-----|-----|
|**target_group_name**|nvarchar(128)|The name of the target group, a collection of databases. |
|**target_group_id**|uniqueidentifier|Unique ID of the target group.|
|**membership_type**|int|Specifies if the target group member is included or excluded in the target group. Valid values for `target_group_name` are `Include` or `Exclude`.|
|**target_type**|nvarchar(128)|Type of target database or collection of databases including all databases in a server, all databases in an elastic pool or a database. Valid values for `target_type` are `SqlServer`, `SqlElasticPool`, `SqlDatabase`.|
|**target_id**|uniqueidentifier|Unique ID of the target group member.|
|**refresh_credential_name**|nvarchar(128)|Name of the database-scoped credential used to connect to the target group member.|
|**subscription_id**|uniqueidentifier|Unique ID of the subscription.|
|**resource_group_name**|nvarchar(128)|Name of the resource group in which the target group member resides.|
|**server_name**|nvarchar(128)|Name of the server contained in the target group. Specified only if `target_type` is `SqlServer`. |
|**database_name**|nvarchar(128)|Name of the database contained in the target group. Specified only when `target_type` is `SqlDatabase`.|
|**elastic_pool_name**|nvarchar(128)|Name of the elastic pool contained in the target group. Specified only when `target_type` is `SqlElasticPool`.|

## Permissions

Members of the *jobs_reader* role can SELECT from this view. For more information, see [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#elastic-job-database-permissions).

> [!CAUTION]
> You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures on your *job database* to add/delete target groups/members, such as [jobs.sp_add_target_group_member](/sql/relational-databases/system-stored-procedures/sp-add-target-group-member-elastic-jobs-transact-sql?view=azuresqldb-current&preserve-view=true).

## Examples

### View target group members

The following example displays members of the target group named `ServerGroup1` for the logical server `London.database.windows.net`.

```sql
SELECT * FROM jobs.target_group_members 
WHERE target_group_name = 'ServerGroup1' 
AND server_name = 'London.database.windows.net';
```

## Related content

- [jobs.sp_add_target_group_member (Azure Elastic Jobs) (Transact-SQL)](../system-stored-procedures/sp-add-target-group-member-elastic-jobs-transact-sql.md)
- [jobs.target_groups (Azure Elastic Jobs) (Transact-SQL)](jobs-target-groups-elastic-jobs-transact-sql.md)
- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
