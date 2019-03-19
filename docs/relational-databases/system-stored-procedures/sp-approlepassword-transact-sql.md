---
title: "sp_approlepassword (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_approlepassword"
  - "sp_approlepassword_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_approlepassword"
ms.assetid: 7967dc0b-bee2-4c63-b8e9-1c3ce2f5db2a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_approlepassword (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the password of an application role in the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER APPLICATION ROLE](../../t-sql/statements/alter-application-role-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_approlepassword [ @rolename= ] 'role' , [ @newpwd = ] 'password'   
```  
  
## Arguments  
 [ **@rolename =** ] **'**_role_**'**  
 Is the name of the application role. *Role* is **sysname**, with no default. *role* must exist in the current database.  
  
 [ **@newpwd =** ] **'**_password_**'**  
 Is the new password for the application role. *password* is **sysname**, with no default. *password* cannot be NULL.  
  
> [!IMPORTANT]  
>  Do not use a NULL password. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_approlepassword** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY APPLICATION ROLE permission on the database.  
  
## Examples  
 The following example sets the password for the `PayrollAppRole` application role to `B3r12-36`.  
  
```  
EXEC sp_approlepassword 'PayrollAppRole', '''B3r12-36';  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [Application Roles](../../relational-databases/security/authentication-access/application-roles.md)   
 [sp_addapprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addapprole-transact-sql.md)   
 [sp_setapprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
