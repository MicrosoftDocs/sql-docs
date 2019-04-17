---
title: "sp_delete_targetserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_targetserver"
  - "sp_delete_targetserver_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_targetserver"
ms.assetid: cc438701-ad91-419d-9f23-ebc4c548c700
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_delete_targetserver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes the specified server from the list of available target servers.  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_targetserver [ @server_name = ] 'server'   
     [ , [ @clear_downloadlist = ] clear_downloadlist ]  
     [ , [ @post_defection = ] post_defection ]  
```  
  
## Arguments  
`[ @server_name = ] 'server'`
 The name of the server to remove as an available target server. *server* is **nvarchar(30)**, with no default.  
  
`[ @clear_downloadlist = ] clear_downloadlist`
 Specifies whether to clear the download list for the target server. *clear_downloadlist* is type **bit**, with a default of **1**. When *clear_downloadlist* is **1**, the procedure clears the download list for the server before deleting the server. When *clear_downloadlist* is **0**, the download list is not cleared.  
  
`[ @post_defection = ] post_defection`
 Specifies whether to post a defect instruction to the target server. *post_defection* is type **bit**, with a default of 1. When *post_defection* is **1**, the procedure posts a defect instruction to the target server before deleting the server. When *post_defection* is **0**, the procedure does not post a defect instruction to the target server.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 The normal way to delete a target server is to call **sp_msx_defect** at the target server. Use **sp_delete_targetserver** only when a manual defection is necessary.  
  
## Permissions  
 To run this stored procedure, users must be granted the **sysadmin** fixed server role.  
  
## Examples  
 The following example removes the server `LONDON1` from the available job servers.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_targetserver  
  @server_name = N'LONDON1' ;  
GO  
```  
  
## See also  
 [sp_help_targetserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-targetserver-transact-sql.md)   
 [sp_msx_defect &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-msx-defect-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
