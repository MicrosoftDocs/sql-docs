---
title: "sys.database_query_store_options (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/23/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "DATABASE_QUERY_STORE_OPTIONS_TSQL"
  - "DATABASE_QUERY_STORE_OPTIONS"
  - "SYS.DATABASE_QUERY_STORE_OPTIONS_TSQL"
  - "SYS.DATABASE_QUERY_STORE_OPTIONS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database_query_store_options catalog view"
  - "sys.database_query_store_options catalog view"
ms.assetid: 16b47d55-8019-41ff-ad34-1e0112178067
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_query_store_options (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

  Returns the Query Store options for this database.  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]), [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**desired_state**|**smallint**|Indicates the desired operation mode of Query Store, explicitly set by user.<br /> 0 = OFF <br /> 1 = READ_ONLY<br /> 2 = READ_WRITE|  
|**desired_state_desc**|**nvarchar(60)**|Textual description of the desired operation mode of Query Store:<br />OFF<br />READ_ONLY<br />READ_WRITE|  
|**actual_state**|**smallint**|Indicates the operation mode of Query Store. In addition to list of desired states required by the user, actual state can be an error state.<br /> 0 = OFF <br /> 1 = READ_ONLY<br /> 2 = READ_WRITE<br /> 3 = ERROR|  
|**actual_state_desc**|**nvarchar(60)**|Textual description of the actual operation mode of Query Store.<br />OFF<br />READ_ONLY<br />READ_WRITE<br />ERROR<br /><br /> There are situations when actual state is different from the desired state:<br />-  If the database is set to read-only mode or if Query Store size exceeds its configured quota, Query Store may operate in read-only mode even if read-write was specified by the user.<br />-  In extreme scenarios Query Store can enter an ERROR state because of internal errors. If this happens, Query Store can be recovered by executing the `sp_query_store_consistency_check` stored procedure in the affected database.|  
|**readonly_reason**|**int**|When the **desired_state_desc** is READ_WRITE and the **actual_state_desc** is READ_ONLY, **readonly_reason** returns a bit map to indicate why the Query Store is in readonly mode.<br /><br /> **1** - database is in read-only mode<br /><br /> **2** - database is in single-user mode<br /><br /> **4** - database is in emergency mode<br /><br /> **8** - database is secondary replica (applies to Always On and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] geo-replication). This value can be effectively observed only on **readable** secondary replicas<br /><br /> **65536** - the Query Store has reached the size limit set by the MAX_STORAGE_SIZE_MB option.<br /><br /> **131072** - The number of different statements in Query Store has reached the internal memory limit. Consider removing queries that you do not need or upgrading to a higher service tier to enable transferring Query Store to read-write mode.<br />**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> **262144** - Size of in-memory items waiting to be persisted on disk has reached the internal memory limit. Query Store will be  in read-only mode temporarily until the in-memory items are persisted on disk. <br />**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> **524288** - Database has reached disk size limit. Query Store is part of user database, so if there is no more available space for a database, that means that Query Store cannot grow further anymore.<br />**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. <br /> <br /> To switch the Query Store operations mode back to read-write, see **Verify Query Store is Collecting Query Data Continuously** section of [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md#Verify).|  
|**current_storage_size_mb**|**bigint**|Size of Query Store on disk in megabytes.|  
|**flush_interval_seconds**|**bigint**|The period for regular flushing of Query Store data to disk in seconds. Default value is **900** (15 min).<br /><br /> Change by using the `ALTER DATABASE <database> SET QUERY_STORE (DATA_FLUSH_INTERVAL_SECONDS  = <interval>)` statement.|  
|**interval_length_minutes**|**bigint**|The statistics aggregation interval in minutes. Arbitrary values are not allowed . Use one of the following: 1, 5, 10, 15, 30, 60, and 1440 minutes. The default value is **60** minutes.|  
|**max_storage_size_mb**|**bigint**|Maximum disk size for the Query Store in megabytes (MB). Default value is **100** MB.<br />For [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Premium edition, default is 1 GB and for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Basic edition, default is 10 MB.<br /><br /> Change by using the `ALTER DATABASE <database> SET QUERY_STORE (MAX_STORAGE_SIZE_MB = <size>)` statement.|  
|**stale_query_threshold_days**|**bigint**|Number of days that queries with no policy settings are kept in Query Store. Default value is **30**. Set to 0 to disable the retention policy.<br />For [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] Basic edition, default is 7 days.<br /><br /> Change by using the `ALTER DATABASE <database> SET QUERY_STORE ( CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = <value> ) )` statement.|  
|**max_plans_per_query**|**bigint**|Limits the maximum number of stored plans. Default value is **200**. If the maximum value is reached, Query Store stops capturing new plans for that query. Setting to 0 removes the limitation with regards to the number of captured plans.<br /><br /> Change by using the `ALTER DATABASE<database> SET QUERY_STORE (MAX_PLANS_PER_QUERY = <n>)` statement.|  
|**query_capture_mode**|**smallint**|The currently active query capture mode:<br /><br /> **1** = ALL - all queries are captured. This is the default configuration value for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).<br /><br /> 2 = AUTO - capture relevant queries based on execution count and resource consumption. This is the default configuration value for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].<br /><br /> 3 = NONE - stop capturing new queries. Query Store will continue to collect compile and runtime statistics for queries that were captured already. Use this configuration cautiously since you may miss to capture important queries.|  
|**query_capture_mode_desc**|**nvarchar(60)**|Textual description of the actual capture mode of Query Store:<br /><br /> ALL (default for [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)])<br /><br /> **AUTO** (default for [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)])<br /><br /> NONE|  
|**size_based_cleanup_mode**|**smallint**|Controls whether cleanup will be automatically activated when total amount of data gets close to maximum size:<br /><br /> 0 = OFF - size based cleanup won't be automatically activated.<br /><br /> **1** = AUTO - size based cleanup will be automatically activated when size on disk reaches **90 percent** of *max_storage_size_mb*. This is the default configuration value.<br /><br />Size based cleanup removes the least expensive and oldest queries first. It stops when approximately **80 percent** of *max_storage_size_mb* is reached.|  
|**size_based_cleanup_mode_desc**|**nvarchar(60)**|Textual description of the actual size-based cleanup mode of Query Store:<br /><br /> OFF <br /> **AUTO** (default)|  
|**wait_stats_capture_mode**|**smallint**|Controls whether Query Store performs capture of wait statistics: <br /><br /> 0 = OFF <br /> **1** = ON<br /> **Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|
|**wait_stats_capture_mode_desc**|**nvarchar(60)**|Textual description of the actual wait statistics capture mode: <br /><br /> OFF <br /> **ON** (default)<br /> **Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
  
## Permissions  
 Requires the `VIEW DATABASE STATE` permission.  
  
## See Also  
 [sys.query_context_settings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-context-settings-transact-sql.md)   
 [sys.query_store_plan &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)   
 [sys.query_store_query &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)   
 [sys.query_store_query_text &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql.md)   
 [sys.query_store_runtime_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql.md)   
 [sys.query_store_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-wait-stats-transact-sql.md)  
 [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.fn_stmt_sql_handle_from_sql_stmt &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)   
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)  
  
  
