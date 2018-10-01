---
title: "sp_addrolemember (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addrolemember_TSQL"
  - "sp_addrolemember"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addrolemember"
ms.assetid: a583c087-bdb3-46d2-b9e5-3921b3e6d10b
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_addrolemember (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Adds a database user, database role, Windows login, or Windows group to a database role in the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER ROLE](../../t-sql/statements/alter-role-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
sp_addrolemember [ @rolename = ] 'role',  
    [ @membername = ] 'security_account'  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
sp_addrolemember 'role', 'security_account'  
```  
  
## Arguments  
 [ @rolename= ] '*role*'  
 Is the name of the database role in the current database. *role* is a **sysname**, with no default.  
  
 [ @membername= ] '*security_account*'  
 Is the security account being added to the role. *security_account* is a **sysname**, with no default. *security_account* can be a database user, database role, Windows login, or Windows group.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 A member added to a role by using sp_addrolemember inherits the permissions of the role. If the new member is a Windows-level principal without a corresponding database user, a database user will be created but may not be fully mapped to the login. Always check that the login exists and has access to the database.  
  
 A role cannot include itself as a member. Such "circular" definitions are not valid, even when membership is only indirectly implied by one or more intermediate memberships.  
  
 sp_addrolemember cannot add a fixed database role, fixed server role, or dbo to a role. sp_addrolemember cannot be executed within a user-defined transaction.  
  
 Only use sp_addrolemember to add a member to a database role. To add a member to a server role, use [sp_addsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md).  
  
## Permissions  
 Adding members to flexible database roles requires one of the following:  
  
-   Membership in the db_securityadmin or db_owner fixed database role.  
  
-   Membership in the role that owns the role.  
  
-   **ALTER ANY ROLE** permission or **ALTER** permission on the role.  
  
 Adding members to fixed database roles requires membership in the db_owner fixed database role.  
  
## Examples  
  
### A. Adding a Windows login  
 The following example adds the Windows login `Contoso\Mary5` to the `AdventureWorks2012` database as user `Mary5`. The user `Mary5` is then added to the `Production` role.  
  
> [!NOTE]  
>  Because `Contoso\Mary5` is known as the database user `Mary5` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database, the user name `Mary5` must be specified. The statement will fail unless a `Contoso\Mary5` login exists. Test by using a login from your domain.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE USER Mary5 FOR LOGIN [Contoso\Mary5] ;  
GO  
```  
  
### B. Adding a database user  
 The following example adds the database user `Mary5` to the `Production` database role in the current database.  
  
```  
EXEC sp_addrolemember 'Production', 'Mary5';  
```  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Adding a Windows login  
 The following example adds the login `LoginMary` to the `AdventureWorks2008R2` database as user `UserMary`. The user `UserMary` is then added to the `Production` role.  
  
> [!NOTE]  
>  Because the login `LoginMary` is known as the database user `UserMary` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database, the user name `UserMary` must be specified. The statement will fail unless a `Mary5` login exists. Logins and users usually have the same name. This example uses different names to differentiate the actions affecting the login vs. the user.  
  
```  
-- Uses AdventureWorks  
  
CREATE USER UserMary FOR LOGIN LoginMary ;  
GO  
EXEC sp_addrolemember 'Production', 'UserMary'  
```  
  
### D. Adding a database user  
 The following example adds the database user `UserMary` to the `Production` database role in the current database.  
  
```  
EXEC sp_addrolemember 'Production', 'UserMary'  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_addsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md)   
 [sp_droprolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droprolemember-transact-sql.md)   
 [sp_grantdbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantdbaccess-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md)  
  
  
