---
title: "sys.dm_exec_plan_attributes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_exec_plan_attributes_TSQL"
  - "dm_exec_plan_attributes_TSQL"
  - "dm_exec_plan_attributes"
  - "sys.dm_exec_plan_attributes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_plan_attributes dynamic management function"
ms.assetid: dacf3ab3-f214-482e-aab5-0dab9f0a3648
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_exec_plan_attributes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row per plan attribute for the plan specified by the plan handle. You can use this table-valued function to get details about a particular plan, such as the cache key values or the number of current simultaneous executions of the plan.  
  
> [!NOTE]  
>  Some of the information returned through this function maps to the [sys.syscacheobjects](../../relational-databases/system-compatibility-views/sys-syscacheobjects-transact-sql.md) backward compatibility view.

## Syntax  
```  
sys.dm_exec_plan_attributes ( plan_handle )  
```  
  
## Arguments  
 *plan_handle*  
 Uniquely identifies a query plan for a batch that has executed and whose plan resides in the plan cache. *plan_handle* is **varbinary(64)**. The plan handle can be obtained from the [sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md) dynamic management view.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|attribute|**varchar(128)**|Name of the attribute associated with this plan. The table immediately below this one lists the possible attributes, their data types, and their descriptions.|  
|value|**sql_variant**|Value of the attribute that is associated with this plan.|  
|is_cache_key|**bit**|Indicates whether the attribute is used as part of the cache lookup key for the plan.|  

From the above table, **attribute** can have the following values:

|Attribute|Data type|Description|  
|---------------|---------------|-----------------|  
|set_options|**int**|Indicates the option values that the plan was compiled with.|  
|objectid|**int**|One of the main keys used for looking up an object in the cache. This is the object ID stored in [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) for database objects (procedures, views, triggers, and so on). For plans of type "Adhoc" or "Prepared", it is an internal hash of the batch text.|  
|dbid|**int**|Is the ID of the database containing the entity the plan refers to.<br /><br /> For ad hoc or prepared plans, it is the database ID from which the batch is executed.|  
|dbid_execute|**int**|For system objects stored in the **Resource** database, the database ID from which the cached plan is executed. For all other cases, it is 0.|  
|user_id|**int**|Value of -2 indicates that the batch submitted does not depend on implicit name resolution and can be shared among different users. This is the preferred method. Any other value represents the user ID of the user submitting the query in the database.| 
|language_id|**smallint**|ID of the language of the connection that created the cache object. For more information, see [sys.syslanguages &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md).|  
|date_format|**smallint**|Date format of the connection that created the cache object. For more information, see [SET DATEFORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/set-dateformat-transact-sql.md).|  
|date_first|**tinyint**|Date first value. For more information, see [SET DATEFIRST &#40;Transact-SQL&#41;](../../t-sql/statements/set-datefirst-transact-sql.md).|  
|status|**int**|Internal status bits that are part of the cache lookup key.|  
|required_cursor_options|**int**|Cursor options specified by the user such as the cursor type.|  
|acceptable_cursor_options|**int**|Cursor options that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may implicitly convert to in order to support the execution of the statement. For example, the user may specify a dynamic cursor, but the query optimizer is permitted to convert this cursor type to a static cursor.|  
|inuse_exec_context|**int**|Number of currently executing batches that are using the query plan.|  
|free_exec_context|**int**|Number of cached execution contexts for the query plan that are not being currently used.|  
|hits_exec_context|**int**|Number of times the execution context was obtained from the plan cache and reused, saving the overhead of recompiling the SQL statement. The value is an aggregate for all batch executions so far.|  
|misses_exec_context|**int**|Number of times that an execution context could not be found in the plan cache, resulting in the creation of a new execution context for the batch execution.|  
|removed_exec_context|**int**|Number of execution contexts that have been removed because of memory pressure on the cached plan.|  
|inuse_cursors|**int**|Number of currently executing batches containing one or more cursors that are using the cached plan.|  
|free_cursors|**int**|Number of idle or free cursors for the cached plan.|  
|hits_cursors|**int**|Number of times that an inactive cursor was obtained from the cached plan and reused. The value is an aggregate for all batch executions so far.|  
|misses_cursors|**int**|Number of times that an inactive cursor could not be found in the cache.|  
|removed_cursors|**int**|Number of cursors that have been removed because of memory pressure on the cached plan.|  
|sql_handle|**varbinary**(64)|The SQL handle for the batch.|  
|merge_action_type|**smallint**|The type of trigger execution plan used as the result of a MERGE statement.<br /><br /> 0 indicates a non-trigger plan, a trigger plan that does not execute as the result of a MERGE statement, or a trigger plan that executes as the result of a MERGE statement that only specifies a DELETE action.<br /><br /> 1 indicates an INSERT trigger plan that runs as the result of a MERGE statement.<br /><br /> 2 indicates an UPDATE trigger plan that runs as the result of a MERGE statement.<br /><br /> 3 indicates a DELETE trigger plan that runs as the result of a MERGE statement containing a corresponding INSERT or UPDATE action.<br /><br /> For nested triggers run by cascading actions, this value is the action of the MERGE statement that caused the cascade.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Remarks  
  
## Set Options  
 Copies of the same compiled plan might differ only by the value in the **set_options** column. This indicates that different connections are using different sets of SET options for the same query. Using different sets of options is usually undesirable because it can cause extra compilations, less plan reuse, and plan cache inflation because of multiple copies of plans in the cache.  
  
### Evaluating Set Options  
 To translate the value returned in **set_options** to the options with which the plan was compiled, subtract the values from the **set_options** value, starting with the largest possible value, until you reach 0. Each value you subtract corresponds to an option that was used in the query plan. For example, if the value in **set_options** is 251, the options the plan was compiled with are ANSI_NULL_DFLT_ON (128), QUOTED_IDENTIFIER (64), ANSI_NULLS(32), ANSI_WARNINGS (16), CONCAT_NULL_YIELDS_NULL (8), Parallel Plan(2) and ANSI_PADDING (1).  
  
|Option|Value|  
|------------|-----------|  
|ANSI_PADDING|1|  
|Parallel Plan|2|  
|FORCEPLAN|4|  
|CONCAT_NULL_YIELDS_NULL|8|  
|ANSI_WARNINGS|16|  
|ANSI_NULLS|32|  
|QUOTED_IDENTIFIER|64|  
|ANSI_NULL_DFLT_ON|128|  
|ANSI_NULL_DFLT_OFF|256|  
|NoBrowseTable<br /><br /> Indicates that the plan does not use a work table to implement a FOR BROWSE operation.|512|  
|TriggerOneRow<br /><br /> Indicates that the plan contains single row optimization for AFTER trigger delta tables.|1024|  
|ResyncQuery<br /><br /> Indicates that the query was submitted by internal system stored procedures.|2048|  
|ARITH_ABORT|4096|  
|NUMERIC_ROUNDABORT|8192|  
|DATEFIRST|16384|  
|DATEFORMAT|32768|  
|LanguageID|65536|  
|UPON<br /><br /> Indicates that the database option PARAMETERIZATION was set to FORCED when the plan was compiled.|131072|  
|ROWCOUNT|**Applies To:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]<br /><br /> 262144|  
  
## Cursors  
 Inactive cursors are cached in a compiled plan so that the memory used to store the cursor can be reused by concurrent users of cursors. For example, suppose that a batch declares and uses a cursor without deallocating it. If there are two users executing the same batch, there will be two active cursors. Once the cursors are deallocated (potentially in different batches), the memory used to store the cursor is cached and not released. This list of inactive cursors is kept in the compiled plan. The next time a user executes the batch, the cached cursor memory will be reused and initialized appropriately as an active cursor.  
  
### Evaluating Cursor Options  
 To translate the value returned in **required_cursor_options** and **acceptable_cursor_options** to the options with which the plan was compiled, subtract the values from the column value, starting with the largest possible value, until you reach 0. Each value you subtract corresponds to a cursor option that was used in the query plan.  
  
|Option|Value|  
|------------|-----------|  
|None|0|  
|INSENSITIVE|1|  
|SCROLL|2|  
|READ ONLY|4|  
|FOR UPDATE|8|  
|LOCAL|16|  
|GLOBAL|32|  
|FORWARD_ONLY|64|  
|KEYSET|128|  
|DYNAMIC|256|  
|SCROLL_LOCKS|512|  
|OPTIMISTIC|1024|  
|STATIC|2048|  
|FAST_FORWARD|4096|  
|IN PLACE|8192|  
|FOR *select_statement*|16384|  
  
## Examples  
  
### A. Returning the attributes for a specific plan  
 The following example returns all plan attributes for a specified plan. The `sys.dm_exec_cached_plans` dynamic management view is queried first to obtain the plan handle for the specified plan. In the second query, replace `<plan_handle>` with a plan handle value from the first query.  
  
```sql  
SELECT plan_handle, refcounts, usecounts, size_in_bytes, cacheobjtype, objtype   
FROM sys.dm_exec_cached_plans;  
GO  
SELECT attribute, value, is_cache_key  
FROM sys.dm_exec_plan_attributes(<plan_handle>);  
GO  
```  
  
### B. Returning the SET options for compiled plans and the SQL handle for cached plans  
 The following example returns a value representing the options that each plan was compiled with. In addition, the SQL handle for all the cached plans is returned.  
  
```sql  
SELECT plan_handle, pvt.set_options, pvt.sql_handle  
FROM (  
    SELECT plan_handle, epa.attribute, epa.value   
    FROM sys.dm_exec_cached_plans   
        OUTER APPLY sys.dm_exec_plan_attributes(plan_handle) AS epa  
    WHERE cacheobjtype = 'Compiled Plan') AS ecpa   
PIVOT (MAX(ecpa.value) FOR ecpa.attribute IN ("set_options", "sql_handle")) AS pvt;  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)  
  
  

