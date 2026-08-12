---
title: Cardinality Estimation Feedback for Expressions in SQL Server 2025
description: Describes the CE feedback for expressions feature in SQL Server 2025.
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 12/08/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
# CustomerIntent: As a database engineer, I want to understand the capabilities of the CE feedback for expressions feature in SQL Server 2025 so that I can effectively implement and support this technology.
monikerRange: "=azuresqldb-current || =sql-server-ver17 || =sql-server-linux-ver17 || =fabric-sqldb"
---

# Cardinality estimation (CE) feedback for expressions

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Inaccurate cardinality estimates often cause poor performance during query optimization. Cardinality estimation (CE) feedback for expressions extends the framework started by the CE feedback feature. The goal is to improve cardinality estimates for repeating expressions. The feedback for expressions feature learns from previous executions of expressions across queries, to find appropriate CE model choices and apply what has been learned to future executions of those expressions. Like [CE feedback](intelligent-query-processing-cardinality-estimation-feedback.md), model recommendations are tested and applied automatically to future query executions.

The feedback for expressions feature identifies and uses a [model](https://techcommunity.microsoft.com/blog/azuresqlblog/cardinality-estimation-feedback-explained-by-kate-smith-akatesmith/4197930) assumption that better fits a given query's expression and data distribution, which in turn improves query execution plan quality. Currently, the feedback for expressions feature can identify plan operators where the estimated number of rows and the actual number of rows are very different. Feedback is applied to expressions within a query when significant model estimation errors occur, and there's a viable alternate model to try.

Different versions of the Database Engine use [different CE](intelligent-query-processing-cardinality-estimation-feedback.md#cardinality-estimation-ce-feedback-implementation) model assumptions, based on how data is distributed and queried.

## Use CE feedback for expressions

CE Feedback for Expressions monitors query executions and identifies subexpressions that consistently result in cardinality misestimation. Feedback is generated based on observed patterns and applied during query compilation to improve estimation accuracy.

### Prerequisites and configuration

To use CE Feedback for Expressions, the following prerequisites must be met:

- The database must use **compatibility level 160 or later**.
- The `CE_FEEDBACK_FOR_EXPRESSIONS` database-scoped configuration must be enabled (enabled by default).
- To check the current status of the database scoped configuration:

```sql
SELECT name,
       value,
       value_for_secondary
FROM sys.database_scoped_configurations
WHERE name = 'CE_FEEDBACK_FOR_EXPRESSIONS';
```

The feature can be enabled on a database with the following database scoped configuration command:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET CE_FEEDBACK_FOR_EXPRESSIONS = ON;
```

To disable the feedback for expressions feature for a database, disable the `CE_FEEDBACK_FOR_EXPRESSIONS` database-scoped configuration:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET CE_FEEDBACK_FOR_EXPRESSIONS = OFF;
```

## How it works

While fingerprints aren't a new concept with the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)], a *fingerprint* in the context of the feedback for expressions feature refers to the combination of the computed signatures from within an expression. For example, a business analyst in a fictitious business might wish to obtain information about any of their customers' orders where those customers spent more than $10,000. A select statement that involves gathering data from a Customers table that is also joining to an Orders table might be one way to view this type of data:

```sql
SELECT *
FROM Customer AS C
     INNER JOIN Orders AS O
         ON C.custkey = O.o_custkey
WHERE O.o_totalprice > 10000;
```

For this query, the query optimizer might choose to get data from each table - `Customer`, followed by `Orders`, select all of the associated columns from both tables, and *join* the data (with a filter) where the `totalprice` of an order is greater than $10,000.
Each logical expression such as a filter or join within a query plan generates a signature that contributes to a fingerprint. CE Feedback for expressions uses these fingerprints to learn and apply feedback across queries that share similar subexpressions, even if the overall query structure is different.

The feature focuses on expressions with consistent cardinality overestimation/underestimation across queries. It analyzes two different workload patterns that are currently not eligible for CE feedback:

- Workloads without repeated executions, but with repeated expression patterns. For example, a commonly used join pattern.

- Queries in which one part of the query might benefit from a different CE model than another portion of the same query. For example, the join between tables `A` and `B` might require simple containment, and the join between tables `C` and `D`, which might require base containment.

The feedback for expressions feature applies filter and join assumptions to correct misestimation issues, such as:

Filters:

- `ASSUME_MIN_SELECTIVITY_FOR_FILTER_ESTIMATES`
- `ASSUME_PARTIAL_CORRELATION_FOR_FILTER_ESTIMATES`
- `ASSUME_FULL_INDEPENDENCE_FOR_FILTER_ESTIMATES`

Joins:

- `ASSUME_JOIN_PREDICATE_DEPENDS_ON_FILTERS`
- Base containment assumption (no hint should need to be passed)

These assumptions reflect different CE model strategies, such as containment and independence. For more conceptual background, see [Cardinality Estimation Feedback Explained by Kate Smith](https://techcommunity.microsoft.com/blog/azuresqlblog/cardinality-estimation-feedback-explained-by-kate-smith-akatesmith/4197930) and [Cardinality Estimation for Correlated Columns in SQL Server 2016](https://techcommunity.microsoft.com/blog/sqlserver/cardinality-estimation-for-correlated-columns-in-sql-server-2016/384467).

### Hint lifecycle

Feedback hints progress through the following states:

- Monitoring: The system observes repeated executions of a subexpression and tracks whether cardinality misestimation persists.
- Applying: If misestimation continues, a feedback hint could be generated and applied during query compilation to adjust the CE model.
- Blocked: If the applied hint results in a suboptimal cardinality estimate, it's blocked from future use.

This lifecycle ensures that feedback is only applied when beneficial and avoids regression in estimation quality.

### Regression protection

CE feedback for expressions includes regression protection. If a hint causes a worse cardinality estimate than before, it's blocked. However, this protection is limited to cardinality estimation and doesn't evaluate query execution time. For execution/runtime related regressions, [automatic plan correction](../automatic-tuning/automatic-tuning.md#automatic-plan-correction) might intervene. If the automatic plan correction feature isn't enabled, actions that the feature would take are recorded and available by querying the [sys.dm_db_tuning_recommendations](../system-dynamic-management-objects/sys-dm-db-tuning-recommendations-transact-sql.md#example-2)
dynamic management view.

## Telemetry and monitoring

CE Feedback for Expressions activity can be monitored using the following tools:

- Extended events:
  - `adhoc_ce_feedback_query_level_telemetry`
  - `query_adhoc_ce_feedback_expression_hint`
  - `query_adhoc_ce_feedback_hint`

The CE Feedback extended events `query_ce_feedback_begin_analysis` and `query_ce_feedback_telemetry` can also be useful while tracking the activity of the feature.

- Fingerprint data is cached in a dedicated memory clerk named `AdHocCEFeedbackCache`. This cache can be accessed via the system catalog view `sys.dm_exec_ce_feedback_cache`.

- Showplan integration

  When a CE feedback for expressions hint is applied, the query plan includes a `CardinalityFeedback` attribute in the Showplan XML. This tag indicates that feedback was used to adjust the cardinality estimate for a specific subexpression.

<a id="caching-and-persistence"></a>

## Cache and persistence

Persisted feedback is stored in an internal Query Store table (`sys.plan_persist_ce_feedback_for_expressions`) and reloaded on startup. This ensures the system doesn't need to relearn feedback for fingerprints it has already encountered. The cache persistence mechanism is lossy in nature, meaning feedback is only persisted to disk periodically. The frequency of persistence isn't currently configurable.

If the SQL Server instance restarts or memory is cleared before the next persistence cycle, feedback generated since the last flush can be lost.

## Limitations

Persistence isn't currently available for Query Store on readable secondaries. CE feedback for expressions can apply feedback differently on a primary replica and on a secondary replica. However, the feedback isn't persisted on secondary replicas and only exists within the memory based cache in that scenario. If a failover event occurs, the feedback that had been learned on any of the readable secondaries is lost.

## Related content

- [Cardinality estimation (CE) feedback](intelligent-query-processing-cardinality-estimation-feedback.md)
