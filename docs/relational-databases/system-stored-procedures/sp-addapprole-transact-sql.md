---
title: "sp_addapprole (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addapprole_TSQL"
  - "sp_addapprole"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addapprole"
ms.assetid: 24200295-9a54-4cab-9922-fb2e88632721
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_addapprole (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds an application role to the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE APPLICATION ROLE](../../t-sql/statements/create-application-role-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addapprole [ @rolename = ] 'role' , [ @password = ] 'password'  
```  
  
## Arguments  
`[ @rolename = ] 'role'`
 Is the name of the new application role. *role* is **sysname**, with no default. *role* must be a valid identifier and cannot already exist in the current database.  
  
 Application role names can contain from 1 up to 128 characters, including letters, symbols, and numbers. Role names cannot contain a backslash (\\) nor be NULL or an empty string ('').  
  
`[ @password = ] 'password'`
 Is the password required to activate the application role. *password* is **sysname**, with no default. *password* cannot be NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], users (and roles) are not fully distinct from schemas. Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], schemas are fully distinct from roles. This new architecture is reflected in the behavior of CREATE APPLICATION ROLE. This statement supersedes **sp_addapprole**.  
  
 To maintain backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], **sp_addapprole** will do the following:  
  
-   If a schema with the same name as the application role does not already exist, such a schema will be created. The new schema will be owned by the application role, and it will be the default schema of the application role.  
  
-   If a schema of the same name as the application role already exists, the procedure will fail.  
  
-   Password complexity is not checked by **sp_addapprole**. But password complexity is checked by CREATE APPLICATION ROLE.  
  
 The parameter *password* is stored as a one-way hash.  
  
 The **sp_addapprole** stored procedure cannot be executed from within a user-defined transaction.  
  
> [!IMPORTANT]  
>  The Microsoft ODBC **encrypt** option is not supported by **SqlClient**. When you can, prompt users to enter application role credentials at run time. Avoid storing credentials in a file. If you must persist credentials, encrypt them by using the CryptoAPI functions.  
  
## Permissions  
 Requires ALTER ANY APPLICATION ROLE permission on the database. If a schema with the same name and owner as the new role does not already exist, also requires CREATE SCHEMA permission on the database.  
  
## Examples  
 The following example adds the new application role `SalesApp` with the password `x97898jLJfcooFUYLKm387gf3` to the current database.  
  
```  
EXEC sp_addapprole 'SalesApp', 'x97898jLJfcooFUYLKm387gf3' ;  
GO  
```  
  
## See Also  
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)  
  
  
