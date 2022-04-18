---
description: "sp_query_store_force_plan (Transact-SQL)"
title: "sp_query_store_force_plan (Transact-SQL)"
ms.custom: ""
ms.date: "09/20/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "SYS.SP_QUERY_STORE_FORCE_PLAN"
  - "SP_QUERY_STORE_FORCE_PLAN"
  - "SYS.SP_QUERY_STORE_FORCE_PLAN_TSQL"
  - "SP_QUERY_STORE_FORCE_PLAN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_query_store_force_plan"
  - "sp_query_store_force_plan"
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_force_plan (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Enables forcing a particular plan for a particular query.  
  
 When a plan is forced for a particular query, every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encounters the query, it tries to force the plan in the Query Optimizer. If plan forcing fails, an Extended Event is fired and the Query Optimizer is instructed to optimize in the normal way.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
sp_query_store_force_plan [ @query_id = ] query_id , [ @plan_id = ] plan_id [;]  
```  
  
## Arguments  
#### `[ @query_id = ] query_id`
 Is the id of the query. *query_id* is a **bigint**, with no default.  
  
#### `[ @plan_id = ] plan_id`
 Is the id of the query plan to be forced. *plan_id* is a **bigint**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
  The resulting execution plan forced by this feature will be the same or similar to the plan being forced. Because the resulting plan may not be identical to the plan specified by `sp_query_store_force_plan`, the performance of the plans may vary. In rare cases, the performance difference may be significant and negative; in that case, the administrator must remove the forced plan.

## Permissions  
 Requires the **ALTER** permission on the database.
  
## Examples  
 The following example returns information about the queries in the query store.  
  
```sql  
SELECT Txt.query_text_id, Txt.query_sql_text, Pl.plan_id, Qry.*  
FROM sys.query_store_plan AS Pl  
JOIN sys.query_store_query AS Qry  
    ON Pl.query_id = Qry.query_id  
JOIN sys.query_store_query_text AS Txt  
    ON Qry.query_text_id = Txt.query_text_id ;  
```  
  
 After you identify the query_id and plan_id that you want to force, use the following example to force the query to use a plan.  
  
```sql  
EXEC sp_query_store_force_plan 3, 3;  
```  
  
## See Also  
 [sp_query_store_remove_plan &#40;Transct-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)   
 [sp_query_store_remove_query &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)   
 [sp_query_store_unforce_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)   
 [Monitoring Performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [sp_query_store_reset_exec_stats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)   
 [sp_query_store_flush_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)       
 [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md#CheckForced)    
  
  
