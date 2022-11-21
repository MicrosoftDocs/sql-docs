---
title: "SQL Server, HTTP Storage object"
description: Learn about the SQLServer:HTTP Storage performance object, which consists of performance counters that monitor an Azure Storage account. 
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, HTTP Storage object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:HTTP Storage** performance object consists of performance counters that monitor a Microsoft Azure Storage account. Using [SQL Server Data Files in Microsoft Azure](../../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md) feature, you can store database files in Azure Storage Blobs. This performance object treats each Azure Storage account as a different drive.  
  
|Counter Name|Description|  
|------------------|-----------------|  
|**Avg. Bytes/Read**|Average number of bytes transferred from the HTTP storage per read.|  
|**Avg. Bytes/Read BASE**|For internal use only.|
|**Avg. Bytes/Transfer**|Average number of bytes transferred from the HTTP storage during read or write operations.|  
|**Avg. Bytes/Transfer BASE**|For internal use only.|
|**Avg. Bytes/Write**|Average number of bytes transferred from the HTTP storage per write.|  
|**Avg. Bytes/Write BASE**|For internal use only.|
|**Avg. microsec/Read**|The average number of microseconds it takes to do each read from the HTTP storage.|  
|**Avg. microsec/Read BASE**|For internal use only.|
|**Avg. microsec/Read Comp**|The average number of microseconds it takes for HTTP to complete the read to storage.| 
|**Avg. microsec/Read Comp BASE**|For internal use only.|
|**Avg. microsec/Transfer**|The average number of microseconds it takes to do each transfer to the HTTP storage.|  
|**Avg. microsec/Transfer BASE**|For internal use only.|
|**Avg. microsec/Write**|The average number of microseconds it takes to do each write to the HTTP storage.|  
|**Avg. microsec/Write BASE**|For internal use only.|
|**Avg. microsec/Write Comp**|The average number of microseconds it takes for HTTP to complete the write to storage.|  
|**Avg. microsec/Write Comp BASE**|For internal use only.|
|**HTTP Storage IO failed/sec**|Number of failed write requests sent to the HTTP storage per second.| 
|**HTTP Storage IO retry/sec**|Number of retry requests sent to the HTTP storage per second.|  
|**Outstanding HTTP Storage IO**|The total number of outstanding I/Os towards a HTTP storage.|  
|**Read Bytes/sec**|Amount of data being transferred from the HTTP storage per second during read operations.|  
|**Reads/sec**|Number of reads per second on the HTTP storage.|  
|**Total Bytes/sec**|Amount of data being transferred from the HTTP storage per second during read or write operations.|  
|**Transfers/sec**|Number of read and write operations per second on the HTTP storage.|  
|**Write Bytes/sec**|Amount of data being transferred from the HTTP storage per second during write operations.|  
|**Writes/sec**|Number of writer per second on the HTTP storage.|  

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%HTTP Storage%';
```  
  
## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
