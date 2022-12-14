---
description: "sp_resync_targetserver (Transact-SQL)"
title: "sp_resync_targetserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_resync_targetserver"
  - "sp_resync_targetserver_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_resync_targetserver"
ms.assetid: 40e44df7-d3e3-44ee-b149-08aba629a21f
author: markingmyname
ms.author: maghan
---
# sp_resync_targetserver (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Resynchronizes all multiserver jobs in the specified target server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_resync_targetserver  
     [ @server_name = ] 'server'  
```  
  
## Arguments  
`[ @server_name = ] 'server'`
 The name of the server to resynchronize. *server* is **sysname**, with no default. If **ALL** is specified, all target servers are resynchronized.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 Reports the result of **sp_post_msx_operation** actions.  
  
## Remarks  
 **sp_resync_targetserver** deletes the current set of instructions for the target server and posts a new set for the target server to download. The new set consists of an instruction to delete all multiserver jobs, followed by an insert for each job currently targeted at the server.  
  
## Permissions  
 Permissions to execute this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example resynchronizes the `SEATTLE1` target server.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_resync_targetserver  
    N'SEATTLE1' ;  
GO  
```  
  
## See Also  
 [sp_help_downloadlist &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-downloadlist-transact-sql.md)   
 [sp_post_msx_operation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-post-msx-operation-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
