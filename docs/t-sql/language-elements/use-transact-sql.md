---
title: "USE (Transact-SQL)"
description: "USE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "11/28/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "USE_TSQL"
  - "USE"
helpviewer_keywords:
  - "USE statement"
  - "database context [SQL Server]"
  - "context changes [SQL Server]"
  - "modifying database context"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# USE (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Changes the database context to the specified database or database snapshot in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
USE { database_name }   
[;]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name*  
 Is the name of the database or database snapshot to which the user context is switched. Database and database snapshot names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the database parameter can only refer to the current database. If a database other than the current database is provided, the `USE` statement does not switch between databases, and error code 40508 is returned. To change databases, you must directly connect to the database. The USE statement is marked as not applicable to SQL Database at the top of this page, because even though you can have the `USE` statement in a batch, it doesn't do anything.
  
## Remarks  
 When a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the login is automatically connected to its default database and acquires the security context of a database user. If no database user has been created for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, the login connects as guest. If the database user does not have CONNECT permission on the database, the USE statement will fail. If no default database has been assigned to the login, its default database will be set to master.  
  
 USE is executed at both compile and execution time and takes effect immediately. Therefore, statements that appear in a batch after the USE statement are executed in the specified database.  
  
## Permissions  
 Requires CONNECT permission on the target database.  
  
## Examples  
 The following example changes the database context to the `AdventureWorks2012` database.  
  
```sql  
USE AdventureWorks2012;  
GO  
```  
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../statements/create-database-transact-sql.md)   
 [DROP DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-transact-sql.md)   
 [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)  
  
