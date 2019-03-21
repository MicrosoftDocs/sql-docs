---
title: "sys.dm_db_resource_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "08/14/2018"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_resource_stats"
  - "sys.dm_db_resource_stats_TSQL"
  - "dm_db_resource_stats"
  - "dm_db_resource_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_resource_stats"
  - "dm_db_resource_stats"
ms.assetid: 6e76b39f-236e-4bbf-b0b5-38be190d81e8
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.dm_db_resource_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns CPU, I/O, and memory consumption for an [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] database. One row exists for every 15 seconds, even if there is no activity in the database. Historical data is maintained for one hour.  
  
|Columns|Data Type|Description|  
|-------------|---------------|-----------------|  
|end_time|**datetime**|UTC time indicates the end of the current reporting interval.|  
|avg_cpu_percent|**decimal (5,2)**|Average compute utilization in percentage of the limit of the service tier.|  
|avg_data_io_percent|**decimal (5,2)**|Average data I/O utilization in percentage of the limit of the service tier.|  
|avg_log_write_percent|**decimal (5,2)**|Average write I/O throughput utilization as percentage of the limit of the service tier.|  
|avg_memory_usage_percent|**decimal (5,2)**|Average memory utilization in percentage of the limit of the service tier.<br /><br /> This includes memory used for  buffer pool pages and storage of In-Memory OLTP objects.|  
|xtp_storage_percent|**decimal (5,2)**|Storage utilization for In-Memory OLTP in percentage of the limit of the service tier (at the end of the reporting interval). This includes memory used for storage of the following In-Memory OLTP objects: memory-optimized tables, indexes, and table variables. It also includes memory used for processing ALTER TABLE operations.<br /><br /> Returns 0 if In-Memory OLTP is not used in the database.|  
|max_worker_percent|**decimal (5,2)**|Maximum concurrent workers (requests) in percentage of the limit of the database's service tier.|  
|max_session_percent|**decimal (5,2)**|Maximum concurrent sessions in percentage of the limit of the database's service tier.|  
|dtu_limit|**int**|Current max database DTU setting for this database during this interval. For databases using the vCore-based model, this column is NULL.|
|cpu_limit|**decimal (5,2)**|Number of vCores for this database during this interval. For databases using the DTU-based model, this column is NULL.|
|||
  
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/) and [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/).  
  
## Permissions  
 This view requires VIEW DATABASE STATE permission.  
  
## Remarks  
 The data returned by **sys.dm_db_resource_stats** is expressed as a percentage of the maximum allowed limits for the service tier/performance level that you are running.
 
 If the database was failed over to another server within the last 60 minutes, the view will only return data for the time it has been the primary database since that failover.  
  
 For a less granular view of this data, use **sys.resource_stats** catalog view in the **master** database. This view captures data every 5 minutes and maintains historical data for 14 days.  For more information, see [sys.resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database.md).  
  
 When a database is a member of an elastic pool, resource statistics presented as percent values, are expressed as the percent of the max limit for the databases as set in the elastic pool configuration.  
  
## Example  
  
The following example returns resource utilization data ordered by the most recent time for the currently connected database.  
  
```  
SELECT * FROM sys.dm_db_resource_stats ORDER BY end_time DESC;  
  
```  
  
 The following example identifies the average DTU consumption in terms of a percentage of the maximum allowed DTU limit in the performance level for the user database over the past hour. Consider increasing the performance level as these percentages near 100% on a consistent basis.  
  
```  
SELECT end_time,   
  (SELECT Max(v)    
   FROM (VALUES (avg_cpu_percent), (avg_data_io_percent), (avg_log_write_percent)) AS    
   value(v)) AS [avg_DTU_percent]   
FROM sys.dm_db_resource_stats;  
  
```  
  
 The following example returns the average and maximum values for CPU percent, data and log I/O, and memory consumption over the last hour.  
  
```  
SELECT    
    AVG(avg_cpu_percent) AS 'Average CPU Utilization In Percent',   
    MAX(avg_cpu_percent) AS 'Maximum CPU Utilization In Percent',   
    AVG(avg_data_io_percent) AS 'Average Data IO In Percent',   
    MAX(avg_data_io_percent) AS 'Maximum Data IO In Percent',   
    AVG(avg_log_write_percent) AS 'Average Log Write I/O Throughput Utilization In Percent',   
    MAX(avg_log_write_percent) AS 'Maximum Log Write I/O Throughput Utilization In Percent',   
    AVG(avg_memory_usage_percent) AS 'Average Memory Usage In Percent',   
    MAX(avg_memory_usage_percent) AS 'Maximum Memory Usage In Percent'   
FROM sys.dm_db_resource_stats;  
  
```  
  
## See Also  
 [sys.resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database.md)   
 [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/)   
 [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/)  
  
  
