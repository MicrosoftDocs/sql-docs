---
description: "sysmail_help_status_sp (Transact-SQL)"
title: "sysmail_help_status_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysmail_help_status_sp"
  - "sysmail_help_status_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_help_status_sp"
ms.assetid: b44277c6-81e8-4b4d-85b3-a2f04d602e7a
author: markingmyname
ms.author: maghan
---
# sysmail_help_status_sp (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Displays the status of Database Mail queues. Use **sysmail_start_sp** to start the Database Mail queues and **sysmail_stop_sp** to stop the Database Mail queues.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_help_status_sp  
```  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Status**|**nvarchar(7)**|The status of the Database Mail. Possible values are **STARTED** and **STOPPED**.|  
  
## Permissions  
 By default, only members of the **sysadmin** fixed server role can access this procedure.  
  
## Examples  
 The following example displays the status of Database Mail.  
  
```  
EXECUTE msdb.dbo.sysmail_help_status_sp ;  
GO  
```  
  
 Result set:  
  
```  
Status  
-------  
STARTED  
```  
  
## See Also  
 [Database Mail External Program](../../relational-databases/database-mail/database-mail-external-program.md)   
 [sysmail_start_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-start-sp-transact-sql.md)   
 [sysmail_stop_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-stop-sp-transact-sql.md)  
  
  
