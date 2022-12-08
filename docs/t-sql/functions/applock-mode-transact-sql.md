---
title: "APPLOCK_MODE (Transact-SQL)"
description: "APPLOCK_MODE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "APPLOCK_MODE_TSQL"
  - "APPLOCK_MODE"
helpviewer_keywords:
  - "locking [SQL Server], applications"
  - "applications [SQL Server], locks"
  - "sessions [SQL Server], application locks"
  - "APPLOCK_MODE function"
dev_langs:
  - "TSQL"
---
# APPLOCK_MODE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function returns the lock mode held by the lock owner on a particular application resource. As an application lock function, APPLOCK_MODE operates on the current database. The database is the scope of the application locks.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
APPLOCK_MODE( 'database_principal' , 'resource_name' , 'lock_owner' )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
'*database_principal*'  
The user, role, or application role that can be granted permissions to objects in the database. To successfully call the function, the function caller must be a member of *database_principal*, dbo, or the db_owner fixed database role.
  
'*resource_name*'  
A lock resource name specified by the client application. The application must ensure a unique resource name. The specified name is hashed internally into a value that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] lock manager can internally store. *resource_name*is **nvarchar(255)**, with no default. *resource_name* is binary compared, and is case-sensitive regardless of the collation settings of the current database.
  
'*lock_owner*'  
The owner of the lock, which is the *lock_owner* value when the lock was requested. *lock_owner* is **nvarchar(32)**, and the value can be either **Transaction** (the default) or **Session**.
  
## Return types
**nvarchar(32)**
  
## Return value
Returns the lock mode held by the lock owner on a particular application resource. Lock mode can have any one of these values:

:::row:::
   :::column span="":::
      **NoLock**<br>      **Update**<br>      **\*SharedIntentExclusive**
   :::column-end:::
   :::column span="":::
      **IntentShared**<br>      **IntentExclusive**<br>      **\*UpdateIntentExclusive**

   :::column-end:::
   :::column span="":::
      **Shared**<br>      **Exclusive**
   :::column-end:::
:::row-end:::
  
*This lock mode is a combination of other lock modes and sp_getapplock cannot explicitly acquire it.
  
## Function properties
**Nondeterministic**
  
**Nonindexable**
  
**Nonparallelizable**
  
## Examples  
Two users (User A and User B), with separate sessions, run the following sequence of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.
  
User A runs:
  
```sql
USE AdventureWorks2012;  
GO  
BEGIN TRAN;  
DECLARE @result INT;  
EXEC @result=sp_getapplock  
    @DbPrincipal='public',  
    @Resource='Form1',  
    @LockMode='Shared',  
    @LockOwner='Transaction';  
SELECT APPLOCK_MODE('public', 'Form1', 'Transaction');  
GO  
```  
  
User B then runs:
  
```sql
Use AdventureWorks2012;  
GO  
BEGIN TRAN;  
SELECT APPLOCK_MODE('public', 'Form1', 'Transaction');  
--Result set: NoLock  
  
SELECT APPLOCK_TEST('public', 'Form1', 'Shared', 'Transaction');  
--Result set: 1 (Lock is grantable.)  
  
SELECT APPLOCK_TEST('public', 'Form1', 'Exclusive', 'Transaction');  
--Result set: 0 (Lock is not grantable.)  
GO  
```  
  
User A then runs:
  
```sql
EXEC sp_releaseapplock @Resource='Form1', @DbPrincipal='public';  
GO  
```  
  
User B then runs:
  
```sql
SELECT APPLOCK_TEST('public', 'Form1', 'Exclusive', 'Transaction');  
--Result set: '1' (The lock is grantable.)  
GO  
```  
  
User A and User B then run:
  
```sql
COMMIT TRAN;  
GO  
```  
  
## See also
[APPLOCK_TEST &#40;Transact-SQL&#41;](../../t-sql/functions/applock-test-transact-sql.md)  
[sp_getapplock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-getapplock-transact-sql.md)  
[sp_releaseapplock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-releaseapplock-transact-sql.md)
  
  
