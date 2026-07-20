---
title: Guidelines for Online Index Operations
description: Guidelines for online index operations.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/22/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: best-practice
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "clustered indexes, online operations"
  - "online index operations"
  - "indexes [SQL Server], online operations"
  - "disk space [SQL Server], indexes"
  - "nonclustered indexes [SQL Server], online operations"
  - "transaction logs [SQL Server], indexes"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Guidelines for online index operations

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

When you perform online index operations, the following guidelines apply:

- Clustered indexes must be created, rebuilt, or dropped offline when the underlying table contains the following large object (LOB) data types: **image**, **ntext**, and **text**.

- Nonunique nonclustered indexes can be created online when the table has columns using the LOB data types but none of these columns are used in the index definition as either key or included columns.

- Indexes on local temp tables can't be created, rebuilt, or dropped online. This restriction doesn't apply to indexes on global temp tables.

- You can start an online index operation as a resumable operation by using the `RESUMABLE` clause of [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md). A resumable index operation can restart after an unexpected failure, database failover, or an `ALTER INDEX PAUSE` command and continue from where it was interrupted.

> [!NOTE]  
> Online index operations aren't available in every edition of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

The following table shows the index operations that can be performed online, the indexes that are excluded from these online operations, and resumable index restrictions. Additional restrictions are also included.

| Online index operation | Excluded indexes | Other restrictions |
| --- | --- | --- |
| `ALTER INDEX REBUILD` | Disabled clustered index or disabled indexed view<br /><br />XML index<br /><br />Index on a local temp table | Specifying the keyword `ALL` can cause the operation to fail when the table contains an excluded index.<br /><br />Additional restrictions on rebuilding disabled indexes apply. For more information, see [Disable indexes and constraints](disable-indexes-and-constraints.md). |
| `CREATE INDEX` | XML index<br /><br />Initial unique clustered index on a view<br /><br />Index on a local temp table | |
| `CREATE INDEX WITH DROP_EXISTING` | Disabled clustered index or disabled indexed view<br /><br />Index on a local temp table<br /><br />XML index | |
| `DROP INDEX` | Disabled index<br /><br />XML index<br /><br />Nonclustered index<br /><br />Index on a local temp table | Multiple indexes can't be specified within a single statement. |
| `ALTER TABLE ADD CONSTRAINT` (`PRIMARY KEY` or `UNIQUE`) | Index on a local temp table<br /><br />Clustered index | Only one subclause is allowed at a time. For example, you can't add and drop `PRIMARY KEY` or `UNIQUE` constraints in the same `ALTER TABLE` statement. |
| `ALTER TABLE DROP CONSTRAINT` (`PRIMARY KEY` or `UNIQUE`) | Clustered index | |

The underlying table can't be modified, truncated, or dropped while an online index operation is in progress.

The online option setting (`ON` or `OFF`) specified when you create or drop a clustered index is applied to any nonclustered indexes that must be rebuilt. For example, if the clustered index is built online by using `CREATE INDEX WITH DROP_EXISTING, ONLINE = ON`, all associated nonclustered indexes are also recreated online.

When you create or rebuild a `UNIQUE` index online, the index builder and a concurrent user transaction might try to insert the same key, therefore violating uniqueness. If a row entered by a user is inserted into the new index (target) before the original row from the source table is moved to the new index, the online index operation fails.

Although not common, the online index operation can cause a deadlock when it interacts with database updates because of user or application activities. In these rare cases, the user or application activity is selected as the deadlock victim.

You can perform concurrent online index DDL operations on the same table or view only when you're creating multiple new nonclustered indexes, or reorganizing nonclustered indexes. All other online index operations performed at the same time fail. For example, you can't create a new index online while rebuilding an existing index online on the same table.

An online operation can't be performed when an index contains a column of the large object type, and the same transaction makes data modifications before the online index operation starts. To work around this issue, move the online index operation outside of the transaction, or move it before any data modifications in the same transaction.

## Disk space considerations

Online index operations require more disk space than offline index operations.

- During index creation and index rebuild operations, additional space is required for the index being built (or rebuilt). Commonly, this additional space is the same as the current space occupied by the index, but it could be greater or smaller depending on the compression used in the current or the rebuilt index.

- In addition, disk space is required for the temporary mapping index. This temporary index is used in online index operations that create, rebuild, or drop a clustered index.

- Dropping a clustered index online requires as much space as creating (or rebuilding) a clustered index online.

For more information, see [Disk space requirements for index DDL operations](disk-space-requirements-for-index-ddl-operations.md).

## Performance considerations

Although online index operations permit concurrent user update activity, the index operations can take longer if the update activity is heavy. Typically, online index operations are slower than equivalent offline index operations, regardless of the concurrent update activity level.

Because both the source and target structures are maintained during the online index operation, the resource usage for insert, update, and delete transactions is increased, potentially doubled. This could cause a decrease in performance and greater resource usage, especially CPU time, during the index operation. Online index operations are fully logged.

Although we recommend online operations, you should evaluate your environment and specific requirements. It might be optimal to run index operations offline. In doing so, user access to the data is restricted during the operation, but the operation finishes faster and uses fewer resources.

On multiprocessor computers that are running [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, index operations can use parallelism to perform the scan and sort operations associated with the index statement. You can use the `MAXDOP` index option to control the degree of parallelism of the online index operation. In this way, you can balance the resources that are used by index operation with resources of the concurrent users. For more information, see [Configure parallel index operations](configure-parallel-index-operations.md). For more information about the editions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that support parallel index operations, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

Because a shared (`S`) lock or a schema modification (`Sch-M`) lock is held in the final phase of the index operation, be careful when you run an online index operation inside an explicit user transaction, such as `BEGIN TRANSACTION ... COMMIT` block. Doing this causes the locks to be held until the end of the transaction, potentially blocking other workloads.

If index page locks are disabled using `ALLOW_PAGE_LOCKS = OFF`, online index rebuild can increase index fragmentation when it runs with `MAXDOP` greater than 1. For more information, see [How It Works: Online Index Rebuild - Can Cause Increased Fragmentation](https://techcommunity.microsoft.com/blog/sqlserversupport/how-it-works-online-index-rebuild---can-cause-increased-fragmentation/317246).

## Transaction log considerations

Large-scale index operations performed offline or online can generate large amounts of transaction log. This is because both offline and online index rebuild operations are fully logged. To make sure that the index operation can be rolled back, the transaction log can't be truncated until the index operation is completed; however, the log can be backed up during the index operation.

Therefore, the transaction log must have sufficient space to store both the index operation transactions and any concurrent user transactions during the index operation. For more information, see [Transaction log disk space for index operations](transaction-log-disk-space-for-index-operations.md).

Online index operations don't cause high transaction log growth if [accelerated database recovery (ADR)](../accelerated-database-recovery-concepts.md) is enabled.

## Persistent version store considerations

If ADR is enabled, creating, or rebuilding a large index online can substantially increase the size of persistent version store (PVS) while the index operation is in progress. Ensure that the database has sufficient free space for PVS to grow. For more information, see [Monitor and troubleshoot accelerated database recovery](../accelerated-database-recovery-troubleshoot.md).

## Resumable index considerations

The `RESUMABLE` index option for `CREATE INDEX` and `ALTER INDEX` applies to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] (`ALTER INDEX` starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and `CREATE INDEX` starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)]. For more information, see [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) and [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md).

To use the `RESUMABLE` option, you must also use the `ONLINE` option. When you perform resumable index create or rebuild, the following guidelines apply:

- You have better control over managing, planning, and extending index maintenance windows. You can pause and restart an index create or rebuild operation multiple times to fit your maintenance windows.

- You can recover from index create or rebuild failures (such as database failovers or running out of disk space) without having to restart the index operation from the beginning.

- When an index operation is paused, both the original index and the newly created one require disk space and need to be updated during DML operations.

- The `SORT_IN_TEMPDB = ON` option isn't supported.

- Disabled indexes aren't supported.

> [!TIP]  
> Resumable index operations don't require a large transaction, allowing frequent log truncation during this operation and avoiding large log growth. The data required to resume and complete an index operation is stored in the data files of a database.

Generally, there's no performance difference between resumable and nonresumable online index operations. For resumable `CREATE INDEX`, there's a constant overhead that might cause noticeably slower operations for smaller tables.

When a resumable index operation is paused:

- For mostly read workloads, the performance degradation is insignificant.
- For update-heavy workloads, you might experience some throughput degradation depending on the workload specifics.

Generally, there's no difference in defragmentation quality between resumable and nonresumable online index create or rebuild.

While an online index operation is paused, any transaction that requires a table-level exclusive (`X`) lock on the table that contains the paused index fails. For example, this might occur with `INSERT ... WITH (TABLOCK)` operations. In this case, you get error 10637:

```output
Cannot perform this operation on '<object name>' with ID (<object ID>) as one or more indexes are currently in resumable index rebuild state. Please refer to sys.index_resumable_operations for more details.
```

To resolve error 10637, remove the `TABLOCK` hint from your transaction, or unpause the index operation and wait for it to complete before attempting your transaction again.

## Online default options

You can set online and resumable index operations as the default options at the database level by setting the `ELEVATE_ONLINE` or `ELEVATE_RESUMABLE` database-scoped configurations. With these default options, you can avoid accidentally starting an offline index operation that makes a table or index inaccessible while it's running. Both options cause the database engine to automatically elevate certain index operations to online or resumable execution.

You can set either option as `FAIL_UNSUPPORTED`, `WHEN_SUPPORTED`, or `OFF`. You can set different values for `ELEVATE_ONLINE` and `ELEVATE_RESUMABLE`. For more information, see [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

Both `ELEVATE_ONLINE` and `ELEVATE_RESUMABLE` only apply to DDL statements that support the online and resumable syntax respectively. For example, if you attempt to create an XML index with `ELEVATE_ONLINE = FAIL_UNSUPPORTED`, the operation runs offline since XML indexes don't support the `ONLINE` option. The options only affect DDL statements that are submitted without specifying an `ONLINE` or `RESUMABLE` option. For example, by submitting a statement with `ONLINE = OFF` or `RESUMABLE = OFF`, the user can override a `FAIL_UNSUPPORTED` setting and run a statement offline and/or nonresumably.

> [!NOTE]  
> `ELEVATE_ONLINE` and `ELEVATE_RESUMABLE` don't apply to XML index operations.

## Related content

- [How online index operations work](how-online-index-operations-work.md)
- [Perform index operations online](perform-index-operations-online.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
