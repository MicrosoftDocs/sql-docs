---
title: "SET LOCK_TIMEOUT (Transact-SQL)"
description: SET LOCK_TIMEOUT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "09/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LOCK_TIMEOUT_TSQL"
  - "SET_LOCK_TIMEOUT_TSQL"
  - "SET LOCK_TIMEOUT"
  - "LOCK_TIMEOUT"
helpviewer_keywords:
  - "timeout options [SQL Server], locks"
  - "releasing locks"
  - "LOCK_TIMEOUT option"
  - "SET LOCK_TIMEOUT statement"
  - "locking [SQL Server], time-outs"
  - "wait time for lock releases [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET LOCK_TIMEOUT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Specifies the number of milliseconds a statement waits for a lock to be released.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SET LOCK_TIMEOUT timeout_period  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *timeout_period*  
 Is the number of milliseconds that will pass before [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns a locking error. A value of -1 (default) indicates no time-out period (that is, wait forever).  
  
 When a wait for a lock exceeds the time-out value, an error is returned. A value of 0 means to not wait at all and return a message as soon as a lock is encountered.  
  
## Remarks  
 At the beginning of a connection, this setting has a value of -1. After it is changed, the new setting stays in effect for the remainder of the connection.  
  
 The setting of SET LOCK_TIMEOUT is set at execute or run time and not at parse time.  
  
 The READPAST locking hint provides an alternative to this SET option.  
  
 CREATE DATABASE, ALTER DATABASE, and DROP DATABASE statements do not honor the SET LOCK_TIMEOUT setting.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
  
### A: Set the lock timeout to 1800 milliseconds  
 The following example sets the lock time-out period to `1800` milliseconds.  
  
```sql  
SET LOCK_TIMEOUT 1800;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Set the lock timeout to wait forever for a lock to be released.  
 The following example sets the lock timeout to wait forever and never expire. This is the default behavior that is already set at the beginning of each connection.  
  
```sql  
SET LOCK_TIMEOUT -1;  
```  
  
 The following example sets the lock time-out period to `1800` milliseconds. In this release, [!INCLUDE[ssDW](../../includes/ssdw-md.md)] will parse the statement successfully, but will ignore the value 1800 and continue to use the default behavior.  
  
```sql  
SET LOCK_TIMEOUT 1800;  
```  
  
## See Also  
 [@@LOCK_TIMEOUT &#40;Transact-SQL&#41;](../../t-sql/functions/lock-timeout-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  

