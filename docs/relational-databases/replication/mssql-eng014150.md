---
title: "MSSQL_ENG014150"
description: "MSSQL_ENG014150"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "MSSQL_ENG014150 error"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG014150
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
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
 The Log Reader Agent, Queue Reader Agent, and Distribution Agent typically run continuously, whereas the other agents typically run on demand or on a schedule. If you do not expect an agent to have completed a run, check the status of the agent. For more information, see [Monitor Replication Agents](../../relational-databases/replication/monitor/monitor-replication-agents.md).  
  
## Related content

- [Replication Agent Administration](../../relational-databases/replication/agents/replication-agent-administration.md)
- [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)
- [Replication Distribution Agent](../../relational-databases/replication/agents/replication-distribution-agent.md)
- [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md)
- [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md)
- [Replication Queue Reader Agent](../../relational-databases/replication/agents/replication-queue-reader-agent.md)
- [Replication Snapshot Agent](../../relational-databases/replication/agents/replication-snapshot-agent.md)
