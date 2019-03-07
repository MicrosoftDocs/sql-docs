---
title: "sys.dm_exec_sql_text (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_sql_text"
  - "sys.dm_exec_sql_text"
  - "sys.dm_exec_sql_text_TSQL"
  - "dm_exec_sql_text_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_sql_text dynamic management function"
ms.assetid: 61b8ad6a-bf80-490c-92db-58dfdff22a24
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_sql_text (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the text of the SQL batch that is identified by the specified *sql_handle*. This table-valued function replaces the system function **fn_get_sql**.  
  
 
## Syntax  
  
```  
sys.dm_exec_sql_text(sql_handle | plan_handle)  
```  
  
## Arguments  
*sql_handle*  
Is the SQL handle of the batch to be looked up. *sql_handle* is **varbinary(64)**. *sql_handle* can be obtained from the following dynamic management objects:  
  
-   [sys.dm_exec_query_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
  
-   [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  
  
-   [sys.dm_exec_cursors](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cursors-transact-sql.md)  
  
-   [sys.dm_exec_xml_handles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)  
  
-   [sys.dm_exec_query_memory_grants](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)  
  
-   [sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md)  
  
*plan_handle*  
Uniquely identifies a query plan for a batch that is cached or is currently executing. *plan_handle* is **varbinary(64)**. *plan_handle* can be obtained from the following dynamic management objects:  
  
-   [sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)  
  
-   [sys.dm_exec_query_stats](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
  
-   [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**dbid**|**smallint**|ID of database.<br /><br /> For ad hoc and prepared SQL statements, the ID of the database where the statements were compiled.|  
|**objectid**|**int**|ID of object.<br /><br /> Is NULL for ad hoc and prepared SQL statements.|  
|**number**|**smallint**|For a numbered stored procedure, this column returns the number of the stored procedure. For more information, see [sys.numbered_procedures &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-numbered-procedures-transact-sql.md).<br /><br /> Is NULL for ad hoc and prepared SQL statements.|  
|**encrypted**|**bit**|1 = SQL text is encrypted.<br /><br /> 0 = SQL text is not encrypted.|  
|**text**|**nvarchar(max** **)**|Text of the SQL query.<br /><br /> Is NULL for encrypted objects.|  
  
## Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  
  
## Remarks  
For ad hoc queries, the SQL handles are hash values based on the SQL text being submitted to the server, and can originate from any database. 

For database objects such as stored procedures, triggers or functions, the SQL handles are derived from the database ID, object ID, and object number. 

Plan handle is a hash value derived from the compiled plan of the entire batch. 

> [!NOTE]
> **dbid** cannot be determined from *sql_handle* for ad hoc queries. To determine **dbid** for ad hoc queries, use *plan_handle* instead.
  
## Examples 

### A. Conceptual Example
The following is a basic example to illustrate passing a **sql_handle** either directly or with **CROSS APPLY**.
  1.  Create activity.  
Execute the following T-SQL in a new query window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].   
      ```sql
      -- Identify current spid (session_id)
      SELECT @@SPID;
      GO
  
      -- Create activity
        WAITFOR DELAY '00:02:00';
      ```
      
    2.  Using **CROSS APPLY**.  
    The sql_handle from **sys.dm_exec_requests** will be passed to **sys.dm_exec_sql_text** using **CROSS APPLY**. Open a new query window and pass the spid identified in step 1. In this example the spid happens to be `59`.

        ```sql
        SELECT t.*
        FROM sys.dm_exec_requests AS r
        CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) AS t
        WHERE session_id = 59 -- modify this value with your actual spid
         ```      
 
    2.  Passing **sql_handle** directly.  
Acquire the **sql_handle** from **sys.dm_exec_requests**. Then, pass the **sql_handle** directly to **sys.dm_exec_sql_text**. Open a new query window and pass the spid identified in step 1 to **sys.dm_exec_requests**. In this example the spid happens to be `59`. Then pass the returned **sql_handle** as an argument to **sys.dm_exec_sql_text**.

        ```sql
        -- acquire sql_handle
        SELECT sql_handle FROM sys.dm_exec_requests WHERE session_id = 59  -- modify this value with your actual spid
        
        -- pass sql_handle to sys.dm_exec_sql_text
        SELECT * FROM sys.dm_exec_sql_text(0x01000600B74C2A1300D2582A2100000000000000000000000000000000000000000000000000000000000000) -- modify this value with your actual sql_handle
         ```      
    
  
### B. Obtain information about the top five queries by average CPU time  
 The following example returns the text of the SQL statement and average CPU time for the top five queries.  
  
```sql  
SELECT TOP 5 total_worker_time/execution_count AS [Avg CPU Time],  
    SUBSTRING(st.text, (qs.statement_start_offset/2)+1,   
        ((CASE qs.statement_end_offset  
          WHEN -1 THEN DATALENGTH(st.text)  
         ELSE qs.statement_end_offset  
         END - qs.statement_start_offset)/2) + 1) AS statement_text  
FROM sys.dm_exec_query_stats AS qs  
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st  
ORDER BY total_worker_time/execution_count DESC;  
```  
  
### C. Provide batch-execution statistics  
 The following example returns the text of SQL queries that are being executed in batches and provides statistical information about them.  
  
```sql  
SELECT s2.dbid,   
    s1.sql_handle,    
    (SELECT TOP 1 SUBSTRING(s2.text,statement_start_offset / 2+1 ,   
      ( (CASE WHEN statement_end_offset = -1   
         THEN (LEN(CONVERT(nvarchar(max),s2.text)) * 2)   
         ELSE statement_end_offset END)  - statement_start_offset) / 2+1))  AS sql_statement,  
    execution_count,   
    plan_generation_num,   
    last_execution_time,     
    total_worker_time,   
    last_worker_time,   
    min_worker_time,   
    max_worker_time,  
    total_physical_reads,   
    last_physical_reads,   
    min_physical_reads,    
    max_physical_reads,    
    total_logical_writes,   
    last_logical_writes,   
    min_logical_writes,   
    max_logical_writes    
FROM sys.dm_exec_query_stats AS s1   
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS s2    
WHERE s2.objectid is null   
ORDER BY s1.sql_handle, s1.statement_start_offset, s1.statement_end_offset;  
```  
  
## See also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
 [sys.dm_exec_cursors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cursors-transact-sql.md)   
 [sys.dm_exec_xml_handles &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)   
 [sys.dm_exec_query_memory_grants &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)   
 [Using APPLY](../../t-sql/queries/from-transact-sql.md#using-apply)   
 [sys.dm_exec_text_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-text-query-plan-transact-sql.md)  

