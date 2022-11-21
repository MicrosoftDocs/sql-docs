---
title: "DROP ROLE (Transact-SQL)"
description: DROP ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP ROLE"
  - "DROP_ROLE_TSQL"
helpviewer_keywords:
  - "deleting roles"
  - "database roles [SQL Server], removing"
  - "removing roles"
  - "DROP ROLE statement"
  - "roles [SQL Server], removing"
  - "dropping roles"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP ROLE (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Removes a role from the database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
-- Syntax for SQL Server  
  
DROP ROLE [ IF EXISTS ] role_name  
```  
  

```syntaxsql  
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  

DROP ROLE role_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the role only if it already exists.  
  
 *role_name*  
 Specifies the role to be dropped from the database.  
  
## Remarks  
 Roles that own securables cannot be dropped from the database. To drop a database role that owns securables, you must first transfer ownership of those securables or drop them from the database. Roles that have members cannot be dropped from the database. To drop a role that has members, you must first remove members of the role.  
  
 To remove members from a database role, use [ALTER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-role-transact-sql.md).  
  
 You cannot use DROP ROLE to drop a fixed database role.  
  
 Information about role membership can be viewed in the sys.database_role_members catalog view.  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
 To remove a server role, use [DROP SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-role-transact-sql.md).  
  
## Permissions  
 Requires **ALTER ANY ROLE** permission on the database, or **CONTROL** permission on the role, or membership in the **db_securityadmin**.  
  
## Examples  
 The following example drops the database role `purchasing` from the `AdventureWorks2012` database.  
  
```sql  
DROP ROLE purchasing;  
GO  
```  
  
  
## See Also  
 [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md)   
 [ALTER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-role-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
