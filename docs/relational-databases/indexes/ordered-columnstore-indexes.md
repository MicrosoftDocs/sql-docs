---
title: "Performance Tuning with Ordered Columnstore Indexes"
description: "Learn more about how ordered columnstore indexes can benefit query performance."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: nibruno, xiaoyul, randolphwest, dfurman
ms.date: 01/12/2026
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Performance tuning with ordered columnstore indexes

[!INCLUDE [sqlserver2022-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricsqldb.md)]

Ordered columnstore indexes can provide faster performance by skipping large amounts of ordered data that don't match the query predicate. While loading data into an ordered columnstore index and maintaining order through index rebuild takes longer than in a non-ordered index, indexed queries can run faster with ordered columnstore.

When a query reads a columnstore index, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] checks the minimum and maximum values stored in each column segment. The process eliminates segments that fall outside the bounds of the query predicate. In other words, it skips these segments when reading data from disk or memory. A query finishes faster if the number of segments to read and their total size is significantly smaller.

With certain data load patterns, data in a columnstore index might be ordered implicitly without specifying the `ORDER` clause. For example, if data loads occur every day, then the data might be ordered by a `load_date` column. In this case, query performance can already benefit from this implicit order. Ordering the columnstore index by the same `load_date` column explicitly in the `ORDER` clause isn't likely to provide an extra performance benefit.

For ordered columnstore index availability in various SQL platforms and SQL Server versions, see [Ordered columnstore index availability](columnstore-indexes-overview.md#ordered-columnstore-index-availability).

For more information about recently added features for columnstore indexes, see [What's new in columnstore indexes](columnstore-indexes-what-s-new.md).

## Ordered vs. non-ordered columnstore index

In a columnstore index, the data in each column of each rowgroup is compressed into a separate segment. Each segment contains metadata describing its minimum and maximum values, so the query execution process can skip segments that fall outside the bounds of the query predicate.

When a columnstore index isn't ordered, the index builder doesn't sort the data before compressing it into segments. That means that segments with overlapping value ranges can occur, causing queries to read more segments to obtain the required data. As a result, queries can take longer to finish.

When you create an ordered columnstore index by specifying the `ORDER` clause in the [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md) statement, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] sorts the data in each segment of each order column before the index builder compresses the data into segments. With sorted data, segment overlapping is reduced or eliminated, allowing queries to use a more efficient segment elimination and thus faster performance because there are fewer segments and less data to read.

<a id="reduce-segment-overlap"></a>

## Reduce segment overlap and improve query performance

When you build an ordered columnstore index, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] sorts the data on a best-effort basis. Depending on the available memory, the data size, the degree of parallelism, the index type (clustered vs. nonclustered), and the type of index build (offline vs. online), the order in a column in a columnstore index might be full with no segment overlap, or partial with some segment overlap. When there are fewer overlapping segments, a query that can take advantage of column order runs faster.

> [!TIP]  
> Even if the order in a column of a columnstore index is partial, segments can still be eliminated (skipped). A full order isn't required to gain performance benefits if a partial order avoids many segment overlaps.

The following table describes the resulting order type when you create or rebuild an ordered columnstore index, depending on the index build options.

| Prerequisites | Order type |
| --- | --- |
| `ONLINE = ON` and `MAXDOP = 1` | Full |
| `ONLINE = OFF`, `MAXDOP = 1`, and the data to sort fully fits in the query workspace memory | Full |
| All other cases | Partial |

In the first case when both `ONLINE = ON` and `MAXDOP = 1`, the sort isn't limited by the query workspace memory because an online build of an ordered columnstore index uses the `tempdb` database to spill the data that doesn't fit in memory. This approach can make the index build process slower due to the additional `tempdb` I/O, and requires sufficient free space in `tempdb`. However, because the index build is performed online, queries can continue using the existing index while the new ordered index is being built.

Similarly, with an offline rebuild of a partitioned columnstore index, the rebuild is done one partition at a time. Other partitions remain available for queries.

When `MAXDOP` is greater than 1, each thread used for ordered columnstore index build works on a subset of data and sorts it locally. There's no global sorting across data sorted by different threads. Using parallel threads can reduce the time to create the index, but it results in more overlapping segments than when using a single thread.

You can create or rebuild ordered columnstore indexes online only in some SQL platforms and SQL Server versions. For more information, see [Feature summary for product releases](columnstore-indexes-what-s-new.md#feature-summary-for-product-releases).

In SQL Server, online index operations aren't available in all editions. [!INCLUDE [editions-latest](../../includes/editions-latest.md)] For more information, see [Perform index operations online](perform-index-operations-online.md).

For certain data types and encodings, the [sys.column_store_segments](../system-catalog-views/sys-column-store-segments-transact-sql.md) system view can help you find the number of segment overlaps. A [sample script](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/columnstore/ordered-columnstore/order-quality.sql) based on this view determines the order quality for eligible columns of all columnstore indexes in the current database.

## Query performance

The performance gain from an ordered columnstore index depends on the query patterns, the size of data, the number of overlapping segments, and the compute resources available for query execution.

Queries with the following patterns typically run faster with ordered columnstore indexes:

- Queries that have equality, inequality, or range predicates.
- Queries where the predicate columns and the ordered CCI columns are the same.

In the following example, table `T1` has a clustered columnstore index with `Col_C`, `Col_B`, and `Col_A` as ordered columns.

```sql
CREATE CLUSTERED COLUMNSTORE INDEX OrderedCCI
ON T1
ORDER (Col_C, Col_B, Col_A);
```

Query 1 benefits from the ordered columnstore index more than queries 2 and 3, because query 1 references all the ordered columns in its predicate.

```sql
-- query 1
SELECT *
FROM T1
WHERE Col_C = 'c'
      AND Col_B = 'b'
      AND Col_A = 'a';

-- query 2
SELECT *
FROM T1
WHERE Col_B = 'b'
      AND Col_A = 'a';

-- query 3
SELECT *
FROM T1
WHERE Col_A = 'a'
      AND Col_C = 'c';
```

## Data load performance

The performance of a data load into a table with an ordered columnstore index is similar to a partitioned table. Loading data can take longer than with a non-ordered columnstore index because of the data sorting operation, but queries can run faster afterwards.

### Add new data or update existing data

The new data resulting from a DML batch or a bulk load operation on a table with an ordered columnstore index is sorted within that batch only. There's no global sorting that includes existing data in the table because compressed rowgroups in a columnstore index are immutable.

To reduce segment overlap after inserting new data or updating existing data, rebuild the columnstore index.

## Examples

### Create an ordered columnstore index

Clustered ordered columnstore index:

```sql
CREATE CLUSTERED COLUMNSTORE INDEX OCCI
ON dbo.Table1
ORDER(Column1, Column2);
```

Nonclustered ordered columnstore index:

```sql
CREATE NONCLUSTERED COLUMNSTORE INDEX ONCCI
ON dbo.Table1(Column1, Column2, Column3)
ORDER(Column1, Column2);
```

### Check for ordered columns and order ordinal

```sql
SELECT OBJECT_SCHEMA_NAME(c.object_id) AS schema_name,
       OBJECT_NAME(c.object_id) AS table_name,
       c.name AS column_name,
       i.column_store_order_ordinal
FROM sys.index_columns AS i
     INNER JOIN sys.columns AS c
         ON i.object_id = c.object_id
        AND c.column_id = i.column_id
WHERE column_store_order_ordinal > 0;
```

### Add or remove order columns and rebuild an existing ordered columnstore index

Clustered ordered columnstore index:

```sql
CREATE CLUSTERED COLUMNSTORE INDEX OCCI
ON dbo.Table1
ORDER(Column1, Column2)
WITH (DROP_EXISTING = ON);
```

Nonclustered ordered columnstore index:

```sql
CREATE NONCLUSTERED COLUMNSTORE INDEX ONCCI
ON dbo.Table1(Column1, Column2, Column3)
ORDER(Column1, Column2)
WITH (DROP_EXISTING = ON);
```

<a id="create-an-ordered-clustered-columnstore-index-online-with-full-sort-on-a-heap-table"></a>

### Create an ordered clustered columnstore index online with full order on a heap table

```sql
CREATE CLUSTERED COLUMNSTORE INDEX OCCI
ON dbo.Table1
ORDER(Column1)
WITH (ONLINE = ON, MAXDOP = 1);
```

<a id="rebuild-an-ordered-clustered-columnstore-index-online-with-full-sort"></a>

### Rebuild an ordered clustered columnstore index online with full order

```sql
CREATE CLUSTERED COLUMNSTORE INDEX OCCI
ON dbo.Table1
ORDER(Column1)
WITH (DROP_EXISTING = ON, ONLINE = ON, MAXDOP = 1);
```

## Related content

- [Columnstore index design guidelines](../sql-server-index-design-guide.md#columnstore_index)
- [Columnstore indexes - data loading guidance](columnstore-indexes-data-loading-guidance.md)
- [Get started with columnstore indexes for real-time operational analytics](get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Columnstore indexes in data warehousing](columnstore-indexes-data-warehouse.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)
- [Columnstore index architecture](../sql-server-index-design-guide.md#columnstore_index)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
