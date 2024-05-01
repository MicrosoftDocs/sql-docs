---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 04/24/2024
ms.topic: include
---

### Active sessions

Description: Sessions running a request, is a blocker, or has an open transaction.\
Dataset Name: SqlServerActiveSessions\
Collection Frequency: 30 seconds\
**Collected Fields:**

* connection_id
* database_id
* database_name
* machine_name
* sample_time_utc
* session_id
* session_status
* sql_server_instance_name

### CPU Utilization

Description: CPU utilization over time.\
Dataset Name: SqlServerCPUUtilization\
Collection Frequency: 10 seconds\
**Collected Fields:**

* avg_cpu_percent
* idle_cpu_percent
* machine_name
* other_process_cpu_percent
* process_sample_time_utc
* sample_time_utc
* sql_process_cpu_percent
* sql_server_instance_name

### Database properties

Description: Includes database options and other database metadata.\
Dataset Name: SqlServerDatabaseProperties\
Collection Frequency: 5 minutes\
**Collected Fields:**

* collation_name
* collection_time_utc
* compatibility_level
* containment_desc
* count_suspect_pages
* create_date
* database_id
* database_name
* delayed_durability_desc
* force_last_good_plan_actual_state
* is_accelerated_database_recovery_on
* is_auto_create_stats_on
* is_auto_shrink_on
* is_auto_update_stats_async_on
* is_auto_update_stats_on
* is_broker_enabled
* is_cdc_enabled
* is_change_feed_enabled
* is_distributor
* is_encrypted
* is_in_standby
* is_ledger_on
* is_merge_published
* is_parameterization_forced
* is_primary_replica
* is_published
* is_read_committed_snapshot_on
* is_read_only
* is_subscribed
* last_good_checkdb_time
* log_reuse_wait_desc
* machine_name
* notable_db_scoped_configs
* page_verify_option_desc
* query_store_actual_state_desc
* query_store_query_capture_mode_desc
* recovery_model_desc
* sample_time_utc
* snapshot_isolation_state
* sql_server_instance_name
* state_desc
* updateability
* user_access_desc

### Database storage utilization

Description: Includes its storage usage and persistent version store.\
Dataset Name: SqlServerDatabaseStorageUtilization\
Collection Frequency: 1 minute\
**Collected Fields:**

* collection_time_utc
* count_data_files
* count_log_files
* data_size_allocated_mb
* data_size_used_mb
* database_id
* database_name
* is_primary_replica
* log_size_allocated_mb
* log_size_used_mb
* machine_name
* online_index_version_store_size_mb
* persistent_version_store_size_mb
* sample_time_utc
* sql_server_instance_name

### Memory utilization

Description: Memory clerks and memory consumption by the clerk.\
Dataset Name: SqlServerMemoryUtilization\
Collection Frequency: 10 seconds\
**Collected Fields:**

* machine_name
* memory_size_mb
* memory_clerk_name
* memory_clerk_type
* sample_time_utc
* sql_server_instance_name

### Performance counters (common)

Description: Includes common performance counters recorded by SQL Server.\
Dataset Name: SqlServerPerformanceCountersCommon\
Collection Frequency: 1 minute\
**Collected Counters:**

* Active Temp Tables
* Active Transactions
* Background writer pages/sec
* Batch Requests/sec
* Buffer cache hit ratio
* Cache Hit Ratio
* Checkpoint pages/sec
* Errors/sec
* Free Space in tempdb (KB)
* Granted Workspace Memory (KB)
* Latch Waits/sec
* Lazy writes/sec
* Lock Memory (KB)
* Locked page allocations (KB)
* Log Bytes Flushed/sec
* Log Flushes/sec
* Logical Connections
* Logins/sec
* Logouts/sec
* Number of Deadlocks/sec
* OS available physical memory (KB)
* Out of memory count
* Page life expectancy
* Page reads/sec
* Page writes/sec
* Process physical memory in use (KB)
* Process physical memory low
* Processes blocked
* Readahead pages/sec
* SQL Attention rate
* SQL Compilations/sec
* SQL Re-Compilations/sec
* System memory signal state high
* System memory signal state low
* Target Server Memory (KB)
* Temp Tables Creation Rate
* Total Server Memory (KB)
* Transactions/sec
* User Connections
* Write Transactions/sec

### Performance counters (detailed)

Description: Includes detailed performance counters recorded by SQL Server.\
Dataset Name: SqlServerPerformanceCountersDetailed\
Collection Frequency: 1 minute\
**Collected Counters:**

* Average Wait Time (ms)
* Backup/Restore Throughput/sec
* Bulk Copy Rows/sec
* Bulk Copy Throughput/sec
* Cache Object Counts
* Connection Memory (KB)
* Data File Size (KB)
* Database pages
* Errors/sec
* Failed Auto-Params/sec
* Free list stalls/sec
* Large page allocations (KB)
* Local node page lookups/sec
* Lock Timeouts (timeout > 0)/sec
* Log File Size (KB)
* Log File Used Size (KB)
* Log Flush Wait Time
* Log Growths
* Log Shrinks
* Optimizer Memory (KB)
* Page lookups/sec
* Percent Log Used
* Process virtual memory low
* Remote node page lookups/sec
* Shrink Data Movement Bytes/sec
* Temp Tables For Destruction
* Version Cleanup rate (KB/s)
* Version Generation rate (KB/s)
* Version Store Size (KB)
* XTP Memory Used (KB)

### Storage I/O

Description: Includes cumulative IOPS, throughput, and latency statistics.\
Dataset Name: SqlServerStorageIO\
Collection Frequency: 10 seconds\
**Collected Fields:**

* database_id
* database_name
* file_id
* file_max_size_mb
* file_size_mb
* file_type
* io_stall_queued_read_ms
* io_stall_queued_write_ms
* io_stall_read_ms
* io_stall_write_ms
* machine_name
* num_of_bytes_read
* num_of_bytes_written
* num_of_reads
* num_of_writes
* sample_time_utc
* size_on_disk_bytes
* sql_server_instance_name

### Wait stats

Description: Includes wait types and wait statistics for the database engine instance.\
Dataset Name: SqlServerWaitStats\
Collection Frequency: 10 seconds\
**Collected Fields:**

* machine_name
* max_wait_time_ms
* resource_wait_time_ms
* sample_time_utc
* signal_wait_time_ms
* sql_server_instance_name
* wait_category
* wait_time_ms
* wait_type
* waiting_tasks_count
