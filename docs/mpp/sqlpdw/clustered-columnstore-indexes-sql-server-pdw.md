---
title: "Clustered Columnstore Indexes (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3e401b81-88cf-42a9-82ba-8a83920f77c1
caps.latest.revision: 38
author: BarbKess
---
# Clustered Columnstore Indexes (SQL Server PDW)
The in-memory clustered columnstore index stores and manages data by using column-based data storage and column-based query processing. Use this storage method to achieve up to **10x query performance** gains over traditional row-oriented storage, and up to **7x data compression** over the uncompressed data size.  
  
> [!NOTE]  
> We view the clustered columnstore index as the standard for storing tables in SQL Server PDW, and expect it will be used in most scenarios.  
  
The clustered columnstore index:  
  
-   Is updateable.  
  
-   Includes all columns of the table and is the means for which the entire table is stored.  
  
-   Delivers high query performance by storing and compressing data by columns.  
  
-   Benefits from high compression ratios by using fewer reads to bring compressed data into memory and then using the reduced data volume for the in-memory processing.  
  
## Contents  
  
-   [Basics](#Basics)  
  
-   [Processes](#Processes)  
  
-   [Related Statements](#RS)  
  
-   [Metadata](#Metadata)  
  
-   [Performance Tips](#Performance)  
  
-   [Examples](#Examples)  
  
## <a name="Basics"></a>Basics  
![Clustered Columnstore Index](../sqlpdw/media/SQL_Server_PDW_Columnstore_PhysicalStorage.png "SQL_Server_PDW_Columnstore_PhysicalStorage")  
  
A clustered columnstore index is a technology for storing, retrieving and managing data by using a columnar data format, called a columnstore. The data is compressed, stored, and managed as a collection of partial columns, called column segments.  
  
Some of the clustered columnstore index data is stored temporarily in a rowstore table, called a deltastore, until it is compressed and moved into the columnstore. The clustered columnstore index operates on both the columnstore and the deltastore, to return the correct query results.  
  
The remainder of this topic explains these concepts and provides examples.  
  
### Key Characteristics  
In SQL Server PDW, a clustered columnstore index:  
  
-   Includes all columns in the table. It is the method for storing the entire table.  
  
-   Is the only index on the table. It cannot be combined with other indexes.  
  
-   Uses columnstore compression. The compression is not configurable.  
  
-   Does not physically store columns in a sorted order. Instead, it stores data to improve compression and performance..  
  
-   Applies after the user data is copied to the correct Compute node, distribution, and partition. SQL Server PDW moves the data to the correct location, and then adds the data to the clustered columnstore index.  
  
-   Is comprised of many parallel clustered columnstore indexes.  For a distributed table, there is one clustered columnstore index for every partition of every distribution and for every Compute node. For a replicated table, there is one clustered columnstore index for every partition of the replicated table on every Compute node. .  
  
### <a name="Benefits"></a>Benefits  
SQL Server takes advantage of the column-based data layout to significantly improve compression rates and query execution time. For example:  
  
-   Columns often have similar data, which results in high compression rates. In turn, higher compression rates further improve query performance by using a smaller in-memory footprint.  
  
-   High compression rates improve query performance by using a smaller in-memory footprint. In turn, query performance can improve because SQL Server PDW can perform more query and data operations in-memory.  
  
-   Queries often select only a few columns from a table, which reduces total I/O to and from the physical media.  
  
-   Advanced query execution technology processes chunks of columns called *batches* in a streamlined manner, which reduces CPU usage.  
  
### <a name="Concepts"></a>Key Terms  
The following are key terms and concepts that you will need to know in order to better understand how to use clustered columnstore indexes.  
  
rowgroup  
A rowgroup is a group of rows that are compressed into columnstore format at the same time.  Each column in the rowgroup is compressed and stored separately onto the physical media. For the minimum and maximum rows per rowgroup, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
column segment  
A column segment is the basic storage unit for a clustered columnstore index. It is a group of column values that are compressed and physically stored together on the physical media.  Rowgroups define the column values that are in each column segment.  
  
1.  Each column is comprised of one or many column segments.  
  
2.  Each rowgroup contains one column segment for every column in the table.  
  
3.  When SQL Server PDW compresses a rowgroup, it compresses each column within the rowgroup as one column segment.  
  
4.  
  
columnstore  
A columnstore is data that is logically organized as a table with rows and columns, and physically stored in a columnar data format.  In SQL Server PDW, the columnstore contains compressed column segments.  
  
rowstore  
A rowstore is data that is organized as rows and columns, and then physically stored in a row-wise data format. This has been the traditional way to store relational table data.  
  
deltastore  
A deltastore is a rowstore table that holds rows until the number of rows is large enough to be moved into the columnstore. During a bulk load, most of the rows go directly to the columnstore without passing through the deltastore.  
  
When you perform a bulk load, most of the rows will go directly to the columnstore without passing through the deltastore. Some rows at the end of the bulk load might be too few in number to meet the minimum size of a rowgroup. When this happens, the final rows go to the deltastore instead of the columnstore.  
  
Rows accumulate in each deltastore until the number of rows is the maximum number of rows allowed for a rowgroup.  
  
When a deltastore contains the maximum number of rows per rowgroup, SQL Server PDW marks the rowgroup as “CLOSED”.  A background process, called the “tuple-mover”, then moves the rowgroup into the columnstore.  The rowgroup is marked as “COMPRESSED”.  
  
When the deltastore contains the maximum number of rows per rowgroup, SQL Server PDW marks the rowgroup as “CLOSED”.  A background process, called the “tuple-mover”, finds the CLOSED rowgroup and moves into the columnstore. The rowgrop is compressed into column segments and the column segments are stored in the columnstore.  
  
For each columnstore there can be multiple deltastores.  
  
-   If a deltastore is locked, SQL Server PDW will try to obtain a lock on a different deltastore. If there are no deltastores available, SQL Server PDW will create a new deltastore.  
  
-   For a distributed table, there are one or more deltastores for every partition of every distribution across all the Compute nodes..  
  
-   For a replicated table, there are one or more deltastores for each partition for each replicated table across all of the Compute nodes.  
  
## <a name="Processes"></a>Processes  
  
### Create a Clustered Columnstore Index  
There are multiple ways to create a clustered columnstore index. For example:  
  
-   Use [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md) and the CLUSTERED COLUMNSTORE INDEX option to create a new table.  
  
-   Use [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md) and the CLUSTERED COLUMNSTORE INDEX option to create a new table based on copying data from an existing table. For best performance, we recommend this method for converting large fact tables from rowstore to columnstore.  
  
-   Use  [CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-columnstore-index-sql-server-pdw.md) to convert a rowstore table to a columnstore table.  The syntax for this approach is simple and easy to use.  This approach is performed as an atomic operation within each Compute node. It runs serially across each Compute node, whereas CREATE TABLE AS SELECT is perfomed in parallel across the distributions and across the Compute nodes. For best performance, we recommend this approach to save syntax development time if you need to convert many small dimension tables from rowstore to columnstore.  
  
See [Examples](#Examples).  
  
### Add Data to a Clustered Columnstore Index  
You can add data to an existing clustered columnstore index by using any of the standard loading methods.  For example, dwloader, Integration Services, and INSERT … SELECT can all load data into a clustered columnstore index. For more information about loading methods, see [Load &#40;SQL Server PDW&#41;](../sqlpdw/load-sql-server-pdw.md).  
  
To load data into a clustered columnstore index, SQL Server PDW first moves the data the correct physical location on the appliance.  
  
-   For a partitioned, distributed table, the location is a partition within a distribution on a Compute node.  
  
-   For a non-partitioned, distributed table, the location is a distribution on a Compute node.  
  
-   For a  replicated table, the location is each Compute node.  
  
The following diagram shows how data gets added to a clustered columnstore index. This operation occurs per partition, and per distribution for a distributed table, and per Compute node for a replicated table.  
  
![Loading into a clustered columnstore index](../sqlpdw/media/SQL_Server_PDW_ColumnStore_LoadProcess.png "SQL_Server_PDW_ColumnStore_LoadProcess")  
  
As the diagram suggests, to load data into a clustered columnstore index, SQL Server:  
  
1.  Divides the bulk load into rowgroups of the maximum 1,048,576 rows.  
  
    1.  For distributed tables, the rowgroups are at the partition level. For example, a distributed table with 10 logical partitions on a 6 Compute node appliance with 8 distributions per Compute node, will be stored with 480 actual partitions. Rows will be loaded into a minimum of 480 rowgroups and 480 deltastores.  
  
    2.  For replicated tables, the rowgroups are per partition cross all of the Compute nodes. For example, a replicated table with 10 logical partitions on a 6 Compute node appliance will be stored with 60 actual partitions since the replicated table is duplicated on each Compute node.  
  
2.  For each maximum-size rowgroup, SQL Server PDW will:  
  
    1.  Mark the rowgroup as CLOSED.  
  
    2.  Bypass the deltastore.  
  
    3.  Compress each column segment with the rowgroup with columnstore compression.  
  
    4.  Physically store each compressed column segment into the columnstore.  
  
3.  Inserts the final rows of the bulk load as follows:  
  
    1.  If the number of rows meets the 102,400 minimum rows per rowgroup requirement, the rowgroup is compressed into the columnstore.  
  
    2.  If the number of rows is less than the 102,400 minimum rows per rowgroup, the rows are added to the deltastore.  
  
Columnstore compression uses in-memory operations to compress each rowgroup. If all rows in a specific rowgroup (i.e. 1,048,576 rows) cannot fit into the available memory, the compression routines will reduce the number of rows in that rowgroup. As a result, multiple smaller compressed rowgroups could be added to the columnstore index. As rowgroups get smaller, the resulting columnstore compression degrades.  
  
**Columnstore Versus Deltastore Loading Scenarios**  
  
The following examples describe when loaded rows go directly to the columnstore or when they go to the deltastore. The minimum number of rows per rowgroup is 102,400, and the maximum number of rows per rowgroup is 1,048,576.  
  
|Rows to Bulk Load|Rows Added to the Columnstore|Rows Added to the Deltastore|  
|---------------------|---------------------------------|--------------------------------|  
|102,000|0|102,000|  
|145,000|145,000<br /><br />Rowgroup size: 145,000|0|  
|1,048,577|1,048,576<br /><br />Rowgroup size: 1,048,576.|1|  
|2,252,152|2,252,152<br /><br />Rowgroup sizes: 1,048,576, 1,048,576, 155,000.|0|  
  
**Loading Example**  
  
The following example shows the results of loading 1,048,577 rows into a partition. The results show that one COMPRESSED rowgroup in the columnstore (as compressed column segments), and 1 row in the deltastore.  
  
```  
SELECT * FROM sys. pdw_nodes_column_store_row_groups  
```  
  
![Rowgroup and deltastore for a batch load](../sqlpdw/media/SQL_Server_PDW_ColumnStore_batchload.png "SQL_Server_PDW_ColumnStore_batchload")  
  
### <a name="DML"></a>Perform DML Operations on a Clustered Columnstore Index  
Clustered columnstore indexes support the INSERT, UPDATE, DELETE DML options.  
  
INSERT  
To insert a row with the INSERT statement:  
  
-   SQL Server PDW inserts the new row into the deltastore.  
  
DELETE  
To delete a row with the DELETE statement:  
  
-   If the row is in the columnstore, SQL Server PDW marks the row as logically deleted but does not reclaim the physical storage for the row until the index is rebuilt.  
  
-   If the row is in the deltastore, SQL Server PDW logically and physically deletes the row.  
  
UPDATE  
To update a row with the UPDATE statement:  
  
-   If the row is in the columnstore, SQL Server PDW marks the row as logically deleted, and then inserts the updated row into the deltastore.  
  
-   If the row is in the deltastore, SQL Server PDW updates the row in the deltastore.  
  
### Rebuild a Clustered Columnstore Index  
Rebuilding a clustered columnstore index removes fragmentation by re-compressing all of the data in the index. It also forces all of the data to be stored in the columnstore. The deltastore is empty after a rebuild.  
  
> [!NOTE]  
> Since rebuilding forces all of the data into the columnstore, some of the rowgroups might have less than the 102,400 minimum rows per rowgroup that is used for bulk loading.  
  
You can use CREATE COLUMNSTORE INDEX or ALTER INDEX to perform a full rebuild of an existing clustered columnstore index. Additionally, you can use ALTER INDEX to rebuild a specific partition.  
  
To rebuild a clustered columnstore index, SQL Server:  
  
-   Acquires an exclusive lock on the table or partition while the rebuild occurs.  The data is “offline” and unavailable during the rebuild.  
  
-   Defragments the columnstore by physically deleting rows that have been logically deleted from the table; the deleted bytes are reclaimed on the physical media.  
  
-   Merges the rowstore data, stored in the deltastore, with the data in the columnstore before it rebuilds the index. When the rebuild is finished, all data is stored in columnstore format, and the deltastore, which is a rowstore, contains 0 rows.  
  
-   Re-compresses all data into the columnstore. Two copies of the columnstore index exist while the rebuild is taking place. When the rebuild is finished, SQL Server deletes the original columnstore index.  
  
When to rebuild a clustered columnstore index:  
  
-   Use caution before rebuilding an entire clustered columnstore index because this takes a long time if the index is large, and it requires enough disk space to store an additional copy of the index while it is rebuilding. Usually it is only necessary to rebuild the most recently used partition  
  
-   Rebuild a partition after heavy DML operations to defragment the index and reduce disk storage. For example, deleting rows marks them for deletion but does not actually delete the row from the columnstore until the index is rebuilt. Delting rows also occurs during an update operation; to update a row, SQL Server PDW inserts a new row, and then marks the old row for deletion.  
  
-   Rebuild a partition after loading data to ensure all data is stored in the columnstore. This is useful, especially if the loaded rows land in one or more deltastores of the individual distributions. Note that if multiple loads occur at the same time, each distribution could end up having multiple deltastores.  
  
### Reorganize a Clustered Columnstore Index  
You can use ALTER INDEX … REORGANIZE to move all CLOSED rowgroups into the columnstore.  
  
When to reorganize a clustered columnstore index:  
  
-   Reorganize a clustered columnstore index after one or more data loads to achieve query performance benefits as quickly as possible. Reorganizing will initially require additional CPU resources to compress the data, which could slow overall system performance. However, as soon as the data is compressed, query performance can improve.  
  
Reorganizing is not required for moving CLOSED rowgroups into the columnstore. The tuple-mover process will eventually find all CLOSED rowgroups and move them. However, the tuple-mover is single-threaded and might not move rowgroups fast enough for your workload.  
  
## <a name="RS"></a>Related Statements  
  
### A. CREATE TABLE  
Use the [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md) statement and CLUSTERED COLUMNSTORE INDEX table option to create a new columnstore table with a clustered columnstore index.  
  
### B. CREATE COLUMNSTORE INDEX  
Use the [CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-columnstore-index-sql-server-pdw.md) statement to rebuild an existing clustered columnstore index, or to convert a small dimension table from a rowstore to a columnstore.  
  
Using CREATE COLUMNSTORE INDEX to convert a rowstore table to a columnstore table:  
  
-   Retains the metadata and objects associated with the table. For example, the columnstore table retains security permissions, default constraints, views, and statistics.  
  
-   Is simple and convenient to use, especially if you need to convert a large number of small dimension tables.  
  
-   Should only be used for small dimension tables because of performance considerations.  For more information, see the Performnace section in this topic.  
  
For both rebuilding and converting operations, you can keep the same index name by using the DROP_EXISTING option.  
  
### C. CREATE TABLE AS SELECT  
Use the [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md) statement to:  
  
-   Convert a large rowstore table to a columnstore table. You can quickly start using the new table while the conversion occurs in parallel across all dimensions, and in the background.  
  
-   Or, to create an additional clustered columnstore index that includes only a few of the columns in your original table. This requires extra storage space since data is copied to the new table.  
  
Using CREATE TABLE AS SELECT to convert a rowstore table to a columnstore table:  
  
-   Does not remove the original table.  You need to drop the original table, if desired, after CREATE TABLE AS SELECT finishes.  
  
-   Copies the table data to a temporary rowstore table and make the index available for read and write operations before all data has been moved to the columnar data format.  
  
-   Performs the conversions from rowstore to columnstore in parallel across the distributions.  
  
### D. DROP INDEX  
Use the [DROP INDEX &#40;SQL Server PDW&#41;](../sqlpdw/drop-index-sql-server-pdw.md) statement to drop a clustered columnstore index. This operation will drop the index and convert the columnstore table to a rowstore heap.  
  
### E. ALTER INDEX  
Use the [ALTER INDEX &#40;SQL Server PDW&#41;](../sqlpdw/alter-index-sql-server-pdw.md) statement to rebuild or reorganize an existing columnstore index.  
  
Using ALTER INDEX, the rebuild:  
  
## <a name="Metadata"></a>Metadata  
All of the columns in a clustered columnstore index are stored in the metadata as included columns. The clustered columnstore index does not have key columns.  
  
-   [sys.indexes &#40;SQL Server PDW&#41;](../sqlpdw/sys-indexes-sql-server-pdw.md)  
  
-   [sys.index_columns &#40;SQL Server PDW&#41;](../sqlpdw/sys-index-columns-sql-server-pdw.md)  
  
-   [sys.partitions &#40;SQL Server PDW&#41;](../sqlpdw/sys-partitions-sql-server-pdw.md)  
  
-   [sys.pdw_nodes_column_store_segments &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-segments-sql-server-pdw.md)  
  
-   [sys.pdw_nodes_column_store_dictionaries &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-dictionaries-sql-server-pdw.md)  
  
-   [sys.pdw_nodes_column_store_row_groups &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-row-groups-sql-server-pdw.md)  
  
## <a name="Performance"></a>Performance Tips  
  
### A. Use CREATE TABLE AS SELECT to convert large tables from rowstore to columnstore.  
For best performance, use CREATE TABLE AS SELECT instead of CREATE COLUMNSTORE INDEX to convert fact and large dimension tables from rowstore to columnstore.  The CREATE COLUMNSTORE INDEX operation is an easy way to convert small dimension tables; it is a slow way to convert large tables. For example:  
  
-   The CREATE CLUSTERED COLUMNSTORE INDEX operation is atomic. To guarantee that all changes for a distributed table can be reversed in case of failure, SQL Server PDW creates a global transaction, distributed across all Compute nodes, that creates the clustered columnstore indexes serially across the distributions within each Compute node, and in parallel across all of the Compute nodes.  This is several times slower than the CTAS statement, which does not run as a global distributed transaction, and therefore runs in parallel across the distributions on each Compute node.  
  
### B. Convert a fact table to a columnstore table before creating additional small columnstore tables based on the fact table.  
If you plan to create smaller tables from a rowstore fact table, convert the fact table to a columnstore table with CREATE TABLE AS SELECT, and then create the smaller tables. This method is much faster than creating smaller tables from a rowstore fact tables. When the table is already stored aross the distributions with a clustered columnstore index, the data is already stored and compressed as columns. This makes it simple to create a new columnstore table.  
  
Conversely, if the original table is a rowstore, SQL Server PDW will have to convert the data from rowstore to columnstore for each new small table. This takes more time than converting from columnstore to columnstore.  
  
### C. Use ALTER INDEX REBUILD to rebuild only a specific partition of the columnstore table.  
Large fact and large dimension tables are usually partitioned in order to perform backup and management operations on chunks of the table. For partitioned tables, you do not need to rebuild the entire clustered columnstore index because fragmentation is likely to occur in only the partitions that have been modified recently. This is much more practical than rebuilding the entire table.  
  
## <a name="Examples"></a>Examples  
  
### A. Create a New “Empty” Columnstore Table  
To create a new columnstore table as an empty table, use the CREATE TABLE statement with the CLUSTERED COLUMNSTORE INDEX option.  Columnstore tables support the standard table options such as distributed, replicated, and partitioned. They also support the standard table arguments such as default constraints, and collations.  
  
The clustered columnstore index does not affect how the data is distributed to the Compute nodes. Instead, it affects the way the data is stored after it is distributed; the rows within each distribution are stored together as a columnstore. Similarly, replicated tables are stored as columnstore tables on each Compute node.  
  
The following example creates a distributed table with a clustered columnstore index.  
  
```  
--Create a distributed table with a clustered columnstore index.  
CREATE TABLE DistFactTable (  
    colA int CONSTRAINT constraint_colA DEFAULT 0,  
    colB nvarchar(32) COLLATE Frisian_100_CS_AS  
)   
WITH (  
    DISTRIBUTION = HASH ( colB ),  
    CLUSTERED COLUMNSTORE INDEX  
);  
```  
  
The following example creates a replicated table with a clustered columnstore index.  
  
```  
--Create a replicated dimension table with a clustered columnstore index.  
CREATE TABLE RepDimTable (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
)  
WITH (   
    DISTRIBUTION = REPLICATE,  
    CLUSTERED COLUMNSTORE INDEX   
);  
```  
  
### B. Convert a small dimension table from rowstore to columnstore  
This example uses [CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-columnstore-index-sql-server-pdw.md) to convert a small dimension table from a rowstore table to a columnstore table with a clustered columnstore index. This conversion method is a simple way to convert small dimension tables; it is a slow way to convert large tables.  
  
> [!IMPORTANT]  
> For large tables, use CREATE TABLE AS SELECT instead of CREATE COLUMNSTORE INDEX to convert fact tables and large dimension tables from rowstore to columnstore.  
  
To convert a small replicated rowstore table to a columnstore table.  
  
1.  Before you begin, create a small dimension table to use in the example.  
  
    ```  
    --Create a rowstore dimension table with a clustered index and also a non-clustered index.  
    CREATE TABLE MyDimTable (  
        ProductKey [int] NOT NULL,  
        OrderDateKey [int] NOT NULL,  
         DueDateKey [int] NOT NULL,  
         ShipDateKey [int] NOT NULL )  
    )  
    WITH (  
        DISTRIBUTION = REPLICATE,  
        CLUSTERED INDEX ( ProductKey )  
    );  
  
    --Add a non-clustered index.  
    CREATE INDEX my_index ON MyDimTable ( ProductKey, OrderDateKey );  
    ```  
  
2.  Drop all non-clustered indexes from the rowstore table.  
  
    ```  
    --Drop all non-clustered indexes  
    DROP INDEX my_index ON MyDimTable;  
    ```  
  
3.  Drop the clustered index.  
  
    -   Do this only if you want to specify a new name for the index when it is converted to a clustered columnstore index. If you do not drop the clustered index, the new clustered columnstore index will have keep the same name.  
  
        > [!NOTE]  
        > The name of the index might be easier to remember if you use your own name. All rowstore clustered indexes use the default name which is `ClusteredIndex\_<GUID>`.  
  
    ```  
    --Process for dropping a clustered index.  
    --First, look up the name of the clustered rowstore index.  
    --Clustered rowstore indexes always use the DEFAULT name ‘ClusteredIndex_<GUID>’.  
    SELECT i.name   
    FROM sys.indexes i   
    JOIN sys.tables t  
    ON ( i.type_desc = 'CLUSTERED' ) WHERE t.name = 'MyDimTable';  
  
    --Drop the clustered rowstore index.  
    DROP INDEX ClusteredIndex_d473567f7ea04d7aafcac5364c241e09 ON MyDimTable;  
    ```  
  
4.  Convert the rowstore table to a columnstore table with a clustered columnstore index.  
  
    ```  
    --Option 1: Convert to columnstore and name the new clustered columnstore index MyCCI.  
    CREATE CLUSTERED COLUMNSTORE INDEX MyCCI ON MyDimTable;  
  
    --Option 2: Convert to columnstore and use the rowstore clustered   
    --index name for the columnstore clustered index name.  
    --First, look up the name of the clustered rowstore index.  
    SELECT i.name   
    FROM sys.indexes i  
    JOIN sys.tables t   
    ON ( i.type_desc = 'CLUSTERED' )  
    WHERE t.name = 'MyDimTable';  
  
    --Second, create the clustered columnstore index and   
    --Replace ClusteredIndex_d473567f7ea04d7aafcac5364c241e09  
    --with the name of your clustered index.  
    CREATE CLUSTERED COLUMNSTORE INDEX   
    ClusteredIndex_d473567f7ea04d7aafcac5364c241e09  
     ON MyDimTable  
    WITH DROP_EXISTING = ON;  
    ```  
  
### C. Convert a large fact table from rowstore to columnstore  
For best performance, use CREATE TABLE AS SELECT instead of CREATE COLUMNSTORE INDEX to convert fact and large dimension tables from rowstore to columnstore.  
  
The following example creates a new table named `myFactTable`, using the column definitions and data from the source table `dimCustomer`.  
  
```  
USE SQLServerPDW2012;  
CREATE TABLE myTable   
WITH (  
    CLUSTERED COLUMNSTORE INDEX,  
    DISTRIBUTION = HASH (id),  
)  
AS SELECT *   
FROM dimCustomer;  
```  
  
### D. Create several small clustered columnstore indexes on some of the columns.  
The best way to create a clustered columnstore index on only some of the table columns is to create the original table with a clustered columnstore index, and then use CREATE TABLE AS SELECT (CTAS) with the CLUSTERED COLUMNSTORE INDEX option to create an additional table with a clustered columnstore index.  
  
For example, if you have a large table with many columns and plan to use CTAS to create smaller columnstore tables, you will achieve the best performance by first creating the larger table as a columnstore table.  
  
It is faster to create additional clustered columnstore indexes from a columnstore table rather than from a rowstore table.  When the table is already stored on the distributions as a columnstore, the data is already stored and compressed as columns. It is simple to copy the already compressed columns to a new table.  
  
Conversely, if the original table is a rowstore, in order to create a new columnstore table, SQL Server PDW will convert the data to columnstore format for each new table, which takes more time.  
  
Note that creating additional clustered columnstore indexes is only an option if you have additional storage available. This is because each additional clustered columnstore index is on a new columnstore table.  
  
The following example creates a new table with a clustered columnstore index on only two columns. The full table is already a columnstore table with a clustered columnstore index.  
  
```  
--Recommended Method  
--Creates a new fact table with fewer columns than the original fact table.  
--Recommended method because ColumnStoreFactTable already has a clustered columnstore index.  
CREATE TABLE ColumnStoreFactTable2   
AS SELECT id, lastName  
FROM ColumnStoreFactTable  
WITH (  
    DISTRIBUTION = HASH (id),  
    CLUSTERED COLUMNSTORE INDEX  
);  
```  
  
This next example takes more time to complete because the original table is a rowstore table.  
  
```  
CREATE TABLE RowstoreTable (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
)   
WITH ( DISTRIBUTION = HASH (id) );  
  
CREATE TABLE ColumnstoreFactTable2   
AS SELECT id, lastName   
FROM RowstoreTable   
WITH (  
    DISTRIBUTION = HASH (id),  
    CLUSTERED COLUMNSTORE INDEX  
);  
```  
  
### E. Rebuild a specific partition of the columnstore.  
To rebuild a partition of a large clustered columnstore index, use ALTER INDEX REBUILD. You can also use ALTER INDEX with the REBUILD option to rebuild all partitions, or to rebuild a non-partitioned clustered columnstore index.  
  
```  
ALTER INDEX cci_fact3   
ON fact3  
REBUILD PARTITION = 12;  
```  
  
### F. Rebuild the entire clustered columnstore index  
There are two ways to rebuild the full clustered columnstore index. You can use CREATE CLUSTERED COLUMNSTORE INDEX, or ALTER INDEX and the REBUILD option. Both methods achieve the same results.  
  
```  
--Determine the Clustered Columnstore Index name of MyDimTable.  
SELECT i.object_id, i.name, t.object_id, t.name   
FROM sys.indexes i   
JOIN sys.tables t  
ON (i.type_desc = 'CLUSTERED COLUMNSTORE')  
WHERE t.name = 'RowstoreDimTable';  
  
--Rebuild the entire index by using CREATE CLUSTERED INDEX.  
CREATE CLUSTERED COLUMNSTORE INDEX my_CCI   
ON MyFactTable  
WITH ( DROP_EXISTING = ON );  
  
--Rebuild the entire index by using ALTER INDEX and the REBUILD option.  
ALTER INDEX my_CCI  
ON MyFactTable  
REBUILD PARTITION = ALL  
WITH ( DROP_EXISTING = ON );  
```  
  
### G. Convert a columnstore table to a rowstore table with a clustered index  
To convert a columnstore table to a rowstore table with a clustered index, use the CREATE INDEX statement with the DROP_EXISTING option.  
  
```  
CREATE CLUSTERED INDEX ci_MyTable   
ON MyFactTable  
WITH ( DROP EXISTING = ON );  
```  
  
### H. Convert a columnstore table to a rowstore heap  
To convert a columnstore table to a rowstore heap, simply drop the clustered columnstore index.  
  
```  
DROP INDEX MyCCI   
ON MyFactTable;  
```  
