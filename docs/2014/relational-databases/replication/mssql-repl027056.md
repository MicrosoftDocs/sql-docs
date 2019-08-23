---
title: "MSSQL_REPL027056 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_REPL027056 error"
ms.assetid: 92d62f3c-b8ae-482e-a348-2e9a8ee9786e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_REPL027056
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|27056|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The merge process was unable to change generation history at the '%1'. When troubleshooting, restart the synchronization with verbose history logging and specify an output file to which to write.|  
  
## Explanation  
 This error is typically raised as a result of contention in merge replication system tables that have grown excessively large. Large system tables are typically caused by a long publication retention period, because metadata must be stored in these tables until the retention period is reached.  
  
## User Action  
 **To resolve the issue:**  
  
1.  Decrease the value of the -**DownloadGenerationsPerBatch** and **-UploadGenerationsPerBatch** parameters for the Merge Agent to allow processing to continue while you address the underlying issue causing the error. Agent parameters can be specified in agent profiles and on the command line. For more information, see:  
  
    -   [Work with Replication Agent Profiles](agents/replication-agent-profiles.md)  
  
    -   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
    -   [Replication Agent Executables Concepts](concepts/replication-agent-executables-concepts.md).  
  
2.  Specify the lowest setting possible for the publication retention period. For more information, see [Subscription Expiration and Deactivation](subscription-expiration-and-deactivation.md).  
  
3.  As part of maintenance for merge replication, occasionally check the growth of the system tables associated with merge replication: **MSmerge_contents**, **MSmerge_genhistory**, and **MSmerge_tombstone**, **MSmerge_current_partition_mappings**, and **MSmerge_past_partition_mappings**. Periodically re-index these tables. For more information, see [Reorganize and Rebuild Indexes](../indexes/indexes.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
