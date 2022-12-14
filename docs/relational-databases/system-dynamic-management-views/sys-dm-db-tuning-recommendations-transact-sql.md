---
title: "sys.dm_db_tuning_recommendations (Transact-SQL)"
description: Learn how to find potential performance issues and recommended fixes in SQL Server and Azure SQL Database
author: "jovanpop-msft"
ms.author: "jovanpop"
ms.date: "03/12/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_tuning_recommendations"
  - "dm_db_tuning_recommendations"
  - "sys.dm_db_tuning_recommendations_TSQL"
  - "dm_db_tuning_recommendations_TSQL"
helpviewer_keywords:
  - "database tuning recommendations feature [SQL Server], sys.dm_db_tuning_recommendations dynamic management view"
  - "sys.dm_db_tuning_recommendations dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm\_db\_tuning\_recommendations (Transact-SQL)
[!INCLUDE[sqlserver2017-asdb](../../includes/applies-to-version/sqlserver2017-asdb.md)]

  Returns detailed information about tuning recommendations.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.

| **Column name** | **Data type** | **Description** |
| --- | --- | --- |
| **name** | **nvarchar(4000)** | Unique name of recommendation. |
| **type** | **nvarchar(4000)** | The name of the automatic tuning option that produced the recommendation, for example, `FORCE_LAST_GOOD_PLAN` |
| **reason** | **nvarchar(4000)** | Reason why this recommendation was provided. |
| **valid\_since** | **datetime2** | The first time this recommendation was generated. |
| **last\_refresh** | **datetime2** | The last time this recommendation was generated. |
| **state** | **nvarchar(4000)** | JSON document that describes the state of the recommendation. Following fields are available:<br />-   `currentValue` - current state of the recommendation.<br />-   `reason` - constant that describes why the recommendation is in the current state.|
| **is\_executable\_action** | **bit** | 1 = The recommendation can be executed against the database via [!INCLUDE[tsql_md](../../includes/tsql-md.md)] script.<br />0 = The recommendation cannot be executed against the database (for example: information only or reverted recommendation) |
| **is\_revertable\_action** | **bit** | 1 = The recommendation can be automatically monitored and reverted by Database engine.<br />0 = The recommendation cannot be automatically monitored and reverted. Most &quot;executable&quot; actions will be &quot;revertable&quot;. |
| **execute\_action\_start\_time** | **datetime2** | Date the recommendation is applied. |
| **execute\_action\_duration** | **time** | Duration of the execute action. |
| **execute\_action\_initiated\_by** | **nvarchar(4000)** | `User` = User manually forced plan in the recommendation. <br /> `System` = System automatically applied recommendation. |
| **execute\_action\_initiated\_time** | **datetime2** | Date the recommendation was applied. |
| **revert\_action\_start\_time** | **datetime2** | Date the recommendation was reverted. |
| **revert\_action\_duration** | **time** | Duration of the revert action. |
| **revert\_action\_initiated\_by** | **nvarchar(4000)** | `User` = User manually unforced recommended plan. <br /> `System` = System automatically reverted recommendation. |
| **revert\_action\_initiated\_time** | **datetime2** | Date the recommendation was reverted. |
| **score** | **int** | Estimated value/impact for this recommendation on the 0-100 scale (the larger the better) |
| **details** | **nvarchar(max)** | JSON document that contains more details about the recommendation. Following fields are available:<br /><br />`planForceDetails`<br />-    `queryId` - query\_id of the regressed query.<br />-    `regressedPlanId` - plan_id of the regressed plan.<br />-   `regressedPlanExecutionCount` - Number of executions of the query with regressed plan before the regression is detected.<br />-    `regressedPlanAbortedCount` - Number of detected errors during the execution of the regressed plan.<br />-    `regressedPlanCpuTimeAverage` - Average CPU time (in micro seconds) consumed by the regressed query before the regression is detected.<br />-    `regressedPlanCpuTimeStddev` - Standard deviation of CPU time consumed by the regressed query before the regression is detected.<br />-    `recommendedPlanId` - plan_id of the plan that should be forced.<br />-   `recommendedPlanExecutionCount`- Number of executions of the query with the plan that should be forced before the regression is detected.<br />-    `recommendedPlanAbortedCount` - Number of detected errors during the execution of the plan that should be forced.<br />-    `recommendedPlanCpuTimeAverage` - Average CPU time (in micro seconds) consumed by the query executed with the plan that should be forced (calculated before the regression is detected).<br />-    `recommendedPlanCpuTimeStddev` Standard deviation of CPU time consumed by the regressed query before the regression is detected.<br /><br />`implementationDetails`<br />-  `method` - The method that should be used to correct the regression. Value is always `TSql`.<br />-    `script` - [!INCLUDE[tsql_md](../../includes/tsql-md.md)] script that should be executed to force the recommended plan. |
  
## Remarks  
 Information returned by `sys.dm_db_tuning_recommendations` is updated when database engine identifies potential query performance regression, and is not persisted. Recommendations are kept only until the database engine is restarted. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time.  Database administrators should periodically make backup copies of the tuning recommendation if they want to keep it after server recycling.   

 The `currentValue` field in the `state` column might have the following values:
 
 | Status | Description |
 |--------|-------------|
 | `Active` | Recommendation is active and not yet applied. User can take recommendation script and execute it manually. |
 | `Verifying` | Recommendation is applied by [!INCLUDE[ssde_md](../../includes/ssde_md.md)] and internal verification process compares performance of the forced plan with the regressed plan. |
 | `Success` | Recommendation is successfully applied. |
 | `Reverted` | Recommendation is reverted because there are no significant performance gains. |
 | `Expired` | Recommendation has expired and cannot be applied anymore. |

JSON document in `state` column contains the reason that describes why is the recommendation in the current state. Values in the reason field might be: 

| Reason | Description |
|--------|-------------|
| `SchemaChanged` | Recommendation expired because the schema of a referenced table is changed. New recommendation will be created if a new query plan regression is detected on the new schema. |
| `StatisticsChanged`| Recommendation expired due to the statistic change on a referenced table. New recommendation will be created if a new query plan regression is detected based on new statistics. |
| `ForcingFailed` | Recommended plan cannot be forced on a query. Find the `last_force_failure_reason` in the [sys.query_store_plan](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md) view to find the reason of the failure. |
| `AutomaticTuningOptionDisabled` | `FORCE_LAST_GOOD_PLAN` option is disabled by the user during verification process. Enable `FORCE_LAST_GOOD_PLAN` option using [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) statement or force the plan manually using the script in the `details` column. |
| `UnsupportedStatementType` | Plan cannot be forced on the query. Examples of unsupported queries are cursors and `INSERT BULK` statement. |
| `LastGoodPlanForced` | Recommendation is successfully applied. |
| `AutomaticTuningOptionNotEnabled`| [!INCLUDE[ssde_md](../../includes/ssde_md.md)] identified potential performance regression, but the `FORCE_LAST_GOOD_PLAN` option is not enabled - see [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md). Apply recommendation manually or enable `FORCE_LAST_GOOD_PLAN` option. |
| `VerificationAborted`| Verification process is aborted due to the restart or Query Store cleanup. |
| `VerificationForcedQueryRecompile`| Query is recompiled because there is no significant performance improvement. |
| `PlanForcedByUser`| User manually forced the plan using [sp_query_store_force_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md) procedure. Database engine will not apply the recommendation if user explicitly decided to force some plan. |
| `PlanUnforcedByUser` | User manually unforced the plan using [sp_query_store_unforce_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md) procedure. Since the user explicitly reverted the recommended plan, database engine will keep using the current plan and generate a new recommendation if some plan regression occurs in future. |
| `UserForcedDifferentPlan` | User manually forced different plan using [sp_query_store_force_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md) procedure. Database engine will not apply the recommendation if user explicitly decided to force some plan. |
| `TempTableChanged` | A temporary table that was used in the plan is changed. |

 Statistic in the `details` column do not show runtime plan statistics (for example, current CPU time). The recommendation details are taken at the time of regression detection and describe why [!INCLUDE[ssde_md](../../includes/ssde_md.md)] identified performance regression. Use `regressedPlanId` and `recommendedPlanId` to query [Query Store catalog views](../../relational-databases/performance/how-query-store-collects-data.md) to find exact runtime plan statistics.

## Examples of using tuning recommendations information  

### Example 1
The following gets the generated [!INCLUDE[tsql](../../includes/tsql-md.md)] script that forces a good plan for any given query:  
 
```sql
SELECT name, reason, score,
    JSON_VALUE(details, '$.implementationDetails.script') AS script,
    details.* 
FROM sys.dm_db_tuning_recommendations
CROSS APPLY OPENJSON(details, '$.planForceDetails')
    WITH (    [query_id] int '$.queryId',
            regressed_plan_id int '$.regressedPlanId',
            last_good_plan_id int '$.recommendedPlanId') AS details
WHERE JSON_VALUE(state, '$.currentValue') = 'Active';
```
### Example 2
The following gets the generated [!INCLUDE[tsql](../../includes/tsql-md.md)] script that forces a good plan for any given query and additional information about the estimated gain:

```sql
SELECT reason, score,
      script = JSON_VALUE(details, '$.implementationDetails.script'),
      planForceDetails.*,
      estimated_gain = (regressedPlanExecutionCount + recommendedPlanExecutionCount)
                  *(regressedPlanCpuTimeAverage - recommendedPlanCpuTimeAverage)/1000000,
      error_prone = IIF(regressedPlanErrorCount > recommendedPlanErrorCount, 'YES','NO')
FROM sys.dm_db_tuning_recommendations
CROSS APPLY OPENJSON (Details, '$.planForceDetails')
    WITH (  [query_id] int '$.queryId',
            regressedPlanId int '$.regressedPlanId',
            recommendedPlanId int '$.recommendedPlanId',
            regressedPlanErrorCount int,
            recommendedPlanErrorCount int,
            regressedPlanExecutionCount int,
            regressedPlanCpuTimeAverage float,
            recommendedPlanExecutionCount int,
            recommendedPlanCpuTimeAverage float
          ) AS planForceDetails;
```

### Example 3
The following gets the generated [!INCLUDE[tsql](../../includes/tsql-md.md)] script that forces a good plan for any given query and additional information that includes the query text and the query plans stored in Query Store:

```sql
WITH cte_db_tuning_recommendations
AS (SELECT reason,
        score,
        query_id,
        regressedPlanId,
        recommendedPlanId,
        current_state = JSON_VALUE(state, '$.currentValue'),
        current_state_reason = JSON_VALUE(state, '$.reason'),
        script = JSON_VALUE(details, '$.implementationDetails.script'),
        estimated_gain = (regressedPlanExecutionCount + recommendedPlanExecutionCount)
                * (regressedPlanCpuTimeAverage - recommendedPlanCpuTimeAverage)/1000000,
        error_prone = IIF(regressedPlanErrorCount > recommendedPlanErrorCount, 'YES','NO')
    FROM sys.dm_db_tuning_recommendations
    CROSS APPLY OPENJSON(Details, '$.planForceDetails')
    WITH ([query_id] int '$.queryId',
        regressedPlanId int '$.regressedPlanId',
        recommendedPlanId int '$.recommendedPlanId',
        regressedPlanErrorCount int,    
        recommendedPlanErrorCount int,
        regressedPlanExecutionCount int,
        regressedPlanCpuTimeAverage float,
        recommendedPlanExecutionCount int,
        recommendedPlanCpuTimeAverage float
        )
    )
SELECT qsq.query_id,
    qsqt.query_sql_text,
    dtr.*,
    CAST(rp.query_plan AS XML) AS RegressedPlan,
    CAST(sp.query_plan AS XML) AS SuggestedPlan
FROM cte_db_tuning_recommendations AS dtr
INNER JOIN sys.query_store_plan AS rp ON rp.query_id = dtr.query_id
    AND rp.plan_id = dtr.regressedPlanId
INNER JOIN sys.query_store_plan AS sp ON sp.query_id = dtr.query_id
    AND sp.plan_id = dtr.recommendedPlanId
INNER JOIN sys.query_store_query AS qsq ON qsq.query_id = rp.query_id
INNER JOIN sys.query_store_query_text AS qsqt ON qsqt.query_text_id = qsq.query_text_id;
```

For more information about JSON functions that can be used to query values in the recommendation view, see [JSON Support](../json/json-data-sql-server.md) in [!INCLUDE[ssde_md](../../includes/ssde_md.md)].
  
## Permissions  

Requires `VIEW SERVER STATE` permission in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].   
Requires the `VIEW DATABASE STATE` permission for the database in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].   

## See also  
 [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)   
 [sys.database_automatic_tuning_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)   
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [JSON Support](../json/json-data-sql-server.md)
 [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md)    
