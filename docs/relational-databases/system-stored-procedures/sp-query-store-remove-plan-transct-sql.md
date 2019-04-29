---
title: "sp_query_store_remove_plan (Transct-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "SYS.SP_QUERY_STORE_REMOVE_PLAN_TSQL"
  - "SP_QUERY_STORE_REMOVE_PLAN_TSQL"
  - "SP_QUERY_STORE_REMOVE_PLAN"
  - "SYS.SP_QUERY_STORE_REMOVE_PLAN"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_query_store_remove_plan"
  - "sp_query_store_remove_plan"
ms.assetid: 88734726-135b-4b61-9f3f-f568c1fbece6
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_remove_plan (Transct-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Removes  a single plan from the query store.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_query_store_remove_plan [ @plan_id = ] plan_id [;]  
```  
  
## Arguments  
`[ @plan_id = ] plan_id`
 Is the id of the query plan to be removed. *plan_id* is a **bigint**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
  
## Permissions  
 Requires the **EXECUTE** permission on the database, and **DELETE** permission on the query store catalog views.  
  
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
  
 After you identify the plan_id that you want to delete, use the following example to delete a query plan.  
  
```  
EXEC sp_query_store_remove_plan 3;  
```  
  
## See Also  
 [sp_query_store_force_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)   
 [sp_query_store_remove_query &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)   
 [sp_query_store_unforce_plan &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)   
 [sp_query_store_reset_exec_stats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)   
 [sp_query_store_flush_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)  
  
  
