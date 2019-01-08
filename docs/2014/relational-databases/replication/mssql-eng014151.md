---
title: "MSSQL_ENG014151 | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014151 error"
ms.assetid: 54b45e70-46b3-4c7a-a5bf-06f6dd028ceb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014151
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14151|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Replication-%s: agent %s failed. %s|  
  
## Explanation  
 This error is raised for any replication agent failure. The text at the end of the message depends on the context of the failure.  
  
## User Action  
 This error can occur in a number of situations; use the following approaches as necessary:  
  
-   Restart the failed agent to see if it will now run without failures. For more information, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](concepts/replication-agent-executables-concepts.md).  
  
-   Check the agent history and job history for other errors that occurred around the same time. For information about viewing agent status and error details in Replication Monitor, see [View Information and Perform Tasks using Replication Monitor](monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
-   Verify that basic connectivity is working between the computers accessed by the agent, and then connect to each computer with a utility like the [sqlcmd Utility](../../tools/sqlcmd-utility.md). When connecting, use the same account under which the agent makes connections. For more information about the permissions required by each agent account, see [Replication Agent Security Model](security/replication-agent-security-model.md).  
  
-   If the error occurs while creating or applying a snapshot, check the files in the snapshot directory for errors.  
  
-   If the error continues to occur, increase the logging of the agent and specify an output file for the log. Depending on the context of the error, this could provide the steps leading up to the error and/or additional error messages.  
  
## See Also  
 [Replication Agent Administration](agents/replication-agent-administration.md)   
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)   
 [Replication Distribution Agent](agents/replication-distribution-agent.md)   
 [Replication Log Reader Agent](agents/replication-log-reader-agent.md)   
 [Replication Merge Agent](agents/replication-merge-agent.md)   
 [Replication Queue Reader Agent](agents/replication-queue-reader-agent.md)   
 [Replication Snapshot Agent](agents/replication-snapshot-agent.md)  
  
  
