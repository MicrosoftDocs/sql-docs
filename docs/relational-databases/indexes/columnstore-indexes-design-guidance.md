---
title: "Columnstore indexes - Design guidance"
description: "High-level recommendations for designing columnstore indexes."
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/04/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: best-practice
ms.custom:
  - ignite-2025
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Columnstore indexes - design guidance

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

High-level recommendations for designing columnstore indexes. A few good design decisions help you achieve the high data compression and query performance that columnstore indexes are designed to provide.

## Prerequisites

This article assumes you are familiar with columnstore architecture and terminology. For more information, see [Columnstore indexes: Overview](columnstore-indexes-overview.md) and [Columnstore Index Architecture](../../relational-databases/sql-server-index-design-guide.md#columnstore_index).

### Know your data requirements

Before designing a columnstore index, understand as much as possible about your data requirements. For example, think through the answers to these questions:

- How large is my table?
- Do my queries mostly perform analytics that scan large ranges of values?  Columnstore indexes are designed to work well for large range scans rather than looking up specific values.
- Does my workload perform lots of updates and deletes? Columnstore indexes work well when the data is stable. Queries should be updating and deleting less than 10% of the rows.
- Do I have fact and dimension tables for a data warehouse?
- Do I need to perform analytics on a transactional workload? If so, see the [columnstore design guidance for real-time operational analytics](#use-a-nonclustered-columnstore-index-for-real-time-analytics).

You might not need a columnstore index. Rowstore (or B-tree) tables with heaps or clustered indexes perform best on queries that seek into the data, searching for a particular value, or for queries on a small range of values. Use rowstore indexes with transactional workloads since they tend to require mostly table seeks instead of large range table scans.

## Choose the best columnstore index for your needs

A columnstore index is either clustered or nonclustered.  A clustered columnstore index can have one or more nonclustered B-tree indexes. Columnstore indexes are easy to try. If you create a table as a columnstore index, you can easily convert the table back to a rowstore table by dropping the columnstore index.

Here is a summary of the options and recommendations.

| Columnstore option | Recommendations for when to use | Compression |
| :----------------- | :------------------- | :---------- |
| [Clustered columnstore index](#use-a-clustered-columnstore-index-for-large-data-warehouse-tables) | Use for:<br /><br />1) Traditional data warehouse workload with a star or snowflake schema<br /><br />2) Internet of Things (IOT) workloads that insert large volumes of data with minimal updates and deletes. | Average of 10x |
| [Ordered columnstore index](#use-an-ordered-columnstore-index-for-large-data-warehouse-tables) | Use when a clustered columnstore index is queried via a single ordered predicate column or column set. This guidance is similar to choosing the key columns for a rowstore clustered index, though the compressed underlying rowgroups behave differently. For more information, see [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md#order-for-clustered-columnstore) and [Performance tuning with ordered columnstore indexes](ordered-columnstore-indexes.md). | Average of 10x |
| [Nonclustered B-tree indexes on a clustered columnstore index](#add-b-tree-nonclustered-indexes-for-efficient-table-seeks) | Use to:<br /><br />   1. Enforce primary key and foreign key constraints on a clustered columnstore index.<br /><br />   2. Speed up queries that search for specific values or small ranges of values.<br /><br />   3. Speed up updates and deletes of specific rows.| 10x on average plus some additional storage for the NCIs.|
| [Nonclustered columnstore index on a disk-based heap or B-tree index](#add-b-tree-nonclustered-indexes-for-efficient-table-seeks) | Use for:<br /><br />1) An OLTP workload that has some analytics queries. You can drop B-tree indexes created for analytics and replace them with one nonclustered columnstore index.<br /><br />2) Many traditional OLTP workloads that perform Extract Transform and Load (ETL) operations to move data to a separate data warehouse. You can eliminate ETL and a separate data warehouse by creating a nonclustered columnstore index on some of the OLTP tables. | NCCI is an additional index that requires 10% more storage on average.|
| [Columnstore index on an in-memory table](#use-a-nonclustered-columnstore-index-for-real-time-analytics) | Same recommendations as nonclustered columnstore index on a disk-based table, except the base table is an in-memory table. | Columnstore index is an additional index.|

## Use a clustered columnstore index for large data warehouse tables

The clustered columnstore index is more than an index, it is the primary table storage. It achieves high data compression and a significant improvement in query performance on large data warehousing fact and dimension tables. Clustered columnstore indexes are best suited for analytics queries rather than transactional queries, since analytics queries tend to perform operations on large ranges of values rather than looking up specific values.

Consider using a clustered columnstore index when:

- Each partition has at least a million rows. Columnstore indexes have rowgroups within each partition. If the table is too small to fill a rowgroup within each partition, you might not get the benefits of columnstore compression and query performance.
- Queries primarily perform analytics on ranges of values. For example, to find the average value of a column, the query needs to scan all the column values. It then aggregates the values by summing them to determine the average.
- Most of the inserts are on large volumes of data with minimal updates and deletes. Many workloads such as Internet of Things (IOT) insert large volumes of data with minimal updates and deletes. These workloads can benefit from the compression and query performance gains that come from using a clustered columnstore index.

Don't use a clustered columnstore index when:

- The table requires **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data types. Or, design the columnstore index so that it doesn't include these columns (Applies to: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and previous versions).
- The table data is not permanent. Consider using a heap or temporary table when you need to store and delete the data quickly.
- The table has less than one million rows per partition. 
- More than 10% of the operations on the table are updates and deletes. Large numbers of updates and deletes cause fragmentation. The fragmentation affects compression rates and query performance until you run an operation called reorganize, which forces all data into the columnstore and removes fragmentation. For more information, see [Minimizing index fragmentation in columnstore index](/archive/blogs/sqlserverstorageengine/columnstore-index-defragmentation-using-reorganize-command).

For more information, see [Columnstore indexes in data warehousing](columnstore-indexes-data-warehouse.md).

## Use an ordered columnstore index for large data warehouse tables

For ordered columnstore index availability, see [Columnstore indexes: Overview](columnstore-indexes-overview.md#ordered-columnstore-index-availability).

Consider using an ordered columnstore index in the following scenarios:

- When data is relatively static (without frequently writes and deletes) and the ordered columnstore index key is static, ordered columnstore indexes can provide significant performance advantages over non-ordered columnstore indexes or rowstore indexes for analytical workloads.
- The more distinct values in the first column of the ordered columnstore index key, the better the performance gains might be. This is due to improved segment elimination for string data. For more information, see [segment elimination](columnstore-indexes-query-performance.md#segment-elimination).
- Choose an ordered columnstore index key that is frequently queried and can benefit from segment elimination, especially the first column of the key. Performance gains due to segment elimination on other columns in the table are less predictable.
- Use cases where only the most recent analytical data must be queried, for example, the last 15 seconds, ordered columnstore indexes can provide segment elimination for older data. The first column in the key of the ordered columnstore data must be the date/time data, such as an inserted or created date/time. The segment elimination would be more effective in an ordered columnstore index than in an unordered columnstore index.
- Consider ordered columnstore indexes on tables containing keys with GUID data, where the uniqueidentifier data type can now be used for [segment elimination](columnstore-indexes-query-performance.md#segment-elimination).

An ordered columnstore index might not be as effective in these scenarios:

- Similar to other columnstore indexes, a high rate of insert activity could create excessive storage I/O.
- For workloads where there are a lot of write operations, the quality of segment elimination will be reduced over time because of rowgroup maintenance by the tuple mover. This can be mitigated by regular maintenance of the columnstore index with `ALTER INDEX REORGANIZE`.

## Add B-tree nonclustered indexes for efficient table seeks

Beginning with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can create nonclustered B-tree or rowstore indexes as secondary indexes on a clustered columnstore index. The nonclustered B-tree index is updated as changes occur to the columnstore index. This is a powerful feature that you can use to your advantage.

By using the secondary B-tree index, you can efficiently search for specific rows without scanning through all the rows.  Other options become available too. For example, you can enforce a primary or foreign key constraint by using a UNIQUE constraint on the B-tree index. Since a non-unique value fails to insert into the B-tree index, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] cannot insert the value into the columnstore.

Consider using a B-tree index on a columnstore index to:

- Run queries that search for particular values or small ranges of values.
- Enforce a constraint such as a primary key or foreign key constraint.
- Efficiently perform update and delete operations. The B-tree index is able to quickly locate the specific rows for updates and deletes without scanning the full table or partition of a table.
- You have additional storage available to store the B-tree index.

## Use a nonclustered columnstore index for real-time analytics

Beginning with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can have a nonclustered columnstore index on a rowstore disk-based table or an in-memory OLTP table. This makes it possible to run the analytics in real-time on a transactional table. While transactions are occurring on the underlying table, you can run analytics on the columnstore index. Since one table manages both indexes, changes are available in real-time to both the rowstore and the columnstore indexes.

Since a columnstore index achieves 10x better data compression than a rowstore index, it only needs a small amount of extra storage. For example, if the compressed rowstore table takes 20 GB, the columnstore index might require an additional 2 GB. The additional space required also depends on the number of columns in the nonclustered columnstore index.

 Consider using a nonclustered columnstore index to:

- Run analytics in real-time on a transactional rowstore table. You can replace existing B-tree indexes that are designed for analytics with a nonclustered columnstore index.

- Eliminate the need for a separate data warehouse. Traditionally, companies run transactions on a rowstore table and then load the data into a separate data warehouse to run analytics. For many workloads, you can eliminate the loading process and the separate data warehouse by creating a nonclustered columnstore index on transactional tables.

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] offers several strategies to make this scenario performant. It's easy to try it since you can enable a nonclustered columnstore index with no changes to your OLTP application.

To add additional processing resources, you can run the analytics on a readable secondary. Using a readable secondary separates the processing of the transactional workload and the analytics workload.

For more information, see [Get started with Columnstore for real-time operational analytics](get-started-with-columnstore-for-real-time-operational-analytics.md)

For more information on choosing the best columnstore index, see Sunil Agarwal's blog [Which columnstore index is right for my workload?](/archive/blogs/sql_server_team/columnstore-index-which-columnstore-index-is-right-for-my-workload).

## Use table partitions for data management and query performance

Columnstore indexes support partitioning, which is a good way to manage and archive data. Partitioning also improves query performance by limiting operations to one or more partitions.

### Use partitions to make the data easier to manage

For large tables, the only practical way to manage ranges of data is by using partitions. The advantages of partitions for rowstore tables also apply to columnstore indexes.

For example, both rowstore and columnstore tables use partitions to:

- Control the size of incremental backups. You can back up partitions to separate filegroups and then mark them as read-only. By doing this, future backups skip the read-only filegroups. 
- Save storage costs by moving an older partition to less expensive storage. For example, you could use partition switching to move a partition to a less expensive storage location.
- Perform operations efficiently by limiting the operations to a partition. For example, you can target only the fragmented partitions for index maintenance.

Additionally, with a columnstore index, you use partitioning to:

- Save an additional 30% in storage costs. You can compress older partitions with the `COLUMNSTORE_ARCHIVE` compression options. Query performance might be slower, which could be acceptable if the partition is queried infrequently.

### Use partitions to improve query performance

By using partitions, you can limit your queries to scan only specific partitions, which limits the number of rows to scan. For example, if the index is partitioned by year and the query is analyzing data from last year, it only needs to scan the data in one partition.

### Use fewer partitions for a columnstore index

Unless you have a large enough data size, a columnstore index performs best with fewer partitions than what you might use for a rowstore index. If you don't have at least one million rows per partition, most of your rows might go to the deltastore where they don't receive the performance benefit of columnstore compression. For example, if you load one million rows into a table with 10 partitions and each partition receives 100,000 rows, all of the rows go to the delta rowgroups.

Example:

- Load 1,000,000 rows into one partition or a non-partitioned table. You get one compressed rowgroup with 1,000,000 rows. This is great for high data compression and fast query performance.
- Load 1,000,000 rows evenly into 10 partitions. Each partition gets 100,000 rows, which is less than the minimum threshold for columnstore compression. As a result the columnstore index could have 10 delta rowgroups with 100,000 rows in each. There are ways to force the delta rowgroups into the columnstore. However, if these are the only rows in the columnstore index, the compressed rowgroups are too small for best compression and query performance.

For more information about partitioning, see Sunil Agarwal's blog post, [Should I partition my columnstore index?](/archive/blogs/sqlserverstorageengine/columnstore-index-should-i-partition-my-columnstore-index).

## Choose the appropriate data compression method

The columnstore index offers two choices for data compression: columnstore compression and archive compression. You can choose the compression option when you create the index, or change it later with [ALTER INDEX ... REBUILD](../../t-sql/statements/alter-index-transact-sql.md).

### Use columnstore compression for best query performance

Columnstore compression typically achieves 10x better compression rates over rowstore indexes. It is the standard compression method for columnstore indexes and enables fast query performance.

### Use archive compression for best data compression

Archive compression is designed for maximum compression when query performance is not as important. It achieves higher data compression rates than columnstore compression, but it comes with a price. It takes longer to compress and decompress the data, so it is not well-suited for fast query performance.

## Use optimizations when you convert a rowstore table to a columnstore index

If your data is already in a rowstore table, you can use [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md) to convert the table to a clustered columnstore index. There are a couple optimizations that will improve query performance after the table is converted, described next.

### Use MAXDOP to improve rowgroup quality

You can configure the maximum number of processors for converting a heap or clustered B-tree index to a columnstore index. To configure the processors, use the maximum degree of parallelism option (MAXDOP).

If you have large amounts of data, MAXDOP `1` might be too slow.  Increasing MAXDOP to `4` works fine. If this results in a few rowgroups that do not have the optimal number of rows, you can run [ALTER INDEX REORGANIZE](../../t-sql/statements/alter-index-transact-sql.md) to merge them together in the background.

### Keep the sorted order of a B-tree index

Since the B-tree index already stores rows in a sorted order, preserving that order when the rows get compressed into the columnstore index can improve query performance.

The columnstore index does not sort the data, but it does use metadata to track the minimum and maximum values of each column segment in each rowgroup.  When scanning for a range of values, it can quickly compute when to skip the rowgroup. When the data is ordered, more rowgroups can be skipped.

To preserve the sorted order during conversion:

- Use [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md) with the DROP_EXISTING clause. This also preserves the name of the index. If you have scripts that already use the name of the rowstore index you don't need to update them.

    This example converts a clustered rowstore index on a table named `MyFactTable` to a clustered columnstore index. The index name, `ClusteredIndex_d473567f7ea04d7aafcac5364c241e09`, stays the same.

    ```sql
    CREATE CLUSTERED COLUMNSTORE INDEX ClusteredIndex_d473567f7ea04d7aafcac5364c241e09
    ON MyFactTable
    WITH (DROP_EXISTING = ON);
    ```

### Understand segment elimination

Each rowgroup contains one column segment for every column in the table. Each column segment is compressed together and stored on physical media.

There is metadata with each segment to allow for fast elimination of segments without reading them. Data type choices might have a significant impact on query performance based common filter predicates for queries on the columnstore index. For more information, see [segment elimination](columnstore-indexes-query-performance.md#segment-elimination).

## Related tasks

For a summary of common tasks for creating and maintaining columnstore indexes, see [Related tasks](columnstore-indexes-overview.md#related-tasks).

## Related content

- [What's new in columnstore indexes](columnstore-indexes-what-s-new.md)
- [Columnstore indexes in data warehousing](columnstore-indexes-data-warehouse.md)
- [Columnstore indexes - Query performance](columnstore-indexes-query-performance.md)
