---
title: "sp_helpntgroup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpntgroup"
  - "sp_helpntgroup_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpntgroup"
ms.assetid: 02b4f7c1-480a-436c-8bae-7a2488be45d2
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpntgroup (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports information about Windows groups with accounts in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpntgroup [ [ @ntname= ] 'name' ]   
```  
  
## Arguments  
 [ **@ntname =** ] **'***name***'**  
 Is the name of the Windows group. *name* is **sysname**, with a default of NULL. *name* must be a valid Windows group with access to the current database. If *name* is not specified, all Windows groups with access to the current database are included in the output.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**NTGroupName**|**sysname**|Name of the Windows group.|  
|**NTGroupId**|**smallint**|Group identifier (ID).|  
|**SID**|**varbinary(85)**|Security identifier (SID) of **NTGroupName**.|  
|**HasDbAccess**|**int**|1 = Windows group has permission to access the database.|  
  
## Remarks  
 To see a list of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] roles in the current database, use **sp_helprole**.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example prints a list of the Windows groups with access to the current database.  
  
```  
EXEC sp_helpntgroup;  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_grantdbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantdbaccess-transact-sql.md)   
 [sp_helprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprole-transact-sql.md)   
 [sp_revokedbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokedbaccess-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
