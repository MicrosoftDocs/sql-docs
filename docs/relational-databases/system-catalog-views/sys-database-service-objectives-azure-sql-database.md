---
title: "sys.database_service_objectives"
titleSuffix: Azure SQL Database
description: sys.database_service_objectives (Azure SQL Database)
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/25/2022
ms.service: sql-database
ms.topic: conceptual
ms.custom: seo-dt-2019
f1_keywords:
  - "DATABASE_SERVICE_OBJECTIVES_TSQL"
dev_langs:
  - "TSQL"
keywords:
  - "elastic pool"
  - "elastic pool, management"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest"
---
# sys.database_service_objectives (Azure SQL Database)
[!INCLUDE [asdb-asdbmi-asa](../../includes/applies-to-version/asdb-asdbmi-asa.md)]

Returns the edition (service tier), service objective (pricing tier), and elastic pool name, if any, for an Azure SQL database or a dedicated SQL pool in Azure Synapse Analytics.

- If logged on to the `master` database in an Azure SQL Database server, returns information on all databases.
- For dedicated SQL pools in Azure Synapse Analytics, you must be connected to the `master` database. This applies to both dedicated SQL pools in Azure Synapse workspaces and dedicated SQL pools (formerly SQL DW).
    
## Result set
  
|Column Name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|int|The ID of the database, unique within the logical server. Joinable with [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).|  
|edition|sysname|The service tier for the database or data warehouse: **Basic**, **Standard**, **Premium** or **Data Warehouse**.|  
|service_objective|sysname|The pricing tier of the database. If the database is in an elastic pool, returns **ElasticPool**.<br /><br /> On the **Basic** tier, returns **Basic**.<br /><br /> **Single database in a standard service tier** returns one of the following: S0, S1, S2, S3, S4, S6, S7, S9 or S12.<br /><br /> **Single database in a premium tier** returns of the following: P1, P2, P4, P6, P11 or P15.<br /><br /> **Azure Synapse Analytics** returns DW100 through DW30000c.<br /><br /> For details, see [single databases](/azure/sql-database/sql-database-dtu-resource-limits-single-databases/), [elastic pools](/azure/sql-database/sql-database-dtu-resource-limits-elastic-pools/), [data warehouses](/azure/sql-data-warehouse/what-is-a-data-warehouse-unit-dwu-cdwu/)|  
|elastic_pool_name|sysname|The name of the [elastic pool](/azure/azure-sql/database/elastic-pool-overview) that the database belongs to. Returns **NULL** if the database is a single database or a dedicated SQL pool.|  

## Permissions  
 Requires **dbManager** permission on the `master` database.  At the database level, the user must be the creator or owner.  

## Remarks
 
To change the service settings, see [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-transact-sql.md) and [ALTER DATABASE (Azure Synapse Analytics)](../../t-sql/statements/alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true).  

This catalog view is not supported in serverless SQL pools in Azure Synapse Analytics.

For information on pricing, see [SQL Database options and performance: SQL Database Pricing](https://azure.microsoft.com/pricing/details/sql-database/) and [Azure Synapse Analytics Pricing](https://azure.microsoft.com/pricing/details/sql-data-warehouse/).  
  
  
## Examples  
 This example can be run on the `master` database or on Azure SQL Database user databases. The query returns the name, service, and performance tier information of the database(s).  
  
```sql  
SELECT  d.name,   
     slo.*    
FROM sys.databases d   
JOIN sys.database_service_objectives slo    
ON d.database_id = slo.database_id;  
```  
  
## See also

 - [Azure SQL Database Catalog Views](azure-sql-database-catalog-views.md)
 - [sys.databases](sys-databases-transact-sql.md)
 - [sys.event_log](sys-event-log-azure-sql-database.md)
 - [sys.dm_operation_status](../system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md)
 - [sys.dm_db_resource_stats](../system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database.md)
 - [sys.database_connection_stats](sys-database-connection-stats-azure-sql-database.md)
 - [sys.database_service_objectives](sys-database-service-objectives-azure-sql-database.md)
 - [sys.dm_user_db_resource_governance](../system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database.md)

## Next steps

 - [Monitor Azure SQL Database with Azure Monitor](/azure/azure-sql/database/monitoring-sql-database-azure-monitor)
