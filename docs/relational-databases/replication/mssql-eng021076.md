---
title: "MSSQL_ENG021076"
description: "MSSQL_ENG021076"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "MSSQL_ENG021076 error"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG021076
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21076|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The initial snapshot for article '%s' is not yet available.|  
  
## Explanation  
 Error MSSQL_ENG021076 can be raised if the Distribution Agent is started before the Snapshot Agent has finished generating the snapshot. This error is raised only if the publication contains a single article. If the publication contains more than one article, MSSQL_ENG021075 is raised instead. For more information, see [MSSQL_ENG021075](../../relational-databases/replication/mssql-eng021075.md).  
  
## User Action  
 If the Snapshot Agent for the publication has not been started since the subscription was created, or if it has not been started since the last time you chose to reinitialize the subscription, start the Snapshot Agent and let it complete before starting the Distribution Agent. For more information, see [Create and Apply the Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
 If the Snapshot Agent does not complete, check the Snapshot Agent history for errors and address them. For information about viewing agent status and error details in Replication Monitor, see [View Information and Perform Tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
 If the error continues to occur, increase the logging of the agent and specify an output file for the log. Depending on the context of the error, this could provide the steps leading up to the error and/or additional error messages.  
  
## Related content

- [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)
