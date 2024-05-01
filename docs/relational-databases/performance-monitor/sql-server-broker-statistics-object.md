---
title: "SQL Server, Broker Statistics object"
description: "Learn about the SQLServer:Broker Statistics performance object, which contains performance counters that report Service Broker information for Database Engine."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "SQLServer:Broker Statistics"
  - "Broker Statistics object"
---
# SQL Server, Broker Statistics object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Broker Statistics** performance object contains performance counters that report general [!INCLUDE [ssSB](../../includes/sssb-md.md)] information for an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. The following table lists the counters that this object contains:  
  
|**SQL Server Broker Statistics** counters|Description|  
|-------------------------------------------|-----------------|  
|**Activation Errors Total**|The number of times a [!INCLUDE [ssSB](../../includes/sssb-md.md)] activation stored procedure exited with an error.|  
|**Broker Transaction Rollbacks**|The number of rolled-back transactions that contained DML statements related to [!INCLUDE [ssSB](../../includes/sssb-md.md)], such as SEND and RECEIVE.|  
|**Corrupted Messages Total**|The number of corrupted messages that were received by the instance.|  
|**Dequeued Transmission Msgs/sec**|The number of messages that have been removed from the [!INCLUDE [ssSB](../../includes/sssb-md.md)] transmission queue per second.|  
|**Dialog timer event count**|The number of timers active in the dialog protocol layer. This number corresponds to the number of active dialogs.|  
|**Dropped Messages Total**|The number of messages that were received by the instance, but could not be delivered to a queue.|  
|**Enqueued Local Messages Total**|The number of messages that have been put into the queues in the instance, counting only messages that did not arrive through the network.|  
|**Enqueued Local Messages/sec**|The number of messages per second that have been put into the queues in the instance, counting only messages that did not arrive through the network.|  
|**Enqueued Messages Total**|The total number of messages that have been put into the queues in the instance.|  
|**Enqueued Messages/sec**|The number of messages from local endpoints and the transport that are successfully delivered into local target queues per second, including messages of all priority levels.|  
|**Enqueued P1 Msgs/sec**|The number of priority 1 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P2 Msgs/sec**|The number of priority 2 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P3 Msgs/sec**|The number of priority 3 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P4 Msgs/sec**|The number of priority 4 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P5 Msgs/sec**|The number of priority 5 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P6 Msgs/sec**|The number of priority 6 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P7 Msgs/sec**|The number of priority 7 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P8 Msgs/sec**|The number of priority 8 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P9 Msgs/sec**|The number of priority 9 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued P10 Msgs/sec**|The number of priority 10 messages from local endpoints and the transport that are successfully delivered into local target queues, per second.|  
|**Enqueued Transmission Msgs/sec**|The number of messages that have been placed in the [!INCLUDE [ssSB](../../includes/sssb-md.md)] transmission queue per second.|  
|**Enqueued Transport Msg Frag Tot**|The number of message fragments that have been put into the queues in the instance, counting only messages that arrived through the network.|  
|**Enqueued Transport Msg Frags/sec**|The number of message fragments per second that have been put into the queues in the instance.|  
|**Enqueued Transport Msgs Total**|The number of messages that have been put into the queues in the instance, counting only messages that arrived through the network.|  
|**Enqueued Transport Msgs/sec**|The number of messages per second that have been put into the queues in the instance, counting only messages that arrived through the network.|  
|**Forwarded Messages Total**|The total number of [!INCLUDE [ssSB](../../includes/sssb-md.md)] messages forwarded by this computer.|  
|**Forwarded Messages/sec**|The number of messages per second forwarded by this computer.|  
|**Forwarded Msg Byte Total**|The total size, in bytes, of the messages forwarded by this computer.|  
|**Forwarded Msg Bytes/sec**|The size, in bytes, of messages per second forwarded by this computer.|  
|**Forwarded Msg Discarded Total**|The number of messages that this computer received for forwarding, but did not successfully forward.|  
|**Forwarded Msg Discarded/sec**|The number of messages per second that this computer received for forwarding, but did not successfully forward.|  
|**Forwarded Pending Msg Bytes**|The total size of the messages currently held for forwarding.|  
|**Forwarded Pending Msg Count**|The total number of messages currently held for forwarding.|  
|**SQL RECEIVE Total**|The total number of [!INCLUDE [tsql](../../includes/tsql-md.md)] RECEIVE statements processed by the Broker.|  
|**SQL RECEIVEs/sec**|The number of [!INCLUDE [tsql](../../includes/tsql-md.md)] RECEIVE statements processed by the Broker per second.|  
|**SQL SEND Total**|The total number of [!INCLUDE [tsql](../../includes/tsql-md.md)] SEND statements executed.|  
|**SQL SENDs/sec**|The number of [!INCLUDE [tsql](../../includes/tsql-md.md)] SEND statements executed per second.|  

 
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Broker Statistics%';
```  
  
## Related content

- [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)
- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
