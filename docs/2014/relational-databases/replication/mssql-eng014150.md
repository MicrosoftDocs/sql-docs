---
title: "MSSQL_ENG014150 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014150 error"
ms.assetid: c3dd3109-abf3-4b38-a4e9-ef48d0235656
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014150
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14150|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Replication-%s: agent %s succeeded. %s|  
  
## Explanation  
 This message indicates that a replication agent has successfully finished running. Replication uses the following agents:  
  
-   The Snapshot Agent. This agent is used by all publications.  
  
-   The Log Reader Agent. This agent is used by all transactional publications.  
  
-   The Queue Reader Agent. This agent is used by transactional publications enabled for queued updating subscriptions.  
  
-   The Distribution Agent. This agent synchronizes subscriptions to transactional and snapshot publications.  
  
-   The Merge Agent. This agent synchronizes subscriptions to merge publications.  
  
-   Replication maintenance jobs.  
  
## User Action  
 The Log Reader Agent, Queue Reader Agent, and Distribution Agent typically run continuously, whereas the other agents typically run on demand or on a schedule. If you do not expect an agent to have completed a run, check the status of the agent. For more information, see [Monitor Replication Agents](agents/replication-agents-overview.md).  
  
## See Also  
 [Replication Agent Administration](agents/replication-agent-administration.md)   
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)   
 [Replication Distribution Agent](agents/replication-distribution-agent.md)   
 [Replication Log Reader Agent](agents/replication-log-reader-agent.md)   
 [Replication Merge Agent](agents/replication-merge-agent.md)   
 [Replication Queue Reader Agent](agents/replication-queue-reader-agent.md)   
 [Replication Snapshot Agent](agents/replication-snapshot-agent.md)  
  
  
