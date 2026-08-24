---
title: "Monitor and Troubleshoot Accelerated Database Recovery"
description: "Monitor and troubleshoot accelerated database recovery and persistent version store"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: derekw, dfurman, randolphwest
ms.date: 04/18/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: troubleshooting-general
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15 || =azuresqldb-mi-current || =azuresqldb-current || =fabric-sqldb"
---

# Monitor and troubleshoot accelerated database recovery

[!INCLUDE [SQL Server 2019 Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../includes/applies-to-version/sqlserver2019-asdb-asdbmi-fabricsqldb.md)]

This article helps you monitor, diagnose, and resolve issues with [accelerated database recovery (ADR)](accelerated-database-recovery-concepts.md) in [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] and later, [!INCLUDE[ssazuremi-md](../includes/ssazuremi-md.md)], [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)], and [!INCLUDE[fabric-sqldb](../includes/fabric-sqldb.md)].

## Examine the size of the PVS

Use the [sys.dm_tran_persistent_version_store_stats](system-dynamic-management-views/sys-dm-tran-persistent-version-store-stats.md) DMV to identify if the persistent version store (PVS) size is larger than expected.

The following example diagnostic query shows the information about the current PVS size, the cleanup processes, and other details in all databases where PVS size is greater than zero:

# [SQL Server and SQL Managed Instance](#tab/sql-server-sql-mi)

```sql
SELECT pvss.database_id,
       DB_NAME(pvss.database_id) AS database_name,
       pvss.persistent_version_store_size_kb / 1024. / 1024 AS persistent_version_store_size_gb,
       100 * pvss.persistent_version_store_size_kb / df.total_db_size_kb AS pvs_percent_of_database_size,
       df.total_db_size_kb / 1024. / 1024 AS total_db_size_gb,
       pvss.online_index_version_store_size_kb / 1024. / 1024 AS online_index_version_store_size_gb,
       pvss.current_aborted_transaction_count,
       pvss.aborted_version_cleaner_start_time,
       pvss.aborted_version_cleaner_end_time,
       pvss.oldest_aborted_transaction_id,
       pvss.oldest_active_transaction_id,
       dt.database_transaction_begin_time AS oldest_transaction_begin_time,
       asdt.session_id AS active_transaction_session_id,
       asdt.elapsed_time_seconds AS active_transaction_elapsed_time_seconds,
       pvss.pvs_off_row_page_skipped_low_water_mark,
       pvss.pvs_off_row_page_skipped_min_useful_xts,
       pvss.pvs_off_row_page_skipped_oldest_aborted_xdesid
FROM sys.dm_tran_persistent_version_store_stats AS pvss
CROSS APPLY (
            SELECT SUM(size * 8.) AS total_db_size_kb
            FROM sys.master_files AS mf
            WHERE mf.database_id = pvss.database_id
                  AND
                  mf.state = 0
                  AND
                  mf.type = 0
            ) AS df
LEFT JOIN sys.dm_tran_database_transactions AS dt
ON pvss.oldest_active_transaction_id = dt.transaction_id
   AND
   pvss.database_id = dt.database_id
LEFT JOIN sys.dm_tran_active_snapshot_database_transactions AS asdt
ON pvss.min_transaction_timestamp = asdt.transaction_sequence_num
   OR
   pvss.online_index_min_transaction_timestamp = asdt.transaction_sequence_num
WHERE pvss.persistent_version_store_size_kb > 0
ORDER BY persistent_version_store_size_kb DESC;
```

# [SQL Database and SQL database in Fabric](#tab/sqldb)

```sql
SELECT pvss.database_id,
       DB_NAME(pvss.database_id) AS database_name,
       pvss.persistent_version_store_size_kb / 1024. / 1024 AS persistent_version_store_size_gb,
       100 * pvss.persistent_version_store_size_kb / IIF(pvss.database_id = 2, tdf.total_db_size_kb, df.total_db_size_kb) AS pvs_percent_of_database_size,
       IIF(pvss.database_id = 2, tdf.total_db_size_kb, df.total_db_size_kb) / 1024. / 1024 AS total_db_size_gb,
       pvss.online_index_version_store_size_kb / 1024. / 1024 AS online_index_version_store_size_gb,
       pvss.current_aborted_transaction_count,
       pvss.aborted_version_cleaner_start_time,
       pvss.aborted_version_cleaner_end_time,
       pvss.oldest_aborted_transaction_id,
       pvss.oldest_active_transaction_id,
       dt.database_transaction_begin_time AS oldest_transaction_begin_time,
       asdt.session_id AS active_transaction_session_id,
       asdt.elapsed_time_seconds AS active_transaction_elapsed_time_seconds,
       pvss.pvs_off_row_page_skipped_low_water_mark,
       pvss.pvs_off_row_page_skipped_min_useful_xts,
       pvss.pvs_off_row_page_skipped_oldest_aborted_xdesid
FROM sys.dm_tran_persistent_version_store_stats AS pvss
CROSS APPLY (
            SELECT SUM(size * 8.) AS total_db_size_kb
            FROM sys.database_files
            WHERE state = 0
                  AND
                  type = 0
            ) AS df
CROSS APPLY (
            SELECT SUM(size * 8.) AS total_db_size_kb
            FROM tempdb.sys.database_files
            WHERE state = 0
                  AND
                  type = 0
            ) AS tdf
LEFT JOIN sys.dm_tran_database_transactions AS dt
ON pvss.oldest_active_transaction_id = dt.transaction_id
   AND
   pvss.database_id = dt.database_id
LEFT JOIN sys.dm_tran_active_snapshot_database_transactions AS asdt
ON pvss.min_transaction_timestamp = asdt.transaction_sequence_num
   OR
   pvss.online_index_min_transaction_timestamp = asdt.transaction_sequence_num
WHERE pvss.database_id IN (DB_ID(), 2)
ORDER BY persistent_version_store_size_kb DESC;
```

---

Check the `pvs_percent_of_database_size` column to see the size of the PVS relative to the total database size. Note any difference between the typical PVS size and the baselines seen during typical periods of application activity. PVS is considered large if it's significantly larger than the baseline or if it's close to 50% of the database size.

If the size of PVS is not decreasing, use the following troubleshooting steps to find and resolve the reason for large PVS size.

> [!TIP]
> Columns mentioned in the following troubleshooting steps refer to the columns in the result set of the diagnostic query in this section.

Large PVS size might be caused by any of the following reasons:
- [Long-running active transactions](#check-for-long-running-active-transactions)
- [Long-running active snapshot scans](#check-for-long-running-active-transactions)
- [Long-running queries on secondary replicas](#check-for-long-running-queries-on-secondary-replicas)
- [Aborted transactions](#check-for-large-number-of-aborted-transactions) 

## Check for long-running active transactions

Long-running active transactions can prevent PVS cleanup in databases that have ADR enabled. Check the start time of the oldest active transaction using the `oldest_transaction_begin_time` column. To find long-running transactions, use the following example query. You can set thresholds for transaction duration and the amount of generated transaction log:

```sql
DECLARE @LongTxThreshold int = 900;  /* The number of seconds to use as a duration threshold for long-running transactions */
DECLARE @LongTransactionLogBytes bigint = 1073741824; /* The number of bytes to use as the generated log threshold for long-running transactions */

SELECT  dbtr.database_id,
        DB_NAME(dbtr.database_id) AS database_name,
        st.session_id,
        st.transaction_id,
        atr.name,
        sess.login_time,
        dbtr.database_transaction_log_bytes_used,
        CASE WHEN GETDATE() >= DATEADD(second, @LongTxThreshold, tr.transaction_begin_time) THEN 'DurationThresholdExceeded'
             WHEN dbtr.database_transaction_log_bytes_used >= @LongTransactionLogBytes THEN 'LogThresholdExceeded'
             ELSE 'Unknown'
        END
        AS reason
FROM sys.dm_tran_active_transactions AS tr
INNER JOIN sys.dm_tran_session_transactions AS st
ON tr.transaction_id = st.transaction_id
INNER JOIN sys.dm_exec_sessions AS sess
ON st.session_id = sess.session_id
INNER JOIN sys.dm_tran_database_transactions AS dbtr
ON tr.transaction_id = dbtr.transaction_id
INNER JOIN sys.dm_tran_active_transactions AS atr
ON atr.transaction_id = st.transaction_id
WHERE GETDATE() >= DATEADD(second, @LongTxThreshold, tr.transaction_begin_time)
      OR
      dbtr.database_transaction_log_bytes_used >= @LongTransactionLogBytes;
```

With the sessions identified, consider killing the session, if allowed. Review the application to determine the nature of the problematic transactions to avoid the problem in the future.

For more information about troubleshooting long-running queries, see: 
- [Troubleshooting slow running queries in SQL Server](/troubleshoot/sql/performance/troubleshoot-slow-running-queries)
- [Detectable types of query performance bottlenecks in Azure SQL Database](/azure/azure-sql/database/identify-query-performance-issues)
- [Detectable types of query performance bottlenecks in SQL Server and Azure SQL Managed Instance](/azure/azure-sql/managed-instance/identify-query-performance-issues)

<a id="pvs-active-snapshot-scans"></a>

## Check for long-running active snapshot scans

Long-running active snapshot scans can prevent PVS cleanup in databases that have ADR enabled. Statements using `READ COMMITTED` snapshot isolation (RCSI) or `SNAPSHOT` [isolation levels](../t-sql/statements/set-transaction-isolation-level-transact-sql.md) receive instance-level timestamps. A snapshot scan uses the timestamp to determine version row visibility for the RCSI or `SNAPSHOT` transaction. Every statement using RCSI has its own timestamp, whereas `SNAPSHOT` isolation has a transaction-level timestamp.

These instance-level transaction timestamps are used even in single-database transactions, because any transaction might be promoted to a cross-database transaction. Snapshot scans can, therefore, prevent PVS cleanup in any database on the same database engine instance. Similarly, even when ADR isn't enabled, snapshot scans can prevent cleanup of the version store in `tempdb`. As a result, PVS might grow in size when long-running transactions that use `SNAPSHOT` or RCSI are present.

The `pvs_off_row_page_skipped_min_useful_xts` column shows the number of pages skipped during cleanup due to a long snapshot scan. If this column shows a large value, it means that a long snapshot scan is preventing PVS cleanup.

Use the following example query to find the sessions with the long-running `SNAPSHOT` or RCSI transaction:

```sql
SELECT sdt.transaction_id,
       sdt.transaction_sequence_num,
       s.database_id,
       s.session_id,
       s.login_time,
       GETDATE() AS query_time,
       s.host_name,
       s.program_name,
       s.login_name,
       s.last_request_start_time
FROM sys.dm_tran_active_snapshot_database_transactions AS sdt
INNER JOIN sys.dm_exec_sessions AS s
ON sdt.session_id = s.session_id;
```

To prevent PVS cleanup delays:

- Consider killing the long active transaction session that is delaying PVS cleanup, if possible.
- Tune long-running queries to reduce query duration.
- Review the application to determine the nature of the problematic active snapshot scan. Consider a different isolation level, such as `READ COMMITTED`, instead of `SNAPSHOT` or RCSI for long-running queries that are delaying PVS cleanup. This problem occurs more frequently with the `SNAPSHOT` isolation level.
- In Azure SQL Database elastic pools, consider moving databases that have long-running transactions using `SNAPSHOT` isolation or RCSI out of the elastic pool to avoid PVS cleanup delay in other databases in the same pool.

## Check for long-running queries on secondary replicas

If the database has secondary replicas, check if the secondary low watermark is advancing.

A large value in the `pvs_off_row_page_skipped_low_water_mark` column might be an indication of a cleanup delay because of a long-running query on a secondary replica. In addition to holding up PVS cleanup, a long-running query on a secondary replica can also hold up [ghost cleanup](ghost-record-cleanup-process-guide.md).

You can use the following example queries on the primary replica to find if long-running queries on secondary replicas might be preventing PVS cleanup. If a write workload is running on the primary replica, but the value in the `low_water_mark_for_ghosts` column isn't increasing from one execution of the example query to the next, then PVS and ghost cleanup might be held up by a long-running query on a secondary replica.

# [SQL Server and SQL Managed Instance](#tab/sql-server-sql-mi)

```sql
SELECT database_id,
       DB_NAME(database_id) AS database_name,
       low_water_mark_for_ghosts,
       synchronization_state_desc,
       synchronization_health_desc,
       is_suspended,
       suspend_reason_desc,
       secondary_lag_seconds
FROM sys.dm_hadr_database_replica_states
WHERE is_local = 1
      AND
      is_primary_replica = 1;
```

For more information, see the description of the `low_water_mark_for_ghosts` column in [sys.dm_hadr_database_replica_states](system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md).

# [SQL Database and SQL database in Fabric](#tab/sqldb)

```sql
SELECT database_id,
       DB_NAME(database_id) AS database_name,
       low_water_mark_for_ghosts,
       synchronization_state_desc,
       synchronization_health_desc,
       is_suspended,
       suspend_reason_desc,
       secondary_lag_seconds
FROM sys.dm_database_replica_states
WHERE is_local = 1
      AND
      is_primary_replica = 1;
```

For more information, see the description of the `low_water_mark_for_ghosts` column in [sys.dm_database_replica_states](system-dynamic-management-views/sys-dm-database-replica-states-azure-sql-database.md).

---

Connect to each readable secondary replica, find the session with the long-running query and consider killing the session, if allowed. For more information, see [Find slow queries](/troubleshoot/sql/performance/troubleshoot-slow-running-queries#find-slow-queries).

## Check for large number of aborted transactions

Check the `aborted_version_cleaner_start_time` and `aborted_version_cleaner_end_time` columns to see if the last aborted transaction cleanup has completed. The `oldest_aborted_transaction_id` should be moving higher after the aborted transaction cleanup completes. If the `oldest_aborted_transaction_id` is much lower than `oldest_active_transaction_id`, and the `current_abort_transaction_count` value is large, there's likely an old aborted transaction preventing PVS cleanup.

To solve PVS cleanup delay due to a large number of aborted transactions, consider the following:

- If you are using [!INCLUDE [sql-server-2022](../includes/sssql22-md.md)], increase the value of the `ADR Cleaner Thread Count` server configuration. For more information, see [Server configuration: ADR Cleaner Thread Count](../database-engine/configure-windows/adr-cleaner-thread-count-configuration-option.md).
- If possible, stop the workload to let the version cleaner make progress.
- Review the application to identify and resolve the high transaction abort rate issue. The aborts might come from a high rate of deadlocks, duplicate keys, constraint violations, or query timeouts.
- Optimize the workload to reduce locks that are incompatible with the object-level or partition-level `IX` locks required by the PVS cleaner. For more information, see [Lock compatibility](sql-server-transaction-locking-and-row-versioning-guide.md#lock_compatibility).
- If using [!INCLUDE [ssnoversion-md.md](../includes/ssnoversion-md.md)], disable ADR as an emergency-only step to control PVS size. See [Disable ADR](accelerated-database-recovery-management.md#disable-adr).
- If using [!INCLUDE [ssnoversion-md.md](../includes/ssnoversion-md.md)], and if the aborted transaction cleanup hasn't recently completed successfully, check the error log for messages reporting `VersionCleaner` issues.
- If PVS size isn't reduced as expected even after a cleanup has completed, check the `pvs_off_row_page_skipped_oldest_aborted_xdesid` column. Large values indicate space is still being used by row versions from aborted transactions.

## Control PVS size

If you have a workload with a high volume of DML statements (`INSERT`, `UPDATE`, `DELETE`, `MERGE`), such as high-volume OLTP, and observe that PVS size is large, you might need to increase the value of the `ADR Cleaner Thread Count` server configuration to keep PVS size under control. For more information, see [Server configuration: ADR Cleaner Thread Count](../database-engine/configure-windows/adr-cleaner-thread-count-configuration-option.md), which is available starting from [!INCLUDE [sql-server-2022](../includes/sssql22-md.md)].

In [!INCLUDE [sql-server-2019](../includes/sssql19-md.md)], or if increasing the value of `ADR Cleaner Thread Count` configuration doesn't help reduce PVS size sufficiently, the workload might require a period of rest/recovery for the PVS cleanup process to reclaim space.

To activate the PVS cleanup process manually between workloads or during maintenance windows, use the system stored procedure [sys.sp_persistent_version_cleanup](system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md).

For example:

```sql
EXEC sys.sp_persistent_version_cleanup [WideWorldImporters];
```

An active transaction might prevent the PVS cleanup process from starting. If this occurs, the session running the `sys.sp_persistent_version_cleanup` stored procedure waits with the [PVS_CLEANUP_LOCK](./system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md#pvs_cleanup_lock) wait type. You can wait for the transaction to complete, or you can consider killing the blocker session with an active transaction, if possible.

## Capture cleanup failures

Beginning with [!INCLUDE [sql-server-2022](../includes/sssql22-md.md)], notable PVS cleanup messages are recorded in the error log. Cleanup statistics are also reported by the `tx_mtvc2_sweep_stats` [extended event](extended-events/extended-events.md).

## Known issues

- In [!INCLUDE [sql-server-2025](../includes/sssql25-md.md)], when ADR in `tempdb` is enabled and temporary tables are created and dropped (or truncated) at a high rate, workload throughput might be substantially reduced because of latch contention on the `sys.sysseobjvalues` system table. This issue is under investigation. A fix is planned for a later release.

## Related content

- [Accelerated database recovery](accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](accelerated-database-recovery-management.md)
- [sys.dm_tran_persistent_version_store_stats (Transact-SQL)](system-dynamic-management-objects/sys-dm-tran-persistent-version-store-stats.md)
- [sys.sp_persistent_version_cleanup (Transact-SQL)](system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md)
- [sys.dm_tran_aborted_transactions (Transact-SQL)](system-dynamic-management-objects/sys-dm-tran-aborted-transactions.md)
