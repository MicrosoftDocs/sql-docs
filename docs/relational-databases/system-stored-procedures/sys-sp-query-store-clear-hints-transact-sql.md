---
description: "The sp_query_store_clear_hints system stored procedure removes all Query Store hints for a given query."
title: "sp_query_store_clear_hints (Transact-SQL)"
ms.custom:
- event-tier1-build-2022
ms.date: "05/24/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
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
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-ver16||>=sql-server-linux-ver16"
---
# sp_query_store_clear_hints (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

  Removes all [Query Store hints](../performance/query-store-hints.md) for a given query_id.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sp_query_store_clear_hints
    @query_id bigint;
```  

## Return Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 Query Store hints are created by [sys.sp_query_store_set_hints (Transact-SQL)](sys-sp-query-store-set-hints-transact-sql.md).

## Permissions  
 Requires the **ALTER** permission on the database.
  
## Examples  

### Clear query hint text

 The following example removes the query store hint text:
  
```sql
EXEC sys.sp_query_store_clear_hints @query_id = 39;
```  

### View Query Store hints

The following example returns existing Query Store hints:

```sql
SELECT query_hint_id, query_id, query_hint_text, last_query_hint_failure_reason, last_query_hint_failure_reason_desc, query_hint_failure_count, source, source_desc 
FROM sys.query_store_query_hints 
WHERE query_id = 39;
```

## Next steps
- [sys.sp_query_store_set_hints (Transact-SQL)](sys-sp-query-store-set-hints-transact-sql.md)   
- [sys.query_store_query_hints (Transact-SQL)](../system-catalog-views/sys-query-store-query-hints-transact-sql.md)   
- [Query Store hints](../performance/query-store-hints.md).
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
