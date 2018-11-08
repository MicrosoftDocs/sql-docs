---
title: "sp_droprolemember (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_droprolemember_TSQL"
  - "sp_droprolemember"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_droprolemember"
ms.assetid: c2f19ab1-e742-4d56-ba8e-8ffd40cf4925
ms.author: vanto
manager: craigg
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_droprolemember (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes a security account from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] role in the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER ROLE](../../t-sql/statements/alter-role-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
sp_droprolemember [ @rolename = ] 'role' ,   
     [ @membername = ] 'security_account'  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
sp_droprolemember 'role' ,  
     'security_account'  
```  
  
## Arguments  
 [ **@rolename =** ] **'***role***'**  
 Is the name of the role from which the member is being removed. *role* is **sysname**, with no default. *role* must exist in the current database.  
  
 [ **@membername =** ] **'***security_account***'**  
 Is the name of the security account being removed from the role. *security_account* is **sysname**, with no default. *security_account* can be a database user, another database role, a Windows login, or a Windows group. *security_account* must exist in the current database.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 sp_droprolemember removes a member from a database role by deleting a row from the sysmembers table. When a member is removed from a role the member loses any permissions it has by membership in that role.  
  
 To remove a user from a fixed server role, use sp_dropsrvrolemember. Users cannot be removed from the public role, and dbo cannot be removed from any role.  
  
 Use sp_helpuser to see the members of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] role, and use ALTER ROLE to add a member to a role.  
  
## Permissions  
 Requires ALTER permission on the role.  
  
## Examples  
 The following example removes the user `JonB` from the role `Sales`.  
  
```  
EXEC sp_droprolemember 'Sales', 'Jonb';  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example removes the user `JonB` from the role `Sales`.  
  
```  
EXEC sp_droprolemember 'Sales', 'JonB'  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sp_droprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droprole-transact-sql.md)   
 [sp_dropsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsrvrolemember-transact-sql.md)   
 [sp_helpuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpuser-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  

