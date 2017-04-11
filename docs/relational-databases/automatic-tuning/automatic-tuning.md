---
title: "Automatic tuning | Microsoft Docs"
ms.custom: ""
ms.date: "4/16/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "performance tuning [SQL Server]"
  - "binary collations [SQL Server]"
ms.assetid: 
caps.latest.revision: 
author: "jovanpop-msft"
ms.author: "jovanpop"
manager: "jhubbard"
---
# Automatic tuning
[!INCLUDE[tsql-appliesto-ssvNxt-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

  Automatic tuning is a database feature that provides insight into potential query performance problems, recommend solutions, and automatically fix identified problems.

Automatic tuning in [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)], notifies you whenever a potential performance issue is detected, and lets you apply corrective actions,
or lets the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically fix performance problems.

## Forcing last good plan

[!INCLUDE[ssdenoversion_md](../../includes/ssdenoversion_md.md)] may use different SQL plans to execute the [!INCLUDE[tsql_md](../../includes/tsql_md.md)] queries. Query plans
depend on the statistics, indexes, and other factors, so the optimal plan that should be used to execute some [!INCLUDE[tsql_md](../../includes/tsql_md.md)] query might be changed
over time. In some cases, new plan might not be better than the previous one, and this might cause a performance regression.

 ![SQL plan choice regression](media/plan-choice-regression.png "SQL plan choice regression") 

In order to prevent unexpected performance issues, users must periodically monitor system and look for the queries that regressed. If any plan regressed, user should find some
previous good plan and force it instead of the current one. The best practice would be to force last known good plan because older plans might be invalid due to statistic or index changes.
When user forces last known good plan, he should monitor performance of the query that is executed using the forced plan and verify that forced plan works as expected. Depending on
the results of monitoring and analysis, plan should be forced or user should find some other way to optimize the query.
Manually forced plans should not be forced forever, because the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] should be able to apply optimal plans. The user or DBA should eventually
unforce the plan and let the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] find the optimal plan.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides all necessary views and procedures required to monitor performance and fix problems in Query Store. However, continuous
monitoring and fixing performance issues might be a tedious process.

## Automatic plan change regression detection

In [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)], the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] detects and shows potential plan choice regressions and the recommended
actions that should be applied in the **sys.dm\_db\_tuning\_recommendations** view. The view shows information about the problem, the importance of the issue, and details such as the
identified query, the id of the regressed plan, the id of the plan that was used as baseline for comparison, and the [!INCLUDE[tsql_md](../../includes/tsql_md.md)] statement that can
be executed to fix the problem.

| type | description | datetime | score | details | … |
| --- | --- | --- | --- | --- | --- |
| `FORCE_LAST_GOOD_PLAN` | CPU time changed from 4ms to 14ms | 3/17/2017 | 83 | {`queryId`,`forcedPlanId`,`regressedPlanId`,`T-SQL`} |   |
| `FORCE_LAST_GOOD_PLAN` | CPU time changed from 37ms to 84ms | 3/16/2017 | 26 | {`queryId`,`forcedPlanId`,`regressedPlanId`,`T-SQL`} |   |

The most important information shown in this view are:
 - Type of the recommended action - `FORCE_LAST_GOOD_PLAN`.
 - Description that contains information why [!INCLUDE[ssde_md](../../includes/ssde_md.md)] thinks that this is a potential performance regression.
 - Datetime when the potential regression is detected.
 - Score of this recommendation. 
 - Details about the issues such as id of the detected plan, id of the regressed plan, id of the plan that should be forced to fix the issue, [!INCLUDE[tsql_md](../../includes/tsql_md.md)]
 script that might be applied to fix the issue, etc.

Use the following query to obtain a script that will fix the issue:

```   
SELECT description, score,
      JSON_VALUE(details, '$.implementationDetails.script') script,
      planForceDetails.*
FROM sys.dm_db_tuning_recommendations
  CROSS APPLY OPENJSON (Details, '$.planForceDetails')
    WITH (  [query_id] int '$.queryId',
            [new plan_id] int '$.regressedPlanId',
            [recommended plan_id] int '$.forcedPlanId'
          ) as planForceDetails;
```

[!INCLUDE[ssresult-md](../../includes/ssresult-md.md)]     

| description | score | script | query\_id | new plan\_id | regressed plan\_id |
| --- | --- | --- | --- | --- | --- | --- |
| CPU time changed from 3ms to 46ms | 36 | EXEC sp\_query\_store\_force\_plan 12, 17; | 12 | 28 | 17 |


## Automatic plan choice correction

In addition to detection, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] can automatically switch to the last known good plan whenever the regression is detected.

![SQL plan choice correction](media/force-last-good-plan.png "SQL plan choice correction") 

When the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] applies a recommendation, it automatically monitors the performance of the forced plan. The forced plan will be retained until
a recompile (e.g. on next statistics or schema change) if it is better than the regressed plan. If the forced plan is not better than the regressed plan, the new plan will be unforced
and the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will compile a new plan.

The user can enable automatic tuning per database and specify that last good plan should be forced whenever some plan change regression is detected. Automatic tuning is enabled using
the following command:

```   
ALTER DATABASE current
SET AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = ON ); 
```

The status of advisor is shown in the following view:

```    
SELECT name, desired_state_desc_, actual_state_desc, reason_desc
FROM sys.database_automatic_tuning_options;
```
[!INCLUDE[ssresult-md](../../includes/ssresult-md.md)]     

| name | desired\_state\_desc | actual\_state\_desc | reason\_desc |
| --- | --- | --- | --- | --- | --- | --- |
| FORCE\_LAST\_GOOD\_PLAN | ON | OFF | QUERY_STORE_OFF |

`FORCE_LAST_GOOD_PLAN` option might be in `OFF` state even if the user specified `ON`. The option might be disabled if Query Store is disabled or in read-only mode. Column `actual_state_desc`
give information about the current state of automatic tuning option, and column `reason_desc` give information why is actual state diferent that desired state. Values in `reason_desc` column
 are shown in the following table:

| reason\_code | reason\_desc | Description |
| --- | --- | --- |
| 2 | `DISABLED` | Option is disabled by system. |
| 11 | `QUERY_STORE_OFF` | Query Store is turned off. |
| 12 | `QUERY_STORE_READ_ONLY` | Query Store is in read-only mode. |
| 13 | `NOT_SUPPORTED` | Available only in Enterprise edition of SQL Server. |

## See Also  
 [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [sys.database_automatic_tuning_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
 [sys.dm_db_tuning_recommendations &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)
