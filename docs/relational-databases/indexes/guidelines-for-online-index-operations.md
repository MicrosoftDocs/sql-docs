---
title: Guidelines for online index operations
description: Guidelines for online index operations.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 11/30/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "clustered indexes, online operations"
  - "online index operations"
  - "indexes [SQL Server], online operations"
  - "disk space [SQL Server], indexes"
  - "nonclustered indexes [SQL Server], online operations"
  - "transaction logs [SQL Server], indexes"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Guidelines for online index operations

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Database MI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When you perform online index operations, the following guidelines apply:

- Clustered indexes must be created, rebuilt, or dropped offline when the underlying table contains the following large object (LOB) data types: **image**, **ntext**, and **text**.
- Nonunique nonclustered indexes can be created online when the table contains LOB data types but none of these columns are used in the index definition as either key or nonkey (included) columns.
- Indexes on local temp tables can't be created, rebuilt, or dropped online. This restriction doesn't apply to indexes on global temp tables.
- Indexes can be resumed from where it stopped after an unexpected failure, database failover, or a `PAUSE` command. See [Create Index](../../t-sql/statements/create-index-transact-sql.md) and [Alter Index](../../t-sql/statements/alter-index-transact-sql.md).

> [!NOTE]  
> Online index operations aren't available in every edition of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

The following table shows the index operations that can be performed online, the indexes that are excluded from these online operations, and resumable index restrictions. Additional restrictions are also included.

| Online index operation | Excluded indexes | Other restrictions |
| --- | --- | --- |
| `ALTER INDEX REBUILD` | Disabled clustered index or disabled indexed view<br /><br />XML index<br /><br />Columnstore index<br /><br />Index on a local temp table | Specifying the keyword `ALL` can cause the operation to fail when the table contains an excluded index.<br /><br />Additional restrictions on rebuilding disabled indexes apply. For more information, see [Disable Indexes and Constraints](disable-indexes-and-constraints.md). |
| `CREATE INDEX` | XML index<br /><br />Initial unique clustered index on a view<br /><br />Index on a local temp table | |
| `CREATE INDEX WITH DROP_EXISTING` | Disabled clustered index or disabled indexed view<br /><br />Index on a local temp table<br /><br />XML index | |
| `DROP INDEX` | Disabled index<br /><br />XML index<br /><br />Nonclustered index<br /><br />Index on a local temp table | Multiple indexes can't be specified within a single statement. |
| `ALTER TABLE ADD CONSTRAINT` (`PRIMARY KEY` or `UNIQUE`) | Index on a local temp table<br /><br />Clustered index | Only one subclause is allowed at a time. For example, you can't add and drop `PRIMARY KEY` or `UNIQUE` constraints in the same `ALTER TABLE` statement. |
| `ALTER TABLE DROP CONSTRAINT` (`PRIMARY KEY` or `UNIQUE`) | Clustered index | |

The underlying table can't be modified, truncated, or dropped while an online index operation is in process.

The online option setting (`ON` or `OFF`) specified when you create or drop a clustered index is applied to any nonclustered indexes that must be rebuilt. For example, if the clustered index is built online by using `CREATE INDEX WITH DROP_EXISTING, ONLINE=ON`, all associated nonclustered indexes are recreated online also.

When you create or rebuild a `UNIQUE` index online, the index builder and a concurrent user transaction might try to insert the same key, therefore violating uniqueness. If a row entered by a user is inserted into the new index (target) before the original row from the source table is moved to the new index, the online index operation fails.

Although not common, the online index operation can cause a deadlock when it interacts with database updates because of user or application activities. In these rare cases, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] selects the user or application activity as a deadlock victim.

You can perform concurrent online index DDL operations on the same table or view only when you're creating multiple new nonclustered indexes, or reorganizing nonclustered indexes. All other online index operations performed at the same time fail. For example, you can't create a new index online while rebuilding an existing index online on the same table.

An online operation can't be performed when an index contains a column of the large object type, and in the same transaction there are update operations before this online operation. To work around this issue, place the online operation outside the transaction or place it before any updates in the transaction.

## Disk space considerations

Online index operations require more disk space requirements than offline index operations.

- During index creation and index rebuild operations, additional space is required for the index being built (or rebuilt).
- In addition, disk space is required for the temporary mapping index. This temporary index is used in online index operations that create, rebuild, or drop a clustered index.
- Dropping a clustered index online requires as much space as creating (or rebuilding) a clustered index online.

For more information, see [Disk Space Requirements for Index DDL Operations](disk-space-requirements-for-index-ddl-operations.md).

## Performance considerations

Although online index operations permit concurrent user update activity, the index operations can take longer if the update activity is very heavy. Typically, online index operations are slower than equivalent offline index operations, regardless of the concurrent update activity level.

Because both the source and target structures are maintained during the online index operation, the resource usage for insert, update, and delete transactions is increased, potentially up to double. This could cause a decrease in performance and greater resource usage, especially CPU time, during the index operation. Online index operations are fully logged.

Although we recommend online operations, you should evaluate your environment and specific requirements. It might be optimal to run index operations offline. In doing so, user access to the data is restricted during the operation, but the operation finishes faster and uses fewer resources.

On multiprocessor computers that are running [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], index statements can use more processors to perform the scan and sort operations associated with the index statement just like other queries do. You can use the `MAXDOP` index option to control the number of processors dedicated to the online index operation. In this way, you can balance the resources that are used by index operation with resources of the concurrent users. For more information, see [Configure Parallel Index Operations](configure-parallel-index-operations.md). For more information about the editions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that support parallel index operations, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

Because an S-lock or Sch-M lock is held in the final phase of the index operation, be careful when you run an online index operation inside an explicit user transaction, such as `BEGIN TRANSACTION ... COMMIT` block. Doing this causes the lock to be held until the end of the transaction, therefore impeding user concurrency.

Online index rebuilding can increase fragmentation when it's runs with `MAXDOP` greater than `1`, and `ALLOW_PAGE_LOCKS=OFF`. For more information, see [How It Works: Online Index Rebuild - Can Cause Increased Fragmentation](/archive/blogs/psssql/how-it-works-online-index-rebuild-can-cause-increased-fragmentation).

## Transaction log considerations

Large-scale index operations performed offline or online can generate large data loads, which cause the transaction log to quickly fill. This is because both offline and online index rebuild operations are fully logged. To make sure that the index operation can be rolled back, the transaction log can't be truncated until the index operation is completed; however, the log can be backed up during the index operation.

Therefore, the transaction log must have sufficient space to store both the index operation transactions and any concurrent user transactions during the index operation. For more information, see [Transaction Log Disk Space for Index Operations](transaction-log-disk-space-for-index-operations.md).

## Resumable index considerations

The resumable index option for create index and index rebuild applies to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] (index rebuild starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and create index supported in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]), and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. For more information see [Create Index](../../t-sql/statements/create-index-transact-sql.md) and [Alter Index](../../t-sql/statements/alter-index-transact-sql.md).

When you perform resumable online index create or rebuild, the following guidelines apply:

- Managing, planning and extending of index maintenance windows. You can pause and restart an index create or rebuild operation multiple times to fit your maintenance windows.
- Recovering from index create or rebuild failures (such as database failovers or running out of disk space).
- When an index operation is paused, both the original index and the newly created one require disk space and need to be updated during DML operations.
- Enables truncation of transaction logs during an index create or rebuild operation.
- `SORT_IN_TEMPDB=ON` option isn't supported.
- Disabled indexes aren't supported.

> [!IMPORTANT]  
> Resumable index create or rebuild doesn't require you to keep open a long running transaction, allowing log truncation during this operation and a better log space management. With the new design, we managed to keep necessary data in a database together with all references required to restart the resumable operation.

Generally, there's no performance difference between resumable and nonresumable online index rebuild. For create resumable index, there's a constant overhead that causes a small performance difference between resumable and nonresumable index create. This difference is mostly noticeable only for smaller tables.

When you update a resumable index while an index operation is paused:

- For read-mostly workloads, the performance effect is insignificant.
- For update-heavy workloads, you can experience some throughput degradation (our testing shows less than 10% degradation).

Generally, there's no difference in defragmentation quality between resumable and nonresumable online index create or rebuild.

> [!NOTE]  
> While an online index operation is paused, any operation that requires a table-level exclusive lock on the table that contains the paused index will fail. This is most often encountered with `INSERT ... WITH (TABLOCK)` operations. You might see the following error:
>  
> `Msg 10637, Level 16, State 1, Line 32: Cannot perform this operation on 'object' with ID (objectid) as one or more indexes are currently in resumable index rebuild state. Please refer to sys.index_resumable_operations for more details.`
>  
> To resolve error 10637, remove the `TABLOCK` hint from your transaction, or unpause the index operation and wait for it to complete before attempting your transaction again.

## Online default options

You can set default options for online or resumable at a database level by setting the `ELEVATE_ONLINE` or `ELEVATE_RESUMABLE` database scoped configuration options. With these default options, you can avoid accidentally performing an operation that takes your database table offline. Both options cause the engine to automatically elevate certain operations to online or resumable execution.  
You can set either option as `FAIL_UNSUPPORTED`, `WHEN_SUPPORTED`, or `OFF` using the [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) command. You can set different values for online and resumable.

Both `ELEVATE_ONLINE` and `ELEVATE_RESUMABLE` only apply to DDL statements that support the online and resumable syntax respectively. For example, if you attempt to create an XML index with `ELEVATE_ONLINE=FAIL_UNSUPORTED`, the operation will run offline since XML indexes don't support the `ONLINE=` syntax. The options only affect DDL statements that are submitted without specifying an ONLINE or RESUMABLE option. For example, by submitting a statement with `ONLINE=OFF` or `RESUMABLE=OFF`, the user can override a `FAIL_UNSUPPORTED` setting and run a statement offline and/or nonresumably.

> [!NOTE]  
> `ELEVATE_ONLINE` and `ELEVATE_RESUMABLE` don't apply to XML index operations.

## Related content

- [How Online Index Operations Work](how-online-index-operations-work.md)
- [Perform Index Operations Online](perform-index-operations-online.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
