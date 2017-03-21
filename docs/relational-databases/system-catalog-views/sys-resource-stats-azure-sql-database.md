---
title: "sys.resource_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "05/23/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "resource_stats"
  - "sys.resource_stats"
  - "sys.resource_stats_TSQL"
  - "resource_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.resource_stats"
  - "resource_stats"
ms.assetid: 02379a1b-3622-4578-8c59-a1b8f1a17914
caps.latest.revision: 28
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# sys.resource_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns CPU usage and storage data for an Azure SQL Database. The data is collected and aggregated within five-minute intervals. For each user database, there is one row for every five-minute reporting window in which there is change in resource consumption. This includes CPU usage, storage size change or database SKU modification. Idle databases with no changes may not have rows for every five minute interval. Historical data is retained for approximately 14 days.  
  
 The **sys.resource_stats** view has different definitions depending on the version of the Azure SQL Database Server that the database is associated with. Consider these differences and any modifications your application requires when upgrading to a new server version.  
  
 The following table describes the columns available in a v12 server:  
  
|Columns (v12 server)|Data Type|Description|  
|----------------------------|---------------|-----------------|  
|start_time|**datetime**|UTC time indicating the start of the 5 minute reporting interval.|  
|end_time|**datetime**|UTC time indicating the end of the 5 minute reporting interval.|  
|database_name|**varchar**|Name of the user database.|  
|sku|**varchar**|Service Tier of the database. The following are the possible values:<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium|  
|storage_in_megabytes|**float**|Maximum storage size in megabytes for the time period, including database data, indexes, stored procedures and metadata.|  
|avg_cpu_percent|**numeric**|Average compute utilization in percentage of the limit of the service tier.|  
|avg_data_io_percent|**numeric**|Average I/O utilization in percentage based on the limit of the service tier.|  
|avg_log_write_percent|**numeric**|Average write resource utilization in percentage of the limit of the service tier.|  
|max_worker_percent|**decimal(5,2)**|Maximum concurrent workers (requests) in percentage based on the limit of the database’s service tier.<br /><br /> Maximum is currently calculated for the 5 minute interval based on the 15 second samples of concurrent worker counts.|  
|max_session_percent|**decimal(5,2)**|Maximum concurrent sessions in percentage based on the limit of the database’s service tier.<br /><br /> Maximum is currently calculated for the 5 minute interval based on the 15 second samples of concurrent session counts.|  
|dtu_limit|**int**|Current max database DTU setting for this database during this interval.|  
  
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/) and [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/).  
  
 The following table describes the columns available in a v11 server:  
  
|Columns (v11 server)|Data Type|Description|  
|----------------------------|---------------|-----------------|  
|start_time|**datetime**|UTC time indicating the start of the 5 minute reporting interval.|  
|end_time|**datetime**|UTC time indicating the end of the 5 minute reporting interval.|  
|database_name|**nvarchar**|Name of the database.|  
|sku|**nvarchar**|Service Tier of the database. The following are the possible values:<br /><br /> Web<br /><br /> Business<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium|  
|usage_in_seconds|**int**|*Note: This column is deprecated for v11 and its value is always set to 0.*<br /><br /> CPU time used since the last measurement was taken.<br /><br /> For CPU measurement, we recommend that you use the **avg_cpu_cores_used** column rather than this column.|  
|storage_in_megabytes|**decimal**|Maximum storage size in megabytes for the time period, including database data, indexes, stored procedures and metadata.|  
|avg_cpu_cores_used|**decimal**|*Note: This column is deprecated for v11 and its value is always set to 0.*<br /><br /> Average CPU cores used in this interval.|  
|avg_physical_read_iops|**decimal**|*Note: This column is deprecated for v11 and its value is always set to 0.*<br /><br /> Average read IOPS in this interval.|  
|avg_physical_write_iops|**decimal**|*Note: This column is deprecated for v11 and its value is always set to 0.*<br /><br /> Average write IOPS in this interval.|  
|active_memory_used_kb|**bigint**|*Note: This column is deprecated for v11 and its value is always set to 0.*<br /><br /> Count of active memory being used at the end of this interval.|  
|active_session_count|**int**|Count of active sessions at the end of this interval.|  
|active_worker_count|**int**|Count of active workers at the end of this interval.|  
|avg_cpu_percent|**decimal**|Average compute utilization in percentage of the limit of the service tier.|  
|avg_physical_data_read_percent|**decimal**|Average I/O utilization in percentage based on the limit of the service tier.|  
|avg_log_write_percent|**decimal**|Average write resource utilization in percentage of the limit of the service tier.|  
  
## Permissions  
 This view is available to all user roles with permissions to connect to the virtual **master** database.  
  
## Remarks  
 The data returned by **sys.resource_stats** is expressed as a percentage of the maximum allowed DTU limits for the service tier/performance level that you are running for Basic, Standard, and Premium databases.  For Web and Business tiers, these numbers indicate the percentages in terms of the Standard S2 performance tier.  For example, when executing against a Web database, if avg_cpu_percent returns 70%, that indicates 70% of the S2 tier limit. In addition, for Web and Business tiers, the percentages may reflect a number in excess of 100%, which is also based on the S2 tier limit.  
  
 When a database is a member of an elastic pool, resource statistics presented as percent values, are expressed as the percent of the max DTU limit for the databases as set in the elastic pool configuration.  
  
 For a more granular view of this data, use **sys.dm_db_resource_stats** dynamic management view in a user database. This view captures data every 15 seconds and maintains historical data for 1 hour.  For more information, see [sys.dm_db_resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database.md).  

## Examples  
 The following example returns all databases that are averaging at least 80% of compute utilization over the last one week.  
  
```  
DECLARE @s datetime;  
DECLARE @e datetime;  
SET @s= DateAdd(d,-7,GetUTCDate());  
SET @e= GETUTCDATE();  
SELECT database_name, AVG(avg_cpu_percent) AS Average_Compute_Utilization   
FROM sys.resource_stats   
WHERE start_time BETWEEN @s AND @e  
GROUP BY database_name  
HAVING AVG(avg_cpu_percent) >= 80  
```  
  
 The following example calculates the average DTU percentage consumption for a given database. (This query only works when run against a v11 server.)  
  
```  
SELECT start_time, end_time,      
  (SELECT Max(v)      
   FROM (VALUES (avg_cpu_percent), (avg_physical_data_read_percent), (avg_log_write_percent)) AS value(v)) AS [avg_DTU_percent]    
FROM sys.resource_stats   
WHERE database_name = '<your db name>'   
ORDER BY end_time DESC;  
  
```  
  
## See Also  
 [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/)   
 [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/)  
  
  
