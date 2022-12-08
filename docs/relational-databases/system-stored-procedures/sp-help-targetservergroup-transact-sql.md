---
description: "sp_help_targetservergroup (Transact-SQL)"
title: "sp_help_targetservergroup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_targetservergroup_TSQL"
  - "sp_help_targetservergroup"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_targetservergroup"
ms.assetid: ec3a4a68-b591-431c-9518-053ede522d0c
author: markingmyname
ms.author: maghan
---
# sp_help_targetservergroup (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Lists all target servers in the specified group. If no group is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns information about all target server groups.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_targetservergroup  
    [ [ @name = ] 'name' ]  
```  
  
## Argument  
`[ @name = ] 'name'`
 Is the name of the target server group for which to return information. *name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**servergroup_id**|**int**|Identification number of the server group|  
|**name**|**sysname**|Name of the server group|  
  
## Permissions  
 Permissions to execute this procedure default to the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Listing information for all target server groups  
 This example lists information for all target server groups.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_targetservergroup ;  
GO  
```  
  
### B. Listing information for a specific target server group  
 This example lists information for the `Servers Maintaining Customer Information` target server group.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_targetservergroup   
    N'Servers Maintaining Customer Information' ;  
GO  
```  
  
## See Also  
 [sp_add_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-targetservergroup-transact-sql.md)   
 [sp_delete_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-targetservergroup-transact-sql.md)   
 [sp_update_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-targetservergroup-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
