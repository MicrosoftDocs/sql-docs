---
title: Clustered and Nonclustered Indexes
description: Learn about clustered and nonclustered indexes in SQL Server, Azure SQL, and SQL database in Fabric, and how the query optimizer uses them to make queries faster.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/11/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: concept-article
ms.update-cycle: 1825-days
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "query optimizer [SQL Server], index usage"
  - "index concepts [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Clustered and nonclustered indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

An index is an on-disk structure associated with a table or view that speeds retrieval of rows from the table or view. An index contains keys built from one or more columns in the table or view. These keys are stored in a structure (B-tree) that enables the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] to find the row or rows associated with the key values quickly and efficiently.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

A table or view can contain *clustered* and *nonclustered* indexes.

Both clustered and nonclustered indexes can be unique. With a unique index, no two rows can have the same value for the index key. Otherwise, the index isn't unique and multiple rows can share the same key value. For more information, see [Create a unique index](create-unique-indexes.md).

Indexes are automatically maintained for a table or view whenever the table data is modified.

For more types of special purpose indexes, including [indexes on memory-optimized tables](../in-memory-oltp/indexes-for-memory-optimized-tables.md), see [Indexes](indexes.md).

## Clustered indexes

Clustered indexes sort and store the data rows in the table or view based on their key values. These key values are the columns included in the index definition. You can have only one clustered index per table, because the data rows themselves can be stored in only one order.

The only time the data rows in a table are stored in sorted order is when the table contains a clustered index. When a table has a clustered index, the table is called a *clustered table*. If a table has no clustered index, its data rows are stored in an unordered structure called a *heap*.

## Nonclustered indexes

Nonclustered indexes have a structure separate from the data rows. A nonclustered index contains the nonclustered index key values and each key value entry has a pointer to the data row that contains the key value.

The pointer from an index row in a nonclustered index to a data row is called a row locator. The structure of the row locator depends on whether the data pages are stored in a heap or a clustered table. For a heap, a row locator is a pointer to the row. For a clustered table, the row locator is the clustered index key.

You can add nonkey columns to the leaf level of the nonclustered index to bypass existing index key limits, and execute fully covered queries. For more information, see [Create indexes with included columns](create-indexes-with-included-columns.md). For details about index key limits, see [Maximum capacity specifications for SQL Server](../../sql-server/maximum-capacity-specifications-for-sql-server.md).

## Indexes and constraints

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] automatically creates indexes when you define `PRIMARY KEY` and `UNIQUE` constraints on table columns. For example, when you create a table with a `UNIQUE` constraint, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] automatically creates a nonclustered index. If you configure a `PRIMARY KEY`, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] automatically creates a clustered index, unless a clustered index already exists. When you try to enforce a `PRIMARY KEY` constraint on an existing table and a clustered index already exists on that table, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] enforces the primary key by using a nonclustered index.

For more information, see [Create primary keys](../tables/create-primary-keys.md) and [Create unique constraints](../tables/create-unique-constraints.md).

<a id="how-indexes-are-used-by-the-query-optimizer"></a>

## How the query optimizer uses indexes

Well-designed indexes can reduce disk I/O operations and consume fewer system resources. Therefore, these indexes improve query performance. Indexes can be helpful for various queries that contain `SELECT`, `UPDATE`, `DELETE`, or `MERGE` statements. Consider the query `SELECT JobTitle, HireDate FROM HumanResources.Employee WHERE BusinessEntityID = 250` in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. When you run this query, the query optimizer evaluates each available method for retrieving the data and selects the most efficient method. The method might be a table scan, or it might be scanning one or more indexes if they exist.

During a table scan, the query optimizer reads all the rows in the table, and extracts the rows that meet the criteria of the query. A table scan generates many disk I/O operations and can be resource intensive. However, a table scan could be the most efficient method if, for example, the result set of the query is a high percentage of rows from the table.

When the query optimizer uses an index, it searches the index key columns, finds the storage location of the rows needed by the query and extracts the matching rows from that location. Generally, searching the index is much faster than searching the table. Unlike a table, an index frequently contains very few columns per row and the rows are in sorted order.

The query optimizer typically selects the most efficient method when executing queries. However, if no indexes are available, the query optimizer must use a table scan. You need to design and create indexes that are best suited to your environment so that the query optimizer has a selection of efficient indexes from which to select. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] provides the [Database Engine Tuning Advisor](../performance/database-engine-tuning-advisor.md) to help with the analysis of your database environment and in the selection of appropriate indexes.

When a beneficial index doesn't exist, the optimizer records the suggestion in execution plans and the missing index dynamic management views. To act on those suggestions, see [Tune nonclustered indexes with missing index suggestions](tune-nonclustered-missing-index-suggestions.md).

> [!NOTE]  
> A disabled index remains in metadata, but the optimizer ignores it until you rebuild it. For more information, see [Disable indexes and constraints](disable-indexes-and-constraints.md) and [Enable indexes and constraints](enable-indexes-and-constraints.md).

For more information about index design guidelines and internals, see the [Index architecture and design guide](../sql-server-index-design-guide.md).

## Related content

- [Index architecture and design guide](../sql-server-index-design-guide.md)
- [Create a clustered index](create-clustered-indexes.md)
- [Create nonclustered indexes](create-nonclustered-indexes.md)
