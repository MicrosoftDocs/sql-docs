---
title: "DENY System Object Permissions (Transact-SQL)"
description: DENY System Object Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "DENY statement, system objects"
  - "encryption [SQL Server], system objects"
  - "system objects [SQL Server]"
  - "cryptography [SQL Server], system objects"
dev_langs:
  - "TSQL"
---
# DENY System Object Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Denies permissions on system objects such as stored procedures, extended stored procedures, functions, and views.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DENY { SELECT | EXECUTE } ON [ sys.]system_object TO principal   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 [ **sys.**]  
 The **sys** qualifier is required only when you are referring to catalog views and dynamic management views.  
  
 *system_object*  
 Specifies the object on which permission is being denied.  
  
 *principal*  
 Specifies the principal from which the permission is being revoked.  
  
## Remarks  
 This statement can be used to deny permissions on certain stored procedures, extended stored procedures, table-valued functions, scalar functions, views, catalog views, compatibility views, INFORMATION_SCHEMA views, dynamic management views, and system tables that are installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each of these system objects exists as a unique record in the resource database (**mssqlsystemresource**). The resource database is read-only. A link to the object is exposed as a record in the **sys** schema of every database.  
  
 Default name resolution resolves unqualified procedure names to the resource database. Therefore, the **sys** qualifier is only required when you are specifying catalog views and dynamic management views.  
  
> [!CAUTION]  
>  Denying permissions on system objects will cause applications that depend on them to fail. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses catalog views and may not function as expected if you change the default permissions on catalog views.  
  
 Denying permissions on triggers and on columns of system objects is not supported.  
  
 Permissions on system objects will be preserved during upgrades of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 System objects are visible in the [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md) catalog view. The permissions on system objects are visible in the [sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md) catalog view in the **master** database.  
  
 The following query returns information about permissions of system objects:  
  
```sql
SELECT * FROM master.sys.database_permissions AS dp   
    JOIN sys.system_objects AS so  
    ON dp.major_id = so.object_id  
    WHERE dp.class = 1 AND so.parent_object_id = 0 ;  
GO  
```  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples  
 The following example denies `EXECUTE` permission on `xp_cmdshell` to `public`.  
  
```sql
DENY EXECUTE ON sys.xp_cmdshell TO public;  
GO  
```  
  
## See Also  
 [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)   
 [sys.database_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)   
 [GRANT System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-system-object-permissions-transact-sql.md)   
 [REVOKE System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-system-object-permissions-transact-sql.md)  
  
  
