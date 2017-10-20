---
title: "APPLOCK_TEST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "APPLOCK_TEST_TSQL"
  - "APPLOCK_TEST"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "locking [SQL Server], applications"
  - "APPLOCK_TEST function"
  - "applications [SQL Server], locks"
  - "sessions [SQL Server], application locks"
  - "testing application locks"
ms.assetid: 4ea33d04-f8e9-46ff-ae61-985bd3eaca2c
caps.latest.revision: 31
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# APPLOCK_TEST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns information about whether or not a lock can be granted on a particular application resource for a specified lock owner without acquiring the lock. APPLOCK_TEST is an application lock function, and it operates on the current database. The scope of application locks is the database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
APPLOCK_TEST ( 'database_principal' , 'resource_name' , 'lock_mode' , 'lock_owner' )  
```  
  
## Arguments  
**'** *database_principal* **'**  
Is the user, role, or application role that can be granted permissions to objects in the database. The caller of the function must be a member of *database_principal*, **dbo**, or the **db_owner** fixed database role in order to call the function successfully.
  
**'** *resource_name* **'**  
Is a lock resource name specified by the client application. The application must ensure that the resource is unique. The specified name is hashed internally into a value that can be stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] lock manager. *resource_name*is **nvarchar(255)** with no default. *resource_name* is binary compared and is case-sensitive, regardless of the collation settings of the current database.
  
**'** *lock_mode* **'**  
Is the lock mode to be obtained for a particular resource. *lock_mode* is **nvarchar(32)** and has no default value. The value can be any of the following: **Shared**, **Update**, **IntentShared**, **IntentExclusive**, **Exclusive**.
  
**'** *lock_owner* **'**  
Is the owner of the lock, which is the *lock_owner* value when the lock was requested. *lock_owner* is **nvarchar(32)**. The value can be **Transaction** (the default) or **Session**. If default or **Transaction** is explicitly specified, APPLOCK_TEST must be executed from within a transaction.
  
## Return types
**smallint**
  
## Return value
Returns 0 when the lock cannot be granted to the specified owner and returns 1 if the lock can be granted.
  
## Function properties
**Nondeterministic**
  
**Nonindexable**
  
**Nonparallelizable**
  
## Examples  
In the following example, two users (**User A** and **User B**) with separate sessions run the following sequence of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.
  
**User A** runs:
  
```sql
USE AdventureWorks2012;  
GO  
BEGIN TRAN;  
DECLARE @result int;  
EXEC @result=sp_getapplock  
    @DbPrincipal='public',  
    @Resource='Form1',  
    @LockMode='Shared',  
    @LockOwner='Transaction';  
SELECT APPLOCK_MODE('public', 'Form1', 'Transaction');  
GO  
```  
  
**User B** then runs:
  
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
  
**User A** then runs:
  
```sql
EXEC sp_releaseapplock @Resource='Form1', @DbPrincipal='public';  
GO  
```  
  
**User B** then runs:
  
```sql
SELECT APPLOCK_TEST('public', 'Form1', 'Exclusive', 'Transaction');  
--Result set: '1' (The lock is grantable.)  
GO  
```  
  
**User A** and **User B** then both run:
  
```sql
COMMIT TRAN;  
GO  
```  
  
## See also
[APPLOCK_MODE &#40;Transact-SQL&#41;](../../t-sql/functions/applock-mode-transact-sql.md)  
[sp_getapplock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-getapplock-transact-sql.md)  
[sp_releaseapplock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-releaseapplock-transact-sql.md)
  
  
