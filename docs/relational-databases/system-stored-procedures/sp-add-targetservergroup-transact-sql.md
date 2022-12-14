---
description: "sp_add_targetservergroup (Transact-SQL)"
title: "sp_add_targetservergroup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_add_targetservergroup"
  - "sp_add_targetservergroup_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_targetservergroup"
ms.assetid: acb69343-d766-46ff-b771-0c7655c5231a
author: markingmyname
ms.author: maghan
---
# sp_add_targetservergroup (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Adds the specified server group.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_targetservergroup [ @name = ] 'name'   
```  
  
## Arguments  
`[ @name = ] 'name'`
 The name of the server group to create. *name* is **sysname**, with no default. *name* cannot contain commas.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Target server groups provide an easy way to target a job at a collection of target servers. For more information, see [sp_apply_job_to_targets](../../relational-databases/system-stored-procedures/sp-apply-job-to-targets-transact-sql.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute this procedure.  
  
## Examples  
 The following example creates the target server group named `Servers Processing Customer Orders`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_targetservergroup  
    'Servers Processing Customer Orders' ;  
GO  
```  
  
## See Also  
 [sp_apply_job_to_targets &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-apply-job-to-targets-transact-sql.md)   
 [sp_delete_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-targetservergroup-transact-sql.md)   
 [sp_help_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-targetservergroup-transact-sql.md)   
 [sp_update_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-targetservergroup-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
