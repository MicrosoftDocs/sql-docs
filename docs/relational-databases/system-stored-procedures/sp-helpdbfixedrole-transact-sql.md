---
title: "sp_helpdbfixedrole (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpdbfixedrole"
  - "sp_helpdbfixedrole_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpdbfixedrole"
ms.assetid: ad87e9a0-b901-4e37-9950-aa517d680fc3
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpdbfixedrole (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a list of the fixed database roles.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdbfixedrole [ [ @rolename = ] 'role' ]   
```  
  
## Arguments  
`[ @rolename = ] 'role'`
 Is the name of a fixed database role. *role* is **sysname**, with a default of NULL. If *role* is specified, only information about that role is returned; otherwise, a list and description of all fixed database roles is returned.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**DbFixedRole**|**sysname**|Name of the fixed database role.|  
|**Description**|**nvarchar(70)**|Description of **DbFixedRole.**|  
  
## Remarks  
 Fixed database roles, as shown in the following table, are defined at the database level and have permissions to perform specific database-level administrative activities. Fixed database roles cannot be added or removed. The permissions granted to a fixed database role cannot be changed.  
  
|Fixed database role|Description|  
|-------------------------|-----------------|  
|**db_owner**|Database owners|  
|**db_accessadmin**|Database access administrators|  
|**db_securityadmin**|Database security administrators|  
|**db_ddladmin**|Database DDL administrators|  
|**db_backupoperator**|Database backup operators|  
|**db_datareader**|Database data readers|  
|**db_datawriter**|Database data writers|  
|**db_denydatareader**|Database deny data readers|  
|**db_denydatawriter**|Database deny data writers|  
  
 The following table shows stored procedures that are used for modifying database roles.  
  
|Stored procedure|Action|  
|----------------------|------------|  
|**sp_addrolemember**|Adds a database user to a fixed database role.|  
|**sp_helprole**|Displays a list of the members of a fixed database role.|  
|**sp_droprolemember**|Removes a member from a fixed database role.|  
  
## Permissions  
 Requires membership in the **public** role.  
  
 Information returned is subject to restrictions on access to metadata. Entities on which the principal has no permission do not appear. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example shows a list of all fixed database roles.  
  
```  
EXEC sp_helpdbfixedrole;  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sp_dbfixedrolepermission &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbfixedrolepermission-transact-sql.md)   
 [sp_droprolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droprolemember-transact-sql.md)   
 [sp_helprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprole-transact-sql.md)   
 [sp_helprolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprolemember-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
