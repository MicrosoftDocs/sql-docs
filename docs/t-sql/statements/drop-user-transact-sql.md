---
title: "DROP USER (Transact-SQL)"
description: DROP USER (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/12/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_USER_TSQL"
  - "DROP USER"
helpviewer_keywords:
  - "dropping users"
  - "DROP USER statement"
  - "deleting users"
  - "database user removal [SQL Server]"
  - "removing users"
  - "users [SQL Server], removing"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP USER (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Removes a user from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
-- Syntax for SQL Server and Azure SQL Database  
  
DROP USER [ IF EXISTS ] user_name  
```  
  
```syntaxsql  
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
DROP USER user_name  
```  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level), [!INCLUDE[sssds](../../includes/sssds-md.md)]).  
  
 Conditionally drops the user only if it already exists.  
  
 *user_name*  
 Specifies the name by which the user is identified inside this database.  
  
## Remarks  
 Users that own securables cannot be dropped from the database. Before dropping a database user that owns securables, you must first drop or transfer ownership of those securables.  
  
 The guest user cannot be dropped, but guest user can be disabled by revoking its CONNECT permission by executing REVOKE CONNECT FROM GUEST within any database other than master or tempdb.  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 Requires ALTER ANY USER permission on the database.  
  
## Examples  
 The following example removes database user `AbolrousHazem` from the `AdventureWorks2012` database.  
  
```sql  
DROP USER AbolrousHazem;  
GO  
```  
  
## See Also  
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [ALTER USER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-user-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)
