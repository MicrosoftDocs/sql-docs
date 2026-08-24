---
title: "Indexes"
description: Indexes
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 08/21/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: concept-article
ms.update-cycle: 1825-days
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "index types [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

## Available index types

The following table lists the types of indexes available in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and provides links to additional information.

| Index type | Description | Additional information |
| --- | --- | --- |
| Hash | With a hash index, data is accessed through an in-memory hash table. Hash indexes consume a fixed amount of memory, which is a function of the bucket count. | [Indexes on Memory-Optimized Tables](../in-memory-oltp/indexes-for-memory-optimized-tables.md)<br /><br />[Hash Index Design Guidelines](../sql-server-index-design-guide.md#hash_index) |
| memory-optimized Nonclustered | For memory-optimized nonclustered indexes, memory consumption is a function of the row count and the size of the index key columns | [Indexes on Memory-Optimized Tables](../in-memory-oltp/indexes-for-memory-optimized-tables.md)<br /><br />[Memory-Optimized Nonclustered Index Design Guidelines](../sql-server-index-design-guide.md#inmem_nonclustered_index) |
| Clustered | A clustered index sorts and stores the data rows of the table or view in order based on the clustered index key. The clustered index is implemented as a B-tree index structure that supports fast retrieval of the rows, based on their clustered index key values. | [Clustered and nonclustered indexes](clustered-and-nonclustered-indexes-described.md)<br /><br />[Create a clustered index](create-clustered-indexes.md)<br /><br />[Clustered Index Design Guidelines](../sql-server-index-design-guide.md#Clustered) |
| Nonclustered | A nonclustered index can be defined on a table or view with a clustered index or on a heap. Each index row in the nonclustered index contains the nonclustered key value and a row locator. This locator points to the data row in the clustered index or heap having the key value. The rows in the index are stored in the order of the index key values, but the data rows aren't guaranteed to be in any particular order unless a clustered index is created on the table. | [Clustered and nonclustered indexes](clustered-and-nonclustered-indexes-described.md)<br /><br />[Create nonclustered indexes](create-nonclustered-indexes.md)<br /><br />[Nonclustered Index Design Guidelines](../sql-server-index-design-guide.md#Nonclustered) |
| Unique | A unique index ensures that the index key contains no duplicate values and therefore every row in the table or view is in some way unique.<br /><br />Uniqueness can be a property of both clustered and nonclustered indexes. | [Create a unique index](create-unique-indexes.md)<br /><br />[Unique Index Design Guidelines](../sql-server-index-design-guide.md#Unique) |
| Columnstore | An in-memory columnstore index stores and manages data by using column-based data storage and column-based query processing.<br /><br />Columnstore indexes work well for data warehousing workloads that primarily perform bulk loads and read-only queries. Use the columnstore index to achieve up to **10x query performance** gains over traditional row-oriented storage, and up to **7x data compression** over the uncompressed data size. | [Columnstore indexes: overview](columnstore-indexes-overview.md)<br /><br />[Columnstore Index Design Guidelines](../sql-server-index-design-guide.md#columnstore_index) |
| Index with included columns | A nonclustered index that is extended to include nonkey columns in addition to the key columns. | [Create indexes with included columns](create-indexes-with-included-columns.md) |
| Index on computed columns | An index on a column that is derived from the value of one or more other columns, or certain deterministic inputs. | [Indexes on computed columns](indexes-on-computed-columns.md) |
| Filtered | An optimized nonclustered index, especially suited to cover queries that select from a well-defined subset of data. It uses a filter predicate to index a portion of rows in the table. A well-designed filtered index can improve query performance, reduce index maintenance costs, and reduce index storage costs compared with full-table indexes. | [Create filtered indexes](create-filtered-indexes.md)<br /><br />[Filtered Index Design Guidelines](../sql-server-index-design-guide.md#Filtered) |
| Spatial | A spatial index provides the ability to perform certain operations more efficiently on spatial objects (*spatial data*) in a column of the **geometry** data type. The spatial index reduces the number of objects on which relatively costly spatial operations need to be applied. | [Spatial Indexes Overview](../spatial/spatial-indexes-overview.md) |
| XML | A shredded, and persisted, representation of the XML binary large objects (BLOBs) in the **xml** data type column. | [XML indexes (SQL Server)](../xml/xml-indexes-sql-server.md) |
| Full-text | A special type of token-based functional index that is built and maintained by the Microsoft Full-Text Engine for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. It provides efficient support for sophisticated word searches in character string data. | [Populate Full-Text Indexes](../search/populate-full-text-indexes.md) |

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Related content

- [Index architecture and design guide](../sql-server-index-design-guide.md)
- [SORT_IN_TEMPDB Option For Indexes](sort-in-tempdb-option-for-indexes.md)
- [Disable indexes and constraints](disable-indexes-and-constraints.md)
- [Enable indexes and constraints](enable-indexes-and-constraints.md)
- [Rename Indexes](rename-indexes.md)
- [Set Index Options](set-index-options.md)
- [Disk space requirements for index DDL operations](disk-space-requirements-for-index-ddl-operations.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)
- [Specify fill factor for an index](specify-fill-factor-for-an-index.md)
- [Page and extent architecture guide](../pages-and-extents-architecture-guide.md)
- [Clustered and nonclustered indexes](clustered-and-nonclustered-indexes-described.md)
