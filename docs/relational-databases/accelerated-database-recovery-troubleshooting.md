---
description: "Troubleshoot accelerated database recovery"
title: "Troubleshoot accelerated database recovery"
ms.date: "01/20/2022"
ms.prod: sql
ms.prod_service: backup-restore
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: kfarlee
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# Troubleshooting accelerated database recovery

[!INCLUDE [SQL Server 2019, ASDB, ASDBMI ](../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

This article helps administrators diagnose issues with accelerated database recovery (ADR).

## Examine the persistent version store (PVS)

Leverage the `sys.dm_tran_persistent_version_store_stats` DMV to identify if the size of the PVS is growing larger than expected, and then to determine which factor is preventing persistent version store (PVS) cleanup.

The sample query shows all information about the cleanup processes and shows the current PVS size, oldest aborted transaction_id, and other details:

```sql

SELECT 
 db_name(pvss.database_id) AS DBName,
 pvss.persistent_version_store_size_kb / 1024. / 1024 AS persistent_version_store_size_gb,
 100 * pvss.persistent_version_store_size_kb / df.total_db_size_kb AS pvs_pct_of_database_size,
 df.total_db_size_kb/1024./1024 AS total_db_size_gb,
 pvss.online_index_version_store_size_kb / 1024. / 1024 AS online_index_version_store_size_gb,
 pvss.current_aborted_transaction_count,
 pvss.aborted_version_cleaner_start_time,
 pvss.aborted_version_cleaner_end_time,
 dt.database_transaction_begin_time AS oldest_transaction_begin_time,
 asdt.session_id AS active_transaction_session_id,
 asdt.elapsed_time_seconds AS active_transaction_elapsed_time_seconds
FROM sys.dm_tran_persistent_version_store_stats AS pvss
CROSS APPLY (SELECT SUM(size*8.) AS total_db_size_kb FROM sys.database_files WHERE [state] = 0 and [type] = 0 ) AS df 
LEFT JOIN sys.dm_tran_database_transactions AS dt
ON pvss.oldest_active_transaction_id = dt.transaction_id
   AND
   pvss.database_id = dt.database_id
LEFT JOIN sys.dm_tran_active_snapshot_database_transactions AS asdt
ON pvss.min_transaction_timestamp = asdt.transaction_sequence_num
   OR
   pvss.online_index_min_transaction_timestamp = asdt.transaction_sequence_num
WHERE pvss.database_id = DB_ID();
```

1. Check `pvs_pct_of_database_size` size, note any difference from the typical, compared to baselines during other periods of application activity. PVS is considered large if it's significantly larger than baseline or if it is close to 50% of the size of the database. Use the following steps as a troubleshooting aid for a PVS that is large.

2. Active transactions prevent cleaning up the PVS. Retrieve `oldest_active_transaction_id` and check whether this transaction has been active for a really long time by querying `sys.dm_tran_database_transactions` based on the transaction ID. Check for long-running, active transactions with a query like the below sample, which declares variables to set thresholds for duration or log amount:

    ```sql
    DECLARE @longTxThreshold int = 1800; --number of seconds to use as a duration threshold for long-running transactions
    DECLARE @longTransactionLogBytes bigint = 2147483648; --number of bytes to use as a log amount threshold for long-running transactions
    
    SELECT
        dbtr.database_id, 
        transess.session_id,  
        transess.transaction_id , 
        atr.name, 
        sess.login_time,  
        dbtr.database_transaction_log_bytes_used, 
        CASE
           WHEN getdate() >= dateadd(second, @longTxThreshold, tr.transaction_begin_time) then 'DurationThresholdExceeded' 
           WHEN dbtr.database_transaction_log_bytes_used >= @longTransactionLogBytes then 'LogThresholdExceeded' 
           ELSE 'unknown' END AS Reason 
      FROM
        sys.dm_tran_active_transactions AS tr  
        INNER JOIN sys.dm_tran_session_transactions AS transess on tr.transaction_id = transess.transaction_id  
        INNER JOIN sys.dm_exec_sessions AS sess on transess.session_id = sess.session_id 
        INNER JOIN sys.dm_tran_database_transactions AS dbtr on tr.transaction_id = dbtr.transaction_id 
        INNER JOIN sys.dm_tran_active_transactions AS atr on atr.transaction_id = transess.transaction_id 
    WHERE transess.session_id <> @@spid AND 
        ( getdate() >= dateadd(second, @longTxThreshold, tr.transaction_begin_time) OR
          dbtr.database_transaction_log_bytes_used >= @longTransactionLogBytes );
    ```
    
    With the session(s) identified, consider killing the session, if allowed. Also, review the application to determine the nature of the problematic active transaction(s). 

3. The persistent version cleanup may be held up due to long active snapshot scan(s). If `pvs_off_row_page_skipped_min_useful_xts` shows a large value <!TODO>, it means there is a long snapshot scan preventing PVS cleanup. Below query can be used to decide which is the session,  

    ```sql
    SELECT snap.transaction_id, snap.transaction_sequence_num, session.session_id, session.login_time, GETUTCDATE() as now, session.host_name, session.program_name, session.login_name, session.last_request_start_time 
    FROM sys.dm_tran_active_snapshot_database_transactions AS snap
    INNER JOIN sys.dm_exec_sessions AS session ON snap.session_id = session.session_id  
    ORDER BY snap.transaction_sequence_num asc;
    ```
    
    The solution is same as long active transaction, consider killing the session, if allowed. Also, review the application to determine the nature of the problematic active snapshot scan. 

4. When the PVS size is growing due to long running transactions on primary or secondary replicas, investigate the long running queries and address the bottleneck. The `sys.dm_tran_aborted_transactions` DMV shows all aborted transactions. The `is_nest_abort` column indicates that the transaction was committed, but there are portions that aborted (savepoints/nested transactions) which can block the PVS cleanup process. 

5. If the database is part of an availability group, check the `secondary_low_water_mark`. This is the same as the `low_water_mark_for_ghosts` reported by `sys.dm_hadr_database_replica_states`. Query `sys.dm_hadr_database_replica_states` to see whether one of the replicas is holding this value behind, since this will also prevent PVS cleanup. The version cleanup is held up due to read queries on readable secondaries. Both SQL Server on-premise and Azure SQL DB support readable secondaries. In the `sys.dm_tran_persistent_version_store_stats` DMV, the `pvs_off_row_page_skipped_low_water_mark` can also give indications of a secondary replica delay. 

    The solution is same as snapshot scan hold up. Go to the secondaries, find the session which is issuing the long query and consider killing the session, if allowed. Note that the secondary hold up not only impacts ADR version cleanup, it can also prevent ghost records clean up. 

6. Check `min_transaction_timestamp` (or `online_index_min_transaction_timestamp` if the online PVS is holding up) and based on that check `sys.dm_tran_active_snapshot_database_transactions` for the column `transaction_sequence_num` to find the session that has the old snapshot transaction holding up PVS cleanup.

7. If none of the above applies, then it means that the cleanup is held by aborted transactions. Check the last time the `aborted_version_cleaner_last_start_time`  and `aborted_version_cleaner_last_end_time` to see if the aborted transaction cleanup has completed. The `oldest_aborted_transaction_id` should be moving higher after the aborted transaction cleanup completes. If the `oldest_aborted_transaction_id` is much smaller than `oldest_active_transaction_id`, and `current_abort_transaction_count` has a great value, there is an old aborted transaction preventing PVS cleanup. To address:

    - If possible, stop the workload to let version cleaner making progress. 
    - Optimize the workload to reduce object level lock usage. 
    - Review the application to see any high transaction abort issue. Deadlock, duplicate key and other constraint violations may trigger high abort rate.  
    - If on SQL Server, disable ADR as an emergency-only step to control both PVS size and abort transaction number. See [Disable the ADR feature](accelerated-database-recovery-management.md#to-disable-the-adr-feature).

8. If the aborted transaction hasn't completed successfully recently, check the error log for messages reporting `VersionCleaner` issues.

9. Monitor the SQL Server error log for 'PreallocatePVS' entries. If there are 'PreallocatePVS' entries present, then this means you may need to increase the ADR ability to preallocate pages for background tasks as performance can be improved when the ADR background thread preallocates enough pages and the percentage of foreground PVS allocations is close to 0. You can use the `sp_configure 'ADR Preallocation Factor'` to increase this amount. For more information, see [ADR preallocation factor server configuration option](../database-engine/configure-windows/adr-preallocation-factor-server-configuration-option.md).

## Start PVS cleanup process manually 

ADR is not recommended for database environments with a high transaction count of update/deletes, such as high-volume OLTP, without a period of rest/recovery for the PVS cleanup process to reclaim space. 

To activate the PVS cleanup process manually between workloads or during maintenance windows, use the system stored procedure [sys.sp_persistent_version_cleanup](system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md). 

```sql
EXEC sys.sp_persistent_version_cleanup [database_name]; 
```

For example,

```sql
EXEC sys.sp_persistent_version_cleanup [WideWorldImporters];
```

## Next steps 

- [Manage accelerated database recovery](accelerated-database-recovery-management.md)