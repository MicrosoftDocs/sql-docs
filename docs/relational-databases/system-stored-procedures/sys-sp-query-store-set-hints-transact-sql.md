---
description: "The sys.sp_query_store_set_hints system stored procedure creates or updates Query Store hints for a given query, allowing you to influence queries without changing application code or database objects."
title: "sys.sp_query_store_set_hints (Transact-SQL)"
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_query_store_set_hints_TSQL"
  - "sys.sp_query_store_set_hints_TSQL"
  - "sp_query_store_set_hints"
  - "sys.sp_query_store_set_hints"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_query_store_set_hints"
  - "sp_query_store_set_hints"
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-ver16||>=sql-server-linux-ver16"
---
# sp_query_store_set_hints (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

 Creates or updates [Query Store hints](../performance/query-store-hints.md) for a given `query_id`.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sp_query_store_set_hints
    @query_id = 'query_id',
    @query_hints = 'query_hints'
    [, @query_hints_comment = 'query_hints_comment' ]
    [, @override_plan_guide_and_hints = 'override_plan_guide_and_hints' ]
    [, @fail_execution_on_error = 'fail_execution_on_error' ]
    [, @query_hint_scope = 'query_hint_scope' ] [;]  
```  

## Arguments

#### @query_id

Bigint. Required. The Query Store `query_id` from [sys.query_store_query](../system-catalog-views/sys-query-store-query-transact-sql.md).

#### @query_hints

Nvarchar(max). String of query options beginning with `'OPTION`. For more information, see [Supported query hints](#supported-query-hints) in this article.

#### [ @query_hints_comment ]

Nvarchar(max). User-editable comment on query hint. Use to provide context, application, event, or other information useful to yourself and other administrators in the future.

#### [ @override_plan_guide_and_hints ]

Bit. Defaults to `1`. Controls whether the new Query Store hint will override an existing plan guide or an in-place hint in the code.

#### [ @fail_execution_on_error ] 

Bit. Default is `0`. Controls whether errors in applying a query hint will be surfaced to the client. The default for this argument is `0` which matches the existing Query Store plan forcing overall behavior where failing operations are silently logged and do not block query execution.

#### [ @query_hint_scope ] 

Tinyint. By default, the scope of a new Query Store hint is the local replica only. Determines the scope at which the hint will be applied, as per the `replica_group_id` column in [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md).

## Return Values  
 0 (success) or 1 (failure)  
  
## Remarks  

Hints are specified in a valid T-SQL string format `N'OPTION (..)'`.

* If no Query Store hint exists for a specific `query_id`, a new Query Store hint will be created. 
* If a Query Store hint already exists for a specific `query_id`, the last value provided will override previously specified values for the associated query. 
* If a `query_id` doesn't exist, an error will be raised. 

In the cases where a hint would cause a query to fail, the hint is ignored and the latest failure details can be viewed in [sys.query_store_query_hints](../system-catalog-views/sys-query-store-query-hints-transact-sql.md).

To remove hints associated with a `query_id`, use the system stored procedure [sys.sp_query_store_clear_hints](sys-sp-query-store-clear-hints-transact-sql.md). 

### Supported query hints 

These [query hints](../../t-sql/queries/hints-transact-sql-query.md) are supported as Query Store hints:

```syntaxsql
{ HASH | ORDER } GROUP   
  | { CONCAT | HASH | MERGE } UNION   
  | { LOOP | MERGE | HASH } JOIN   
  | EXPAND VIEWS   
  | FAST number_rows   
  | FORCE ORDER   
  | IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX  
  | KEEP PLAN   
  | KEEPFIXED PLAN  
  | MAX_GRANT_PERCENT = percent  
  | MIN_GRANT_PERCENT = percent  
  | MAXDOP number_of_processors   
  | NO_PERFORMANCE_SPOOL   
  | OPTIMIZE FOR UNKNOWN  
  | PARAMETERIZATION { SIMPLE | FORCED }   
  | RECOMPILE  
  | ROBUST PLAN   
  | USE HINT   ( '<hint_name>' [ , ...n ] )
```

The following query hints are currently unsupported:
*    OPTIMIZE FOR(@var = val)
*    MAXRECURSION
*    USE PLAN (instead, consider Query Store's original plan forcing capability, [sp_query_store_force_plan](sp-query-store-force-plan-transact-sql.md)).
*    DISABLE_DEFERRED_COMPILATION_TV
*    DISABLE_TSQL_SCALAR_UDF_INLINING
*    [Table hints (for example, FORCESEEK, READUNCOMMITTED, INDEX)](../../t-sql/queries/hints-transact-sql-table.md)
  
## Permissions  
 Requires the **ALTER** permission on the database.
  
## Examples  

### Identify a query in Query Store

The following example queries [sys.query_store_query_text](../system-catalog-views/sys-query-store-query-text-transact-sql.md) and [sys.query_store_query](../system-catalog-views/sys-query-store-query-transact-sql.md) to return the `query_id` for an executed query text fragment. 

In this example, the query we're attempting to tune is in the `SalesLT` sample database: 

```sql
SELECT * FROM SalesLT.Address as A 
INNER JOIN SalesLT.CustomerAddress as CA
on A.AddressID = CA.AddressID
WHERE PostalCode = '98052' ORDER BY A.ModifiedDate DESC;
```

Note that Query Store doesn't immediately reflect query data to its system views.

Identify the query in the query store system catalog views:

```sql
SELECT q.query_id, qt.query_sql_text
FROM sys.query_store_query_text qt 
INNER JOIN sys.query_store_query q ON 
    qt.query_text_id = q.query_text_id 
WHERE query_sql_text like N'%PostalCode =%'  
  AND query_sql_text not like N'%query_store%';
GO
```

In the following samples, the previous query example in the `SalesLT` database was identified as `query_id` 39.

### Apply single hint

 The following example applies the RECOMPILE hint to `query_id` 39, as identified in Query Store:
  
```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(RECOMPILE)';
```  

 The following example applies the hint to force the [legacy cardinality estimator](../performance/cardinality-estimation-sql-server.md) to `query_id` 39, identified in Query Store:
  
```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(USE HINT(''FORCE_LEGACY_CARDINALITY_ESTIMATION''))';
```  

### Apply multiple hints

The following example applies multiple query hints to `query_id` 39, including RECOMPILE, MAXDOP 1, and the SQL 2012 query optimizer behavior:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(RECOMPILE, MAXDOP 1, USE HINT(''QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_110''))';
```

### View Query Store hints

The following example returns existing Query Store hints:

```sql
SELECT query_hint_id, query_id, query_hint_text, last_query_hint_failure_reason, last_query_hint_failure_reason_desc, query_hint_failure_count, source, source_desc 
FROM sys.query_store_query_hints 
WHERE query_id = 39;
```

### Remove the hint from a query

Use the following example to remove the hint from `query_id` 39, using the [sp_query_store_clear_hints (Transact-SQL)](sys-sp-query-store-clear-hints-transact-sql.md) system stored procedure.  

```sql
EXEC sys.sp_query_store_clear_hints @query_id = 39;
```

## Next steps

- [Query Store hints](../performance/query-store-hints.md)
- [Table Hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-table.md)  
- [sp_query_store_clear_hints (Transact-SQL)](sys-sp-query-store-clear-hints-transact-sql.md)   
- [sys.query_store_query_hints (Transact-SQL)](../system-catalog-views/sys-query-store-query-hints-transact-sql.md)   
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
