---
title: "sp_certify_removable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_certify_removable_TSQL"
  - "sp_certify_removable"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_certify_removable"
ms.assetid: ca12767f-0ae5-4652-b523-c23473f100a1
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_certify_removable (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Verifies that a database is correctly configured for distribution on removable media and reports any problems to the user.  
  
> **IMPORTANT!!** [!INCLUDE[ssNoteDepFutureAvoid](../../t-sql/statements/create-database-sql-server-transact-sql.md) instead.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_certify_removable [ @dbname= ] 'dbname'  
     [ , [ @autofix = ] 'auto' ]  
```  
  
## Arguments  
 [ **@dbname=**] **'***dbname***'**  
 Specifies the database to be verified. *dbname* is **sysname**.  
  
 [ **@autofix=**] **'auto'**  
 Gives ownership of the database and all database objects to the system administrator, and drops any user-created database users and nondefault permissions. *auto* is **nvarchar(4)**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 If the database is correctly configured, **sp_certify_removable** performs the following:  
  
-   Sets the database offline so the files can be copied.  
  
-   Updates statistics on all tables and reports any ownership or user problems  
  
-   Marks the data filegroups as read-only so these files can be copied to read-only media.  
  
 The system administrator must be the owner of the database and all database objects. The system administrator is a known user that exists on all servers that are running [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and can be expected to exist when the database is later distributed and installed.  
  
 If you run **sp_certify_removable** without the **auto** value and it returns information about any of the following conditions:  
  
-   The system administrator is not the database owner.  
  
-   Any user-created users exist.  
  
-   The system administrator does not own all objects in the database.  
  
-   Nondefault permissions have been granted.  
  
 You can correct those conditions in the following ways:  
  
-   Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools and procedures, and then run **sp_certify_removable** again.  
  
-   Just run **sp_certify_removable** with the **auto** value.  
  
 Note that this stored procedure only checks for users and user permissions. You can  add groups to the database and to grant permissions to those groups. For more information, see [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md).  
  
## Permissions  
 Execute permissions are restricted to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example certifies that the `inventory` database is ready to be removed.  
  
```  
EXEC sp_certify_removable inventory, AUTO;  
```  
  
## See Also  
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 [sp_create_removable &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-removable-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sp_dbremove &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbremove-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
