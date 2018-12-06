---
title: "Database Mail Messaging Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Mail [SQL Server], host databases"
  - "Database Mail [SQL Server], messaging objects"
  - "mail host databases [SQL Server]"
  - "host databases [Database Mail]"
ms.assetid: 5aa2886e-1db1-4066-85df-57ccf4538c54
author: stevestein
ms.author: sstein
manager: craigg
---
# Database Mail Messaging Objects
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **msdb** database is the Database Mail host database. This database contains the stored procedures and messaging objects for Database Mail. Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] includes the Database Mail Configuration Wizard for enabling Database Mail, creating and managing profiles and accounts, and configuring Database Mail options.  
  
##  <a name="ComponentsAndConcepts"></a> Objects in **msdb** database  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] must be enabled in the **msdb** database. However, Database Mail does not use [!INCLUDE[ssSB](../../includes/sssb-md.md)] networking. Therefore, users do not have to create a [!INCLUDE[ssSB](../../includes/sssb-md.md)] endpoint to use Database Mail. The external Database Mail process uses a standard [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection to communicate with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Database Mail exposes the following objects in the **msdb** database when Database Mail is enabled.  
  
 These objects are the interface for Database Mail within the mail host database. Other objects are installed to implement the functionality provided by the objects listed above. However, those objects are reserved for internal use.  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|[sysmail_allitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-allitems-transact-sql.md)|**View**|Lists all messages submitted to Database Mail.|  
|[sysmail_event_log &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-event-log-transact-sql.md)|**View**|Lists messages about the behavior of the [Database Mail External Program](../../relational-databases/database-mail/database-mail-external-program.md).|  
|[sysmail_faileditems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-faileditems-transact-sql.md)|**View**|Information about messages that Database Mail could not sent.|  
|[sysmail_mailattachments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-mailattachments-transact-sql.md)|**View**|Information about attachments to Database Mail messages.|  
|[sysmail_sentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-sentitems-transact-sql.md)|**View**|Information about messages that have been sent using Database Mail.|  
|[sysmail_unsentitems &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sysmail-unsentitems-transact-sql.md)|**View**|Information about messages that Database Mail in currently trying to send.|  
|[sp_send_dbmail &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-send-dbmail-transact-sql.md)|**Stored Procedure**|Sends e-mail messages using Database Mail.|  
|[sysmail_delete_log_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-delete-log-sp-transact-sql.md)|**Stored Procedure**|Deletes messages from the Database Mail log.|  
|[sysmail_delete_mailitems_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-delete-mailitems-sp-transact-sql.md)|**Stored Procedure**|Deletes mail items from the Database Mail queue.|  
|[sysmail_help_status_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-help-status-sp-transact-sql.md)|**Stored Procedure**|Indicates if Database Mail is started.|  
|[sysmail_start_sp (Transact-SQL)](../../relational-databases/system-stored-procedures/sysmail-start-sp-transact-sql.md)|**Stored Procedure**|Starts the Service Broker objects that the external program uses. These objects are started by default.|  
|[sysmail_stop_sp (Transact-SQL)](../../relational-databases/system-stored-procedures/sysmail-stop-sp-transact-sql.md)|**Stored Procedure**|Stops the Service Broker objects that the external program uses.|  
  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
