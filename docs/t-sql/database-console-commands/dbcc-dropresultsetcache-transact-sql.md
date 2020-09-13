---
description: "DBCC DROPRESULTSETCACHE  (Transact-SQL)"
title: DBCC DROPRESULTSETCACHE (Transact-SQL)
ms.custom: ""
ms.date: "07/03/2019"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 73f598cf-b02a-4dba-8d89-9fc0b55a12b8
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
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

[!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Permissions

Requires membership in the DB_OWNER fixed server role.

## Remarks

- This command empties the result set cache for all queries.  

- Turning OFF the result set cache feature for a database also deletes all cached results.  

- Pausing a database enabled with result set caching won't delete the cached results.  

## See also

- [Performance tuning with result set caching](/azure/sql-data-warehouse/performance-tuning-result-set-caching)</br>
- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options?view=azure-sqldw-latest)</br>
- [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql?view=azure-sqldw-latest)</br>
- [SET RESULT SET CACHING &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-result-set-caching-transact-sql)</br>
- [DBCC SHOWRESULTCACHESPACEUSED &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-showresultcachespaceused-transact-sql)
