---
description: "sp_changedbowner (Transact-SQL)"
title: "sp_changedbowner (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_changedbowner"
  - "sp_changedbowner_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_changedbowner"
ms.assetid: 516ef311-e83b-45c9-b9cd-0e0641774c04
author: VanMSFT
ms.author: vanto
---
# sp_changedbowner (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the owner of the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changedbowner [ @loginame = ] 'login'  
     [ , [ @map = ] remap_alias_flag ]  
```  
  
## Arguments  
 [ @loginame= ] '*login*'  
 Is the login ID of the new owner of the current database. *login* is **sysname**, with no default. *login* must be an already existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows user. *login* cannot become the owner of the current database if it already has access to the database through an existing user security account within the database. To avoid this, drop the user within the current database first.  
  
 [ @map= ] *remap_alias_flag*  
 The *remap_alias_flag* parameter is deprecated because login aliases have been removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Using the *remap_alias_flag* parameter does not cause an error but has no effect.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 After sp_changedbowner is executed, the new owner is known as the dbo user inside the database. The dbo has implied permissions to perform all activities in the database.  
  
 The owner of the master, model, or tempdb system databases cannot be changed.  
  
 To display a list of the valid *login* values, execute the sp_helplogins stored procedure.  
  
 Executing sp_changedbowner with only the *login* parameter changes database ownership to *login*.  
  
 You can change the owner of any securable by using the ALTER AUTHORIZATION statement. For more information, see [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md).  
  
## Permissions  
 Requires TAKE OWNERSHIP permission on the database. If the new owner has a corresponding user in the database, requires IMPERSONATE permission on the login, otherwise requires CONTROL SERVER permission on the server.  
  
## Examples  
 The following example makes the login `Albert` the owner of the current database.  
  
```  
EXEC sp_changedbowner 'Albert';  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md)   
 [sp_dropalias &#40;Transact-SQL&#41;](./system-stored-procedures-transact-sql.md)   
 [sp_dropuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropuser-transact-sql.md)   
 [sp_helpdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdb-transact-sql.md)   
 [sp_helplogins &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplogins-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
