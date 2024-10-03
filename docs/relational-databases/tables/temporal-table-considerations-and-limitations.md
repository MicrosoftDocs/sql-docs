---
title: Temporal table considerations and limitations
description: Considerations and limitations to be aware of when working with temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Temporal table considerations and limitations

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

There are some considerations and limitations to be aware of when working with temporal tables, due to the nature of system-versioning:

- A temporal table must have a primary key defined, in order to correlate records between the current table and the history table. The history table can't have a primary key defined.

- The `SYSTEM_TIME` period columns used to record the `ValidFrom` and `ValidTo` values must be defined with a data type of **datetime2**.

- Temporal syntax works on tables or views that are *stored locally* in the database. With remote objects such as tables on a linked server, or external tables, you can't use the `FOR` clause or period predicates directly in the query.

- If the name of a history table is specified during history table creation, you must specify the schema and table name.

- By default, the history table is `PAGE` compressed.

- If current table is partitioned, the history table is created on default file group because partitioning configuration isn't replicated automatically from the current table to the history table.

- Temporal and history tables can't use FileTable or FILESTREAM. FileTable and FILESTREAM allow data manipulation outside of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], so system versioning can't be guaranteed.

- A node or edge table can't be created as or altered to a temporal table.

- While temporal tables support blob data types, such as **(n)varchar(max)**, **varbinary(max)**, **(n)text**, and **image**, they incur significant storage costs and have performance implications due to their size. As such, when designing your system, care should be taken when using these data types.

- The history table must be created in the same database as the current table. Temporal querying over linked servers isn't supported.

- The history table can't have constraints (primary key, foreign key, table, or column constraints).

- Indexed views aren't supported on top of temporal queries (queries that use `FOR SYSTEM_TIME` clause).

- Online option (`WITH (ONLINE = ON`) has no effect on `ALTER TABLE ALTER COLUMN` in a system-versioned temporal table. `ALTER` column isn't performed as an online operation, regardless of which value was specified for the `ONLINE` option.

- `INSERT` and `UPDATE` statements can't reference the `SYSTEM_TIME` period columns. Attempts to insert values directly into these columns are blocked.

- `TRUNCATE TABLE` isn't supported while `SYSTEM_VERSIONING` is `ON`.

- Direct modification of the data in a history table isn't permitted.

::: moniker range=" =sql-server-2016"

- `ON DELETE CASCADE` and `ON UPDATE CASCADE` aren't permitted on the current table. In other words, when temporal table is referencing table in the foreign key relationship (corresponding to `parent_object_id` in `sys.foreign_key`) `CASCADE` options aren't allowed. To work around this limitation, use application logic or after triggers to maintain consistency on delete in primary key table (corresponding to `referenced_object_id` in `sys.foreign_key`). If primary key table is temporal and referencing table is non-temporal, there's no such limitation.

::: moniker-end

- `INSTEAD OF` triggers aren't permitted on either the current or the history table to avoid invalidating the DML logic. `AFTER` triggers are permitted only on the current table. These triggers are blocked on the history table to avoid invalidating the DML logic.

- Usage of replication technologies is limited:

  - **Availability groups:** Fully supported

  - **Change data capture and change tracking:** Supported only on the current table

  - **Snapshot and transactional replication**: Only supported for a single publisher without temporal being enabled, and *one* subscriber with temporal enabled. Use of multiple subscribers isn't supported due to a dependency on the local system clock, which can lead to inconsistent temporal data. In this case, the publisher is used for an OLTP workload while subscriber serves for offloading reporting (including `AS OF` querying). When the distribution agent starts, it opens a transaction that is held open until distribution agent stops. `ValidFrom` and `ValidTo` are populated to the begin time of the first transaction that distribution agent starts. It might be preferable to run the distribution agent on a schedule rather than the default behavior of running it continuously, if having `ValidFrom` and `ValidTo` populated with a time that is close to the current system time is important to your application or organization. For more information, see [Temporal table usage scenarios](temporal-table-usage-scenarios.md).

  - **Merge replication:** Not supported for temporal tables

- Regular queries only affect data in the current table. To query data in the history table, you must use temporal queries. For more information, see [Query data in a system-versioned temporal table](querying-data-in-a-system-versioned-temporal-table.md).

- An optimal indexing strategy includes a clustered columns store index and/or a B-tree rowstore index on the current table, and a clustered columnstore index on the history table, for optimal storage size and performance. If you create/use your own history table, we strongly recommend that you create this type of index consisting of period columns starting with the end of period column. This index speeds up temporal querying, and speeds up the queries that are part of the data consistency check. The default history table has a clustered rowstore index created for you based on the period columns (end, start). At a minimum, a nonclustered rowstore index is recommended.

- The following objects/properties aren't replicated from the current to the history table when the history table is created:

  - Period definition
  - Identity definition
  - Indexes
  - Statistics
  - Check constraints
  - Triggers
  - Partitioning configuration
  - Permissions
  - Row-level security predicates

- A history table can't be configured as current table in a chain of history tables.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Partition with temporal tables](partitioning-with-temporal-tables.md)
- [Temporal table security](temporal-table-security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
