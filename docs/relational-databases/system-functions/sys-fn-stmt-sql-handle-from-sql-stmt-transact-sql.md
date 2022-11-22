---
description: "sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)"
title: "sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 6794e073-0895-4507-aba3-c3545acc843f
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Gets the **stmt_sql_handle** for a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement under given parameterization type (simple or forced). This allows you to refer to queries stored in the Query Store by using their **stmt_sql_handle** when you know their text.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sys.fn_stmt_sql_handle_from_sql_stmt   
(  
    'query_sql_text',   
    [ query_param_type   
) [;]  
```  
  
## Arguments  
 *query_sql_text*  
 Is the text of the query in the query store that you want the handle of. *query_sql_text* is a **nvarchar(max)**, with no default.  
  
 *query_param_type*  
 Is the parameter type of the query. *query_param_type* is a **tinyint**. Possible values are:  
  
-   NULL - defaults to 0  
  
-   0 - None  
  
-   1 - User  
  
-   2 - Simple  
  
-   3 - Forced  
  
## Columns Returned  
 The following table lists the columns that sys.fn_stmt_sql_handle_from_sql_stmt returns.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|**statement_sql_handle**|**varbinary(64)**|The SQL handle.|  
|**query_sql_text**|**nvarchar(max)**|The text of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**query_parameterization_type**|**tinyint**|The query parameterization type.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
  
## Permissions  
 Requires the **EXECUTE** permission on the database, and **DELETE** permission on the query store catalog views.  
  
## Examples  
 The following example executes a statement, and then uses `sys.fn_stmt_sql_handle_from_sql_stmt` to return the SQL handle of that statement.  
  
```  
SELECT * FROM sys.databases;   
SELECT * FROM sys.fn_stmt_sql_handle_from_sql_stmt('SELECT * FROM sys.databases', NULL);  
```  
  
 Use the function to correlate Query Store data with other dynamic management views. The following example:  
  
```  
SELECT qt.query_text_id, q.query_id, qt.query_sql_text, qt.statement_sql_handle,  
q.context_settings_id, qs.statement_context_id   
FROM sys.query_store_query_text AS qt  
JOIN sys.query_store_query AS q   
    ON qt.query_text_id = q.query_id  
CROSS APPLY sys.fn_stmt_sql_handle_from_sql_stmt (qt.query_sql_text, null) AS fn_handle_from_stmt  
JOIN sys.dm_exec_query_stats AS qs   
    ON fn_handle_from_stmt.statement_sql_handle = qs.statement_sql_handle;  
```  
  
## See Also  
 [sp_query_store_force_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)   
 [sp_query_store_remove_plan &#40;Transct-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)   
 [sp_query_store_unforce_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)   
 [sp_query_store_reset_exec_stats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)   
 [sp_query_store_flush_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)   
 [sp_query_store_remove_query &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)  
  
  
