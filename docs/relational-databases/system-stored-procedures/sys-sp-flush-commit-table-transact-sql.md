---
description: "sys.sp_flush_commit_table (Transact-SQL)"
title: "sys.sp_flush_commit_table (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_flush_commit_table"
  - "sp_flush_commit_table_TSQL"
  - "sys.sp_flush_commit_table"
  - "sys.sp_flush_commit_table_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_flush_commit_tables"
  - "sp_flush_commit_tables"
ms.assetid: 
author: JetterMcTedder
ms.author: bspendolini
---
# sys.sp_flush_commit_table (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Flushes the in memory syscommittab to disk to help with Change Tracking cleanup.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  

```sql
sys.sp_flush_commit_table  [@flush_ts = ] flush_ts 
[ , [@cleanup_version = ] cleanup_version ]
```

## Arguments

'[@flush_ts = ] flush_ts' is the current change tracking version. flush_ts is bigint and cannot be NULL.

'[@cleanup_version = ] cleanup_version' is the watermark change tracking version for syscommittab cleanup. cleanup_version is bigint, with a default of NULL.

## Return Code Values

 **0** (success) or **1** (failure)

## Example

```sql
EXEC sys.sp_flush_commit_table 11;
GO

Started executing query at Line 1
(10 rows affected)
Total execution time: 00:00:00.076
```
  
## Remarks

This procedure must be run in a database that has change tracking enabled.

## Permissions

 Only a member of the sysadmin server role or db_owner database role can execute this procedure.  
  
## See Also  

 [About Change Tracking &#40;Transact-SQL&#41;](../../relational-databases/track-changes/about-change-tracking-sql-server.md)  
 [Change Tracking Cleanup and Troubleshooting &#40;Transact-SQL&#41;](../../relational-databases/track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)  
 [Change Tracking System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/change-tracking-tables-transact-sql.md)  
 [Change Tracking Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)  
