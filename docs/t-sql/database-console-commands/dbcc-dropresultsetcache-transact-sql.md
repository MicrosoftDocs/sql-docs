---
title: DBCC DROPRESULTSETCACHE (Transact-SQL)
description: "DBCC DROPRESULTSETCACHE  (Transact-SQL)"
author: mstehrani
ms.author: emtehran
ms.reviewer: wiassaf
ms.date: "07/03/2019"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "language-reference"
dev_langs:
  - "TSQL"
monikerRange: "= azure-sqldw-latest"
---

# DBCC DROPRESULTSETCACHE  (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Removes all result set cache entries from an Azure [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC DROPRESULTSETCACHE
[;]  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Permissions

Requires membership in the DB_OWNER fixed server role.

## Remarks

- This command empties the result set cache for all queries.  

- Turning OFF the result set cache feature for a database also deletes all cached results.  

- Pausing a database enabled with result set caching won't delete the cached results.  

## See also

- [Performance tuning with result set caching](/azure/sql-data-warehouse/performance-tuning-result-set-caching)</br>
- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../statements/alter-database-transact-sql-set-options.md?view=azure-sqldw-latest&preserve-view=true)</br>
- [ALTER DATABASE &#40;Transact-SQL&#41;](../statements/alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)</br>
- [SET RESULT SET CACHING &#40;Transact-SQL&#41;](../statements/set-result-set-caching-transact-sql.md)</br>
- [DBCC SHOWRESULTCACHESPACEUSED &#40;Transact-SQL&#41;](./dbcc-showresultcachespaceused-transact-sql.md)