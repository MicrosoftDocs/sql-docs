---
title: "Accelerated Database Recovery (ADR)"
description: "Learn about accelerated database recovery (ADR), which redesigned the database engine recovery process to significantly speed up recovery and improve database availability in SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric."
author: dnethi
ms.author: dinethi
ms.reviewer: wiassaf, derekw, dfurman
ms.date: 12/17/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15 || =azuresqldb-mi-current || =azuresqldb-current || =fabric-sqldb"
---

# Accelerated database recovery

[!INCLUDE [SQL Server 2019 Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../includes/applies-to-version/sqlserver2019-asdb-asdbmi-fabricsqldb.md)]

Accelerated database recovery (ADR) improves database availability, especially in the presence of long-running transactions, by redesigning the database engine recovery process.

ADR was introduced in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and improved in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and [!INCLUDE [sssql25-md](../includes/sssql25-md.md)]. ADR is also available in [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse_sqlpool_only](../includes/ssazuresynapse_sqlpool_only.md)], and [!INCLUDE [fabric-sqldb](../includes/fabric-sqldb.md)].

> [!NOTE]  
> ADR is always enabled in Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.

This article provides an overview of ADR. To work with ADR, review [Manage accelerated database recovery](accelerated-database-recovery-management.md).

For more information about transaction log and database recovery, see [SQL Server transaction log architecture and management guide](sql-server-transaction-log-architecture-and-management-guide.md) and [Restore and recovery overview (SQL Server)](backup-restore/restore-and-recovery-overview-sql-server.md).

## Overview

The primary benefits of ADR are:

- **Fast and consistent database recovery**

  Long-running transactions don't affect the overall recovery time, enabling fast and consistent database recovery irrespective of the number of active transactions in the system or their size.

- **Instantaneous transaction rollback**

  Transaction rollback is instantaneous, irrespective of the time that the transaction has been active or the number of updates that have been made.

- **Aggressive log truncation**

  The transaction log is aggressively truncated, even in the presence of active long-running transactions, which prevents it from growing out of control.

## The traditional database recovery process

Without ADR, database recovery follows the [ARIES](https://people.eecs.berkeley.edu/~brewer/cs262/Aries.pdf) recovery model and consists of three phases, which are illustrated and explained in more detail in the following diagram:

:::image type="content" source="media/accelerated-database-recovery-concepts/current-recovery-process.png" alt-text="Diagram of current recovery process." lightbox="media/accelerated-database-recovery-concepts/current-recovery-process.png":::

- **Analysis phase**

  The database engine performs a forward scan of the transaction log from the beginning of the last successful checkpoint (or the oldest dirty page log sequence number (LSN)) until the end, to determine the state of each transaction at the time the engine stopped.

- **Redo phase**

  The database engine performs a forward scan of the transaction log from the oldest uncommitted transaction to the end. This process redoes all committed operations to restore the database to its state at the time of the crash.

- **Undo phase**

  For each transaction that was active at the time of the crash, the database engine traverses the log backward, undoing the operations that this transaction performed.

- With the traditional database recovery process, recovery after a crash can take a long time if a long-running transaction was active.

  The time for the database engine to recover from an unexpected restart is (roughly) proportional to the size of the longest active transaction in the system at the time of the crash. Recovery requires a rollback of all incomplete transactions. The length of time required is proportional to the work that the transaction has performed and the time it has been active.

- Canceling, or rolling back, a large transaction can take a long time, because it uses the same undo recovery phase as described previously.

- The database engine can't truncate the transaction log when there are long-running transactions because their corresponding log records are needed for the recovery and rollback processes. As a result, the transaction log can grow very large and consume a lot of storage space.

## The accelerated database recovery process

ADR addresses previous issues with the traditional recovery model by completely redesigning the database engine recovery process to:

- Make the recovery time constant since there's no longer a need to scan the transaction log from the beginning of the oldest active transaction. With ADR, the transaction log is only processed from the last successful checkpoint (or oldest dirty page LSN). As a result, recovery time isn't affected by long-running transactions and is typically instantaneous.

- Minimize the required transaction log space since there's no longer a need to retain the log for the whole transaction. As a result, the transaction log can be truncated aggressively as checkpoints and backups occur.

At a high level, ADR achieves fast database recovery by versioning all physical database modifications and only undoing nonversioned operations, which are limited and can be undone almost instantly. Any transactions that were active at the time of a crash are marked as aborted and, therefore, any versions generated by these transactions can be ignored by concurrent user queries.

The ADR recovery process has the same three phases as the traditional recovery process. How these phases operate with ADR is illustrated in the following diagram:

:::image type="content" source="media/accelerated-database-recovery-concepts/adr-recovery-process.png" alt-text="Diagram of ADR recovery process." lightbox="media/accelerated-database-recovery-concepts/adr-recovery-process.png":::

- **Analysis phase**

  The process remains the same as the traditional recovery model with the addition of reconstructing the secondary log stream (SLOG) and copying log records for nonversioned operations.

- **Redo phase**

  Broken into two subphases

  - Subphase 1

    Redo from SLOG (oldest uncommitted transaction up to the last checkpoint). Redo is a fast operation as it might only need to process a few records from the SLOG.

  - Subphase 2

    Redo from transaction log starts from last successful checkpoint (instead of oldest uncommitted transaction).

- **Undo phase**

  The undo phase with ADR completes almost instantaneously by using SLOG to undo nonversioned operations and persistent version store (PVS) using logical revert to perform row level version-based undo.

For an explanation of accelerated database recovery, watch this eight-minute video:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Advanced-Database-Recovery--Data-Exposed/player?WT.mc_id=dataexposed-c9-niner]

## ADR recovery components

The four key components of ADR are:

- **Persistent version store (PVS)**

  The persistent version store (PVS) is a database engine mechanism for persisting row versions in the database itself instead of in the traditional version store in the `tempdb` database. PVS enables resource isolation and improves availability of readable secondaries.

  PVS stores row versions either directly on data pages being modified, or in a separate system table. For more information, see [Space used by the persistent version store (PVS)](sql-server-transaction-locking-and-row-versioning-guide.md#space-used-by-the-persistent-version-store-pvs).

- **Logical revert**

  Logical revert is the asynchronous process responsible for performing row-level version-based undo - providing instant transaction rollback and undo for all versioned operations.

  - Keeps track of all aborted transactions
  - Performs rollback using PVS for all user transactions
  - Releases all locks immediately after transaction abort

- **SLOG**

  The SLOG is a secondary in-memory log stream that stores log records for nonversioned operations (such as metadata cache invalidation, lock acquisitions, and so on). The SLOG is:

  - Low volume and in-memory
  - Persisted on disk during the checkpoint process
  - Periodically truncated as transactions commit
  - Accelerates redo and undo by processing only nonversioned operations
  - Enables aggressive transaction log truncation by preserving only the required log records

- **Cleaner**

  The cleaner is the asynchronous process that wakes up periodically and cleans obsolete row versions from PVS.

## Workloads that benefit from ADR

ADR benefits most workloads by improving database availability, and is particularly beneficial for workloads that have:

- Long-running transactions.
- Active transactions that cause the transaction log to grow significantly.
- Long periods of database unavailability due to long running recovery (such as from unexpected service restart or manual transaction rollback).

The benefits of ADR require version storage and extra processing, which might introduce a performance overhead for certain workloads. For example, with write-intensive workloads generating many row versions, data pages might be split more often to accommodate the [in-row](sql-server-transaction-locking-and-row-versioning-guide.md#space-used-by-the-persistent-version-store-pvs) versions. Because all row versions are logged, the amount of generated transaction log can increase as well.

For most workloads, the performance overhead of ADR ranges from not detectable to minor. In an optimal database management strategy, you balance the certain benefits of ADR against the potential performance overhead.

ADR isn't supported in databases using [database mirroring](../database-engine/database-mirroring/database-mirroring-sql-server.md), an older and deprecated high availability feature.

## Best practices for ADR

- Avoid unnecessary long-running transactions. Though ADR speeds up database recovery even with long-running transactions, such transactions can delay version cleanup and increase PVS size.

- Avoid large transactions that include DDL operations. ADR uses the secondary log stream (SLOG) mechanism to track DDL operations used in recovery. SLOG is only used while the transaction is active. SLOG is checkpointed, so avoiding large transactions that use SLOG can help the overall performance. These scenarios can cause the SLOG to take up more space:
  - Many DDLs are executed in one transaction. For example, in one transaction, rapidly creating and dropping temp tables.
  - A table has very large number of partitions/indexes that are modified. For example, a `DROP TABLE` operation on such table would require a large reservation of SLOG memory, which would delay truncation of the transaction log and delay undo/redo operations. As a workaround, drop the indexes individually and gradually, then drop the table.

  For more information about SLOG, see [ADR recovery components](#adr-recovery-components).

- Prevent or reduce unnecessary aborted transactions. A high transaction abort rate puts pressure on the PVS cleaner and lowers ADR performance. The aborts might come from a high rate of deadlocks, duplicate keys, constraint violations, query timeouts, or other exceptions.
  The [sys.dm_tran_aborted_transactions](system-dynamic-management-views/sys-dm-tran-aborted-transactions.md) DMV shows all aborted transactions on the database engine instance. The `nested_abort` column indicates that the transaction committed but within the transaction a rollback to a savepoint occurred. The presence of such rollbacks can also delay the PVS cleanup process.

- Ensure there's sufficient space in the database to account for PVS usage. If the database doesn't have enough room for PVS to grow, ADR might fail to generate versions, causing DML statements to fail.

- When ADR is enabled with write-intensive workloads, transaction log generation rate might increase substantially because row versions written to PVS are logged. This might increase the size of transaction log backups.

- When you use [transactional replication](replication/transactional/transactional-replication.md), [snapshot replication](replication/snapshot-replication.md), or [change data capture (CDC)](track-changes/about-change-data-capture-sql-server.md), the aggressive log truncation behavior of ADR is disabled to allow the log reader to collect changes from the transaction log. Make sure that the transaction log is sufficiently large.

  If using [CDC](/azure/azure-sql/database/change-data-capture-overview) or [change feed](/azure/synapse-analytics/synapse-link/sql-database-synapse-link) in Azure SQL Database, you might need to increase your service tier or compute size to ensure that sufficient transaction log space is available for the needs of all your workloads. Similarly, in Azure SQL Managed Instance you might need to increase your instance maximum storage size.

- For [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], when tiered performance storage is available, consider placing the PVS filegroup on higher tier storage. For more information, see [Change the PVS filegroup](accelerated-database-recovery-management.md#change-the-pvs-filegroup).

- Starting with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], consider enabling multi-threaded PVS cleanup if the single-threaded cleaner performance is insufficient. For more information, see [Server configuration: ADR Cleaner Thread Count](../database-engine/configure-windows/adr-cleaner-thread-count-configuration-option.md).

- Starting with [!INCLUDE [sssql25-md](../includes/sssql25-md.md)], if you enable ADR in `tempdb`, you might need to allocate additional space for PVS data in the `tempdb` data files. The size of PVS in `tempdb` can be [monitored](accelerated-database-recovery-troubleshoot.md#examine-the-size-of-the-pvs) in the same way as in a user database.

- If you observe issues such as high database space usage by PVS or slow PVS cleanup, see [Monitor and troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md).

## ADR improvements in SQL Server 2025

<a id="adr_tempdb"></a>

- **ADR in `tempdb`

  Starting with [!INCLUDE [sssql25-md](../includes/sssql25-md.md)], ADR can be enabled in the [tempdb database](databases/tempdb-database.md).

  Without ADR, and even with minimal logging used in `tempdb`, transactions that involve objects such as [temporary tables](../t-sql/statements/create-table-transact-sql.md#temporary-tables), [table variables](../t-sql/data-types/table-transact-sql.md), or non-temporary tables created in `tempdb` can be affected by long rollback and high transaction log usage. Running out of `tempdb` transaction log space can cause significant disruptions and application downtime.

  [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] makes the **Instantaneous transaction rollback** and **Aggressive log truncation** benefits of ADR available for workloads using transactions in `tempdb`.

  To enable ADR in `tempdb`, see [Enable ADR](accelerated-database-recovery-management.md#enable-adr).

## ADR improvements in SQL Server 2022

There are several improvements to address persistent version store (PVS) storage and improve overall scalability. For more information about new features of [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], see [What's new in SQL Server 2022](../sql-server/what-s-new-in-sql-server-2022.md).

The same improvements are also available in [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse_sqlpool_only](../includes/ssazuresynapse_sqlpool_only.md)], and [!INCLUDE [fabric-sqldb](../includes/fabric-sqldb.md)].

- **User transaction cleanup**

  Clean pages that can't be cleaned by the regular process due to lock acquisition failure.

  This feature allows user transactions to run cleanup on pages that couldn't be addressed by the regular cleanup process due to table level lock conflicts. This improvement helps ensure that the ADR cleanup process doesn't fail indefinitely because user workloads can't acquire table level locks.

- **Reduce memory footprint for PVS page tracker**

  This improvement tracks PVS pages at the extent level, in order to reduce the memory footprint needed to maintain versioned pages.

- **PVS cleaner improvements**

  PVS cleaner has improved version cleanup efficiency to improve how the database engine tracks and records row versions for aborted transactions. This leads to improvements in memory and storage usage.

- **Transaction-level persistent version store (PVS)**

  This improvement allows ADR to clean up versions that belong to committed transactions independent of whether there are aborted transactions in the system. With this improvement, PVS pages can be deallocated, even if the cleanup can't complete a successful sweep to trim the aborted transaction map.

  The result of this improvement is reduced PVS growth even if PVS cleanup is slow or fails.

- **Multi-threaded version cleanup**

  In [!INCLUDE [sssql19-md](../includes/sssql19-md.md)], the cleanup process is single threaded within a database engine instance.

  Beginning with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], multi-threaded version cleanup is supported. This allows multiple databases on the same database engine instance to be cleaned in parallel, or a single database to be cleaned faster. For more information, see [Server configuration: ADR Cleaner Thread Count](../database-engine/configure-windows/adr-cleaner-thread-count-configuration-option.md).

  A new extended event, `tx_mtvc2_sweep_stats`, has been added for monitoring of the ADR PVS multi-threaded version cleaner.

## Related content

- [Manage accelerated database recovery](accelerated-database-recovery-management.md)
- [Monitor and troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md)
