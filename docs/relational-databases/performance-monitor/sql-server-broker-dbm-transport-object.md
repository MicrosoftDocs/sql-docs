---
title: "SQL Server, Broker - DBM Transport Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Broker / DBM Transport object"
  - "SQLServer:Broker/DBM Transport"
ms.assetid: eddb60b6-20a9-416c-adf3-4bc1687944fa
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Broker - DBM Transport Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Broker / DBM Transport** performance object contains performance counters that report networking information for Service Broker and database mirroring. The table below lists the counters that this object contains.  
  
|SQL Server Broker / DBM Transport counter|Description|  
|------------------------------------------------|-----------------|  
|**Current Bytes for Recv I/O**|This counter reports the number of bytes to be read by the currently running transport receive operations.|  
|**Current Bytes for Send I/O**|This counter reports the number of bytes in message fragments that are currently in the process of being sent over the network.|  
|**Current Msg Frags for Send I/O**|This counter reports the total number of message fragments that are in the process of being sent over the network.|  
|**Message Fragment P1 Sends/sec**|This counter reports the number of priority 1 message fragments sent over the network per second.|  
|**Message Fragment P2 Sends/sec**|This counter reports the number of priority 2 message fragments sent over the network per second.|  
|**Message Fragment P3 Sends/sec**|This counter reports the number of priority 3 message fragments sent over the network per second.|  
|**Message Fragment P4 Sends/sec**|This counter reports the number of priority 4 message fragments sent over the network per second.|  
|**Message Fragment P5 Sends/sec**|This counter reports the number of priority 5 message fragments sent over the network per second.|  
|**Message Fragment P6 Sends/sec**|This counter reports the number of priority 6 message fragments sent over the network per second.|  
|**Message Fragment P7 Sends/sec**|This counter reports the number of priority 7 message fragments sent over the network per second.|  
|**Message Fragment P8 Sends/sec**|This counter reports the number of priority 8 message fragments sent over the network per second.|  
|**Message Fragment P9 Sends/sec**|This counter reports the number of priority 9 message fragments sent over the network per second.|  
|**Message Fragment P10 Sends/sec**|This counter reports the number of priority 10 message fragments sent over the network per second.|  
|**Message Fragment Receives/sec**|This counter reports the number of message fragments received over the network per second.|   
|**Message Fragment Sends/sec**|This counter reports the number of message fragments of all priorities sent over the network per second.|  
|**Msg Fragment Recv Size Avg**|This counter reports the average size of message fragments received over the network.|  
|**Msg Fragment Recv Size Avg Base**|For internal use only.| 
|**Msg Fragment Send Size Avg**|This counter reports the average size of the message fragments sent over the network.|  
|**Msg Fragment Send Size Avg Base**|For internal use only.|
|**Open Connection Count**|This counter reports the number of network connections that Service Broker currently has open.|  
|**Pending Bytes for Recv I/O**|This counter reports the number of bytes contained in message fragments that have been received from the network but have not yet been placed on a queue or discarded.|  
|**Pending Bytes for Send I/O**|This counter reports the total number of bytes in message fragments that are ready to be sent over the network.|  
|**Pending Msg Frags for Recv I/O**|This counter reports the number of message fragments that have been received from the network, but that have not yet been placed on a queue or discarded.|  
|**Pending Msg Frags for Send I/O**|This counter reports the total number of message fragments that are ready to be sent over the network.|  
|**Receive I/O bytes/sec**|This counter reports the number of bytes per second received over the network by Service Broker endpoints and Database Mirroring endpoints.|  
|**Receive I/O Bytes Total**|This counter reports the total number of bytes received over the network by Service Broker endpoints and Database Mirroring endpoints.|  
|**Receive I/O Len Avg**|This counter reports the average number of bytes for a transport receive operation.|  
|**Receive I/O Len Avg Base**|For internal use only.|
|**Receive I/Os/sec**|This counter reports the number of transport receive I/O operations per second that the Service Broker / DBM transport layer has completed. Notice that a transport receive operation may contain more than one message fragment.|  
|**Recv I/O Buffer Copies bytes/sec**|The rate at which transport receive I/O operations had to move buffer fragments in memory.|
|**Recv I/O Buffer Copies Count**|The number of times when transport receive I/O operations had to move buffer fragments in memory.| 
|**Send I/O bytes/sec**|This counter reports the number of bytes per second sent over the network by Service Broker endpoints and Database Mirroring endpoints.|   
|**Send I/O Bytes Total**|This counter reports the total number of bytes sent over the network by Service Broker endpoints and Database Mirroring endpoints.| 
|**Send I/O Len Avg**|This counter reports the average size in bytes of each transport send operation. Notice that a transport send operation may contain more than one message fragment.|  
|**Send I/O Len Avg Base**|For internal use only.|
|**Send I/Os/sec**|This counter reports the number of transport send I/O operations per second that have completed. Notice that a transport send operation may contain more than one message fragment.|  
  
## See Also  
 [sys.dm_broker_forwarded_messages &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-broker-forwarded-messages-transact-sql.md)   
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
