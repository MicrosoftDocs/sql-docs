---
title: "SETUSER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SETUSER_TSQL"
  - "SETUSER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "delegation [SQL Server]"
  - "impersonation [SQL Server]"
  - "SETUSER statement"
  - "user impersonation [SQL Server]"
ms.assetid: 7acfac5c-9ad6-4226-b874-7add36c4ea43
author: VanMSFT
ms.author: vanto
manager: craigg
---
# SETUSER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Allows a member of the **sysadmin** fixed server role or the owner of a database to impersonate another user.  
  
> [!IMPORTANT]  
>  SETUSER is included for backward compatibility only. SETUSER may not be supported in a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. We recommend that you use [EXECUTE AS](../../t-sql/statements/execute-as-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SETUSER [ 'username' [ WITH NORESET ] ]   
```  
  
## Arguments  
 **'** *username* **'**  
 Is the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Windows user in the current database that is impersonated. When *username* is not specified, the original identity of the system administrator or database owner impersonating the user is reset.  
  
 WITH NORESET  
 Specifies that subsequent SETUSER statements (with no specified *username*) should not reset the user identity to system administrator or database owner.  
  
## Remarks  
 SETUSER can be used by a member of the **sysadmin** fixed server role or the owner of a database to adopt the identity of another user to test the permissions of the other user. Membership in the db_owner fixed database role is not sufficient.  
  
 Only use SETUSER with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] users. SETUSER is not supported with Windows users. When SETUSER has been used to assume the identity of another user, any objects that the impersonating user creates are owned by the user being impersonated. For example, if the database owner assumes the identity of user **Margaret** and creates a table called **orders**, the **orders** table is owned by **Margaret**, not the system administrator.  
  
 SETUSER remains in effect until another SETUSER statement is issued or until the current database is changed with the USE statement.  
  
> [!NOTE]  
>  If SETUSER WITH NORESET is used, the database owner or system administrator must log off and then log on again to reestablish his or her own rights.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role or must be the owner of the database. Membership in the **db_owner** fixed database role is not sufficient  
  
## Examples  
 The following example shows how the database owner can adopt the identity of another user. User `mary` has created a table called `computer_types`. By using SETUSER, the database owner impersonates `mary` to grant user `joe` access to the `computer_types` table, and then resets his or her own identity.  
  
```  
SETUSER 'mary';  
GO  
GRANT SELECT ON computer_types TO joe;  
GO  
--To revert to the original user  
SETUSER;  
```  
  
## See Also  
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [USE &#40;Transact-SQL&#41;](../../t-sql/language-elements/use-transact-sql.md)  
  
  
