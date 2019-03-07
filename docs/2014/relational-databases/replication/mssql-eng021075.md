---
title: "MSSQL_ENG021075 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG021075 error"
ms.assetid: c8c29543-d1f6-49d5-b6c8-e8c3aa373090
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG021075
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21075|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The initial snapshot for publication '%s' is not yet available.|  
  
## Explanation  
 Error MSSQL_ENG021075 is raised if the Distribution Agent or Merge Agent is started before the Snapshot Agent has finished generating the snapshot.  
  
## User Action  
 If the Snapshot Agent for the publication has not been started since the subscription was created, or if it has not been started since the last time you chose to reinitialize the subscription, start the Snapshot Agent and let it complete before starting the Distribution Agent or Merge Agent. For more information, see [Create and Apply the Snapshot](create-and-apply-the-snapshot.md).  
  
 If the Snapshot Agent does not complete, check the Snapshot Agent history for errors and address them. For information about viewing agent status and error details in Replication Monitor, see [View Information and Perform Tasks using Replication Monitor](monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
 If the error continues to occur, increase the logging of the agent and specify an output file for the log. Depending on the context of the error, this could provide the steps leading up to the error and/or additional error messages.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
