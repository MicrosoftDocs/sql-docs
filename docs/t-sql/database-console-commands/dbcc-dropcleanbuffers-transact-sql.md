---
title: "DBCC DROPCLEANBUFFERS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROPCLEANBUFFERS"
  - "DBCC_DROPCLEANBUFFERS_TSQL"
  - "DROPCLEANBUFFERS_TSQL"
  - "DBCC DROPCLEANBUFFERS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "clean buffers"
  - "cold buffer cache"
  - "buffer pools [SQL Server]"
  - "dropping buffers"
  - "removing buffers"
  - "DBCC DROPCLEANBUFFERS statement"
ms.assetid: a4121927-f2ce-4926-aa2c-9b1519dac048
author: pmasl
ms.author: umajay
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DBCC DROPCLEANBUFFERS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

Removes all clean buffers from the buffer pool, and columnstore objects from the columnstore object pool.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax
Syntax for SQL Server: 

```sql
DBCC DROPCLEANBUFFERS [ WITH NO_INFOMSGS ]  
```  
Syntax for Azure SQL Warehouse and Parallel Data Warehouse:

```sql  
DBCC DROPCLEANBUFFERS ( COMPUTE | ALL ) [ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
 WITH NO_INFOMSGS  
 Suppresses all informational messages. Informational messages are always suppressed on [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
 COMPUTE  
 Purge the data cache in memory from each Compute node.  
  
 ALL  
 Purge the data cache in memory from each Compute node and from the Control node. This is the default if you do not specify a value.  
  
## Remarks  
Use DBCC DROPCLEANBUFFERS to test queries with a cold buffer cache without shutting down and restarting the server.
To drop clean buffers from the buffer pool and columnstore objects from the columnstore object pool, first use CHECKPOINT to produce a cold buffer cache. This forces all dirty pages for the current database to be written to disk and cleans the buffers. After you do this, you can issue DBCC DROPCLEANBUFFERS command to remove all buffers from the buffer pool.
  
## Result Sets  
DBCC DROPCLEANBUFFERS on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns:
  
```sql
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  

Applies to: SQL Server, Parallel Data Warehouse 

- Requires membership in the **sysadmin** fixed server role.  

Applies to: Azure SQL Data Warehouse

- Requires membership in the DB_OWNER fixed server role.  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[CHECKPOINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/checkpoint-transact-sql.md)  
  
  
