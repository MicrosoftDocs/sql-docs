---
title: "sys.fn_translate_permissions (Transact-SQL)"
description: "sys.fn_translate_permissions (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_translate_permissions"
  - "sys.fn_translate_permissions_TSQL"
  - "fn_translate_permissions"
  - "fn_translate_permissions_TSQL"
helpviewer_keywords:
  - "permissions bitmask [SQL Server]"
  - "sys.fn_translate_permissions function"
  - "fn_translate_permissions function"
dev_langs:
  - "TSQL"
---
# sys.fn_translate_permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Translates the permissions bitmask returned by SQL Trace into a table of permissions names.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_translate_permissions ( level , perms )  
```  
  
## Arguments  
 *level*  
 Is the kind of securable to which the permission is applied. *level* is **nvarchar(60)**.  
  
 *perms*  
 Is a bitmask that is returned in the permissions column. *perms* is **varbinary(16)**.  
  
## Returns  
 **table**  
  
## Remarks  
 The value returned in the **permissions** column of a SQL Trace is an integer representation of a bitmask used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to calculate effective permissions. Each of the 25 kinds of securables has its own set of permissions with corresponding numerical values. **sys.fn_translate_permissions** translates this bitmask into a table of permissions names.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Example  
 The following query uses `sys.fn_builtin_permissions` to display the permissions that apply to certificates, and then uses `sys.fn_translate_permissions` to return the results of the permissions bitmask.  
  
```  
SELECT * FROM sys.fn_builtin_permissions('CERTIFICATE');  
SELECT '0001' AS Input, * FROM sys.fn_translate_permissions('CERTIFICATE', 0001);  
SELECT '0010' AS Input, * FROM sys.fn_translate_permissions('CERTIFICATE', 0010);  
SELECT '0011' AS Input, * FROM sys.fn_translate_permissions('CERTIFICATE', 0011);  
```  
  
## See Also  
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [sys.server_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)   
 [sys.database_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)  
  
  
