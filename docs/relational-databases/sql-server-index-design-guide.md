---
title: Index Architecture and Design Guide
description: Learn about designing efficient indexes in SQL Server and Azure SQL to achieve good database and application performance. Read about index architecture and best practices.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/01/2025
ms.service: sql
ms.subservice: supportability
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "index design guide"
  - "index design guidance"
  - "guide, index design"
  - "guidance, index design"
  - "index internals"
  - "index architecture"
  - "sql server index internals"
  - "sql server index architecture"
  - "sql server index design guide"
  - "sql server index design guidance"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Index architecture and design guide

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Designing efficient indexes is key to achieving good database and application performance. A lack of indexes, over-indexing, or poorly designed indexes are top sources of database performance problems.

This guide describes index architecture and fundamentals, and provides best practices to help you design effective indexes to meet the needs of your applications.

For more information about available index types, see [Indexes](indexes/indexes.md).

This guide covers the following types of indexes:

| Primary storage format | Index type |
| --- | --- |
| **Disk-based rowstore** | |
| | Clustered |
| | Nonclustered |
| | Unique |
| | Filtered |
| **Columnstore** | |
| | Clustered columnstore |
| | Nonclustered columnstore |
| **Memory-optimized** | |
| | Hash |
| | Memory-optimized nonclustered |

For information about XML indexes, see [XML indexes (SQL Server)](xml/xml-indexes-sql-server.md) and [Selective XML indexes (SXI)](xml/selective-xml-indexes-sxi.md).

For information about spatial indexes, see [Spatial Indexes Overview](spatial/spatial-indexes-overview.md).

For information about full-text indexes, see [Populate Full-Text Indexes](search/populate-full-text-indexes.md).

## Index basics

Think about a regular book: at the end of the book, there's an index that helps to quickly locate information within the book. The index is a sorted list of keywords and next to each keyword is a set of page numbers pointing to the pages where each keyword can be found.

A rowstore index is similar: it's an ordered list of values and for each value there are pointers to the data [pages](pages-and-extents-architecture-guide.md) where these values are located. The index itself is also stored on pages, referred to as *index pages*. In a regular book, if the index spans multiple pages and you have to find pointers to all the pages that contain the word `SQL` for example, you would have to leaf through from the start of the index until you locate the index page that contains the keyword `SQL`. From there, you follow the pointers to all the book pages. This could be optimized further if at the very beginning of the index, you create a single page that contains an alphabetical list of where each letter can be found. For example: "A through D - page 121", "E through G - page 122" and so on. This extra page would eliminate the step of leafing through the index to find the starting place. Such a page doesn't exist in regular books, but it does exist in a rowstore index. This single page is referred to as the root page of the index. The root page is the starting page of the tree structure used by an index. Following the tree analogy, the end pages that contain pointers to the actual data are referred to as "leaf pages" of the tree.

An index is an on-disk or in-memory structure associated with a table or view that speeds retrieval of rows from the table or view. A rowstore index contains keys built from the values in one or more columns in the table or view. For rowstore indexes, these keys are stored in a tree structure (B+ tree) that enables the [!INCLUDE [ssde-md](../includes/ssde-md.md)] to find the rows associated with the key values quickly and efficiently.

A rowstore index stores data logically organized as a table with rows and columns, and physically stored in a row-wise data format called *rowstore*<sup>1</sup>. There's an alternative way to store data column-wise, called *[columnstore](#columnstore-index-architecture)*.

The design of the right indexes for a database and its workload is a complex balancing act between query speed, index update cost, and storage cost. Narrow disk-based rowstore indexes, or indexes with few columns in the index key, require less storage space and a smaller update overhead. Wide indexes, on the other hand, might improve more queries. You might have to experiment with several different designs before finding the most efficient set of indexes. As the application evolves, indexes might need to change to maintain optimal performance. Indexes can be added, modified, and removed without affecting the database schema or application design. Therefore, you shouldn't hesitate to experiment with different indexes.

The query optimizer in the [!INCLUDE [ssde-md](../includes/ssde-md.md)] usually chooses the most effective indexes to execute a query. To see which indexes the query optimizer uses for a specific query, in [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)], on the **Query** menu, select **Display Estimated Execution Plan** or **Include Actual Execution Plan**.

Don't always equate index usage with good performance, and good performance with efficient index use. If using an index always helped produce the best performance, the job of the query optimizer would be simple. In reality, an incorrect index choice can cause less than optimal performance. Therefore, the task of the query optimizer is to select an index, or a combination of indexes, only when it improves performance, and to avoid indexed retrieval when it hinders performance.

A common design mistake is to create many indexes speculatively to "give the optimizer choices". The resulting overindexing slows down data modifications and can cause concurrency problems.

<sup>1</sup> Rowstore has been the traditional way to store relational table data. *Rowstore* refers to a table where the underlying data storage format is a heap, a B+ tree ([clustered index](#clustered-index-architecture)), or a memory-optimized table. *Disk-based rowstore* excludes memory-optimized tables.

### Index design tasks

The following tasks make up our recommended strategy for designing indexes:

1. **Understand the characteristics of the database and the application**.

   For example, in an online transaction processing (OLTP) database with frequent data modifications that must sustain a high throughput, a few narrow rowstore indexes targeted for the most critical queries would be a good initial index design. For extremely high throughput, consider memory-optimized tables and indexes, which provide a lock and latch-free design. For more information, see [Memory-optimized nonclustered index design guidelines](#memory-optimized-nonclustered-index-design-guidelines) and [Hash index design guidelines](#memory-optimized-hash-index-design-guidelines) in this guide.

   Conversely, for an analytics or data warehousing (OLAP) database that must process very large data sets quickly, using clustered columnstore indexes would be especially appropriate. For more information, see [Columnstore indexes: overview](indexes/columnstore-indexes-overview.md) or [Columnstore index architecture](#columnstore-index-architecture) in this guide.

1. **Understand the characteristics of the most frequently used queries**.

   For example, knowing that a frequently used query joins two or more tables helps you determine the set of indexes for these tables.

1. **Understand the data distribution in the columns used in the query predicates**.

   For example, an index might be useful for columns with many distinct data values, but less so for columns with many duplicate values. For columns with many NULLs or those that have well-defined subsets of data, you can use a filtered index. For more information, see [Filtered index design guidelines](#filtered-index-design-guidelines) in this guide.

1. **Determine which index options can enhance performance**.

   For example, creating a clustered index on an existing large table could benefit from the `ONLINE` index option. The `ONLINE` option allows for concurrent activity on the underlying data to continue while the index is being created or rebuilt. Using row or page [data compression](data-compression/data-compression.md) can improve performance by reducing the I/O and memory footprint of the index. For more information, see [CREATE INDEX](../t-sql/statements/create-index-transact-sql.md).

1. **Examine existing indexes on the table to prevent creating duplicate or very similar indexes**.

   It's often better to modify an existing index than to create a new but mostly duplicate index. For example, consider adding one or two extra included columns to an existing index, instead of creating a new index with these columns. This is particularly relevant when you [tune nonclustered indexes with missing index suggestions](indexes/tune-nonclustered-missing-index-suggestions.md), or if you use the [Database Engine Tuning Advisor](performance/database-engine-tuning-advisor.md), where you might be offered similar variations of indexes on the same table and columns.

<a id="General_Design"></a>

## General index design guidelines

Understanding the characteristics of your database, queries, and table columns can help you design optimal indexes initially and modify the design as your applications evolve.

### Database considerations

When you design an index, consider the following database guidelines:

- A large number of indexes on a table affect the performance of `INSERT`, `UPDATE`, `DELETE`, and `MERGE` statements because data in indexes might have to change as data in the table changes. For example, if a column is used in several indexes and you execute an `UPDATE` statement that modifies that column's data, each index that contains that column must be updated as well.

  - Avoid over-indexing heavily updated tables and keep indexes narrow, that is, with as few columns as possible.

  - You can have more indexes on tables that have few data modifications but large volumes of data. For such tables, a variety of indexes can help query performance while the index update overhead remains acceptable. However, don't create indexes speculatively. Monitor index usage, and remove unused indexes over time.

- Indexing small tables might not be optimal because it can take the [!INCLUDE [ssde-md](../includes/ssde-md.md)] longer to traverse the index searching for data than to perform a base table scan. Therefore, indexes on small tables might never be used, but must still be updated as the data in the table is updated.

- Indexes on views can provide significant performance gains when the view contains aggregations and/or joins. For more information, see [Create indexed views](views/create-indexed-views.md).

- Databases on primary replicas in Azure SQL Database automatically generate [database advisor performance recommendations](/azure/azure-sql/database/database-advisor-implement-performance-recommendations) for indexes. You can optionally [enable automatic index tuning](/azure/azure-sql/database/automatic-tuning-enable).

- [Query Store helps identify queries with suboptimal performance](performance/best-practice-with-the-query-store.md#start-with-query-performance-troubleshooting) and provides a history of [query execution plans](performance/execution-plans.md) that let you see the indexes selected by the optimizer. You can use this data to make your index tuning changes most impactful by focusing on the most frequent and resource consuming queries.

### Query considerations

When you design an index, consider the following query guidelines:

<a id="sargable"></a>

- Create nonclustered indexes on the columns that are frequently used in [predicates](../t-sql/queries/predicates.md) and join expressions in queries. These are your *SARGable* columns. However, you should avoid adding unnecessary column to indexes. Adding too many index columns can adversely affect disk space and index update performance.

  [!INCLUDE [search-argument](../includes/paragraph-content/search-argument.md)]

  > [!TIP]  
  > Always make sure that the indexes you create are actually used by the query workload. Drop unused indexes.
  >
  > Index usage statistics are available in [sys.dm_db_index_usage_stats](system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md) and [sys.dm_db_index_operational_stats](system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md).

- Covering indexes can improve query performance because all the data needed to meet the requirements of the query exists within the index itself. That is, only the index pages, and not the data pages of the table or clustered index, are required to retrieve the requested data; therefore, reducing overall disk I/O. For example, a query of columns `A` and `B` on a table that has a composite index created on columns `A`, `B`, and `C` can retrieve the specified data from the index alone.

  > [!NOTE]  
  > A *covering* index is a [nonclustered index](#nonclustered-index-architecture) that satisfies all data access by a query directly without accessing the base table.

  Such indexes have all the necessary *[SARGable](#sargable)* columns in the index key, and *non-SARGable* columns as [included](#use-included-columns-in-nonclustered-indexes) columns. This means that all the columns needed by the query, either in the `WHERE`, `JOIN`, and `GROUP BY` clauses, or in the `SELECT` or `UPDATE` clauses, are present in the index.

  There's potentially much less I/O to execute the query, if the index is narrow enough when compared to the rows and columns in the table itself, meaning it's a small subset of all columns.

  Consider covering indexes when retrieving a small portion of a large table, and where that small portion is defined by a fixed predicate.

  Avoid creating a covering index with too many columns because that diminishes its benefit while inflating database storage, I/O, and memory footprint.

- Write queries that insert or modify as many rows as possible in a single statement, instead of using multiple queries to update the same rows. This reduces the index update overhead.

### Column considerations

When you design an index consider the following column guidelines:

- Keep the length of the index key short, particularly for clustered indexes.

- Columns that are of the **ntext**, **text**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **json**, and **vector** data types can't be specified as index key columns. However, columns with these data types can be added to a nonclustered index as nonkey (included) index columns. For more information, see the section [Use included columns in nonclustered indexes](#use-included-columns-in-nonclustered-indexes) in this guide.

- Examine column uniqueness. A unique index instead of a nonunique index on the same key columns provides additional information for the query optimizer that makes the index more useful. For more information, see [Unique index design guidelines](#unique-index-design-guidelines) in this guide.

- Examine data distribution in the column. Creating an index on a column with many rows but few distinct values might not improve query performance even if the index is used by the query optimizer. As an analogy, a physical telephone directory sorted alphabetically on family name doesn't expedite locating a person if all people in the city are named Smith or Jones. For more information about data distribution, see [Statistics](statistics/statistics.md).

- Consider using filtered indexes on columns that have well-defined subsets, for example columns with many NULLs, columns with categories of values, and columns with distinct ranges of values. A well-designed filtered index can improve query performance, reduce index update costs, and reduce storage costs by storing a small subset of all rows in the table if that subset is relevant for many queries.

- Consider the order of the index key columns if the key contains multiple columns. The column that is used in the query predicate in an equality (`=`), inequality (`>`,`>=`,`<`,`<=`), or `BETWEEN` expression, or participates in a join, should be placed first. Additional columns should be ordered based on their level of distinctness, that is, from the most distinct to the least distinct.

  For example, if the index is defined as `LastName`, `FirstName`, the index is useful when the query predicate in the `WHERE` clause is `WHERE LastName = 'Smith'` or `WHERE LastName = Smith AND FirstName LIKE 'J%'`. However, the query optimizer wouldn't use the index for a query that searched only on `WHERE FirstName = 'Jane'`, or the index wouldn't improve the performance of such a query.

- Consider indexing computed columns if they are included in query predicates. For more information, see [Indexes on computed columns](indexes/indexes-on-computed-columns.md).

### Index characteristics

After you determine that an index is appropriate for a query, you can select the type of index that best fits your situation. Index characteristics include:

- Clustered or nonclustered
- Unique or nonunique
- Single column or multicolumn
- Ascending or descending order for the key columns in the index
- All rows or filtered, for nonclustered indexes
- Columnstore or rowstore
- Hash or nonclustered for memory-optimized tables

<a id="Index_placement"></a>

### Index placement on filegroups or partitions schemes

As you develop your index design strategy, you should consider the placement of the indexes on the filegroups associated with the database.

By default, indexes are stored in the same filegroup as the base table (clustered index or heap) on which the index is created. Other configurations are possible, including:

- Create nonclustered indexes on a filegroup other than the filegroup of the base table.

- Partition clustered and nonclustered indexes to span multiple filegroups.

For nonpartitioned tables, the simplest approach is usually the best: create all tables on the same filegroup, and add as many data files to the filegroup as necessary to utilize all available physical storage.

More advanced index placement approaches can be considered when tiered storage is available. For example, you could create a filegroup for frequently accessed tables with files on faster disks, and a filegroup for archive tables on slower disks.

You can move a table with a clustered index from one filegroup to another by dropping the clustered index and specifying a new filegroup or partition scheme in the `MOVE TO` clause of the `DROP INDEX` statement or by using the `CREATE INDEX` statement with the `DROP_EXISTING` clause.

#### Partitioned indexes

You can also consider partitioning disk-based heaps, clustered, and nonclustered indexes across multiple filegroups. Partitioned indexes are partitioned horizontally (by row), based on a partition function. The partition function defines how each row is mapped to a partition based on the values of a certain column you designate, called the partitioning column. A partition scheme specifies the mapping of a set of partitions to a filegroup.

Partitioning an index can provide the following benefits:

- Make large databases more manageable. OLAP systems, for example, can implement partition-aware ETL that greatly simplifies adding and removing data in bulk.

- Make certain types of queries, such as long-running analytical queries, run faster. When queries use a partitioned index, the [!INCLUDE [ssde-md](../includes/ssde-md.md)] can process multiple partitions at the same time and skip (eliminate) partitions that aren't needed by the query.

> [!WARNING]  
> Partitioning rarely improves query performance in OLTP systems, but it can introduce a significant overhead if a transactional query must access many partitions.

For more information, see [Partitioned tables and indexes](partitions/partitioned-tables-and-indexes.md).

### Index sort order design guidelines

When defining indexes, consider whether each index key column should be stored in ascending or descending order. Ascending is the default. The syntax of the `CREATE INDEX`, `CREATE TABLE`, and `ALTER TABLE` statements supports the keywords `ASC` (ascending) and `DESC` (descending) on individual columns in indexes and constraints.

Specifying the order in which key values are stored in an index is useful when queries referencing the table have `ORDER BY` clauses that specify different directions for the key column or columns in that index. In these cases, the index can remove the need for a **Sort** [operator](showplan-logical-and-physical-operators-reference.md) in the query plan.

For example, the buyers in the [!INCLUDE [ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] purchasing department have to evaluate the quality of products they purchase from vendors. The buyers are most interested in finding products sent by vendors with a high rejection rate.

As shown in the following query against the [AdventureWorks sample database](../samples/adventureworks-install-configure.md), retrieving the data to meet this criteria requires the `RejectedQty` column in the `Purchasing.PurchaseOrderDetail` table to be sorted in descending order (large to small) and the `ProductID` column to be sorted in ascending order (small to large).

```sql
SELECT RejectedQty,
       ((RejectedQty / OrderQty) * 100) AS RejectionRate,
       ProductID,
       DueDate
FROM Purchasing.PurchaseOrderDetail
ORDER BY RejectedQty DESC, ProductID ASC;
```

The following [execution plan](performance/display-an-actual-execution-plan.md) for this query shows that the query optimizer used a **Sort** operator to return the result set in the order specified by the `ORDER BY` clause.

:::image type="content" source="media/sql-server-index-design-guide/index-sort-part-1.png" alt-text="Diagram of an execution plan for this query showing that the query optimizer used a SORT operator to return the result set in the order specified by the ORDER BY clause.":::

If a disk-based rowstore index is created with key columns that match those in the `ORDER BY` clause in the query, the **Sort** operator in the query plan is eliminated, making query plan more efficient.

```sql
CREATE NONCLUSTERED INDEX IX_PurchaseOrderDetail_RejectedQty
ON Purchasing.PurchaseOrderDetail
    (RejectedQty DESC, ProductID ASC, DueDate, OrderQty);
```

After the query is executed again, the following execution plan shows that the **Sort** operator is no longer present and the newly created nonclustered index is used.

:::image type="content" source="media/sql-server-index-design-guide/index-sort-part-2.png" alt-text="Diagram of an execution plan showing that the SORT operator has been eliminated and the newly created nonclustered index is used.":::

The [!INCLUDE [ssDE](../includes/ssde-md.md)] can scan an index in either direction. An index defined as `RejectedQty DESC, ProductID ASC` can still be used for a query in which the sort directions of the columns in the `ORDER BY` clause are reversed. For example, a query with the `ORDER BY` clause `ORDER BY RejectedQty ASC, ProductID DESC` can use the same index.

Sort order can be specified only for the key columns in index. The [sys.index_columns](system-catalog-views/sys-index-columns-transact-sql.md) catalog view reports whether an index column is stored in ascending or descending order.

<a id="Clustered"></a>

## Clustered index design guidelines

The clustered index stores all rows and all columns of a table. Rows are sorted in the order of index key values. There can only be one clustered index per table.

The term **base table** can refer either to a clustered index or to a heap. A heap is an *unsorted* data structure on disk that contains all rows and all columns of a table.

With a few exceptions, every table should have a clustered index. The desirable properties of the clustered index are:

| Property | Description |
| --- | --- |
| **Narrow** | The clustered index key is a part of any nonclustered index on the same base table. A narrow key, or a key where the total length of key columns is small, reduces the storage, I/O, and memory overhead of all indexes on a table.<br /><br />To calculate the key length, add up the storage sizes for the data types used by key columns. For more information, see [Data type categories](../t-sql/data-types/data-types-transact-sql.md#data-type-categories). |
| **Unique** | If the clustered index isn't unique, a 4-byte internal uniqueifier column is automatically added to the index key to ensure uniqueness. Adding an existing unique column to the clustered index key avoids the storage, I/O, and memory overhead of the uniqueifier column in all indexes on a table. Additionally, the query optimizer can generate more efficient query plans when an index is unique. |
| **Ever-increasing** | In an ever-increasing index, data is always added on the last page of the index. This avoids page splits in the middle of the index, which reduce [page density](indexes/reorganize-and-rebuild-indexes.md#concepts-index-fragmentation-and-page-density) and decrease performance. |
| **Immutable** | The clustered index key is a part of any nonclustered index. When a key column of a clustered index is modified, a change must also be made in all nonclustered indexes, which adds a CPU, logging, I/O, and memory overhead. The overhead is avoided if the key columns of the clustered index are immutable. |
| **Has not nullable columns only** | If a row has nullable columns, it must include an internal structure called a *NULL block*, which adds 3-4 bytes of storage per row in an index. Making all columns of the clustered index not nullable avoids this overhead. |
| **Has fixed width columns only** | Columns using variable width data types such as **varchar** or **nvarchar** use an additional 2 bytes per value compared to fixed width data types. Using fixed width data types such as **int** avoids this overhead in all indexes on the table. |

Satisfying as many of these properties as possible when designing a clustered index makes not only the clustered index, but also all nonclustered indexes on the same table more efficient. Performance is improved by avoiding storage, I/O, and memory overheads.

For example, a clustered index key with a single **int** or **bigint** not nullable column has all of these properties if it's populated by an `IDENTITY` clause or a default constraint using a [sequence](sequence-numbers/sequence-numbers.md) and isn't updated after a row is inserted.

Conversely, a clustered index key with a single **uniqueidentifier** column is wider because it uses 16 bytes of storage instead of 4 bytes for **int** and 8 bytes for **bigint**, and doesn't satisfy the **ever-increasing** property unless the values are generated sequentially.

> [!TIP]  
> When you create a `PRIMARY KEY` constraint, a unique index supporting the constraint is created automatically. By default, this index is clustered; however, if this index doesn't satisfy the desired properties of the clustered index, you can create the constraint as nonclustered and create a different clustered index instead.

If you don't create a clustered index, the table is stored as a heap, which is generally not recommended.

### Clustered index architecture

Rowstore indexes are organized as B+ trees. Each page in an index B+ tree is called an index node. The top node of the B+ tree is called the root node. The bottom nodes in the index are called the leaf nodes. Any index levels between the root and the leaf nodes are collectively known as intermediate levels. In a clustered index, the leaf nodes contain the data pages of the underlying table. The root and intermediate level nodes contain index pages holding index rows. Each index row contains a key value and a pointer to either an intermediate level page in the B+ tree, or a data row in the leaf level of the index. The pages in each level of the index are linked in a doubly linked list.

Clustered indexes have one row in [sys.partitions](system-catalog-views/sys-partitions-transact-sql.md) for each partition used by the index, with `index_id = 1`. By default, a clustered index has a single partition. When a clustered index has multiple partitions, each partition has a separate B+ tree structure that contains the data for that specific partition. For example, if a clustered index has four partitions, there are four B+ tree structures, one in each partition.

Depending on the data types in the clustered index, each clustered index structure has one or more allocation units in which to store and manage the data for a specific partition. At a minimum, each clustered index has one `IN_ROW_DATA` allocation unit per partition. The clustered index also has one `LOB_DATA` allocation unit per partition if it contains large object (LOB) columns such as **nvarchar(max)**. It also has one `ROW_OVERFLOW_DATA` allocation unit per partition if it contains variable length columns that exceed the 8,060-byte row size limit.

The pages in the B+ tree structure are ordered on the value of the clustered index key. All inserts are made on the page where the key value in the inserted row fits in the ordering sequence among existing pages. Within a page, rows aren't necessarily stored in any physical order. However, the page maintains a logical ordering of rows using an internal structure called a *slot array*. Entries in the slot array are maintained in the index key order.

This illustration shows the structure of a clustered index in a single partition.

:::image type="content" source="media/sql-server-index-design-guide/clustered-index.png" alt-text="Diagram showing the structure of a clustered index in a single partition.":::

<a id="Nonclustered"></a>

## Nonclustered index design guidelines

The main difference between a clustered and a nonclustered index is that a nonclustered index contains a subset of the columns in the table, usually sorted differently from the clustered index. Optionally, a nonclustered index can be filtered, which means that it contains a subset of all rows in the table.

A disk-based rowstore nonclustered index contains the row locators that point to the storage location of the row in the base table. You can create multiple nonclustered indexes on a table or indexed view. Generally, nonclustered indexes should be designed to improve the performance of frequently used queries that would need to scan the base table otherwise.

Similar to the way you use an index in a book, the query optimizer searches for a data value by searching the nonclustered index to find the location of the data value in the table and then retrieves the data directly from that location. This makes nonclustered indexes the optimal choice for exact match queries because the index contains entries describing the exact location in the table of the data values being searched for in the queries.

For example, to query the `HumanResources.Employee` table for all employees that report to a specific manager, the query optimizer might use the nonclustered index `IX_Employee_ManagerID`; this has `ManagerID` as its first key column. Because the `ManagerID` values are ordered in the nonclustered index, the query optimizer can quickly find all entries in the index that match the specified `ManagerID` value. Each index entry points to the exact page and row in the base table where the corresponding data from all other columns can be retrieved. After the query optimizer finds all entries in the index, it can go directly to the exact page and row to retrieve the data instead of scanning the entire base table.

### Nonclustered index architecture

Disk-based rowstore nonclustered indexes have the same B+ tree structure as clustered indexes, except for the following differences:

- A nonclustered index doesn't necessarily contain all columns and rows of the table.

- The leaf level of a nonclustered index is made up of index pages instead of data pages. The index pages on the leaf level of a nonclustered index contain key columns. Optionally, they might also contain a subset of other columns in the table as [included columns](#use-included-columns-in-nonclustered-indexes), to avoid retrieving them from the base table.

The row locators in nonclustered index rows are either a pointer to a row or are a clustered index key for a row, described as follows:

- If the table has a clustered index, or the index is on an indexed view, the row locator is the clustered index key for the row.

- If the table is a heap, which means it doesn't have a clustered index, the row locator is a pointer to the row. The pointer is built from the file identifier (ID), page number, and number of the row on the page. The whole pointer is known as a Row ID (RID).

Row locators also ensure uniqueness for nonclustered index rows. The following table describes how the [!INCLUDE [ssde-md](../includes/ssde-md.md)] adds row locators to nonclustered indexes:

| Base table type | Nonclustered index type | Row locator |
| --- | --- | --- |
| **Heap** | | |
| | Nonunique | RID added to key columns |
| | Unique | RID added to included columns |
| **Unique clustered index** | | |
| | Nonunique | Clustered index keys added to key columns |
| | Unique | Clustered index keys added to included columns |
| **Non-unique clustered index** | | |
| | Nonunique | Clustered index keys and uniqueifier (when present) added to key columns |
| | Unique | Clustered index keys and uniqueifier (when present) added to included columns |

The [!INCLUDE [ssde-md](../includes/ssde-md.md)] never stores a given column more than once in a nonclustered index. The index key order specified by the user when they create a nonclustered index is always honored: any row locator columns that need to be added to the key of a nonclustered index are added at the end of the key, following the columns specified in the index definition. Clustered index key row locators in a nonclustered index can be used in query processing, regardless of whether they are explicitly specified in the index definition or added implicitly.

The following examples show how row locators are implemented in nonclustered indexes:

| Clustered index | Nonclustered index definition | Nonclustered index definition with row locators | Explanation |
| --- | --- | --- | --- |
| Unique clustered index with key columns (`A`, `B`, `C`) | Nonunique nonclustered index with key columns (`B`, `A`) and included columns (`E`, `G`) | Key columns (`B`, `A`, `C`) and included columns (`E`, `G`) | The nonclustered index is nonunique, so the row locator needs to be present in the index keys. Columns `B` and `A` from the row locator are already present, so only column `C` is added. Column `C` is added to the end of the key column list. |
| Unique clustered index with key column (`A`) | Nonunique nonclustered index with key columns (`B`, `C`) and included column (`A`) | Key columns (`B`, `C`, `A`) | The nonclustered index is nonunique, so the row locator is added to the key. Column `A` isn't already specified as a key column, so it's added to the end of the key column list. Column `A` is now in the key, so there's no need to store it as an included column. |
| Unique clustered index with key column (`A`, `B`) | Unique nonclustered index with key column (`C`) | Key column (`C`) and included columns (`A`, `B`) | The nonclustered index is unique, so the row locator is added to the included columns. |

Nonclustered indexes have one row in [sys.partitions](system-catalog-views/sys-partitions-transact-sql.md) for each partition used by the index, with `index_id > 1`. By default, a nonclustered index has a single partition. When a nonclustered index has multiple partitions, each partition has a B+ tree structure that contains the index rows for that specific partition. For example, if a nonclustered index has four partitions, there are four B+ tree structures, one in each partition.

Depending on the data types in the nonclustered index, each nonclustered index structure has one or more allocation units in which to store and manage the data for a specific partition. At a minimum, each nonclustered index has one `IN_ROW_DATA` allocation unit per partition that stores the index B+ tree pages. The nonclustered index also has one `LOB_DATA` allocation unit per partition if it contains large object (LOB) columns such as **nvarchar(max)**. Additionally, it has one `ROW_OVERFLOW_DATA` allocation unit per partition if it contains variable length columns that exceed the 8,060-byte row size limit.

The following illustration shows the structure of a nonclustered index in a single partition.

:::image type="content" source="media/sql-server-index-design-guide/non-clustered-index.png" alt-text="Diagram showing the structure of a nonclustered index in a single partition.":::

<a id="Included_Columns"></a>

### Use included columns in nonclustered indexes

In addition to key columns, a nonclustered index can also have nonkey columns stored in the leaf level. These nonkey columns are called included columns and are specified in the `INCLUDE` clause of the `CREATE INDEX` statement.

An index with included nonkey columns can significantly improve query performance when it covers the query, that is, when all columns used in the query are in the index either as key or nonkey columns. Performance gains are achieved because the [!INCLUDE [ssde-md](../includes/ssde-md.md)] can locate all the column values within the index; the base table isn't accessed, resulting in fewer disk I/O operations.

If a column must be retrieved by a query, but isn't used in the query predicates, aggregations, and sorts, add it as an included column and not as a key column. This has the following advantages:

- Included columns can use data types not allowed as index key columns.

- Included columns aren't considered by the [!INCLUDE [ssde-md](../includes/ssde-md.md)] when calculating the number of index key columns or index key size. With included columns, you aren't limited by the 900-byte maximum key size. You can create wider indexes that cover more queries.

- When you move a column from the index key to included columns, index build takes less time because the index sort operation becomes faster.

If the table has a clustered index, the column or columns defined in the clustered index key are automatically added to each nonunique nonclustered index on the table. It's not necessary to specify them either in the nonclustered index key or as included columns.

#### Guidelines for indexes with included columns

Consider the following guidelines when you design nonclustered indexes with included columns:

- Included columns can only be defined in nonclustered indexes on tables or indexed views.

- All data types are allowed except **text**, **ntext**, and **image**.

- Computed columns that are deterministic and either precise or imprecise can be included columns. For more information, see [Indexes on computed columns](indexes/indexes-on-computed-columns.md).

- As with key columns, computed columns derived from **image**, **ntext**, and **text** data types can be included columns as long as the computed column data type is allowed in an included column.

- Column names can't be specified in both the `INCLUDE` list and in the key column list.

- Column names can't be repeated in the `INCLUDE` list.

- At least one key column must be defined in an index. The maximum number of included columns is 1,023. This is the maximum number of table columns minus 1.

- Regardless of the presence of included columns, index key columns must follow the existing index size restrictions of 16 key columns maximum, and a total index key size of 900 bytes.

#### Design recommendations for indexes with included columns

Consider redesigning nonclustered indexes with a large index key size so that only columns used in query predicates, aggregations, and sorts are key columns. Make all other columns that cover the query included nonkey columns. In this way, you have all columns needed to cover the query, but the index key itself is small and efficient.

For example, assume that you want to design an index to cover the following query.

```sql
SELECT AddressLine1,
       AddressLine2,
       City,
       StateProvinceID,
       PostalCode
FROM Person.Address
WHERE PostalCode BETWEEN N'98000' AND N'99999';
```

To cover the query, each column must be defined in the index. Although you could define all columns as key columns, the key size would be 334 bytes. Because the only column used as search criteria is the `PostalCode` column, having a length of 30 bytes, a better index design would define `PostalCode` as the key column and include all other columns as nonkey columns.

The following statement creates an index with included columns to cover the query.

```sql
CREATE INDEX IX_Address_PostalCode
ON Person.Address (PostalCode)
    INCLUDE (AddressLine1, AddressLine2, City, StateProvinceID);
```

To validate that the index covers the query, create the index, then [display the estimated execution plan](performance/display-the-estimated-execution-plan.md). If the execution plan shows an **Index Seek** operator for the `IX_Address_PostalCode` index, the query is covered by the index.

#### Performance considerations for indexes with included columns

Avoid creating indexes with a very large number of included columns. Even though the index might be covering for more queries, its performance benefit is decreased because:

- Fewer index rows fit on a page. This increases disk I/O and reduces cache efficiency.

- More disk space is required to store the index. In particular, adding **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml** data types in included columns can significantly increase disk space requirements. This is because the column values are copied into the index leaf level. Therefore, they reside in both the index and the base table.

- Data modification performance decreases because many columns must be modified both in the based table and in the nonclustered index.

You have to determine whether the gains in query performance outweigh the decrease in data modification performance and the increase in disk space requirements.

<a id="Unique"></a>

## Unique index design guidelines

A unique index guarantees that the index key contains no duplicate values. Creating a unique index is only possible when uniqueness is a characteristic of the data itself. For example, if you want to make sure that the values in the `NationalIDNumber` column in the `HumanResources.Employee` table are unique, when the primary key is `EmployeeID`, create a `UNIQUE` constraint on the `NationalIDNumber` column. The constraint rejects any attempt to introduce rows with duplicate national ID numbers.

With multicolumn unique indexes, the index guarantees that each combination of values in the index key is unique. For example, if a unique index is created on a combination of `LastName`, `FirstName`, and `MiddleName` columns, no two rows in the table could have the same values for these columns.

Both clustered and nonclustered indexes can be unique. You can create a unique clustered index and multiple unique nonclustered indexes on the same table.

The benefits of unique indexes include:

- Business rules that require data uniqueness are enforced.
- Additional information helpful to the query optimizer is provided.

Creating a `PRIMARY KEY` or `UNIQUE` constraint automatically creates a unique index on the specified columns. There are no significant differences between creating a `UNIQUE` constraint and creating a unique index independent of a constraint. Data validation occurs in the same manner and the query optimizer doesn't differentiate between a unique index created by a constraint or manually created. However, you should create a `UNIQUE` or `PRIMARY KEY` constraint on the column when the enforcement of business rules is the goal. By doing this, the objective of the index is clear.

### Unique index considerations

- A unique index, `UNIQUE` constraint, or `PRIMARY KEY` constraint can't be created if duplicate key values exist in the data.

- If the data is unique and you want uniqueness enforced, creating a unique index instead of a nonunique index on the same combination of columns provides additional information for the query optimizer that can produce more efficient execution plans. Creating a `UNIQUE` constraint or a unique index is recommended in this case.

- A unique nonclustered index can contain included nonkey columns. For more information, see [Use included columns in nonclustered indexes](#use-included-columns-in-nonclustered-indexes).

- Unlike a `PRIMARY KEY` constraint, a `UNIQUE` constraint or a unique index can be created with a nullable column in the index key. For the purposes of uniqueness enforcement, two NULLs are considered equal. For example, this means that in a single-column unique index, the column can be NULL for one row in the table only.

<a id="Filtered"></a>

## Filtered index design guidelines

A filtered index is an optimized nonclustered index, especially suited for queries that require a small subset of data in the table. It uses a filter predicate in the index definition to index a portion of rows in the table. A well-designed filtered index can improve query performance, reduce index update costs, and reduce index storage costs compared with a full-table index.

Filtered indexes can provide the following advantages over full-table indexes:

- **Improved query performance and plan quality**

  A well-designed filtered index improves query performance and execution plan quality because it's smaller than a full-table nonclustered index. A filtered index has filtered [statistics](statistics/statistics.md), which are more accurate than full-table statistics because they cover only the rows in the filtered index.

- **Reduced index update costs**

  An index is updated only when data manipulation language (DML) statements affect the data in the index. A filtered index reduces index update costs compared with a full-table nonclustered index because it's smaller and is only updated when the data in the index is affected. It's possible to have a large number of filtered indexes, especially when they contain data that is affected infrequently. Similarly, if a filtered index contains only the frequently affected data, the smaller size of the index reduces the cost of updating statistics.

- **Reduced index storage costs**

  Creating a filtered index can reduce disk storage for nonclustered indexes when a full-table index isn't necessary. You might be able to replace a full-table nonclustered index with multiple filtered indexes without significantly increasing the storage requirements.

Filtered indexes are useful when columns contain well-defined subsets of data. Examples are:

- Columns that contain many NULLs.

- Heterogeneous columns that contain categories of data.

- Columns that contain ranges of values such as amounts, time, and dates.

Reduced update costs for filtered indexes are most noticeable when the number of rows in the index is small compared with a full-table index. If the filtered index includes most of the rows in the table, it could cost more to maintain than a full-table index. In this case, you should use a full-table index instead of a filtered index.

Filtered indexes are defined on one table and only support simple comparison operators. If you need a filter expression that has complex logic or references multiple tables, you should create an [indexed computed column](indexes/indexes-on-computed-columns.md) or an [indexed view](views/create-indexed-views.md).

### Filtered index design considerations

In order to design effective filtered indexes, it's important to understand what queries your application uses and how they relate to subsets of your data. Some examples of data that have well-defined subsets are columns with many NULLs, columns with heterogeneous categories of values and columns with distinct ranges of values.

The following design considerations give several scenarios for when a filtered index can provide advantages over full-table indexes.

#### Filtered indexes for subsets of data

When a column only has a few relevant values for queries, you can create a filtered index on the subset of values. For example, when the column is mostly NULL and the query requires only non-NULL values, you can create a filtered index containing the non-NULL rows.

For example, the [AdventureWorks sample database](../samples/adventureworks-install-configure.md) has a `Production.BillOfMaterials` table with 2,679 rows. The `EndDate` column has only 199 rows that contain a non-NULL value and the other 2480 rows contain NULL. The following filtered index covers queries that return the columns defined in the index and that require only rows with a non-NULL value for `EndDate`.

```sql
CREATE NONCLUSTERED INDEX FIBillOfMaterialsWithEndDate
ON Production.BillOfMaterials (ComponentID, StartDate)
    WHERE EndDate IS NOT NULL;
```

The filtered index `FIBillOfMaterialsWithEndDate` is valid for the following query. [Display the Estimated Execution Plan](performance/display-the-estimated-execution-plan.md) to determine if the query optimizer used the filtered index.

```sql
SELECT ProductAssemblyID,
       ComponentID,
       StartDate
FROM Production.BillOfMaterials
WHERE EndDate IS NOT NULL
      AND ComponentID = 5
      AND StartDate > '20080101';
```

For more information about how to create filtered indexes and how to define the filtered index predicate expression, see [Create filtered indexes](indexes/create-filtered-indexes.md).

#### Filtered indexes for heterogeneous data

When a table has heterogeneous data rows, you can create a filtered index for one or more categories of data.

For example, the products listed in the `Production.Product` table are each assigned to a `ProductSubcategoryID`, which are in turn associated with the product categories Bikes, Components, Clothing, or Accessories. These categories are heterogeneous because their column values in the `Production.Product` table aren't closely correlated. For example, the columns `Color`, `ReorderPoint`, `ListPrice`, `Weight`, `Class`, and `Style` have unique characteristics for each product category. Suppose that there are frequent queries for accessories, which have subcategories between 27 and 36 inclusive. You can improve the performance of queries for accessories by creating a filtered index on the accessories subcategories as shown in the following example.

```sql
CREATE NONCLUSTERED INDEX FIProductAccessories
ON Production.Product (ProductSubcategoryID, ListPrice)
INCLUDE (Name)
WHERE ProductSubcategoryID >= 27 AND ProductSubcategoryID <= 36;
```

The filtered index `FIProductAccessories` covers the following query because the query results are contained in the index and the query plan doesn't require accessing the base table. For example, the query predicate expression `ProductSubcategoryID = 33` is a subset of the filtered index predicate `ProductSubcategoryID >= 27` and `ProductSubcategoryID <= 36`, the `ProductSubcategoryID` and `ListPrice` columns in the query predicate are both key columns in the index, and name is stored in the leaf level of the index as an included column.

```sql
SELECT Name,
       ProductSubcategoryID,
       ListPrice
FROM Production.Product
WHERE ProductSubcategoryID = 33
      AND ListPrice > 25.00;
```

#### Key and included columns in filtered indexes

It's a best practice to add a small number of columns in a filtered index definition, only as necessary for the query optimizer to choose the filtered index for the query execution plan. The query optimizer can choose a filtered index for the query regardless of whether it does or doesn't cover the query. However, the query optimizer is more likely to choose a filtered index if it covers the query.

In some cases, a filtered index covers the query without including the columns in the filtered index expression as key or included columns in the filtered index definition. The following guidelines explain when a column in the filtered index expression should be a key or included column in the filtered index definition. The examples refer to the filtered index, `FIBillOfMaterialsWithEndDate` that was created previously.

A column in the filtered index expression doesn't need to be a key or included column in the filtered index definition if the filtered index expression is equivalent to the query predicate and the query doesn't return the column in the filtered index expression with the query results. For example, `FIBillOfMaterialsWithEndDate` covers the following query because the query predicate is equivalent to the filter expression, and `EndDate` isn't returned with the query results. The `FIBillOfMaterialsWithEndDate` index doesn't need `EndDate` as a key or included column in the filtered index definition.

```sql
SELECT ComponentID,
       StartDate
FROM Production.BillOfMaterials
WHERE EndDate IS NOT NULL;
```

A column in the filtered index expression should be a key or included column in the filtered index definition if the query predicate uses the column in a comparison that isn't equivalent to the filtered index expression. For example, `FIBillOfMaterialsWithEndDate` is valid for the following query because it selects a subset of rows from the filtered index. However, it doesn't cover the following query because `EndDate` is used in the comparison `EndDate > '20040101'`, which isn't equivalent to the filtered index expression. The query processor can't execute this query without examining the values of `EndDate`. Therefore, `EndDate` should be a key or included column in the filtered index definition.

```sql
SELECT ComponentID,
       StartDate
FROM Production.BillOfMaterials
WHERE EndDate > '20040101';
```

A column in the filtered index expression should be a key or included column in the filtered index definition if the column is in the query result set. For example, `FIBillOfMaterialsWithEndDate` doesn't cover the following query because it returns the `EndDate` column in the query results. Therefore, `EndDate` should be a key or included column in the filtered index definition.

```sql
SELECT ComponentID,
       StartDate,
       EndDate
FROM Production.BillOfMaterials
WHERE EndDate IS NOT NULL;
```

The clustered index key of the table doesn't need to be a key or included column in the filtered index definition. The clustered index key is automatically included in all nonclustered indexes, including filtered indexes.

#### Data conversion operators in the filter predicate

If the comparison operator specified in the filtered index expression of the filtered index results in an implicit or explicit data conversion, an error occurs if the conversion occurs on the left side of a comparison operator. A solution is to write the filtered index expression with the data conversion operator (`CAST` or `CONVERT`) on the right side of the comparison operator.

The following example creates a table with columns of different data types.

```sql
CREATE TABLE dbo.TestTable
(
    a INT,
    b VARBINARY(4)
);
```

In the following filtered index definition, column `b` is implicitly converted to an integer data type for comparing it to the constant 1. This generates error message 10611 because the conversion occurs on the left-hand side of the operator in the filtered predicate.

```sql
CREATE NONCLUSTERED INDEX TestTabIndex
ON dbo.TestTable (a, b)
    WHERE b = 1;
```

The solution is to convert the constant on the right-hand side to be of the same type as column `b`, as seen in the following example:

```sql
CREATE INDEX TestTabIndex
ON dbo.TestTable (a, b)
    WHERE b = CONVERT (VARBINARY(4), 1);
```

Moving the data conversion from the left side to the right side of a comparison operator might change the meaning of the conversion. In the previous example, when the `CONVERT` operator was added to the right side, the comparison changed from an **int** comparison to a **varbinary** comparison.

<a id="columnstore_index"></a>

## Columnstore index architecture

A *columnstore index* is a technology for storing, retrieving, and managing data by using a columnar data format, called columnstore. For more information, see [Columnstore indexes: overview](indexes/columnstore-indexes-overview.md).

For version information and to find out what's new, visit [What's new in columnstore indexes](indexes/columnstore-indexes-what-s-new.md).

Knowing these basics makes it easier to understand other columnstore articles that explain how to use this technology effectively.

### Data storage uses columnstore and rowstore

When discussing columnstore indexes, we use the terms *rowstore* and *columnstore* to emphasize the format for the data storage. Columnstore indexes use both types of storage.

:::image type="content" source="media/sql-server-index-design-guide/physical-storage.png" alt-text="Diagram of a clustered columnstore index.":::

- A **columnstore** is data that is logically organized as a table with rows and columns, and physically stored in a column-wise data format.

  A columnstore index physically stores most of the data in columnstore format. In columnstore format, the data is compressed and uncompressed as columns. There's no need to uncompress other values in each row that aren't requested by the query. This makes it fast to scan an entire column of a large table.

- A **rowstore** is data that is logically organized as a table with rows and columns, and then physically stored in a row-wise data format. This has been the traditional way to store relational table data such as a clustered B+ tree index or a heap.

  A columnstore index also physically stores some rows in a rowstore format called a **deltastore**. The deltastore, also called delta rowgroups, is a holding place for rows that are too few in number to qualify for compression into the columnstore. Each delta rowgroup is implemented as a clustered B+ tree index, which is a rowstore.

### Operations are performed on rowgroups and column segments

The columnstore index groups rows into manageable units. Each of these units is called a **rowgroup**. For best performance, the number of rows in a rowgroup is large enough to improve the compression ratio and small enough to benefit from in memory operations.

For example, the columnstore index performs these operations on rowgroups:

- Compresses rowgroups into the columnstore. Compression is performed on each column segment within a rowgroup.

- Merges rowgroups during an `ALTER INDEX ... REORGANIZE` operation, including removal of deleted data.

- Recreates all rowgroups during an `ALTER INDEX ... REBUILD` operation.

- Reports on rowgroup health and fragmentation in the dynamic management views (DMVs).

The deltastore is comprised of one or more rowgroups called **delta rowgroups**. Each delta rowgroup is a clustered B+ tree index that stores small bulk loads and inserts until the rowgroup contains 1,048,576 rows, at which time a process called the **tuple-mover** automatically compresses a closed rowgroup into the columnstore.

For more information about rowgroup statuses, see [sys.dm_db_column_store_row_group_physical_stats](system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md).

> [!TIP]  
> Having too many small rowgroups decreases the columnstore index quality. A reorganize operation merges smaller rowgroups, following an internal threshold policy that determines how to remove deleted rows and combine the compressed rowgroups. After a merge, the index quality is improved.

In [!INCLUDE [sql-server-2019](../includes/sssql19-md.md)] and later versions, the tuple-mover is helped by a background merge task that automatically compresses smaller open delta rowgroups that have existed for some time as determined by an internal threshold, or merges compressed rowgroups from which a large number of rows has been deleted.

Each column has some of its values in each rowgroup. These values are called **column segments**. Each rowgroup contains one column segment for every column in the table. Each column has one column segment in each rowgroup.

:::image type="content" source="media/sql-server-index-design-guide/column-segment.png" alt-text="Diagram of a clustered columnstore column segment.":::

When the columnstore index compresses a rowgroup, it compresses each column segment separately. To uncompress an entire column, the columnstore index only needs to uncompress one column segment from each rowgroup.

### Small loads and inserts go to the deltastore

A columnstore index improves columnstore compression and performance by compressing at least 102,400 rows at a time into the columnstore index. To compress rows in bulk, the columnstore index accumulates small loads and inserts in the deltastore. The deltastore operations are handled in the background. To return query results, the clustered columnstore index combines query results from both the columnstore and the deltastore.

Rows go to the deltastore when they are:

- Inserted with the `INSERT INTO ... VALUES` statement.

- At the end of a bulk load, and they number less than 102,400.

- Updated. Each update is implemented as a delete and an insert.

The deltastore also stores a list of IDs for deleted rows that have been marked as deleted but not yet physically deleted from the columnstore.

### When delta rowgroups are full, they get compressed into the columnstore

Clustered columnstore indexes collect up to 1,048,576 rows in each delta rowgroup before compressing the rowgroup into the columnstore. This improves the compression of the columnstore index. When a delta rowgroup reaches the maximum number of rows, it transitions from an `OPEN` to `CLOSED` state. A background process named the tuple-mover checks for closed row groups. If the process finds a closed rowgroup, it compresses the rowgroup and stores it into the columnstore.

When a delta rowgroup has been compressed, the existing delta rowgroup transitions into `TOMBSTONE` state to be removed later by the tuple-mover when there's no reference to it, and the new compressed rowgroup is marked as `COMPRESSED`.

For more information about rowgroup statuses, see [sys.dm_db_column_store_row_group_physical_stats](system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md).

You can force delta rowgroups into the columnstore by using [ALTER INDEX](../t-sql/statements/alter-index-transact-sql.md) to rebuild or reorganize the index. If there's memory pressure during compression, the columnstore index might reduce the number of rows in the compressed rowgroup.

### Each table partition has its own rowgroups and delta rowgroups

The concept of partitioning is the same in a clustered index, a heap, and a columnstore index. Partitioning a table divides the table into smaller groups of rows according to a range of column values. It's often used for managing the data. For example, you could create a partition for each year of data, and then use partition switching to archive old data to less expensive storage.

Rowgroups are always defined within a table partition. When a columnstore index is partitioned, each partition has its own compressed rowgroups and delta rowgroups. A nonpartitioned table contains one partition.

> [!TIP]  
> Consider using table partitioning if there's a need to remove data from the columnstore. Switching out and truncating partitions that aren't needed anymore is an efficient strategy to delete data without introducing fragmentation in the columnstore.

### Each partition can have multiple delta rowgroups

Each partition can have more than one delta rowgroups. When the columnstore index needs to add data to a delta rowgroup and the delta rowgroup is locked by another transaction, the columnstore index tries to obtain a lock on a different delta rowgroup. If there are no delta rowgroups available, the columnstore index creates a new delta rowgroup. For example, a table with 10 partitions could easily have 20 or more delta rowgroups.

### Combine columnstore and rowstore indexes on the same table

A nonclustered index contains a copy of part or all of the rows and columns in the underlying table. The index is defined as one or more columns of the table, and has an optional condition that filters the rows.

You can create an updatable *nonclustered columnstore index on a rowstore table*. The columnstore index stores a copy of the data so you do need extra storage. However, the data in the columnstore index compresses to a much smaller size than the rowstore table requires. By doing this, you can run analytics on the columnstore index and OLTP workloads on the rowstore index at the same time. The columnstore is updated when data changes in the rowstore table, so both indexes are working against the same data.

A rowstore table can have one nonclustered columnstore index. For more information, see [Columnstore indexes - design guidance](indexes/columnstore-indexes-design-guidance.md).

You can have *one or more nonclustered rowstore indexes on a clustered columnstore table*. By doing this, you can perform efficient table seeks on the underlying columnstore. Other options become available too. For example, you can enforce uniqueness by using a `UNIQUE` constraint on the rowstore table. When a nonunique value fails to insert into the rowstore table, the [!INCLUDE [ssde-md](../includes/ssde-md.md)] doesn't insert the value into the columnstore either.

### Nonclustered columnstore performance considerations

The nonclustered columnstore index definition supports using a filtered condition. To minimize the performance effect of adding a columnstore index, use a filter expression to create a nonclustered columnstore index on only the subset of data required for analytics.

A memory-optimized table can have one columnstore index. You can create it when the table is created or add it later with [ALTER TABLE](../t-sql/statements/alter-table-transact-sql.md).

For more information, see [Columnstore indexes - query performance](indexes/columnstore-indexes-query-performance.md).

<a id="hash_index"></a>

## Memory-optimized hash index design guidelines

When using [In-Memory OLTP](in-memory-oltp/overview-and-usage-scenarios.md), all memory-optimized tables must have at least one index. For a memory-optimized table, every index is also memory-optimized. Hash indexes are one of the possible index types in a memory-optimized table. For more information, see [Indexes on Memory-Optimized Tables](in-memory-oltp/indexes-for-memory-optimized-tables.md).

### Memory-optimized hash index architecture

A hash index consists of an array of pointers, and each element of the array is called a hash bucket.

- Each bucket is 8 bytes, which are used to store the memory address of a link list of key entries.
- Each entry is a value for an index key, plus the address of its corresponding row in the underlying memory-optimized table.
- Each entry points to the next entry in a link list of entries, all chained to the current bucket.

The number of buckets must be specified at index creation time:

- The lower the ratio of buckets to table rows or to distinct values, the longer the average bucket link list is.
- Short link lists perform faster than long link lists.
- The maximum number of buckets in hash indexes is 1,073,741,824.

> [!TIP]  
> To determine the right `BUCKET_COUNT` for your data, see [Configure the hash index bucket count](#configure-the-hash-index-bucket-count).

The hash function is applied to the index key columns and the result of the function determines what bucket that key falls into. Each bucket has a pointer to rows whose hashed key values are mapped to that bucket.

The hashing function used for hash indexes has the following characteristics:

- The [!INCLUDE [ssde-md](../includes/ssde-md.md)] has one hash function that is used for all hash indexes.
- The hash function is deterministic. The same input key value is always mapped to the same bucket in the hash index.
- Multiple index keys might be mapped to the same hash bucket.
- The hash function is balanced, meaning that the distribution of index key values over hash buckets typically follows a Poisson or bell curve distribution, not a flat linear distribution.
- Poisson distribution isn't an even distribution. Index key values aren't evenly distributed in the hash buckets.
- If two index keys are mapped to the same hash bucket, there's a *hash collision*. A large number of hash collisions can have a performance effect on read operations. A realistic goal is for 30 percent of the buckets to contain two different key values.

The interplay of the hash index and the buckets is summarized in the following image.

:::image type="content" source="media/sql-server-index-design-guide/hash-index-buckets.png" alt-text="Diagram showing interaction between hash index and buckets.":::

### Configure the hash index bucket count

The hash index bucket count is specified at index create time, and can be changed using the `ALTER TABLE...ALTER INDEX REBUILD` syntax.

In most cases, the bucket count should be between 1 and 2 times the number of distinct values in the index key.
You might not always be able to predict how many values a particular index key has. Performance is usually still good if the `BUCKET_COUNT` value is within 10 times of the actual number of key values, and overestimating is generally better than underestimating.

Too *few* buckets can have the following drawbacks:

- More hash collisions of distinct key values.
- Each distinct value is forced to share the same bucket with a different distinct value.
- The average chain length per bucket grows.
- The longer the bucket chain, the slower the speed of equality lookups in the index.

Too *many* buckets can have the following drawbacks:

- Too high a bucket count can result in more empty buckets.
- Empty buckets affect the performance of full index scans. If scans are performed regularly, consider picking a bucket count close to the number of distinct index key values.
- Empty buckets use memory, though each bucket uses only 8 bytes.

> [!NOTE]  
> Adding more buckets does nothing to reduce the chaining together of entries that share a duplicate value. The rate of value duplication is used to decide whether a hash index or a nonclustered index is the appropriate index type, not to calculate the bucket count.

### Performance considerations for hash indexes

The performance of a hash index is:

- Excellent when the predicate in the `WHERE` clause specifies an *exact* value for each column in the hash index key. A hash index reverts to a scan given an inequality predicate.
- Poor when the predicate in the `WHERE` clause looks for a *range* of values in the index key.
- Poor when the predicate in the `WHERE` clause stipulates one specific value for the *first* column of a two column hash index key, but doesn't specify a value for *other* columns of the key.

> [!TIP]  
> The predicate must include *all* columns in the hash index key. The hash index requires the entire key to seek into the index.

If a hash index is used, and the number of unique index keys is more than 100 times smaller than the row count, consider either increasing to a larger bucket count to avoid large row chains, or use a [nonclustered index](#memory-optimized-nonclustered-index-architecture) instead.

### Create a hash index

When creating a hash index, consider:

- A hash index can exist only on a memory-optimized table. It can't exist on a disk-based table.
- A hash index is nonunique by default, but can be declared as unique.

The following example creates a unique hash index:

```sql
ALTER TABLE MyTable_memop ADD INDEX ix_hash_Column2
    UNIQUE HASH (Column2) WITH (BUCKET_COUNT = 64);
```

### Row versions and garbage collection in memory-optimized tables

In a memory-optimized table, when a row is affected by an `UPDATE` statement, the table creates an updated version of the row. During the update transaction, other sessions might be able to read the older version of the row, and so avoid the performance slowdown associated with a row lock.

The hash index could also have different versions of its entries to accommodate the update.

Later when the older versions are no longer needed, a garbage collection (GC) thread traverses the buckets and their link lists to clean away old entries. The GC thread performs better if the link list chain lengths are short. For more information, see [In-Memory OLTP Garbage Collection](in-memory-oltp/in-memory-oltp-garbage-collection.md).

<a id="inmem_nonclustered_index"></a>

## Memory-optimized nonclustered index design guidelines

Besides hash indexes, nonclustered indexes are the other possible index types in a memory-optimized table. For more information, see [Indexes on Memory-Optimized Tables](in-memory-oltp/indexes-for-memory-optimized-tables.md).

### Memory-optimized nonclustered index architecture

Nonclustered indexes on memory-optimized tables are implemented using a data structure called a Bw-tree, originally envisioned and described by Microsoft Research in 2011. A Bw-tree is a lock and latch-free variation of a B-tree. For more information, see [The Bw-tree: A B-tree for New Hardware Platforms](https://www.microsoft.com/research/publication/the-bw-tree-a-b-tree-for-new-hardware).

At a high level, the Bw-tree can be understood as a map of pages organized by page ID (PidMap), a facility to allocate and reuse page IDs (PidAlloc) and a set of pages linked in the page map and to each other. These three high level subcomponents make up the basic internal structure of a Bw-tree.

The structure is similar to a normal B-tree in the sense that each page has a set of key values that are ordered and there are levels in the index each pointing to a lower level and the leaf levels point to a data row. However there are several differences.

Just like with hash indexes, multiple data rows can be linked together to support versioning. The page pointers between the levels are logical page IDs, which are offsets into a page mapping table, that in turn has the physical address for each page.

There are no in-place updates of index pages. New delta pages are introduced for this purpose.

- No latching or locking is required for page updates.
- Index pages aren't a fixed size.

The key value in each nonleaf level page is the highest value that the child that it points to contains, and each row also contains that page logical page ID. On the leaf-level pages, along with the key value, it contains the physical address of the data row.

Point lookups are similar to B-trees, except that because pages are linked in only one direction, the [!INCLUDE [ssde-md](../includes/ssde-md.md)] follows right page pointers, where each nonleaf page has the highest value of its child, rather than lowest value as in a B-tree.

If a leaf-level page has to change, the [!INCLUDE [ssde-md](../includes/ssde-md.md)] doesn't modify the page itself. Rather, the [!INCLUDE [ssde-md](../includes/ssde-md.md)] creates a delta record that describes the change, and appends it to the previous page. Then it also updates the page map table address for that previous page, to the address of the delta record that now becomes the physical address for this page.

There are three different operations that can be required for managing the structure of a Bw-tree: consolidation, split, and merge.

#### Delta consolidation

A long chain of delta records can eventually degrade search performance as it could require long chain traversal when searching through an index. If a new delta record is added to a chain that already has 16 elements, the changes in the delta records are consolidated into the referenced index page, and the page is then rebuilt, including the changes indicated by the new delta record that triggered the consolidation. The newly rebuilt page has the same page ID but a new memory address.

:::image type="content" source="media/sql-server-index-design-guide/page-mapping-table.png" alt-text="Diagram showing the memory-optimized page mapping table.":::

#### Split page

An index page in Bw-tree grows on as-needed basis starting from storing a single row to storing a maximum of 8 KB. Once the index page grows to 8 KB, a new insert of a single row causes the index page to split. For an internal page, this means when there's no more room to add another key value and pointer, and for a leaf page, it means that the row would be too large to fit on the page once all the delta records are incorporated. The statistics information in the page header for a leaf page keeps track of how much space is required to consolidate the delta records. This information is adjusted as each new delta record is added.

A split operation is done in two atomic steps. In the following diagram, assume a leaf-page forces a split because a key with value 5 is being inserted, and a nonleaf page exists pointing to the end of the current leaf-level page (key value 4).

:::image type="content" source="media/sql-server-index-design-guide/split-operation.png" alt-text="Diagram showing a memory-optimized index split operation." lightbox="media/sql-server-index-design-guide/split-operation.png":::

**Step 1:** Allocate two new pages `P1` and `P2`, and split the rows from old `P1` page onto these new pages, including the newly inserted row. A new slot in the *page mapping table* is used to store the physical address of page `P2`. Pages `P1` and `P2` aren't accessible to any concurrent operations yet. In addition, the logical pointer from `P1` to `P2` is set. Then, in one atomic step update the page mapping table to change the pointer from old `P1` to new `P1`.

**Step 2:** The nonleaf page points to `P1` but there's no direct pointer from a nonleaf page to `P2`. `P2` is only reachable via `P1`. To create a pointer from a nonleaf page to `P2`, allocate a new nonleaf page (internal index page), copy all the rows from old nonleaf page, and add a new row to point to `P2`. Once this is done, in one atomic step, update the page mapping table to change the pointer from old nonleaf page to new nonleaf page.

#### Merge page

When a `DELETE` operation results in a page having less than 10 percent of the maximum page size (8 KB), or with a single row on it, that page is merged with a contiguous page.

When a row is deleted from a page, a delta record for the delete is added. Additionally, a check is made to determine if the index page (nonleaf page) qualifies for merge. This check verifies if the remaining space after deleting the row is less than 10 percent of maximum page size. If it does qualify, the merge is performed in three atomic steps.

In the following image, assume a `DELETE` operation deletes the key value 10.

:::image type="content" source="media/sql-server-index-design-guide/merge-operation.png" alt-text="Diagram showing a memory-optimized index merge operation." lightbox="media/sql-server-index-design-guide/merge-operation.png":::

**Step 1:** A delta page representing key value `10` (blue triangle) is created and its pointer in the nonleaf page `Pp1` is set to the new delta page. Additionally a special merge-delta page (green triangle) is created, and it's linked to point to the delta page. At this stage, both pages (delta page and merge-delta page) aren't visible to any concurrent transaction. In one atomic step, the pointer to the leaf-level page `P1` in the page mapping table is updated to point to the merge-delta page. After this step, the entry for key value `10` in `Pp1` now points to the merge-delta page.

**Step 2:** The row representing key value `7` in the nonleaf page `Pp1` needs to be removed, and the entry for key value `10` updated to point to `P1`. To do this, a new nonleaf page `Pp2` is allocated and all the rows from `Pp1` are copied, except for the row representing key value `7`; then the row for key value `10` is updated to point to page `P1`. Once this is done, in one atomic step, the page mapping table entry pointing to `Pp1` is updated to point to `Pp2`. `Pp1` is no longer reachable.

**Step 3:** The leaf-level pages `P2` and `P1` are merged and the delta pages removed. To do this, a new page `P3` is allocated and the rows from `P2` and `P1` are merged, and the delta page changes are included in the new `P3`. Then, in one atomic step, the page mapping table entry pointing to page `P1` is updated to point to page `P3`.

### Performance considerations for memory-optimized nonclustered indexes

The performance of a nonclustered index is better than with hash indexes when querying a memory-optimized table with inequality predicates.

A column in a memory-optimized table can be part of both a hash index and a nonclustered index.

When a key column in a nonclustered index has many duplicate values, performance can degrade for updates, inserts, and deletes. One way to improve performance in this situation is to add a column that has better selectivity in the index key.

## Index metadata

To examine index metadata such as index definitions, properties, and data statistics, use the following system views:

- [sys.objects](system-catalog-views/sys-objects-transact-sql.md)
- [sys.indexes](system-catalog-views/sys-indexes-transact-sql.md)
- [sys.index_columns](system-catalog-views/sys-index-columns-transact-sql.md)
- [sys.columns](system-catalog-views/sys-columns-transact-sql.md)
- [sys.types](system-catalog-views/sys-types-transact-sql.md)
- [sys.partitions](system-catalog-views/sys-partitions-transact-sql.md)
- [sys.internal_partitions](system-catalog-views/sys-internal-partitions-transact-sql.md)
- [sys.dm_db_index_usage_stats](system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md)
- [sys.dm_db_partition_stats](system-dynamic-management-views/sys-dm-db-partition-stats-transact-sql.md)
- [sys.dm_db_index_operational_stats](system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)

The previous views apply to all index types. For columnstore indexes, additionally use the following views:

- [sys.column_store_row_groups](system-catalog-views/sys-column-store-row-groups-transact-sql.md)
- [sys.column_store_segments](system-catalog-views/sys-column-store-segments-transact-sql.md)
- [sys.column_store_dictionaries](system-catalog-views/sys-column-store-dictionaries-transact-sql.md)
- [sys.dm_column_store_object_pool](system-dynamic-management-views/sys-dm-column-store-object-pool-transact-sql.md)
- [sys.dm_db_column_store_row_group_operational_stats](system-dynamic-management-views/sys-dm-db-column-store-row-group-operational-stats-transact-sql.md)
- [sys.dm_db_column_store_row_group_physical_stats](system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md)

For columnstore indexes, all columns are stored in the metadata as included columns. The columnstore index doesn't have key columns.

For indexes on memory-optimized tables, additionally use the following views:

- [sys.hash_indexes](system-catalog-views/sys-hash-indexes-transact-sql.md)
- [sys.dm_db_xtp_hash_index_stats](system-dynamic-management-views/sys-dm-db-xtp-hash-index-stats-transact-sql.md)
- [sys.dm_db_xtp_index_stats](system-dynamic-management-views/sys-dm-db-xtp-index-stats-transact-sql.md)
- [sys.dm_db_xtp_nonclustered_index_stats](system-dynamic-management-views/sys-dm-db-xtp-nonclustered-index-stats-transact-sql.md)
- [sys.dm_db_xtp_object_stats](system-dynamic-management-views/sys-dm-db-xtp-object-stats-transact-sql.md)
- [sys.dm_db_xtp_table_memory_stats](system-dynamic-management-views/sys-dm-db-xtp-table-memory-stats-transact-sql.md)
- [sys.memory_optimized_tables_internal_attributes](system-catalog-views/sys-memory-optimized-tables-internal-attributes-transact-sql.md)

## Related content

- [CREATE INDEX (Transact-SQL)](../t-sql/statements/create-index-transact-sql.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](indexes/reorganize-and-rebuild-indexes.md)
- [Partitioned tables and indexes](partitions/partitioned-tables-and-indexes.md)
- [Indexes on Memory-Optimized Tables](in-memory-oltp/indexes-for-memory-optimized-tables.md)
- [Columnstore indexes: overview](indexes/columnstore-indexes-overview.md)
- [Indexes on computed columns](indexes/indexes-on-computed-columns.md)
- [Tune nonclustered indexes with missing index suggestions](indexes/tune-nonclustered-missing-index-suggestions.md)
