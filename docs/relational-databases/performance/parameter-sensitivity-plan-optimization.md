---
title: Parameter Sensitive Plan optimization
description: Learn about Parameter Sensitive Plan Optimization in the Query Store.
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Store"
dev_langs:
 - "TSQL"
author: thesqlsith
ms.author: derekw
ms.reviewer: maghan
ms.custom:
- event-tier1-build-2022
ms.date: 07/29/2022
monikerRange: "=azuresqldb-current||>=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---

# Parameter Sensitive Plan optimization

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Parameter Sensitive Plan (PSP) optimization is part of the Intelligent query processing family of features. It addresses the scenario where a single cached plan for a parameterized query is not optimal for all possible incoming parameter values. This is the case with non-uniform data distributions. For more information, see
[Parameter Sensitivity](../query-processing-architecture-guide.md#parameter-sensitivity) and [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse).

For more information on existing workarounds for this problem scenario, see:
- [Investigate and resolve parameter-sensitive issues](/troubleshoot/sql/performance/troubleshoot-high-cpu-usage-issues#step-5-investigate-and-resolve-parameter-sensitive-issues)
- [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse)
- [Queries that have parameter sensitive plan (PSP) problems](/azure/azure-sql/database/identify-query-performance-issues#parameter-sensitivity).

PSP optimization automatically enables multiple, active cached plans for a single parameterized statement. Cached execution plans will accommodate different data sizes based on the customer-provided runtime parameter value(s).

## Understanding parameterization

In the SQL Server Database Engine, using parameters or parameter markers in Transact-SQL statements increases the ability of the relational engine to match new Transact-SQL statements with existing, previously compiled execution plans and promote plan reutilization. For more information, see [Simple Parameterization](../query-processing-architecture-guide.md#simple-parameterization).

You can also override the default simple parameterization behavior of SQL Server by specifying that all `SELECT`, `INSERT`, `UPDATE`, and `DELETE` statements in a database be parameterized subject to certain limitations. For more information, see [Forced Parameterization](../query-processing-architecture-guide.md#forced-parameterization).

## PSP optimization implementation

During the initial compilation, column statistics histograms identify non-uniform distributions and evaluate the most "at-risk" parameterized predicates, up to three out of all available predicates.

For eligible plans, the initial compilation produces a **dispatcher plan** that contains the PSP optimization logic called a dispatcher expression. A dispatcher plan maps to **query variants** based on the cardinality range boundary values predicates.

For each predicate that is chosen, the Query Processor bucketizes them into predicate cardinality ranges based on the runtime cardinality, as seen in the following picture:

:::image type="content" source="media/parameter-sensitivity-plan-optimization/psp-boundaries.jpg" alt-text="Graphic of P S P boundaries.":::

Dispatcher plans are automatically rebuilt if there are significant data distribution changes. Query variant plans will independently recompile as needed, same as any other query plan type, and subject to default recompilation events. For more information about recompilation, see [Recompiling Execution Plans](../query-processing-architecture-guide.md#recompiling-execution-plans).

For each query variant mapping to a given dispatcher:

- The `query_plan_hash` is unique. This column is available in `sys.dm_exec_query_stats`, and other Dynamic Management Views and catalog tables.
- The `plan_handle` is unique. This column is available in `sys.dm_exec_query_stats`, `sys.dm_exec_sql_text`, `sys.dm_exec_cached_plans`, and in other Dynamic Management Views and Functions, and catalog tables.
- The `query_hash` is common to other variants mapping to the same dispatcher, so it's possible to determine aggregate resource usage for queries that differ only by input parameter values. This column is available in `sys.dm_exec_query_stats`, `sys.query_store_query`, and other Dynamic Management Views and catalog tables.
- The `sql_handle` is unique due to special PSP optimization identifiers being added to the query text during compilation. This column is available in `sys.dm_exec_query_stats`, `sys.dm_exec_sql_text`, `sys.dm_exec_cached_plans`, and in other Dynamic Management Views and Functions, and catalog tables. The same handle information is available in the Query Store as the `last_compile_batch_sql_handle` column in the `sys.query_store_query` catalog table.
- The `query_id` is unique in the Query Store. This column is available in `sys.query_store_query`, and other Query Store catalog tables.

## Considerations

To enable PSP optimization, enable database compatibility level 160 for the database you are connected to when executing the query.

For additional insights into the PSP optimization feature, we recommend that Query Store integration also be enabled by ensuring that the Query Store is turned on.  The following example will turn on the Query Store for a database called `my_psp_database`:

`ALTER DATABASE my_psp_database  SET QUERY_STORE (QUERY_CAPTURE_MODE = AUTO);`

To disable PSP optimization at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SENSITIVE_PLAN_OPTIMIZATION = OFF` database scoped configuration.

- `Dispatcher` element shows details of the predicate boundaries as derived from a statistics histogram.

 ```xml
 <Dispatcher>
  <ParameterSensitivePredicate LowBoundary="100" HighBoundary="1000000">
    <StatisticsInfo Database="[PropertyMLS]" Schema="[dbo]" Table="[Property]" Statistics="[NCI_Property_AgentId]" ModificationCount="0" SamplingPercent="100" LastUpdate="2019-09-07T15:32:16.89" />
    <Predicate>
   <ScalarOperator ScalarString="[PropertyMLS].[dbo].[Property].[AgentId]=[@AgentId]">
     <Compare CompareOp="EQ">
    <ScalarOperator>
      <Identifier>
     <ColumnReference Database="[PropertyMLS]" Schema="[dbo]" Table="[Property]" Column="AgentId" />
      </Identifier>
    </ScalarOperator>
    <ScalarOperator>
      <Identifier>
     <ColumnReference Column="@AgentId" />
      </Identifier>
    </ScalarOperator>
     </Compare>
   </ScalarOperator>
    </Predicate>
  </ParameterSensitivePredicate>
 </Dispatcher>
 ```

- `QueryVariantID` attribute on QueryPlan element for query variant identification.

 ```xml
 <QueryPlan DegreeOfParallelism="1" MemoryGrant="1024" CachedPlanSize="40" CompileTime="2" CompileCPU="2" CompileMemory="224" QueryVariantID="1">
 ```

To disable PSP optimization at the query level, use the `DISABLE_PARAMETER_SENSITIVE_PLAN_OPTIMIZATION` query hint.

If parameter sniffing is disabled by trace flag 4136, `PARAMETER_SNIFFING` database scoped configuration, or the `USE HINT('DISABLE_PARAMETER_SNIFFING')` query hint, PSP optimization will be disabled for the associated workloads and execution contexts. For more information, see [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md) and [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

The number of unique plan variants per dispatcher stored in the plan cache is limited to avoid cache bloating. The internal threshold isn't documented.

The number of unique plan variants stored for a query in the Query Store store is limited by the `max_plans_per_query` configuration option.

### Extended Events

The following XEs are available for the feature:

- `parameter_sensitive_plan_optimization_skipped_reason`: Occurs when the parameter sensitive plan feature is skipped. Use this event to monitor the reason why parameter sensitive plan optimization is skipped.
- `parameter_sensitive_plan_optimization`: Occurs when a query uses Parameter Sensitive Plan (PSP) Optimization feature. Debug channel only.
- `parameter_sensitive_plan_testing`: Occurs when parameter-sensitive plan is tested. Debug channel only.

### Plan forcing in Query Store

Uses the same [sp_query_store_force_plan](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md) and [sp_query_store_unforce_plan](../system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md) stored procedures to operate on dispatcher or variant plans.

If a variant is forced, the parent dispatcher is not forced.
If a dispatcher is forced, only variants from that dispatcher are considered eligible for use:

- Previously forced variants from other dispatchers will become inactive but retain the *forced* status until their dispatcher is forced again
- Previously forced variants in the same dispatcher that had become inactive are forced again

## Known issues

At this time, the following are known issue(s) that will be addressed in upcoming CTP releases:

- When you have a stored procedure created in the non-dbo schema, and you have queries inside the stored procedure that reference tables without schema, error 208 can occur.

 ```sql
 -- This fails
 CREATE OR ALTER PROCEDURE my_schema.my_proc @var1 int
 AS
 SELECT c2 FROM my_table WHERE c1 = @var1;
 
 -- This works
 CREATE OR ALTER PROCEDURE my_schema.my_proc @var1 int
 AS
 SELECT c2 FROM my_schema.my_table WHERE c1 = @var1;
 ```

## Next steps

- [Intelligent query processing](intelligent-query-processing.md)  
- [Parameter Sensitivity](../query-processing-architecture-guide.md#parameter-sensitivity)
- [Simple Parameterization](../query-processing-architecture-guide.md#simple-parameterization)
- [Forced Parameterization](../query-processing-architecture-guide.md#forced-parameterization)
- [Hints query](../../t-sql/queries/hints-transact-sql-query.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [Recompiling Execution Plans](../query-processing-architecture-guide.md#recompiling-execution-plans)
- [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse)
