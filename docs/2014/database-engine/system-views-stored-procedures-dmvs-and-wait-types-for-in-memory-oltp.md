---
title: "System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: efaa59e3-dbfa-407f-b1aa-cb0c6602ea17
caps.latest.revision: 27
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP
  This topic provides brief descriptions and links to the many database objects that support In-Memory OLTP.  
  
### System Views  
  
|System View|Description|In-Memory OLTP feature|  
|-----------------|-----------------|-----------------------------|  
|[sys.data_spaces &#40;Transact-SQL&#41;](../Topic/sys.data_spaces%20\(Transact-SQL\).md)|Check if a filegroup contains memory-optimized data.|The following columns display additional values: **type** and **type_desc**.|  
|[sys.indexes &#40;Transact-SQL&#41;](../Topic/sys.indexes%20\(Transact-SQL\).md)|Check if an index is on a memory-optimized table.|The following columns display additional values: **type** and **type_desc**.|  
|[sys.parameters &#40;Transact-SQL&#41;](../Topic/sys.parameters%20\(Transact-SQL\).md)|Check a parameter is not-nullable (for more efficient execution of a natively-compiled stored procedure).|**is_nullable** column.|  
|[sys.all_sql_modules &#40;Transact-SQL&#41;](../Topic/sys.all_sql_modules%20\(Transact-SQL\).md)|Check if a stored procedure is natively compiled.|**uses_native_compilation** column.|  
|[sys.sql_modules &#40;Transact-SQL&#41;](../Topic/sys.sql_modules%20\(Transact-SQL\).md)|Check if a stored procedure is natively compiled.|**uses_native_compilation** column.|  
|[sys.table_types &#40;Transact-SQL&#41;](../Topic/sys.table_types%20\(Transact-SQL\).md)|Check if a table is memory-optimized.|**is_memory_optimized** column.|  
|[sys.tables &#40;Transact-SQL&#41;](../Topic/sys.tables%20\(Transact-SQL\).md)|Check if a table is memory-optimized, and check a table’s durability setting.|**durability**, **durability_desc**, and **is_memory_optimized** columns.|  
|[sys.hash_indexes &#40;Transact-SQL&#41;](../Topic/sys.hash_indexes%20\(Transact-SQL\).md)|Show the hash indexes of a memory-optimized table.|In-memory OLTP specific.|  
  
### Metadata Functions  
  
|Metadata Function|Description|In-Memory OLTP feature|  
|-----------------------|-----------------|-----------------------------|  
|[OBJECTPROPERTYEX &#40;Transact-SQL&#41;](../Topic/OBJECTPROPERTYEX%20\(Transact-SQL\).md)|Check if database objects are memory-optimized.|**ExecIsWithNativeCompilation** and **TableIsMemoryOptimized** properties.<br /><br /> The **IsSchemaBound** property supports the Procedure object type (returns 0 for procedures instead of NULL).|  
|[SERVERPROPERTY &#40;Transact-SQL&#41;](../Topic/SERVERPROPERTY%20\(Transact-SQL\).md)|Check if a server supports In-Memory OLTP.|**IsXTPSupported** property.|  
  
### System Stored Procedures  
  
|Stored Procedure|Description|  
|----------------------|-----------------|  
|[sys.sp_xtp_bind_db_resource_pool &#40;Transact-SQL&#41;](../Topic/sys.sp_xtp_bind_db_resource_pool%20\(Transact-SQL\).md)|Bind an In-Memory OLTP database to a resource pool.|  
|[sys.sp_xtp_checkpoint_force_garbage_collection &#40;Transact-SQL&#41;](../Topic/sys.sp_xtp_checkpoint_force_garbage_collection%20\(Transact-SQL\).md)|Initiate garbage collection on an In-Memory OLTP database.|  
|[sys.sp_xtp_control_proc_exec_stats &#40;Transact-SQL&#41;](../Topic/sys.sp_xtp_control_proc_exec_stats%20\(Transact-SQL\).md)|Enable statistics collection for natively compiled stored procedures.|  
|[sys.sp_xtp_control_query_exec_stats &#40;Transact-SQL&#41;](../Topic/sys.sp_xtp_control_query_exec_stats%20\(Transact-SQL\).md)|Enable per query statistics collection for natively compiled stored procedures.|  
|[sys.sp_xtp_merge_checkpoint_files &#40;Transact-SQL&#41;](../Topic/sys.sp_xtp_merge_checkpoint_files%20\(Transact-SQL\).md)|Merge data and delta files.|  
|[sys.sp_xtp_unbind_db_resource_pool &#40;Transact-SQL&#41;](../Topic/sys.sp_xtp_unbind_db_resource_pool%20\(Transact-SQL\).md)|Remove the binding between a database and a resource pool.|  
  
## Dynamic Management Views (DMVs)  
 There are several DMVs for memory-optimized tables.  
  
 For details, see [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../Topic/Memory-Optimized%20Table%20Dynamic%20Management%20Views%20\(Transact-SQL\).md).  
  
## Wait Types  
 There are several wait types that support In-Memory OLTP.  
  
 For details, see wait types that are prefixed with **WAIT_XTP**, and **XTPPROC** in the [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../Topic/sys.dm_os_wait_stats%20\(Transact-SQL\).md) topic.  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../2014/database-engine/in-memory-oltp-in-memory-optimization.md)   
 [Transact-SQL Support for In-Memory OLTP](../../2014/database-engine/transact-sql-support-for-in-memory-oltp.md)  
  
  