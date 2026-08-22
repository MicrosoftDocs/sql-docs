---
title: "Columnstore indexes - Query performance"
description: "Columnstore index query performance recommendations for achieving the fast query performance."
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

# Columnstore indexes - query performance

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

This article includes recommendations for achieving the fast query performance with columnstore indexes.

Columnstore indexes can achieve up to 100 times better performance on analytics and data warehousing workloads, and up to 10 times better data compression than traditional rowstore indexes. These recommendations help your queries achieve the fast query performance that columnstore indexes are designed to provide.

## Recommendations for improving query performance

Here are some recommendations for achieving the high-performance columnstore indexes are designed to provide.

### 1. Organize data to eliminate more rowgroups from a full table scan

- **Carefully choose insert order.** In common case in traditional data warehouse, the data is indeed inserted in time order and analytics is done in time dimension. For example, analyzing sales by quarter. For this kind of workload, the rowgroup elimination happens automatically. In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can find out number rowgroups skipped as part of query processing.

- **Use a rowstore clustered index.** If the common query predicate is on a column (for example, `C1`) unrelated to the insert order, create a rowstore clustered index on column `C1`. Then, drop the rowstore clustered index and create a clustered columnstore index. If you create the clustered columnstore index explicitly using `MAXDOP = 1`, the resulting clustered columnstore index is perfectly ordered on column `C1`. If you specify `MAXDOP = 8`, then you see overlap of values across eight rowgroups. For a nonclustered columnstore index (NCCI), if the table has a rowstore clustered index, the rows are already ordered by the clustered index key. In this case, the nonclustered columnstore index is also automatically ordered. A columnstore index doesn't inherently maintain the order of rows. As new rows are inserted or older rows are updated, you might need to repeat the process as the analytics query performance might deteriorate.

- **Implement table partitioning.** You can partition the columnstore index, then use partition elimination to reduce number of rowgroups to scan. For example, a fact table stores purchases made by customers. A common query pattern is to find quarterly purchases by `customer`. In this case, combine the insert order column with partitioning on `customer` column. Each partition contains rows for each `customer`, ordered upon insertion. Also, consider using table partitioning if there's a need to remove older data from the columnstore. Switching out and truncating partitions that aren't needed is an efficient strategy to delete data without generating fragmentation.

- **Avoid deleting large amounts of data**. Removing compressed rows from a rowgroup isn't a synchronous operation. It would be expensive to decompress a rowgroup, delete the row, and then recompress it. Therefore, when you delete data from compressed rowgroups these rowgroups are still scanned, even though they return fewer rows. If the number of deleted rows for several rowgroups is large enough to be merged into fewer rowgroups, reorganizing the columnstore increases the quality of the index and query performance improves. If your data deletion process usually empties entire rowgroups, consider using table partitioning. Switch out partitions that aren't needed anymore and truncate them, instead of deleting rows.

    > [!NOTE]  
    > Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], the tuple-mover is helped by a background merge task. This task automatically compresses smaller OPEN delta rowgroups that have existed for some time, as determined by an internal threshold, or merges COMPRESSED rowgroups from where a large number of rows has been deleted. This improves the columnstore index quality over time.
    > If deleting large amounts of data from the columnstore index is required, consider splitting that operation into smaller delete batches over time. Batching allows the background merge task to handle the task of merging smaller rowgroups, and improves index quality. Then, there's no need to schedule index reorganization maintenance windows after data deletion.
    > For more information about columnstore terms and concepts, see [Columnstore indexes: overview](columnstore-indexes-overview.md).

### 2. Plan for enough memory to create columnstore indexes in parallel

Creating a columnstore index is by default a parallel operation unless memory is constrained. Creating the index in parallel requires more memory than creating the index serially. When there is ample memory, creating a columnstore index takes on the order of 1.5 times as long as building a B-tree on the same columns.

The memory required for creating a columnstore index depends on the number of columns, the number of string columns, the degree of parallelism (DOP), and the characteristics of the data. For example, if your table has fewer than one million rows, [!INCLUDE [ssDE](../../includes/ssde-md.md)] uses only one thread to create the columnstore index.

If your table has more than one million rows, but [!INCLUDE [ssDE](../../includes/ssde-md.md)] can't get a large enough memory grant to create the index using MAXDOP, [!INCLUDE [ssDE](../../includes/ssde-md.md)] automatically decreases `MAXDOP` as needed. In some cases, DOP must be decreased to one in order to build the index under constrained memory in the available memory grant.

Since [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the query always operates in batch mode. In previous releases, batch execution is only used when DOP is greater than one.

## Columnstore performance explained

Columnstore indexes achieve high query performance by combining high-speed in-memory batch mode processing with techniques that greatly reduce I/O requirements. Since analytics queries scan large numbers of rows, they are typically I/O-bound, and therefore reducing I/O during query execution is critical to the design of columnstore indexes. Once data is read into memory, it is critical to reduce the number of in-memory operations.

Columnstore indexes reduce I/O and optimize in-memory operations through high data compression, columnstore elimination, rowgroup elimination, and batch processing.

### Data compression

Columnstore indexes achieve up to 10 times greater data compression than rowstore indexes. This greatly reduces the I/O required to execute analytics queries and therefore improves query performance.

- Columnstore indexes read compressed data from disk, which means fewer bytes of data need to be read into memory.

- Columnstore indexes store data in compressed form in memory, reducing I/O by avoiding reading the same data into memory. For example, with 10 times compression, columnstore indexes can keep 10 times more data in memory, compared to storing the data in uncompressed form. With more data in memory, it is more likely that the columnstore index finds the data it needs in memory without incurring unnecessary reads from disk.

- Columnstore indexes compress data by columns instead of by rows, achieving high compression rates and reducing the size of the data stored on disk. Each column is compressed and stored independently. Data within a column always has the same data type, and tends to have similar values. Columnstore data compression techniques are great at achieving higher compression rates when values are similar.

For example, a fact table stores customer addresses and has a column for `country-region`. The total number of possible values is fewer than 200. Some of those values are repeated many times. If the fact table has 100 million rows, the `country-region` column compresses easily and requires little storage. Row-by-row compression isn't able to capitalize on the similarity of column values in this way, and must use more bytes to compress the values in the `country-region` column.

### Column elimination

Columnstore indexes skip reading in columns that aren't referenced by the query. Column elimination further reduces I/O for query execution and therefore improves query performance.

- Column elimination is possible because the data is organized and compressed column by column. In contrast, when data is stored row-by-row, the column values in each row are physically stored together and can't be easily separated. The Query Processor needs to read in an entire row to retrieve specific column values, increasing I/O because extra data is unnecessarily read into memory.

For example, if a table has 50 columns and the query only uses five of those columns, the columnstore index only fetches the five columns from disk. It skips reading in the other 45 columns, reducing I/O by another 90%, assuming all columns are of similar size. If the same data are stored in a rowstore, the query processor needs to read the remaining 45 columns.

### Rowgroup elimination

For full table scans, a large percentage of the data usually doesn't match the query predicate criteria. By using metadata, the columnstore index is able to skip reading in the rowgroups that don't contain data required for the query result, all without actual I/O. This ability, called rowgroup elimination, reduces I/O for full table scans and therefore improves query performance.

**When does a columnstore index need to perform a full table scan?**

Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can create one or more regular nonclustered rowstore, or B-tree, indexes on a clustered columnstore index. The nonclustered B-tree indexes can speed up a query that has an equality predicate or a predicate with a small range of values. For more complicated predicates, the query optimizer might choose a full table scan. Without the ability to skip rowgroups, a full table scan can be time-consuming, especially for large tables.

**When does an analytics query benefit from rowgroup elimination for a full-table scan?**

For example, a retail business models their sales data using a fact table with clustered columnstore index. Each new sale stores various attributes of the transaction, including the date a product is sold. Interestingly, even though columnstore indexes don't guarantee a sorted order, rows in this table are loaded in a date-sorted order. Over time this table grows. Although the retail business might keep sales data for the last 10 years, an analytics query might only need to compute an aggregate for last quarter. Columnstore indexes can eliminate accessing the data for the previous 39 quarters by just looking at the metadata for the date column. This is a 97% reduction in the amount of data that is read into memory and processed.

**Which rowgroups are skipped in a full table scan?**

To determine which rows groups to eliminate, the columnstore index uses metadata to store the minimum and maximum values of each column segment for each rowgroup. When none of the column segment ranges meet the query predicate criteria, the entire rowgroup is skipped without doing any actual I/O. This works because the data is usually loaded in a sorted order. Although row sorting isn't guaranteed, similar data values are often located within the same rowgroup or a neighboring rowgroup.

For more information about rowgroups, see [Columnstore index design guidelines](../../relational-databases/sql-server-index-design-guide.md#columnstore_index).

### Batch mode execution

[Batch mode execution](../../relational-databases/query-processing-architecture-guide.md#batch-mode-execution) processes rows in groups, usually up to 900 at a time, to improve efficiency. For example, the query `SELECT SUM(Sales) FROM SalesData` calculates the total sales from the `SalesData` table. In batch mode, the query engine processes the data in groups of 900 rows. This approach reduces the metadata access cost and other types of overhead by spreading them across all rows in a batch, rather than incurring the overhead for each row. Additionally, batch mode works with compressed data when possible and removes some of the exchange operators used in row mode, significantly speeding up analytical queries.

Not all query execution operators can be executed in batch mode. For example, data manipulation language (DML) operations such as insert, delete, or update are executed one row at a time. Batch mode operator such as Scan, Join, Aggregate, Sort, and others can improve query performance. Since the columnstore index was introduced in [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], there is a sustained effort to expand the operators that can be executed in the batch mode. The following table shows the operators that run in batch mode according to the product version.

|Batch Mode Operators|When used|[!INCLUDE [ssSQL11](../../includes/sssql11-md.md)]|[!INCLUDE [ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE [ssSDS](../../includes/sssds-md.md)]<sup>1</sup>|Comments|
|---------------------------|------------------------|---------------------|---------------------|---------------------------------------|--------------|
|DML operations (insert, delete, update, merge)||no|no|no|Performance gains from using batch mode with DML aren't significant.|
|columnstore index scan|SCAN|Not available|yes|yes|For columnstore indexes, we can push the predicate to the SCAN node.|
|columnstore index Scan (nonclustered)|SCAN|yes|yes|yes|yes|
|index seek||Not available|Not available|no|We perform a seek operation through a nonclustered B-tree index in row mode.|
|compute scalar|Expression that evaluates to a scalar value.|yes|yes|yes|Like all batch mode operators, there are some restrictions on data type.|
|concatenation|UNION and UNION ALL|no|yes|yes||
|filter|Applying predicates|yes|yes|yes||
|hash match|Hash-based aggregate functions, outer hash join, right hash join, left hash join, right inner join, left inner join|yes|yes|yes|Restrictions for aggregation: no min/max for strings. Aggregation functions available are sum/count/avg/min/max.<br />Restrictions for join: no mismatched type joins on non-integer types.|
|merge join||no|no|no||
|multi-threaded queries||yes|yes|yes||
|nested loops||no|no|no||
|single-threaded queries running under MAXDOP 1||no|no|yes||
|single-threaded queries with a serial query plan||no|no|yes||
|sort|Order by clause on SCAN with columnstore index.|no|no|yes||
|top sort||no|no|yes||
|window aggregates||Not available|Not available|yes|New operator in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].|

<sup>1</sup> Applies to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [ssSDS](../../includes/sssds-md.md)] Premium tiers, Standard tiers - S3 and higher, and all vCore tiers, and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

For more information, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#batch-mode-execution).

### Aggregate pushdown

 A normal execution path for aggregate computation to fetch the qualifying rows from the SCAN node and aggregate the values in Batch Mode. While this delivers good performance, starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the aggregate operation can be pushed to the SCAN node. Aggregate pushdown improves the performance of aggregate computations by orders of magnitude on top of Batch Mode execution, provided the following conditions are met:

- The aggregates are `MIN`, `MAX`, `SUM`, `COUNT` and `COUNT(*)`. 
- Aggregate operator must be on top of SCAN node or SCAN node with `GROUP BY`.
- This aggregate isn't a distinct aggregate.
- The aggregate column isn't a string column.
- The aggregate column isn't a virtual column. 
- The input and output data type must be one of the following and must fit within 64 bits:
    - **tinyint**, **int**, **bigint**, **smallint**, **bit**
    - **smallmoney**, **money**, **decimal** and **numeric** with precision <= 18
    - **smalldate**, **date**, **datetime**, **datetime2**, **time**

For example, aggregate pushdown is done in both of the following queries:

```sql
SELECT  productkey, SUM(TotalProductCost)
FROM FactResellerSalesXL_CCI
GROUP BY productkey;
    
SELECT  SUM(TotalProductCost)
FROM FactResellerSalesXL_CCI;
```

### String predicate pushdown

When designing a data warehouse schema, the recommended schema modeling is to use star-schema or snowflake schema consisting of one or more fact tables and many dimension tables. 

> [!TIP]
> The [fact table](https://wikipedia.org/wiki/Fact_table) stores the business measurements or transactions and [dimension table](https://wikipedia.org/wiki/Dimension_table) store the dimensions across which facts need to be analyzed. For more information on dimensional modeling, see [Dimensional modeling in Microsoft Fabric](/fabric/data-warehouse/dimensional-modeling-overview).

For example, a fact can be a record representing a sale of a particular product in a specific region while the dimension represents a set of regions, products and so on. The fact and dimension tables are connected through a primary/foreign key relationship. Most commonly used analytics queries join one or more dimension tables with the fact table.

Let us consider a dimension table `Products`. A typical primary key is `ProductCode`, commonly represented as string. For performance of queries, it is a best practice to create surrogate key, typically an **integer** column, to refer to the row in the dimension table from the fact table.

The columnstore index runs analytics queries with joins and predicates involving numeric or integer-based keys efficiently. [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] improved the performance of analytics queries with string-based columns significantly, by pushing down the predicates with string columns to the SCAN node.

String predicate pushdown leverages the primary/secondary dictionary created for columns to improve the query performance. For example, consider a string column segment within a rowgroup consisting of 100 distinct string values. Each distinct string value is referenced 10,000 times on average, assuming one million rows. With string predicate pushdown, the query execution computes the predicate against the values in the dictionary. If the predicate qualifies, all rows referring to the dictionary value are automatically qualified. This improves the performance in two ways:

- Only the qualified row is returned reducing number of the rows that need to flow out of scan node.
- The number of string comparisons are reduced. In this example, only 100 string comparisons are required as against 1 million comparisons. There are some limitations:
    -   No string predicate pushdown for delta rowgroups. There is no dictionary for columns in delta rowgroups.
    -   No string predicate pushdown if dictionary exceeds 64-KB entries.
    -   Expression evaluating nulls aren't supported.

## Segment elimination

Data type choices might have a significant impact on query performance based common filter predicates for queries on the columnstore index.

In columnstore data, row groups are made up of column segments. There is metadata with each segment to allow for fast elimination of segments without reading them. This segment elimination applies to numeric, date, and time data types, and the **datetimeoffset** data type with scale less than or equal to two. Beginning in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], segment elimination capabilities extend to string, binary, guid data types, and the **datetimeoffset** data type for scale greater than two.

After upgrading to a version of SQL Server that supports string min/max segment elimination ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later), the columnstore index doesn't benefit from this feature until it is rebuilt using a `ALTER INDEX REBUILD` or `CREATE INDEX WITH (DROP_EXISTING = ON)`.

Segment elimination doesn't apply to LOB data types, such as the (max) data type lengths.

Currently, only [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later supports clustered columnstore rowgroup elimination for the prefix of `LIKE` predicates, for example `column LIKE 'string%'`. Segment elimination isn't supported for non-prefix use of `LIKE`, such as `column LIKE '%string'`.

[Ordered columnstore indexes](columnstore-indexes-overview.md#ordered-columnstore-indexes) also benefit from segment elimination, especially for string columns. In ordered columnstore indexes, segment elimination on the first column in the index key is most effective, because it is sorted. Performance gains due to segment elimination on other columns in the table is less predictable. For more on ordered columnstore indexes, see [Use an ordered columnstore index for large data warehouse tables](columnstore-indexes-design-guidance.md#use-an-ordered-columnstore-index-for-large-data-warehouse-tables). For ordered columnstore index availability, see [Ordered column index availability](columnstore-indexes-overview.md#ordered-columnstore-index-availability).

Using the query connection option [SET STATISTICS IO](../../t-sql/statements/set-statistics-io-transact-sql.md), you can view segment elimination in action. Look for output such as the following to indicate that segment elimination has occurred. Row groups are made up of column segments, so this might indicate segment elimination. The following `SET STATISTICS IO` output example of a query, roughly 83% data was skipped by the query:

```output
...
Table 'FactResellerSalesPartCategoryFull'. Segment reads 16, segment skipped 83.
...
```

## Related content

- [Columnstore Index Design Guidelines](../sql-server-index-design-guide.md#columnstore_index)
- [Columnstore indexes - data loading guidance](columnstore-indexes-data-loading-guidance.md)
- [Get started with columnstore indexes for real-time operational analytics](get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Columnstore indexes in data warehousing](columnstore-indexes-data-warehouse.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)
- [Columnstore Index Architecture](../sql-server-index-design-guide.md#columnstore_index)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
