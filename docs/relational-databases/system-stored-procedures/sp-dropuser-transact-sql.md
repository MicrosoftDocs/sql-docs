---
title: "sp_dropuser (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropuser"
  - "sp_dropuser_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dropuser"
ms.assetid: e28f18f9-7ecf-4568-89f4-fe5c520df386
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropuser (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a database user from the current database. **sp_dropuser** provides compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP USER](../../t-sql/statements/drop-user-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropuser [ @name_in_db = ] 'user'  
```  
  
## Arguments  
`[ @name_in_db = ] 'user'`
 Is the name of the user to remove. *user* is a **sysname**, with no default. *user* must exist in the current database. When specifying a Windows login, use the name by which the database knows that login.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_dropuser** executes **sp_revokedbaccess** to remove the user from the current database.  
  
 Use **sp_helpuser** to display a list of the user names that can be removed from the current database.  
  
 When a database user is removed, any aliases to that user are also removed. If the user owns an empty schema with the same name as the user, the schema will be dropped. If the user owns any other securables in the database, the user will not be dropped. Ownership of the objects must first be transferred to another principal. For more information, see [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md). Removing a database user automatically removes the permissions associated with that user and removes the user from any database roles of which it is a member.  
  
 **sp_dropuser** cannot be used to remove the database owner (**dbo**) **INFORMATION_SCHEMA** users, or the **guest** user from the **master** or **tempdb** databases. In nonsystem databases, `EXEC sp_dropuser 'guest'` will revoke CONNECT permission from user **guest**. But the user itself will not be dropped.  
  
 **sp_dropuser** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY USER permission on the database.  
  
## Examples  
 The following example removes the user `Albert` from the current database.  
  
```  
EXEC sp_dropuser 'Albert';  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_grantdbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantdbaccess-transact-sql.md)   
 [DROP USER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-user-transact-sql.md)   
 [sp_revokedbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokedbaccess-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
