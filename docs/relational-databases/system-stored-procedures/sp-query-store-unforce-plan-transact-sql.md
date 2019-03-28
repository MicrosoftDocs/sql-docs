---
title: "sp_query_store_unforce_plan (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "SP_QUERY_STORE_UNFORCE_PLAN_TSQL"
  - "SP_QUERY_STORE_UNFORCE_PLAN"
  - "SYS.SP_QUERY_STORE_UNFORCE_PLAN"
  - "SYS.SP_QUERY_STORE_UNFORCE_PLAN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_query_store_unforce_plan"
  - "sp_query_store_unforce_plan"
ms.assetid: a52f91d0-ff1e-46ad-ba36-b32d9623c9ab
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_unforce_plan (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Enables unforcing a particular plan for a particular query.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_query_store_unforce_plan [ @query_id = ] query_id , [ @plan_id = ] plan_id [;]  
```  
  
## Arguments  
`[ @query_id = ] query_id`
 Is the id of the query. *query_id* is a **bigint**, with no default.  
  
`[ @plan_id = ] plan_id`
 Is the id of the query plan that will no longer be enforced. *plan_id* is a **bigint**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
  
## Permissions  
 Requires the **EXECUTE** permission on the database, and **INSERT**, **UPDATE**, and **DELETE** permission on the query store catalog views.  
  
## Examples  
 The following example returns information about the queries in the query store.  
  
```  
SELECT Txt.query_text_id, Txt.query_sql_text, Pl.plan_id, Qry.*  
FROM sys.query_store_plan AS Pl  
JOIN sys.query_store_query AS Qry  
    ON Pl.query_id = Qry.query_id  
JOIN sys.query_store_query_text AS Txt  
    ON Qry.query_text_id = Txt.query_text_id ;  
```  
  
 After you identify the query_id and plan_id that you want to unforce, use the following example to unforce the plan.  
  
```  
EXEC sp_query_store_unforce_plan 3, 3;  
```  
  
## See Also  
 [sp_query_store_force_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)   
 [sp_query_store_remove_plan &#40;Transct-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)   
 [sp_query_store_remove_query &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)   
 [sp_query_store_reset_exec_stats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)   
 [sp_query_store_flush_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)  
  
  
