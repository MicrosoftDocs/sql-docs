---
title: "sp_helpsrvrole (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpsrvrole_TSQL"
  - "sp_helpsrvrole"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpsrvrole"
ms.assetid: 5c7f39f3-c261-4f70-8beb-08242d4ac242
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpsrvrole (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a list of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fixed server roles.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpsrvrole [ [ @srvrolename = ] 'role' ]  
```  
  
## Arguments  
 [ **@srvrolename=** ] **'***role***'**  
 Is the name of the fixed server role. *role* is **sysname**, with a default of NULL. *role* can be one of the following values.  
  
|Fixed server role|Description|  
|-----------------------|-----------------|  
|sysadmin|System administrators|  
|securityadmin|Security administrators|  
|serveradmin|Server administrators|  
|setupadmin|Setup administrators|  
|processadmin|Process administrators|  
|diskadmin|Disk administrators|  
|dbcreator|Database creators|  
|bulkadmin|Can execute BULK INSERT statements|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|ServerRole|**sysname**|Name of the server role|  
|Description|**sysname**|Description of ServerRole|  
  
## Remarks  
 Fixed server roles are defined at the server level and have permissions to perform specific server-level administrative activities. Fixed server roles cannot be added, removed, or changed.  
  
 To add or removed members from server roles, see [ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-role-transact-sql.md).  
  
 All logins are a member of public. sp_helpsrvrole does not recognize the public role because, internally, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not implement public as a role.  
  
 sp_helpsrvrole does not take a user-defined server role as an argument. To list the user-defined server roles, see the examples in [ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-role-transact-sql.md).  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
  
### A. Listing the fixed server roles  
 The following query returns the list of fixed server roles.  
  
```  
EXEC sp_helpsrvrole ;  
```  
  
### B. Listing fixed and user-defined server roles  
 The following query returns a list of both fixed and user-defined server roles.  
  
```  
SELECT * FROM sys.server_principals WHERE type = 'R' ;  
```  
  
### C. Returning a description of a fixed server role  
 The following query returns the name and description of the `diskadmin` fixed server roles.  
  
```  
sp_helpsrvrole 'diskadmin' ;  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md)   
 [sp_addsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md)   
 [sp_dropsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsrvrolemember-transact-sql.md)   
 [sp_helpsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsrvrolemember-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
