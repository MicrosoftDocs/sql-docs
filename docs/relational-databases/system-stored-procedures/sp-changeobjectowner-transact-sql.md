---
title: "sp_changeobjectowner (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changeobjectowner_TSQL"
  - "sp_changeobjectowner"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_changeobjectowner"
ms.assetid: 45b3dc1c-1cde-45b7-a248-5195c12973e9
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_changeobjectowner (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the owner of an object in the current database.  
  
> [!IMPORTANT]
>  This stored procedure only works with the objects available in [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SCHEMA](../../t-sql/statements/alter-schema-transact-sql.md) or [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md) instead. **sp_changeobjectowner** changes both the schema and the owner. To preserve compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this stored procedure will only change object owners when both the current owner and the new owner own schemas that have the same name as their database user names.  
> 
> [!IMPORTANT]
>  A new permission requirement has been added to this stored procedure.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changeobjectowner [ @objname = ] 'object' , [ @newowner = ] 'owner'  
```  
  
## Arguments  
 [ **@objname =** ] **'**_object_**'**  
 Is the name of an existing table, view, user-defined function, or stored procedure in the current database. *object* is an **nvarchar(776)**, with no default. *object* can be qualified with the owner of the existing object, in the form _existing_owner_**.**_object_ if the schema and its owner have the same name.  
  
 [ **@newowner=**] **'**_owner_ **'**  
 Is the name of the security account that will be the new owner of the object. *owner* is **sysname**, with no default. *owner* must be a valid database user, server role, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login, or Windows group with access to the current database. If the new owner is a Windows user or Windows group for which there is no corresponding database-level principal, a database user will be created.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_changeobjectowner** removes all existing permissions from the object. You will have to reapply any permissions that you want to keep after running **sp_changeobjectowner**. Therefore, we recommend that you script out existing permissions before running **sp_changeobjectowner**. After ownership of the object has been changed, you can use the script to reapply permissions. You must modify the object owner in the permissions script before running.  
  
 To change the owner of a securable, use ALTER AUTHORIZATION. To change a schema, use ALTER SCHEMA.  
  
## Permissions  
 Requires membership in the **db_owner** fixed database role, or membership in both the **db_ddladmin** fixed database role and the **db_securityadmin** fixed database role, and also CONTROL permission on the object.  
  
## Examples  
 The following example changes the owner of the `authors` table to `Corporate\GeorgeW`.  
  
```  
EXEC sp_changeobjectowner 'authors', 'Corporate\GeorgeW';  
GO  
```  
  
## See Also  
 [ALTER SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/alter-schema-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [sp_changedbowner &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
