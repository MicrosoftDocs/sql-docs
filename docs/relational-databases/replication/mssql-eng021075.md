---
description: "MSSQL_ENG021075"
title: "MSSQL_ENG021075 | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG021075 error"
ms.assetid: c8c29543-d1f6-49d5-b6c8-e8c3aa373090
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG021075
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
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
 If the Snapshot Agent for the publication has not been started since the subscription was created, or if it has not been started since the last time you chose to reinitialize the subscription, start the Snapshot Agent and let it complete before starting the Distribution Agent or Merge Agent. For more information, see [Create and Apply the Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
 If the Snapshot Agent does not complete, check the Snapshot Agent history for errors and address them. For information about viewing agent status and error details in Replication Monitor, see [View Information and Perform Tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
 If the error continues to occur, increase the logging of the agent and specify an output file for the log. Depending on the context of the error, this could provide the steps leading up to the error and/or additional error messages.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
