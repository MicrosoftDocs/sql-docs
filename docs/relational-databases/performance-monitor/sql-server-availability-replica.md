---
title: "SQL Server, Availability Replica object"
description: Learn about SQLServer:Availability Replica performance object, which contains performance counters about availability replicas in Always On availability groups.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "performance counters [SQL Server], AlwaysOn Availability Groups"
  - "SQLServer:Availability Replica"
  - "Availability Groups [SQL Server], performance counters"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Availability Replica object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Availability Replica** performance object contains performance counters that report information about the availability replicas in Always On availability groups in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. All availability replica performance counters apply to both the primary replica and the secondary replicas, with send/receive counters reflecting the local replica. For the most part, the primary replica sends most of the data, and the secondary replicas receive the data. However, secondary replicas send ACKs and some other background traffic to the primary replicas. Note that on a given availability replica, some counters will show a zero value, depending on the current role, primary or secondary, of the local replica.  
  
|Counter Name|Description|  
|------------------|-----------------|  
|**Bytes Received from Replica/sec**|**In SQL Server 2012 and 2014:** Actual number of bytes (compressed) received from the availability replica per second (sync or async). Pings and status updates will generate network traffic even on databases with no user updates. <BR/> <BR/> **In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and above:** Actual number of bytes received (compressed for async, uncompressed for sync) from the availability replica per second.|  
|**Bytes Sent to Replica/sec**|**In SQL Server 2012 and 2014:** Actual number of bytes  (compressed) sent per second over the network to the remote availability replica (sync or async). Compression is enabled for both sync and async replica by default. <BR/> <BR/> **In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and above:** Number of bytes sent to the remote availability replica per second. Before compression for async replica. (Actual number of bytes for sync replica which has no compression)|  
|**Bytes Sent to Transport/sec**|**In SQL Server 2012 & 2014:** Actual number of bytes sent per second (compressed) over the network to the remote availability replica (sync or async). Compression is enabled for both sync and async replica by default. <BR/> <BR/> **In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and above:** Number of bytes sent to the remote availability replica per second Before compression for async replica. (Actual number of bytes for sync replica which has no compression)|  
|**Flow Control Time (ms/sec)**|Time in milliseconds that log stream messages waited for send flow control, in the last second.|  
|**Flow Control/sec**|Number of times flow-control initiated in the last second. **Flow Control Time (ms/sec)** divided by **Flow Control/sec** is the average time per wait.|  
|**Receives from Replica/sec**|Number of Always On messages received from the replica per second.|  
|**Resent Messages/sec**|Number of Always On messages resent in the last second. A resent message is a message that was attempted to be sent but was unable to complete and must be attempted to be sent again. While this could occur due to different reasons, it is not the same as a TCP retransmission.|  
|**Sends to Replica/sec**|Number of Always On messages sent to this availability replica per second.|  
|**Sends to Transport/sec**|Actual number of Always On messages sent per second over the network to the remote availability replica. On the primary replica this is the number of messages sent to the secondary replica. On the secondary replica this is the number of messages sent to the primary replica.|  

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Availability Replica%';
```  

## See also 
 
 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 - [SQL Server, Database Replica](../../relational-databases/performance-monitor/sql-server-database-replica.md)   
 - [Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
