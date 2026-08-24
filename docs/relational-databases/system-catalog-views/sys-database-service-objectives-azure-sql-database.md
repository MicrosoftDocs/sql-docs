---
title: "sys.database_service_objectives"
titleSuffix: Azure SQL Database & Azure Synapse Analytics & SQL database in Fabric
description: "sys.database_service_objectives returns the edition (service tier), service objective (pricing tier), and elastic pool name, if any, for an Azure SQL database or a dedicated SQL pool in Azure Synapse Analytics."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 11/18/2025
ms.service: azure-sql-database
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DATABASE_SERVICE_OBJECTIVES_TSQL"
dev_langs:
  - "TSQL"
keywords:
  - "elastic pool"
  - "elastic pool, management"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || =fabric-sqldb"
---
# sys.database_service_objectives
[!INCLUDE [asdb-asa](../../includes/applies-to-version/asdb-asa-fabricsqldb.md)]

Returns the edition (service tier), service objective (pricing tier), and elastic pool name, if any. 

Returns data only in Azure SQL database, SQL database in Fabric, or dedicated SQL pool in Azure Synapse Analytics.

If the current database context is the `master` database in an Azure SQL Database logical server, returns information on all databases.

## Result set
  
|Column Name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `database_id` |**int**|The ID of the database, unique within the logical server. Joinable with [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) on the `database_id` column, but with not other system views where the `database_id` column is present. For details, see [DB_ID](../../t-sql/functions/db-id-transact-sql.md#remarks).|  
| `edition` |**sysname**|The service tier for the database or data warehouse: **Basic**, **Standard**, **Premium**, or **Data Warehouse**.|  
| `service_objective` |**sysname**|The pricing tier of the database. If the database is in an elastic pool, returns **ElasticPool**.<br /><br /> On the **Basic** tier, returns **Basic**.<br /> **Single database in a standard service tier** returns one of the following: S0, S1, S2, S3, S4, S6, S7, S9, or S12.<br /> **Single database in a premium tier** returns of the following: P1, P2, P4, P6, P11, or P15.<br /> **Azure Synapse Analytics** returns DW100 through DW30000c.<br />**SQL database in Fabric** returns `FabricSQLDB` always.|  
| `elastic_pool_name` |**sysname**|The name of the [elastic pool](/azure/azure-sql/database/elastic-pool-overview) that the database belongs to. Returns `NULL` if the database is a single database or a dedicated SQL pool.|  

## Permissions

 Requires **dbManager** permission on the `master` database. At the database level, the user must be the creator or owner.  

## Remarks

For details on service objectives, see [single databases](/azure/sql-database/sql-database-dtu-resource-limits-single-databases/), [elastic pools](/azure/sql-database/sql-database-dtu-resource-limits-elastic-pools/). For Azure Synapse Analytics, see [DWUs](/azure/sql-data-warehouse/what-is-a-data-warehouse-unit-dwu-cdwu/).

To change the service settings, see [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-transact-sql.md) and [ALTER DATABASE (Azure Synapse Analytics)](../../t-sql/statements/alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true).  

For dedicated SQL pools in Azure Synapse Analytics, you must be connected to the `master` database. This applies to both dedicated SQL pools in Azure Synapse workspaces and dedicated SQL pools (formerly SQL DW). This catalog view is not supported in serverless SQL pools in Azure Synapse Analytics.

## Examples

 This query returns the name, service, service objective, and elastic pool name (if present) of the current database context.
  
```sql
SELECT  d.name, slo.edition, slo.service_objective, slo.elastic_pool_name
FROM sys.database_service_objectives AS slo
JOIN sys.databases d ON slo.database_id = d.database_id
WHERE d.name = DB_NAME();
```  

## Next step

> [!div class="nextstepaction"]
> [Monitor Azure SQL Database with Azure Monitor](/azure/azure-sql/database/monitoring-sql-database-azure-monitor)

## Related content

- [Azure SQL Database and SQL database in Fabric catalog views](azure-sql-database-catalog-views.md)
- [sys.databases (Transact-SQL)](sys-databases-transact-sql.md)
- [sys.event_log (Azure SQL Database)](sys-event-log-azure-sql-database.md)
- [sys.dm_operation_status](../system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md)
- [sys.dm_db_resource_stats](../system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database.md)
- [sys.database_connection_stats (Azure SQL Database)](sys-database-connection-stats-azure-sql-database.md)
- [sys.database_service_objectives](sys-database-service-objectives-azure-sql-database.md)
- [sys.dm_user_db_resource_governance](../system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database.md)
