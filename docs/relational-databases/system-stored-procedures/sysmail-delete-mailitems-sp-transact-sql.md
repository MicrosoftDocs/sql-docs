---
title: "sysmail_delete_mailitems_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_delete_mailitems_sp_TSQL"
  - "sysmail_delete_mailitems_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_delete_mailitems_sp"
ms.assetid: f87c9f4a-bda1-4bce-84b2-a055a3229ecd
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_delete_mailitems_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Permanently deletes e-mail messages from the Database Mail internal tables.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_delete_mailitems_sp  [ [ @sent_before = ] 'sent_before' ]  
    [ , [ @sent_status = ] 'sent_status' ]  
```  
  
## Arguments  
`[ @sent_before = ] 'sent_before'`
 Deletes e-mails up to the date and time provided as the *sent_before* argument. *sent_before* is **datetime** with NULL as default. NULL indicates all dates.  
  
`[ @sent_status = ] 'sent_status'`
 Deletes e-mails of the type specified by *sent_status*. *sent_status* is **varchar(8)** with no default. Valid entries are **sent**, **unsent**, **retrying**, and **failed**. NULL indicates all statuses.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Database Mail messages and their attachments are stored in the **msdb** database. Messages should be periodically deleted to prevent **msdb** from growing larger than expected and to comply with your organizations document retention program. Use the **sysmail_delete_mailitems_sp** stored procedure to permanently delete e-mail messages from the Database Mail tables. An optional argument allows you to delete only older e-mails by providing a date and time. E-mails older than that argument will be deleted. Another optional argument allows you to delete only e-mails of a certain type, specified as the **sent_status** argument. You must provide an argument either for **@sent_before** or **@sent_status**. To delete all messages, use **@sent_before = getdate()**.  
  
 Deleting e-mail also deletes attachments related to those messages. Deleting e-mail does not delete the corresponding entries in **sysmail_event_log**. Use [sysmail_delete_log_sp](../../relational-databases/system-stored-procedures/sysmail-delete-log-sp-transact-sql.md) to delete items from the log.  
  
## Permissions  
 By default, this stored procedure is granted for execution to members off the **sysadmin** fixed server role and **DatabaseMailUserRole**. Members of the **sysadmin** fixed server role can execute this procedure to delete e-mails sent by all users. Members of **DatabaseMailUserRole** can only delete e-mails sent by that user.  
  
## Examples  
  
### A. Deleting all e-mails  
 The following example deletes all e-mails in the Database Mail system.  
  
```  
DECLARE @GETDATE datetime  
SET @GETDATE = GETDATE();  
EXECUTE msdb.dbo.sysmail_delete_mailitems_sp @sent_before = @GETDATE;  
GO  
```  
  
### B. Deleting the oldest e-mails  
 The following example deletes e-mails in the Database Mail log that are older than `October 9, 2005`.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_mailitems_sp   
    @sent_before = 'October 9, 2005' ;  
GO  
```  
  
### C. Deleting all e-mails of a certain type  
 The following example deletes all failed e-mails in the Database Mail log.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_mailitems_sp   
    @sent_status = 'failed' ;  
GO  
```  
  
## See Also  
 [sysmail_allitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-allitems-transact-sql.md)   
 [sysmail_event_log &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-event-log-transact-sql.md)   
 [sysmail_mailattachments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-mailattachments-transact-sql.md)   
 [Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs](../../relational-databases/database-mail/create-a-sql-server-agent-job-to-archive-database-mail-messages-and-event-logs.md)  
  
  
