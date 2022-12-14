---
title: "Query Hints (Transact-SQL)"
description: "Query hints specify that the indicated hints are used in the scope of a query. They affect all operators in the statement."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, randolphwest
ms.date: 10/24/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "Query_Hint_TSQL"
  - "Query_TSQL"
  - "Query"
  - "Query Hint"
  - "MAX_GRANT_PERCENT"
  - "MAX_GRANT_PERCENT_TSQL"
  - "MIN_GRANT_PERCENT"
  - "MIN_GRANT_PERCENT_TSQL"
  - "EXTERNALPUSHDOWN"
  - "EXTERNALPUSHDOWN_TSQL"
  - "NOLOCK_TSQL"
  - "MAXDOP_TSQL"
  - "USE_HINT_TSQL"
helpviewer_keywords:
  - "REPORT PLAN query hint"
  - "FORCE ORDER query hint"
  - "HASH JOIN query hint"
  - "query hints [SQL Server]"
  - "OPTIMIZE FOR query hint"
  - "FORCESCAN query hint"
  - "RECOMPILE query hint"
  - "MAXRECURSION query hint"
  - "MERGE JOIN query hint"
  - "HASH GROUP query hint"
  - "KEEP PLAN query hint"
  - "UNION query hint"
  - "FORCESEEK query hint"
  - "ORDER GROUP query hint"
  - "LOOP JOIN query hint"
  - "KEEPFIXED PLAN query hint"
  - "FAST query hint"
  - "MAXDOP query hint"
  - "PARAMETERIZATION query hint"
  - "hints [SQL Server], query"
  - "JOIN query hint"
  - "USE PLAN query hint"
  - "EXPAND VIEWS query hint"
  - "MAX_GRANT_PERCENT query hint"
  - "MIN_GRANT_PERCENT query hint"
  - "EXTERNALPUSHDOWN query hint"
  - "USE HINT query hint"
  - "QUERY_PLAN_PROFILE query hint"
dev_langs:
  - "TSQL"
---

# Hints (Transact-SQL) - Query

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Query hints specify that the indicated hints are used in the scope of a query. They affect all operators in the statement. If UNION is involved in the main query, only the last query involving a UNION operation can have the OPTION clause. Query hints are specified as part of the [OPTION clause](../queries/option-clause-transact-sql.md). Error 8622 occurs if one or more query hints cause the Query Optimizer not to generate a valid plan.

> [!CAUTION]  
> Because the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer typically selects the best execution plan for a query, we recommend only using hints as a last resort for experienced developers and database administrators.

**Applies to:**

- [DELETE](../statements/delete-transact-sql.md)
- [INSERT](../statements/insert-transact-sql.md)
- [SELECT](../queries/select-transact-sql.md)
- [UPDATE](../queries/update-transact-sql.md)
- [MERGE](../statements/merge-transact-sql.md)

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
<query_hint> ::=
{ { HASH | ORDER } GROUP
  | { CONCAT | HASH | MERGE } UNION
  | { LOOP | MERGE | HASH } JOIN
  | DISABLE_OPTIMIZED_PLAN_FORCING
  | EXPAND VIEWS
  | FAST <integer_value>
  | FORCE ORDER
  | { FORCE | DISABLE } EXTERNALPUSHDOWN
  | { FORCE | DISABLE } SCALEOUTEXECUTION
  | IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX
  | KEEP PLAN
  | KEEPFIXED PLAN
  | MAX_GRANT_PERCENT = <numeric_value>
  | MIN_GRANT_PERCENT = <numeric_value>
  | MAXDOP <integer_value>
  | MAXRECURSION <integer_value>
  | NO_PERFORMANCE_SPOOL
  | OPTIMIZE FOR ( @variable_name { UNKNOWN | = <literal_constant> } [ , ...n ] )
  | OPTIMIZE FOR UNKNOWN
  | PARAMETERIZATION { SIMPLE | FORCED }
  | QUERYTRACEON <integer_value>
  | RECOMPILE
  | ROBUST PLAN
  | USE HINT ( <use_hint_name> [ , ...n ] )
  | USE PLAN N'<xml_plan>'
  | TABLE HINT ( <exposed_object_name> [ , <table_hint> [ [, ]...n ] ] )
}

<table_hint> ::=
{ NOEXPAND [ , INDEX ( <index_value> [ ,...n ] ) | INDEX = ( <index_value> ) ]
  | INDEX ( <index_value> [ ,...n ] ) | INDEX = ( <index_value> )
  | FORCESEEK [ ( <index_value> ( <index_column_name> [,... ] ) ) ]
  | FORCESCAN
  | HOLDLOCK
  | NOLOCK
  | NOWAIT
  | PAGLOCK
  | READCOMMITTED
  | READCOMMITTEDLOCK
  | READPAST
  | READUNCOMMITTED
  | REPEATABLEREAD
  | ROWLOCK
  | SERIALIZABLE
  | SNAPSHOT
  | SPATIAL_WINDOW_MAX_CELLS = <integer_value>
  | TABLOCK
  | TABLOCKX
  | UPDLOCK
  | XLOCK
}

<use_hint_name> ::=
{ 'ASSUME_JOIN_PREDICATE_DEPENDS_ON_FILTERS'
  | 'ASSUME_MIN_SELECTIVITY_FOR_FILTER_ESTIMATES'
  | 'ASSUME_FULL_INDEPENDENCE_FOR_FILTER_ESTIMATES'
  | 'ASSUME_PARTIAL_CORRELATION_FOR_FILTER_ESTIMATES'
  | 'DISABLE_BATCH_MODE_ADAPTIVE_JOINS'
  | 'DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK'
  | 'DISABLE_DEFERRED_COMPILATION_TV'
  | 'DISABLE_INTERLEAVED_EXECUTION_TVF'
  | 'DISABLE_OPTIMIZED_NESTED_LOOP'
  | 'DISABLE_OPTIMIZER_ROWGOAL'
  | 'DISABLE_PARAMETER_SNIFFING'
  | 'DISABLE_ROW_MODE_MEMORY_GRANT_FEEDBACK'
  | 'DISABLE_TSQL_SCALAR_UDF_INLINING'
  | 'DISALLOW_BATCH_MODE'
  | 'ENABLE_HIST_AMENDMENT_FOR_ASC_KEYS'
  | 'ENABLE_QUERY_OPTIMIZER_HOTFIXES'
  | 'FORCE_DEFAULT_CARDINALITY_ESTIMATION'
  | 'FORCE_LEGACY_CARDINALITY_ESTIMATION'
  | 'QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n'
  | 'QUERY_PLAN_PROFILE'
}
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### { HASH | ORDER } GROUP

Specifies that aggregations that the query's GROUP BY or DISTINCT clause describes should use hashing or ordering.

#### { MERGE | HASH | CONCAT } UNION

Specifies that all UNION operations are run by merging, hashing, or concatenating UNION sets. If more than one UNION hint is specified, the Query Optimizer selects the least expensive strategy from those hints specified.

#### { LOOP | MERGE | HASH } JOIN

Specifies all join operations are performed by LOOP JOIN, MERGE JOIN, or HASH JOIN in the whole query. If you specify more than one join hint, the optimizer selects the least expensive join strategy from the allowed ones.

If you specify a join hint in the same query's FROM clause for a specific table pair, this join hint takes precedence in the joining of the two tables. The query hints, though, must still be honored. The join hint for the pair of tables may only restrict the selection of allowed join methods in the query hint. For more information, see [Join Hints (Transact-SQL)](../queries/hints-transact-sql-join.md).

#### DISABLE_OPTIMIZED_PLAN_FORCING

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Disables [Optimized plan forcing](../../relational-databases/performance/optimized-plan-forcing-query-store.md) for a query.

Optimized plan forcing reduces compilation overhead for repeating forced queries. Once the query execution plan is generated, specific compilation steps are stored for reuse as an optimization replay script. An optimization replay script is stored as part of the compressed showplan XML in [Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md), in a hidden `OptimizationReplay` attribute.

#### EXPAND VIEWS

Specifies the indexed views are expanded. Also specifies the Query Optimizer won't consider any indexed view as a replacement for any query part. A view is expanded when the view definition replaces the view name in the query text.

This query hint virtually disallows direct use of indexed views and indexes on indexed views in the query plan.

> [!NOTE]  
> The indexed view remains condensed if there's a direct reference to the view in the query's SELECT part. The view also remains condensed if you specify WITH (NOEXPAND) or WITH (NOEXPAND, INDEX( *<index_value>* [ , *...n* ] ) ). For more information about the query hint NOEXPAND, see [Using NOEXPAND](../queries/hints-transact-sql-table.md#using-noexpand).

The hint only affects the views in the statements' SELECT part, including those views in INSERT, UPDATE, MERGE, and DELETE statements.

#### FAST *<integer_value>*

Specifies that the query is optimized for fast retrieval of the first *<integer_value>* number of rows. This result is a non-negative integer. After the first *<integer_value>* number of rows are returned, the query continues execution and produces its full result set.

#### FORCE ORDER

Specifies that the join order indicated by the query syntax is preserved during query optimization. Using FORCE ORDER doesn't affect possible role reversal behavior of the Query Optimizer.

> [!NOTE]  
> In a MERGE statement, the source table is accessed before the target table as the default join order, unless the WHEN SOURCE NOT MATCHED clause is specified. Specifying FORCE ORDER preserves this default behavior.

#### { FORCE | DISABLE } EXTERNALPUSHDOWN

Force or disable the pushdown of the computation of qualifying expressions in Hadoop. Only applies to queries using PolyBase. Won't push down to Azure storage.

#### { FORCE | DISABLE } SCALEOUTEXECUTION

Force or disable scale out execution of PolyBase queries that are using external tables in SQL Server 2019 Big Data Clusters. This hint will only be honored by a query using the master instance of a SQL big data cluster. The scale out will occur across the compute pool of the big data cluster.

#### KEEP PLAN

Changes the [recompilation thresholds](../../relational-databases/statistics/statistics.md#auto_update_statistics-option) for temporary tables, and makes them identical to those for permanent tables. The estimated recompile threshold starts an automatic recompile for the query when the estimated number of indexed column changes have been made to a table by running one of the following statements:

- UPDATE
- DELETE
- MERGE
- INSERT

Specifying KEEP PLAN makes sure a query won't be recompiled as frequently when there are multiple updates to a table.

#### KEEPFIXED PLAN

Forces the Query Optimizer not to recompile a query because of changes in statistics. Specifying KEEPFIXED PLAN makes sure that a query recompiles only if the schema of the underlying tables changes, or if `sp_recompile` runs against those tables.

#### IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]).

Prevents the query from using a nonclustered memory optimized columnstore index. If the query contains the query hint to avoid the use of the columnstore index, and an index hint to use a columnstore index, the hints are in conflict and the query returns an error.

#### MAX_GRANT_PERCENT = <numeric_value>

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

The maximum memory grant size in PERCENT of configured memory limit. The query is guaranteed not to exceed this limit. The actual limit can be lower if the Resource Governor setting is lower than the value specified by this hint. Valid values are between 0.0 and 100.0.

#### MIN_GRANT_PERCENT = <numeric_value>

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

The minimum memory grant size in PERCENT of configured memory limit. The query is guaranteed to get `MAX(required memory, min grant)` because at least required memory is needed to start a query. Valid values are between 0.0 and 100.0.

#### MAXDOP <integer_value>

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

Overrides the **max degree of parallelism** configuration option of `sp_configure`. Also overrides the Resource Governor for the query specifying this option. The MAXDOP query hint can exceed the value configured with `sp_configure`. If MAXDOP exceeds the value configured with Resource Governor, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses the Resource Governor MAXDOP value, described in [ALTER WORKLOAD GROUP (Transact-SQL)](../statements/alter-workload-group-transact-sql.md). All semantic rules used with the **max degree of parallelism** configuration option are applicable when you use the MAXDOP query hint. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

> [!WARNING]  
> If MAXDOP is set to zero, then the server chooses the max degree of parallelism.

#### MAXRECURSION <integer_value>

Specifies the maximum number of recursions allowed for this query. *number* is a nonnegative integer between 0 and 32,767. When 0 is specified, no limit is applied. If this option isn't specified, the default limit for the server is 100.

When the specified or default number for MAXRECURSION limit is reached during query execution, the query ends and an error returns.

Because of this error, all effects of the statement are rolled back. If the statement is a SELECT statement, partial results or no results may be returned. Any partial results returned may not include all rows on recursion levels beyond the specified maximum recursion level.

For more information, see [WITH common_table_expression (Transact-SQL)](../queries/with-common-table-expression-transact-sql.md).

#### NO_PERFORMANCE_SPOOL

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

Prevents a spool operator from being added to query plans (except for the plans when spool is required to guarantee valid update semantics). The spool operator may reduce performance in some scenarios. For example, the spool uses `tempdb`, and `tempdb` contention can occur if there are many concurrent queries running with the spool operations.

#### OPTIMIZE FOR ( *@variable_name* { UNKNOWN | = <literal_constant> } [ , *...n* ] )

Instructs the Query Optimizer to use a particular value for a local variable when the query is compiled and optimized. The value is used only during query optimization, and not during query execution.

- *@variable_name*

  The name of a local variable used in a query, to which a value may be assigned for use with the OPTIMIZE FOR query hint.

- *UNKNOWN*

  Specifies that the Query Optimizer uses statistical data instead of the initial value to determine the value for a local variable during query optimization.

- *<literal_constant>*

  A literal constant value to be assigned *@variable_name* for use with the OPTIMIZE FOR query hint. *<literal_constant>* is used only during query optimization, and not as the value of *@variable_name* during query execution. *<literal_constant>* can be of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type that can be expressed as a literal constant. The data type of *<literal_constant>* must be implicitly convertible to the data type that *@variable_name* references in the query.

OPTIMIZE FOR can counteract the optimizer's default parameter detection behavior. Also use OPTIMIZE FOR when you create plan guides. For more information, see [Recompile a Stored Procedure](../../relational-databases/stored-procedures/recompile-a-stored-procedure.md).

#### OPTIMIZE FOR UNKNOWN

Instructs the Query Optimizer to use the average selectivity of the predicate across all column values instead of using the runtime parameter value when the query is compiled and optimized.

If you use `OPTIMIZE FOR @variable_name = <literal_constant>` and `OPTIMIZE FOR UNKNOWN` in the same query hint, the Query Optimizer will use the *literal_constant* specified for a specific value. The Query Optimizer will use UNKNOWN for the rest of the variable values. The values are used only during query optimization, and not during query execution.

#### PARAMETERIZATION { SIMPLE | FORCED }

Specifies the parameterization rules that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer applies to the query when it's compiled.

> [!IMPORTANT]  
> The PARAMETERIZATION query hint can only be specified inside a plan guide to override the current setting of the PARAMETERIZATION database SET option. It can't be specified directly within a query.
>
> For more information, see [Specify Query Parameterization Behavior by Using Plan Guides](../../relational-databases/performance/specify-query-parameterization-behavior-by-using-plan-guides.md).

SIMPLE instructs the Query Optimizer to attempt simple parameterization. FORCED instructs the Query Optimizer to attempt forced parameterization. For more information, see [Forced Parameterization in the Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization), and [Simple Parameterization in the Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#simple-parameterization).

#### QUERYTRACEON <integer_value>

This option lets you enable a plan-affecting trace flag only during single-query compilation. Like other query-level options, you can use it together with plan guides to match the text of a query being executed from any session, and automatically apply a plan-affecting trace flag when this query is being compiled. The QUERYTRACEON option is only supported for Query Optimizer trace flags. For more information, see [Trace Flags](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

Using this option won't return any error or warning if an unsupported trace flag number is used. If the specified trace flag isn't one that affects a query execution plan, the option will be silently ignored.

To use more than one trace flag in a query, specify one QUERYTRACEON hint for each different trace flag number.

#### RECOMPILE

Instructs the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to generate a new, temporary plan for the query and immediately discard that plan after the query completes execution. The generated query plan doesn't replace a plan stored in cache when the same query runs without the RECOMPILE hint. Without specifying RECOMPILE, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] caches query plans and reuses them. When compiling query plans, the RECOMPILE query hint uses the current values of any local variables in the query. If the query is inside a stored procedure, the current values passed to any parameters.

RECOMPILE is a useful alternative to creating a stored procedure. RECOMPILE uses the WITH RECOMPILE clause when only a subset of queries inside the stored procedure, instead of the whole stored procedure, must be recompiled. For more information, see [Recompile a Stored Procedure](../../relational-databases/stored-procedures/recompile-a-stored-procedure.md). RECOMPILE is also useful when you create plan guides.

#### ROBUST PLAN

Forces the Query Optimizer to try a plan that works for the maximum potential row size, possibly at the expense of performance. When the query is processed, intermediate tables and operators may have to store and process rows that are wider than any one of the input rows when the query is processed. The rows may be so wide that, sometimes, the particular operator can't process the row. If rows are that wide, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] produces an error during query execution. By using ROBUST PLAN, you instruct the Query Optimizer not to consider any query plans that may run into this problem.

If such a plan isn't possible, the Query Optimizer returns an error instead of deferring error detection to query execution. Rows may contain variable-length columns; the [!INCLUDE[ssDE](../../includes/ssde-md.md)] allows for rows to be defined that have a maximum potential size beyond the ability of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to process them. Generally, despite the maximum potential size, an application stores rows that have actual sizes within the limits that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] can process. If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] comes across a row that is too long, an execution error is returned.

#### <a id="use_hint"></a> USE HINT ( '*hint_name*' )

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

Provides one or more additional hints to the query processor. The additional hints are specified by a hint name **inside single quotation marks**.

The following hint names are supported:

- 'ASSUME_JOIN_PREDICATE_DEPENDS_ON_FILTERS' <a id="use_hint_join_containment"></a>

  Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate a query plan using the Simple Containment assumption instead of the default Base Containment assumption for joins, under the Query Optimizer [Cardinality Estimation](../../relational-databases/performance/cardinality-estimation-sql-server.md) model of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or newer. This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 9476.

- 'ASSUME_MIN_SELECTIVITY_FOR_FILTER_ESTIMATES' <a id="use_hint_correlation"></a>  

  Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate a plan using minimum selectivity when estimating AND predicates for filters to account for full correlation. This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 4137 when used with cardinality estimation model of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and earlier versions, and has similar effect when [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 9471 is used with cardinality estimation model of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or higher.

- 'ASSUME_FULL_INDEPENDENCE_FOR_FILTER_ESTIMATES'  

  Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate a plan using maximum selectivity when estimating AND predicates for filters to account for full independence. This hint name is the default behavior of the cardinality estimation model of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and earlier versions, and equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 9472 when used with cardinality estimation model of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or higher.  

  **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'ASSUME_PARTIAL_CORRELATION_FOR_FILTER_ESTIMATES'  

  Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate a plan using most to least selectivity when estimating AND predicates for filters to account for partial correlation. This hint name is the default behavior of the cardinality estimation model of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] or higher.  

  **Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISABLE_BATCH_MODE_ADAPTIVE_JOINS'  

  Disables batch mode adaptive joins. For more information, see [Batch mode Adaptive Joins](../../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-adaptive-joins).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK'  

  Disables batch mode memory grant feedback. For more information, see [Batch mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISABLE_DEFERRED_COMPILATION_TV'  

  Disables table variable deferred compilation. For more information, see [Table variable deferred compilation](../../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISABLE_INTERLEAVED_EXECUTION_TVF'  

  Disables interleaved execution for multi-statement table-valued functions. For more information, see [Interleaved execution for multi-statement table-valued functions](../../relational-databases/performance/intelligent-query-processing-details.md#interleaved-execution-for-mstvfs).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISABLE_OPTIMIZED_NESTED_LOOP'  

  Instructs the query processor not to use a sort operation (batch sort) for optimized nested loop joins when generating a query plan. This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 2340.

- 'DISABLE_OPTIMIZER_ROWGOAL' <a id="use_hint_rowgoal"></a>  

  Causes SQL Server to generate a plan that doesn't use row goal modifications with queries that contain these keywords:

  - TOP
  - OPTION (FAST N)
  - IN
  - EXISTS

  This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 4138.

- 'DISABLE_PARAMETER_SNIFFING'  

  Instructs Query Optimizer to use average data distribution while compiling a query with one or more parameters. This instruction makes the query plan independent on the parameter value that was first used when the query was compiled. This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 4136 or [Database Scoped Configuration](../statements/alter-database-scoped-configuration-transact-sql.md) setting `PARAMETER_SNIFFING = OFF`.

- 'DISABLE_ROW_MODE_MEMORY_GRANT_FEEDBACK'  

  Disables row mode memory grant feedback. For more information, see [Row mode memory grant feedback](../../relational-databases/performance/intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISABLE_TSQL_SCALAR_UDF_INLINING'  

  Disables scalar UDF inlining. For more information, see [Scalar UDF Inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'DISALLOW_BATCH_MODE'  

  Disables batch mode execution. For more information, see [Execution modes](../../relational-databases/query-processing-architecture-guide.md#execution-modes).  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

- 'ENABLE_HIST_AMENDMENT_FOR_ASC_KEYS'  

  Enables automatically generated quick statistics (histogram amendment) for any leading index column for which cardinality estimation is needed. The histogram used to estimate cardinality will be adjusted at query compile time to account for actual maximum or minimum value of this column. This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 4139.

- 'ENABLE_QUERY_OPTIMIZER_HOTFIXES'  

  Enables Query Optimizer hotfixes (changes released in SQL Server Cumulative Updates and Service Packs). This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 4199 or [Database Scoped Configuration](../statements/alter-database-scoped-configuration-transact-sql.md) setting `QUERY_OPTIMIZER_HOTFIXES = ON`.

- 'FORCE_DEFAULT_CARDINALITY_ESTIMATION'  

  Forces the Query Optimizer to use [Cardinality Estimation](../../relational-databases/performance/cardinality-estimation-sql-server.md) model that corresponds to the current database compatibility level. Use this hint to override [Database Scoped Configuration](../statements/alter-database-scoped-configuration-transact-sql.md) setting `LEGACY_CARDINALITY_ESTIMATION = ON` or [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 9481.

- 'FORCE_LEGACY_CARDINALITY_ESTIMATION' <a id="use_hint_ce70"></a>  

  Forces the Query Optimizer to use [Cardinality Estimation](../../relational-databases/performance/cardinality-estimation-sql-server.md) model of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and earlier versions. This hint name is equivalent to [trace flag](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 9481 or [Database Scoped Configuration](../statements/alter-database-scoped-configuration-transact-sql.md) setting `LEGACY_CARDINALITY_ESTIMATION = ON`.

- 'QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n'  

  Forces the Query Optimizer behavior at a query level. This behavior happens as if the query was compiled with database compatibility level *n*, where *n* is a supported database compatibility level (for example 100, 130, etc.). Refer to [sys.dm_exec_valid_use_hints](../../relational-databases/system-dynamic-management-views/sys-dm-exec-valid-use-hints-transact-sql.md) for a list of currently supported values for *n*.  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU10) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

  > [!NOTE]  
  > The QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n hint doesn't override default or legacy cardinality estimation setting, if it's forced through database scoped configuration, trace flag or another query hint such as QUERYTRACEON.  
  > This hint only affects the behavior of the Query Optimizer. It doesn't affect other features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that may depend on the [database compatibility level](../statements/alter-database-transact-sql-compatibility-level.md), such as the availability of certain database features.  
  > To learn more about this hint, see [Developer's Choice: Hinting Query Execution model](/archive/blogs/sql_server_team/developers-choice-hinting-query-execution-model).

- 'QUERY_PLAN_PROFILE'

  Enables lightweight profiling for the query. When a query that contains this new hint finishes, a new Extended Event, query_plan_profile, is fired. This extended event exposes execution statistics and actual execution plan XML similar to the query_post_execution_showplan extended event but only for queries that contains the new hint.  

  **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 CU3 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU11).

  > [!NOTE]  
  > If you enable collecting the query_post_execution_showplan extended event, this will add standard profiling infrastructure to every query that is running on the server and therefore may affect overall server performance.  
  > If you enable the collection of *query_thread_profile* extended event to use lightweight profiling infrastructure instead, this will result in much less performance overhead but will still affect overall server performance.  
  > If you enable the query_plan_profile extended event, this will only enable the lightweight profiling infrastructure for a query that executed with the QUERY_PLAN_PROFILE and therefore will not affect other workloads on the server. Use this hint to profile a specific query without affecting other parts of the server workload.
  > To learn more about lightweight profiling, see [Query Profiling Infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).

The list of all supported USE HINT names can be queried using the dynamic management view [sys.dm_exec_valid_use_hints](../../relational-databases/system-dynamic-management-views/sys-dm-exec-valid-use-hints-transact-sql.md).

> [!TIP]  
> Hint names are case-insensitive.

> [!IMPORTANT]  
> Some USE HINT hints may conflict with trace flags enabled at the global or session level, or database scoped configuration settings. In this case, the query level hint (USE HINT) always takes precedence. If a USE HINT conflicts with another query hint, or a trace flag enabled at the query level (such as by QUERYTRACEON), [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will generate an error when trying to execute the query.

#### <a id="use-plan"></a> USE PLAN N'*<xml_plan>*'

Forces the Query Optimizer to use an existing query plan for a query that is specified by '*<xml_plan>*'. USE PLAN can't be specified with INSERT, UPDATE, MERGE, or DELETE statements.

The resulting execution plan forced by this feature will be the same or similar to the plan being forced. Because the resulting plan may not be identical to the plan specified by USE PLAN, the performance of the plans may vary. In rare cases, the performance difference may be significant and negative; in that case, the administrator must remove the forced plan.

#### TABLE HINT (*<exposed_object_name>* [ , <table_hint> [ [, ]*...n* ] ] )

Applies the specified table hint to the table or view that corresponds to *exposed_object_name*. We recommend using a table hint as a query hint only in the context of a [plan guide](../../relational-databases/performance/plan-guides.md).

*<exposed_object_name>* can be one of the following references:

- When an alias is used for the table or view in the [FROM](../queries/from-transact-sql.md) clause of the query, *exposed_object_name* is the alias.

- When an alias isn't used, *exposed_object_name* is the exact match of the table or view referenced in the FROM clause. For example, if the table or view is referenced using a two-part name, *exposed_object_name* is the same two-part name.

When you specify *exposed_object_name* without also specifying a table hint, any indexes you specify in the query as part of a table hint for the object are disregarded. The Query Optimizer then determines index usage. You can use this technique to eliminate the effect of an INDEX table hint when you can't modify the original query. See Example J.

**<table_hint> ::=** {  
NOEXPAND [ , INDEX ( *<index_value>* [ ,...n ] ) | INDEX = ( *<index_value>* ) ] | INDEX ( *<index_value>* [ ,...n ] ) | INDEX = ( *<index_value>* ) | FORCESEEK [(*<index_value>*(*<index_column_name>* [,... ] ) ) ] | FORCESCAN | HOLDLOCK | NOLOCK | NOWAIT | PAGLOCK | READCOMMITTED | READCOMMITTEDLOCK | READPAST | READUNCOMMITTED | REPEATABLEREAD | ROWLOCK | SERIALIZABLE | SNAPSHOT | SPATIAL_WINDOW_MAX_CELLS = *<integer_value>* | TABLOCK | TABLOCKX | UPDLOCK | XLOCK }  

Is the table hint to apply to the table or view that corresponds to *exposed_object_name* as a query hint. For a description of these hints, see [Table Hints (Transact-SQL)](../queries/hints-transact-sql-table.md).

Table hints other than INDEX, FORCESCAN, and FORCESEEK are disallowed as query hints unless the query already has a WITH clause specifying the table hint. For more information, see the [Remarks section](#remarks).

> [!CAUTION]  
> Specifying FORCESEEK with parameters limits the number of plans that can be considered by the Query Optimizer more than when specifying FORCESEEK without parameters. This may cause a "Plan cannot be generated" error to occur in more cases. In a future release, internal modifications to the Query Optimizer may allow more plans to be considered.

## Remarks

Query hints can't be specified in an INSERT statement, except when a SELECT clause is used inside the statement.

Query hints can be specified only in the top-level query, not in subqueries. When a table hint is specified as a query hint, the hint can be specified in the top-level query or in a subquery. However, the value specified for *<exposed_object_name>* in the TABLE HINT clause must match exactly the exposed name in the query or subquery.

## Specify table hints as query hints

We recommend using the INDEX, FORCESCAN, or FORCESEEK table hint as a query hint only in the context of a [plan guide](../../relational-databases/performance/plan-guides.md). Plan guides are useful when you can't modify the original query, for example, because it's a third-party application. The query hint specified in the plan guide is added to the query before it's compiled and optimized. For ad-hoc queries, use the TABLE HINT clause only when testing plan guide statements. For all other ad-hoc queries, we recommend specifying these hints only as table hints.

When specified as a query hint, the INDEX, FORCESCAN, and FORCESEEK table hints are valid for the following objects:

- Tables
- Views
- Indexed views
- Common table expressions (the hint must be specified in the SELECT statement whose result set populates the common table expression)
- Dynamic Management Views (DMVs)
- Named subqueries

You can specify INDEX, FORCESCAN, and FORCESEEK table hints as query hints for a query that doesn't have any existing table hints. You can also use them to replace existing INDEX, FORCESCAN, or FORCESEEK hints in the query, respectively.

Table hints other than INDEX, FORCESCAN, and FORCESEEK are disallowed as query hints unless the query already has a WITH clause specifying the table hint. In this case, a matching hint must also be specified as a query hint. Specify the matching hint as a query hint by using TABLE HINT in the OPTION clause. This specification preserves the query's semantics. For example, if the query contains the table hint NOLOCK, the OPTION clause in the **@hints** parameter of the plan guide must also contain the NOLOCK hint. See Example K.

## Specify hints with Query Store hints

You can enforce hints on queries identified through Query Store without making code changes, using the [Query Store hints](../../relational-databases/performance/query-store-hints.md) feature. Use the [sys.sp_query_store_set_hints](../../relational-databases/system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md) stored procedure to apply a hint to a query. See Example N.

## Examples

### A. Use MERGE JOIN

The following example specifies that MERGE JOIN runs the JOIN operation in the query. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT *
FROM Sales.Customer AS c
INNER JOIN Sales.CustomerAddress AS ca ON c.CustomerID = ca.CustomerID
WHERE TerritoryID = 5
OPTION (MERGE JOIN);
GO
```

### B. Use OPTIMIZE FOR

The following example instructs the Query Optimizer to use the value `'Seattle'` for `@city_name` and to use the average selectivity of the predicate across all column values for `@postal_code` when optimizing the query. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
CREATE PROCEDURE dbo.RetrievePersonAddress
@city_name NVARCHAR(30),
@postal_code NVARCHAR(15)
AS
SELECT * FROM Person.Address
WHERE City = @city_name AND PostalCode = @postal_code
OPTION ( OPTIMIZE FOR (@city_name = 'Seattle', @postal_code UNKNOWN) );
GO
```

### C. Use MAXRECURSION

MAXRECURSION can be used to prevent a poorly formed recursive common table expression from entering into an infinite loop. The following example intentionally creates an infinite loop and uses the MAXRECURSION hint to limit the number of recursion levels to two. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
--Creates an infinite loop
WITH cte (CustomerID, PersonID, StoreID) AS
(
    SELECT CustomerID, PersonID, StoreID
    FROM Sales.Customer
    WHERE PersonID IS NOT NULL
  UNION ALL
    SELECT cte.CustomerID, cte.PersonID, cte.StoreID
    FROM cte
    JOIN  Sales.Customer AS e
        ON cte.PersonID = e.CustomerID
)
--Uses MAXRECURSION to limit the recursive levels to 2
SELECT CustomerID, PersonID, StoreID
FROM cte
OPTION (MAXRECURSION 2);
GO
```

After the coding error is corrected, MAXRECURSION is no longer required.

### D. Use MERGE UNION

The following example uses the MERGE UNION query hint. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT *
FROM HumanResources.Employee AS e1
UNION
SELECT *
FROM HumanResources.Employee AS e2
OPTION (MERGE UNION);
GO
```

### E. Use HASH GROUP and FAST

The following example uses the HASH GROUP and FAST query hints. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT ProductID, OrderQty, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID, OrderQty
ORDER BY ProductID, OrderQty
OPTION (HASH GROUP, FAST 10);
GO
```

### F. Use MAXDOP

The following example uses the MAXDOP query hint. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT ProductID, OrderQty, SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID, OrderQty
ORDER BY ProductID, OrderQty
OPTION (MAXDOP 2);
GO
```

### G. Use INDEX

The following examples use the INDEX hint. The first example specifies a single index. The second example specifies multiple indexes for a single table reference. In both examples, because you apply the INDEX hint on a table that uses an alias, the TABLE HINT clause must also specify the same alias as the exposed object name. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_create_plan_guide
    @name = N'Guide1',
    @stmt = N'SELECT c.LastName, c.FirstName, e.Title
              FROM HumanResources.Employee AS e
              JOIN Person.Contact AS c ON e.ContactID = c.ContactID
              WHERE e.ManagerID = 2;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT(e, INDEX (IX_Employee_ManagerID)))';
GO
EXEC sp_create_plan_guide
    @name = N'Guide2',
    @stmt = N'SELECT c.LastName, c.FirstName, e.Title
              FROM HumanResources.Employee AS e
              JOIN Person.Contact AS c ON e.ContactID = c.ContactID
              WHERE e.ManagerID = 2;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT(e, INDEX(PK_Employee_EmployeeID, IX_Employee_ManagerID)))';
GO
```

### H. Use FORCESEEK

The following example uses the FORCESEEK table hint. The TABLE HINT clause must also specify the same two-part name as the exposed object name. Specify the name when you apply the INDEX hint on a table that uses a two-part name. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_create_plan_guide
    @name = N'Guide3',
    @stmt = N'SELECT c.LastName, c.FirstName, HumanResources.Employee.Title
              FROM HumanResources.Employee
              JOIN Person.Contact AS c ON HumanResources.Employee.ContactID = c.ContactID
              WHERE HumanResources.Employee.ManagerID = 3
              ORDER BY c.LastName, c.FirstName;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT( HumanResources.Employee, FORCESEEK))';
GO
```

### I. Use multiple table hints

The following example applies the INDEX hint to one table and the FORCESEEK hint to another. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_create_plan_guide
    @name = N'Guide4',
    @stmt = N'SELECT e.ManagerID, c.LastName, c.FirstName, e.Title
              FROM HumanResources.Employee AS e
              JOIN Person.Contact AS c ON e.ContactID = c.ContactID
              WHERE e.ManagerID = 3;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT (e, INDEX( IX_Employee_ManagerID))
                       , TABLE HINT (c, FORCESEEK))';
GO
```

### J. Use TABLE HINT to override an existing table hint

The following example shows how to use the TABLE HINT hint. You can use the hint without specifying a hint to override the INDEX table hint behavior you specify in the FROM clause of the query. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_create_plan_guide
    @name = N'Guide5',
    @stmt = N'SELECT e.ManagerID, c.LastName, c.FirstName, e.Title
              FROM HumanResources.Employee AS e WITH (INDEX (IX_Employee_ManagerID))
              JOIN Person.Contact AS c ON e.ContactID = c.ContactID
              WHERE e.ManagerID = 3;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT(e))';
GO
```

### K. Specify semantics-affecting table hints

The following example contains two table hints in the query: NOLOCK, which is semantic-affecting, and INDEX, which is non-semantic-affecting. To preserve the semantics of the query, the NOLOCK hint is specified in the OPTIONS clause of the plan guide. Along with the NOLOCK hint, specify the INDEX and FORCESEEK hints and replace the non-semantic-affecting INDEX hint in the query during statement compilation and optimization. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_create_plan_guide
    @name = N'Guide6',
    @stmt = N'SELECT c.LastName, c.FirstName, e.Title
              FROM HumanResources.Employee AS e
                   WITH (NOLOCK, INDEX (PK_Employee_EmployeeID))
              JOIN Person.Contact AS c ON e.ContactID = c.ContactID
              WHERE e.ManagerID = 3;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT (e, INDEX(IX_Employee_ManagerID), NOLOCK, FORCESEEK))';
GO
```

The following example shows an alternative method to preserving the semantics of the query and allowing the optimizer to choose an index other than the index specified in the table hint. Allow the optimizer to choose by specifying the NOLOCK hint in the OPTIONS clause. You specify the hint because it's semantic-affecting. Then, specify the TABLE HINT keyword with only a table reference and no INDEX hint. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_create_plan_guide
    @name = N'Guide7',
    @stmt = N'SELECT c.LastName, c.FirstName, e.Title
              FROM HumanResources.Employee AS e
                   WITH (NOLOCK, INDEX (PK_Employee_EmployeeID))
              JOIN Person.Contact AS c ON e.ContactID = c.ContactID
              WHERE e.ManagerID = 2;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (TABLE HINT (e, NOLOCK))';
GO
```

### <a id="l-using-use-hint"></a> L. Use USE HINT

The following example uses the RECOMPILE and USE HINT query hints. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT * FROM Person.Address
WHERE City = 'SEATTLE' AND PostalCode = 98104
OPTION (RECOMPILE, USE HINT ('ASSUME_MIN_SELECTIVITY_FOR_FILTER_ESTIMATES', 'DISABLE_PARAMETER_SNIFFING'));
GO
```

### M. Use QUERYTRACEON HINT

The following example uses the QUERYTRACEON query hints. The example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. You can enable all plan-affecting hotfixes controlled by trace flag 4199 for a particular query using the following query:

```sql
SELECT * FROM Person.Address
WHERE City = 'SEATTLE' AND PostalCode = 98104
OPTION (QUERYTRACEON 4199);
```

You can also use multiple trace flags as in the following query:

```sql
SELECT * FROM Person.Address
WHERE City = 'SEATTLE' AND PostalCode = 98104
OPTION  (QUERYTRACEON 4199, QUERYTRACEON 4137);
```

### N. Use Query Store hints

The [Query Store hints](../../relational-databases/performance/query-store-hints.md) feature in Azure SQL Database provides an easy-to-use method for shaping query plans without changing application code.

First, identify the query that has already been executed in the Query Store catalog views, for example:

```sql
SELECT q.query_id, qt.query_sql_text
FROM sys.query_store_query_text qt
INNER JOIN sys.query_store_query q ON
    qt.query_text_id = q.query_text_id
WHERE query_sql_text like N'%ORDER BY ListingPrice DESC%'
  AND query_sql_text not like N'%query_store%';
GO
```

The following example applies the hint to force the [legacy cardinality estimator](../../relational-databases/performance/cardinality-estimation-sql-server.md) to query_id 39, identified in Query Store:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(USE HINT(''FORCE_LEGACY_CARDINALITY_ESTIMATION''))';
```

The following example applies the hint to enforce a maximum memory grant size in PERCENT of configured memory limit to query_id 39, identified in Query Store:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(MAX_GRANT_PERCENT=10)';
```

The following example applies multiple query hints to query_id 39, including RECOMPILE, MAXDOP 1, and the SQL 2012 query optimizer behavior:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39,
    @query_hints = N'OPTION(RECOMPILE, MAXDOP 1, USE HINT(''QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_110''))';
```

## Next steps

- [Hints (Transact-SQL)](hints-transact-sql.md)
- [Query Store hints](../../relational-databases/performance/query-store-hints.md)
- [Plan guides](../../relational-databases/performance/plan-guides.md)
- [Trace Flags](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
