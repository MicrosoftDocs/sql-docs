---
title: "MSSQLSERVER_5235 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "5235 (Database Engine error)"
ms.assetid: 1aa7e6a5-7ccb-43c8-a1fd-d50e92e0a798
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_5235
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|5235|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC4_ERRORLOG_SUMMARY_PREMATURE_TERMINATION|  
|Message Text|[EMERGENCY] DBCC DBCC_COMMAND_DETAILS executed by USER_NAME terminated abnormally due to error state ERROR_STATE. Elapsed time: HOURS hours MINUTES minutes SECONDS seconds.|  
  
## Explanation  
 This is the summary message that DBCC prints to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log when an unexpected termination occurs while the command is running. The error state reported in the message defines the type of unexpected termination.  
  
 The following table lists and defines the error states.  
  
|Error state|Definition|  
|-----------------|----------------|  
|State 0|The statement was terminated because of a fatal metadata corruption. This message will be accompanied by one or more instances of error 8930.|  
|State 1|The statement was terminated because of an internal check failure. This message will be accompanied by one or more instances of error 8967.|  
|State 2|Basic system table checks of the core storage engine system tables failed. This message will be accompanied by one or more instances of error [7984](mssqlserver-7984-database-engine-error.md), 7985, [7986](mssqlserver-7986-database-engine-error.md), [7987](mssqlserver-7987-database-engine-error.md), or [7988](mssqlserver-7988-database-engine-error.md).|  
|State 3|DBCC emergency-mode repair failed because the database could not be started after rebuilding the transaction log. This message will be accompanied by error 7909.|  
|State 4|A failed assertion or access violation occurred during the execution of the command.|  
|State 5|An unknown failure occurred that unexpectedly terminated the DBCC command.|  
  
## User Action  
 The following table provides the user action that is appropriate for the specified error state.  
  
|Error state|User action|  
|-----------------|-----------------|  
|State 0|Restore from backup.|  
|State 1|Contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Service and Support (CSS).|  
|State 2|Restore from backup.|  
|State 3|Restore from backup.|  
|State 4|Contact CSS.|  
|State 5|Run the command again. If the problem persists, contact CSS.|  
  
## See Also  
 [DBCC &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-transact-sql)  
  
  
