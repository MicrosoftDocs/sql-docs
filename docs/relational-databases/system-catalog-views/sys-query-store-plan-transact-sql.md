---
title: "sys.query_store_plan (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/23/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "QUERY_STORE_PLAN_TSQL"
  - "SYS.QUERY_STORE_PLAN"
  - "SYS.QUERY_STORE_PLAN_TSQL"
  - "QUERY_STORE_PLAN"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "query_store_plan catalog view"
  - "sys.query_store_plan catalog view"
ms.assetid: b4d05439-6360-45db-b1cd-794f4a64935e
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.query_store_plan (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

  Contains information about each execution plan associated with a query.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**plan_id**|**bigint**|Primary key.|  
|**query_id**|**bigint**|Foreign key. Joins to [sys.query_store_query &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md).|  
|**plan_group_id**|**bigint**|ID of the plan group. Cursor queries typically require multiple (populate and fetch) plans. Populate and fetch plans that are compiled together are in the same group.<br /><br /> 0 means plan is not in a group.|  
|**engine_version**|**nvarchar(32)**|Version of the engine used to compile the plan in **'major.minor.build.revision'** format.|  
|**compatibility_level**|**smallint**|Database compatibility level of the database referenced in the query.|  
|**query_plan_hash**|**binary(8)**|MD5 hash of the individual plan.|  
|**query_plan**|**nvarchar(max)**|Showplan XML for the query plan.|  
|**is_online_index_plan**|**bit**|Plan was used during an online index build. <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**is_trivial_plan**|**bit**|Plan is a trivial plan (output in stage 0 of query optimizer). <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**is_parallel_plan**|**bit**|Plan is parallel. <br/>**Note:** Azure SQL Data Warehouse will always return one (1).|  
|**is_forced_plan**|**bit**|Plan is marked as forced when user executes stored procedure **sys.sp_query_store_force_plan**. Forcing mechanism *does not guarantee* that exactly this plan will be used for the query referenced by **query_id**. Plan forcing causes query to be compiled again and typically produces exactly the same or similar plan to the plan referenced by **plan_id**. If plan forcing does not succeed, **force_failure_count** is incremented and **last_force_failure_reason** is populated with the failure reason. <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**is_natively_compiled**|**bit**|Plan includes natively compiled memory optimized procedures. (0 = FALSE, 1 = TRUE). <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**force_failure_count**|**bigint**|Number of times that forcing this plan has failed. It can be incremented only when the query is recompiled (*not on every execution*). It is reset to 0 every time **is_plan_forced** is changed from **FALSE** to **TRUE**. <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**last_force_failure_reason**|**int**|Reason why plan forcing failed.<br /><br /> 0: no failure, otherwise error number of the error that caused the forcing to fail<br /><br /> 8637: ONLINE_INDEX_BUILD<br /><br /> 8683: INVALID_STARJOIN<br /><br /> 8684: TIME_OUT<br /><br /> 8689: NO_DB<br /><br /> 8690: HINT_CONFLICT<br /><br /> 8691: SETOPT_CONFLICT<br /><br /> 8694: DQ_NO_FORCING_SUPPORTED<br /><br /> 8698: NO_PLAN<br /><br /> 8712: NO_INDEX<br /><br /> 8713: VIEW_COMPILE_FAILED<br /><br /> \<other value>: GENERAL_FAILURE <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**last_force_failure_reason_desc**|**nvarchar(128)**|Textual description of last_force_failure_reason_desc.<br /><br /> ONLINE_INDEX_BUILD: query tries to modify data while target table has an index that is being built online<br /><br /> INVALID_STARJOIN: plan contains invalid StarJoin specification<br /><br /> TIME_OUT: Optimizer exceeded number of allowed operations while searching for plan specified by forced plan<br /><br /> NO_DB: A database specified in the plan does not exist<br /><br /> HINT_CONFLICT: Query cannot be compiled because plan conflicts with a query hint<br /><br /> DQ_NO_FORCING_SUPPORTED: Cannot execute query because plan conflicts with use of distributed query or full-text operations.<br /><br /> NO_PLAN: Query processor could not produce query plan because forced plan could not be verified to be valid for the query<br /><br /> NO_INDEX: Index specified in plan no longer exists<br /><br /> VIEW_COMPILE_FAILED: Could not force query plan because of a problem in an indexed view referenced in the plan<br /><br /> GENERAL_FAILURE: general forcing error (not covered with reasons above) <br/>**Note:** Azure SQL Data Warehouse will always return *NONE*.|  
|**count_compiles**|**bigint**|Plan compilation statistics.|  
|**initial_compile_start_time**|**datetimeoffset**|Plan compilation statistics.|  
|**last_compile_start_time**|**datetimeoffset**|Plan compilation statistics.|  
|**last_execution_time**|**datetimeoffset**|Last execution time refers to the last end time of the query/plan.|  
|**avg_compile_duration**|**float**|Plan compilation statistics. <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**last_compile_duration**|**bigint**|Plan compilation statistics. <br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**plan_forcing_type**|**int**|Plan forcing type.<br /><br />0: NONE<br /><br />1: MANUAL<br /><br />2: AUTO|  
|**plan_forcing_type_desc**|**nvarchar(60)**|Text description of plan_forcing_type.<br /><br />NONE: No plan forcing<br /><br />MANUAL: Plan forced by user<br /><br />AUTO: Plan forced by automatic tuning|  

## Plan forcing limitations
Query Store has a mechanism to enforce Query Optimizer to use certain execution plan. 
However, there are some limitations that can prevent a plan to be enforced. 

First, if the plan contains following constructions:
* Insert bulk statement.
* Reference to an external table
* Distributed query or full-text operations
* Use of Global queries 
* Cursors
* Invalid star join specification 

Second, when objects that plan relies on, are no longer available:
* Database (if Database, where plan originated, does not exist anymore)
* Index (no longer there or disabled)

Finally, problems with the plan itself:
* Not legal for query
* Query Optimizer exceeded number of allowed operations
* Incorrectly formed plan XML

## Permissions  
 Requires the **VIEW DATABASE STATE** permission.  
  
## See Also  
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [sys.query_context_settings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-context-settings-transact-sql.md)   
 [sys.query_store_query &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)   
 [sys.query_store_query_text &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql.md)   
 [sys.query_store_runtime_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql.md)   
 [sys.query_store_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-wait-stats-transact-sql.md)  
 [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)  
  
  
