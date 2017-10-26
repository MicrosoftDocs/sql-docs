---
title: "sys.dm_db_resource_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "03/16/2016"
ms.prod: 
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 11
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# sys.dm_db_resource_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns CPU, I/O, and memory consumption for an [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] database. One row exists for every 15 seconds, even if there is no activity in the database. Historical data is maintained for one hour.  
  
|Columns|Data Type|Description|  
|-------------|---------------|-----------------|  
|end_time|**datetime**|UTC time indicates the end of the current reporting interval.|  
|avg_cpu_percent|**decimal (5,2)**|Average compute utilization in percentage of the limit of the service tier.|  
|avg_data_io_percent|**decimal (5,2)**|Average data I/O utilization in percentage of the limit of the service tier.|  
|avg_log_write_percent|**decimal (5,2)**|Average write resource utilization in percentage of the limit of the service tier.|  
|avg_memory_percent|**decimal (5,2)**|Average memory utilization in percentage of the limit of the service tier.<br /><br /> This includes memory used for storage of In-Memory OLTP objects.|  
|xtp_storage_percent|**decimal (5,2)**|Storage utilization for In-Memory OLTP in percentage of the limit of the service tier (at the end of the reporting interval). This includes memory used for storage of the following In-Memory OLTP objects: memory-optimized tables, indexes, and table variables. It also includes memory used for processing ALTER TABLE operations.<br /><br /> Returns 0 if In-Memory OLTP is not used in the database.|  
|max_worker_percent|**decimal (5,2)**|Maximum concurrent workers (requests) in percentage of the limit of the database’s service tier.|  
|max_session_percent|**decimal (5,2)**|Maximum concurrent sessions in percentage of the limit of the database’s service tier.|  
|dtu_limit|**int**|Current max database DTU setting for this database during this interval.|  
  
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/) and [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/).  
  
## Permissions  
 This view requires VIEW DATABASE STATE permission.  
  
## Remarks  
 The data returned by **sys.dm_db_resource_stats** is expressed as a percentage of the maximum allowed DTU limits for the service tier/performance level that you are running for Basic, Standard, and Premium databases. For Web and Business tiers, these numbers indicate the percentages in terms of the Standard S2 performance tier. For example, when executing against a Web or Business database, if avg_cpu_percent returns 70%, that indicates 70% of the S2 tier limit. In addition, for Web and Business tiers, the percentages may reflect a number in excess of 100%, which is also based on the S2 tier limit.  
  
 If the database was failed over to another server within the last 60 minutes, the view will only return data for the time it has been the primary database since that failover.  
  
 For a less granular view of this data, use **sys.resource_stats** catalog view in the **master** database. This view captures data every 5 minutes and maintains historical data for 14 days.  For more information, see [sys.resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database.md).  
  
 When a database is a member of an elastic pool, resource statistics presented as percent values, are expressed as the percent of the max DTU limit for the databases as set in the elastic pool configuration.  
  
## Example  
  
> [!NOTE]  
>  For Web and Business tiers, these numbers indicate the percentages in terms of the Standard S2 performance tier. For example, when executing against a Web or Business database, if avg_cpu_percent returns 70%, that indicates 70% of the S2 tier limit. In addition, for Web and Business tiers, the percentages may reflect a number in excess of 100%, which is also based on the S2 tier limit.  
  
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
    AVG(avg_log_write_percent) AS 'Average Log Write Utilization In Percent',   
    MAX(avg_log_write_percent) AS 'Maximum Log Write Utilization In Percent',   
    AVG(avg_memory_usage_percent) AS 'Average Memory Usage In Percent',   
    MAX(avg_memory_usage_percent) AS 'Maximum Memory Usage In Percent'   
FROM sys.dm_db_resource_stats;  
  
```  
  
## See Also  
 [sys.resource_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database.md)   
 [Service Tiers](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/)   
 [Service tier capabilities and limits](https://azure.microsoft.com/documentation/articles/sql-database-performance-guidance/)  
  
  
