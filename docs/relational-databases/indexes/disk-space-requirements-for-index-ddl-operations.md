---
title: "Disk Space Requirements for Index DDL Operations"
description: Disk Space Requirements for Index DDL Operations
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/22/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "disk space [SQL Server], indexes"
  - "index disk space [SQL Server]"
  - "space [SQL Server], indexes"
  - "indexes [SQL Server], disk space requirements"
  - "temporary disk space [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Disk space requirements for index DDL operations

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Disk space is an important consideration when you create, rebuild, or drop indexes. Inadequate disk space can degrade performance or even cause the index operation to fail. This article provides general information that can help you determine the amount of disk space required for index data definition language (DDL) operations.

## Index operations that require no additional disk space

The following index operations require no additional disk space:

- `ALTER INDEX REORGANIZE`; however, log space is required.

- `DROP INDEX` when you're dropping a nonclustered index.

- `DROP INDEX` when you're dropping a clustered index offline without specifying the `MOVE TO` clause and nonclustered indexes don't exist.

- `CREATE TABLE` (`PRIMARY KEY` or `UNIQUE` constraints)

## Index operations that require additional disk space

All other index DDL operations require additional temporary disk space to use during the operation, and permanent disk space to store the new index structure or structures.

When a new index structure is created, disk space for both the old (source) and new (target) structures is required in their appropriate files and filegroups. The old structure isn't deallocated until the index creation transaction commits.

The following index DDL operations create new index structures and require additional disk space:

- `CREATE INDEX`
- `CREATE INDEX WITH DROP_EXISTING`
- `ALTER INDEX REBUILD`
- `ALTER TABLE ADD CONSTRAINT` (`PRIMARY KEY` or `UNIQUE`)
- `ALTER TABLE DROP CONSTRAINT` (`PRIMARY KEY` or `UNIQUE`) when the constraint is based on a clustered index
- `DROP INDEX MOVE TO` (Applies only to clustered indexes.)

## Temporary disk space for sorting

Besides the disk space required for the source and target structures, temporary disk space is required for sorting, unless the query optimizer finds an execution plan that doesn't require sorting.

If sorting is required, sorting occurs one new index at a time. For example, when you rebuild a clustered index and associated nonclustered indexes within a single statement, the indexes are sorted one after the other. Therefore, the additional temporary disk space that is required for sorting only has to be as large as the largest index in the operation. This is almost always the clustered index.

If the `SORT_IN_TEMPDB` option is set to `ON`, the largest index must fit into `tempdb`. Although this option increases the amount of temporary disk space that is used to create an index, it might reduce the time that is required to create an index when `tempdb` is on a set of disks different from the user database.

If `SORT_IN_TEMPDB` is set to `OFF` (the default) each index, including partitioned indexes, is sorted in its destination disk space; and only the disk space for the new index structures is required.

For an example of calculating disk space, see [Index disk space example](index-disk-space-example.md).

## Temporary disk space for online index operations

When you perform index operations online, additional temporary disk space is required.

If a clustered index is created, rebuilt, or dropped online, a temporary nonclustered index is created to map old bookmarks to new bookmarks. If the `SORT_IN_TEMPDB` option is set to `ON`, this temporary index is created in `tempdb`. If `SORT_IN_TEMPDB` is set to `OFF`, the same filegroup or partition scheme as the target index is used. The temporary mapping index contains one record for each row in the table, and its contents is the union of the old and new bookmark columns, including uniqueifiers and record identifiers and including only a single copy of any column used in both bookmarks. For more information about online index operations, see [Perform index operations online](perform-index-operations-online.md).

> [!NOTE]  
> The `SORT_IN_TEMPDB` option can't be set for `DROP INDEX` statements. The temporary mapping index is always created in the same filegroup or partition scheme as the target index.

Online index operations use row versioning to isolate the index operation from the effects of modifications made by other transactions. This avoids the need for requesting share locks on rows that have been read. Concurrent user update and delete operations during online index operations require space for version records in `tempdb`. For more information, see [Perform index operations online](perform-index-operations-online.md) .

## Related tasks

- [Index disk space example](index-disk-space-example.md)
- [Transaction log disk space for index operations](transaction-log-disk-space-for-index-operations.md)
- [Estimate the size of a table](../databases/estimate-the-size-of-a-table.md)
- [Estimate the size of a clustered index](../databases/estimate-the-size-of-a-clustered-index.md)
- [Estimate the size of a nonclustered index](../databases/estimate-the-size-of-a-nonclustered-index.md)
- [Estimate the size of a heap](../databases/estimate-the-size-of-a-heap.md)

## Related content

- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [DROP INDEX (Transact-SQL)](../../t-sql/statements/drop-index-transact-sql.md)
- [Specify Fill Factor for an Index](specify-fill-factor-for-an-index.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)

