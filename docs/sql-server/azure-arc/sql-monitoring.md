---
title: Monitor Azure Arc-enabled SQL Server in Azure portal
description: How to enable or disable monitoring on Azure Arc-enabled SQL Servers
author: lcwright
ms.author: lancewright
ms.reviewer: mikeray
ms.date: 09/08/2023
ms.topic: conceptual
---

# Monitor Azure Arc-enabled SQL Server
<TO DO>

## Prerequisites 
In order for monitoring data to be collected on an Azure Arc-enabled SQL Server, the following conditions must be met:
* The version of Azure Extension for SQL Server (WindowsAgent.SqlServer) is "<TODO>" or later
* Azure Arc-enabled SQL Servers must be running on Windows operating system
* Azure Arc-enabled SQL Servers must be a Standard or Enterprise (Core) Edition
* The license type on the Azure Arc-enabled SQL Server must be set to "License with Software Assurance" or "Pay-as-you-go"
* The SQL Server built-in login NT AUTHORITY\SYSTEM must be the member of SQL Server sysadmin server role

## Disable or enable collection

> [!IMPORTANT]
> In order to disable or enable data collection, the `sqlServer` extension must be on `v1.1.2491` or later. [Upgrade VM extensions using the Azure Portal.](/azure/azure-arc/servers/manage-vm-extensions-portal)

### Disable monitoring data collection

Run the following command in the Azure CLI to disable monitoring data collection for your Azure Arc-enabled SQL Server. Replace the placeholders for subscription ID, resource group, and resource name:


```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=false' --api-version 2023-09-01-preview
```

### Enable monitoring data collection

To enable the monitoring data collection for an Azure Arc-enabled SQL Server, run the following command in the Azure CLI. Replace the placeholders for subscription ID, resource group, and resource name:

```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=true' --api-version 2023-09-01-preview
```

## Collected data

The following table identifies monitoring data that is collected from DMV datasets on Azure Arc-enabled SQL Server when the monitoring feature is enabled.  No personally identifiable information (PII), end-user identifiable information (EUII), or customer content is collected.

|DMV Name|Description|Collection Frequency|Collected Fields
|----|----|----|----|
|sqlserver_active_sessions|Sessions running a request, is a blocker, or has an open transaction|30 seconds|<li>sample_time_utc<li>sql_server_instance_name<li>machine_name<li>database_id<li>database_name<li>session_id<li>session_status<li>connection_id|
|sqlserver_cpu_utilization|CPU utilization over time.|10 seconds|<li>sample_time_utc<li>process_sample_time_utc<li>sql_server_instance_name<li>machine_name<li>avg_cpu_percent<li>sql_process_cpu_percent<li>other_process_cpu_percent<li>idle_cpu_percent|
|sqlserver_database_properties|Includes database options and other database metadata.|5 minutes|<li>sample_time_utc<li>collection_time_utc<li>sql_server_instance_name<li>machine_name<li>database_id<li>database_name<li>is_primary_replica<li>create_date<li>compatibility_level<li>collation_name<li>user_access_desc<li>is_read_only<li>updateability<li>is_auto_shrink_on<li>state_desc<li>is_in_standby<li>snapshot_isolation_state<li>is_read_committed_snapshot_on<li>recovery_model_desc<li>page_verify_option_desc<li>is_auto_create_stats_on<li>is_auto_update_stats_on<li>is_auto_update_stats_async_on<li>is_parameterization_forced<li>is_published<li>is_subscribed<li>is_merge_published<li>is_distributor<li>is_broker_enabled<li>log_reuse_wait_desc<li>containment_desc<li>is_cdc_enabled<li>is_encrypted<li>delayed_durability_desc<li>is_accelerated_database_recovery_on<li>last_good_checkdb_time<li>notable_db_scoped_configs<li>query_store_actual_state_desc<li>query_store_query_capture_mode_desc<li>force_last_good_plan_actual_state<li>count_suspect_pages<li>is_ledger_on<li>is_change_feed_enabled|
|sqlserver_database_storage_utilization|Includes its storage usage and persistent version store.|1 minute|<li>sample_time_utc<li>collection_time_utc<li>sql_server_instance_name<li>machine_name<li>database_id<li>database_name<li>is_primary_replica<li>data_size_used_mb<li>data_size_allocated_mb<li>count_data_files<li>log_size_used_mb<li>log_size_allocated_mb<li>count_log_files<li>persistent_version_store_size_mb<li>online_index_version_store_size_mb|
|sqlserver_memory_utilization|Memory clerks and memory consumption by the clerk.|10 seconds|<li>sample_time_utc<li>sql_server_instance_name<li>machine_name<li>memory_clerk_type<li>memory_clerk_name<li>memory_size_mb|
|sqlserver_performance_counters_common|Includes common performance counters recorded by SQL Server.|10 seconds|<b>List of common counters:</b> <li>Background writer pages/sec<li>Buffer cache hit ratio<li>Checkpoint pages/sec<li>Lazy writes/sec<li>Page reads/sec<li>Page writes/sec<li>Readahead pages/sec<li>Page life expectancy<li>Log Bytes Flushed/sec<li>Transactions/sec<li>Write Transactions/sec<li>Active Transactions<li>Log Flushes/sec<li>Active Temp Tables<li>Logical Connections<li>Logins/sec<li>Logouts/sec<li>Processes blocked<li>User Connections<li>Temp Tables Creation Rate<li>Latch Waits/sec<li>Number of Deadlocks/sec<li>Target Server Memory (KB)<li>Total Server Memory (KB)<li>Granted Workspace Memory (KB)<li>Lock Memory (KB)<li>Cache Hit Ratio<li>Errors/sec<li>Batch Requests/sec<li>SQL Attention rate<li>SQL Compilations/sec<li>SQL Re-Compilations/sec<li>Free Space in tempdb (KB)<li>Out of memory count<li>System memory signal state high<li>System memory signal state low<li>OS available physical memory (KB)<li>Process physical memory in use (KB)<li>Locked page allocations (KB)<li>Process physical memory low |
|sqlserver_performance_counters_detailed|Includes detailed performance counters recorded by SQL Server.|10 seconds|<b>List of detailed counters:</b> <li>Page lookups/sec"<li>Free list stalls/sec"<li>Database pages"<li>Local node page lookups/sec"<li>Remote node page lookups/sec"<li>Log Growths"<li>Bulk Copy Rows/sec"<li>Bulk Copy Throughput/sec"<li>Shrink Data Movement Bytes/sec"<li>Log Shrinks"<li>Log File(s) Used Size (KB)"<li>Log File(s) Size (KB)"<li>Data File(s) Size (KB)"<li>Backup/Restore Throughput/sec"<li>Log Flush Wait Time"<li>XTP Memory Used (KB)"<li>Percent Log Used"<li>Temp Tables For Destruction"<li>Average Wait Time (ms)"<li>Lock Timeouts (timeout > 0)/sec"<li>Connection Memory (KB)"<li>Optimizer Memory (KB)"<li>Cache Object Counts"<li>Errors/sec"<li>Failed Auto-Params/sec"<li>Version Cleanup rate (KB/s)"<li>Version Generation rate (KB/s)"<li>Version Store Size (KB)"<li>Large page allocations (KB)"<li>Process virtual memory low" |
|sqlserver_storage_io|Includes cumulative IOPS, throughput, and latency statistics.|10 seconds|<li>sample_time_utc<li>sql_server_instance_name<li>machine_name<li>database_id<li>database_name<li>file_id<li>file_type<li>file_size_mb<li>file_max_size_mb<li>io_stall_read_ms<li>num_of_reads<li>num_of_bytes_read<li>io_stall_write_ms<li>num_of_writes<li>num_of_bytes_written<li>io_stall_queued_read_ms<li>io_stall_queued_write_ms<li>size_on_disk_bytes|
|sqlserver_wait_stats|Includes wait types and wait statistics for the database engine instance.|10 seconds|<li>sample_time_utc<li>sql_server_instance_name<li>machine_name<li>wait_type<li>wait_time_ms<li>resource_wait_time_ms<li>signal_wait_time_ms<li>max_wait_time_ms<li>waiting_tasks_count<li>wait_category|

## Limitations
- After adding or removing a SQL instance on your Windows machine, you must restart the sqlServer extension service for the update to take effect.

## Next steps
  
- [Azure Arc-enabled SQL Server and Databases activity logs](activity-logs.md)
- [Azure Arc-enabled SQL Server data collection and reporting](data-collection.md)