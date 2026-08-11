---
title: Heaps (Tables Without Clustered Indexes)
description: Heaps are tables without clustered indexes in SQL Server. Learn when to use a heap, when to avoid one, and how to create, rebuild, and identify heaps.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/10/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "heaps"
  - "forward record"
  - "forwarded record"
  - "forwarding pointer"
  - "RID"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Heaps (tables without clustered indexes)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

A heap is a table without a clustered index. You can create one or more nonclustered indexes on tables stored as a heap. The heap stores data without specifying an order. Usually, the heap initially stores data in the order you insert the rows. However, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] can move data around in the heap to store the rows efficiently. In query results, you can't predict the data order. To guarantee the order of rows returned from a heap, use the `ORDER BY` clause. To specify a permanent logical order for storing the rows, create a clustered index on the table, so that the table isn't a heap.

> [!NOTE]  
> Sometimes, good reasons exist to leave a table as a heap instead of creating a clustered index. However, using heaps effectively is an advanced skill. Most tables should have a carefully chosen clustered index unless a good reason exists for leaving the table as a heap.

## When to use a heap

A heap is ideal for tables that you frequently truncate and reload. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] optimizes space in a heap by filling the earliest available space.

Consider the following:

- Locating free space in a heap can be costly, especially if many deletes or updates occur.
- Clustered indexes offer steady performance for tables that you don't frequently truncate.

For tables that you regularly truncate or recreate, such as temporary or staging tables, using a heap is often more efficient.

The choice between using a heap and a clustered index can significantly affect your database's performance and efficiency.

When you store a table as a heap, you identify individual rows by reference to an 8-byte row identifier (RID) consisting of the file number, data page number, and slot on the page (FileID:PageID:SlotID). The row ID is a small and efficient structure.

Use heaps as staging tables for large, unordered insert operations. Because heaps don't enforce a strict insertion order, the insert operation is usually faster than an equivalent insert into a clustered index. If you read and process the heap's data into a final destination, consider creating a narrow nonclustered index that covers the search predicate the query uses.

> [!NOTE]  
> You retrieve data from a heap in order of data pages, but not necessarily the order in which you inserted data.

You can also use heaps when you always access data through nonclustered indexes and the RID is smaller than a clustered index key.

If a table is a heap and doesn't have any nonclustered indexes, then you must read the entire table (a table scan) to find any row. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't seek a RID directly on the heap. This behavior can be acceptable when the table is small.

## When not to use a heap

Don't use a heap when the data is frequently returned in a sorted order. A clustered index on the sorting column can avoid the sorting operation.  

Don't use a heap when the data is frequently grouped together. Data must be sorted before it's grouped, and a clustered index on the sorting column can avoid the sorting operation.

Don't use a heap when ranges of data are frequently queried from the table. A clustered index on the range column avoids sorting the entire heap.

Don't use a heap when there are no nonclustered indexes and the table is large. The only application for this design is to return the entire table content without a specified order. In a heap, [!INCLUDE [ssDE](../../includes/ssde-md.md)] reads all rows to find any row.

Don't use a heap if you frequently update the data. If you update a record and the update uses more space in the data pages than it currently uses, the record moves to a data page that has enough free space. This move creates a *forwarded record* pointing to the new location of the data. The **forwarding pointer** is written in the page that held the data previously, to indicate the new physical location. This move introduces fragmentation in the heap. When [!INCLUDE [ssDE](../../includes/ssde-md.md)] scans a heap, it follows these pointers. This action limits read-ahead performance, and can incur extra I/O, which reduces scan performance.

## Manage heaps

To create a heap, create a table without a clustered index. If a table already has a clustered index, drop the clustered index to return the table to a heap.

To remove a heap, create a clustered index on the heap.

To rebuild a heap to reclaim wasted space:
- Create a clustered index on the heap, and then drop that clustered index.
- Use the `ALTER TABLE ... REBUILD` command to rebuild the heap.

> [!WARNING]  
> Creating or dropping clustered indexes requires rewriting the entire table. If the table has nonclustered indexes, you must recreate all nonclustered indexes whenever you change the clustered index. Therefore, changing from a heap to a clustered index structure or back can take a lot of time and require disk space for reordering data in `tempdb`.

## Identify heaps

The following query returns a list of heaps from the current database. The list includes:

- Table names
- Schema names
- Number of rows
- Table size in KB
- Index size in KB
- Unused space
- A column to identify a heap

```sql
SELECT t.name AS 'Your TableName',
       s.name AS 'Your SchemaName',
       p.rows AS 'Number of Rows in Your Table',
       SUM(a.total_pages) * 8 AS 'Total Space of Your Table (KB)',
       SUM(a.used_pages) * 8 AS 'Used Space of Your Table (KB)',
       (SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS 'Unused Space of Your Table (KB)',
       CASE
           WHEN i.index_id = 0 THEN 'Yes'
           ELSE 'No'
       END AS 'Is Your Table a Heap?'
FROM sys.tables AS t
     INNER JOIN sys.indexes AS i
         ON t.object_id = i.object_id
     INNER JOIN sys.partitions AS p
         ON i.object_id = p.object_id
        AND i.index_id = p.index_id
     INNER JOIN sys.allocation_units AS a
         ON p.partition_id = a.container_id
     LEFT OUTER JOIN sys.schemas AS s
         ON t.schema_id = s.schema_id
WHERE i.index_id <= 1 -- 0 for Heap, 1 for Clustered Index
GROUP BY t.name, s.name, i.index_id, p.rows
ORDER BY 'Your TableName';
```

## Heap structures

A heap is a table without a clustered index. Heaps have one row in [sys.partitions](../system-catalog-views/sys-partitions-transact-sql.md), with `index_id = 0` for each partition used by the heap. By default, a heap has a single partition. When a heap has multiple partitions, each partition has a heap structure that contains the data for that specific partition. For example, if a heap has four partitions, there are four heap structures; one in each partition.

Depending on the data types in the heap, each heap structure has one or more allocation units to store and manage the data for a specific partition. At a minimum, each heap has one `IN_ROW_DATA` allocation unit per partition. The heap structure also has one `LOB_DATA` allocation unit per partition, if it contains large object (LOB) columns. It also has one `ROW_OVERFLOW_DATA` allocation unit per partition, if it contains variable length columns that exceed the 8,060 byte row size limit.

The column `first_iam_page` in the `sys.system_internals_allocation_units` system view points to the first Index Allocation Map (IAM) page in the chain of IAM pages that manage the space allocated to the heap in a specific partition. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the IAM pages to move through the heap. The data pages and the rows within them aren't in any specific order and aren't linked. The only logical connection between data pages is the information recorded in the IAM pages.

> [!IMPORTANT]  
> The `sys.system_internals_allocation_units` system view is reserved for internal use only. Future compatibility isn't guaranteed.

You can perform table scans or serial reads of a heap by scanning the IAM pages to find the extents that hold pages for the heap. Because the IAM represents extents in the same order that they exist in the data files, this structure means that serial heap scans progress sequentially through each file. Using the IAM pages to set the scan sequence also means that rows from the heap aren't typically returned in the order in which they were inserted.

The following illustration shows how the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] uses IAM pages to retrieve data rows in a single partition heap.

:::image type="content" source="media/heaps-tables-without-clustered-indexes/iam-heap.png" alt-text="Diagram of an IAM heap.":::

## Related content

- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [DROP INDEX (Transact-SQL)](../../t-sql/statements/drop-index-transact-sql.md)
- [Clustered and nonclustered indexes](clustered-and-nonclustered-indexes-described.md)
