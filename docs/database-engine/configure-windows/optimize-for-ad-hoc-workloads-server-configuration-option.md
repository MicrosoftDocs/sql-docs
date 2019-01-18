---
title: "optimize for ad hoc workloads Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "11/17/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "optimize for ad hoc workloads option"
ms.assetid: 0972e028-3a8e-454b-a186-e814a1d431f2
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# optimize for ad hoc workloads Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The **optimize for ad hoc workloads** option is used to improve the efficiency of the plan cache for workloads that contain many single use ad hoc batches. When this option is set to 1, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] stores a small compiled plan stub in the plan cache when a batch is compiled for the first time, instead of the full compiled plan. This helps to relieve memory pressure by not allowing the plan cache to become filled with compiled plans that are not reused. 
  
  The compiled plan stub allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to recognize that this ad hoc batch has been compiled before but has only stored a compiled plan stub, so when this batch is invoked (compiled or executed) again, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] compiles the batch, removes the compiled plan stub from the plan cache, and adds the full compiled plan to the plan cache. 
  
 The compiled plan stub is one of the cacheobjtypes displayed by the sys.dm_exec_cached_plans catalog view. It has a unique sql handle and plan handle. The compiled plan stub does not have an execution plan associated with it and querying for the plan handle will not return an XML Showplan.  
  
 [Trace flag 8032](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) reverts the cache limit parameters to the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] RTM setting which in general allows caches to be larger. Use this setting when frequently reused cache entries do not fit into the cache and when the optimize for ad hoc workloads Server Configuration Option has failed to resolve the problem with plan cache.  
  
> [!WARNING]  
>  Trace flag 8032 can cause poor performance if large caches make less memory available for other memory consumers, such as the buffer pool.  

## Recommendations
Avoid having a large number of single-use plans in the plan cache. A common cause of this problem is when the data types of query parameters is not consistently defined. This particularly applies to the length of strings but can apply to any data type that has a maxlength, a precision, or a scale. For example, if a parameter named @Greeting is passed as an nvarchar(10) on one call and an nvarchar(20) on the next call, separate plans are created for each parameter size. If a query has several parameters and they are not consistently defined when called, a large number of query plans could exist for each query. Plans could exist for each combination of query parameter data types and lengths that have been used.

If the number of single-use plans take a significant portion of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] memory in an OLTP server, and these plans are Ad-hoc plans, use this server option to decrease memory usage with these objects.
To find the number of single-use cached plans, run the following query:

```sql
SELECT objtype, cacheobjtype, 
  AVG(usecounts) AS Avg_UseCount, 
  SUM(refcounts) AS AllRefObjects, 
  SUM(CAST(size_in_bytes AS bigint))/1024/1024 AS Size_MB
FROM sys.dm_exec_cached_plans
WHERE objtype = 'Adhoc' AND usecounts = 1
GROUP BY objtype, cacheobjtype;
```

> [!IMPORTANT]
> Setting the **optimize for ad hoc workloads** to 1 affects only new plans; plans that are already in the plan cache are unaffected.
> To affect already cached query plans immediately, the plan cache needs to be cleared using [ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md), or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has to restart.

## See Also  
 [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
