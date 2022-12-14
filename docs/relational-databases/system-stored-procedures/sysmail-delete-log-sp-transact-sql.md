---
description: "sysmail_delete_log_sp (Transact-SQL)"
title: "sysmail_delete_log_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysmail_delete_log_sp_TSQL"
  - "sysmail_delete_log_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_delete_log_sp"
ms.assetid: e94b37a1-70ad-46a5-86c0-721892156f7c
author: markingmyname
ms.author: maghan
---
# sysmail_delete_log_sp (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Deletes events from the Database Mail log. Deletes all events in the log or those events meeting a date or type criteria.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_delete_log_sp  [ [ @logged_before = ] 'logged_before' ]  
    [, [ @event_type = ] 'event_type' ]  
  
```  
  
## Arguments  
`[ @logged_before = ] 'logged_before'`
 Deletes entries up to the date and time specified by the *logged_before* argument. *logged_before* is **datetime** with NULL as default. NULL indicates all dates.  
  
`[ @event_type = ] 'event_type'`
 Deletes log entries of the type specified as the *event_type*. *event_type* is **varchar(15)** with no default. Valid entries are **success**, **warning**, **error**, and **informational**. NULL indicates all event types.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Use the **sysmail_delete_log_sp** stored procedure to permanently delete entries from the Database Mail log. An optional argument allows you to delete only the older records by providing a date and time. Events older than that argument will be deleted. An optional argument allows you to delete only events of a certain type, specified as the **event_type** argument.  
  
 Deleting entries in the Database Mail log does not delete the e-mails entries from the Database Mail tables. Use [sysmail_delete_mailitems_sp](../../relational-databases/system-stored-procedures/sysmail-delete-mailitems-sp-transact-sql.md) to delete e-mail from the Database Mail tables.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can access this procedure.  
  
## Examples  
  
### A. Deleting all events  
 The following example deletes all events in the Database Mail log.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_log_sp ;  
GO  
```  
  
### B. Deleting the oldest events  
 The following example deletes events in the Database Mail log that are older than October 9, 2005.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_log_sp  
    @logged_before = 'October 9, 2005' ;  
GO  
```  
  
### C. Deleting all events of a certain type  
 The following example deletes success messages in the Database Mail log.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_log_sp  
    @event_type = 'success' ;  
GO  
```  
  
## See Also  
 [sysmail_event_log &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-event-log-transact-sql.md)   
 [sysmail_delete_mailitems_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-delete-mailitems-sp-transact-sql.md)   
 [Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../../relational-databases/database-mail/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md)  
  
  
