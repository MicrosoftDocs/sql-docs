---
title: "MSSQL_ENG014152"
description: "MSSQL_ENG014152"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "MSSQL_ENG014152 error"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG014152
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14152|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Replication-%s: agent %s scheduled for retry. %s|  
  
## Explanation  
 The specified replication agent has been scheduled to retry the requested operation. The process continues to retry until it reaches the configured maximum number of retry attempts for the job step.  
  
 A retry typically occurs because of one of the following reasons:  
  
-   The **QueryTimeout** value is exceeded.  
  
-   The **LoginTimeout** value is exceeded.  
  
-   The replication process was chosen as a deadlock victim.  
  
## User Action  
 If the retry message is infrequent, no user action is required.  
  
 Use [sp_help_jobstep](../../relational-databases/system-stored-procedures/sp-help-jobstep-transact-sql.md) to view the current setting for the maximum number of times the **Run agent** step for the specified replication agent will retry. You can use the `@retry_attempts` parameter of the [sp_update_jobstep](../../relational-databases/system-stored-procedures/sp-update-jobstep-transact-sql.md) stored procedure to adjust the number of times a job step retries.  
  
 If the retry message recurs frequently, troubleshoot the issue based on the message that is causing the retry. Check the agent's history for messages that indicate why the retry had to be scheduled. In some cases you may have to enable more detailed logging for your replication agent. For more information about how to configure logging for replication, see the Microsoft Knowledge Base article [312292](https://support.microsoft.com/kb/312292).  
  
## Related content

- [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)
