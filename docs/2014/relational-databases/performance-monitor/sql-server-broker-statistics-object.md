---
title: "SQL Server, Broker Statistics Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Broker Statistics"
  - "Broker Statistics object"
ms.assetid: e9e36f01-93f6-4e6e-90c6-c7f3fd121737
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, Broker Statistics Object
  The SQLServer:Broker Statistics performance object contains performance counters that report general [!INCLUDE[ssSB](../../includes/sssb-md.md)] information for an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The following table lists the counters that this object contains:  
  
|SQL Server Broker Statistics counters|Description|  
|-------------------------------------------|-----------------|  
|**Activation Errors Total**|The number of times a [!INCLUDE[ssSB](../../includes/sssb-md.md)] activation stored procedure exited with an error.|  
|**Broker Transaction Rollbacks**|The number of rolled-back transactions that contained DML statements related to [!INCLUDE[ssSB](../../includes/sssb-md.md)], such as SEND and RECEIVE.|  
|**Corrupted Messages Total**|The number of corrupted messages that were received by the instance.|  
|**Dequeued Transmission Msgs/sec**|The number of messages that have been removed from the [!INCLUDE[ssSB](../../includes/sssb-md.md)] transmission queue per second.|  
|**Dialog timer event count**|The number of timers active in the dialog protocol layer. This number corresponds to the number of active dialogs.|  
|**Dropped Messages Total**|The number of messages that were received by the instance, but could not be delivered to a queue.|  
|**Enqueued Local Messages Total**|The number of messages that have been put into the queues in the instance, counting only messages that did not arrive through the network.|  
|**Enqueued Local Messages/sec**|The number of messages per second that have been put into the queues in the instance, counting only messages that did not arrive through the network.|  
|**Enqueued Messages Total**|The total number of messages that have been put into the queues in the instance.|  
|**Enqueued Messages/sec**|The number of messages per second that have been put into the queues in the instance. This includes messages of all priority levels.|  
|**Enqueued P1 Msgs/sec**|The number of priority 1 messages per second that have been put into the queues in the instance.|  
|**Enqueued P2 Msgs/sec**|The number of priority 2 messages per second that have been put into the queues in the instance.|  
|**Enqueued P3 Msgs/sec**|The number of priority 3 messages per second that have been put into the queues in the instance.|  
|**Enqueued P4 Msgs/sec**|The number of priority 4 messages per second that have been put into the queues in the instance.|  
|**Enqueued P5 Msgs/sec**|The number of priority 5 messages per second that have been put into the queues in the instance.|  
|**Enqueued P6 Msgs/sec**|The number of priority 6 messages per second that have been put into the queues in the instance.|  
|**Enqueued P7 Msgs/sec**|The number of priority 7 messages per second that have been put into the queues in the instance.|  
|**Enqueued P8 Msgs/sec**|The number of priority 8 messages per second that have been put into the queues in the instance.|  
|**Enqueued P9 Msgs/sec**|The number of priority 9 messages per second that have been put into the queues in the instance.|  
|**Enqueued P10 Msgs/sec**|The number of priority 10 messages per second that have been put into the queues in the instance.|  
|**Enqueued Transmission Msgs/sec**|The number of messages that have been placed in the [!INCLUDE[ssSB](../../includes/sssb-md.md)] transmission queue per second.|  
|**Enqueued Transport Msg Frag Tot**|The number of message fragments that have been put into the queues in the instance, counting only messages that arrived through the network.|  
|**Enqueued Transport Msg Frags/sec**|The number of message fragments per second that have been put into the queues in the instance.|  
|**Enqueued Transport Msgs Total**|The number of messages that have been put into the queues in the instance, counting only messages that arrived through the network.|  
|**Enqueued Transport Msgs/sec**|The number of messages per second that have been put into the queues in the instance, counting only messages that arrived through the network.|  
|**Forwarded Messages Total**|The total number of [!INCLUDE[ssSB](../../includes/sssb-md.md)] messages forwarded by this computer.|  
|**Forwarded Messages/sec**|The number of messages per second forwarded by this computer.|  
|**Forwarded Msg Byte Total**|The total size, in bytes, of the messages forwarded by this computer.|  
|**Forwarded Msg Bytes/sec**|The size, in bytes, of messages per second forwarded by this computer.|  
|**Forwarded Msg Discarded Total**|The number of messages that this computer received for forwarding, but did not successfully forward.|  
|**Forwarded Msg Discarded/sec**|The number of messages per second that this computer received for forwarding, but did not successfully forward.|  
|**Forwarded Pending Msg Bytes**|The total size of the messages currently held for forwarding.|  
|**Forwarded Pending Msg Count**|The total number of messages currently held for forwarding.|  
|**SQL RECEIVE Total**|The total number of [!INCLUDE[tsql](../../includes/tsql-md.md)] RECEIVE statements processed.|  
|**SQL RECEIVEs/sec**|The number of [!INCLUDE[tsql](../../includes/tsql-md.md)] RECEIVE statements processed per second.|  
|**SQL SEND Total**|The total number of [!INCLUDE[tsql](../../includes/tsql-md.md)] SEND statements executed.|  
|**SQL SENDs/sec**|The number of [!INCLUDE[tsql](../../includes/tsql-md.md)] SEND statements executed per second.|  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)  
  
  
