---
title: "Optimized locking"
description: "Learn about the optimized locking enhancement to the Database Engine."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, peskount, praspu
ms.date: 02/22/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
ms.custom:
  - references_regions
helpviewer_keywords:
  - "optimized locking"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---

# Optimized locking

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

This article introduces the optimized locking feature, a new [!INCLUDE [SQL Server Database Engine](../../includes/ssdenoversion-md.md)] capability that offers an improved transaction locking mechanism that reduces lock memory consumption and blocking for concurrent transactions.

## What is optimized locking?

Optimized locking helps to reduce lock memory as very few locks are held for large transactions. In addition, optimized locking also avoids lock escalations. This allows more concurrent access to the table.

Optimized locking is composed of two primary components: **Transaction ID (TID) locking** and **lock after qualification (LAQ)**.

- A transaction ID (TID) is a unique identifier of a transaction. Each row is labeled with the last TID that modified it. Instead of potentially many key or row identifier locks, a single lock on the TID is used. For more information, review the section on [Transaction ID (TID) locking](#optimized-locking-and-transaction-id-tid-locking).
- Lock after qualification (LAQ) is an optimization that evaluates predicates of a query on the latest committed version of the row without acquiring a lock, thus improving concurrency. For more information, review the section on [Lock after qualification (LAQ)](#optimized-locking-and-lock-after-qualification-laq).

For example:

- Without optimized locking, updating 1 million rows in a table might require 1 million exclusive (X) row locks held until the end of the transaction.
- With optimized locking, updating 1 million rows in a table might require 1 million X row locks but each lock is released as soon as each row is updated, and only one TID lock will be held until the end of the transaction.

This article covers these two core concepts of optimized locking in detail.

### Availability

Currently, optimized locking is available in [!INCLUDE [Azure SQL Database](../../includes/ssazure-sqldb.md)] only. For more information, see [Where is optimized locking currently available?](#where-is-optimized-locking-currently-available)

#### Is optimized locking enabled?

Optimized locking is enabled per user database. Connect to your database, then use the following query to check if optimized locking is enabled on your database:

```sql
SELECT IsOptimizedLockingOn = DATABASEPROPERTYEX('testdb', 'IsOptimizedLockingOn');
```

If you are not connected to the database specified in `DATABASEPROPERTYEX`, the result will be `NULL`. You should receive `0` (optimized locking is disabled) or `1` (enabled).

Optimized locking builds on other database features:

- Optimized locking requires [accelerated database recovery (ADR)](/azure/azure-sql/accelerated-database-recovery) to be enabled on the database.
- For the most benefit from optimized locking, [read committed snapshot isolation (RCSI)](../../t-sql/statements/alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true#read_committed_snapshot--on--off--1) should be enabled for the database.

Both ADR and RCSI are enabled by default in [!INCLUDE [asdb](../../includes/ssazure-sqldb.md)]. To verify that these options are enabled for your current database, use the following T-SQL query:

```sql
SELECT name
, is_read_committed_snapshot_on
, is_accelerated_database_recovery_on
FROM  sys.databases
WHERE name = db_name();
```

### Locking overview

This is a short summary of the behavior when optimized locking is not enabled. For more information, review the [Transaction locking and row versioning guide](../sql-server-transaction-locking-and-row-versioning-guide.md).

In the Database Engine, locking is a mechanism that prevents multiple transactions from updating the same data simultaneously, in order to protect data integrity and consistency.

When a transaction needs to modify data, it can request a lock on the data. The lock is granted if no other conflicting locks are held on the data, and the transaction can proceed with the modification. If another conflicting lock is held on the data, the transaction must wait for the lock to be released before it can proceed.

When multiple transactions are allowed to access the same data concurrently, the Database Engine must resolve potentially complex conflicts with concurrent reads and writes. Locking is one of the mechanisms by which the database engine can provide the semantics for the ANSI SQL transaction isolation levels. Although locking in databases is essential, reduced concurrency, deadlocks, complexity, and lock overhead can impact performance and scalability.

### Optimized locking and transaction ID (TID) locking

Every row in the Database Engine internally contains a transaction ID (TID) when row versioning is in use. This TID is persisted on disk. Every transaction modifying a row stamps that row with its TID.

With TID locking, instead of taking the lock on the key of the row, a lock is taken on the TID of the row. The modifying transaction holds an X lock on its TID. Other transactions acquire an S lock on the TID to check if the first transaction is still active. With TID locking, page and row locks continue to be taken for updates, but each page and row lock is released as soon as each row is updated. The only lock held until end of transaction is the X lock on the TID resource, replacing page and row (key) locks as demonstrated in the next demo. (Other standard database and object locks are not affected by optimized locking.)

Optimized locking helps to reduce lock memory as very few locks are held for large transactions. In addition, optimized locking also avoids lock escalations. This allows other concurrent transactions to access the table.

Consider the following T-SQL sample scenario that looks for locks on the user's current session:

```sql
CREATE TABLE t0
(a int PRIMARY KEY not null
,b int null);

INSERT INTO t0 VALUES (1,10),(2,20),(3,30);
GO

BEGIN TRAN
UPDATE t0
SET b=b+10;

SELECT * FROM sys.dm_tran_locks WHERE request_session_id = @@SPID
AND resource_type in ('PAGE','RID','KEY','XACT');

COMMIT TRAN
GO
DROP TABLE IF EXISTS t0;
```

:::image type="content" source="media/optimized-locking/sys-dm-tran-locks-with-optimized-locking.png" alt-text="A screenshot of the result set of a query on sys.dm_tran_locks for a single session shows only one lock when optimized locking is enabled." lightbox="media/optimized-locking/sys-dm-tran-locks-with-optimized-locking.png":::

The same query without the benefit of optimized locking creates four locks:

:::image type="content" source="media/optimized-locking/sys-dm-tran-locks-without-optimized-locking.png" alt-text="A screenshot of the result set of a query on sys.dm_tran_locks for a single session shows three locks when optimized locking is not enabled." lightbox="media/optimized-locking/sys-dm-tran-locks-without-optimized-locking.png":::

The [sys.dm_tran_locks](../system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md) dynamic management view (DMV) can be useful in examining or troubleshooting locking issues, including observing optimized locking in action.

### Optimized locking and lock after qualification (LAQ)

Building on the TID infrastructure, optimized locking changes how query predicates secure locks.

Without optimized locking, predicates from queries are checked row by row in a scan by first taking an update (U) row lock. If the predicate is satisfied, an X row lock is taken before updating the row.

With optimized locking, and when the read committed snapshot isolation level (RCSI) is enabled, predicates are applied on latest committed version without taking any row locks. If the predicate does not satisfy, the query moves to the next row in the scan. If the predicate is satisfied, an X row lock is taken to actually update the row. The X row lock is released as soon as the row update is complete, before the end of the transaction.

Since predicate evaluation is performed without acquiring any locks, concurrent queries modifying different rows will not block each other.

Example:

```sql
CREATE TABLE t1
(a int not null
,b int null);

INSERT INTO t1 VALUES (1,10),(2,20),(3,30);
GO
```

| **Session 1** | **Session 2** |
| :-- | :-- |
| `BEGIN TRAN`<br />`UPDATE t1`<br />`SET b=b+10`<br />`WHERE a=1;` | |
| | `BEGIN TRAN`<br />`UPDATE t1`<br />`SET b=b+10`<br />`WHERE a=2;` |
| `COMMIT TRAN` | |
| | `COMMIT TRAN` |

The behavior of blocking changes with optimized locking in the previous example. Without optimized locking, Session 2 will be blocked.

However, with optimized locking, Session 2 will not be blocked as the latest committed version of row 1 contains a=1, which does not satisfy the predicate of Session 2.

If the predicate is satisfied, we wait for any active transaction on the row to finish. If we had to wait for the S TID lock, the row might have changed, and the latest committed version might have changed. In that case, instead of aborting the transaction due to an update conflict, the Database Engine retries the predicate evaluation on the same row. If the predicate qualifies upon retry, the row will be updated.

Consider the following example when a predicate change is automatically retried:

```sql
CREATE TABLE t2
(a int not null
,b int null);

INSERT INTO t2 VALUES (1,10),(2,20),(3,30);
GO
```

| **Session 1** | **Session 2** |
| :-- | :-- |
| `BEGIN TRAN`<br />`UPDATE t2`<br />`SET b=b+10`<br />`WHERE a=1;` | |
| | `BEGIN TRAN`<br />`UPDATE t2`<br />`SET b=b+10`<br />`WHERE a=1;` |
| `COMMIT TRAN` | |
| | `COMMIT TRAN` |

### <a id="behavior"></a> Query behavior changes with optimized locking and RCSI

Concurrent systems under read committed snapshot isolation level (RCSI) with workloads that rely on strict execution order of transactions, might experience different query behavior when optimized locking is enabled.

Consider the following example where transaction T2 is updating table `t1` based on column `b` that was updated during transaction T1.

```sql
CREATE TABLE t1 (a int not null, b int null);

INSERT INTO t1 VALUES (1,1);
GO
```

| **Session 1** | **Session 2** |
| :-- | :-- |
| BEGIN TRAN T1<br />UPDATE t1<br />SET b=2<br />WHERE a=1; | |
| | BEGIN TRAN T2<br />UPDATE t1<br />SET b=3<br />WHERE b=2; |
| COMMIT TRAN | |
| | COMMIT TRAN |

Let's evaluate the outcome of the above scenario with and without lock after qualification (LAQ), an integral part of optimized locking.

**Without LAQ**

Without LAQ, transaction T2 will be blocked and wait for the transaction T1 to complete.

After both transactions commit, table `t1` will contain the following rows:

```output
 a | b
 1 | 3
```

**With LAQ**

With LAQ, transaction T2 will use the latest committed version of the row b (`b`=1 in the version store) to evaluate its predicate (`b`=2). This row does not qualify; hence it is skipped and T2 moves to the next row without having been blocked by transaction T1. In this example, LAQ removes blocking but leads to different results.

After both transactions commit, table `t1` will contain the following rows:

```output
 a | b
 1 | 2
```

> [!IMPORTANT]  
> Even without LAQ, applications should not assume that SQL Server (under versioning isolation levels) will guarantee strict ordering, without using locking hints. Our general recommendation for customers on concurrent systems under RCSI with workloads that rely on strict execution order of transactions (as shown in the previous exercise), is to [use stricter isolation levels](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).

## Diagnostic additions for optimized locking

To support monitoring and troubleshooting of blocking and deadlocking with optimized locking, look for the following additions:

- Wait types for optimized locking
    - `XACT` wait types and resource descriptions in [sys.dm_os_wait_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md#lck_m_s_xact):
        - `LCK_M_S_XACT_READ` - Occurs when a task is waiting for a shared lock on an `XACT` `wait_resource` type, with an intent to read.
        - `LCK_M_S_XACT_MODIFY` - Occurs when a task is waiting for a shared lock on an `XACT` `wait_resource` type, with an intent to modify.
        - `LCK_M_S_XACT` - Occurs when a task is waiting for a shared lock on an `XACT` `wait_resource` type, where the intent cannot be inferred. Rare.
- Locking resources visibility
    - `XACT` locking resources. For more information, see `resource_description` in [sys.dm_tran_locks (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).
- Wait resource visibility
    - `XACT` wait resources. For more information, see `wait_resource` in [sys.dm_exec_requests (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).
- Deadlock graph
    - Under each resource in the deadlock report's `<resource-list>`, each `<xactlock>` element reports the underlying resources and specific information for locks of each member of a deadlock. For more information and an example, see [Optimized locking and deadlocks](../sql-server-deadlocks-guide.md#optimized-locking-and-deadlocks).

## Best practices with optimized locking

### Enable read committed snapshot isolation (RCSI)

To maximize the benefits of optimized locking, it is recommended to enable [read committed snapshot isolation (RCSI)](../../t-sql/statements/alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true#read_committed_snapshot--on--off--1) on the database and use read committed isolation as the default isolation level. If not enabled, enable RCSI using the following sample:

```sql
ALTER DATABASE databasename SET READ_COMMITTED_SNAPSHOT ON;
```

In [!INCLUDE [asdb](../../includes/ssazure-sqldb.md)], RCSI is enabled by default and read committed is the default isolation level. With RCSI enabled and when using read committed isolation level, readers don't block writers, and writers don't block readers. Readers read a version of the row from the snapshot taken at the start of the query. With LAQ, writers will qualify rows per the predicate based on the latest committed version of the row without acquiring U locks. With LAQ, a query will wait only if the row qualifies and there is an active write transaction on that row. Qualifying based on the latest committed version and locking only the qualified rows reduces blocking and increases concurrency.

In addition to reduced blocking, the lock memory required will be reduced. This is because readers don't take any locks, and writers take only short duration locks, instead of locks that expire at the end of the transaction. When using stricter isolation levels like repeatable read or serializable, the Database Engine is forced to hold row and page locks until the end of the transaction, for both readers and writers, resulting in increased blocking and lock memory.

### Avoid locking hints

While [table and query hints](../../t-sql/queries/hints-transact-sql.md) are honored, they reduce the benefit of optimized locking. Lock hints like `UPDLOCK`, `READCOMMITTEDLOCK`, `XLOCK`, `HOLDLOCK`, etc., in your queries reduce the full benefits of optimized locking. Having such lock hints in the queries forces the Database Engine to take row/page locks and hold them until the end of the transaction, to honor the intent of the lock hints. Some applications have logic where lock hints are needed, for example when reading a row with select with `UPDLOCK` and then updating it later. We recommend using lock hints only where needed.

With optimized locking, there are no restrictions on existing queries and queries do not need to be rewritten. Queries that are not using hints will benefit most from optimized locking.

A table hint on one table in a query will not disable optimized locking for other tables in the same query. Further, optimized locking only affects the locking behavior of tables being updated by an UPDATE statement. For example:

```sql
CREATE TABLE t3
(a int not null
, b int not null);

CREATE TABLE t4
(a int not null
, b int not null);
GO
INSERT INTO t3 VALUES (1,10),(2,20),(3,30);
INSERT INTO t4 VALUES (1,10),(2,20),(3,30);
GO

UPDATE t3 SET t3.b = t4.b
FROM t3
INNER JOIN t4 WITH (UPDLOCK) ON t3.a = t4.a;
```

In the previous query example, only table `t4` will be affected by the locking hint, while `t3` can still benefit from optimized locking.

```sql
UPDATE t3 SET t3.b = t4.b
FROM t3 WITH (REPEATABLEREAD)
INNER JOIN t4 ON t3.a = t4.a;
```

In the previous query example, only table `t3` will use the repeatable read isolation level, and will hold locks until the end of the transaction. Other updates to `t3` can still benefit from optimized locking. The same applies to the `HOLDLOCK` hint.

## Frequently asked questions (FAQ)

### Where is optimized locking currently available?

Currently, optimized locking is available in [!INCLUDE [Azure SQL Database](../../includes/ssazure-sqldb.md)].

Optimized locking is available in the following service tiers:

- all DTU service tiers
- all vCore service tiers, including provisioned and serverless

Optimized locking is not currently available in:

- [!INCLUDE [Azure SQL Managed Instance](../../includes/ssazuremi-md.md)]
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

### Is optimized locking on by default in both new and existing databases?

In [!INCLUDE [Azure SQL Database](../../includes/ssazure-sqldb.md)], yes.

### How can I detect if optimized locking is enabled?

See [Is optimized locking enabled?](#is-optimized-locking-enabled)

### What happens when accelerated database recovery (ADR) is not enabled on my database?

If ADR is disabled, optimized locking is automatically disabled as well.

### What if I want to force queries to block despite optimized locking?

For customers using RCSI, to force blocking between two queries when optimized locking is enabled, use the `READCOMMITTEDLOCK` query hint.

## Related content

- [Transaction locking and row versioning guide](../sql-server-transaction-locking-and-row-versioning-guide.md)
- [Read committed snapshot isolation (RCSI)](../../t-sql/statements/alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true#read_committed_snapshot--on--off--1)
- [sys.dm_tran_locks (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)
- [Accelerated database recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery)
- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
