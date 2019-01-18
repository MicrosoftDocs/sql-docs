---
title: "SQL Server, Database Mirroring Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Database Mirroring"
  - "database mirroring [SQL Server], performance counters"
  - "performance counters [SQL Server], database mirroring"
  - "Database Mirroring object"
ms.assetid: a27b51ee-7637-4525-9424-bcc16947dc13
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Database Mirroring Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLServer:Database Mirroring** performance object contains performance counters that report information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database mirroring. The table below lists the counters that this object contains.  
  
|Name|Description|  
|----------|-----------------|  
|**Bytes Received/sec**|Number of bytes received per second.|  
|**Bytes Sent/sec**|Number of bytes sent per second.|  
|**Log Bytes Received/sec**|Number of bytes of log received per second.|  
|**Log Bytes Redone from Cache/sec**|Number of redone log bytes that were obtained from the mirroring log cache, in the last second.<br /><br /> This counter is used on only the mirror server. On the principal server the value is always 0.|  
|**Log Bytes Sent from Cache/sec**|Number of sent log bytes that were obtained from the mirroring log cache, in the last second.<br /><br /> This counter is used on only the principal server. On the mirror server the value is always 0.|  
|**Log Bytes Sent/sec**|Number of bytes of log sent per second.|  
|**Log Compressed Bytes Rcvd/sec**|Number of compressed bytes of log received, in the last second.|  
|**Log Compressed Bytes Sent/sec**|Number of compressed bytes of log sent, in the last second.|  
|**Log Harden Time (ms)**|Milliseconds that log blocks waited to be hardened to disk, in the last second.|  
|**Log Remaining for Undo KB**|Total kilobytes of log that remain to be scanned by the new mirror server after failover.<br /><br /> This counter is used on only the mirror server during the undo phase. After the undo phase completes, the counter is reset to 0. On the principal server the value is always 0.|  
|**Log Scanned for Undo KB**|Total kilobytes of log that have been scanned by the new mirror server since failover.<br /><br /> This counter is used on only the mirror server during the undo phase. After the undo phase completes, the counter is reset to 0. On the principal server the value is always 0.|  
|**Log Send Flow Control Time (ms)**|Milliseconds that log stream messages waited for send flow control, in the last second.<br /><br /> Sending log data and metadata to the mirroring partner is the most data-intensive operation in database mirroring and might monopolize the database mirroring and Service Broker send buffers. Use this counter to monitor the use of this buffer by the database mirroring session.|  
|**Log Send Queue KB**|Total number of kilobytes of log that have not yet been sent to the mirror server.|  
|**Mirrored Write Transactions/sec**|Number of transactions that wrote to the mirrored database and waited for the log to be sent to the mirror in order to commit, in the last second.<br /><br /> This counter is incremented only when the principal server is actively sending log records to the mirror server.|  
|**Pages Sent/sec**|Number of pages sent per second.|  
|**Receives/sec**|Number of mirroring messages received per second.|  
|**Redo Bytes/sec**|Number of bytes of log rolled forward on the mirror database per second.|  
|**Redo Queue KB**|Total number of kilobytes of hardened log that currently remain to be applied to the mirror database to roll it forward. This is sent to the Principal from the Mirror.|  
|**Send/Receive Ack Time**|Milliseconds that messages waited for acknowledgement from the partner, in the last second.<br /><br /> This counter is helpful in troubleshooting a problem that might be caused by a network bottleneck, such as unexplained failovers, a large send queue, or high transaction latency. In such cases, you can analyze the value of this counter to determine whether the network is causing the problem.|  
|**Sends/sec**|Number of mirroring messages sent per second.|  
|**Transaction Delay**|Delay in waiting for unterminated commit acknowledgement.|  
  
> [!NOTE]  
>  On each partner, some of the counters show a zero value depending on what role the partner is currently performing.  
  
## Remarks  
 The performance counters let you monitor database mirroring performance. For example, you can examine the **Transaction Delay** counter to see if database mirroring is impacting performance on the principal server, you can examine the **Redo Queue** and **Log Send Queue** counters to see how well the mirror database is keeping up with the principal database. You can examine the **Log Bytes Sent/sec** counter to monitor the amount of log sent per second.  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
