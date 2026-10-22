---
title: Optional Parameter Plan Optimization
description: Optional parameter plan optimization improvement.
author: thesqlsith
ms.author: derekw
ms.reviewer: vanto
ms.date: 01/07/2026
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
# CustomerIntent: As a database engineer, I want to understand the capabilities of the Optional parameter plan optimization feature in SQL Server 2025 so that I can effectively implement and support this technology.
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---

# Optional parameter plan optimization (OPPO)

[!INCLUDE [sqlserver2025-asdb-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-fabricsqldb.md)]

Optional parameter plan optimization (OPPO) improves query plan quality for queries that include optional parameters. In these queries, the optimal execution plan depends on whether a parameter value is `NULL` at execution time. The term *optional parameters* refers to a specific variation of the [parameter-sensitive plan](../query-processing-architecture-guide.md#parameter-sensitivity) (PSP) problem, in which the parameter value at execution time determines whether the query requires a seek or a scan.

## Overview

Queries that use optional parameters often include predicates that conditionally apply filters based on whether a parameter value is provided. A common pattern is as follows:

```sql
SELECT column1,
       column2
FROM Table1
WHERE (column1 = @p
       OR @p IS NULL);
```

When `@p IS NOT NULL`, an index seek on `col1` is often the most efficient execution plan. When `@p IS NULL`, the predicate evaluates to `TRUE`, and a scan might be more appropriate. Without OPPO, the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] must compile and cache a single execution plan that's valid for both cases. Because a seek-based plan isn't valid when `@p IS NULL`, the optimizer often chooses a conservative scan-based plan for all executions. This choice can result in inefficient plan choices and excessive resource usage for selective executions.

Traditional hinting techniques such as `OPTIMIZE FOR` aren't effective in this scenario, because the plan must remain correct for both parameter states.

OPPO uses the adaptive plan optimization (Multiplan) infrastructure introduced with Parameter Sensitive Plan (PSP) optimization. This infrastructure generates and caches multiple execution plans for a single statement, which allows OPPO to make different assumptions based on the parameter values used in the query.

## Terminology and how it works

OPPO builds on the adaptive plan optimization (Multiplan) framework, which is also used by [Parameter Sensitive Plan optimization](parameter-sensitive-plan-optimization.md). By using Multiplan, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] can generate and cache multiple execution plans for a single query.

When the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] detects an eligible optional parameter pattern, it creates:

- A dispatcher plan
- One or more query variants, each optimized for a specific parameter value state

At execution time:

- The [!INCLUDE [ssde-md](../../includes/ssde-md.md)] evaluates the parameter value.
- The Multiplan dispatcher selects the appropriate query variant.
- The selected query variant executes.

After the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] selects a query variant, it simplifies predicates based on the actual parameter value. Consider the following expression:

```sql
@p1 IS NULL
```

In this example, the expression is simplified to a [constant result](../query-processing-architecture-guide.md#constant-folding-and-expression-evaluation) for the selected variant. This constant result folding allows the optimizer to generate execution plans that aren't valid in a single reusable plan.

By selecting plans in this way, OPPO enables efficient execution for different parameter states without requiring query rewrites or manual query hints.

OPPO and PSP optimization address different variations of parameter-related plan issues:

- PSP optimization selects plans based on estimated cardinality differences for equality or range predicates.

- OPPO selects plans based on whether a parameter value is `NULL`.

A single query might benefit from both or either feature depending on the predicates involved.

### Supported query patterns

Optional parameter plan optimization applies to queries where `NULL` checks on parameters affect execution plan validity. As an example, consider an application web form for a realty company that allows for optional filtering on the number of bedrooms for a particular listing. OPPO applies to disjunctive optional parameter predicates such as:

```sql
SELECT *
FROM Properties
WHERE bedrooms = @bedrooms
      OR @bedrooms IS NULL;
```

Even if [parameter markers](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse) can sniff the `@bedrooms = 10` parameter, and you know that the cardinality for the number of bedrooms is likely to be very low, the optimizer doesn't produce a plan that seeks on an index that exists on the bedroom column, because that isn't a valid plan for the case where `@bedrooms` is `NULL`. The generated plan doesn't include a scan of the index.

Imagine if you could rewrite this query as two separate statements. Depending on the runtime value of the parameter, you could evaluate the following example:

```sql
IF @bedrooms IS NULL
    SELECT *
    FROM Properties;
ELSE
    SELECT *
    FROM Properties
    WHERE bedrooms = @bedrooms;
```

The feature can achieve this by using the Multiplan infrastructure, which allows a creation of a [dispatcher plan](parameter-sensitive-plan-optimization.md#dispatcher-plan) that dispatches a [query variant](parameter-sensitive-plan-optimization.md#query-variant).

OPPO embeds a system-generated `PLAN PER VALUE` query hint (`optional_predicate`) in the plan metadata to associate each query variant with its parameter state. This hint is system generated and embedded within the query text of the plan. This hint isn't valid for use by an application or to be applied manually.

Continuing with the previous example,

```sql
SELECT *
FROM Properties
WHERE bedrooms = @bedrooms
      OR @bedrooms IS NULL;
```

OPPO can generate two query variants that might have the following attributes added to them within the Showplan XML:

- `@bedrooms` is `NULL`. The query variant *folds* predicates based on the parameter value, allowing a scan-based plan to be generated.

  SELECT * FROM Properties PLAN PER VALUE(ObjectID = 1234, QueryVariantID = *1*, *optional_predicate*(@bedrooms is NULL))

- `@bedrooms IS NOT NULL`

  SELECT * FROM Properties WHERE bedrooms = @bedrooms PLAN PER VALUE(ObjectID = 1234, QueryVariantID = *2*, *optional_predicate*(@bedrooms is NULL))

## Use optional parameter plan optimization

To enable OPPO for a database, the following prerequisites are required:

- The database must use compatibility level 170.
- The `OPTIONAL_PARAMETER_OPTIMIZATION` database-scoped configuration must be enabled.

The `OPTIONAL_PARAMETER_OPTIMIZATION` database-scoped configuration is enabled by default, so a database using compatibility level 170 (the default in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]) uses OPPO by default.

You can ensure that a database uses OPPO in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] by executing the following statements:

```sql
ALTER DATABASE [<database-name-placeholder>]
SET COMPATIBILITY_LEVEL = 170;

ALTER DATABASE SCOPED CONFIGURATION
SET OPTIONAL_PARAMETER_OPTIMIZATION = ON;
```

To disable OPPO for a database, disable the `OPTIONAL_PARAMETER_OPTIMIZATION` database-scoped configuration:

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET OPTIONAL_PARAMETER_OPTIMIZATION = OFF;
```

### Use optional parameter plan optimization via query hinting

Use the `DISABLE_OPTIONAL_PARAMETER_OPTIMIZATION` query hint to disable OPPO for a given query. Specify the hint via the `USE HINT` clause. For more information, see [Query hints](../../t-sql/queries/hints-transact-sql-query.md#use_hint).

This hint works under any compatibility level, and overrides the `OPTIONAL_PARAMETER_OPTIMIZATION` database-scoped configuration.

Specify the `DISABLE_OPTIONAL_PARAMETER_OPTIMIZATION` query hint directly in the query or via [Query Store hints](query-store-hints.md).

### Extended Events

Use the following extended events for troubleshooting and diagnostics. These events are **not** required to use the feature.

- `optional_parameter_optimization_skipped_reason`: Occurs when OPPO decides that a query isn't eligible for optimization. This extended event follows the same pattern as the `parameter_sensitive_plan_optimization_skipped_reason` event that PSP optimization uses. Since a query can generate both PSP optimization and OPPO query variants, check both events to understand why one or neither feature engaged.

  The following query shows all of the possible reasons why PSP was skipped:

  ```sql
  SELECT map_value
  FROM sys.dm_xe_map_values
  WHERE [name] = 'opo_skipped_reason_enum'
  ORDER BY map_key;
  ```

- `query_with_optional_parameter_predicate`: This extended event follows the same pattern as the `query_with_parameter_sensitivity` event that PSP optimization uses. It includes the additional fields that are available in the improvements for PSP optimization.

  These fields display:

  - the number of predicates that the feature found interesting,
  - more details in JSON format regarding the interesting predicates, and
  - whether OPPO is supported for the predicate or predicates.

## Remarks

- The ShowPlan XML for a query variant looks similar to the following example. The predicates that the feature selects have their respective information added to the `PLAN PER VALUE` (`optional_predicate`) hint.

```xml
<Batch>
  <Statements>
    <StmtSimple StatementCompId="4" StatementEstRows="1989" StatementId="1" StatementOptmLevel="FULL" StatementOptmEarlyAbortReason="GoodEnoughPlanFound" CardinalityEstimationModelVersion="170" StatementSubTreeCost="0.0563916" StatementText="SELECT PropertyId, AgentId, ListingPrice, ZipCode, SquareFootage, &#xD;&#xA;           Bedrooms, Bathrooms, ListingDescription&#xD;&#xA;    FROM dbo.Property &#xD;&#xA;    WHERE (@AgentId IS NULL OR AgentId = @AgentId)&#xD;&#xA;      AND (@ZipCode IS NULL OR ZipCode = @ZipCode)&#xD;&#xA;      AND (@MinPrice IS NULL OR ListingPrice &gt;= @MinPrice)&#xD;&#xA;      AND (@HasDescription IS NULL OR &#xD;&#xA;           (@HasDescription = 1 AND ListingDescription IS NOT NULL) OR&#xD;&#xA;           (@HasDescription = 0 AND ListingDescription IS NULL)) option (PLAN PER VALUE(ObjectID = 1269579561, QueryVariantID = 7, optional_predicate(@MinPrice IS NULL),optional_predicate(@ZipCode IS NULL),optional_predicate(@AgentId IS NULL)))" StatementType="SELECT" QueryHash="0x2F701925D1202A9F" QueryPlanHash="0xBA0B2B1A18AF1033" RetrievedFromCache="true" StatementSqlHandle="0x09000033F4BE101B2EE46B1615A038D422710000000000000000000000000000000000000000000000000000" DatabaseContextSettingsId="1" ParentObjectId="1269579561" StatementParameterizationType="1" SecurityPolicyApplied="false">
      <StatementSetOptions ANSI_NULLS="true" ANSI_PADDING="true" ANSI_WARNINGS="true" ARITHABORT="true" CONCAT_NULL_YIELDS_NULL="true" NUMERIC_ROUNDABORT="false" QUOTED_IDENTIFIER="true" />
      <Dispatcher>
        <OptionalParameterPredicate>
          <Predicate>
            <ScalarOperator ScalarString="[@MinPrice] IS NULL">
              <Compare CompareOp="IS">
                <ScalarOperator>
                  <Identifier>
                    <ColumnReference Column="@MinPrice" />
                  </Identifier>
                </ScalarOperator>
                <ScalarOperator>
                  <Const ConstValue="NULL" />
                </ScalarOperator>
              </Compare>
            </ScalarOperator>
          </Predicate>
        </OptionalParameterPredicate>
        <OptionalParameterPredicate>
          <Predicate>
            <ScalarOperator ScalarString="[@ZipCode] IS NULL">
              <Compare CompareOp="IS">
                <ScalarOperator>
                  <Identifier>
                    <ColumnReference Column="@ZipCode" />
                  </Identifier>
                </ScalarOperator>
                <ScalarOperator>
                  <Const ConstValue="NULL" />
                </ScalarOperator>
              </Compare>
            </ScalarOperator>
          </Predicate>
        </OptionalParameterPredicate>
        <OptionalParameterPredicate>
          <Predicate>
            <ScalarOperator ScalarString="[@AgentId] IS NULL">
              <Compare CompareOp="IS">
                <ScalarOperator>
                  <Identifier>
                    <ColumnReference Column="@AgentId" />
                  </Identifier>
                </ScalarOperator>
                <ScalarOperator>
                  <Const ConstValue="NULL" />
                </ScalarOperator>
              </Compare>
            </ScalarOperator>
          </Predicate>
        </OptionalParameterPredicate>
      </Dispatcher>
      <QueryPlan DegreeOfParallelism="1" CachedPlanSize="40" CompileTime="1" CompileCPU="1" CompileMemory="376" QueryVariantID="7">
```

- Example output from the `query_with_optional_parameter_predicate` extended event:

  | Field | Value |
  | --- | --- |
  | `optional_parameter_optimization_supported` | True |
  | `optional_parameter_predicate_count` | 3 |
  | `predicate_details` | `{"Predicates":[{"Skewness":1005.53},{"Skewness":1989.00},{"Skewness":1989.00}]}` |
  | `query_type` | 193 |

## Query eligibility and limitations

OPPO applies only to queries that are eligible for Multiplan optimization. The feature isn't applied in scenarios that include:

- Queries that use local variables instead of parameters
- Queries compiled with `OPTION (RECOMPILE)`
- Queries executed with `SET ANSI_NULLS OFF`
- Auto-parameterized statements

## Related content

- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Recompiling execution plans](../query-processing-architecture-guide.md#recompiling-execution-plans)
- [Parameters and execution plan reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse)
- [Simple parameterization](../query-processing-architecture-guide.md#simple-parameterization)
- [Forced parameterization](../query-processing-architecture-guide.md#forced-parameterization)
- [Query hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [Intelligent query processing in SQL databases](intelligent-query-processing.md)
- [Parameter Sensitivity](../query-processing-architecture-guide.md#parameter-sensitivity)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
