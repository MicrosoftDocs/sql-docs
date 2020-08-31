---
description: "SET RESULT_SET_CACHING (Transact-SQL)"
title: "SET RESULT_SET_CACHING (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 04/16/2020
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
dev_langs:
  - "TSQL"
helpviewer_keywords: 
author: XiaoyuMSFT
ms.author: xiaoyul
monikerRange: "=azure-sqldw-latest || = sqlallproducts-allversions"
---
# SET RESULT SET CACHING (Transact-SQL) 

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Controls the result set caching behavior for the current client session.  

Applies to Azure SQL Data Warehouse  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```syntaxsql
SET RESULT_SET_CACHING { ON | OFF };
```  
  
## Remarks  

Run this command when connected to the user database where you want to configure the result_set_caching setting for.

**ON**   
Enables result set caching for the current client session.  Result set caching cannot be turned ON for a session if it is turned OFF at the database level.

**OFF**   
Disable result set caching for the current client session.

## Examples

Query the result_cache_hit column in [sys.dm_pdw_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql) with a query’s request_id to see if this query was executed with a result cache hit or miss.

```sql
SELECT result_cache_hit
FROM sys.dm_pdw_exec_requests
WHERE request_id = 'QID58286'
```

## Permissions

Requires membership in the public role

## See also

- [Performance tuning with result set caching](/azure/sql-data-warehouse/performance-tuning-result-set-caching)
- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options?view=azure-sqldw-latest)
- [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql?view=azure-sqldw-latest)
- [DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)](/sql/t-sql/database-console-commands/dbcc-showresultcachespaceused-transact-sql)
- [DBCC DROPRESULTSETCACHE (Transact-SQL)](/sql/t-sql/database-console-commands/dbcc-dropresultsetcache-transact-sql)
