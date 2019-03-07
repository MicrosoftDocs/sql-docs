---
title: "sys.resource_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "09/13/2018"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "language-reference"
f1_keywords: 
  - resource_stats
  - sys.resource_stats
  - sys.resource_stats_TSQL
  - resource_stats_TSQL
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.resource_stats"
  - "resource_stats"
ms.assetid: 02379a1b-3622-4578-8c59-a1b8f1a17914
author: CarlRabeler
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.resource_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns CPU usage and storage data for an Azure SQL Database. The data is collected and aggregated within five-minute intervals. For each user database, there is one row for every five-minute reporting window in which there is a change in resource consumption. The data returned includes CPU usage, storage size change, and database SKU modification. Idle databases with no changes may not have rows for every five-minute interval. Historical data is retained for approximately 14 days.  
  
 The **sys.resource_stats** view has different definitions depending on the version of the Azure SQL Database Server that the database is associated with. Consider these differences and any modifications your application requires when upgrading to a new server version.  
  
 The following table describes the columns available in a v12 server:  
  
|Columns|Data Type|Description|  
|----------------------------|---------------|-----------------|  
|start_time|**datetime**|UTC time indicating the start of the five-minute reporting interval.|  
|end_time|**datetime**|UTC time indicating the end of the five-minute reporting interval.|  
|database_name|**varchar**|Name of the user database.|  
|sku|**varchar**|Service Tier of the database. The following are the possible values:<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium<br /><br />General Purpose<br /><br />Business Critical|  
|storage_in_megabytes|**float**|Maximum storage size in megabytes for the time period, including database data, indexes, stored procedures, and metadata.|  
|avg_cpu_percent|**numeric**|Average compute utilization in percentage of the limit of the service tier.|  
|avg_data_io_percent|**numeric**|Average I/O utilization in percentage based on the limit of the service tier.|  
|avg_log_write_percent|**numeric**|Average write resource utilization in percentage of the limit of the service tier.|  
|max_worker_percent|**decimal(5,2)**|Maximum concurrent workers (requests) in percentage based on the limit of the database's service tier.<br /><br /> Maximum is currently calculated for the five-minute interval based on the 15-second samples of concurrent worker counts.|  
|max_session_percent|**decimal(5,2)**|Maximum concurrent sessions in percentage based on the limit of the database's service tier.<br /><br /> Maximum is currently calculated for the five-minute interval based on the 15-second samples of concurrent session counts.|  
|dtu_limit|**int**|Current max database DTU setting for this database during this interval. |  
|allocated_storage_in_megabytes|**float**|The amount of formatted file space in MB made available for storing database data. Formatted file space is also referred to as data space allocated.  For more information, see: [File space management in SQL DB](https://docs.microsoft.com/azure/sql-database/sql-database-file-space-management)|
  
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/).  
    
## Permissions  
 This view is available to all user roles with permissions to connect to the virtual **master** database.  
  
## Remarks  
 The data returned by **sys.resource_stats** is expressed as a percentage of the maximum allowed limits for the service tier/performance level that you are running.  
  
 When a database is a member of an elastic pool, resource statistics presented as percent values, are expressed as the percent of the max limit for the databases as set in the elastic pool configuration.  
  
 For a more granular view of this data, use **sys.dm_db_resource_stats** dynamic management view in a user database. This view captures data every 15 seconds and maintains historical data for 1 hour.  For more information, see [sys.dm_db_resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database.md).  

## Examples  
 The following example returns all databases that are averaging at least 80% of compute utilization over the last one week.  
  
```sql  
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
    
## See Also  
 [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/)   
 [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/)  
  
  
