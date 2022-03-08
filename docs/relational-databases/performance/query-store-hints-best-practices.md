---
title: "Query Store hints (Preview) best practices"
description: "Best practices for the Query Store hints feature, which helps you to shape query plans without changing application code."
ms.custom: ""
ms.date: "3/7/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: performance
ms.topic: conceptual
dev_langs:
 - "TSQL"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current"
---
# Query Store hints (Preview) best practices
[!INCLUDE [asdb-asdbmi](../../includes/applies-to-version/asdb-asdbmi.md)]

This article details best practices for using the [Query Store hints feature](query-store-hints.md). The Query Store hints (preview) feature enables shaping query plan shapes without modifying application code.

> [!Note]
> Query Store hints are a public preview feature currently available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] – including hyperscale databases, as well as Azure SQL Managed Instance.

- For more information on configuring and administering with the Query Store, see [Monitoring performance by using the Query Store](monitoring-performance-by-using-the-query-store.md#monitoring-performance-by-using-the-query-store).
- For information on discovering actionable information and tune performance with the Query Store, see [Tuning performance by using the Query Store](tuning-with-the-query-store.md).

Learn how to improve bulk loading with Query Store hints with this 14-minute video:

> [!VIDEO https://channel9.msdn.com/Shows/data-exposed/using-query-store-hints-to-optimize-memory-grants-improving-performance/player?WT.mc_id=dataexposed-c9-niner]

## Best use cases for Query Store hints

Consider the following use cases as ideal of Query Store hints. For more information, see [When to use Query Store hints](query-store-hints.md#when-to-use-query-store-hints).

### Code cannot be changed

Using Query Store hints allows you to influence the execution plans of queries without changing application code or database objects. No other feature allows for you to apply query hints quickly and easily. 

For information on which query hints can be applied, see [Supported query hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md#supported-query-hints).

### High transaction load, mission critical code

If code changes are impractical because of high uptime requirements or transactional load, Query Store hints can apply query hints to existing query workloads quickly. Adding and removing Query Store hints is easy.

Query Store hints can be added and removed to batches of queries to adjust performance for windows timed for bursts of exceptional workload.

### Replacement for plan guides

Previous to Query Store hints, a developer would have to rely on [plan guides](plan-guides.md) to accomplish similar takss, which can be complex to use. The Query Store hints feature is integrated with the Query Store features of SQL Server Management Studio (SSMS), for visual exploration of queries. 

With plan guides, searching through all plans using query snippets is necessary. The Query Store hints features does not require exact matching queries to impact the resulting query plan. Query Store hints can be applied to a `query_id` in the Query Store dataset. 

Query Store hints override hard-coded statement-level hints and existing plan guides. 

## When to avoid Query Store hints

Avoid Query Store hints in the following use cases.

### Broad impact potential

Query Store hints will affect all executions of the query, regardless of parameter set, source application, user, or result set. In the case of accidentally performance regression, Query Store hints created with [sys.sp_query_store_set_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md) can be removed with [sys.sp_query_store_clear_hints](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md).

Carefully load test changes for mission critical or sensitive systems before applying Query Store hints in production. 

### Forced parameterization and the WITH RECOMPILE hint are not supported

Applying the WITH RECOMPILE query hint with Query Store hints is not supported when the database option [PARAMETERIZATION is set to FORCED](../../t-sql/statements/alter-database-transact-sql-set-options.md#parameterization_option-). For more information, see [Guidelines for Using Forced Parameterization](../../relational-databases/query-processing-architecture-guide.md#ForcedParamGuide).

The RECOMPILE hint is not compatible with forced parameterization set at the database level. If the database has forced parameterization set, and the RECOMPILE hint is part of the hints string set in Query Store for a query, the Database Engine will ignore the RECOMPILE hint and will apply other hints if leveraged.

<!-- t63 warning-->


## See also

- [Query Store hints](query-store-hints.md)
- [sys.query_store_query_hints (Transact-SQL)](../system-catalog-views/sys-query-store-query-hints-transact-sql.md)   
- [sys.sp_query_store_set_hints (Transact-SQL)](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md)   
- [sys.sp_query_store_clear_hints (Transact-SQL)](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md)   
- [Save an Execution Plan in XML Format](save-an-execution-plan-in-xml-format.md)
- [Display and Save Execution Plans](display-and-save-execution-plans.md)
- [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md)  

## Next steps

- [Best practices with Query Store](best-practice-with-the-query-store.md)
- [Monitor performance by using Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism)
