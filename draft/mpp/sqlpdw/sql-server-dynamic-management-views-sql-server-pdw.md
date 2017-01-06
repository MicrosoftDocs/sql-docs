---
title: "SQL Server Dynamic Management Views (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 79eedf6e-d64e-493d-9c42-9da724ac2c92
caps.latest.revision: 11
author: BarbKess
---
# SQL Server Dynamic Management Views (SQL Server PDW)
Lists the SQL Server Dynamic Management Views (DMVs) available in SQL Server PDW.  
  
## <a name="DMV"></a>List of SQL Server DMVs Available in SQL Server PDW  
SQL Server PDW exposes many of the SQL Server dynamic management views (DMVs). These views, when queried in SQL Server PDW, are reporting the state of SQL Server running on the Compute Nodes.  
  
Each SQL Server DMV has a PDW-specific column called **pdw_node_id**, which is the identifier for the Compute node.  
  
> [!NOTE]  
> To use SQL Server PDW DMVs in SQL Server PDW, insert ‘**pdw_nodes_**’ into the name, as shown in the following table.  
  
|DMV Name in SQL Server PDW|Link to SQL ServerTransact\-SQL topic on MSDN|  
|-----------------------------------------------------|------------------------------------------------------------------------------------------------------------------|  
|sys.dm_pdw_nodes_db_file_space_usage|[sys.dm_db_file_space_usage (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174412(v=sql.110).aspx)|  
|sys.dm_pdw_nodes_db_index_usage_stats|[sys.dm_db_index_usage_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188755(v=sql.110).aspx)|  
|sys.dm_pdw_nodes_db_partition_stats|[sys.dm_db_partition_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187737(v=sql11).aspx)|  
|sys.dm_pdw_nodes_db_session_space_usage|[sys.dm_db_session_space_usage (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187938(v=sql11).aspx)|  
|sys.dm_pdw_nodes_db_task_space_usage|[sys.dm_db_task_space_usage (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190288(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_background_job_queue|[sys.dm_exec_background_job_queue (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms173512(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_background_job_queue_stats|[sys.dm_exec_background_job_queue_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms176059(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_cached_plans|[sys.dm_exec_cached_plans (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187404(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_connections|[sys.dm_exec_connections (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms181509(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_procedure_stats|[sys.dm_exec_procedure_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/cc280701(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_query_memory_grants|[sys.dm_exec_query_memory_grants (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms365393(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_query_optimizer_info|[sys.dm_exec_query_optimizer_info (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms175002(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_query_resource_semaphores|[sys.dm_exec_query_resource_semaphores (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms366321(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_query_stats|[sys.dm_exec_query_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms189741(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_requests|[sys.dm_exec_requests (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms177648(v=sql11).aspx)|  
|sys.dm_pdw_nodes_exec_sessions|[sys.dm_exec_sessions (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms176013(v=sql11).aspx)|  
|sys.dm_pdw_nodes_io_cluster_shared_drives|[sys.dm_io_cluster_shared_drives (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188930(v=sql11).aspx)|  
|sys.dm_pdw_nodes_io_pending_io_requests|[sys.dm_io_pending_io_requests (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188762(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_buffer_descriptors|[sys.dm_os_buffer_descriptors (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms173442(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_child_instances|[sys.dm_os_child_instances (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms165698(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_cluster_nodes|[sys.dm_os_cluster_nodes (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187341(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_dispatcher_pools|[sys.dm_os_dispatcher_pools (Transact-SQL)](http://msdn.microsoft.com/en-us/library/bb630336(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_dispatchers|Transact\-SQL documentation is not available.|  
|sys.dm_pdw_nodes_os_hosts|[sys.dm_os_hosts (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187800(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_latch_stats|[sys.dm_os_latch_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms175066(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_loaded_modules|[sys.dm_os_loaded_modules (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms179907(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_brokers|[sys.dm_os_memory_brokers (Transact-SQL)](http://msdn.microsoft.com/en-us/library/bb522548(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_cache_clock_hands|[sys.dm_os_memory_cache_clock_hands (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms173786(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_cache_counters|[sys.dm_os_memory_cache_counters (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188760(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_cache_entries|[sys.dm_os_memory_cache_entries (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms189488(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_cache_hash_tables|[sys.dm_os_memory_cache_hash_tables (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms182388(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_clerks|[sys.dm_os_memory_clerks (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms175019(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_node_access_stats|Transact\-SQL documentation is not available.|  
|sys.dm_pdw_nodes_os_memory_nodes|[sys.dm_os_memory_nodes (Transact-SQL)](http://msdn.microsoft.com/en-us/library/bb510622(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_objects|[sys.dm_os_memory_objects (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms179875(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_memory_pools|[sys.dm_os_memory_pools (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms175022(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_nodes|[sys.dm_os_nodes (Transact-SQL)](http://msdn.microsoft.com/en-us/library/bb510628(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_performance_counters|[sys.dm_os_performance_counters (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187743(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_process_memory|[sys.dm_os_process_memory (Transact-SQL)](http://msdn.microsoft.com/en-us/library/bb510747(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_schedulers|[sys.dm_os_schedulers (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms177526(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_spinlock_stats|Transact\-SQL documentation is not available.|  
|sys.dm_pdw_nodes_os_sys_info|[sys.dm_os_sys_info (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms175048(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_sys_memory|[sys.dm_os_memory_nodes (Transact-SQL)](http://msdn.microsoft.com/en-us/library/bb510622(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_tasks|[sys.dm_os_tasks (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174963(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_threads|[sys.dm_os_threads (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187818(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_virtual_address_dump|[sys.dm_virtual_address_dump (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms186294(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_wait_stats|[sys.dm_os_wait_stats (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms179984(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_waiting_tasks|[sys.dm_waiting_tasks (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188743(v=sql11).aspx)|  
|sys.dm_pdw_nodes_os_workers|[sys.dm_os_workers (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms178626(v=sql11).aspx)|  
|sys.dm_pdw_nodes_resource_governor_resource_pools|[sys.dm_ resource_governor_resource_pools](http://msdn.microsoft.com/en-us/library/bb934023(v=sql11).aspx)|  
|sys.dm_pdw_nodes_resource_governor_workload_groups|[sys.dm_resource_governor_workload_groups](http://msdn.microsoft.com/en-us/library/bb934197(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_active_snapshot_database_transactions|[sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms180023(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_active_transactions|[sys.dm_tran_active_transactions (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174302(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_commit_table|Transact\-SQL documentation is not available.|  
|sys.dm_pdw_nodes_tran_current_snapshot|[sys.dm_tran_current_snapshot (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms184390(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_database_transactions|[sys.dm_tran_database_transactions (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms186957(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_locks|[sys.dm_tran_locks (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190345(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_session_transactions|[sys.dm_tran_session_transactions (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188739(v=sql11).aspx)|  
|sys.dm_pdw_nodes_tran_top_version_generators|[sys.dm_tran_top_version_generators (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188778(v=sql11).aspx)|  
  
## See Also  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
