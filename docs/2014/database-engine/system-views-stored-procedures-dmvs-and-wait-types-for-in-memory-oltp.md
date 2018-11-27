---
title: "System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: efaa59e3-dbfa-407f-b1aa-cb0c6602ea17
author: stevestein
ms.author: sstein
manager: craigg
---
# System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP
  This topic provides brief descriptions and links to the many database objects that support In-Memory OLTP.  
  
### System Views  
  
|System View|Description|In-Memory OLTP feature|  
|-----------------|-----------------|-----------------------------|  
|[sys.data_spaces &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-data-spaces-transact-sql)|Check if a filegroup contains memory-optimized data.|The following columns display additional values: **type** and **type_desc**.|  
|[sys.indexes &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-indexes-transact-sql)|Check if an index is on a memory-optimized table.|The following columns display additional values: **type** and **type_desc**.|  
|[sys.parameters &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-parameters-transact-sql)|Check a parameter is not-nullable (for more efficient execution of a natively-compiled stored procedure).|**is_nullable** column.|  
|[sys.all_sql_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-all-sql-modules-transact-sql)|Check if a stored procedure is natively compiled.|**uses_native_compilation** column.|  
|[sys.sql_modules &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-modules-transact-sql)|Check if a stored procedure is natively compiled.|**uses_native_compilation** column.|  
|[sys.table_types &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-table-types-transact-sql)|Check if a table is memory-optimized.|**is_memory_optimized** column.|  
|[sys.tables &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-tables-transact-sql)|Check if a table is memory-optimized, and check a table's durability setting.|**durability**, **durability_desc**, and **is_memory_optimized** columns.|  
|[sys.hash_indexes &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-hash-indexes-transact-sql)|Show the hash indexes of a memory-optimized table.|In-memory OLTP specific.|  
  
### Metadata Functions  
  
|Metadata Function|Description|In-Memory OLTP feature|  
|-----------------------|-----------------|-----------------------------|  
|[OBJECTPROPERTYEX &#40;Transact-SQL&#41;](/sql/t-sql/functions/objectproperty-transact-sql)|Check if database objects are memory-optimized.|**ExecIsWithNativeCompilation** and **TableIsMemoryOptimized** properties.<br /><br /> The **IsSchemaBound** property supports the Procedure object type (returns 0 for procedures instead of NULL).|  
|[SERVERPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/serverproperty-transact-sql)|Check if a server supports In-Memory OLTP.|**IsXTPSupported** property.|  
  
### System Stored Procedures  
  
|Stored Procedure|Description|  
|----------------------|-----------------|  
|[sys.sp_xtp_bind_db_resource_pool &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-bind-db-resource-pool-transact-sql)|Bind an In-Memory OLTP database to a resource pool.|  
|[sys.sp_xtp_checkpoint_force_garbage_collection &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-checkpoint-force-garbage-collection-transact-sql)|Initiate garbage collection on an In-Memory OLTP database.|  
|[sys.sp_xtp_control_proc_exec_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-control-proc-exec-stats-transact-sql)|Enable statistics collection for natively compiled stored procedures.|  
|[sys.sp_xtp_control_query_exec_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql)|Enable per query statistics collection for natively compiled stored procedures.|  
|[sys.sp_xtp_merge_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-merge-checkpoint-files-transact-sql)|Merge data and delta files.|  
|[sys.sp_xtp_unbind_db_resource_pool &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-unbind-db-resource-pool-transact-sql)|Remove the binding between a database and a resource pool.|  
  
## Dynamic Management Views (DMVs)  
 There are several DMVs for memory-optimized tables.  
  
 For details, see [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql).  
  
## Wait Types  
 There are several wait types that support In-Memory OLTP.  
  
 For details, see wait types that are prefixed with **WAIT_XTP**, and **XTPPROC** in the [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql) topic.  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)   
 [Transact-SQL Support for In-Memory OLTP](../relational-databases/in-memory-oltp/transact-sql-support-for-in-memory-oltp.md)  
  
  
