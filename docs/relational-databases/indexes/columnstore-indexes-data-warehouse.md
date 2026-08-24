---
title: "Columnstore indexes in data warehousing"
description: Learn more about how to benefit from columnstore indexes in data warehousing with the SQL Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/04/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Columnstore indexes in data warehousing

[!INCLUDE [SQL Server Azure SQL Database PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-pdw-fabricsqldb.md)]

Columnstore indexes, in conjunction with partitioning, are essential for building a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data warehouse. This article focuses on key use cases and examples for data warehousing designs with the SQL Database Engine.

## Key features for data warehousing

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] introduced these features for columnstore performance enhancements:

- Always On availability groups support querying a columnstore index on a readable secondary replica.
- Multiple Active Result Sets (MARS) supports columnstore indexes.
- A new dynamic management view [sys.dm_db_column_store_row_group_physical_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md) provides performance troubleshooting information at the row group level.
- All queries on columnstore indexes can run in batch mode. Previously, only parallel queries could run in batch mode.
- The **Sort**, **Distinct Sort**, and **Distinct** operators run in batch mode.
- Window aggregates now runs in batch mode for database compatibility level 130 and higher.
- Aggregate pushdown for efficient processing of aggregates. This is supported on all database compatibility levels.
- String predicate pushdown for efficient processing of string predicates. This is supported on all database compatibility levels.
- Snapshot isolation for database compatibility level 130 and higher.
- Ordered clustered columnstore indexes were introduced with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)]. For more information, see [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md#order-for-clustered-columnstore) and [Performance tuning with ordered columnstore indexes](ordered-columnstore-indexes.md). For ordered columnstore index availability, see [Ordered column index availability](columnstore-indexes-overview.md#ordered-columnstore-index-availability).

For more information about new features in versions and platforms of SQL Server and Azure SQL, see [What's new in columnstore indexes](columnstore-indexes-what-s-new.md).

## Improve performance by combining nonclustered and columnstore indexes

 Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can create rowstore nonclustered indexes on a clustered columnstore index.

### Example: Improve efficiency of table seeks with a nonclustered index

To improve efficiency of table seeks in a data warehouse, you can create a nonclustered index designed to run queries that perform best with table seeks. For example, queries that look for matching values or return a small range of values perform better against a B-tree index rather than a columnstore index. They don't require a full scan of the columnstore index and return the correct result faster by doing a binary search through a B-tree index.

```sql
--BASIC EXAMPLE: Create a nonclustered index on a columnstore table.

--Create the table
CREATE TABLE t_account (
    AccountKey int NOT NULL,
    AccountDescription nvarchar (50),
    AccountType nvarchar(50),
    UnitSold int
);

--Store the table as a columnstore.  
CREATE CLUSTERED COLUMNSTORE INDEX taccount_cci ON t_account;

--Add a nonclustered index.
CREATE UNIQUE INDEX taccount_nc1 ON t_account (AccountKey);
```

### Example: Use a nonclustered index to enforce a primary key constraint on a columnstore table

Because a table can have at most one clustered index, a table with a clustered columnstore index can't have a clustered primary key constraint. To create a primary key constraint on a columnstore table, you must declare it as nonclustered.

The following example creates a table with a nonclustered primary key constraint and then creates a clustered columnstore index on the table. Since any insert or update on the columnstore table also modifies the nonclustered index, all operations that violate the primary key constraint cause the entire operation to fail.

```sql
--Create a primary key constraint on a columnstore table.

--Create a rowstore table with a nonclustered primary key constraint.
CREATE TABLE t_account (
    AccountKey int NOT NULL,
    AccountDescription nvarchar (50),
    AccountType nvarchar(50),
    UnitSold int,
    CONSTRAINT pk_account PRIMARY KEY NONCLUSTERED (AccountKey)
);

--Convert the table to columnstore.
--The primary key constraint is preserved as a nonclustered index on the columnstore table.
CREATE CLUSTERED COLUMNSTORE INDEX t_account_cci ON t_account;
```

### Improve performance by enabling row-level and rowgroup-level locking

To complement the nonclustered index on a columnstore index feature, [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] offers granular locking capability for `SELECT`, `UPDATE`, and `DELETE` operations. Queries can run with row-level locking on index seeks against a nonclustered index and rowgroup-level locking on full table scans against the columnstore index. Use this to achieve higher read/write concurrency by using row-level and rowgroup-level locking appropriately.

```sql
--Granular locking example
--Store table t_account as a columnstore table.
CREATE CLUSTERED COLUMNSTORE INDEX taccount_cci ON t_account

--Add a nonclustered index for use with this example
CREATE UNIQUE INDEX taccount_nc1 ON t_account (AccountKey);

--Look at locking with access through the nonclustered index
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;

BEGIN TRAN
    -- The query plan chooses a seek operation on the nonclustered index
    -- and takes the row lock
    SELECT * 
    FROM t_account 
    WHERE AccountKey = 100;
COMMIT TRAN;
```

### Snapshot isolation and read-committed snapshot isolation

Use snapshot isolation (SI) to guarantee transactional consistency, and read-committed snapshot isolation (RCSI) to guarantee statement level consistency for queries on columnstore indexes. This allows the queries to run without blocking data writers. This non-blocking behavior also significantly reduces the likelihood of deadlocks for complex transactions. For more information, see [Row versioning-based isolation levels in the Database Engine](../sql-server-transaction-locking-and-row-versioning-guide.md#Row_versioning).

## Related content

- [Columnstore indexes - design guidance](columnstore-indexes-design-guidance.md)
- [Columnstore indexes - data loading guidance](columnstore-indexes-data-loading-guidance.md)
- [Columnstore indexes - query performance](columnstore-indexes-query-performance.md)
- [Get started with columnstore indexes for real-time operational analytics](get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)
- [Columnstore Index Architecture](../sql-server-index-design-guide.md#columnstore_index)
