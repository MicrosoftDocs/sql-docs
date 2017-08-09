---
title: "Automatic tuning | Microsoft Docs"
description: Learn about automatic tuning in SQL Server and Azure SQL Database
ms.custom: ""
ms.date: "07/20/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "performance tuning [SQL Server]"
ms.assetid: 
caps.latest.revision: 
author: "jovanpop-msft"
ms.author: "jovanpop"
manager: "jhubbard"
---
# Automatic tuning
[!INCLUDE[tsql-appliesto-ssvNxt-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-asdb-xxxx-xxx.md)]  

  Automatic tuning is a database feature that provides insight into potential query performance problems, recommend solutions, and automatically fix identified problems.

Automatic tuning in [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)] notifies you whenever a potential performance issue is detected, and lets you apply corrective actions,
or lets the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically fix performance problems.
Automatic tuning in [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)] enables you to identify and fix performance issues caused by **SQL plan choice regressions**. Automatic tuning in [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] creates necessary indexes and drops unused indexes.

[!INCLUDE[ssde_md](../../includes/ssde_md.md)] monitors the queries that are executed on the database and automatically improves performance of the workload. [!INCLUDE[ssde_md](../../includes/ssde_md.md)] has a built-in intelligence mechanism that can automatically tune and improve performance of your queries by dynamically adapting the database to your workload. There are two automatic tuning features that are available:

 -	**Automatic plan correction** (available in [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)]) that identifies problematic plans and fixes SQL plan performance problems.
 -	**Automatic index management** (available in  [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)]) that identifies indexes that should be added in your database, and indexes that should be removed.

## Why automatic tuning?

One of the main tasks in classic database administration is monitoring the workload, identifying critical [!INCLUDE[tsql_md](../../includes/tsql_md.md)] queries, indexes that should be added to improve performance, and rarely used indexes. [!INCLUDE[ssde_md](../../includes/ssde_md.md)] provides detailed insight into the queries and indexes that you need to monitor. However, constantly monitoring database is a hard and tedious task, especially when dealing with many databases. Managing a huge number of databases might be impossible to do efficiently. Instead of monitoring and tuning your database manually, you might consider delegating some of the monitoring and tuning actions to [!INCLUDE[ssde_md](../../includes/ssde_md.md)] using automatic tuning feature.

### How does automatic tuning works?

Automatic tuning is a continuous monitoring and analysis process that constantly learns about the characteristic of your workload and identify potential issues and improvements.

![Automatic tuning process](./media/tuning-process.png)

This process enables database to dynamically adapt to your workload by finding what indexes and plans might improve performance of your workloads and what indexes affect your workloads. Based on these findings, automatic tuning applies tuning actions that improve performance of your workload. In addition, database continuously monitors performance after any change made by automatic tuning to ensure that it improves performance of your workload. Any action that didn’t improve performance is automatically reverted. This verification process is a key feature that ensures that any change made by automatic tuning does not decrease the performance of your workload.

## Automatic plan correction

Automatic plan correction is an automatic tuning feature that identifies **SQL plans choice regression** and automatically fix the issue by forcing the last known good plan.

### What is SQL plan choice regression?

[!INCLUDE[ssdenoversion_md](../../includes/ssdenoversion_md.md)] may use different SQL plans to execute the [!INCLUDE[tsql_md](../../includes/tsql_md.md)] queries. Query plans
depend on the statistics, indexes, and other factors. The optimal plan that should be used to execute some [!INCLUDE[tsql_md](../../includes/tsql_md.md)] query might be changed
over time. In some cases, the new plan might not be better than the previous one, and the new plan might cause a performance regression.

 ![SQL plan choice regression](media/plan-choice-regression.png "SQL plan choice regression") 

In order to prevent unexpected performance issues, users must periodically monitor system and look for the queries that regressed. If any plan regressed, user should find some
previous good plan and force it instead of the current one using `sp_query_store_force_plan` procedure. The best practice would be to force last known good plan because older plans might be invalid due to statistic or index changes.
The user who forces the last known good plan should monitor performance of the query that is executed using the forced plan and verify that forced plan works as expected. Depending on
the results of monitoring and analysis, plan should be forced or user should find some other way to optimize the query.
Manually forced plans should not be forced forever, because the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] should be able to apply optimal plans. The user or DBA should eventually
unforce the plan using `sp_query_store_unforce_plan` procedure, and let the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] find the optimal plan.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides all necessary views and procedures required to monitor performance and fix problems in Query Store. However, continuous
monitoring and fixing performance issues might be a tedious process.

[!INCLUDE[ssde_md](../../includes/ssde_md.md)] in [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)] provides information about regressed plans and recommended corrective actions.
Additionally, [!INCLUDE[ssde_md](../../includes/ssde_md.md)] enables you to fully automate this process and let [!INCLUDE[ssde_md](../../includes/ssde_md.md)] fix any problem found related 
to the plan changes.

### How to detect plan choice regression?

In [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)], the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] detects and shows potential plan choice regressions and the recommended
actions that should be applied in the [sys.dm_db_tuning_recommendations &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)
view. The view shows information about the problem, the importance of the issue, and details such as the
identified query, the ID of the regressed plan, the ID of the plan that was used as baseline for comparison, and the [!INCLUDE[tsql_md](../../includes/tsql_md.md)] statement that can
be executed to fix the problem.

| type | description | datetime | score | details | … |
| --- | --- | --- | --- | --- | --- |
| `FORCE_LAST_GOOD_PLAN` | CPU time changed from 4 ms to 14 ms | 3/17/2017 | 83 | `queryId` `recommendedPlanId` `regressedPlanId` `T-SQL` |   |
| `FORCE_LAST_GOOD_PLAN` | CPU time changed from 37 ms to 84 ms | 3/16/2017 | 26 | `queryId` `recommendedPlanId` `regressedPlanId` `T-SQL` |   |

Some columns from this view are described in the following list:
 - Type of the recommended action - `FORCE_LAST_GOOD_PLAN`.
 - Description that contains information why [!INCLUDE[ssde_md](../../includes/ssde_md.md)] thinks that this plan change is a potential performance regression.
 - Datetime when the potential regression is detected.
 - Score of this recommendation. 
 - Details about the issues such as ID of the detected plan, ID of the regressed plan, ID of the plan that should be forced to fix the issue, [!INCLUDE[tsql_md](../../includes/tsql_md.md)]
 script that might be applied to fix the issue, etc. Details are stored in [JSON format](../../relational-databases/json/index.md).

Use the following query to obtain a script that fixes the issue and additional information about the estimated gain:

```   
SELECT reason, score,
      script = JSON_VALUE(details, '$.implementationDetails.script'),
      planForceDetails.*,
      estimated_gain = (regressedPlanExecutionCount+recommendedPlanExecutionCount)
                  *(regressedPlanCpuTimeAverage-recommendedPlanCpuTimeAverage)/1000000,
      error_prone = IIF(regressedPlanErrorCount>recommendedPlanErrorCount, 'YES','NO')
FROM sys.dm_db_tuning_recommendations
  CROSS APPLY OPENJSON (Details, '$.planForceDetails')
    WITH (  [query_id] int '$.queryId',
            [current plan_id] int '$.regressedPlanId',
            [recommended plan_id] int '$.recommendedPlanId',

            regressedPlanErrorCount int,
            recommendedPlanErrorCount int,

            regressedPlanExecutionCount int,
            regressedPlanCpuTimeAverage float,
            recommendedPlanExecutionCount int,
            recommendedPlanCpuTimeAverage float

          ) as planForceDetails;
```

[!INCLUDE[ssresult-md](../../includes/ssresult-md.md)]     

| reason | score | script | query\_id | current plan\_id | recommended plan\_id | estimated\_gain | error\_prone
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| CPU time changed from 3 ms to 46 ms | 36 | EXEC sp\_query\_store\_force\_plan 12, 17; | 12 | 28 | 17 | 11.59 | 0

`estimated_gain` represents the estimated number of seconds that would be saved if the recommended plan would be executed instead of the current plan. The recommended plan should be forced instead of the current plan if the gain is greater than 10 seconds. If there are more errors (for example, time-outs or aborted executions) in the current plan than in the recommended plan, the column `error_prone` would be set to the value `YES`. Error prone plan is another reason why the recommended plan should be forced instead of the current one.

### Automatic plan choice correction

In addition to detection, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] can automatically switch to the last known good plan whenever the regression is detected.

![SQL plan choice correction](media/force-last-good-plan.png "SQL plan choice correction") 

When the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] applies a recommendation, it automatically monitors the performance of the forced plan. The forced plan will be retained until
a recompile (for example, on next statistics or schema change) if it is better than the regressed plan. If the forced plan is not better than the regressed plan, the new plan will be unforced
and the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will compile a new plan.

### Enabling automatic plan choice correction

The user can enable automatic tuning per database and specify that last good plan should be forced whenever some plan change regression is detected. Automatic tuning is enabled using
the following command:

```   
ALTER DATABASE current
SET AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = ON ); 
```
Once you turn-on this option, [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will automatically force any recommendation where the estimated CPU gain is higher than 10 seconds, or the number of errors in the new plan is higher than the number of errors in the recommended plan, and verify that the forced plan is better than the current one.

## Automatic index management

In [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)], index management is easy because [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] learns about your workload and ensures that your data is always optimally indexed. Proper index design is crucial for optimal performance of your workload, and automatic index management can help you optimize your indexes. Automatic index management can either fix performance issues in incorrectly indexed databases, or maintain and improve indexes on the existing database schema. Automatic tuning in [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] performs the following actions:

 - Identifies indexes that could improve performance of your T-SQL queries that read data from the tables.
 - Identifies the redundant indexes or indexes that were not used in longer period of time that could be removed. Removing unnecessary indexes improves performance of the queries that update data in tables.

### Why do you need index management?

Indexes speed up some of your queries that read data from the tables; however, they can slow down the queries that update data. You need to carefully analyze when to create an index and what columns you need to include in the index. Some indexes might not be needed after some time. Therefore, you would need to periodically identify and drop the indexes that do not bring any benefits. If you ignore the unused indexes, performance of the queries that update data would be decreased without any benefit on the queries that read data. Unused indexes also affect overall performance of the system because additional updates require unnecessary logging.

Finding the optimal set of indexes that improve performance of the queries that read data from your tables and have minimal impact on updates might require continuous and complex analysis.

[!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] uses built-in intelligence and advanced rules that analyze your queries, identify indexes that would be optimal for your current workloads, and the indexes might be removed. Azure SQL Database ensures that you have a minimal necessary set of indexes that optimize the queries that read data, with the minimized impact on the other queries.

### How to identify indexes that need to be changed in your database?

[!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] makes index management process easy. Instead of the tedious process of manual workload analysis and index monitoring, [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] analyzes your workload, identifies the queries that could be executed faster with a new index, and identifies unused or duplicated indexes. Find more information about identification of indexes that should be changed at [Find index recommendations in Azure portal](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-advisor-portal).

### Automatic index management

In addition to detection, [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] can automatically apply identified recommendations. If you find that the built-in rules improve the performance of your database, you might let [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] automatically manage your indexes.

To enable automatic tuning in Azure SQL Database and let automatic tuning feature fully manage your workload, see [Enable automatic tuning in Azure SQL Database using Azure portal](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-automatic-tuning-enable).

When the [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] applies a CREATE INDEX or DROP INDEX recommendation, it automatically monitors the performance of the queries that are affected by the index. New index will be retained only if performances of the affected queries are worse. Dropped index will be automatically re-created if there are some queries that run slower due to the absence of the index.

### Automatic index management considerations

Actions required to create necessary indexes in [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] might consume resources and temporally affect workload performance. To minimize the impact of index creation on workload performance, Azure SQL Database finds the appropriate time window for any index management operation. Tuning action is postponed if the database needs resources to execute your workload, and started when the database has enough unused resources that can be used for the maintenance task. One important feature in automatic index management is a verification of the actions. When Azure SQL Database creates or drops index, a monitoring process analyzes performance of your workload to verify that the action improved the performance. If it didn’t bring significant improvement – the action is immediately reverted. This way, Azure SQL Database ensures that automatic actions do not negatively impact performance of your workload. Indexes created by automatic tuning are transparent for the maintenance operation on the underlying schema. Schema changes such as dropping or renaming columns are not blocked by the presence of automatically created indexes. Indexes that are automatically created by Azure SQL Database are immediately dropped when related table or columns is dropped.

## See Also  
 [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [sys.database_automatic_tuning_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)  
 [sys.dm_db_tuning_recommendations &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)    
 [sp_query_store_force_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)     
 [sp_query_store_unforce_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)           
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [JSON functions](../../relational-databases/json/index.md)
