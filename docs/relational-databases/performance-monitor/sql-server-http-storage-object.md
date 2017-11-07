---
title: "SQL Server, HTTP_STORAGE_OBJECT | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ae849f79-c581-42a5-a5cc-0a9ebea171b9
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# SQL Server, HTTP_STORAGE_OBJECT
  The **SQLServer:HTTP_STORAGE_OBJECT** performance object consists of performance counters that monitor Windows Azure Storage account. Using [SQL Server Data Files in Microsoft Azure](../../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md) feature, you can store database files in Windows Azure Storage Blobs. This performance object treats each Windows Azure Storage account as a different drive.  
  
|Counter Name|Description|  
|------------------|-----------------|  
|**Read Bytes/sec**|Amount of data being transferred from the HTTP storage per second during read operations.|  
|**Write Bytes/sec**|Amount of data being transferred from the HTTP storage per second during write operations.|  
|**Total Bytes/sec**|Amount of data being transferred from the HTTP storage per second during read or write operations.|  
|**Reads/sec**|Number of reads per second on the HTTP storage.|  
|**Writes/sec**|Number of writer per second on the HTTP storage.|  
|**Transfers/sec**|Number of read and write operations per second on the HTTP storage.|  
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
|**Avg. microsec/Write**|The average number of microseconds it takes to do each write to the HTTP storage.|  
|**Avg. microsec/Transfer**|The average number of microseconds it takes to do each transfer to the HTTP storage.|  
|**Avg. microsec/Transfer BASE**|For internal use only.|
|**Avg. microsec/Write BASE**|For internal use only.|
|**Avg. microsec/Write Comp**|The average number of microseconds it takes for HTTP to complete the write to storage.|  
|**Avg. microsec/Write Comp BASE**|For internal use only.|
|**Outstanding HTTP Storage I/O**|The total number of outstanding I/Os towards a HTTP storage.|  
|**HTTP Storage IO failed/sec**|Number of failed write requests sent to the HTTP storage per second.| 
|**HTTP Storage I/O Retry/sec**|Number of retry requests sent to the HTTP storage per second.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  