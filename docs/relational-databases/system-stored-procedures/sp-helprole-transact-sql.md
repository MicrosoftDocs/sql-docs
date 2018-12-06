---
title: "sp_helprole (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helprole_TSQL"
  - "sp_helprole"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helprole"
ms.assetid: b023103f-ccf3-44e2-b418-4be9bdd49f4a
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_helprole (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about the roles in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helprole [ [ @rolename = ] 'role' ]  
```  
  
## Arguments  
 [ **@rolename =** ] **'***role***'**  
 Is the name of a role in the current database. *role* is **sysname**, with a default of NULL. *role* must exist in the current database. If *role* is not specified, information about all roles in the current database is returned.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**RoleName**|**sysname**|Name of the role in the current database.|  
|**RoleId**|**smallint**|ID of **RoleName**.|  
|**IsAppRole**|**int**|0 = **RoleName** is not an application role.<br /><br /> 1 = **RoleName** is an application role.|  
  
## Remarks  
 To view the permissions associated with the role, use **sp_helprotect**. To view the members of a database role, use **sp_helprolemember**.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following query returns all the roles in the current database.  
  
```  
EXEC sp_helprole  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md)   
 [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md)   
 [sp_addapprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addapprole-transact-sql.md)   
 [sp_addrole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrole-transact-sql.md)   
 [sp_droprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droprole-transact-sql.md)   
 [sp_helprolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprolemember-transact-sql.md)   
 [sp_helpsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsrvrolemember-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
