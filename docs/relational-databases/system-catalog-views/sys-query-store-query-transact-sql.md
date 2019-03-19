---
title: "sys.query_store_query (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/23/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "QUERY_STORE_QUERY"
  - "SYS.QUERY_STORE_QUERY_TSQL"
  - "SYS.QUERY_STORE_QUERY"
  - "QUERY_STORE_QUERY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "query_store_query catalog view"
  - "sys.query_store_query catalog view"
ms.assetid: bdee149e-7556-4fc3-8242-925dd4b7b6ac
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.query_store_query (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

  Contains  information about the query and its associated overall aggregated runtime execution statistics.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**query_id**|**bigint**|Primary key.|  
|**query_text_id**|**bigint**|Foreign key. Joins to [sys.query_store_query_text &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql.md)|  
|**context_settings_id**|**bigint**|Foreign key. Joins to [sys.query_context_settings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-context-settings-transact-sql.md).<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**object_id**|**bigint**|ID of the database object that the query is part of (stored procedure, trigger, CLR UDF/UDAgg, etc.). 0 if the query is not executed as part of a database object (ad-hoc query).<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**batch_sql_handle**|**varbinary(64)**|ID of the statement batch the query is part of. Populated only if query references temporary tables or table variables.<br/>**Note:** Azure SQL Data Warehouse will always return *NULL*.|  
|**query_hash**|**binary(8)**|MD5 hash of the individual query, based on the logical query tree. Includes optimizer hints.|  
|**is_internal_query**|**bit**|The query was generated internally.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**query_parameterization_type**|**tinyint**|Kind of parameterization:<br /><br /> 0 - None<br /><br /> 1 - User<br /><br /> 2 - Simple<br /><br /> 3 - Forced<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**query_parameterization_type_desc**|**nvarchar(60)**|Textual description for the parameterization type.<br/>**Note:** Azure SQL Data Warehouse will always return *None*.|  
|**initial_compile_start_time**|**datetimeoffset**|Compile start time.|  
|**last_compile_start_time**|**datetimeoffset**|Compile start time.|  
|**last_execution_time**|**datetimeoffset**|Last execution time refers to the last end time of the query/plan.|  
|**last_compile_batch_sql_handle**|**varbinary(64)**|Handle of the last SQL batch in which query was used last time. It can be provided as input to [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md) to get the full text of the batch.<br/>**Note:** Azure SQL Data Warehouse will always return *NULL*.|  
|**last_compile_batch_offset_start**|**bigint**|Information that can be provided to sys.dm_exec_sql_text along with last_compile_batch_sql_handle.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**last_compile_batch_offset_end**|**bigint**|Information that can be provided to sys.dm_exec_sql_text along with last_compile_batch_sql_handle.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**count_compiles**|**bigint**|Compilation statistics.<br/>**Note:** Azure SQL Data Warehouse will always return one (1).|  
|**avg_compile_duration**|**float**|Compilation statistics in microseconds.|  
|**last_compile_duration**|**bigint**|Compilation statistics in microseconds.|  
|**avg_bind_duration**|**float**|Binding statistics in microseconds.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**last_bind_duration**|**bigint**|Binding statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**avg_bind_cpu_time**|**float**|Binding statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**last_bind_cpu_time**|**bigint**|Binding statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|  
|**avg_optimize_duration**|**float**|Optimization statistics in microseconds.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**last_optimize_duration**|**bigint**|Optimization statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**avg_optimize_cpu_time**|**float**|Optimization statistics in microseconds.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**last_optimize_cpu_time**|**bigint**|Optimization statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**avg_compile_memory_kb**|**float**|Compile memory statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**last_compile_memory_kb**|**bigint**|Compile memory statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**max_compile_memory_kb**|**bigint**|Compile memory statistics.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
|**is_clouddb_internal_query**|**bit**|Always 0 in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on-premises.<br/>**Note:** Azure SQL Data Warehouse will always return zero (0).|
  
## Permissions  
 Requires the **VIEW DATABASE STATE** permission.  
  
## See Also  
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [sys.query_context_settings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-context-settings-transact-sql.md)   
 [sys.query_store_plan &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)   
 [sys.query_store_query_text &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql.md)   
 [sys.query_store_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-wait-stats-transact-sql.md)  
 [sys.query_store_runtime_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql.md)   
 [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)   
 [sys.fn_stmt_sql_handle_from_sql_stmt &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)  
  
  
