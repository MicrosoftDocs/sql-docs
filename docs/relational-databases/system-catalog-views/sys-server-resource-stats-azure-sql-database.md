---
title: "sys.server_resource_stats (Azure SQL Managed Instance)"
description: sys.server_resource_stats returns CPU usage, IO, and storage data for Azure SQL Managed Instance.
author: danimir
ms.author: danil
ms.reviewer: mathoma, urmilano, wiassaf
ms.date: "03/09/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "server_resource_stats"
  - "sys.server_resource_stats"
  - "sys.server_resource_stats_TSQL"
  - "server_resource_stats_TSQL"
helpviewer_keywords:
  - "sys.server_resource_stats"
  - "server_resource_stats"
dev_langs:
  - "TSQL"
monikerRange: " = azuresqldb-current || = azuresqldb-mi-current"
---
# sys.server_resource_stats (Azure SQL Managed Instance)

[!INCLUDE[Azure SQL Managed Instance](../../includes/applies-to-version/_asmi.md)]

Returns CPU usage, IO, and storage data for Azure SQL Managed Instance. The data is collected, aggregated and updated within 5 to 10 minutes intervals. There is one row for every 15 seconds reporting. The data returned includes CPU usage, storage size, IO utilization, and SKU. Historical data is retained for approximately 14 days.

The `sys.server_resource_stats` view has different definitions depending on the version of the Azure SQL Managed Instance that the database is associated with. Consider these differences and any modifications your application requires when upgrading to a new server version.

> [!NOTE]
> This dynamic management view applies to Azure SQL Managed Instance only. For an equivalent view for Azure SQL Database, use [sys.resource_stats](sys-resource-stats-azure-sql-database.md).

The following table describes the columns available:  
  
|Columns|Data Type|Description|  
|----------------------------|---------------|-----------------|  
|start_time|**datetime2**|UTC time indicating the start of the fifteen-second reporting interval|  
|end_time|**datetime**|UTC time indicating the end of the fifteen-second reporting interval|
|resource_type|Nvarchar(128)|Type of the resource for which metrics are provided|
|resource_name|nvarchar(128)|Name of the resource.|
|sku|nvarchar(128)|Managed Instance Service Tier of the Instance. The following are the possible values: <br><ul><li>General Purpose</li></ul><ul><li>Business Critical</li></ul>|
|hardware_generation|nvarchar(128)|Hardware generation identifier: such as Gen 4 or Gen 5|
|virtual_core_count|int|Represents number of virtual cores per instance|
|avg_cpu_percent|decimal(5,2)|Average compute utilization in percentage of the limit of the Managed Instance service tier utilized by the instance. It is calculated as sum of CPU time of all resource pools for all databases in the instance and divided by available CPU time for that tier in the given interval.|
|reserved_storage_mb|bigint|Reserved storage per instance (amount of storage space that customer purchased for the managed instance)|
|storage_space_used_mb|decimal(18,2)|Storage used by all database files in a managed instance (including both user and system databases)|
|io_request|bigint|Total number of i/o physical operations within the interval|
|io_bytes_read|bigint|Number of physical bytes read within the interval|
|io_bytes_written|bigint|Number of physical bytes written within the interval|

 
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Managed Instance service tiers](/azure/azure-sql/managed-instance/service-tiers-managed-instance-vcore).  
    
## Permissions  
 Querying a dynamic management view requires **VIEW SERVER STATE** permissions. 

## Remarks  
 The data returned by `sys.server_resource_stats` are expressed as the total used in either bytes or megabytes (stated in column names) other than `avg_cpu`, which is expressed as a percentage of the maximum allowed limits for the service tier/performance level that you are running.  
 
> [!NOTE]
> For more information on troubleshooting CPU utilization using dynamic management views, see [Identify CPU performance issues in Microsoft Azure SQL Managed Instance performance with DMVs](/azure/azure-sql/managed-instance/monitoring-with-dmvs#identify-cpu-performance-issues). 

## Examples  
The following example returns the average CPU usage over the last seven days.  
  
```sql  
DECLARE @s datetime;  
DECLARE @e datetime;  
SET @s= DateAdd(d,-7,GetUTCDate());  
SET @e= GETUTCDATE();  
SELECT AVG(avg_cpu_percent) AS Average_Compute_Utilization   
FROM sys.server_resource_stats   
WHERE start_time BETWEEN @s AND @e;
GO
```  
    
## See also  
 - [Managed Instance Compute Hardware in the vCore Service Tier](/azure/azure-sql/managed-instance/service-tiers-managed-instance-vcore)
 - [Managed Instance Resource Limits](/azure/azure-sql/managed-instance/resource-limits)
 - [sys.dm_os_out_of_memory_events (Azure SQL Database and Azure SQL Managed Instance)](../system-dynamic-management-views/sys-dm-os-out-of-memory-events.md)

## Next steps
 - [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview)

