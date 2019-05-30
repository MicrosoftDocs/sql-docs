---
title: "GRANT System Object Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "encryption [SQL Server], system objects"
  - "system objects [SQL Server]"
  - "GRANT statement, system objects"
ms.assetid: 9d4e89f4-478f-419a-8b50-b096771e3880
author: VanMSFT
ms.author: vanto
manager: craigg
---
# GRANT System Object Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Grants permissions on system objects such as system stored procedures, extended stored procedures, functions, and views.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
GRANT { SELECT | EXECUTE } ON [ sys.]system_object TO principal   
```  
  
## Arguments  
 [ sys.] .  
 The sys qualifier is required only when you are referring to catalog views and dynamic management views.  
  
 *system_object*  
 Specifies the object on which permission is being granted.  
  
 *principal*  
 Specifies the principal to which the permission is being granted.  
  
## Remarks  
 This statement can be used to grant permissions on certain stored procedures, extended stored procedures, table-valued functions, scalar functions, views, catalog views, compatibility views, INFORMATION_SCHEMA views, dynamic management views, and system tables that are installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each of these system objects exists as a unique record in the resource database of the server (mssqlsystemresource). The resource database is read-only. A link to the object is exposed as a record in the sys schema of every database. Permission to execute or select a system object can be granted, denied, and revoked.  
  
 Granting permission to execute or select an object does not necessarily convey all the permissions required to use the object. Most objects perform operations for which additional permissions are required. For example, a user that is granted EXECUTE permission on sp_addlinkedserver cannot create a linked server unless the user is also a member of the sysadmin fixed server role.  
  
 Default name resolution resolves unqualified procedure names to the resource database. Therefore, the sys qualifier is only required when you are specifying catalog views and dynamic management views.  
  
 Granting permissions on triggers and on columns of system objects is not supported.  
  
 Permissions on system objects will be preserved during upgrades of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 System objects are visible in the [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md) catalog view. The permissions on system objects are visible in the [sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md) catalog view in the master database.  
  
 The following query returns information about permissions of system objects:  
  
```  
SELECT * FROM master.sys.database_permissions AS dp   
    JOIN sys.system_objects AS so  
    ON dp.major_id = so.object_id  
    WHERE dp.class = 1 AND so.parent_object_id = 0 ;  
GO  
```  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples  
  
### A. Granting SELECT permission on a view  
 The following example grants the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `Sylvester1` permission to select a view that lists [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. The example then grants the additional permission that is required to view metadata on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins that are not owned by the user.  
  
```  
USE AdventureWorks2012;  
GRANT SELECT ON sys.sql_logins TO Sylvester1;  
GRANT VIEW SERVER STATE to Sylvester1;  
GO  
```  
  
### B. Granting EXECUTE permission on an extended stored procedure  
 The following example grants `EXECUTE` permission on `xp_readmail` to `Sylvester1`.  
  
```  
GRANT EXECUTE ON xp_readmail TO Sylvester1;  
GO  
```  
  
## See Also  
 [sys.system_objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md)   
 [sys.database_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)   
 [REVOKE System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-system-object-permissions-transact-sql.md)   
 [DENY System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-system-object-permissions-transact-sql.md)  
  
  
