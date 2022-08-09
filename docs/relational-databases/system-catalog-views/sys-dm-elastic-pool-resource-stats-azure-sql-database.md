---
title: "sys.dm_elastic_pool_resource_stats"
titleSuffix: Azure SQL Database
description: "sys.dm_elastic_pool_resource_stats returns resource usage statistics for the current database in an elastic pool in Azure SQL Database."
author: WilliaMDAssafMSFT
ms.author: wiassaf
ms.date: 08/09/2022
ms.service: sql-database
ms.prod_service: "sql-database"
ms.topic: "reference"
ms.custom: seo-dt-2019
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

Returns resource usage statistics for the current database in an [elastic pool](/azure/azure-sql/database/elastic-pool-overview) in an Azure SQL Database [logical server](/azure/azure-sql/database/logical-servers). There is one row for each 20 second reporting window (four rows per minute). This includes CPU, IO, Log, storage consumption and concurrent request/session utilization by the current database. This data is retained for 128 samples, or roughly 42 minutes.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**end_time**|**datetime**|UTC time indicating the end of the 15 second reporting interval.|  
|**avg_cpu_percent**|**decimal(5,2)**|Average compute utilization in percentage of the limit of the pool.|  
|**avg_data_io_percent**|**decimal(5,2)**|Average I/O utilization in percentage based on the limit of the pool.|  
|**avg_log_write_percent**|**decimal(5,2)**|Average write resource utilization in percentage of the limit of the pool.|  
|**avg_storage_percent**|**decimal(5,2)**|Average storage utilization in percentage of the storage limit of the pool.|  
|**max_worker_percent**|**decimal(5,2)**|Maximum concurrent workers (requests) in percentage based on the limit of the pool.|  
|**max_session_percent**|**decimal(5,2)**|Maximum concurrent sessions in percentage based on the limit of the pool.|  
|**avg_instance_memory_percent**|**decimal(5,2)**| |
|**avg_instance_cpu_percent**|**decimal(5,2)**| |
|**avg_edtu_percent**|**decimal(5,2)**| |
|**instance_vcores**|**decimal(5,2)**| |
|**used_storage_mb**|**bigint**||
|**allocated_storage_mb**|**bigint**||
|**storage_limit_mb**|**bigint**||
|**used_storage_percent**|**decimal(5,2)**||
|**allocated_storage_percent**|**decimal(5,2)**||
  
## Remarks

 This view exists in each user database database of the [logical server](/azure/azure-sql/database/logical-servers). You must be connected to the desired user database to query `sys.dm_elastic_pool_resource_stats`.  
  
## Permissions

Requires membership in the **dbmanager** role.  
  
## Examples

The following example returns resource utilization data ordered by the most recent time for the current database in an elastic database pool.
  
```sql
SELECT end_time, avg_cpu_percent, avg_data_io_percent, avg_log_write_percent, avg_storage_percent, max_worker_percent, max_session_percent
, avg_instance_memory_percent, avg_instance_cpu_percent, avg_edtu_percent, instance_vcores
, used_storage_mb, allocated_storage_mb, storage_limit_mb, used_storage_percent, allocated_storage_percent
FROM sys.dm_elastic_pool_resource_stats
ORDER BY end_time DESC;  
```

The following example calculates the average DTU percentage consumption for a given pool. Replace `<your pool name>` with the name of your pool before running the query:

```sql
SELECT 
    end_time,
    (SELECT Max(v)
        FROM (VALUES (avg_cpu_percent), (avg_data_io_percent), (avg_log_write_percent)) AS value(v)
        ) AS [avg_DTU_percent]
FROM sys.dm_elastic_pool_resource_stats
ORDER BY end_time DESC;
```

## Next steps

Learn more about elastic pools and related concepts in the following articles:

- [sys.database_usage (Azure SQL Database)](sys-database-usage-azure-sql-database.md)
- [sys.elastic_pool_resource_stats (Azure SQL Database)](sys-elastic-pool-resource-stats-azure-sql-database.md)
- [Elastic pools help you manage and scale multiple databases in Azure SQL Database](/azure/azure-sql/database/elastic-pool-overview)
- [sys.resource_stats (Azure SQL Database)](sys-resource-stats-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview)
