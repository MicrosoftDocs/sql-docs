---
title: "sys.dm_db_tuning_recommendations (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_tuning_recommendations"
  - "dm_db_tuning_recommendations"
  - "sys.dm_db_tuning_recommendations_TSQL"
  - "dm_db_tuning_recommendations_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database tuning recommendations feature [SQL Server], sys.dm_db_tuning_recommendations dynamic management view"
  - "sys.dm_db_tuning_recommendations dynamic management view"
ms.assetid: ced484ae-7c17-4613-a3f9-6d8aba65a110
caps.latest.revision: 37
author: "jovanpop-msft"
ms.author: "jovanpop-msft"
manager: "jhubbard"
---
# sys.dm\_db\_tuning\_recommendations (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns detailed information about tuning recomendations.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn’t belong to the connected tenant is filtered out.

| **Column name** | **Data type** | **Description** |
| --- | --- | --- |
| name | nvarchar(4000) | Unique name of recommendation. |
| type | nvarchar(4000) | FORCE\_LAST\_GOOD\_PLAN |
| reason | nvarchar(4000) | Reason why this recommendation was provided. |
| valid\_since | datetime2 | The first time this recommendation was generated |
| last\_refresh | datetime2 | The last time this recommendation was generated |
| state | JSONnvarchar(4000) | JSON document that describes the state of the recommendation. Following fields are available:<br />
currentValue (e.g. Active, Verifying, Reverted)<br />
reason – code that describes why the recommendation is in the current state.
 |
| is\_executable\_action | bit | 1 = The recommendation can be executed against the database via [!INCLUDE[tsql_md](../../includes/tsql_md.md)] script.<br />0 = The recommendation cannot be executed against the database (for example: information only or reverted recommendation) |
| is\_revertable\_action | bit | 1 = The recommendation can be automatically monitored and reverted by Database engine.0 = The recommendation cannot be automatically monitored and reverted. Most &quot;executable&quot; actions will also be &quot;revertable&quot;. |
| execute\_action\_start\_time | datetime2 | Date the recommendation is applied. |
| execute\_action\_duration | time | Duration of the execute action. |
| execute\_action\_initiated\_by | nvarchar(4000) | User or System |
| execute\_action\_initiated\_time | datetime2 | Date the recommendation is applied. |
| revert\_action\_start\_time | datetime2 | Date the recommendation is reverted. |
| revert\_action\_duration | time | Duration of the revert action. |
| revert\_action\_initiated\_by | nvarchar(4000) | User or System |
| revert\_action\_initiated\_time | datetime2 | Date the recommendation is reverted. |
| score | int | Estimated value/impact for this recommendation on the 0-100 scale (the larger the better) |
| details (a property bag that each advisor can customize) | nvarchar(max) | JSON document that contains more details about the recommendation. Following fields are available:<br />
- planForceDetails<br />
 - queryId - query\_id of the regressed query.<br />
 - regressedPlanId - plan_id of the regressed plan.<br />
 - regressedPlanExecutionCount - Number of execution of the query with regressed plan until the regression is detected.<br />
 - regressedPlanAbortedCount<br />
 - regressedPlanCpuTimeAverage - Average CPU time consumed by the regressed query until the regression is detected.<br />
 - regressedPlanCpuTimeStddev - Standard deviation of CPU time consumed by the regressed query until the regression is detected.<br />
 - forcedPlanId - plan_id of the plan that should be forced.<br />
 - forcedPlanExecutionCount - Number of execution of the query with the plan that should be forced until the regression is detected.<br />
 - forcedPlanAbortedCount<br />
 - forcedPlanCpuTimeAverage - Average CPU time consumed by the query executed with the plan that should be (calculated until the regression is detected).<br />
 - forcedPlanCpuTimeStddev Standard deviation of CPU time consumed by the regressed query until the regression is detected.<br />
- implementationDetails<br />
 - method - The method that should be used to correct the regression.<br />
 - script - [!INCLUDE[tsql_md](../../includes/tsql_md.md)] script that shoudl be executed to force the recommended plan. |
  
## Remarks  
 Information returned by **sys.dm\_db\_tuning\_recommendations** is updated when database engine identifies potential query performance regression, and is not persisted. Recommendations are kept only until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. Database administrators should periodically make backup copies of the tuning recommendation if they want to keep it after server recycling.  
  
## Using tuning recommendations information  
 To convert the recommendation returned by **sys.dm\_db\_tuning\_recommendations** into an information that can be used to see the impact of recommendation and to get the T-SQL script that will fix the issue, you can use the following query:  
 
```
SELECT name, reason, score,
		JSON_VALUE(details, '$.implementationDetails.script') as script,
		details.* 
FROM sys.dm_db_tuning_recommendations
	CROSS APPLY OPENJSON(details, '$.planForceDetails')
				WITH (	query_id int '$.queryId',
						regressed_plan_id int '$.regressedPlanId',
						last_good_plan_id int '$.forcedPlanId') as details
WHERE JSON_VALUE(state, '$.currentValue') = 'Active'
```
  
 For more information about memory-optimized indexes, see [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md).
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Premium Tiers, requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Standard and Basic Tiers, requires the  **Server admin** or an **Azure Active Directory admin** account.  
  
## See Also  
 [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)
 [sys.database_automatic_tuning_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)