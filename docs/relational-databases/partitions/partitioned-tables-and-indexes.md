---
title: "Partitioned tables and indexes"
titleSuffix: SQL Server, Azure SQL Database, Azure SQL Managed Instance
description: "With partitioned tabled and indexes, the data can be divided into units that may be spread across more than one filegroup in a database or stored in a single filegroup."
author: VanMSFT
ms.author: vanto
ms.date: "8/17/2022"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "partitioned tables [SQL Server], about partitioned tables"
  - "partitioned indexes [SQL Server], architecture"
  - "partitioned tables [SQL Server], architecture"
  - "partitioned indexes [SQL Server], about partitioned indexes"
  - "index partitions"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Partitioned tables and indexes
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

SQL Server, Azure SQL Database, and Azure SQL Managed Instance support table and index partitioning. The data of partitioned tables and indexes is divided into units that may be spread across more than one filegroup in a database or stored in a single filegroup. When multiple files exist in a filegroup, data is spread across files using the [proportional fill algorithm](../databases/database-files-and-filegroups.md#file-and-filegroup-fill-strategy). The data is partitioned horizontally, so that groups of rows are mapped into individual partitions. All partitions of a single index or table must reside in the same database. The table or index is treated as a single logical entity when queries or updates are performed on the data. 

Prior to [!INCLUDE[ssSQL15_md](../../includes/sssql16-md.md)] SP1, partitioned tables and indexes were not available in every edition of SQL Server. For a list of features supported by the editions of SQL Server, see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md). Partitioned tables and indexes are available in all service tiers of Azure SQL Database and Azure SQL Managed Instance.

Table partitioning is also available in dedicated SQL pools in Azure Synapse Analytics, with some syntax differences. Learn more in [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition).
  
> [!IMPORTANT]  
> The database engine supports up to 15,000 partitions by default. In versions earlier than [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the number of partitions was limited to 1,000 by default.
  
## Benefits of partitioning  

Partitioning large tables or indexes can have the following manageability and performance benefits.  
  
- You can transfer or access subsets of data quickly and efficiently, while maintaining the integrity of a data collection. For example, an operation such as loading data from an OLTP to an OLAP system takes only seconds, instead of the minutes and hours the operation takes when the data is not partitioned.  
  
- You can perform maintenance or data retention operations on one or more partitions more quickly. The operations are more efficient because they target only these data subsets, instead of the whole table. For example, you can choose to compress data in one or more partitions, rebuild one or more partitions of an index, or truncate data in a single partition. You may also switch individual partitions out of one table and into an archive table.
  
- You may improve query performance, based on the types of queries you frequently run. For example, the query optimizer can process equi-join queries between two or more partitioned tables faster when the partitioning columns are the same as the columns on which the tables are joined. See [Queries](#queries) below for further information.
  
You can improve performance by enabling lock escalation at the partition level instead of a whole table. This can reduce lock contention on the table. To reduce lock contention by allowing lock escalation to the partition, set the `LOCK_ESCALATION` option of the `ALTER TABLE` statement to AUTO.
 
## Components and concepts  

The following terms are applicable to table and index partitioning.  
  
### Partition function  

A partition function is a database object that defines how the rows of a table or index are mapped to a set of partitions based on the values of a certain column, called a [partitioning column](#partitioning-column). Each value in the partitioning column is an input to the partitioning function, which returns a partition value. 

The partition function defines the number of partitions and the partition boundaries that the table will have. For example, given a table that contains sales order data, you may want to partition the table into 12 (monthly) partitions based on a **datetime** column such as a sales date.

A range type (either LEFT or RIGHT), specifies how the boundary values of the partition function will be put into the resulting partitions:

- A LEFT range specifies that the boundary value belongs to the left side of the boundary value interval when interval values are sorted by the database engine in ascending order from left to right. In other words, the highest bounding value will be included within a partition.
- A RIGHT range specifies that the boundary value belongs to the right side of the boundary value interval when interval values are sorted by the database engine in ascending order from left to right. In other words, the lowest bounding value will be included in each partition.

If LEFT or RIGHT is not specified, LEFT range is the default.

For example, the following partition function partitions a table or index into 12 partitions, one for each month of a year's worth of values in a **datetime** column. A RIGHT range is used, indicating that boundary values will serve as lower bounding values in each partition. RIGHT ranges are often simpler to work with when partitioning a table based on a column of **datetime** or **datetime2** data types, as rows with a value of midnight will be stored in the same partition as rows with later values on the same day. Similarly, if using the data type of **date** and  using partitions of a month or more, a RIGHT range keeps the first day of the month in the same partition as later days in that month. This aids in precise [Partition elimination](#partition-elimination) when querying an entire day's worth of data.

```sql  
CREATE PARTITION FUNCTION [myDateRangePF1] (datetime)  
AS RANGE RIGHT FOR VALUES ('2022-02-01', '2022-03-01', '2022-04-01',  
               '2022-05-01', '2022-06-01', '2022-07-01', '2022-08-01',   
               '2022-09-01', '2022-10-01', '2022-11-01', '2022-12-01');  
```
  
The following table shows how a table or index that uses this partition function on partitioning column **datecol** would be partitioned. February 1 is the first boundary point defined in the function, so it acts as the lower boundary of partition 2.
  
|Partition|1|2|...|11|12|  
|---------------|-------|-------|---------|--------|--------|  
|**Values**|**datecol** \< `2022-02-01 12:00AM`|**datecol** >= `2022-02-01 12:00AM` AND **datecol** \< `2022-03-01 12:00AM`||**datecol** >= `2022-11-01 12:00AM` AND **col1** \< `2022-12-01 12:00AM`|**datecol** >= `2022-12-01 12:00AM`| 
  
For both RANGE LEFT and RANGE RIGHT, the leftmost partition has the minimum value of the data type as its lower limit, and the rightmost partition has the maximum value of the data type as its upper limit.

Find more examples of LEFT and RIGHT partition functions in [CREATE PARTITION FUNCTION (Transact-SQL)](../../t-sql/statements/create-partition-function-transact-sql.md).

### Partition scheme

A partition scheme is a database object that maps the partitions of a partition function to one filegroup or to multiple filegroups.

Find example syntax to create partition schemes in [CREATE PARTITION SCHEME (Transact-SQL)](../../t-sql/statements/create-partition-scheme-transact-sql.md).

### Filegroups

The primary reason for placing your partitions on multiple filegroups is to make sure that you can independently perform backup and restore operations on partitions. This is because you can perform backups on individual filegroups. When using tiered storage, using multiple filegroups lets you assign specific partitions to specific storage tiers, for example to place older and less frequently accessed partitions on slower and less expensive storage. All other partitioning benefits apply regardless of the number of filegroups used or partition placement on specific filegroups.

Managing files and filegroups for partitioned tables may add significant complexity to administrative tasks over time. If your backup and restore procedures do not benefit from the use of multiple filegroups, a single filegroup for all partitions is recommended. The same [Rules for designing files and filegroups](../databases/database-files-and-filegroups.md#rules-for-designing-files-and-filegroups) apply to partitioned objects as apply to non-partitioned objects.

> [!NOTE]
> Partitioning is fully supported in Azure SQL Database. Because only the `PRIMARY` filegroup is supported in Azure SQL Database, all partitions must be placed on the `PRIMARY` filegroup.

Find example code to create filegroups for SQL Server and Azure SQL Managed Instance in [ALTER DATABASE (Transact-SQL) File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).

### Partitioning column

The column of a table or index that a partition function uses to partition the table or index. The following considerations apply when selecting a partitioning column:

- Computed columns that participate in a partition function must be explicitly created as PERSISTED.
  - Since only one column can be used as the partition column, in some cases the concatenation of multiple columns with a computed column can be useful.
- Columns of all data types that are valid for use as index key columns can be used as a partitioning column, except **timestamp**.
- Columns of large object (LOB) data types, such as **ntext**, **text**, **image**, **xml**, **varchar(max)**, **nvarchar(max)**, and **varbinary(max)**, cannot be specified. 
- Microsoft .NET Framework common language runtime (CLR) user-defined type and alias data type columns cannot be specified.

To partition an object, specify the partition scheme and partitioning column in the [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md), [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md), and [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md) statements.

When creating a nonclustered index, if partition_scheme_name or filegroup is not specified and the table is partitioned, the index is placed in the same partition scheme, using the same partitioning column, as the underlying table. To change how an existing index is partitioned, use [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) with the DROP_EXISTING clause. This lets you partition a non-partitioned index, make a partitioned index non-partitioned, or change the partition scheme of the index.

### Aligned index  

An index that is built on the same partition scheme as its corresponding table. When a table and its indexes are in alignment, the database engine can switch partitions in or out of the table quickly and efficiently while maintaining the partition structure of both the table and its indexes. An index does not have to participate in the same named [partition function](#partition-function) to be aligned with its base table. However, the partition function of the index and the base table must be essentially the same, in that:

- The arguments of the partition functions have the same data type.
- They define the same number of partitions.
- They define the same boundary values for partitions.  

#### Partitioning clustered indexes

When partitioning a clustered index, the clustering key must contain the partitioning column. When partitioning a nonunique clustered index and the partitioning column is not explicitly specified in the clustering key, the database engine adds the partitioning column by default to the list of clustered index keys. If the clustered index is unique, you must explicitly specify that the clustered index key contain the partitioning column. For more information on clustered indexes and index architecture, see [Clustered Index Design Guidelines](../../relational-databases/sql-server-index-design-guide.md#Clustered).

#### Partitioning nonclustered indexes

When partitioning a unique nonclustered index, the index key must contain the partitioning column. When partitioning a nonunique, nonclustered index, the database engine adds the partitioning column by default as a nonkey (included) column of the index to make sure the index is aligned with the base table. The database engine does not add the partitioning column to the index if it is already present in the index. For more information on nonclustered indexes and index architecture, see [Nonclustered Index Design Guidelines](../../relational-databases/sql-server-index-design-guide.md#Nonclustered).

### Non-aligned index  

A non-aligned index is partitioned differently from its corresponding table. That is, the index has a different [partition scheme](#partition-scheme) that places it on a separate [filegroup or set of filegroups](#filegroups) from the base table. Designing a non-aligned partitioned index can be useful in the following cases:  

- The base table has not been partitioned.  
- The index key is unique and it does not contain the partitioning column of the table.  
- You want the base table to participate in collocated joins with more tables using different join columns.  

### Partition elimination

The process by which the query optimizer accesses only the relevant partitions to satisfy the filter criteria of the query.

Learn more about partition elimination and related concepts in [Query Processing Enhancements on Partitioned Tables and Indexes](../query-processing-architecture-guide.md#query-processing-enhancements-on-partitioned-tables-and-indexes).

## Limitations
  
- The scope of a partition function and scheme is limited to the database in which they have been created. Within the database, partition functions reside in a separate namespace from other functions.  
  
- If any rows in a partitioned table have NULLs in the partitioning column, these rows are placed on the left-most partition. However, if NULL is specified as the first boundary value and RANGE RIGHT is specified in the partition function definition, then the left-most partition remains empty, and NULLs are placed in the second partition.  

## Performance guidelines  

The database engine supports up to 15,000 partitions per table or index. However, using more than 1,000 partitions has implications on memory, partitioned index operations, DBCC commands, and queries. This section describes the performance implications of using more than 1,000 partitions and provides workarounds as needed.

With up to 15,000 partitions allowed per partitioned table or index, you can store data for long durations in a single table. However, you should retain data only for as long as it is needed and maintain a balance between performance and the number of partitions.  
  
### Memory usage and guidelines

We recommend that you use at least 16 GB of RAM if a large number of partitions are in use. If the system does not have enough memory, Data Manipulation Language (DML) statements, Data Definition Language (DDL) statements and other operations can fail due to insufficient memory. Systems with 16 GB of RAM that run many memory-intensive processes may run out of memory on operations that run on a large number of partitions. Therefore, the more memory you have over 16 GB, the less likely you are to encounter performance and memory issues.  
  
Memory limitations can affect the performance or ability of the database engine to build a partitioned index. This is especially the case when the index is not aligned with its base table or is not aligned with its clustered index, if the table already has a clustered index.

In SQL Server and Azure SQL Managed Instance, you can increase the `index create memory (KB)` Server Configuration Option. For more information, see [Configure the index create memory Server Configuration Option](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md). For Azure SQL Database, consider temporarily or permanently increasing the service level objective for the database in the Azure portal to allocate more memory.
  
### Partitioned index operations

Creating and rebuilding [non-aligned indexes](#non-aligned-index) on a table with more than 1,000 partitions is possible, but is not supported. Doing so may cause degraded performance or excessive memory consumption during these operations.  
  
Creating and rebuilding [aligned indexes](#aligned-index) could take longer to execute as the number of partitions increases. We recommend that you do not run multiple create and rebuild index commands at the same time as you may run into performance and memory issues.  
  
When the database engine performs sorting to build partitioned indexes, it first builds one sort table for each partition. It then builds the sort tables either in the respective filegroup of each partition or in **tempdb** if the SORT_IN_TEMPDB index option is specified. Each sort table requires a minimum amount of memory to build. When you are building a partitioned index that is aligned with its base table, sort tables are built one at a time, using less memory. However, when you are building a nonaligned partitioned index, the sort tables are built at the same time. As a result, there must be sufficient memory to handle these concurrent sorts. The larger the number of partitions, the more memory required. The minimum size for each sort table, for each partition, is 40 pages, with 8 kilobytes per page. For example, a nonaligned partitioned index with 100 partitions requires sufficient memory to serially sort 4,000 (40 * 100) pages at the same time. If this memory is available, the build operation will succeed, but performance may suffer. If this memory is not available, the build operation will fail. Alternatively, an aligned partitioned index with 100 partitions requires only sufficient memory to sort 40 pages, because the sorts are not performed at the same time.  
  
For both aligned and non-aligned indexes, the memory requirement can be greater if the database engine is using query parallelism to the build operation on a multiprocessor computer. This is because the greater the degree of parallelism (DOP), the greater the memory requirement. For example, if the database engine sets DOP to 4, a nonaligned partitioned index with 100 partitions requires sufficient memory for four processors to sort 4,000 pages at the same time, or 16,000 pages. If the partitioned index is aligned, the memory requirement is reduced to four processors sorting 40 pages, or 160 (4 * 40) pages. You can use the [MAXDOP](../../t-sql/statements/alter-index-transact-sql.md#maxdop--max_degree_of_parallelism) index option to manually reduce the degrees of parallelism.  

### DBCC commands

With a larger number of partitions, DBCC commands such as [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) and [DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md) could take longer to execute as the number of partitions increases.  

### Queries

After partitioning a table or index, queries that use partition elimination can have comparable or improved performance with larger number of partitions. Queries that do not use partition elimination could take longer to execute as the number of partitions increases.  
  
For example, assume a table has 100 million rows and columns `A`, `B`, and `C`.

-  In scenario 1, the table is divided into 1,000 partitions on column `A`. 
-  In scenario 2, the table is divided into 10,000 partitions on column `A`. 

A query on the table that has a `WHERE` clause filtering on column `A` will perform partition elimination and scan one partition. That same query may run faster in scenario 2 as there are fewer rows to scan in a partition. A query that has a `WHERE` clause filtering on column B will scan all partitions. The query may run faster in scenario 1 than in scenario 2 as there are fewer partitions to scan.  

Queries that use operators such as TOP or MAX/MIN on columns other than the partitioning column may experience reduced performance with partitioning because all partitions must be evaluated. 

Similarly, a query that performs a single-row seek or a small range scan will take longer against a partitioned table than against a non-partitioned table if query predicate does not include the partitioning column, because it will need to perform as many seeks or scans as there are partitions. For this reason, partitioning rarely improves performance in OLTP systems where such queries are common.  

If you frequently run queries that involve an equi-join between two or more partitioned tables, their partitioning columns should be the same as the columns on which the tables are joined. Additionally, the tables, or their indexes, should be collocated. This means that they either use the same named partition function, or they use different partition functions that are essentially the same, in that they:

-  Have the same number of parameters that are used for partitioning, and the corresponding parameters are the same data types.
-  Define the same number of partitions.
-  Define the same boundary values for partitions.

In this way, the query optimizer can process the join faster, because the partitions themselves can be joined. If a query joins two tables that are not collocated or are not partitioned on the join field, the presence of partitions may actually slow down query processing instead of accelerate it.

You may find it useful to use `$PARTITION` in some queries. Learn more in [$PARTITION (Transact-SQL)](../../t-sql/functions/partition-transact-sql.md).

For more information about partition handling in query processing, including parallel query execution strategy for partitioned tables and indexes and additional best practices, see [Query Processing Enhancements on Partitioned Tables and Indexes](../../relational-databases/query-processing-architecture-guide.md#query-processing-enhancements-on-partitioned-tables-and-indexes).

## Behavior changes in statistics computation during partitioned index operations

In Azure SQL Database, Azure SQL Managed Instance, and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and higher, statistics are not created by scanning all the rows in the table when a partitioned index is created or rebuilt. Instead, the query optimizer uses the default sampling algorithm to generate statistics. 

After upgrading a database with partitioned indexes from a version of SQL Server lower than 2012 (11.x), you may notice a difference in the histogram data for these indexes. This change in behavior may affect query performance. To obtain statistics on partitioned indexes by scanning all the rows in the table, use `CREATE STATISTICS` or `UPDATE STATISTICS` with the `FULLSCAN` clause.  
  
## Next steps

Learn more about partitioned tables and index strategies in the following articles:

- [Create Partitioned Tables and Indexes](create-partitioned-tables-and-indexes.md)
- [$PARTITION (Transact-SQL)](../../t-sql/functions/partition-transact-sql.md)
- [Scaling out with Azure SQL Database](/azure/azure-sql/database/elastic-scale-introduction)
- [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition)
- [SQL Server and Azure SQL index architecture and design guide](../sql-server-index-design-guide.md)
- [Partitioned Table and Index Strategies Using SQL Server 2008](https://msdn.microsoft.com/library/dd578580\(SQL.100\).aspx)    
- [How to Implement an Automatic Sliding Window](https://msdn.microsoft.com/library/aa964122\(SQL.90\).aspx)    
- [Bulk Loading into a Partitioned Table](/previous-versions/sql/sql-server-2005/administrator/cc966380(v=technet.10))    
- [Project REAL: Data Lifecycle--Partitioning](/previous-versions/sql/sql-server-2005/administrator/cc966424(v=technet.10))    
- [Query Processing Enhancements on Partitioned Tables and Indexes](/previous-versions/sql/sql-server-2008-r2/ms345599(v=sql.105))    
- [Top 10 Best Practices for Building a Large Scale Relational Data Warehouse](https://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20Relational%20Engine.pdf) in _SQLCAT's Guide to: Relational Engineering_
