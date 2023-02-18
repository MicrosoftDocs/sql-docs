---
title: Parameter Sensitive Plan optimization
description: Learn about Parameter Sensitive Plan Optimization in the Query Store.
author: thesqlsith
ms.author: derekw
ms.reviewer: maghan, randolphwest
ms.date: 12/12/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "Query Store"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---

# Parameter Sensitive Plan optimization

[!INCLUDE[sqlserver2022-and-later](../../includes/applies-to-version/sqlserver2022-and-later.md)]

Parameter Sensitive Plan (PSP) optimization is part of the [Intelligent query processing](intelligent-query-processing.md) family of features. It addresses the scenario where a single cached plan for a parameterized query isn't optimal for all possible incoming parameter values. This is the case with non-uniform data distributions. For more information, see [Parameter Sensitivity](../query-processing-architecture-guide.md#parameter-sensitivity) and [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse).

For more information on existing workarounds for this problem scenario, see:

- [Investigate and resolve parameter-sensitive issues](/troubleshoot/sql/performance/troubleshoot-high-cpu-usage-issues#step-5-investigate-and-resolve-parameter-sensitive-issues)
- [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse)
- [Queries that have parameter sensitive plan (PSP) problems](/azure/azure-sql/database/identify-query-performance-issues#parameter-sensitivity)

PSP optimization automatically enables multiple, active cached plans for a single parameterized statement. Cached execution plans will accommodate different data sizes based on the customer-provided runtime parameter value(s).

## Understand parameterization

In the [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] [!INCLUDE[ssde-md](../../includes/ssde-md.md)], using parameters or parameter markers in Transact-SQL (T-SQL) statements increases the ability of the relational engine to match new T-SQL statements with existing, previously compiled execution plans and promote plan reutilization. For more information, see [Simple Parameterization](../query-processing-architecture-guide.md#simple-parameterization).

You can also override the default simple parameterization behavior of [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] by specifying that all `SELECT`, `INSERT`, `UPDATE`, and `DELETE` statements in a database are parameterized, subject to certain limitations. For more information, see [Forced Parameterization](../query-processing-architecture-guide.md#forced-parameterization).

## PSP optimization implementation

During the initial compilation, column statistics histograms identify non-uniform distributions and evaluate the most *at-risk* parameterized predicates, up to three out of all available predicates. In other words, if multiple predicates within the same query meet the criteria, PSP optimization chooses the top three. The PSP feature limits the number of predicates that are evaluated, in order to avoid bloating the plan cache and the Query Store (if Query Store is enabled) with too many plans.

For eligible plans, the initial compilation produces a *dispatcher plan* that contains the PSP optimization logic called a *dispatcher expression*. A dispatcher plan maps to *query variants* based on the cardinality range boundary values predicates.

### Terminology

#### Dispatcher expression

Evaluates cardinality of predicates based on runtime parameter values and route execution to different query variants.

#### Dispatcher plan

A plan containing the dispatcher expression is cached for the original query. The dispatcher plan is essentially a collection of the predicates that were selected by the feature with a few extra details. For each predicate that is selected some of the details that are included in the dispatcher plan are the *high* and *low* boundary values. These values are used to divide parameter values into different *buckets* or ranges. The dispatcher plan also contains the statistics that were used to calculate the boundary values.

#### Query variant

As a dispatcher plan evaluates the cardinality of predicates based on runtime parameter values, it *bucketizes* those values, and generates separate child queries to compile and execute. These child queries are called query variants. Query variants have their own plans in the plan cache and Query Store.

#### Predicate cardinality range

At runtime, the cardinality of each predicate is evaluated based on runtime parameter values. The dispatcher will bucketize the cardinality values into three predicate cardinality ranges at compile time. For example, the PSP optimization feature can create three ranges that would represent low, medium, and high cardinality ranges, as shown in the following diagram.

:::image type="content" source="media/parameter-sensitivity-plan-optimization/parameter-sensitive-plan-boundaries.png" alt-text="Diagram showing the Parameter Sensitive Plan boundaries.":::

In other words, when a parameterized query is initially compiled, the PSP optimization feature will generate a shell plan known as a dispatcher plan. The dispatcher expression has the logic that bucketizes queries into query variants based on the runtime values of parameters. When actual execution begins, the dispatcher performs two steps:

- the dispatcher evaluates its dispatcher expression for the given set of parameters to compute the cardinality range.

- the dispatcher maps these ranges to specific query variants and compiles and executes the variants. By virtue of having multiple query variants, the PSP optimization feature achieves having multiple plans for a single query.

The cardinality range boundaries can be seen within the ShowPlan XML of a dispatch plan:

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

You will also find a PSP optimization generated hint that is appended to the SQL statement in the ShowPlan XML of a query variant. The hint can't be used directly and **will not** be able to be parsed if added manually. The hint contains the following elements:

> option ( PLAN PER VALUE ( **ObjectID** = (int), **QueryVariantID** = (int), **predicate_range** ( [databaseName].[schemaName].[tableName].[columnName] = @paramName, lowBoundaryValue, highBoundaryValue ) ) )

- **ObjectID** comes from the module (that is, stored procedure, function, trigger) which the current statement is part of; with the assumption that the statement has been generated from a module. If the statement is the result of dynamic or ad-hoc SQL (that is, `sp_executesql`) the ObjectID element will equal 0.
- **QueryVariantID** is roughly equivalent to the combination of ranges for all of the predicates that PSP optimization selected. As an example, if a query has two predicates that are eligible for PSP and each predicate has three ranges, there will be nine query variant ranges numbered 1-9.
- **predicate range** is the predicated cardinality range information generated from the dispatcher expression.

And, within the ShowPlan XML of a query variant (inside of the Dispatcher element):

```xml
<Batch>
  <Statements>
    <StmtSimple StatementText="SELECT PropertyId,&#xD;&#xA;       AgentId,&#xD;&#xA;       MLSLinkId,&#xD;&#xA;       ListingPrice,&#xD;&#xA;       ZipCode,&#xD;&#xA;       Bedrooms,&#xD;&#xA;       Bathrooms&#xD;&#xA;FROM dbo.Property&#xD;&#xA;WHERE AgentId = @AgentId&#xD;&#xA;ORDER BY ListingPrice DESC option (PLAN PER VALUE(ObjectID = 613577224, QueryVariantID = 1, predicate_range([PropertyMLS].[dbo].[Property].[AgentId] = @AgentId, 100.0, 1000000.0)))" StatementId="1" StatementCompId="2" StatementType="SELECT" StatementSqlHandle="0x090085E4372DFC69BB9E7EBA421561DE8E1E0000000000000000000000000000000000000000000000000000" DatabaseContextSettingsId="1" ParentObjectId="0" StatementParameterizationType="1" RetrievedFromCache="false" StatementSubTreeCost="0.021738" StatementEstRows="3" SecurityPolicyApplied="false" StatementOptmLevel="FULL" QueryHash="0x476167A000F589CD" QueryPlanHash="0xDE982107B7C28AAE" StatementOptmEarlyAbortReason="GoodEnoughPlanFound" CardinalityEstimationModelVersion="160">
      <StatementSetOptions QUOTED_IDENTIFIER="true" ARITHABORT="true" CONCAT_NULL_YIELDS_NULL="true" ANSI_NULLS="true" ANSI_PADDING="true" ANSI_WARNINGS="true" NUMERIC_ROUNDABORT="false" />

      <Dispatcher>
        <ParameterSensitivePredicate LowBoundary="100" HighBoundary="1e+06">
          <StatisticsInfo LastUpdate="2019-09-07T15:32:16.89" ModificationCount="0" SamplingPercent="100" Statistics="[NCI_Property_AgentId]" Table="[Property]" Schema="[dbo]" Database="[PropertyMLS]" />
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

    </StmtSimple>
  </Statements>
</Batch>
```

## Remarks

Dispatcher plans are automatically rebuilt if there are significant data distribution changes. Query variant plans will recompile independently as needed, as with any other query plan type, subject to default recompilation events. For more information about recompilation, see [Recompiling Execution Plans](../query-processing-architecture-guide.md#recompiling-execution-plans).

The [sys.query_store_plan (Transact-SQL)](../system-catalog-views/sys-query-store-plan-transact-sql.md#sysquery_store_plan-transact-sql) Query Store system catalog view has been changed to differentiate between a normal compiled plan, a dispatcher plan, and a query variant plan. The new Query Store system catalog view, [sys.query_store_query_variant (Transact-SQL)](../system-catalog-views/sys-query-store-query-variant.md#sysquery_store_query_variant-transact-sql), contains information about the parent-child relationships between the original parameterized queries (also known as parent queries), dispatcher plans, and their child query variants.

The PSP optimization feature currently only works with equality predicates.

When there are multiple predicates that are part of the same table, PSP optimization will select the predicate that has the most data skew based on the underlying statistics histogram. For example, with `SELECT * FROM table WHERE column1 = @predicate1 AND column2 = @predicate2`, because both `column1 = @predicate1` and `column2 = @predicate2` are from the same table, this will be `table1`. However, if the example query involves an operator such as a UNION, PSP will evaluate more than one predicate. As an example, if a query has characteristics similar to `SELECT * FROM table WHERE column1 = @predicate UNION SELECT * FROM table WHERE column1 = @predicate`, PSP will pick at most two predicates in this case, because the system treats this scenario as if they're two different tables. The same behavior can be observed from queries that self join via table aliases.

The ShowPlan XML for a query variant would look similar to the following example, where both predicates that were selected have their respective information added to the PLAN PER VALUE PSP-related hint.

```xml
    <Batch>
      <Statements>
        <StmtSimple StatementText="SELECT  b.PropertyId, &#xD;&#xA;       AgentId, &#xD;&#xA;       MLSLinkId, &#xD;&#xA;       ListingPrice, &#xD;&#xA;       ZipCode, &#xD;&#xA;       Bedrooms, &#xD;&#xA;       Bathrooms &#xD;&#xA;FROM dbo.AgentProperty a join  PropertyDetails b on a.PropertyId = b.PropertyId&#xD;&#xA;WHERE AgentId = @AgentId and  Property_id=@Property_id&#xD;&#xA;UNION&#xD;&#xA;          SELECT  c.PropertyId, &#xD;&#xA;       AgentId, &#xD;&#xA;       MLSLinkId, &#xD;&#xA;       ListingPrice, &#xD;&#xA;       ZipCode, &#xD;&#xA;       Bedrooms, &#xD;&#xA;       Bathrooms &#xD;&#xA;FROM dbo.AgentProperty c join  PropertyDetails d on c.PropertyId = d.PropertyId&#xD;&#xA;WHERE AgentId = @AgentId and  Property_id=@Property_id option (PLAN PER VALUE(ObjectID = 981578535, QueryVariantID = 9, predicate_range([PropertyMLS].[dbo].[AgentProperty].[AgentId] = @AgentId, 100.0, 1000000.0),predicate_range([PropertyMLS].[dbo].[AgentProperty].[AgentId] = @AgentId, 100.0, 1000000.0)))" StatementId="1" StatementCompId="2" StatementType="SELECT" StatementSqlHandle="0x090051FBCD918F8DFD60D324887356B422D90000000000000000000000000000000000000000000000000000" DatabaseContextSettingsId="2" ParentObjectId="0" StatementParameterizationType="1" RetrievedFromCache="false" StatementSubTreeCost="29.2419" StatementEstRows="211837" SecurityPolicyApplied="false" StatementOptmLevel="FULL" QueryHash="0x6D2A4E407085C01E" QueryPlanHash="0x72101C0A0DD861AB" CardinalityEstimationModelVersion="160" BatchModeOnRowStoreUsed="true">
          <StatementSetOptions QUOTED_IDENTIFIER="true" ARITHABORT="true" CONCAT_NULL_YIELDS_NULL="true" ANSI_NULLS="true" ANSI_PADDING="true" ANSI_WARNINGS="true" NUMERIC_ROUNDABORT="false" />
          <Dispatcher>
            <ParameterSensitivePredicate LowBoundary="100" HighBoundary="1e+06">
              <StatisticsInfo LastUpdate="2022-08-11T20:42:35.02" ModificationCount="0" SamplingPercent="100" Statistics="[NCI_AgentProperty_AgentId]" Table="[AgentProperty]" Schema="[dbo]" Database="[PropertyMLS]" />
              <Predicate>
                <ScalarOperator ScalarString="[PropertyMLS].[dbo].[AgentProperty].[AgentId] as [a].[AgentId]=[@AgentId]">
                  <Compare CompareOp="EQ">
                    <ScalarOperator>
                      <Identifier>
                        <ColumnReference Database="[PropertyMLS]" Schema="[dbo]" Table="[AgentProperty]" Alias="[a]" Column="AgentId" />
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
            <ParameterSensitivePredicate LowBoundary="100" HighBoundary="1e+06">
              <StatisticsInfo LastUpdate="2022-08-11T20:42:35.02" ModificationCount="0" SamplingPercent="100" Statistics="[NCI_AgentProperty_AgentId]" Table="[AgentProperty]" Schema="[dbo]" Database="[PropertyMLS]" />
              <Predicate>
                <ScalarOperator ScalarString="[PropertyMLS].[dbo].[AgentProperty].[AgentId] as [c].[AgentId]=[@AgentId]">
                  <Compare CompareOp="EQ">
                    <ScalarOperator>
                      <Identifier>
                        <ColumnReference Database="[PropertyMLS]" Schema="[dbo]" Table="[AgentProperty]" Alias="[c]" Column="AgentId" />
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
          <QueryPlan CachedPlanSize="160" CompileTime="17" CompileCPU="17" CompileMemory="1856" QueryVariantID="9">
```

## Considerations

- To enable PSP optimization, enable database compatibility level 160 for the database you're connected to when executing the query.

- For additional insights into the PSP optimization feature, we recommend that Query Store integration is enabled, by turning on the Query Store. The following example will turn on the Query Store for a pre-existing database called `MyNewDatabase`:

```sql
ALTER DATABASE MyNewDatabase SET QUERY_STORE = ON (OPERATION_MODE = READ_WRITE, QUERY_CAPTURE_MODE = AUTO);
```

> [!NOTE]  
> Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], Query Store is now enabled by default for all newly created databases.

- To disable PSP optimization at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SENSITIVE_PLAN_OPTIMIZATION = OFF` database scoped configuration.

- `Dispatcher` element shows details of the predicate boundaries as derived from a statistics histogram.

```xml
<Dispatcher>
  <ParameterSensitivePredicate LowBoundary="100" HighBoundary="1000000">
    <StatisticsInfo Database="[PropertyMLS]" Schema="[dbo]" Table="[Property]" Statistics="[NCI_Property_AgentId]" ModificationCount="0" SamplingPercent="100" LastUpdate="2019-09-07T15:32:16.89"/>
    <Predicate>
      <ScalarOperator ScalarString="[PropertyMLS].[dbo].[Property].[AgentId]=[@AgentId]">
        <Compare CompareOp="EQ">
          <ScalarOperator>
            <Identifier>
              <ColumnReference Database="[PropertyMLS]" Schema="[dbo]" Table="[Property]" Column="AgentId"/>
            </Identifier>
          </ScalarOperator>
          <ScalarOperator>
            <Identifier>
              <ColumnReference Column="@AgentId"/>
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

- To disable PSP optimization at the query level, use the `DISABLE_PARAMETER_SENSITIVE_PLAN_OPTIMIZATION` query hint.

- If parameter sniffing is disabled by trace flag 4136, `PARAMETER_SNIFFING` database scoped configuration, or the `USE HINT('DISABLE_PARAMETER_SNIFFING')` query hint, PSP optimization will be disabled for the associated workloads and execution contexts. For more information, see [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md) and [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

- The number of unique plan variants per dispatcher stored in the plan cache is limited to avoid cache bloating. The internal threshold isn't documented. Since each SQL batch has the potential of creating multiple plans and each query variant plan has an independent entry in the plan cache, it is possible that the default maximum number of plan entries allow can be reached. If the plan cache eviction rate is observably high, or the sizes of the CACHESTORE_OBJCP and CACHESTORE_SQLCP [cache stores](/previous-versions/tn-archive/cc293624(v=technet.10)) are excessive, you should consider applying Trace Flag [174](https://support.microsoft.com/help/3026083).

- The number of unique plan variants stored for a query in the Query Store store is limited by the `max_plans_per_query` configuration option. As query variants can have more than one plan, a total of 200 plans can be present per query within the Query Store, and this number will include all query variant plans for all the dispatchers that belong to a parent query. Consider increasing the `max_plans_per_query` Query Store configuration option.

Let's assume that a particular Query Store exhibits the following behavior: you have Query ID 10, which has two dispatcher plans, and the dispatcher plans have 20 query variants each (40 query variants in total). The total number of plans for Query ID 10 will be 40 plans for the query variants and the two dispatcher plans. It is also possible that the parent Query ID 10 has 5 regular (non-dispatcher) plans. This will make 47 plans (40 from query variants, 2 dispatcher, and 5 non-PSP related plan). Further, if each query variant also has an average of five plans, it is possible in this scenario to have more than 200 plans in the Query Store for a parent query. This would also depend upon heavy data skew in the dataset(s) that this example parent query may be referencing.

For each query variant mapping to a given dispatcher:

- The `query_plan_hash` is unique. This column is available in `sys.dm_exec_query_stats`, and other dynamic management views and catalog tables.
- The `plan_handle` is unique. This column is available in `sys.dm_exec_query_stats`, `sys.dm_exec_sql_text`, `sys.dm_exec_cached_plans`, and in other Dynamic Management Views and Functions, and catalog tables.
- The `query_hash` is common to other variants mapping to the same dispatcher, so it's possible to determine aggregate resource usage for queries that differ only by input parameter values. This column is available in `sys.dm_exec_query_stats`, `sys.query_store_query`, and other Dynamic Management Views and catalog tables.
- The `sql_handle` is unique due to special PSP optimization identifiers being added to the query text during compilation. This column is available in `sys.dm_exec_query_stats`, `sys.dm_exec_sql_text`, `sys.dm_exec_cached_plans`, and in other Dynamic Management Views and Functions, and catalog tables. The same handle information is available in the Query Store as the `last_compile_batch_sql_handle` column in the `sys.query_store_query` catalog table.
- The `query_id` is unique in the Query Store. This column is available in `sys.query_store_query`, and other Query Store catalog tables.

### Plan forcing in Query Store

Uses the same [sp_query_store_force_plan](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md) and [sp_query_store_unforce_plan](../system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md) stored procedures to operate on dispatcher or variant plans.

If a variant is forced, the parent dispatcher isn't forced.
If a dispatcher is forced, only variants from that dispatcher are considered eligible for use:

- Previously forced variants from other dispatchers will become inactive but retain the *forced* status until their dispatcher is forced again
- Previously forced variants in the same dispatcher that had become inactive are forced again

### Query Store query hint behavior

- When a Query Store hint is added to a query variant (child query), the hint will be applied in the same manner as a non-PSP query. Query variant hints do have higher precedence if a hint has also been applied to the parent query in Query Store.

- When a Query Store hint is added to the parent query and the child query (query variant) doesn't have an existing Query Store hint, the hint from the parent query will be inherited by the child query (query variant).

- If a Query Store query hint is removed from the parent query, the child queries (query variants) will also have the hint removed.
- If a RECOMPILE hint is added to the parent query, the system will generate non-PSP plans after any existing query variant plans have been removed from the plan cache, as the PSP feature doesn't operate on queries that have a RECOMPILE hint.
- Query Store hint results can be observed using the Extended Events `query_store_hints_application_success` and `query_store_hints_application_failed` events. For the [sys.query_store_query_hints](../system-catalog-views/sys-query-store-query-hints-transact-sql.md#sysquery_store_query_hints-transact-sql) table, it will contain information regarding the query hint that has been applied. If the hint has only been applied on a parent query, the system catalog will contain the hint information for the parent query, but not for its child queries although the child queries will inherit the parent query's' hint.

PSP with query hints and plan forcing behavior can be summarized in the table below:

| Query variant hint or plan | Parent has user-applied hint | Parent has feedback-applied hint | Parent has manual forced plan | Parent has APRC <sup>1</sup> forced plan |
| --- | :---: | :---: | :---: | :---: |
| **Hint via user** | Query variant hint | Query variant hint | Query variant hint | N/A |
| **Hint via feedback** | Query variant hint | Query variant hint | Query variant hint | N/A |
| **Plan&nbsp;forced&nbsp;by&nbsp;user** | Query&nbsp;variant<br />forced&nbsp;plan | Query&nbsp;variant<br />forced&nbsp;plan | Query&nbsp;variant<br />forced&nbsp;plan | Query&nbsp;variant<br />forced&nbsp;plan |
| **Plan&nbsp;forced&nbsp;by&nbsp;APRC** | Query&nbsp;variant<br />forced&nbsp;plan | Query&nbsp;variant<br />forced&nbsp;plan | Query&nbsp;variant<br />forced&nbsp;plan | Query&nbsp;variant<br />forced&nbsp;plan |
| **No&nbsp;hint&nbsp;or&nbsp;forced&nbsp;plan** | Parent&nbsp;user's&nbsp;hint | No hint | No action | No action |

<sup>1</sup> Automatic plan correction

### Extended Events

- `parameter_sensitive_plan_optimization_skipped_reason`: Occurs when the parameter sensitive plan feature is skipped. Use this event to monitor the reason why PSP optimization is skipped.

  The following query will show all of the possible reasons why PSP was skipped:

  ```sql
  SELECT name, map_value FROM sys.dm_xe_map_values WHERE name ='psp_skipped_reason_enum' ORDER BY map_key;
  ```

- `parameter_sensitive_plan_optimization`: Occurs when a query uses PSP optimization feature. Debug channel only. Some fields of interest may be:
  - ***is_query_variant***: describes if this is a dispatcher plan (parent) or a query variant plan (child)
  - ***predicate_count***: number of predicates selected by PSP
  - ***query_variant_id***: displays the query variant ID. A value of 0 means the object is a dispatcher plan (parent).

### SQL Server Audit behavior

PSP optimization will provide audit data for the dispatcher plan statement, and any query variants associated with the dispatcher. The additional_information column within [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] Audit will also provide the appropriate t-sql stack information for query variants. Using the `MyNewDatabase` database as an example, if this database has a table called `T2` and a stored procedure with the name of `usp_test`, after the execution of the usp_test stored procedure, the audit log may contain the following entries:

| action_id | object_name | statement | additional_information |
| --- | --- | --- | --- |
| AUSC | | | `<action_info xmlns="http://schemas.microsoft.com/sqlserver/2008/sqlaudit_data"><session><![CDATA[Audit_Testing_Psp$A]]></session><action>event enabled</action><startup_type>manual</startup_type><object><![CDATA[audit_event]]></object></action_info>` |
| EX | usp_test | exec usp_test 300 | |
| SL | T2 | select * from dbo.t2 where ID=@id | `<tsql_stack><frame nest_level = '1' database_name = 'MyNewDatabase' schema_name = 'dbo' object_name = 'usp_test'/></tsql_stack>` |
| SL | T2 | select * from dbo.t2 where ID=@id option (PLAN PER VALUE(ObjectID = 933578364, QueryVariantID = 1, predicate_range([MyNewDatabase].[dbo].[T2].[ID] = @id, 100.0, 100000.0))) | `tsql_stack><frame nest_level = '1' database_name = 'MyNewDatabase' schema_name = 'dbo' object_name = 'usp_test'/></tsql_stack>` |
| EX | usp_test | exec usp_test 60000 | |
| SL | T2 | select * from dbo.t2 where ID=@id | `<tsql_stack><frame nest_level = '1' database_name = 'MyNewDatabase' schema_name = 'dbo' object_name = 'usp_test'/></tsql_stack>` |
| SL | T2 | select * from dbo.t2 where ID=@id option (PLAN PER VALUE(ObjectID = 933578364, QueryVariantID = 3, predicate_range([TestDB_FOR_PSP].[dbo].[T2].[ID] = @id, 100.0, 100000.0))) | `<tsql_stack><frame nest_level = '1' database_name = 'MyNewDatabase' schema_name = 'dbo' object_name = 'usp_test'/></tsql_stack>` |

## Known issues

Since PSP query variants are executed as a new prepared statement, the `objectid` isn't automatically exposed in the various plan cache related `sys.dm_exec_*` DMVs without shredding the ShowPlan XML and applying text pattern matching techniques (that is, additional XQuery processing). Only PSP optimization dispatcher plans currently emit the appropriate parent object ID. The `objectid` is exposed within the Query Store as Query Store allows for a more relational model than the plan cache hierarchy provides, see the Query Store system catalog view, [sys.query_store_query_variant (Transact-SQL)](../system-catalog-views/sys-query-store-query-variant.md#sysquery_store_query_variant-transact-sql) for more information. From a plan cache store perspective, PSP optimization will use the Object Plan CACHESTORE_OBJCP and the CACHESTORE_SQLCP cache stores.

PSP optimization currently compiles and executes each query variant as a new prepared statement, which is one of the reasons that the query variants *lose* their association with any parent modules' `object_id` if the dispatcher plan was based on a module (that is, stored procedure, trigger, function, view, and so on). As a prepared statement, the `object_id` won't be anything that can be mapped to an object in `sys.objects` directly but is essentially a calculated value based on an internal hash of the batch text (see the [Table Returned](../system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md#table-returned) section of the sys.dm_exec_plan_attributes DMV documentation for more information). Query variant plans will be placed in the plan cache object store (CACHESTORE_OBJCP) while dispatcher plans are placed in the SQL Plans cache store (CACHESTORE_SQLCP). The PSP feature will, however, store the `object_id` of a query variant's parent within the ObjectID attribute that is part of the PLAN PER VALUE hint that PSP adds to the ShowPlan XML if the parent query is part of a module and not dynamic or ad-hoc sql. Aggregate performance statistics for cached procedures, functions, and triggers can continue to be used for their respective purposes. More granular execution related statistics such as those found in views similar to the `sys.dm_exec_query_stats` DMV will still contain data for query variants, however, the association between the `object_id` for query variants and objects within the `sys.objects` table won't currently align without additional processing of the ShowPlan XML for each of the query variants in which more granular runtime statistics are required. The runtime and wait statistics information for query variants can be obtained from the Query Store without additional ShowPlan XML parsing techniques if the Query Store is enabled.

You can influence the current skewness thresholds used by the PSP optimization feature, with one or more of the following methods:

- Cardinality estimator (CE) trace flags, such as [Trace Flag 9481](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf9481) (global, session, or query level)
- [Database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#legacy_cardinality_estimation---on--off--primary-) options that attempt to lower the CE model in use, or influence the assumptions that the CE model makes in regards to the independence of multiple predicates. This is especially useful in cases where multi-column statistics don't exist, which affects PSP optimization's ability to evaluate the candidacy of those predicates.

For more information, see the *Increased Correlation Assumption for Multiple Predicates* section of the [Optimizing your query plans with the SQL Server 2014 Cardinality Estimator](/previous-versions/dn673537(v=msdn.10)?redirectedfrom=MSDN) whitepaper. The newer CE model attempts to assume some correlation and less independence for the conjunction and disjunction of predicates. Using the legacy CE model can affect how selectivity of the predicates in a multi-column join scenario can be calculated. This action should only be considered for specific scenarios and it is not generally recommended to use the legacy the CE model for most workloads.

## See also

- [Recompiling Execution Plans](../query-processing-architecture-guide.md#recompiling-execution-plans)
- [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse)
- [Simple Parameterization](../query-processing-architecture-guide.md#simple-parameterization)
- [Forced Parameterization](../query-processing-architecture-guide.md#forced-parameterization)
- [Hints query](../../t-sql/queries/hints-transact-sql-query.md)

## Next steps

- [Intelligent query processing](intelligent-query-processing.md)
- [Parameter Sensitivity](../query-processing-architecture-guide.md#parameter-sensitivity)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
