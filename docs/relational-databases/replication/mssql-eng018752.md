---
title: "MSSQL_ENG018752 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG018752 error"
ms.assetid: 405b2655-acb4-4e15-bcc6-b8f86bb22b37
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_ENG018752
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|18752|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Only one Log Reader Agent or log-related procedure (sp_repldone, sp_replcmds, and sp_replshowcmds) can connect to a database at a time. If you executed a log-related procedure, drop the connection over which the procedure was executed or execute sp_replflush over that connection before starting the Log Reader Agent or executing another log-related procedure.|  
  
## Explanation  
 More than one current connection is trying to execute any of the following: **sp_repldone**, **sp_replcmds**, or **sp_replshowcmds**. The stored procedures [sp_repldone &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repldone-transact-sql.md) and [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md) are stored procedures used by the Log Reader Agent to locate and update information about replicated transactions in a published database. The stored procedure [sp_replshowcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replshowcmds-transact-sql.md) is used to troubleshoot certain types of issues with transactional replication.  
  
 This error is raised in the following circumstances:  
  
-   If the Log Reader Agent for a published database is running and a second Log Reader Agent attempts to run against the same database, the error is raised for the second agent and appears in the agent history.  
  
     In a situation where it appears there are multiple agents, it is possible that one of them is the result of an orphaned process.  
  
-   If the Log Reader Agent for a published database is started and a user executes **sp_repldone**, **sp_replcmds**, or **sp_replshowcmds** against the same database, the error is raised in the application where the stored procedure was executed (such as **sqlcmd**).  
  
-   If no Log Reader Agent is running for a published database and a user executes **sp_repldone**, **sp_replcmds**, or **sp_replshowcmds** and then does not close the connection over which the procedure was executed, the error is raised when the Log Reader Agent attempts to connect to the database.  
  
## User Action  
 The following steps can help you to troubleshoot the problem. If any step allows the Log Reader Agent to start without errors, there is no need to complete the remaining steps.  
  
-   Check the history of the Log Reader agent for any other errors that could be contributing to this error. For information about viewing agent status and error details in Replication Monitor, see [View Information and Perform Tasks with Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
-   Check the output of [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md) for specific process identification numbers (SPIDs) that are connected to the published database. Close any connections that might have run **sp_repldone**, **sp_replcmds**, or **sp_replshowcmds**.  
  
-   Restart the Log Reader Agent. For more information, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
-   Restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service (bring it offline or online in a cluster) on the Distributor. If there is possibility that a scheduled job could have executed **sp_repldone**, **sp_replcmds**, or **sp_replshowcmds** from any other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent for those instances as well. For more information, see [Start, Stop, or Pause the SQL Server Agent Service](https://msdn.microsoft.com/library/c95a9759-dd30-4ab6-9ab0-087bb3bfb97c).  
  
-   Execute [sp_replflush &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md) at the Publisher on the publication database, and then restart the Log Reader Agent.  
  
-   If the error continues to occur, increase the logging of the agent and specify an output file for the log. Depending on the context of the error, this could provide the steps leading up to the error and/or additional error messages.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)   
 [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md)  
  
  
