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
monikerRange: ">=sql-server-ver15"
---
# Troubleshooting accelerated database recovery

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article helps administrators diagnose issues with accelerated database recovery (ADR).

## Examine the persistent version store (PVS)

Leverage the `sys.dm_tran_persistent_version_store_stats` ​DMV to identify if the size of the PVS is growing larger than expected. This DMV shows all information about the cleanup processes and shows the current PVS size, oldest aborted transaction_id, and other details. For example:

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

2. Retrieve `oldest_active_transaction_id` and check whether this transaction has been active for a really long time by querying `sys.dm_tran_database_transactions` based on the transaction ID.

```sql
--TODO I can never seem to see transactions where `oldest_active_transaction_id` exists inside `sys.dm_tran_database_transactions`?
SELECT * FROM sys.dm_tran_persistent_version_store_stats WHERE database_id = DB_ID();
SELECT * FROM sys.dm_tran_database_transactions WHERE database_id = DB_ID();
```

> [!NOTE]
> Active transactions prevent cleaning up PVS.

3. When the PVS size is growing due to long running transactions on primary or secondary, investigate the long running queries and address the bottleneck. The `sys.dm_tran_aborted_transactions` ​DMV shows all aborted transactions. The `is_nest_abort` column indicates that the transaction was committed, but there are portions that aborted (savepoints/nested transactions) which can block the PVS cleanup process. 
4. If the database is part of an availability group, check the `secondary_low_water_mark`. This is the same as the `low_water_mark_for_ghosts` reported by `sys.dm_hadr_database_replica_states`. Query `sys.dm_hadr_database_replica_states` to see whether one of the replicas is holding this value behind, since this will also prevent PVS cleanup.
5. Check `min_transaction_timestamp` (or `online_index_min_transaction_timestamp` if the online PVS is holding up) and based on that check `sys.dm_tran_active_snapshot_database_transactions` for the column `transaction_sequence_num` to find the session that has the old snapshot transaction holding up PVS cleanup.
6. If none of the above applies, then it means that the cleanup is held by aborted transactions. Check the last time the `aborted_version_cleaner_last_start_time`  and `aborted_version_cleaner_last_end_time` to see if the aborted transaction cleanup has completed. The `oldest_aborted_transaction_id` should be moving higher after the aborted transaction cleanup completes.
7. If the aborted transaction hasn't completed successfully recently, check the error log for messages reporting `VersionCleaner` issues.
8. Monitor the SQL Server error log for 'PreallocatePVS' entries. If there are 'PreallocatePVS' entries present, then this means you may need to increase the ADR ability to preallocate pages for background tasks as performance can be improved when the ADR background thread preallocates enough pages and the percentage of foreground PVS allocations is close to 0. You can use the `sp_configure 'ADR Preallocation Factor'` to increase this amount. For more information, see [ADR preallocation factor server configuration option](../database-engine/configure-windows/adr-preallocation-factor-server-configuration-option.md).

## Start PVS cleanup process manually 

ADR is not recommended for database environments with a high transaction count of update/deletes, such as high-volume OLTP, without a period of rest/recovery for the PVS cleanup process to reclaim space. 

To activate the PVS cleanup process manually between workloads or during maintenance windows, use the system stored procedure [sys.sp_persistent_version_cleanup](../../relational-databases/system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md). 

```sql
EXEC sys.sp_persistent_version_cleanup [database_name]; 
```

For example,

```sql
EXEC sys.sp_persistent_version_cleanup [WideWorldImporters];
```

## Next steps 

- [Manage accelerated database recovery](accelerated-database-recovery-management.md)