---
title: "sys.elastic_pool_resource_stats"
titleSuffix: Azure SQL Database
description: sys.elastic_pool_resource_stats (Azure SQL Database)
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/11/2022
ms.service: sql-database
ms.topic: "reference"
ms.custom: seo-dt-2019
f1_keywords:
  - "sys.elastic_pool_resource_stats catalog view"
helpviewer_keywords:
  - "sys.elastic_pool_resource_stats_TSQL"
  - "sys.elastic_pool_resource_stats"
  - "elastic_pool_resource_stats_TSQL"
  - "elastic_pool_resource_stats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.elastic_pool_resource_stats (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns resource usage statistics for all the [elastic pools](/azure/azure-sql/database/elastic-pool-overview) in an Azure SQL Database [logical server](/azure/azure-sql/database/logical-servers). For each elastic pool, there is one row for each 15-second reporting window (four rows per minute). This includes CPU, IO, Log, storage consumption and concurrent request/session utilization by all databases in the pool. This data is retained for 14 days. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**start_time**|**datetime2**|UTC time indicating the start of the 15-second reporting interval.|  
|**end_time**|**datetime2**|UTC time indicating the end of the 15-second reporting interval.|  
|**elastic_pool_name**|**nvarchar(128)**|Name of the elastic database pool.|  
|**avg_cpu_percent**|**decimal(5,2)**|Average compute utilization in percentage of the limit of the pool.|  
|**avg_data_io_percent**|**decimal(5,2)**|Average I/O utilization in percentage based on the limit of the pool.|  
|**avg_log_write_percent**|**decimal(5,2)**|Average write resource utilization in percentage of the limit of the pool.|  
|**avg_storage_percent**|**decimal(5,2)**|Average storage utilization in percentage of the storage limit of the pool.|  
|**max_worker_percent**|**decimal(5,2)**|Maximum concurrent workers (requests) in percentage based on the limit of the pool.|  
|**max_session_percent**|**decimal(5,2)**|Maximum concurrent sessions in percentage based on the limit of the pool.|  
|**elastic_pool_dtu_limit**|**int**|Current max elastic pool DTU setting for this elastic pool during this interval.|  
|**elastic_pool_storage_limit_mb**|**bigint**|Current max elastic pool storage limit setting for this elastic pool in megabytes during this interval.|
|**max_xtp_storage_percent**|**decimal(5,2)**|Maximum storage utilization for In-Memory OLTP as a percentage of pool limit at the end of the reporting interval. This includes memory used for storage of the following In-Memory OLTP objects: memory-optimized tables, indexes, and table variables. It also includes memory used for processing ALTER TABLE operations on memory-optimized tables. <BR /><BR />Returns 0 if In-Memory OLTP is not used in any database in the elastic pool.|
|**avg_login_rate_percent**|**decimal(5,2)**| Identified for informational purposes only. Not supported. Future compatibility is not guaranteed. |
|**avg_instance_cpu_percent**|**decimal(5,2)**| Average CPU usage for the database as a percentage of the pool limit at the end of the reporting interval. Includes CPU utilization by both user and internal workloads.|
|**avg_instance_memory_percent**|**decimal(5,2)**|Average database memory usage as a percentage of the pool limit at the end of the reporting interval. |
|**elastic_pool_cpu_limit**|**decimal(5,2)**| Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.|
|**avg_allocated_storage_percent**|**decimal(5,2)**|The percentage of data space allocated by all databases in the elastic pool.  This is the ratio of data space allocated to data max size for the elastic pool.  For more information, visit [File space management in SQL Database](/azure/sql-database/sql-database-file-space-management).|  

  
## Remarks

 This view exists in the `master` database of the [logical server](/azure/azure-sql/database/logical-servers). You must be connected to the `master` database to query `sys.elastic_pool_resource_stats`.  
  
## Permissions

Requires membership in the **dbmanager** role.  
  
## Examples

The following example returns resource utilization data ordered by the most recent time for all the elastic database pools in the current SQL Database [logical server](/azure/azure-sql/database/logical-servers).  
  
```sql
SELECT start_time, end_time, elastic_pool_name, avg_cpu_percent, avg_data_io_percent,
    avg_log_write_percent, avg_storage_percent, max_worker_percent, max_session_percent,
    elastic_pool_dtu_limit, elastic_pool_storage_limit_mb, avg_allocated_storage_percent
FROM sys.elastic_pool_resource_stats
ORDER BY end_time DESC;  
```

The following example calculates the average DTU percentage consumption for a given pool. Replace `<your pool name>` with the name of your pool before running the query:

```sql
SELECT 
    start_time, 
    end_time,
    (SELECT Max(v)
        FROM (VALUES (avg_cpu_percent), (avg_data_io_percent), (avg_log_write_percent)) AS value(v)
        ) AS [avg_DTU_percent]
FROM sys.elastic_pool_resource_stats
WHERE elastic_pool_name = '<your pool name>'
ORDER BY end_time DESC;
```

## Next steps

Learn more about elastic pools and related concepts in the following articles:

- [Elastic pools help you manage and scale multiple databases in Azure SQL Database](/azure/azure-sql/database/elastic-pool-overview)
- [sys.resource_stats (Azure SQL Database)](sys-resource-stats-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview)
