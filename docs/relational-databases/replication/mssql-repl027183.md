---
title: "MSSQL_REPL027183 | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_REPL027183 error"
ms.assetid: 52c271ac-1a0e-43d5-85d4-35886d1efd32
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_REPL027183
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|27183|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The merge process failed to enumerate changes in articles with parameterized row filters. If this failure continues, increase the query timeout for this process, reduce the retention period for the publication, and improve indexes on published tables.|  
  
## Explanation  
 This error is raised if a Merge Agent timeout occurs while processing changes in a filtered publication. The timeout might be caused by one of the following issues:  
  
-   Not using the precomputed partitions optimization.  
  
-   Index fragmentation on columns used for filtering.  
  
-   Large merge metadata tables, such as **MSmerge_tombstone**, **MSmerge_contents**, and **MSmerge_genhistory**.  
  
-   Filtered tables that are not joined on a unique key and join filters that involve a large number of tables.  
  
## User Action  
 To resolve the issue:  
  
-   Increase the value of the **-QueryTimeOut** parameter for the Merge Agent to allow processing to continue while you address the underlying issues causing the error. Agent parameters can be specified in agent profiles and on the command line. For more information, see:  
  
    -   [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)  
  
    -   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
    -   [Replication Agent Executables Concepts](../../relational-databases/replication/concepts/replication-agent-executables-concepts.md).  
  
-   Use the precomputed partitions optimization if possible. This optimization is used by default if a number of publication requirements are met. For more information about these requirements, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md). If the publication does not meet these requirements, consider redesigning the publication.  
  
-   Specify the lowest setting possible for the publication retention period, because replication cannot clean up metadata in the publication and subscription databases until the retention period is reached. For more information, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
-   As part of maintenance for merge replication, occasionally check the growth of the system tables associated with merge replication: **MSmerge_contents**, **MSmerge_genhistory**, and **MSmerge_tombstone**, **MSmerge_current_partition_mappings**, and **MSmerge_past_partition_mappings**. Periodically re-index these tables. For more information, see [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).  
  
-   Ensure that columns used for filtering are properly indexed and rebuild such indexes if necessary. For more information, see [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).  
  
-   Set the **join_unique_key** property for join filters that are based on unique columns. For more information, see [Join Filters](../../relational-databases/replication/merge/join-filters.md).  
  
-   Limit the number of tables in the join filter hierarchy. If you are generating join filters of five or more tables, consider other solutions: do not filter tables that are small, not subject to change, or are primarily lookup tables. Use join filters only between tables that must be partitioned among subscriptions.  
  
-   Make a smaller number of changes on filtered tables between synchronizations, or run the Merge Agent more frequently. For more information about setting synchronization schedules, see [Specify Synchronization Schedules](../../relational-databases/replication/specify-synchronization-schedules.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
