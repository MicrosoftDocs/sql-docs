---
title: "DBCC FREESESSIONCACHE (Transact-SQL)"
description: "DBCC FREESESSIONCACHE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "FREESESSIONCACHE"
  - "FREESESSIONCACHE_TSQL"
  - "DBCC_FREESESSIONCACHE_TSQL"
  - "DBCC FREESESSIONCACHE"
helpviewer_keywords:
  - "DBCC FREESESSIONCACHE statement"
  - "distributed queries [SQL Server], cache"
  - "clearing distributed query cache"
  - "flushing distributed query cache"
dev_langs:
  - "TSQL"
---
# DBCC FREESESSIONCACHE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Flushes the distributed query connection cache used by distributed queries against an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
```syntaxsql
DBCC FREESESSIONCACHE [ WITH NO_INFOMSGS ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 WITH NO_INFOMSGS  
 Suppresses all informational messages.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
The following example flushes the distributed query cache.
  
```sql
USE AdventureWorks2012;  
GO  
DBCC FREESESSIONCACHE WITH NO_INFOMSGS;  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
  
