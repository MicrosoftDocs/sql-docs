---
description: "Monitoring Replication with Performance Monitor"
title: "Monitoring Replication with Performance Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring performance [SQL Server replication], Performance Monitor"
  - "Performance Monitor [SQL Server], replication"
  - "performance counters [SQL Server replication]"
ms.assetid: 8cd3a270-0328-4bfd-bf23-b1d759cc120c
author: "MashaMSFT"
ms.author: "mathoma"
---
# Monitoring Replication with Performance Monitor
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Performance Monitor allows you to:
- Use graphs, charts, and reports to gauge the efficiency of your computer, and 
- Identify and troubleshoot possible problems (such as unbalanced resource use, insufficient hardware, or poor program design), and 
- Plan for additional hardware needs. 

For more information, see [Monitor Resource Usage &#40;Performance Monitor&#41;](../../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md).  
  
 Performance Monitor uses performance objects and counters, which provide information on the performance of various processes. You can measure replication performance through counters associated with the replication agents:  
  
|Agent|Performance object|Counter|Description|  
|-----------|------------------------|-------------|-----------------|  
|All agents|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Agents|Running|The number of replication agents currently running.|  
|Snapshot Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Snapshot|Snapshot: Delivered Cmds/sec|The number of commands per second delivered to the Distributor.|  
|Snapshot Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Snapshot|Snapshot: Delivered Trans/sec|The number of transactions per second delivered to the Distributor.|  
|Log Reader Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Log reader|Logreader: Delivered Cmds/sec|The number of commands per second delivered to the Distributor.|  
|Log Reader Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Log reader|Logreader: Delivered Trans/sec|The number of transactions per second delivered to the Distributor.|  
|Log Reader Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Log reader|Logreader: Delivery Latency|The current amount of time (in milliseconds) elapsed from when transactions are applied at the Publisher to when they are delivered to the Distributor.|  
|Distribution Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Dist.|Dist: Delivered Cmds/sec|The number of commands per second delivered to the Subscriber.|  
|Distribution Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Dist.|Dist: Delivered Trans/sec|The number of transactions per second delivered to the Subscriber.|  
|Distribution Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Dist.|Dist: Delivery Latency|The current amount of time (in milliseconds) elapsed from when transactions are delivered to the Distributor to when they are applied at the Subscriber.|  
|Merge Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Merge|Conflicts/sec|The number of conflicts per second occurring during the merge process.|  
|Merge Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Merge|Downloaded Changes/sec|The number of rows per second replicated from the Publisher to the Subscriber.|  
|Merge Agent|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: Replication Merge|Uploaded Changes/sec|The number of rows per second replicated from the Subscriber to the Publisher.|  
  
## See Also  
 [Monitoring &#40;Replication&#41;](../../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
