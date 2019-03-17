---
title: "sys.dm_db_xtp_checkpoint_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: "language-reference"
f1_keywords: 
  - "dm_db_xtp_checkpoint_stats"
  - "dm_db_xtp_checkpoint_stats_TSQL"
  - "sys.dm_db_xtp_checkpoint_stats"
  - "sys.dm_db_xtp_checkpoint_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_xtp_checkpoint_stats dynamic management view"
ms.assetid: 8d0b18ca-db4d-4376-9905-3e4457727c46
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_checkpoint_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]

  Returns statistics about the In-Memory OLTP checkpoint operations in the current database. If the database has no In-Memory OLTP objects, returns an empty result set.  
  
 For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
```  
USE In_Memory_db_name
SELECT * FROM sys.dm_db_xtp_checkpoint_stats;  
```  
  
**[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] is substantially different from more recent versions and is discussed lower in the topic at [SQL Server 2014](#bkmk_2014).**
  
## [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and later  
 The following table describes the columns in `sys.dm_db_xtp_checkpoint_stats`, beginning with **[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]**.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|last_lsn_processed|**bigint**|Last LSN seen by the controller.|  
|end_of_log_lsn|**numeric(38)**|The LSN of the end of log.|  
|bytes_to_end_of_log|**bigint**|Log bytes unprocessed by the controller, corresponding to the bytes between `last_lsn_processed` and `end_of_log_lsn`.|  
|log_consumption_rate|**bigint**|Rate of transaction log consumption by the controller (in KB/sec).|  
|active_scan_time_in_ms|**bigint**|Time spent by the controller in actively scanning the transaction log.|  
|total_wait_time_in_ms|**bigint**|Cumulative wait time for the controller while not scanning the log.|  
|waits_for_io|**bigint**|Number of waits for log IO incurred by the controller thread.|  
|io_wait_time_in_ms|**bigint**|Cumulative time spent waiting on log IO by the controller thread.|  
|waits_for_new_log_count|**bigint**|Number of waits incurred by the controller thread for a new log to be generated.|  
|new_log_wait_time_in_ms|**bigint**|Cumulative time spent waiting on a new log by the controller thread.|  
|idle_attempts_count|**bigint**|Number of times the controller transitioned to an idle state.|  
|tx_segments_dispatched|**bigint**|Number of segments seen by the controller and dispatched to the serializers. Segment is a contiguous portion of log that forms a unit of serialization. It is currently sized to 1MB, but can change in future.|  
|segment_bytes_dispatched|**bigint**|Total byte count of bytes dispatched by the controller to serializers, since the database restart.|  
|bytes_serialized|**bigint**|Total count of bytes serialized since database restart.|  
|serializer_user_time_in_ms|**bigint**|Time spent by serializers in user mode.|  
|serializer_kernel_time_in_ms|**bigint**|Time spent by serializers in kernel mode.|  
|xtp_log_bytes_consumed|**bigint**|Total count of log bytes consumed since the database restart.|  
|checkpoints_closed|**bigint**|Count of checkpoints closed since the database restart.|  
|last_closed_checkpoint_ts|**bigint**|Timestamp of the last closed checkpoint.|  
|hardened_recovery_lsn|**numeric(38)**|Recovery will start from this LSN.|  
|hardened_root_file_guid|**uniqueidentifier**|GUID of the root file that hardened as a result of the last completed checkpoint.|  
|hardened_root_file_watermark|**bigint**|**Internal Only**. How far it is valid to read the root file up to (this is an internally relevant type only - called BSN).|  
|hardened_truncation_lsn|**numeric(38)**|LSN of the truncation point.|  
|log_bytes_since_last_close|**bigint**|Bytes from last close to the current end of log.|  
|time_since_last_close_in_ms|**bigint**|Time since last close of the checkpoint.|  
|current_checkpoint_id|**bigint**|Currently new segments are being assigned to this checkpoint. The checkpoint system is a pipeline. The current checkpoint is the one which segments from the log are being assigned to. Once it's reached a limit, the checkpoint is released by the controller and a new one created as current.|  
|current_checkpoint_segment_count|**bigint**|Count of segments in the current checkpoint.|  
|recovery_lsn_candidate|**bigint**|**Internally Only**. Candidate to be picked as recoverylsn when current_checkpoint_id closes.|  
|outstanding_checkpoint_count|**bigint**|Number of checkpoints in the pipeline waiting to be closed.|  
|closing_checkpoint_id|**bigint**|ID of the closing checkpoint.<br /><br /> Serializers are working in parallel, so once they're finished then the checkpoint is a candidate to be closed by close thread. But the close thread can only close one at a time and it must be in order, so the closing checkpoint is the one that the close thread is working on.|  
|recovery_checkpoint_id|**bigint**|ID of the checkpoint to be used in recovery.|  
|recovery_checkpoint_ts|**bigint**|Time stamp of recovery checkpoint.|  
|bootstrap_recovery_lsn|**numeric(38)**|Recovery LSN for the bootstrap.|  
|bootstrap_root_file_guid|**uniqueidentifier**|GUID of the root file for the bootstrap.|  
|internal_error_code|**bigint**|Error seen by any of the controller, serializer, close, and merge threads.|
|bytes_of_large_data_serialized|**bigint**|The amount of data that was serialized. |  
  
##  <a name="bkmk_2014"></a> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 The following table describes the columns in `sys.dm_db_xtp_checkpoint_stats`, for **[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]**.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|log_to_process_in_bytes|**bigint**|The number of log bytes between the thread's current log sequence number (LSN) and the end-of-log.|  
|total_log_blocks_processed|**bigint**|Total number of log blocks processed since server startup.|  
|total_log_records_processed|**bigint**|Total number of log records processed since server startup.|  
|xtp_log_records_processed|**bigint**|Total number of In-Memory OLTP log records processed since server startup.|  
|total_wait_time_in_ms|**bigint**|Cumulative wait time in ms.|  
|waits_for_io|**bigint**|Number of waits for log IO.|  
|io_wait_time_in_ms|**bigint**|Cumulative time spent waiting on log IO.|  
|waits_for_new_log|**bigint**|Number of waits for new log to be generated.|  
|new_log_wait_time_in_ms|**bigint**|Cumulative time spend waiting on new log.|  
|log_generated_since_last_checkpoint_in_bytes|**bigint**|Amount of log generated since the last In-Memory OLTP checkpoint.|  
|ms_since_last_checkpoint|**bigint**|Amount of time in milliseconds since the last In-Memory OLTP checkpoint.|  
|checkpoint_lsn|**numeric (38)**|The recovery log sequence number (LSN) associated with the last completed In-Memory OLTP checkpoint.|  
|current_lsn|**numeric (38)**|The LSN of the log record that is currently processing.|  
|end_of_log_lsn|**numeric (38)**|The LSN of the end of the log.|  
|task_address|**varbinary(8)**|The address of the SOS_Task. Join to sys.dm_os_tasks to find additional information.|  
  
## Permissions  
 Requires `VIEW DATABASE STATE` permission on the server.  
  
## See Also  
 [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)  
  
  
