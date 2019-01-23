---
title: "MSSQL_ENG014160 | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014160 error"
ms.assetid: d0f3855e-d095-4a81-a5bd-9d7ad51f2c77
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014160
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14160|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The threshold [%s:%s] for the publication [%s] has been set. One or more subscriptions to this publication have expired.|  
  
## Explanation  
 Replication lets you enable warnings for several conditions. This includes imminent subscription expiration. Subscriptions can expire if they are not synchronized within a specified *retention period*. For more information, see [Subscription Expiration and Deactivation](subscription-expiration-and-deactivation.md).  
  
 When you enable a warning by using Replication Monitor or [sp_replmonitorchangepublicationthreshold](/sql/relational-databases/system-stored-procedures/sp-replmonitorchangepublicationthreshold-transact-sql), you specify a threshold that determines when a warning is triggered. When that threshold is met or exceeded, a warning is displayed in Replication Monitor, and an event is written to the Windows event log. Reaching a threshold can also trigger a SQL Server Agent alert. For more information, see [Set Thresholds and Warnings in Replication Monitor](monitor/set-thresholds-and-warnings-in-replication-monitor.md) and [Programmatically Monitor Replication](monitoring-replication.md).  
  
## User Action  
 The resolution for this issue depends on the reason the warning was raised:  
  
-   If the threshold has been exceeded, but the subscription has not yet expired, synchronize the subscription. For more information, see [Synchronize Data](synchronize-data.md).  
  
-   If the agent has been running, but has not been replicating changes properly, this can cause the subscription to expire. For transactional replication, make sure that the Distribution Agent and Log Reader Agent are running. For merge replication, make sure the Merge Agent is running. For information about how to start these agents, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](concepts/replication-agent-executables-concepts.md).  
  
-   If the subscription has expired, it must either be reinitialized or dropped and re-created, depending on the type of subscription and how long it has been expired. For more information, see [Subscription Expiration and Deactivation](subscription-expiration-and-deactivation.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
