---
title: "sys.sp_query_store_set_hints (Transact-SQL)"
description: "Creates or updates Query Store hints for a given query, allowing you to influence queries without changing application code or database objects."
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "language-reference"
f1_keywords:
  - "sp_query_store_set_hints_TSQL"
  - "sys.sp_query_store_set_hints_TSQL"
  - "sp_query_store_set_hints"
  - "sys.sp_query_store_set_hints"
helpviewer_keywords:
  - "sys.sp_query_store_set_hints"
  - "sp_query_store_set_hints"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-ver16 || >=sql-server-linux-ver16"
---
# sp_query_store_set_hints (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Creates or updates [Query Store hints](../performance/query-store-hints.md) for a given *query_id*.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_set_hints
    [ @query_id = ] query_id ,
    [ @query_hints = ] 'query_hints'
    [ , [ @query_hint_scope = ] 'replica_group_id' ]
[ ; ]
```

## Arguments

#### [ @query_id = ] *query_id*

The Query Store `query_id` column from [sys.query_store_query](../system-catalog-views/sys-query-store-query-transact-sql.md). *@query_id* is **bigint**.

#### [ @query_hints = ] N'*query_hints*'

A character string of query options beginning with `OPTION`. *@query_hints* is **nvarchar(max)**. For more information, see [Supported query hints](#supported-query-hints) in this article.

#### [ @query_hint_scope = ] '*replica_group_id*'

By default, the scope of a new Query Store hint is the local replica only. *@query_hint_scope* is **tinyint**. This optional parameter determines the scope at which the hint will be applied on a secondary replica when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. The optional *query_hint_scope* argument defaults only to the local replica (primary or secondary), but you can optionally specify a *replica_group_id* referencing [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md).

## Return values

`0` (success) or `1` (failure).

## Remarks

Hints are specified in a valid T-SQL string format `N'OPTION (..)'`.

- If no Query Store hint exists for a specific *query_id*, a new Query Store hint will be created.
- If a Query Store hint already exists for a specific *query_id*, the last value provided will override previously specified values for the associated query.
- If a *query_id* doesn't exist, an error will be raised.

In the cases where a hint would cause a query to fail, the hint is ignored and the latest failure details can be viewed in [sys.query_store_query_hints](../system-catalog-views/sys-query-store-query-hints-transact-sql.md).

To remove hints associated with a *query_id*, use the system stored procedure [sys.sp_query_store_clear_hints](sys-sp-query-store-clear-hints-transact-sql.md).

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
  | USE HINT ( '<hint_name>' [ , ...n ] )
```

The following query hints are currently unsupported:

- `OPTIMIZE FOR ( @var = val)`
- `MAXRECURSION`
- `USE PLAN` (instead, consider Query Store's original plan forcing capability, [sp_query_store_force_plan](sp-query-store-force-plan-transact-sql.md)).
- `DISABLE_DEFERRED_COMPILATION_TV`
- `DISABLE_TSQL_SCALAR_UDF_INLINING`
- [Table hints (for example, FORCESEEK, READUNCOMMITTED, INDEX)](../../t-sql/queries/hints-transact-sql-table.md)

## Permissions

Requires the ALTER permission on the database.

## Examples

### Identify a query in Query Store

The following example queries [sys.query_store_query_text](../system-catalog-views/sys-query-store-query-text-transact-sql.md) and [sys.query_store_query](../system-catalog-views/sys-query-store-query-transact-sql.md) to return the *query_id* for an executed query text fragment.

In this example, the query we're attempting to tune is in the `SalesLT` sample database:

```sql
SELECT * FROM SalesLT.Address as A
INNER JOIN SalesLT.CustomerAddress as CA
on A.AddressID = CA.AddressID
WHERE PostalCode = '98052' ORDER BY A.ModifiedDate DESC;
```

Query Store doesn't immediately reflect query data to its system views.

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

In the following samples, the previous query example in the `SalesLT` database was identified as *query_id* 39.

### Apply single hint

The following example applies the RECOMPILE hint to *query_id* 39, as identified in Query Store:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(RECOMPILE)';
```

The following example applies the hint to force the [legacy cardinality estimator](../performance/cardinality-estimation-sql-server.md) to *query_id* 39, identified in Query Store:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(USE HINT(''FORCE_LEGACY_CARDINALITY_ESTIMATION''))';
```

### Apply multiple hints

The following example applies multiple query hints to *query_id* 39, including RECOMPILE, MAXDOP 1, and the SQL 2012 query optimizer behavior:

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

Use the following example to remove the hint from *query_id* 39, using the [sp_query_store_clear_hints (Transact-SQL)](sys-sp-query-store-clear-hints-transact-sql.md) system stored procedure.

```sql
EXEC sys.sp_query_store_clear_hints @query_id = 39;
```

## Related content

- [Query Store hints](../performance/query-store-hints.md)
- [Table hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-table.md)
- [sp_query_store_clear_hints (Transact-SQL)](sys-sp-query-store-clear-hints-transact-sql.md)
- [sys.query_store_query_hints (Transact-SQL)](../system-catalog-views/sys-query-store-query-hints-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
