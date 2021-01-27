---
description: "sp_query_store_clear_hints (Transact-SQL)"
title: "sp_query_store_clear_hints (Transact-SQL)"
ms.custom: ""
ms.date: "01/26/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_query_store_clear_hints_TSQL"
  - "sys.sp_query_store_clear_hints_TSQL"
  - "sp_query_store_clear_hints"
  - "sys.sp_query_store_clear_hints"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_query_store_clear_hints"
  - "sp_query_store_clear_hints"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current"
---
# sp_query_store_clear_hints (Transact-SQL)
[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

  Removes [Query Store Hints](../performance/query-store-hints.md).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql
sp_query_store_clear_hints
    @query_id bigint;
```  

## Return Values  
 0 (success) or 1 (failure)  
  
## Remarks  

### Supported Query Hints
  
## Permissions  
 Requires the **ALTER** permission on the database.
  
## Examples  

### Clear query hint text

 The following example removes the query store hint text:
  
```sql
EXEC sys.sp_query_store_clear_hints @query_id = 39;
```  

### View Query Store Hints

The following example returns existing Query Store Hints:

```sql
SELECT query_hint_id, query_id, query_hint_text, last_query_hint_failure_reason, last_query_hint_failure_reason_desc, query_hint_failure_count, source, source_desc, comment 
FROM sys.query_store_query_hints 
WHERE query_id = 39;
```

## See Also  
- [sys.sp_query_store_set_hints (Transact-SQL)](sys-sp-query-store-set-hints-transact-sql.md)   
- [sys.query_store_query_hints (Transact-SQL)](../system-catalog-views/sys-query-store-query-hints-transact-sql.md)   
- [Query Store Hints](../performance/query-store-hints.md).
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
