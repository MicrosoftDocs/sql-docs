---
description: "sp_helprotect (Transact-SQL)"
title: "sp_helprotect (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_helprotect"
  - "sp_helprotect_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helprotect"
ms.assetid: faaa3e40-1c95-43c2-9fdc-c61a1d3cc0c3
author: markingmyname
ms.author: maghan
---
# sp_helprotect (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a report that has information about user permissions for an object, or statement permissions, in the current database.  
  
> [!IMPORTANT]  
>  **sp_helprotect** does not return information about securables that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. Use [sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md) and [fn_builtin_permissions](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md) instead.  
  
 Does not list permissions that are always assigned to the fixed server roles or fixed database roles. Does not include logins or users that receive permissions based on their membership in a role.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helprotect [ [ @name = ] 'object_statement' ]   
     [ , [ @username = ] 'security_account' ]   
     [ , [ @grantorname = ] 'grantor' ]   
     [ , [ @permissionarea = ] 'type' ]  
```  
  
## Arguments  
`[ @name = ] 'object_statement'`
 Is the name of the object in the current database, or a statement, that has the permissions to report. *object_statement* is **nvarchar(776)**, with a default of NULL, which returns all object and statement permissions. If the value is an object (table, view, stored procedure, or extended stored procedure), it must be a valid object in the current database. The object name can include an owner qualifier in the form _owner_**.**_object_.  
  
 If *object_statement* is a statement, it can be a CREATE statement.  
  
`[ @username = ] 'security_account'`
 Is the name of the principal for which permissions are returned. *security_account* is **sysname**, with a default of NULL, which returns all principals in the current database. *security_account* must exist in the current database.  
  
`[ @grantorname = ] 'grantor'`
 Is the name of the principal that granted permissions. *grantor* is **sysname**, with a default of NULL, which returns all information for permissions granted by any principal in the database.  
  
`[ @permissionarea = ] 'type'`
 Is a character string that indicates whether to display object permissions (character string **o**), statement permissions (character string **s**), or both (**os**). *type* is **varchar(10)**,with a default of **os**. *type* can be any combination of **o** and **s**, with or without commas or spaces between **o** and **s**.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Owner**|**sysname**|Name of the object owner.|  
|**Object**|**sysname**|Name of the object.|  
|**Grantee**|**sysname**|Name of the principal to which permissions were granted.|  
|**Grantor**|**sysname**|Name of the principal that granted permissions to the specified grantee.|  
|**ProtectType**|**nvarchar(10)**|Name of the type of protection:<br /><br /> GRANT REVOKE|  
|**Action**|**nvarchar(60)**|Name of the permission. Valid permission statements depend upon the type of object.|  
|**Column**|**sysname**|Type of permission:<br /><br /> All = Permission covers all current columns of the object.<br /><br /> New = Permission covers any new columns that might be changed (by using the ALTER statement) on the object in the future.<br /><br /> All+New = Combination of All and New.<br /><br /> Returns a period if the type of permission does not apply to columns.|  
  
## Remarks  
 All the parameters in the following procedure are optional. If executed with no parameters, `sp_helprotect` displays all the permissions that have been granted or denied in the current database.  
  
 If some but not all the parameters are specified, use named parameters to identify the particular parameter, or `NULL` as a placeholder. For example, to report all permissions for the grantor database owner (`dbo`), execute the following:  
  
```  
EXEC sp_helprotect NULL, NULL, dbo;  
```  
  
 Or  
  
```  
EXEC sp_helprotect @grantorname = 'dbo';  
```  
  
 The output report is sorted by permission category, owner, object, grantee, grantor, protection type category, protection type, action, and column sequential ID.  
  
## Permissions  
 Requires membership in the **public** role.  
  
 Information returned is subject to restrictions on access to metadata. Entities on which the principal has no permission do not appear. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Listing the permissions for a table  
 The following example lists the permissions for the `titles` table.  
  
```  
EXEC sp_helprotect 'titles';  
```  
  
### B. Listing the permissions for a user  
 The following example lists all permissions that user `Judy` has in the current database.  
  
```  
EXEC sp_helprotect NULL, 'Judy';  
```  
  
### C. Listing the permissions granted by a specific user  
 The following example lists all permissions that were granted by user `Judy` in the current database, and uses `NULL` as a placeholder for the missing parameters.  
  
```  
EXEC sp_helprotect NULL, NULL, 'Judy';  
```  
  
### D. Listing the statement permissions only  
 The following example lists all the statement permissions in the current database, and uses `NULL` as a placeholder for the missing parameters.  
  
```  
EXEC sp_helprotect NULL, NULL, NULL, 's';   
```  
  
### e. Listing the permissions for a CREATE statement  
 The following example list all users who have been granted the CREATE TABLE permission.  
  
```  
EXEC sp_helprotect @name = 'CREATE TABLE';  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
