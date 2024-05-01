---
title: "Accelerated database recovery"
description: "Accelerated database recovery"
author: mashamsft
ms.author: mathoma
ms.reviewer: wiassaf, derekw, randolphwest
ms.date: 03/06/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# Accelerated database recovery

[!INCLUDE [SQL Server 2019, ASDB, ASDBMI, ASDW](../includes/applies-to-version/sqlserver2019-asdb-asdbmi-asa.md)]

Accelerated database recovery (ADR) improves database availability, especially in the presence of long running transactions, by redesigning the SQL database engine recovery process. 

ADR was introduced in [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] and improved in [!INCLUDE[sssql22-md](../includes/sssql22-md.md)]. ADR is also available for databases in Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse SQL. ADR is enabled by default in SQL Database and SQL Managed Instance and can't be disabled. For more about ADR in Azure SQL, see [Accelerated Database Recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery).

This article provides an overview of the ADR feature. To work with ADR, review [Manage accelerated database recovery](accelerated-database-recovery-management.md). 


## Overview

The primary benefits of ADR are:

- **Fast and consistent database recovery**

  With ADR, long running transactions don't impact the overall recovery time, enabling fast and consistent database recovery irrespective of the number of active transactions in the system or their sizes.

- **Instantaneous transaction rollback**

  With ADR, transaction rollback is instantaneous, irrespective of the time that the transaction has been active or the number of updates that has performed.

- **Aggressive log truncation**

  With ADR, the transaction log is aggressively truncated, even in the presence of active long running transactions, which prevents it from growing out of control.

## The current database recovery process

Without ADR, database recovery in SQL Server follows the [ARIES](https://people.eecs.berkeley.edu/~brewer/cs262/Aries.pdf) recovery model and consists of three phases, which are illustrated in the following diagram and explained in more detail following the diagram.

:::image type="content" source="media/accelerated-database-recovery-concepts/current-recovery-process.png" alt-text="Diagram of current recovery process." lightbox="media/accelerated-database-recovery-concepts/current-recovery-process.png":::

- **Analysis phase**

  SQL Server conducts a forward scan of the transaction log from the beginning of the last successful checkpoint (or the oldest dirty page LSN) until the end, to determine the state of each transaction at the time SQL Server stopped.

- **Redo phase**

  SQL Server performs forward scan of the transaction log from the oldest uncommitted transaction until the end, to bring the database to the state it was at the time of the crash by redoing all committed operations.

- **Undo phase**

  For each transaction that was active as of the time of the crash, SQL Server traverses the log backward, undoing the operations that this transaction performed.

Based on this design, the time it takes the database engine to recover from an unexpected restart is (roughly) proportional to the size of the longest active transaction in the system at the time of the crash. Recovery requires a rollback of all incomplete transactions. The length of time required is proportional to the work that the transaction has performed and the time it has been active. Therefore, the SQL Server recovery process can take a long time in the presence of long running transactions (such as large bulk insert operations or index build operations against a large table).

Also, canceling, or rolling back, a large transaction based on this design can also take a long time, because it uses the same undo recovery phase as described previously.

In addition, the database engine can't truncate the transaction log when there are long running transactions because their corresponding log records are needed for the recovery and rollback processes. As a result, some transaction logs grow very large and consume huge amounts of drive space.

## The accelerated database recovery process

ADR addresses the previous issues by completely redesigning the database engine recovery process to:

- Make it constant time/instant by avoiding having to scan the log from/to the beginning of the oldest active transaction. With ADR, the transaction log is only processed from the last successful checkpoint (or oldest dirty page log sequence number (LSN)). As a result, recovery time isn't affected by long running transactions.
- Minimize the required transaction log space since there's no longer a need to process the log for the whole transaction. As a result, the transaction log can be truncated aggressively as checkpoints and backups occur.

At a high level, ADR achieves fast database recovery by versioning all physical database modifications and only undoing logical operations, which are limited and can be undone almost instantly. Any transactions that were active at the time of a crash are marked as aborted and, therefore, any versions generated by these transactions can be ignored by concurrent user queries.

The ADR recovery process has the same three phases as the current recovery process. How these phases operate with ADR is illustrated in the following diagram.

:::image type="content" source="media/accelerated-database-recovery-concepts/adr-recovery-process.png" alt-text="Diagram of ADR recovery process." lightbox="media/accelerated-database-recovery-concepts/adr-recovery-process.png":::

- **Analysis phase**

  The process remains the same as today with the addition of reconstructing the SLOG (system log stream) and copying log records for nonversioned operations.
  
- **Redo** phase

  Broken into two subphases
  - Subphase 1

      Redo from SLOG (oldest uncommitted transaction up to last checkpoint). Redo is a fast operation as it only needs to process a few records from the SLOG.

  - Sub phase 2

     Redo from transaction log starts from last checkpoint (instead of oldest uncommitted transaction).
     
- **Undo phase**

   The undo phase with ADR completes almost instantaneously by using SLOG to undo nonversioned operations and persisted version store (PVS) with logical revert to perform row level version-based undo.

You can also watch this eight-minute video that explains Accelerated Database Recovery:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Advanced-Database-Recovery--Data-Exposed/player?WT.mc_id=dataexposed-c9-niner]

## ADR recovery components

The four key components of ADR are:

- **Persisted version store (PVS)**

  The persisted version store is a database engine mechanism for persisting the row versions generated in the database itself instead of the traditional `tempdb` version store. PVS enables resource isolation and improves availability of readable secondaries. There's one PVS thread per instance in [!INCLUDE[sssql19-md](../includes/sssql19-md.md)]. Starting with [!INCLUDE[sssql22-md](../includes/sssql22-md.md)], SQL Server has one PVS cleaner thread per database.

- **Logical Revert**

  Logical revert is the asynchronous process responsible for performing row-level version-based undo - providing instant transaction rollback and undo for all versioned operations.

  - Keeps track of all aborted transactions
  - Performs rollback using PVS for all user transactions
  - Releases all locks immediately after transaction abort

- **SLOG**

  The SLOG is a secondary in-memory log stream that stores log records for nonversioned operations (such as metadata cache invalidation, lock acquisitions, and so on). The SLOG is:

  - Low volume and in-memory
  - Persisted on disk by being serialized during the checkpoint process
  - Periodically truncated as transactions commit
  - Accelerates redo and undo by processing only the nonversioned operations  
  - Enables aggressive transaction log truncation by preserving only the required log records

- **Cleaner**

  The cleaner is the asynchronous process that wakes up periodically and cleans obsolete row versions from PVS.

## ADR improvements in [!INCLUDE[sssql22-md](../includes/sssql22-md.md)]

There are several improvements to address persistent version store (PVS) storage and improve overall scalability. For more information about new features of [!INCLUDE[sssql22-md](../includes/sssql22-md.md)], see [What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]](../sql-server/what-s-new-in-sql-server-2022.md).

- **User transaction cleanup** 

  Clear pages that couldn't be cleaned by the regular process due to lock failure. 

  This feature allows user transactions to run cleanup on pages that couldn't be addressed by the regular cleanup process due to table level lock conflicts. This improvement helps ensure that the ADR cleanup process doesn't fail indefinitely because user workloads can't acquire table level locks.

- **Reducing memory footprint for PVS page tracker**
  
  This improvement tracks persisted version store (PVS) pages at the extent level, in order to reduce the memory footprint needed to maintain versioned pages.
 
- **Accelerated Data Recovery (ADR) Cleaner improvements**
  
  Accelerated Data Recovery (ADR) cleaner has improved version cleanup efficiencies to improve how SQL Server tracks and records aborted versions of a page leading to improvements in memory and capacity.
 
- **Transaction-level persisted version store (PVS)** 
  
  This improvement allows ADR to clean up versions belonging to committed transactions independent of whether there are aborted transactions in the system. With this improvement persisted version store (PVS) pages can be deallocated, even if the cleanup can't complete a successful sweep in order to trim the aborted transaction map. 
  
  The result of this improvement is reduced persisted version store (PVS) growth even if ADR cleanup is slow or fails.

- **Multi-threaded version cleanup**  
  
  In [!INCLUDE[sssql19-md](../includes/sssql19-md.md)], the cleanup process is single threaded within a SQL Server instance.
  
  Beginning with [!INCLUDE[sssql22-md](../includes/sssql22-md.md)], this process uses multi-threaded version cleanup. This allows multiple databases in the same SQL Server instance to be cleaned in parallel. This improvement is valuable when you have multiple large databases.

  To adjust the number of threads for version cleanup scalability, set `ADR Cleaner Thread Count` with `sp_configure`. The thread count is capped at the number of cores for the instance.

  As a best practice, we recommend using the same number of ADR cleaner threads as the number of your databases. For instance, if you configure the `ADR Cleaner Thread Count` to be `4` on a SQL Server instance with two databases, the ADR cleaner will still only allocate one thread per database, leaving the remaining two threads idle.

  The example below changes the thread count to `4`:

  ```sql
  EXEC sp_configure 'ADR Cleaner Thread Count', '4';
  RECONFIGURE WITH OVERRIDE; 
  ```

- **New extended event**

  A new extended event, `tx_mtvc2_sweep_stats`, has been added for telemetry on the ADR PVS multi-threaded version cleaner.

## Best practices and guidance

For guidance on workloads that are and aren't recommended for ADR, see [Manage accelerated database recovery](accelerated-database-recovery-management.md).

> [!IMPORTANT]  
> In Azure SQL Database, idle transactions (transactions that haven't written to the transaction log for six hours) are automatically terminated, to free up resources.

## Related content

- [Manage accelerated database recovery](accelerated-database-recovery-management.md)
- [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md)
- [Accelerated Database Recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery)
