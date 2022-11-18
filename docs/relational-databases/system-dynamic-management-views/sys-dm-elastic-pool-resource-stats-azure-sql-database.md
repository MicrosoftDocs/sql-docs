---
title: "sys.dm_elastic_pool_resource_stats"
titleSuffix: Azure SQL Database
description: "sys.dm_elastic_pool_resource_stats returns resource usage statistics for the current database in an elastic pool in Azure SQL Database."
author: WilliaMDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, arvindsh
ms.date: 08/11/2022
ms.service: sql-database
ms.topic: "reference"
ms.custom: 
f1_keywords:
  - "sys.dm_elastic_pool_resource_stats catalog view"
helpviewer_keywords:
  - "sys.dm_elastic_pool_resource_stats_TSQL"
  - "sys.dm_elastic_pool_resource_stats"
  - "dm_elastic_pool_resource_stats_TSQL"
  - "dm_elastic_pool_resource_stats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_elastic_pool_resource_stats (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns resource usage statistics for the [elastic pool](/azure/azure-sql/database/elastic-pool-overview) containing the current database on an Azure SQL Database [logical server](/azure/azure-sql/database/logical-servers). This includes CPU, Data IO, Log IO, storage consumption and concurrent request/session utilization by the pool. The view returns the same data in any database in the same elastic pool.

The `sys.dm_elastic_pool_resource_stats` dynamic management view is similar to [sys.elastic_pool_resource_stats (Azure SQL Database)](../system-catalog-views/sys-elastic-pool-resource-stats-azure-sql-database.md), with the following differences:

|sys.elastic_pool_resource_stats|sys.dm_elastic_pool_resource_stats|
|---|---|
| Available in the `master` database | Available in any user database in an elastic pool |
| Provides data for all elastic pools on a logical server | Provides data for the elastic pool containing the current database |
| Populated asynchronously, data may appear with a few minutes delay | Populated in real time. A new row is added every few seconds. |
| Retains data for 14 days | Retains data for approximately 40 minutes |
| Intended for historical monitoring and trend analysis | Intended for real-time monitoring and troubleshooting |

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**end_time**|**datetime**|UTC time indicating the end of the reporting interval.|  
|**avg_cpu_percent**|**decimal(5,2)**|Average CPU utilization as a percentage of pool limit.|  
|**avg_data_io_percent**|**decimal(5,2)**|Average IOPS utilization as a percentage of pool limit.|  
|**avg_log_write_percent**|**decimal(5,2)**|Average log write throughput utilization as a percentage of pool limit.|  
|**avg_storage_percent**|**decimal(5,2)**| Not supported and may be removed in a future update. Use **used_storage_percent**. |  
|**max_worker_percent**|**decimal(5,2)**|Maximum concurrent workers as a percentage of pool limit.|  
|**max_session_percent**|**decimal(5,2)**|Maximum concurrent sessions as a percentage of pool limit.|  
|**avg_instance_memory_percent**|**decimal(5,2)**| Average consumption of memory by the database engine instance hosting the pool, as a percentage of instance limit. |
|**avg_instance_cpu_percent**|**decimal(5,2)**| |
|**avg_edtu_percent**|**decimal(5,2)**| For DTU elastic pools, average eDTU utilization as a percentage of pool limit. |
|**instance_vcores**|**decimal(5,2)**| The number of vCores provisioned for the database engine instance hosting the pool. |
|**used_storage_mb**|**bigint**| The amount of used storage in all databases in the pool, in megabytes. |
|**allocated_storage_mb**|**bigint**| The amount of storage allocated for all data files in all databases in the pool, in megabytes. |
|**storage_limit_mb**|**bigint**| The maximum data size limit of the pool, in megabytes. |
|**used_storage_percent**|**decimal(5,2)**| Used data storage utilization in all databases in the pool, as a percentage of pool storage limit (maximum data size). |
|**allocated_storage_percent**|**decimal(5,2)**| The amount of storage allocated for all data files in all databases in the pool, as a percentage of pool storage limit (maximum data size). |
  
## Remarks

The dynamic management view `sys.dm_elastic_pool_resource_stats` exists in every database, including single databases. You must be connected to a user database in an elastic pool to obtain elastic pool resource utilization data from this view. 
  
## Permissions

This view requires VIEW SERVER STATE permission.
  
## Examples

The following example returns resource utilization data ordered by the most recent time for the elastic pool containing the current database.
  
```sql
SELECT end_time, avg_cpu_percent, avg_data_io_percent, avg_log_write_percent, max_worker_percent, max_session_percent
, avg_instance_memory_percent, avg_instance_cpu_percent, avg_edtu_percent, instance_vcores
, used_storage_mb, allocated_storage_mb, storage_limit_mb, used_storage_percent, allocated_storage_percent
FROM sys.dm_elastic_pool_resource_stats
ORDER BY end_time DESC;  
```

## Next steps

Learn more about elastic pools and related concepts in the following articles:

- [sys.elastic_pool_resource_stats (Azure SQL Database)](../system-catalog-views/sys-elastic-pool-resource-stats-azure-sql-database.md)
- [Elastic pools help you manage and scale multiple databases in Azure SQL Database](/azure/azure-sql/database/elastic-pool-overview)
- [sys.resource_stats (Azure SQL Database)](../system-catalog-views/sys-resource-stats-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview)
