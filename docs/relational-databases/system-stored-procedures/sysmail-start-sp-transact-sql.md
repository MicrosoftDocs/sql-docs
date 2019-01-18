---
title: "sysmail_start_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_start_sp"
  - "sysmail_start_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_start_sp"
ms.assetid: 25fd7bb6-cfdd-463f-bea8-c6fcb805d3f5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sysmail_start_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Starts Database Mail by starting the [!INCLUDE[ssSB](../../includes/sssb-md.md)] objects that the external program uses.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_start_sp  
```  
  
## Arguments  
 None  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Database Mail is not enabled or installed upon [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Use the Database Mail Configuration wizard to enable and install the Database Mail objects.  
  
 This stored procedure is in the **msdb** database. This stored procedure starts the Database Mail queue that holds outgoing message requests and enables the [!INCLUDE[ssSB](../../includes/sssb-md.md)] activation for the external program.  
  
 When the queues are started, the Database Mail external program can process messages. This procedure allows you to restart the queues after the queues have been stopped with the **sysmail_stop_sp** stored procedure.  
  
> [!NOTE]  
>  This stored procedure only starts the queues for Database Mail. This stored procedure does not activate [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery in the database.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example shows starting Database Mail in the **msdb** database. The example assumes that Database Mail has been enabled.  
  
```  
USE msdb ;  
GO  
  
EXECUTE dbo.sysmail_start_sp ;  
GO  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail XPs Server Configuration Option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md)   
 [sysmail_stop_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-stop-sp-transact-sql.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
