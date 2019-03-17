---
title: "Indexes for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "11/28/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: eecc5821-152b-4ed5-888f-7c0e6beffed9
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Indexes on Memory-Optimized Tables
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

All memory-optimized tables must have at least one index, because it is the indexes that connect the rows together. On a memory-optimized table, every index is also memory-optimized. There are several ways in which an index on a memory-optimized index differs from a traditional index on a disk-base table:  

- Data rows are not stored on pages, so there is no collection of pages or extents, no partitions or allocation units that can be referenced to get all the pages for a table. There is the concept of index pages for one of the available types of indexes, but they are stored differently than indexes for disk-based tables. They do not accrue the traditional type of fragmentation within a page, so they have no fillfactor.
- Changes made to indexes on memory-optimized tables during data manipulation are never written to disk. Only the data rows, and changes to the data, are written to the transaction log. 
- Memory-optimized indexes are rebuilt when the database is brought back online. 

All indexes on memory-optimized tables are created based on the index definitions during database recovery.

The index must be one of the following:  
  
- Hash index  
- Memory-optimized Nonclustered index (meaning the default internal structure of a B-tree) 
  
*Hash* indexes are discussed in more detail in [Hash Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#hash_index).
*Nonclustered* indexes are discussed in more detail in [Nonclustered Index for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#inmem_nonclustered_index).  
*Columnstore* indexes are discussed in [another article](../../relational-databases/indexes/columnstore-indexes-overview.md).  

## Syntax for memory-optimized indexes  
  
Each CREATE TABLE statement for a memory-optimized table must include an index, either explicitly through an INDEX or implicitly through a PRIMAY KEY or UNIQUE constraint.
  
To be declared with the default DURABILITY = SCHEMA\_AND_DATA, the memory-optimized table must have a primary key. The PRIMARY KEY NONCLUSTERED clause in the following CREATE TABLE statement satisfies two requirements:  
  
- Provides an index to meet the minimum requirement of one index in the CREATE TABLE statement.  
- Provides the primary key that is required for the SCHEMA\_AND_DATA clause.  

    ```sql
    CREATE TABLE SupportEvent  
    (  
        SupportEventId   int NOT NULL  
            PRIMARY KEY NONCLUSTERED,  
        ...  
    )  
        WITH (  
            MEMORY_OPTIMIZED = ON,  
            DURABILITY = SCHEMA\_AND_DATA);  
    ```
> [!NOTE]  
> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] have a limit of 8 indexes per memory-optimized table or table type. 
> Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], there is no longer a limit on the number of indexes specific to memory-optimized tables and table types.
  
### Code sample for syntax  
  
This subsection contains a Transact-SQL code block that demonstrates the syntax to create various indexes on a memory-optimized table. The code demonstrates the following:  
  
1. Create a memory-optimized table.  
2. Use ALTER TABLE statements to add two indexes.  
3. INSERT a few rows of data.  
   
    ```sql
    DROP TABLE IF EXISTS SupportEvent;  
    go  

    CREATE TABLE SupportEvent  
    (  
        SupportEventId   int               not null   identity(1,1)  
        PRIMARY KEY NONCLUSTERED,  

        StartDateTime        datetime2     not null,  
        CustomerName         nvarchar(16)  not null,  
        SupportEngineerName  nvarchar(16)      null,  
        Priority             int               null,  
        Description          nvarchar(64)      null  
    )  
        WITH (  
        MEMORY_OPTIMIZED = ON,  
        DURABILITY = SCHEMA\_AND_DATA);  
    go  
        
        --------------------  
        
    ALTER TABLE SupportEvent  
        ADD CONSTRAINT constraintUnique_SDT_CN  
        UNIQUE NONCLUSTERED (StartDateTime DESC, CustomerName);  
    go  

    ALTER TABLE SupportEvent  
        ADD INDEX idx_hash_SupportEngineerName  
        HASH (SupportEngineerName) WITH (BUCKET_COUNT = 64);  -- Nonunique.  
    go  
        
        --------------------  
        
    INSERT INTO SupportEvent  
        (StartDateTime, CustomerName, SupportEngineerName, Priority, Description)  
        VALUES  
        ('2016-02-23 13:40:41:123', 'Abby', 'Zeke', 2, 'Display problem.'     ),  
        ('2016-02-24 13:40:41:323', 'Ben' , null  , 1, 'Cannot find help.'    ),  
        ('2016-02-25 13:40:41:523', 'Carl', 'Liz' , 2, 'Button is gray.'      ),  
        ('2016-02-26 13:40:41:723', 'Dave', 'Zeke', 2, 'Cannot unhide column.');  
    go 
    ``` 
  
## Duplicate index key values

Duplicate index key values can impact the performance of operations on memory-optimized tables. Large numbers of duplicates (e.g., 100+) make the job of maintaining an index inefficient because duplicate chains must be traversed for most index operations. The impact can be seen in `INSERT`, `UPDATE`, and `DELETE` operations on memory-optimized tables. 

This problem is more visible in the case of hash indexes, due both to the lower cost per operation for hash indexes and the interference of large duplicate chains with the hash collision chain. To reduce duplication in an index, use a nonclustered index and add additional columns (for example from the primary key) to the end of the index key to reduce the number of duplicates. For more information about hash collisions, see [Hash Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#hash_index).

For example, consider a `Customers` table with a primary key on `CustomerId` and an index on column `CustomerCategoryID`. There will typically be many customers in a given category, and thus many duplicate values for a given key in the index on CustomerCategoryID. In this scenario, best practice is to use a nonclustered index on `(CustomerCategoryID, CustomerId)`. This index can be used for queries that use a predicate involving `CustomerCategoryID`, and does not contain duplication, and therefore does not cause inefficiency in index maintenance.

The following query shows the average number of duplicate index key values for the index on `CustomerCategoryID` in table `Sales.Customers`, in the sample database [WideWorldImporters](../../sample/world-wide-importers/wide-world-importers-documentation.md).

```sql
SELECT AVG(row_count) FROM
    (SELECT COUNT(*) AS row_count 
	    FROM Sales.Customers
	    GROUP BY CustomerCategoryID) a
```

To evaluate the average number of index key duplicates for your own table and index, replace `Sales.Customers` with your table name, and replace `CustomerCategoryID` with the list of index key columns.

## Comparing when to use each index type  
  
The nature of your particular queries determines which type of index is the best choice.  

When implementing memory-optimized tables in an existing application, the general recommendation is to start with nonclustered indexes, as their capabilities more closely resemble the capabilities of traditional clustered and nonclustered indexes on disk-based tables. 
  
### Recommendations for nonclustered index use  
  
A nonclustered index is preferable over a hash index when:  
  
- Queries have an `ORDER BY` clause on the indexed column.  
- Queries where only the leading column(s) of a multi-column index is tested.  
- Queries test the indexed column by use of a `WHERE` clause with:  
  - An inequality: `WHERE StatusCode != 'Done'`  
  - A value range scan: `WHERE Quantity >= 100`  
  
In all the following SELECTs, a nonclustered index is preferable over a hash index:  

```sql
SELECT CustomerName, Priority, Description 
FROM SupportEvent  
WHERE StartDateTime > DateAdd(day, -7, GetUtcDate());  
    
SELECT CustomerName, Priority, Description 
FROM SupportEvent  
WHERE CustomerName != 'Ben';  
    
SELECT StartDateTime, CustomerName  
FROM SupportEvent  
ORDER BY StartDateTime;  
    
SELECT CustomerName  
FROM SupportEvent  
WHERE StartDateTime = '2016-02-26';  
```
  
### Recommendations for hash index use   
  
[Hash indexes](../../relational-databases/sql-server-index-design-guide.md#hash_index) are primarily used for point lookups and not for range scans.

A hash index is preferable over a nonclustered index when queries use equality predicates, and the `WHERE` clause maps to all index key columns, as in the following example:  
  
```sql
SELECT CustomerName 
FROM SupportEvent  
WHERE SupportEngineerName = 'Liz';
```  

### Multi-column index  
  
A multi-column index could be a nonclustered index or a hash index. Suppose the index columns are col1 and col2. Given the following `SELECT` statement, only the nonclustered index would be useful to the query optimizer:  
  
```sql
SELECT col1, col3  
FROM MyTable_memop  
WHERE col1 = 'dn';  
```

The hash index needs the `WHERE` clause to specify an equality test for each of the columns in its key. Else the hash index is not useful to the query optimizer.  
  
Neither index type is useful if the `WHERE` clause specifies only the second column in the index key.  

### Summary table to compare index use scenarios  
  
The following table lists all operations that are supported by the different index types. *Yes* means that the index can efficiently service the request, and *No* means that the index cannot efficiently satisfy the request. 
  
| Operation | Memory-optimized, <br/> hash | Memory-optimized, <br/> nonclustered | Disk-based, <br/> (non)clustered |  
| :-------- | :--------------------------- | :----------------------------------- | :------------------------------------ |  
| Index Scan, retrieve all table rows. | Yes | Yes | Yes |  
| Index seek on equality predicates (=). | Yes <br/> (Full key is required.) | Yes  | Yes |  
| Index seek on inequality and range predicates <br/> (>, <, <=, >=, `BETWEEN`). | No <br/> (Results in an index scan.) | Yes <sup>1</sup> | Yes |  
| Retrieve rows in a sort order that matches the index definition. | No | Yes | Yes |  
| Retrieve rows in a sort-order that matches the reverse of the index definition. | No | No | Yes |  

<sup>1</sup> For a memory-optimized Nonclustered index, the full key is not required to perform an index seek.  

## Automatic index and statistics management

Leverage solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, amongst other parameters, and update statistics with a linear threshold.

## <a name="Additional_Reading"></a> See Also   
 [SQL Server Index Design Guide](../../relational-databases/sql-server-index-design-guide.md)   
 [Hash Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#hash_index)   
 [Nonclustered Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#inmem_nonclustered_index)    
 [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag)  
