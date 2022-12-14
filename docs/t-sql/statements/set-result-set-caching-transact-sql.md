---
title: "SET RESULT_SET_CACHING (Transact-SQL)"
description: SET RESULT_SET_CACHING (Transact-SQL)
author: mstehrani
ms.author: emtehran
ms.reviewer: wiassaf
ms.date: 04/16/2020
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# SET RESULT SET CACHING (Transact-SQL) 

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Controls the result set caching behavior for the current client session.  

Applies to [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```syntaxsql
SET RESULT_SET_CACHING { ON | OFF };
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Remarks  

Run this command when connected to the user database where you want to configure the result_set_caching setting for.

**ON**   
Enables result set caching for the current client session.  Result set caching cannot be turned ON for a session if it is turned OFF at the database level.

**OFF**   
Disable result set caching for the current client session.

> [!NOTE]
> Result set caching should not be used in conjunction with [DECRYPTBYKEY](../functions/decryptbykey-transact-sql.md). If this cryptographic function must be used, ensure you have result set caching disabled (either at [session-level]() or [database-level](./alter-database-transact-sql-set-options.md)) at the time of execution.

## Examples

Query the result_cache_hit column in [sys.dm_pdw_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md) with a query's request_id to see if this query was executed with a result cache hit or miss.

```sql
SELECT result_cache_hit
FROM sys.dm_pdw_exec_requests
WHERE request_id = 'QID58286'
```

## Permissions

Requires membership in the public role

## See also

- [Performance tuning with result set caching](/azure/sql-data-warehouse/performance-tuning-result-set-caching)
- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](./alter-database-transact-sql-set-options.md?preserve-view=true&view=azure-sqldw-latest&preserve-view=true)
- [ALTER DATABASE &#40;Transact-SQL&#41;](./alter-database-transact-sql.md?preserve-view=true&view=azure-sqldw-latest&preserve-view=true)
- [DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)](../database-console-commands/dbcc-showresultcachespaceused-transact-sql.md)
- [DBCC DROPRESULTSETCACHE (Transact-SQL)](../database-console-commands/dbcc-dropresultsetcache-transact-sql.md)