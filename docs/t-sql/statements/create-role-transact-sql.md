---
title: "CREATE ROLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE ROLE"
  - "DATABASE ROLE"
  - "ROLE_TSQL"
  - "DATABASE_ROLE_TSQL"
  - "CREATE_ROLE_TSQL"
  - "CREATE DATABASE ROLE"
  - "ROLE"
  - "CREATE_DATABASE_ROLE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database roles [SQL Server], creating"
  - "CREATE DATABASE ROLE statement"
  - "roles [SQL Server], creating"
  - "CREATE ROLE statement"
ms.assetid: b0cd54ad-e81d-4d71-acec-8a6d7261ca08
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE ROLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Creates a new database role in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE ROLE role_name [ AUTHORIZATION owner_name ]  
```  
  
## Arguments  
 *role_name*  
 Is the name of the role to be created.  
  
 AUTHORIZATION *owner_name*  
 Is the database user or role that is to own the new role. If no user is specified, the role will be owned by the user that executes CREATE ROLE.  
  
## Remarks  
 Roles are database-level securables. After you create a role, configure the database-level permissions of the role by using GRANT, DENY, and REVOKE. To add members to a database role, use [ALTER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-role-transact-sql.md). For more information, see [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md).  
  
 Database roles are visible in the sys.database_role_members and sys.database_principals catalog views.  
  
 For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 Requires **CREATE ROLE** permission on the database or membership in the **db_securityadmin** fixed database role. When you use the **AUTHORIZATION** option, the following permissions are also required:  
  
-   To assign ownership of a role to another user, requires IMPERSONATE permission on that user.  
  
-   To assign ownership of a role to another role, requires membership in the recipient role or ALTER permission on that role.  
  
-   To assign ownership of a role to an application role, requires ALTER permission on the application role.  
  
## Examples  
The following examples all use the AdventureWorks database.   

### A. Creating a database role that is owned by a database user  
 The following example creates the database role `buyers` that is owned by user `BenMiller`.  
  
```  
CREATE ROLE buyers AUTHORIZATION BenMiller;  
GO  
```  
  
### B. Creating a database role that is owned by a fixed database role  
 The following example creates the database role `auditors` that is owned the `db_securityadmin` fixed database role.  
  
```  
CREATE ROLE auditors AUTHORIZATION db_securityadmin;  
GO  
```  
  
## See Also  
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [ALTER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-role-transact-sql.md)   
 [DROP ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-role-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)  
  
  


