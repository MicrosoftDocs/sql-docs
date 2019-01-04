---
title: "sys.query_context_settings (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/29/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "QUERY_CONTEXT_SETTINGS_TSQL"
  - "SYS.QUERY_CONTEXT_SETTINGS_TSQL"
  - "SYS.QUERY_CONTEXT_SETTINGS"
  - "QUERY_CONTEXT_SETTINGS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.query_context_settings catalog view"
ms.assetid: 3c1887df-6bd8-491e-82fc-d25ad9589faf
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.query_context_settings (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

  Contains  information about the semantics affecting context settings associated with a query. There are a number of context settings available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that influence the query semantics (defining the correct result of the query). The same query text compiled under different settings may produce different results (depending on the underlying data).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**context_settings_id**|**bigint**|Primary key. This value is exposed in Showplan XML for queries.|  
|**set_options**|**varbinary(8)**|Bit mask reflecting state of several SET options. For more information, see [sys.dm_exec_plan_attributes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md).|  
|**language_id**|**smallint**|The id of the language. For more information, see [sys.syslanguages &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md).|  
|**date_format**|**smallint**|The date format. For more information, see [SET DATEFORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/set-dateformat-transact-sql.md).|  
|**date_first**|**tinyint**|The date first value. For more information, see [SET DATEFIRST &#40;Transact-SQL&#41;](../../t-sql/statements/set-datefirst-transact-sql.md).|  
|**status**|**varbinary(2)**|Bitmask field that indicates type of query or context in which query was executed. <br />Column value can be combination of multiple flags (expressed in hexadecimal):<br /><br /> 0x0 - regular query (no specific flags)<br /><br /> 0x1 - query that was executed through one of the cursor APIs stored procedures<br /><br /> 0x2 - query for notification<br /><br /> 0x4 - internal query<br /><br /> 0x8 - auto parameterized query without universal parameterization<br /><br /> 0x10 - cursor fetch refresh query<br /><br /> 0x20 - query that is being used in cursor update requests<br /><br /> 0x40 - initial result set is returned when a cursor is opened (Cursor Auto Fetch)<br /><br /> 0x80 - encrypted query<br /><br /> 0x100 - query in context of row-level security predicate|  
|**required_cursor_options**|**int**|Cursor options specified by the user such as the cursor type.|  
|**acceptable_cursor_options**|**int**|Cursor options that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may implicitly convert to in order to support the execution of the statement.|  
|**merge_action_type**|**smallint**|The type of trigger execution plan used as the result of a **MERGE** statement.<br /><br /> 0 indicates a non-trigger plan, a trigger plan that does not execute as the result of a **MERGE** statement, or a trigger plan that executes as the result of a **MERGE** statement that only specifies a **DELETE** action.<br /><br /> 1 indicates an **INSERT** trigger plan that runs as the result of a **MERGE** statement.<br /><br /> 2 indicates an **UPDATE** trigger plan that runs as the result of a **MERGE** statement.<br /><br /> 3 indicates a **DELETE** trigger plan that runs as the result of a **MERGE** statement containing a corresponding **INSERT** or **UPDATE** action.<br /><br /> <br /><br /> For nested triggers run by cascading actions, this value is the action of the **MERGE** statement that caused the cascade.|  
|**default_schema_id**|**int**|ID of the default schema, which is used to resolve names that are not fully qualified.|  
|**is_replication_specific**|**bit**|Used for replication.|  
|**is_contained**|**varbinary(1)**|1 indicates a contained database.|  
  
## Permissions  
 Requires the **VIEW DATABASE STATE** permission.  
  
## See Also  
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [sys.query_store_plan &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)   
 [sys.query_store_query &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)   
 [sys.query_store_query_text &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql.md)   
 [sys.query_store_runtime_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql.md)   
 [sys.query_store_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-wait-stats-transact-sql.md)   
 [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)   
 [sys.fn_stmt_sql_handle_from_sql_stmt &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)  
  
  
