---
title: "SQL Server, HTTP_STORAGE_OBJECT | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: ae849f79-c581-42a5-a5cc-0a9ebea171b9
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, HTTP Storage
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLServer:HTTP Storage** performance object consists of performance counters that monitor a Microsoft Azure Storage account. Using [SQL Server Data Files in Microsoft Azure](../../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md) feature, you can store database files in Windows Azure Storage Blobs. This performance object treats each Windows Azure Storage account as a different drive.  
  
|Counter Name|Description|  
|------------------|-----------------|  
|**Avg. Bytes/Read**|Average number of bytes transferred from the HTTP storage per read.|  
|**Avg. Bytes/Transfer**|Average number of bytes transferred from the HTTP storage during read or write operations.|  
|**Avg. Bytes/Write**|Average number of bytes transferred from the HTTP storage per write.|  
|**Avg. microsec/Read**|The average number of microseconds it takes to do each read from the HTTP storage.|  
|**Avg. microsec/Read Comp**|The average number of microseconds it takes for HTTP to complete the read to storage.| 
|**Avg. microsec/Transfer**|The average number of microseconds it takes to do each transfer to the HTTP storage.|  
|**Avg. microsec/Write**|The average number of microseconds it takes to do each write to the HTTP storage.|  
|**Avg. microsec/Write Comp**|The average number of microseconds it takes for HTTP to complete the write to storage.|  
|**HTTP Storage IO failed/sec**|Number of failed write requests sent to the HTTP storage per second.| 
|**HTTP Storage I/O retry/sec**|Number of retry requests sent to the HTTP storage per second.|  
|**Outstanding HTTP Storage I/O**|The total number of outstanding I/Os towards a HTTP storage.|  
|**Read Bytes/sec**|Amount of data being transferred from the HTTP storage per second during read operations.|  
|**Reads/sec**|Number of reads per second on the HTTP storage.|  
|**Total Bytes/sec**|Amount of data being transferred from the HTTP storage per second during read or write operations.|  
|**Transfers/sec**|Number of read and write operations per second on the HTTP storage.|  
|**Write Bytes/sec**|Amount of data being transferred from the HTTP storage per second during write operations.|  
|**Writes/sec**|Number of writer per second on the HTTP storage.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
