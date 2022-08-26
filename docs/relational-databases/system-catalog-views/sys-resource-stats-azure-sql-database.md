---
title: "sys.resource_stats (Azure SQL Database)"
description: sys.resource_stats returns CPU usage and storage data for an Azure SQL Database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: "4/18/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "resource_stats"
  - "sys.resource_stats"
  - "sys.resource_stats_TSQL"
  - "resource_stats_TSQL"
helpviewer_keywords:
  - "sys.resource_stats"
  - "resource_stats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.resource_stats (Azure SQL Database)
[!INCLUDE[Azure SQL Database Azure](../../includes/applies-to-version/asdb.md)]

Returns CPU usage and storage data for a database in Azure SQL Database. The data is collected and aggregated within five-minute intervals. For each user database, there is one row for every five-minute reporting window in which there is a change in resource consumption. The data returned includes CPU usage, storage size change, and database SKU modification. Idle databases with no changes may not have rows for every five-minute interval. Historical data is retained for approximately 14 days.  

> [!NOTE]
> This dynamic management view applies to Azure SQL Database only. For an equivalent view for Azure SQL Managed Instance, use [sys.server_resource_stats](sys-server-resource-stats-azure-sql-database.md).

|Columns|Data Type|Description|  
|----------------------------|---------------|-----------------|  
|start_time|**datetime**|UTC time indicating the start of the five-minute reporting interval.|  
|end_time|**datetime**|UTC time indicating the end of the five-minute reporting interval.|  
|database_name|**nvarchar(128)**|Name of the user database.|  
|sku|**nvarchar(128)**|Service Tier of the database. The following are the possible values:<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium<br /><br />General Purpose<br /><br />Business Critical|  
|storage_in_megabytes|**float**|Maximum storage size in megabytes for the time period, including database data, indexes, stored procedures, and metadata.|  
|avg_cpu_percent|**decimal(5,2)**|Average compute utilization in percentage of the limit of the service tier.|  
|avg_data_io_percent|**decimal(5,2)**|Average I/O utilization in percentage based on the limit of the service tier. For Hyperscale databases, see [Data IO in resource utilization statistics](/azure/sql-database/sql-database-hyperscale-performance-diagnostics#data-io-in-resource-utilization-statistics).|  
|avg_log_write_percent|**decimal(5,2)**|Average write resource utilization in percentage of the limit of the service tier.|  
|max_worker_percent|**decimal(5,2)**|Maximum concurrent workers (requests) in percentage based on the limit of the database's service tier.<br /><br /> Maximum is currently calculated for the five-minute interval based on the 15-second samples of concurrent worker counts.|  
|max_session_percent|**decimal(5,2)**|Maximum concurrent sessions in percentage based on the limit of the database's service tier.<br /><br /> Maximum is currently calculated for the five-minute interval based on the 15-second samples of concurrent session counts.|  
|dtu_limit|**int**|Current max database DTU setting for this database during this interval. |
|xtp_storage_percent|**decimal (5,2)**|Storage utilization for In-Memory OLTP in percentage of the limit of the service tier (at the end of the reporting interval). This includes memory used for storage of the following In-Memory OLTP objects: memory-optimized tables, indexes, and table variables. It also includes memory used for processing ALTER TABLE operations. For more information, see [Monitor In-Memory OLTP](/azure/azure-sql/in-memory-oltp-monitor-space).<br /><br /> Returns 0 if In-Memory OLTP is not used in the database.|
|avg_login_rate_percent|**decimal (5,2)**|Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.|
|avg_instance_cpu_percent|**decimal (5,2)**|Average database CPU usage as a percentage of the SQL Database process.|
|avg_instance_memory_percent|**decimal (5,2)**|Average database memory usage as a percentage of the SQL Database process.|
|cpu_limit|**decimal (5,2)**|Number of vCores for this database during this interval. For databases using the DTU-based model, this column is NULL.|
|allocated_storage_in_megabytes|**float**|The amount of formatted file space in MB made available for storing database data. Formatted file space is also referred to as data space allocated.  For more information, see: [File space management in SQL Database](/azure/sql-database/sql-database-file-space-management)|
  
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Service Tiers](/azure/azure-sql/database/purchasing-models).  
    
## Permissions  
 In Azure SQL Database, this view is available to all user roles with permissions to connect to the virtual `master` database. 
   
## Remarks  
 The data returned by `sys.resource_stats` is expressed as a percentage of the maximum allowed limits for the service tier/performance level that you are running.  
  
 When a database is a member of an elastic pool, resource statistics presented as percent values, are expressed as the percent of the max limit for the databases as set in the elastic pool configuration.  
  
 For a more granular view of this data, use `sys.dm_db_resource_stats` dynamic management view in a user database. This view captures data every 15 seconds and maintains historical data for 1 hour. For more information, see [sys.dm_db_resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database.md).  

> [!NOTE]
> For more information on troubleshooting CPU utilization using dynamic management views, see [Identify CPU performance issues in Microsoft Azure SQL Database and Azure SQL Managed Instance performance](/azure/azure-sql/database/monitoring-with-dmvs#identify-cpu-performance-issues). 

 To review recent out of memory events, use [sys.dm_os_out_of_memory_events](../system-dynamic-management-views/sys-dm-os-out-of-memory-events.md).

## Examples  

You must be connected to the `master` database on the [logical server](/azure/azure-sql/database/logical-servers) to query `sys.resource_stats`.

The following example returns all databases that are averaging at least 80% of CPU utilization over the last one week.  
  
```sql  
DECLARE @s datetime;  
DECLARE @e datetime;  
SET @s= DateAdd(d,-7,GetUTCDate());  
SET @e= GETUTCDATE();  

SELECT database_name, AVG(avg_cpu_percent) AS Average_CPU_Utilization   
FROM sys.resource_stats   
WHERE start_time BETWEEN @s AND @e  
GROUP BY database_name  
HAVING AVG(avg_cpu_percent) >= 80;
GO
```  
    
## See also  

 - [Service Tiers](/azure/azure-sql/database/purchasing-models)   
 - [Service tier capabilities and limits](/azure/azure-sql/database/performance-guidance) 
 - [sys.dm_os_out_of_memory_events (Azure SQL Database and Azure SQL Managed Instance)](../system-dynamic-management-views/sys-dm-os-out-of-memory-events.md)

## Next steps

Learn more about related concepts in the following articles:

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview)
- [sys.resource_usage (Azure SQL Database and Azure SQL Managed Instance)](sys-resource-usage-azure-sql-database.md)
- [sys.dm_db_resource_stats (Azure SQL Database and Azure SQL Managed Instance)](../system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database.md)
