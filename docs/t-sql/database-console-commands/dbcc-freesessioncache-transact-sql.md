---
title: "DBCC FREESESSIONCACHE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FREESESSIONCACHE"
  - "FREESESSIONCACHE_TSQL"
  - "DBCC_FREESESSIONCACHE_TSQL"
  - "DBCC FREESESSIONCACHE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DBCC FREESESSIONCACHE statement"
  - "distributed queries [SQL Server], cache"
  - "clearing distributed query cache"
  - "flushing distributed query cache"
ms.assetid: a256ba63-7e11-4d5e-abc0-1fa4ed072e63
author: pmasl
ms.author: umajay
manager: craigg
---
# DBCC FREESESSIONCACHE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

Flushes the distributed query connection cache used by distributed queries against an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
```sql
DBCC FREESESSIONCACHE [ WITH NO_INFOMSGS ]  
```  
  
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
  
  
