---
title: "MSSQL_ENG014163 | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014163 error"
ms.assetid: b53dd463-ba36-421e-9745-67c7387e68dd
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_ENG014163
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14163|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The threshold [%s:%s] for the publication [%s] has been set. Please make sure that the merge agent is running and can match the expected requirement.|  
  
## Explanation  
 Replication lets you enable warnings for several conditions. This includes exceeding a specified length of time for synchronizing changes between a merge Publisher and Subscriber. Different times can be specified for LAN connections and dial-up connections.  
  
 When you enable a warning by using Replication Monitor or [sp_replmonitorchangepublicationthreshold](../../relational-databases/system-stored-procedures/sp-replmonitorchangepublicationthreshold-transact-sql.md), you specify a threshold that determines when a warning is triggered. When that threshold is met or exceeded, a warning is displayed in Replication Monitor, and an event is written to the Windows event log. Reaching a threshold can also trigger a SQL Server Agent alert. For more information, see [Set Thresholds and Warnings in Replication Monitor](../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md) and [Programmatically Monitor Replication](../../relational-databases/replication/monitor/programmatically-monitor-replication.md).  
  
## User Action  
 If a subscription exceeds a duration threshold, you must determine whether there is a performance issue with the system or whether the threshold should be adjusted. After you configure replication, develop a performance baseline that will let you determine how replication behaves with a workload that is typical for your applications and topology. Include duration of synchronization in this baseline so that you can set an appropriate value for the threshold.  
  
 If the threshold value is appropriate, but it is being exceeded, you must determine where the performance bottleneck is in the system. For more information about how to monitor and troubleshoot replication performance, see [Monitor Performance with Replication Monitor](../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
