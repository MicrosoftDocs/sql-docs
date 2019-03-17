---
title: System views - Analytics Platform System Parallel Data Warehouse | Microsoft Docs
description: System views for Analytic Platform System (APS) SQL Server Parallel Data Warehouse (PDW).
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# System views for Analytics Platform System Parallel Data Warehouse
System views for Analytic Platform System (APS) SQL Server Parallel Data Warehouse (PDW).

## Parallel Data Warehouse catalog views
* [sys.pdw_column_distribution_properties](https://msdn.microsoft.com/library/mt204022.aspx)
* [sys.pdw_database_mappings](https://msdn.microsoft.com/library/mt203891.aspx)
* [sys.pdw_distributions](https://msdn.microsoft.com/library/mt203892.aspx)
* [sys.pdw_index_mappings](https://msdn.microsoft.com/library/mt203912.aspx)
* [sys.pdw_loader_backup_run_details](../relational-databases/system-catalog-views/sys-pdw-loader-backup-run-details-transact-sql.md)
* [sys.pdw_loader_backup_runs](../relational-databases/system-catalog-views/sys-pdw-loader-backup-runs-transact-sql.md)
* [sys.pdw_nodes_column_store_dictionaries](https://msdn.microsoft.com/library/mt203902.aspx)
* [sys.pdw_nodes_column_store_row_groups](https://msdn.microsoft.com/library/mt203880.aspx)
* [sys.pdw_nodes_column_store_segments](https://msdn.microsoft.com/library/mt203916.aspx)
* [sys.pdw_nodes_columns](https://msdn.microsoft.com/library/mt203881.aspx)
* [sys.pdw_nodes_indexes](https://msdn.microsoft.com/library/mt203885.aspx)
* [sys.pdw_nodes_partitions](https://msdn.microsoft.com/library/mt203908.aspx)
* [sys.pdw_nodes_pdw_physical_databases](https://msdn.microsoft.com/library/mt203897.aspx)
* [sys.pdw_nodes_tables](https://msdn.microsoft.com/library/mt203886.aspx)
* [sys.pdw_table_distribution_properties](https://msdn.microsoft.com/library/mt203896.aspx)
* [sys.pdw_table_mappings](https://msdn.microsoft.com/library/mt203876.aspx)

## Parallel Data Warehouse dynamic management views (DMVs)
* [sys.dm_pdw_dms_cores](https://msdn.microsoft.com/library/mt203911.aspx)
* [sys.dm_pdw_dms_external_work](../relational-databases/system-dynamic-management-views/sys-dm-pdw-dms-external-work-transact-sql.md)
* [sys.dm_pdw_dms_workers](https://msdn.microsoft.com/library/mt203878.aspx)
* [sys.dm_pdw_errors](https://msdn.microsoft.com/library/mt203904.aspx)
* [sys.dm_pdw_exec_connections](https://msdn.microsoft.com/library/mt203882.aspx)
* [sys.dm_pdw_exec_requests](https://msdn.microsoft.com/library/mt203887.aspx)
* [sys.dm_pdw_exec_sessions](https://msdn.microsoft.com/library/mt203883.aspx)
* [sys.dm_pdw_hadoop_operations](../relational-databases/system-dynamic-management-views/sys-dm-pdw-hadoop-operations-transact-sql.md)
* [sys.dm_pdw_lock_waits](https://msdn.microsoft.com/library/mt203901.aspx)
* [sys.dm_pdw_nodes](https://msdn.microsoft.com/library/mt203907.aspx)
* [sys.dm_pdw_nodes_database_encryption_keys](https://msdn.microsoft.com/library/mt203922.aspx)
* [sys.dm_pdw_os_threads](https://msdn.microsoft.com/library/mt203917.aspx)
* [sys.dm_pdw_request_steps](https://msdn.microsoft.com/library/mt203913.aspx)
* [sys.dm_pdw_resource_waits](https://msdn.microsoft.com/library/mt203906.aspx)
* [sys.dm_pdw_sql_requests](https://msdn.microsoft.com/library/mt203889.aspx)
* [sys.dm_pdw_sys_info](https://msdn.microsoft.com/library/mt203900.aspx)
* [sys.dm_pdw_wait_stats](https://msdn.microsoft.com/library/mt203909.aspx)
* [sys.dm_pdw_waits](https://msdn.microsoft.com/library/mt203909.aspx)

## SQL Server DMVs applicable to Parallel Data Warehouse
The following DMVs are applicable to Parallel Data Warehouse, but must be executed by connecting to the **master** database.

* [sys.database_service_objectives](../relational-databases/system-catalog-views/sys-database-service-objectives-azure-sql-database.md)
* [sys.dm_operation_status](../relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md)
* [sys.fn_helpcollations()](../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)

## SQL Server catalog views
* [sys.all_columns](https://msdn.microsoft.com/library/ms177522.aspx)
* [sys.all_objects](https://msdn.microsoft.com/library/ms178618.aspx)
* [sys.all_parameters](https://msdn.microsoft.com/library/ms190340.aspx)
* [sys.all_sql_modules](https://msdn.microsoft.com/library/ms184389.aspx)
* [sys.all_views](https://msdn.microsoft.com/library/ms189510.aspx)
* [sys.assemblies](https://msdn.microsoft.com/library/ms189790.aspx)
* [sys.assembly_modules](https://msdn.microsoft.com/library/ms180052.aspx)
* [sys.assembly_types](https://msdn.microsoft.com/library/ms178020.aspx)
* [sys.certificates](https://msdn.microsoft.com/library/ms189774.aspx)
* [sys.check_constraints](https://msdn.microsoft.com/library/ms187388.aspx)
* [sys.columns](https://msdn.microsoft.com/library/ms176106.aspx)
* [sys.computed_columns](https://msdn.microsoft.com/library/ms188744.aspx)
* [sys.credentials](../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)
* [sys.data_spaces](https://msdn.microsoft.com/library/ms190289.aspx)
* [sys.database_credentials](../relational-databases/system-catalog-views/sys-database-credentials-transact-sql.md)
* [sys.database_files](https://msdn.microsoft.com/library/ms174397.aspx)
* [sys.database_permissions](https://msdn.microsoft.com/library/ms188367.aspx)
* [sys.database_principals](https://msdn.microsoft.com/library/ms187328.aspx)
* [sys.database_role_members](https://msdn.microsoft.com/library/ms189780.aspx)
* [sys.databases](https://msdn.microsoft.com/library/ms178534.aspx)
* [sys.default_constraints](https://msdn.microsoft.com/library/ms173758.aspx)
* [sys.external_data_sources](https://msdn.microsoft.com/library/dn935019.aspx)
* [sys.external_file_formats](https://msdn.microsoft.com/library/dn935025.aspx)
* [sys.external_tables](https://msdn.microsoft.com/library/dn935029.aspx)
* [sys.filegroups](https://msdn.microsoft.com/library/ms187782.aspx)
* [sys.foreign_key_columns](https://msdn.microsoft.com/library/ms186306.aspx)
* [sys.foreign_keys](https://msdn.microsoft.com/library/ms189807.aspx)
* [sys.identity_columns](https://msdn.microsoft.com/library/ms187334.aspx)
* [sys.index_columns](https://msdn.microsoft.com/library/ms175105.aspx)
* [sys.indexes](https://msdn.microsoft.com/library/ms173760.aspx)
* [sys.key_constraints](https://msdn.microsoft.com/library/ms174321.aspx)
* [sys.numbered_procedures](https://msdn.microsoft.com/library/ms179865.aspx)
* [sys.objects](https://msdn.microsoft.com/library/ms190324.aspx)
* [sys.parameters](https://msdn.microsoft.com/library/ms176074.aspx)
* [sys.partition_functions](https://msdn.microsoft.com/library/ms187381.aspx)
* [sys.partition_parameters](https://msdn.microsoft.com/library/ms175054.aspx)
* [sys.partition_range_values](https://msdn.microsoft.com/library/ms187780.aspx)
* [sys.partition_schemes](https://msdn.microsoft.com/library/ms189752.aspx)
* [sys.partitions](https://msdn.microsoft.com/library/ms175012.aspx)
* [sys.procedures](https://msdn.microsoft.com/library/ms188737.aspx)
* [sys.schemas](https://msdn.microsoft.com/library/ms176011.aspx)
* [sys.securable_classes](https://msdn.microsoft.com/library/ms408301.aspx)
* [sys.sql_expression_dependencies](https://msdn.microsoft.com/library/bb677315.aspx)
* [sys.sql_modules](https://msdn.microsoft.com/library/ms175081.aspx)
* [sys.stats](https://msdn.microsoft.com/library/ms177623.aspx)
* [sys.stats_columns](https://msdn.microsoft.com/library/ms187340.aspx)
* [sys.symmetric_keys](../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)
* [sys.synonyms](../relational-databases/system-catalog-views/sys-synonyms-transact-sql.md)
* [sys.syscharsets](../relational-databases/system-compatibility-views/sys-syscharsets-transact-sql.md)
* [sys.syscolumns](../relational-databases/system-compatibility-views/sys-syscolumns-transact-sql.md)
* [sys.sysdatabases](../relational-databases/system-compatibility-views/sys-sysdatabases-transact-sql.md)
* [sys.syslanguages](../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)
* [sys.sysobjects](../relational-databases/system-compatibility-views/sys-sysobjects-transact-sql.md)
* [sys.sysreferences](../relational-databases/system-compatibility-views/sys-sysreferences-transact-sql.md)
* [sys.system_columns](https://msdn.microsoft.com/library/ms178596.aspx)
* [sys.system_objects](https://msdn.microsoft.com/library/ms173551.aspx)
* [sys.system_parameters](../relational-databases/system-catalog-views/sys-system-parameters-transact-sql.md)
* [sys.system_sql_modules](https://msdn.microsoft.com/library/ms188034.aspx)
* [sys.system_views](https://msdn.microsoft.com/library/ms187764.aspx)
* [sys.systypes](../relational-databases/system-compatibility-views/sys-systypes-transact-sql.md)
* [sys.sysusers](../relational-databases/system-compatibility-views/sys-sysusers-transact-sql.md)
* [sys.tables](https://msdn.microsoft.com/library/ms187406.aspx)
* [sys.types](https://msdn.microsoft.com/library/ms188021.aspx)
* [sys.views](https://msdn.microsoft.com/library/ms190334.aspx)

## SQL Server DMVs available in Parallel Data Warehouse
Parallel Data Warehouse exposes many of the SQL Server dynamic management views (DMVs). These views, when queried in Parallel Data Warehouse, are reporting the state of SQL  Server databases running on the distributions.

Each of these DMV's has a specific column called pdw_node_id. This is the identifier for the Compute node. 

> [!NOTE]
> To use these view, insert 'pdw_nodes_' into the name, as shown in the following table.
> 
> 

| DMV name in Parallel Data Warehouse | Link to SQL Server T-SQL topic |
|:--- |:--- |
| sys.dm_pdw_nodes_db_file_space_usage |[sys.dm_db_file_space_usage](https://msdn.microsoft.com/library/ms174412.aspx) |
| sys.dm_pdw_nodes_db_index_usage_stats |[sys.dm_db_index_usage_stats](https://msdn.microsoft.com/library/ms188755.aspx) |
| sys.dm_pdw_nodes_db_partition_stats |[sys.dm_db_partition_stats](https://msdn.microsoft.com/library/ms187737.aspx) |
| sys.dm_pdw_nodes_db_session_space_usage |[sys.dm_db_session_space_usage](https://msdn.microsoft.com/library/ms187938.aspx) |
| sys.dm_pdw_nodes_db_task_space_usage |[sys.dm_db_task_space_usage](https://msdn.microsoft.com/library/ms190288.aspx) |
| sys.dm_pdw_nodes_exec_background_job_queue |[sys.dm_exec_background_job_queue](https://msdn.microsoft.com/library/ms173512.aspx) |
| sys.dm_pdw_nodes_exec_background_job_queue_stats |[sys.dm_exec_background_job_queue_stats](https://msdn.microsoft.com/library/ms176059.aspx) |
| sys.dm_pdw_nodes_exec_cached_plans |[sys.dm_exec_cached_plans](https://msdn.microsoft.com/library/ms187404.aspx) |
| sys.dm_pdw_nodes_exec_connections |[sys.dm_exec_connections](https://msdn.microsoft.com/library/ms181509.aspx) |
| sys.dm_pdw_nodes_exec_procedure_stats |[sys.dm_exec_procedure_stats](https://msdn.microsoft.com/library/cc280701.aspx) |
| sys.dm_pdw_nodes_exec_query_memory_grants |[sys.dm_exec_query_memory_grants](https://msdn.microsoft.com/library/ms365393.aspx) |
| sys.dm_pdw_nodes_exec_query_optimizer_info |[sys.dm_exec_query_optimizer_info](https://msdn.microsoft.com/library/ms175002.aspx) |
| sys.dm_pdw_nodes_exec_query_resource_semaphores |[sys.dm_exec_query_resource_semaphores](https://msdn.microsoft.com/library/ms366321.aspx) |
| sys.dm_pdw_nodes_exec_query_stats |[sys.dm_exec_query_stats](https://msdn.microsoft.com/library/ms189741.aspx) |
| sys.dm_pdw_nodes_exec_requests |[sys.dm_exec_requests](https://msdn.microsoft.com/library/ms177648.aspx) |
| sys.dm_pdw_nodes_exec_sessions |[sys.dm_exec_sessions](../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) |
| sys.dm_pdw_nodes_io_pending_io_requests |[sys.dm_io_pending_io_requests](https://msdn.microsoft.com/library/ms188762.aspx) |
| sys.dm_pdw_nodes_os_buffer_descriptors |[sys.dm_os_buffer_descriptors](https://msdn.microsoft.com/library/ms173442.aspx) |
| sys.dm_pdw_nodes_os_child_instances |[sys.dm_os_child_instances](https://msdn.microsoft.com/library/ms165698.aspx) |
| sys.dm_pdw_nodes_os_cluster_nodes |[sys.dm_os_cluster_nodes](https://msdn.microsoft.com/library/ms187341.aspx) |
| sys.dm_pdw_nodes_os_dispatcher_pools |[sys.dm_os_dispatcher_pools](https://msdn.microsoft.com/library/bb630336.aspx) |
| sys.dm_pdw_nodes_os_dispatchers |Transact-SQL Documentation is not available. |
| sys.dm_pdw_nodes_os_hosts |[sys.dm_os_hosts](https://msdn.microsoft.com/library/ms187800.aspx) |
| sys.dm_pdw_nodes_os_latch_stats |[sys.dm_os_latch_stats](https://msdn.microsoft.com/library/ms175066.aspx) |
| sys.dm_pdw_nodes_os_memory_brokers |[sys.dm_os_memory_brokers](https://msdn.microsoft.com/library/bb522548.aspx) |
| sys.dm_pdw_nodes_os_memory_cache_clock_hands |[sys.dm_os_memory_cache_clock_hands](https://msdn.microsoft.com/library/ms173786.aspx) |
| sys.dm_pdw_nodes_os_memory_cache_counters |[sys.dm_os_memory_cache_counters](https://msdn.microsoft.com/library/ms188760.aspx) |
| sys.dm_pdw_nodes_os_memory_cache_entries |[sys.dm_os_memory_cache_entries](https://msdn.microsoft.com/library/ms189488.aspx) |
| sys.dm_pdw_nodes_os_memory_cache_hash_tables |[sys.dm_os_memory_cache_hash_tables](https://msdn.microsoft.com/library/ms182388.aspx) |
| sys.dm_pdw_nodes_os_memory_clerks |[sys.dm_os_memory_clerks](https://msdn.microsoft.com/library/ms175019.aspx) |
| sys.dm_pdw_nodes_os_memory_node_access_stats |Transact-SQL Documentation is not available. |
| sys.dm_pdw_nodes_os_memory_nodes |[sys.dm_os_memory_nodes](https://msdn.microsoft.com/library/bb510622.aspx) |
| sys.dm_pdw_nodes_os_memory_objects |[sys.dm_os_memory_objects](../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md) |
| sys.dm_pdw_nodes_os_memory_pools |[sys.dm_os_memory_pools](https://msdn.microsoft.com/library/ms175022.aspx) |
| sys.dm_pdw_nodes_os_nodes |[sys.dm_os_nodes](https://msdn.microsoft.com/library/bb510628.aspx) |
| sys.dm_pdw_nodes_os_performance_counters |[sys.dm_os_performance_counters](https://msdn.microsoft.com/library/ms187743.aspx) |
| sys.dm_pdw_nodes_os_process_memory |[sys.dm_os_process_memory](https://msdn.microsoft.com/library/bb510747.aspx) |
| sys.dm_pdw_nodes_os_schedulers |[sys.dm_os_schedulers](https://msdn.microsoft.com/library/ms177526.aspx) |
| sys.dm_pdw_nodes_os_spinlock_stats |Transact-SQL Documentation is not available. |
| sys.dm_pdw_nodes_os_sys_info |[sys.dm_os_sys_info](https://msdn.microsoft.com/library/ms175048.aspx) |
| sys.dm_pdw_nodes_os_sys_memory |[sys.dm_os_memory_nodes](https://msdn.microsoft.com/library/bb510622.aspx) |
| sys.dm_pdw_nodes_os_tasks |[sys.dm_os_tasks](https://msdn.microsoft.com/library/ms174963.aspx) |
| sys.dm_pdw_nodes_os_threads |[sys.dm_os_threads](https://msdn.microsoft.com/library/ms187818.aspx) |
| sys.dm_pdw_nodes_os_virtual_address_dump |[sys.dm_os_virtual_address_dump](../relational-databases/system-dynamic-management-views/sys-dm-os-virtual-address-dump-transact-sql.md) |
| sys.dm_pdw_nodes_os_wait_stats |[sys.dm_os_wait_stats](../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md) |
| sys.dm_pdw_nodes_os_waiting_tasks |[sys.dm_os_waiting_tasks](../relational-databases/system-dynamic-management-views/sys-dm-os-waiting-tasks-transact-sql.md) |
| sys.dm_pdw_nodes_os_workers |[sys.dm_os_workers](../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md) |
| sys.dm_pdw_nodes_resource_governor_resource_pools |[sys.dm_resource_governor_resource_pools](https://msdn.microsoft.com/library/bb934023.aspx) |
| sys.dm_pdw_nodes_resource_governor_workload_groups |[sys.dm_resource_governor_workload_groups](https://msdn.microsoft.com/library/bb934197.aspx) |
| sys.dm_pdw_nodes_tran_active_snapshot_database_transactions |[sys.dm_tran_active_snapshot_database_transactions](https://msdn.microsoft.com/library/ms180023.aspx) |
| sys.dm_pdw_nodes_tran_active_transactions |[sys.dm_tran_active_transactions](https://msdn.microsoft.com/library/ms174302.aspx) |
| sys.dm_pdw_nodes_tran_commit_table |[sys.dm_tran_commit_table](../relational-databases/system-dynamic-management-views/change-tracking-sys-dm-tran-commit-table.md) |
| sys.dm_pdw_nodes_tran_current_snapshot |[sys.dm_tran_current_snapshot](https://msdn.microsoft.com/library/ms184390.aspx) |
| sys.dm_pdw_nodes_tran_current_transaction |[sys.dm_tran_current_transaction](../relational-databases/system-dynamic-management-views/sys-dm-tran-current-transaction-transact-sql.md) |
| sys.dm_pdw_nodes_tran_database_transactions |[sys.dm_tran_database_transactions](../relational-databases/system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md) |
| sys.dm_pdw_nodes_tran_locks |[sys.dm_tran_locks](https://msdn.microsoft.com/library/ms190345.aspx) |
| sys.dm_pdw_nodes_tran_session_transactions |[sys.dm_tran_session_transactions](https://msdn.microsoft.com/library/ms188739.aspx) |
| sys.dm_pdw_nodes_tran_top_version_generators |[sys.dm_tran_top_version_generators](https://msdn.microsoft.com/library/ms188778.aspx) |

## SQL Server 2016 PolyBase DMVs available in Parallel Data Warehouse
* [sys.dm_exec_compute_node_errors](https://msdn.microsoft.com/library/mt146380.aspx)
* [sys.dm_exec_compute_node_status](https://msdn.microsoft.com/library/mt146382.aspx)
* [sys.dm_exec_compute_nodes](../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md)
* [sys.dm_exec_distributed_request_steps](../relational-databases/system-dynamic-management-views/sys-dm-exec-distributed-request-steps-transact-sql.md)
* [sys.dm_exec_distributed_requests](https://msdn.microsoft.com/library/mt146385.aspx)
* [sys.dm_exec_distributed_sql_requests](https://msdn.microsoft.com/library/mt146390.aspx)
* [sys.dm_exec_dms_services](../relational-databases/system-dynamic-management-views/sys-dm-exec-dms-services-transact-sql.md)
* [sys.dm_exec_dms_workers](../relational-databases/system-dynamic-management-views/sys-dm-exec-dms-workers-transact-sql.md)
* [sys.dm_exec_external_operations](../relational-databases/system-dynamic-management-views/sys-dm-exec-external-operations-transact-sql.md)
* [sys.dm_exec_external_work](../relational-databases/system-dynamic-management-views/sys-dm-exec-external-work-transact-sql.md)

## SQL Server INFORMATION_SCHEMA views
* [CHECK_CONSTRAINTS](../relational-databases/system-information-schema-views/check-constraints-transact-sql.md)
* [COLUMNS](../relational-databases/system-information-schema-views/columns-transact-sql.md)
* [PARAMETERS](../relational-databases/system-information-schema-views/parameters-transact-sql.md)
* [ROUTINES](../relational-databases/system-information-schema-views/routines-transact-sql.md)
* [SCHEMATA](../relational-databases/system-information-schema-views/schemata-transact-sql.md)
* [TABLES](../relational-databases/system-information-schema-views/tables-transact-sql.md)
* [VIEW_COLUMN_USAGE](../relational-databases/system-information-schema-views/view-column-usage-transact-sql.md)
* [VIEW_TABLE_USAGE](../relational-databases/system-information-schema-views/view-table-usage-transact-sql.md)
* [VIEWS](../relational-databases/system-information-schema-views/views-transact-sql.md)

## Next steps
For more reference information, see [T-SQL language elements](tsql-language-elements.md) and [T-SQL statements](tsql-statements.md).

<!--Image references-->

<!--Article references-->
[SQL Data Warehouse reference overview]: sql-data-warehouse-overview-reference.md

<!--MSDN references-->


<!--Other Web references-->
