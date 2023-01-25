---
title: "Database Engine Error Severities | Microsoft Docs"
description: Use this list of severity levels to understand the type of problem that SQL Server encountered when the SQL Server Database Engine raises an error.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined error messages [SQL Server]"
  - "severity levels [SQL Server]"
  - "retrieving error severity"
  - "errors [SQL Server], severity"
  - "TRY...CATCH [SQL Server]"
ms.assetid: 3e7f5925-6edd-42e1-bf17-f7deb03993a7
author: MashaMSFT
ms.author: mathoma
---
# Database Engine Error Severities
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  When an error is raised by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], the severity of the error indicates the type of problem encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Levels of Severity  
 The following table lists and describes the severity levels of the errors raised by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
|Severity level|Description|  
|--------------------|-----------------|  
|0-9|Informational messages that return status information or report errors that are not severe. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not raise system errors with severities of 0 through 9.|  
|10|Informational messages that return status information or report errors that are not severe. For compatibility reasons, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] converts severity 10 to severity 0 before returning the error information to the calling application.|  
|11-16|Indicate errors that can be corrected by the user.|  
|11|Indicates that the given object or entity does not exist.|  
|12|A special severity for queries that do not use locking because of special query hints. In some cases, read operations performed by these statements could result in inconsistent data, since locks are not taken to guarantee consistency.|  
|13|Indicates transaction deadlock errors.|  
|14|Indicates security-related errors, such as permission denied.|  
|15|Indicates syntax errors in the [!INCLUDE[tsql](../../includes/tsql-md.md)] command.|  
|16|Indicates general errors that can be corrected by the user.|  
|17-19|Indicate software errors that cannot be corrected by the user. Inform your system administrator of the problem.|  
|17|Indicates that the statement caused [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run out of resources (such as memory, locks, or disk space for the database) or to exceed some limit set by the system administrator.|  
|18|Indicates a problem in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] software, but the statement completes execution, and the connection to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is maintained. The system administrator should be informed every time a message with a severity level of 18 occurs.|  
|19|Indicates that a nonconfigurable [!INCLUDE[ssDE](../../includes/ssde-md.md)] limit has been exceeded and the current batch process has been terminated. Error messages with a severity level of 19 or higher stop the execution of the current batch. Severity level 19 errors are rare and must be corrected by the system administrator or your primary support provider. Contact your system administrator when a message with a severity level 19 is raised. Error messages with a severity level from 19 through 25 are written to the error log.|  
|20-24|Indicate system problems and are fatal errors, which means that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] task that is executing a statement or batch is no longer running. The task records information about what occurred and then terminates. In most cases, the application connection to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] may also terminate. If this happens, depending on the problem, the application might not be able to reconnect.<br /><br /> Error messages in this range can affect all of the processes accessing data in the same database and may indicate that a database or object is damaged. Error messages with a severity level from 19 through 24 are written to the error log.|  
|20|Indicates that a statement has encountered a problem. Because the problem has affected only the current task, it is unlikely that the database itself has been damaged.|  
|21|Indicates that a problem has been encountered that affects all tasks in the current database, but it is unlikely that the database itself has been damaged.|  
|22|Indicates that the table or index specified in the message has been damaged by a software or hardware problem.<br /><br /> Severity level 22 errors occur rarely. If one occurs, run DBCC CHECKDB to determine whether other objects in the database are also damaged. The problem might be in the buffer cache only and not on the disk itself. If so, restarting the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] corrects the problem. To continue working, you must reconnect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]; otherwise, use DBCC to repair the problem. In some cases, you may have to restore the database.<br /><br /> If restarting the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not correct the problem, then the problem is on the disk. Sometimes destroying the object specified in the error message can solve the problem. For example, if the message reports that the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] has found a row with a length of 0 in a nonclustered index, delete the index and rebuild it.|  
|23|Indicates that the integrity of the entire database is in question because of a hardware or software problem.<br /><br /> Severity level 23 errors occur rarely. If one occurs, run DBCC CHECKDB to determine the extent of the damage. The problem might be in the cache only and not on the disk itself. If so, restarting the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] corrects the problem. To continue working, you must reconnect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]; otherwise, use DBCC to repair the problem. In some cases, you may have to restore the database.|  
|24|Indicates a media failure. The system administrator may have to restore the database. You may also have to call your hardware vendor.|  
  
## User-Defined Error Message Severity  
 **sp_addmessage** can be used to add user-defined error messages with severities from 1 through 25 to the **sys.messages** catalog view. These user-defined error messages can be used by RAISERROR. For more information, see [sp_addmessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md).  
  
 RAISERROR can be used to generate user-defined error messages with severities from 1 through 25. RAISERROR can reference a user-defined error message stored in the **sys.messages** catalog view or build a message dynamically. When using the user-defined error message in **sys.messages** while generating an error, the severity specified by RAISERROR overrides the severity specified in **sys.messages**. For more information, see [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md).  
  
## Error Severity and TRY...CATCH  
 A TRY...CATCH construct catches all execution errors with severity greater than 10 that do not terminate the database connection.  
  
 Errors with severity from 0 through 10 are informational messages and do not cause execution to jump from the CATCH block of a TRY...CATCH construct.  
  
 Errors that terminate the database connection, usually with severity from 20 through 25, are not handled by the CATCH block because execution is aborted when the connection terminates.  
  
 For more information, see [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md).  
  
## Retrieving Error Severity  
 The ERROR_SEVERITY system function can be used to retrieve the severity of the error that caused the CATCH block of a TRY...CATCH construct to be run. ERROR_SEVERITY returns NULL if called outside the scope of a CATCH block. For more information, see [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md).  
  
## See Also  
 [Understanding Database Engine Errors](../../relational-databases/errors-events/understanding-database-engine-errors.md)   
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)  
  
  
