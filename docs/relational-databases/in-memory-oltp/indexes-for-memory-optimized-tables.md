---
title: "Indexes for Memory-Optimized Tables | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "06/12/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: eecc5821-152b-4ed5-888f-7c0e6beffed9
caps.latest.revision: 14
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Indexes for Memory-Optimized Tables
[!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]

  
This article describes the types of indexes that are available for a memory-optimized table. The article:  
  
- Provides short code examples to demonstrate the Transact-SQL syntax.  
- Describes how memory-optimized indexes differ from traditional disk-based indexes.  
- Explains the circumstances when each type of memory-optimized index is best.  
  
  
*Hash* indexes are discussed in more detail in a [closely related article](../../relational-databases/in-memory-oltp/hash-indexes-for-memory-optimized-tables.md).  
  
  
*Columnstore* indexes are discussed in [another article](~/relational-databases/indexes/columnstore-indexes-overview.md).  
  
  
## A. Syntax for memory-optimized indexes  
  
Each CREATE TABLE statement for a memory-optimized table must include between 1 and 8 clauses to declare indexes. The index must be one of the following:  
  
- Hash index.  
- Nonclustered index (meaning the default internal structure of a B-tree).  
  
  
To be declared with the default DURABILITY = SCHEMA_AND_DATA, the memory-optimized table must have a primary key. The PRIMARY KEY NONCLUSTERED clause in the following CREATE TABLE statement satisfies two requirements:  
  
- Provides an index to meet the minimum requirement of one index in the CREATE TABLE statement.  
- Provides the primary key that is required for the SCHEMA_AND_DATA clause.  
  
  
  
    CREATE TABLE SupportEvent  
    (  
        SupportEventId   int NOT NULL  
            PRIMARY KEY NONCLUSTERED,  
        ...  
    )  
        WITH (  
            MEMORY_OPTIMIZED = ON,  
            DURABILITY = SCHEMA_AND_DATA);  
  
  
  
### A.1 Code sample for syntax  
  
This subsection contains a Transact-SQL code block that demonstrates the syntax to create various indexes on a memory-optimized table. The code demonstrates the following:  
  
  
1. Create a memory-optimized table.  
2. Use ALTER TABLE statements to add two indexes.  
3. INSERT a few rows of data.  
  
  
  
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
        DURABILITY = SCHEMA_AND_DATA);  
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
        ('2016-02-25 13:40:41:123', 'Abby', 'Zeke', 2, 'Display problem.'     ),  
        ('2016-02-25 13:40:41:323', 'Ben' , null  , 1, 'Cannot find help.'    ),  
        ('2016-02-25 13:40:41:523', 'Carl', 'Liz' , 2, 'Button is gray.'      ),  
        ('2016-02-25 13:40:41:723', 'Dave', 'Zeke', 2, 'Cannot unhide column.');  
    go  
  
  
  
## B. Nature of memory-optimized indexes  
  
On a memory-optimized table, every index is also memory-optimized. There are several ways in which an index on a memory-optimized index differs from a traditional index on a disk-base table.  
  
Each memory-optimized index exists only in active memory. The index has no representation on the disk.  
  
- Memory-optimized indexes are rebuilt when the database is brought back online.  
  
  
When an SQL UPDATE statement modifies data in a memory-optimized table, corresponding changes to its indexes are not written to the log.  
  
  
The entries in a memory-optimized index contain a direct memory address to the row in the table.  
  
- In contrast, entries in a traditional B-tree index on disk contain a key value that the system must first use to find the memory address to the associated table row.  
  
  
Memory-optimized indexes have no fixed pages as do disk-based indexes.  
  
- They do not accrue the traditional type of fragmentation within a page, so they have no fillfactor.  
  
## C. Duplicate index key values

Duplicate index key values can impact the performance of operations on memory-optimized tables. Large numbers of duplicates (e.g., 100+) make the job of maintaining an index inefficient because duplicate chains must be traversed for most index operations. The impact can be seen in INSERT, UPDATE, and DELETE operations on memory-optimized tables. This problem is more visible in the case of hash indices, due both to the lower cost per operation for hash indices and the interference of large duplicate chains with the hash collision chain. To reduce duplication in an index, use a nonclustered index and add additional columns (for example from the primary key) to the end of the index key to reduce the number of duplicates.

Consider, as an example, a Customers table with a primary key on CustomerId and an index on column CustomerCategoryID. There will typically be many customers in a given category, and thus many duplicate values for a given key in the index on CustomerCategoryID. In this scenario, best practice is to use a nonclustered index on (CustomerCategoryID, CustomerId). This index can be used for queries that use a predicate involving CustomerCategoryID, and does not contain duplication, and thus does not cause inefficiency in index maintenance.

The following query shows the average number of duplicate index key values for the index on `CustomerCategoryID` in table `Sales.Customers`, in the sample database [WideWorldImporters](https://msdn.microsoft.com/library/mt734199(v=sql.1).aspx).

```Transact-SQL
    SELECT AVG(row_count) FROM
	   (SELECT COUNT(*) AS row_count 
	    FROM Sales.Customers
	    GROUP BY CustomerCategoryID) a
```

To evaluate the average number of index key duplicates for your own table and index, replace `Sales.Customers` with your table name, and replace `CustomerCategoryID` with the list of index key columns.

## D. Comparing when to use each index type  
  
  
The nature of your particular queries determines which type of index is the best choice.  

When implementing memory-optimized tables in an existing application, the general recommendation is to start with nonclustered indexes, as their capabilities more closely resemble the capabilities of traditional clustered and nonclustered indexes on disk-based tables. 
  
  
### D.1 Strengths of nonclustered indexes  
  
  
A nonclustered index is preferable over a hash index when:  
  
- Queries have an ORDER BY clause on the indexed column.  
- Queries where only the leading column(s) of a multi-column index is tested.  
- Queries test the indexed column by use of a WHERE clause with:  
  - An inequality: *WHERE StatusCode != 'Done'*  
  - A value range: *WHERE Quantity >= 100*  
  
  
In all the following SELECTs, a nonclustered index is preferable over a hash index:  
  
  
  
    SELECT col2 FROM TableA  
        WHERE StartDate > DateAdd(day, -7, GetUtcDate());  
      
    SELECT col3 FROM TableB  
        WHERE ActivityCode != 5;  
      
    SELECT StartDate, LastName  
        FROM TableC  
        ORDER BY StartDate;  
      
    SELECT IndexKeyColumn2  
        FROM TableD  
        WHERE IndexKeyColumn1 = 42;  
  
  
  
### D.2 Strengths of hash indexes  
  
  
A [hash index](../../relational-databases/in-memory-oltp/hash-indexes-for-memory-optimized-tables.md) is preferable over a nonclustered index when:  
  
- Queries test the indexed columns by use of a WHERE clause with an exact equality on all index key columns, as in the following:  
  
  
  
    SELECT col9 FROM TableZ  
        WHERE Z_Id = 2174;  
  
  
  
### D.3 Summary table to compare index strengths  
  
  
The following table lists all operations that are supported by the different index types.  
  
  
| Operation | Memory-optimized, <br/> hash | Memory-optimized, <br/> nonclustered | Disk-based, <br/> (non)clustered |  
| :-------- | :--------------------------- | :----------------------------------- | :------------------------------------ |  
| Index Scan, retrieve all table rows. | Yes | Yes | Yes |  
| Index seek on equality predicates (=). | Yes <br/> (Full key is required.) | Yes  | Yes |  
| Index seek on inequality and range predicates <br/> (>, <, <=, >=, BETWEEN). | No <br/> (Results in an index scan.) | Yes | Yes |  
| Retrieve rows in a sort order that matches the index definition. | No | Yes | Yes |  
| Retrieve rows in a sort-order that matches the reverse of the index definition. | No | No | Yes |  
  
  
In the table, Yes means that the index can efficiently service the request, and No means that the index cannot efficiently satisfy the request.  
