---
description: "sys.fn_my_permissions (Transact-SQL)"
title: "sys.fn_my_permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.fn_my_permissions_TSQL"
  - "fn_my_permissions_TSQL"
  - "sys.fn_my_permissions"
  - "fn_my_permissions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_my_permissions function"
  - "sys.fn_my_permissions function"
ms.assetid: 30f97f00-03d8-443a-9de9-9ec420b7699b
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_my_permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a list of the permissions effectively granted to the principal on a securable. A related function is [HAS_PERMS_BY_NAME](../../t-sql/functions/has-perms-by-name-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_my_permissions ( securable , 'securable_class' )  
```  
  
## Arguments  
 *securable*  
 Is the name of the securable. If the securable is the server or a database, this value should be set to NULL. *securable* is a scalar expression of type **sysname**. *securable* can be a multipart name.  
  
 '*securable_class*'  
 Is the name of the class of securable for which permissions are listed. *securable_class* is a **sysname**. *securable_class* must be one of the following: APPLICATION ROLE, ASSEMBLY, ASYMMETRIC KEY, CERTIFICATE, CONTRACT, DATABASE, ENDPOINT, FULLTEXT CATALOG, LOGIN, MESSAGE TYPE, OBJECT, REMOTE SERVICE BINDING, ROLE, ROUTE, SCHEMA, SERVER, SERVICE, SYMMETRIC KEY, TYPE, USER, XML SCHEMA COLLECTION.  
  
## Columns Returned  
 The following table lists the columns that **fn_my_permissions** returns. Each row that is returned describes a permission held by the current security context on the securable. Returns NULL if the query fails.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|entity_name|**sysname**|Name of the securable on which the listed permissions are effectively granted.|  
|subentity_name|**sysname**|Column name if the securable has columns, otherwise NULL.|  
|permission_name|**nvarchar**|Name of the permission.|  
  
## Remarks  
 This table-valued function returns a list of the effective permissions held by the calling principal on a specified securable. An effective permission is any one of the following:  
  
-   A permission granted directly to the principal, and not denied.  
  
-   A permission implied by a higher-level permission held by the principal and not denied.  
  
-   A permission granted to a role or group of which the principal is a member, and not denied.  
  
-   A permission held by a role or group of which the principal is a member, and not denied.  
  
 The permission evaluation is always performed in the security context of the caller. To determine whether some other principal has an effective permission, the caller must have IMPERSONATE permission on that principal.  
  
 For schema-level entities, one-, two-, or three-part nonnull names are accepted. For database-level entities, a one-part name is accepted, with a null value meaning "*current database*". For the server itself, a null value (meaning "current server") is required. **fn_my_permissions** cannot check permissions on a linked server.  
  
 The following query will return a list of built-in securable classes:  
  
```  
SELECT DISTINCT class_desc FROM fn_builtin_permissions(default)  
    ORDER BY class_desc;  
GO  
```  
  
 If DEFAULT is supplied as the value of *securable* or *securable_class*, the value will be interpreted as NULL.  
 
   
## Permissions  
 Requires membership in the public role.  
 
## Examples  
  
### A. Listing effective permissions on the server  
 The following example returns a list of the effective permissions of the caller on the server.  
  
```  
SELECT * FROM fn_my_permissions(NULL, 'SERVER');  
GO  
```  
  
### B. Listing effective permissions on the database  
 The following example returns a list of the effective permissions of the caller on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE AdventureWorks2012;  
SELECT * FROM fn_my_permissions (NULL, 'DATABASE');  
GO  
```  
  
### C. Listing effective permissions on a view  
 The following example returns a list of the effective permissions of the caller on the `vIndividualCustomer` view in the `Sales` schema of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE AdventureWorks2012;  
SELECT * FROM fn_my_permissions('Sales.vIndividualCustomer', 'OBJECT')   
    ORDER BY subentity_name, permission_name ;   
GO   
```  
  
### D. Listing effective permissions of another user  
 The following example returns a list of the effective permissions of database user `Wanida` on the `Employee` table in the `HumanResources` schema of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The caller requires IMPERSONATE permission on user `Wanida`.  
  
```  
EXECUTE AS USER = 'Wanida';  
SELECT * FROM fn_my_permissions('HumanResources.Employee', 'OBJECT')   
    ORDER BY subentity_name, permission_name ;    
REVERT;  
GO  
```  
  
### E. Listing effective permissions on a certificate  
 The following example returns a list of the effective permissions of the caller on a certificate named `Shipping47` in the current database.  
  
```  
SELECT * FROM fn_my_permissions('Shipping47', 'CERTIFICATE');  
GO  
```  
  
### F. Listing effective permissions on an XML Schema Collection  
 The following example returns a list of the effective permissions of the caller on an XML Schema Collection named `ProductDescriptionSchemaCollection` in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE AdventureWorks2012;  
SELECT * FROM fn_my_permissions('ProductDescriptionSchemaCollection',  
    'XML SCHEMA COLLECTION');  
GO  
```  
  
### G. Listing effective permissions on a database user  
 The following example returns a list of the effective permissions of the caller on a user named `MalikAr` in the current database.  
  
```  
SELECT * FROM fn_my_permissions('MalikAr', 'USER');  
GO  
```  
  
### H. Listing effective permissions of another login  
 The following example returns a list of the effective permissions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof` on the `Employee` table in the `HumanResources` schema of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The caller requires IMPERSONATE permission on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof`.  
  
```  
EXECUTE AS LOGIN = 'WanidaBenshoof';  
SELECT * FROM fn_my_permissions('AdventureWorks2012.HumanResources.Employee', 'OBJECT')   
    ORDER BY subentity_name, permission_name ;    
REVERT;  
GO  
```  
  
## See Also  
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Securables](../../relational-databases/security/securables.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [EXECUTE AS &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-transact-sql.md)  
  
  
