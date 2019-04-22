---
title: "sys.server_resource_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2018"
ms.service: sql-database
ms.reviewer: carlrab, edmaca
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
ms.assetid: 
author: jovanpop-msft
ms.author: jovanpop
manager: craigg
monikerRange: "=azuresqldb-current||=sqlallproducts-allversions"
---
# sys.server_resource_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

Returns CPU usage, IO, and storage data for an Azure SQL Managed Instance. The data is collected and aggregated within five-minute intervals. There is one row for every 15 seconds reporting. The data returned includes CPU usage, storage size, IO utilization, and managed instance SKU. Historical data is retained for approximately 14 days.

The **sys.server_resource_stats** view has different definitions depending on the version of the Azure SQL managed instance that the database is associated with. Consider these differences and any modifications your application requires when upgrading to a new server version.
 
  
 The following table describes the columns available in a v12 server:  
  
|Columns|Data Type|Description|  
|----------------------------|---------------|-----------------|  
|start_time|**datetime2**|UTC time indicating the start of the fifteen-second reporting interval|  
|end_time|**datetime**|UTC time indicating the end of the fifteen-second reporting interval|
|resource_type|Nvarchar(128)|Type of the resource for which metrics are provided|
|resource_name|nvarchar(128)|Name of the resource.|
|sku|nvarchar(128)|Managed Instance Service Tier of the Instance. The following are the possible values: <br><ul><li>General Purpose</li></ul><ul><li>Business Critical</li></ul>|
|hardware_generation|nvarchar(128)|Hardware generation identifier: such as Gen 4 or Gen 5|
|virtual_core_count|int|Represents number of virtual cores per instance (8, 16 or 24 in Public Preview)|
|avg_cpu_percent|decimal(5,2)|Average compute utilization in percentage of the limit of the Managed Instance service tier utilized by the instance. It is calculated as sum of CPU time of all resource pools for all databases in the instance and divided by available CPU time for that tier in the given interval.|
|reserved_storage_mb|bigint|Reserved storage per instance (amount of storage space that customer purchased for the managed instance)|
|storage_space_used_mb|decimal(18,2)|Storage used by all managed instance databases' files (including both user and system databases)|
|io_request|bigint|Total number of i/o physical operations within the interval|
|io_bytes_read|bigint|Number of physical bytes read within the interval|
|io_bytes_written|bigint|Number of physical bytes written within the interval|

 
> [!TIP]  
>  For more context about these limits and service tiers, see the topics [Managed Instance service tiers](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance#managed-instance-service-tier).  
    
## Permissions  
 This view is available to all user roles with permissions to connect to the **master** database.  
  
## Remarks  
 The data returned by **sys.server_resource_stats** are expressed as the total used in either bytes or megabytes (stated in column names) other than avg_cpu, which is expressed as a percentage of the maximum allowed limits for the service tier/performance level that you are running.  
 
## Examples  
 The following example returns all databases that are averaging at least 80% of compute utilization over the last one week.  
  
```sql  
DECLARE @s datetime;  
DECLARE @e datetime;  
SET @s= DateAdd(d,-7,GetUTCDate());  
SET @e= GETUTCDATE();  
SELECT resource_name, AVG(avg_cpu_percent) AS Average_Compute_Utilization   
FROM sys.server_resource_stats   
WHERE start_time BETWEEN @s AND @e  
GROUP BY resource_name  
HAVING AVG(avg_cpu_percent) >= 80  
```  
    
## See Also  
 [Managed Instance service tiers](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance#managed-instance-service-tier)
