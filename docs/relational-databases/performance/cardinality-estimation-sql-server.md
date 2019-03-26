---
title: "Cardinality Estimation (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "02/24/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "cardinality estimator"
  - "CE (cardinality estimator)"
  - "estimating cardinality"
ms.assetid: baa8a304-5713-4cfe-a699-345e819ce6df
author: julieMSFT
ms.author: jrasnick
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Cardinality Estimation (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer is a cost-based Query Optimizer. This means that it selects query plans that have the lowest estimated processing cost to execute. The Query Optimizer determines the cost of executing a query plan based on two main factors:

- The total number of rows processed at each level of a query plan, referred to as the cardinality of the plan.
- The cost model of the algorithm dictated by the operators used in the query.

The first factor, cardinality, is used as an input parameter of the second factor, the cost model. Therefore, improved cardinality leads to better estimated costs and, in turn, faster execution plans.

Cardinality estimation (CE) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is derived primarily from histograms that are created when indexes or statistics are created, either manually or automatically. Sometimes, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also uses constraint information and logical rewrites of queries to determine cardinality.

In the following cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot accurately calculate cardinalities. This causes inaccurate cost calculations that may cause suboptimal query plans. Avoiding these constructs in queries may improve query performance. Sometimes, alternative query formulations or other measures are possible and these are pointed out:

- Queries with predicates that use comparison operators between different columns of the same table.
- Queries with predicates that use operators, and any one of the following are true:
  - There are no statistics on the columns involved on either side of the operators.
  - The distribution of values in the statistics is not uniform, but the query seeks a highly selective value set. This situation can be especially true if the operator is anything other than the equality (=) operator.
  - The predicate uses the not equal to (!=) comparison operator or the `NOT` logical operator.
- Queries that use any of the SQL Server built-in functions or a scalar-valued, user-defined function whose argument is not a constant value.
- Queries that involve joining columns through arithmetic or string concatenation operators.
- Queries that compare variables whose values are not known when the query is compiled and optimized.

This article illustrates how you can assess and choose the best CE configuration for your system. Most systems benefit from the latest CE because it is the most accurate. The CE predicts how many rows your query will likely return. The cardinality prediction is used by the Query Optimizer to generate the optimal query plan. With more accurate estimations, the Query Optimizer can usually do a better job of producing a more optimal query plan.  
  
Your application system could possibly have an important query whose plan is changed to a slower plan due to the new CE. Such a query might be like one of the following:  
  
- An OLTP (online transaction processing) query that runs so frequently that multiple instance of it often run concurrently.  
- A SELECT with substantial aggregation that runs during your OLTP business hours.  
  
You have techniques for identifying a query that performs slower with the new CE. And you have options for how to address the performance issue.     
  
## Versions of the CE  
In 1998, a major update of the CE was part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0, for which the compatibility level was 70. This version of the CE model is set on four basic assumptions:

-  **Independence:** Data distributions on different columns are assumed to be independent of each other, unless correlation information is available and usable.
-  **Uniformity:** Distinct values are evenly spaced and that they all have the same frequency. More precisely, within each [histogram](../../relational-databases/statistics/statistics.md#histogram) step, distinct values are evenly spread and each value has same frequency. 
-  **Containment (Simple):** Users query for data that exists. For example, for an equality join between two tables, factor in the predicates selectivity<sup>1</sup> in each input histogram, before joining histograms to estimate the join selectivity. 
-  **Inclusion:** For filter predicates where `Column = Constant`, the constant is assumed to actually exist for the associated column. If a corresponding histogram step is non-empty, one of the step's distinct values is assumed to match the value from the predicate.

  <sup>1</sup> Row count that satisfies the predicate.

Subsequent updates started with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], meaning compatibility levels 120 and above. The CE updates for levels 120 and above incorporate updated assumptions and algorithms that work well on modern data warehousing and on OLTP workloads. From the CE 70 assumptions, the following model assumptions were changed starting with CE 120:

-  **Independence** becomes **Correlation:** The combination of the different column values are not necessarily independent. This may resemble more real-life data querying.
-  **Simple Containment** becomes **Base Containment:** Users might query for data that does not exist. For example, for an equality join between two tables, we use the base tables histograms to estimate the join selectivity, and then factor in the predicates selectivity.
  
**Compatibility level:** You can ensure your database is at a particular level by using the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code for [COMPATIBILITY_LEVEL](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

```sql  
SELECT ServerProperty('ProductVersion');  
GO  
  
ALTER DATABASE <yourDatabase>  
SET COMPATIBILITY_LEVEL = 130;  
GO  
  
SELECT d.name, d.compatibility_level  
FROM sys.databases AS d  
WHERE d.name = 'yourDatabase';  
GO  
```  
  
For a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database set at compatibility level 120 or above, activation of the [trace flag 9481](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) forces the system to use the CE version 70.  
  
**Legacy CE:** For a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database set at compatibility level 120 and above, the CE version 70 can be can be activated by using the at the database level by using the [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).
  
```sql  
ALTER DATABASE SCOPED CONFIGURATION 
SET LEGACY_CARDINALITY_ESTIMATION = ON;  
GO  
  
SELECT name, value  
FROM sys.database_scoped_configurations  
WHERE name = 'LEGACY_CARDINALITY_ESTIMATION';  
GO
```  
 
Or starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1, the [Query Hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) `USE HINT ('FORCE_LEGACY_CARDINALITY_ESTIMATION')`.
 
 ```sql  
SELECT CustomerId, OrderAddedDate  
FROM OrderTable  
WHERE OrderAddedDate >= '2016-05-01'; 
OPTION (USE HINT ('FORCE_LEGACY_CARDINALITY_ESTIMATION'));  
```
 
**Query store:** Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], the query store is a handy tool for examining the performance of your queries. In [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)], in the **Object Explorer** under your database node, a **Query Store** node is displayed when the query store is enabled.  
  
```sql  
ALTER DATABASE <yourDatabase>  
SET QUERY_STORE = ON;  
GO  
  
SELECT q.actual_state_desc AS [actual_state_desc_of_QueryStore],  
        q.desired_state_desc,  
        q.query_capture_mode_desc  
FROM sys.database_query_store_options AS q;  
GO  
  
ALTER DATABASE <yourDatabase>  
SET QUERY_STORE CLEAR;  
```  
  
> [!TIP] 
> We recommend that you install the latest release of [Management Studio](https://msdn.microsoft.com/library/mt238290.aspx) and update it often.  
  
Another option for tracking the cardinality estimation process is to use the extended event named **query_optimizer_estimate_cardinality**. The following [!INCLUDE[tsql](../../includes/tsql-md.md)] code sample runs on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It writes a .xel file to `C:\Temp\` (although you can change the path). When you open the .xel file in [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)], its detailed information is displayed in a user friendly manner.  
  
```sql  
DROP EVENT SESSION Test_the_CE_qoec_1 ON SERVER;  
go  
  
CREATE EVENT SESSION Test_the_CE_qoec_1  
ON SERVER  
ADD EVENT sqlserver.query_optimizer_estimate_cardinality  
    (  
        ACTION (sqlserver.sql_text)  
            WHERE (  
                sql_text LIKE '%yourTable%'  
                and sql_text LIKE '%SUM(%'  
            )  
    )  
ADD TARGET package0.asynchronous_file_target   
        (SET  
            filename = 'c:\temp\xe_qoec_1.xel',  
            metadatafile = 'c:\temp\xe_qoec_1.xem'  
        );  
GO  
  
ALTER EVENT SESSION Test_the_CE_qoec_1  
ON SERVER  
STATE = START;  --STOP;  
GO  
```  
  
For information about extended events as tailored for [!INCLUDE[ssSDS](../../includes/sssds-md.md)], see [Extended events in SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-xevent-db-diff-from-svr/).  
  
## Steps to assess the CE version  
  
Next are steps you can use to assess whether any of your most important queries perform less well under the latest CE. Some of the steps are performed by running a code sample presented in a preceding section.  
  
1.  Open [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)]. Ensure your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]database is set to the highest available compatibility level.  
  
2.  Perform the following preliminary steps:  
  
    1.  Open [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)].  
  
    2.  Run the T-SQL to ensure that your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is set to the highest available compatibility level.  
  
    3.  Ensure that your database has its `LEGACY_CARDINALITY_ESTIMATION` configuration turned OFF.  
  
    4.  CLEAR your query store. Of course, ensure your query store is ON.  
  
    5.  Run the statement: `SET NOCOUNT OFF;`  
  
3.  Run the statement: `SET STATISTICS XML ON;`  
  
4.  Run your important query.  
  
5.  In the results pane, on the **Messages** tab, note the actual number of rows affected.  
  
6.  In the results pane on the **Results** tab, double-click the cell that contains the statistics in XML format. A graphic query plan is displayed.  
  
7.  Right-click the first box in the graphic query plan, and then click **Properties**.  
  
8.  For later comparison with a different configuration, note the values for the following properties:  
  
    -   **CardinalityEstimationModelVersion**.  
  
    -   **Estimated Number of Rows**.  
  
    -   **Estimated I/O Cost**, and several similar *Estimated* properties that involve actual performance rather than row count predictions.  
  
    -   **Logical Operation** and **Physical Operation**. *Parallelism* is a good value.  
  
    -   **Actual Execution Mode**. *Batch* is a good value, better than *Row*.  
  
9. Compare the estimated number of rows to the actual number of rows. Is the CE inaccurate by 1% (high or low), or by 10%?  
  
10. Run: `SET STATISTICS XML OFF;`  
  
11. Run the T-SQL to decrease the compatibility level of your database by one level (such as from 130 down to 120).  
  
12. Rerun all the non-preliminary steps.  
  
13. Compare the CE property values from the two runs.  
  
    - Is the inaccuracy percentage under the newest CE less than under the older CE?  
  
14. Finally, compare the various performance property values from the two runs.  
  
    -   Did your query use a different plan under the two differing CE estimations?  
  
    -   Did your query run slower under the latest CE?  
  
    -   Unless your query runs better and with a different plan under the older CE, you almost certainly want the latest CE.  
  
    -   However, if your query runs with a faster plan under the older CE, consider forcing the system to use the faster plan and to ignore the CE. This way you can have the latest CE on for everything, while keeping the faster plan in the one odd case.  
  
## How to activate the best query plan  
  
Suppose that with CE 120 or above, a less efficient query plan is generated for your query. Here are some options you have to activate the better plan:  
  
1. You could set the compatibility level to a value lower than the latest available, for your whole database.  
  
   - For example, setting the compatibility level 110 or lower activates CE 70, but it makes all queries subject to the previous CE model.  
  
   - Further, setting a lower compatibility level also misses a number of improvements in the query optimizer for latest versions.  
  
2. You could use `LEGACY_CARDINALITY_ESTIMATION` database option, to have the whole database use the older CE, while retaining other improvements in the query optimizer.   

3. You could use `LEGACY_CARDINALITY_ESTIMATION` query hint, to have a single query use the older CE, while retaining other improvements in the query optimizer.  
  
For the finest control, you could *force* the system to use the plan that was generated with CE 70 during your testing. After you *pin* your preferred plan, you can set your whole database to use the latest compatibility level and CE. The option is elaborated next.  
  
### How to force a particular query plan  
  
The query store gives you different ways that you can force the system to use a particular query plan:  
  
- Execute **sp_query_store_force_plan**.  
  
- In [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)], expand your **Query Store** node, right-click **Top Resource Consuming Nodes**, and then click **View Top Resource Consuming Nodes**. The display shows buttons labeled **Force Plan** and **Unforce Plan**.  
  
For more information about the query store, see [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).  
  
## Examples of CE improvements  
  
This section describes example queries that benefit from the enhancements implemented in the CE in recent releases. This is background information that does not call for specific action on your part.  
  
### Example A. CE understands maximum value might be higher than when statistics were last gathered  
  
Suppose statistics were last gathered for `OrderTable` on `2016-04-30`, when the maximum `OrderAddedDate` was `2016-04-30`. The CE 120 (and above version) understands that columns in `OrderTable` which have *ascending* data might have values larger than the maximum recorded by the statistics. This understanding improves the query plan for [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statements such as the following.  
  
```sql  
SELECT CustomerId, OrderAddedDate  
FROM OrderTable  
WHERE OrderAddedDate >= '2016-05-01';  
```  
  
### Example B. CE understands that filtered predicates on the same table are often correlated  
  
In the following SELECT we see filtered predicates on `Model` and `ModelVariant`. We intuitively understand that when `Model` is 'Xbox' there is a chance the `ModelVariant` is 'One', given that Xbox has a variant called One.  
  
Starting with CE 120, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] understands there might be a correlation between the two columns on the same table, `Model` and `ModelVariant`. The CE makes a more accurate estimation of how many rows will be returned by the query, and the [query optimizer](../../relational-databases/query-processing-architecture-guide.md#optimizing-select-statements) generates a more optimal plan.  
  
```sql  
SELECT Model, Purchase_Price  
FROM dbo.Hardware  
WHERE Model = 'Xbox' AND  
      ModelVariant = 'One';  
```  
  
### Example C. CE no longer assumes any correlation between filtered predicates from different tables 
With extense new research on modern workloads and actual business data reveal that predicate filters from different tables usually do not correlate with each other. In the following query, the CE assumes there is no correlation between `s.type` and `r.date`. Therefore the CE makes a lower estimate of the number of rows returned.  
  
```sql  
SELECT s.ticket, s.customer, r.store  
FROM dbo.Sales    AS s  
CROSS JOIN dbo.Returns  AS r  
WHERE s.ticket = r.ticket AND  
      s.type = 'toy' AND  
      r.date = '2016-05-11';  
```  
  
## See Also  
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)   
 [Optimizing Your Query Plans with the SQL Server 2014 Cardinality Estimator](https://msdn.microsoft.com/library/dn673537.aspx)  
 [Query Hints](../../t-sql/queries/hints-transact-sql-query.md)     
 [USE HINT Query Hints](../../t-sql/queries/hints-transact-sql-query.md#use_hint)       
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)    
 [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)   
