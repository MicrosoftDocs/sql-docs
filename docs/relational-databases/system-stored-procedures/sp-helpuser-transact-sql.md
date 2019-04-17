---
title: "sp_helpuser (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpuser"
  - "sp_helpuser_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpuser"
ms.assetid: 9c70b41d-ef4c-43df-92da-bd534c287ca1
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpuser (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports information about database-level principals in the current database.  
  
> [!IMPORTANT]  
>  **sp_helpuser** does not return information about securables that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. Use [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpuser [ [ @name_in_db = ] 'security_account' ]  
```  
  
## Arguments  
`[ @name_in_db = ] 'security_account'`
 Is the name of database user or database role in the current database. *security_account* must exist in the current database. *security_account* is **sysname**, with a default of NULL. If *security_account* is not specified, **sp_helpuser** returns information about all database principals.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 The following table shows the result set when neither a user account nor a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Windows user is specified for *security_account*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**UserName**|**sysname**|Users in the current database.|  
|**RoleName**|**sysname**|Roles to which **UserName** belongs.|  
|**LoginName**|**sysname**|Login of **UserName**.|  
|**DefDBName**|**sysname**|Default database of **UserName**.|  
|**DefSchemaName**|**sysname**|Default schema of the database user.|  
|**UserID**|**smallint**|ID of **UserName** in the current database.|  
|**SID**|**smallint**|User security identification number (SID).|  
  
 The following table shows the result set when no user account is specified and aliases exist in the current database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**LoginName**|**sysname**|Logins aliased to users in the current database.|  
|**UserNameAliasedTo**|**sysname**|User name in the current database to which the login is aliased.|  
  
 The following table shows the result set when a role is specified for *security_account*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Role_name**|**sysname**|Name of the role in the current database.|  
|**Role_id**|**smallint**|Role ID for the role in the current database.|  
|**Users_in_role**|**sysname**|Member of the role in the current database.|  
|**Userid**|**smallint**|User ID for the member of the role.|  
  
## Remarks  
 To see information about membership of database roles, use [sys.database_role_members](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md). To see information about server role members, use [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md), and to see information about server-level principals, use [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md).  
  
## Permissions  
 Requires membership in the **public** role.  
  
 Information returned is subject to restrictions on access to metadata. Entities on which the principal has no permission do not appear. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Listing all users  
 The following example lists all users in the current database.  
  
```  
EXEC sp_helpuser;  
```  
  
### B. Listing information for a single user  
 The following example lists information about the user database owner (`dbo`).  
  
```  
EXEC sp_helpuser 'dbo';  
```  
  
### C. Listing information for a database role  
 The following example lists information about the `db_securityadmin` fixed database role.  
  
```  
EXEC sp_helpuser 'db_securityadmin';  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.server_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md)  
  
  
