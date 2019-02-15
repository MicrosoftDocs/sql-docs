---
title: "CREATE TABLE (Azure SQL Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "07/14/2017"
ms.service: sql-data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: ea21c73c-40e8-4c54-83d4-46ca36b2cf73
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# CREATE TABLE (Azure SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Creates a new table in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
 
To understand tables and how to use them, see [Tables in SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-overview/).

NOTE: Discussions about SQL Data Warehouse in this article apply to both SQL Data Warehouse and Parallel Data Warehouse unless otherwise noted. 
 
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

<a name="Syntax"></a>   
## Syntax  
  
```  
-- Create a new table. 
CREATE TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name   
    ( 
      { column_name <data_type>  [ <column_options> ] } [ ,...n ]   
    )  
    [ WITH [ <table_option> [ ,...n ] ) ]  
[;]  
   
<column_options> ::=
    [ COLLATE Windows_collation_name ]  
    [ NULL | NOT NULL ] -- default is NULL  
    [ [ CONSTRAINT constraint_name ] DEFAULT constant_expression  ]
  
<table_option> ::= 
    {   
        CLUSTERED COLUMNSTORE INDEX --default for SQL Data Warehouse 
      | HEAP --default for Parallel Data Warehouse   
      | CLUSTERED INDEX ( { index_column_name [ ASC | DESC ] } [ ,...n ] ) -- default is ASC 
    }  
    { 
        DISTRIBUTION = HASH ( distribution_column_name ) 
      | DISTRIBUTION = ROUND_ROBIN -- default for SQL Data Warehouse
      | DISTRIBUTION = REPLICATE -- default for Parallel Data Warehouse
    }   
    | PARTITION ( partition_column_name RANGE [ LEFT | RIGHT ] -- default is LEFT  
        FOR VALUES ( [ boundary_value [,...n] ] ) )  
  
<data type> ::=   
      datetimeoffset [ ( n ) ]  
    | datetime2 [ ( n ) ]  
    | datetime  
    | smalldatetime  
    | date  
    | time [ ( n ) ]  
    | float [ ( n ) ]  
    | real [ ( n ) ]  
    | decimal [ ( precision [ , scale ] ) ]   
    | numeric [ ( precision [ , scale ] ) ]   
    | money  
    | smallmoney  
    | bigint  
    | int   
    | smallint  
    | tinyint  
    | bit  
    | nvarchar [ ( n | max ) ]  -- max applies only to SQL Data Warehouse 
    | nchar [ ( n ) ]  
    | varchar [ ( n | max )  ] -- max applies only to SQL Data Warehouse  
    | char [ ( n ) ]  
    | varbinary [ ( n | max ) ] -- max applies only to SQL Data Warehouse  
    | binary [ ( n ) ]  
    | uniqueidentifier  
```  

<a name="Arguments"></a>   
## Arguments  
 *database_name*  
 The name of the database that will contain the new table. The default is the current database.  
  
 *schema_name*  
 The schema for the table. Specifying *schema* is optional. If blank, the default schema will be used.  
  
 *table_name*  
 The name of the new table. To create a local temporary table, precede the table name with #.  For explanations and guidance on temporary tables, see [Temporary tables in Azure SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-temporary/). 
 
 *column_name*  
 The name of a table column.
   
### <a name="ColumnOptions"></a> Column options

 `COLLATE` *Windows_collation_name*  
 Specifies the collation for the expression. The collation must be one of the Windows collations supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of Windows collations supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Windows Collation Name (Transact-SQL)](windows-collation-name-transact-sql.md)/).  
  
 `NULL` | `NOT NULL`  
 Specifies whether `NULL` values are allowed in the column. The default is `NULL`.  
  
 [ `CONSTRAINT` *constraint_name* ] `DEFAULT` *constant_expression*  
 Specifies the default column value.  
  
 | Argument | Explanation |
 | -------- | ----------- |
 | *constraint_name* | The optional name for the constraint. The constraint name is unique within the database. The name can be reused in other databases. |
 | *constant_expression* | The default value for the column. The expression must be a literal value or a constant. For example, these constant expressions are allowed: `'CA'`, `4`. These constant expressions aren't allowed: `2+3`, `CURRENT_TIMESTAMP`. |
  

### <a name="TableOptions"></a> Table structure options
For guidance on choosing the type of table, see [Indexing tables in Azure SQL Data Warehouse](https://docs.microsoft.com/azure/sql-data-warehouse/sql-data-warehouse-tables-index/).
  
 `CLUSTERED COLUMNSTORE INDEX`  
Stores the table as a clustered columnstore index. The clustered columnstore index applies to all of the table data. This behavior is the default for [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].   
 
 `HEAP`   
  Stores the table as a heap. This behavior is the default for [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
 `CLUSTERED INDEX` ( *index_column_name* [ ,...*n* ] )  
 Stores the table as a clustered index with one or more key columns. This behavior stores the data by row. Use *index_column_name* to specify the name of one or more key columns in the index.  For more information, see Rowstore Tables in the General Remarks.
 
 `LOCATION = USER_DB`   
 This option is deprecated. It's syntactically accepted, but no longer required and no longer affects behavior.   
  
### <a name="TableDistributionOptions"></a> Table distribution options
To understand how to choose the best distribution method and use distributed tables, see [Distributing tables in Azure SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-distribute/).

`DISTRIBUTION = HASH` ( *distribution_column_name* )   
Assigns each row to one distribution by hashing the value stored in *distribution_column_name*. The algorithm is deterministic, which means it always hashes the same value to the same distribution.  The distribution column should be defined as NOT NULL because all rows that have NULL are assigned to the same distribution.

`DISTRIBUTION = ROUND_ROBIN`   
Distributes the rows evenly across all the distributions in a round-robin fashion. This behavior is the default for [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].

`DISTRIBUTION = REPLICATE`    
Stores one copy of the table on each Compute node. For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] the table is stored on a distribution database on each Compute node. For [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], the table is stored in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] filegroup that spans the Compute node. This behavior is the default for [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].
  
### <a name="TablePartitionOptions"></a> Table partition options
For guidance on using table partitions, see [Partitioning tables in SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-partition/).

 `PARTITION` ( *partition_column_name* `RANGE` [ `LEFT` | `RIGHT` ] `FOR VALUES` ( [ *boundary_value* [,...*n*] ] ))   
Creates one or more table partitions. These partitions are horizontal table slices that allow you to apply operations to subsets of rows regardless of whether the table is stored as a heap, clustered index, or clustered columnstore index. Unlike the distribution column, table partitions don't determine the distribution where each row is stored. Instead, table partitions determine how the rows are grouped and stored within each distribution.  
 
| Argument | Explanation |
| -------- | ----------- |
|*partition_column_name*| Specifies the column that [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] will use to partition the rows. This column can be any data type. [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] sorts the partition column values in ascending order. The low-to-high ordering goes from `LEFT` to `RIGHT` in the `RANGE` specification. |  
| `RANGE LEFT` | Specifies the boundary value belongs to the partition on the left (lower values). The default is LEFT. |
| `RANGE RIGHT` | Specifies the boundary value belongs to the partition on the right (higher values). | 
| `FOR VALUES` ( *boundary_value* [,...*n*] ) | Specifies the boundary values for the partition. *boundary_value* is a constant expression. It can't be NULL. It must either match or be implicitly convertible to the data type of *partition_column_name*. It can't be truncated during implicit conversion so that the size and scale of the value don't match the data type of *partition_column_name*<br></br><br></br>If you specify the `PARTITION` clause, but don't specify a boundary value, [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] will create a partitioned table with one partition. If applicable, you can split the table into two partitions at a later time.<br></br><br></br>If you specify one boundary value, the resulting table has two partitions; one for the values lower than the boundary value and one for the values higher than the boundary value. If you move a partition into a non-partitioned table, the non-partitioned table will receive the data, but will not have the partition boundaries in its metadata.| 
 
 See [Create a partitioned table](#PartitionedTable) in the Examples section.

### <a name="DataTypes"></a> Data types
[!INCLUDE[ssSDW](../../includes/sssdw-md.md)] supports the most commonly used data types. Below is a list of the supported data types along with their details and storage bytes. To better understand data types and how to use them, see [ Data types for tables in SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-data-types).

For a table of data type conversions, see the Implicit Conversions section, of [CAST and CONVERT (Transact-SQL)](https://msdn.microsoft.com/library/ms187928/).

`datetimeoffset` [ ( *n* ) ]  
 The default value for *n* is 7.  
  
 `datetime2` [ ( *n* ) ]  
Same as `datetime`, except that you can specify the number of fractional seconds. The default value for *n* is `7`.  
  
|*n* value|Precision|Scale|  
|--:|--:|-:|  
|`0`|19|0|  
|`1`|21|1|  
|`2`|22|2|  
|`3`|23|3|  
|`4`|24|4|  
|`5`|25|5|  
|`6`|26|6|  
|`7`|27|7|  
  
 `datetime`  
 Stores date and time of day with 19 to 23 characters according to the Gregorian calendar. The date can contain year, month, and day. The time contains hour, minutes, seconds.As an option, you can display three digits for fractional seconds. The storage size is 8 bytes.  
  
 `smalldatetime`  
 Stores a date and a time. Storage size is 4 bytes.  
  
 `date`  
 Stores a date using a maximum of 10 characters for year, month, and day according to the Gregorian calendar. The storage size is 3 bytes. Date is stored as an integer.  
  
 `time` [ ( *n* ) ]  
 The default value for *n* is `7`.  
  
 `float` [ ( *n* ) ]  
 Approximate number data type for use with floating point numeric data. Floating point data is approximate, which means that not all values in the data type range can be represented exactly. *n* specifies the number of bits used to store the mantissa of the `float` in scientific notation. *n* dictates the precision and storage size. If *n* is specified, it must be a value between `1` and `53`. The default value of *n* is `53`.  
  
| *n* value | Precision | Storage size |  
| --------: | --------: | -----------: |  
| 1-24   | 7 digits  | 4 bytes      |  
| 25-53  | 15 digits | 8 bytes      |  
  
 [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] treats *n* as one of two possible values. If `1`<= *n* <= `24`, *n* is treated as `24`. If `25` <= *n* <= `53`, *n* is treated as `53`.  
  
 The [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] `float` data type complies with the ISO standard for all values of *n* from `1` through `53`. The synonym for double precision is `float(53)`.  
  
 `real` [ ( *n* ) ]  
 The definition of real is the same as float. The ISO synonym for `real` is `float(24)`.  
  
 `decimal` [ ( *precision* [ *, scale* ] ) ] | `numeric` [ ( *precision* [ *, scale* ] ) ]  
 Stores fixed precision and scale numbers.  
  
 *precision*  
 The maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point. The precision must be a value from `1` through the maximum precision of `38`. The default precision is `18`.  
  
 *scale*  
 The maximum number of decimal digits that can be stored to the right of the decimal point. *Scale* must be a value from `0` through *precision*. You can only specify *scale* if *precision* is specified. The default scale is `0` and so `0` <= *scale* <= *precision*. Maximum storage sizes vary, based on the precision.  
  
| Precision | Storage bytes  |  
| ---------: |-------------: |  
|  1-9       |             5 |  
| 10-19      |             9 |  
| 20-28      |            13 |  
| 29-38      |            17 |  
  
 `money` | `smallmoney`  
 Data types that represent currency values.  
  
| Data Type | Storage bytes |  
| --------- | ------------: |  
| `money`|8|  
| `smallmoney` |4|  
  
 `bigint` | `int` | `smallint` | `tinyint`  
 Exact-number data types that use integer data. The storage is shown in the following table.  
  
| Data Type | Storage bytes |  
| --------- | ------------: |  
| `bigint`|8|  
| `int` |4|  
| `smallint` |2|  
| `tinyint` |1|  
  
 `bit`  
 An integer data type that can take the value of `1`, `0`, or `NULL. [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] optimizes storage of bit columns. If there are 8 or fewer bit columns in a table, the columns are stored as 1 byte. If there are from 9-16 bit columns, the columns are stored as 2 bytes, and so on.  
  
 `nvarchar` [ ( *n* | `max` ) ]  -- `max` applies only to [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].  
 Variable-length Unicode character data. *n* can be a value from 1 through 4000. `max` indicates that the maximum storage size is 2^31-1 bytes (2 GB). Storage size in bytes is two times the number of characters entered + 2 bytes. The data entered can be zero characters in length.  
  
 `nchar` [ ( *n* ) ]  
 Fixed-length Unicode character data with a length of *n* characters. *n* must be a value from `1` through `4000`. The storage size is two times *n* bytes.  
  
 `varchar` [ ( *n*  | `max` ) ]  -- `max` applies only to [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].   
 Variable-length, non-Unicode character data with a length of *n* bytes. *n* must be a value from `1` to `8000`. `max` indicates that the maximum storage size is 2^31-1 bytes (2 GB).The storage size is the actual length of data entered + 2 bytes.  
  
 `char` [ ( *n* ) ]  
 Fixed-length, non-Unicode character data with a length of *n* bytes. *n* must be a value from `1` to `8000`. The storage size is *n* bytes. The default for *n* is `1`.  
  
 `varbinary` [ ( *n*  | `max` ) ]  -- `max` applies only to [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].  
 Variable-length binary data. *n* can be a value from `1` to `8000`. `max` indicates that the maximum storage size is 2^31-1 bytes (2 GB). The storage size is the actual length of data entered + 2 bytes. The default value for *n* is 7.  
  
 `binary` [ ( *n* ) ]  
 Fixed-length binary data with a length of *n* bytes. *n* can be a value from `1` to `8000`. The storage size is *n* bytes. The default value for *n* is `7`.  
  
 `uniqueidentifier`  
 Is a 16-byte GUID.  
   
<a name="Permissions"></a>  
## Permissions  
 Creating a table requires permission in the `db_ddladmin` fixed database role, or:
 - `CREATE TABLE` permission on the database
 - `ALTER SCHEMA` permission on the schema that will contain the table. 

Creating a partitioned table requires permission in the `db_ddladmin` fixed database role, or

- `ALTER ANY DATASPACE` permission
  
 The login that creates a local temporary table receives `CONTROL`, `INSERT`, `SELECT`, and `UPDATE` permissions on the table.  
 
<a name="GeneralRemarks"></a>  
## General Remarks  
 
For minimum and maximum limits, see [SQL Data Warehouse capacity limits](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-service-capacity-limits/). 
 
### Determining the number of table partitions
Each user-defined table is divided into multiple smaller tables that are stored in separate locations called distributions. [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] uses 60 distributions. In [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], the number of distributions depends on the number of Compute nodes.
 
Each distribution contains all table partitions. For example, if there are 60 distributions and four table partitions plus one empty partition, there will be 300 partitions (5 x 60= 300). If the table is a clustered columnstore index, there will be one columnstore index per partition, which means you'll have 300 columnstore indexes.

We recommend using fewer table partitions to ensure each columnstore index has enough rows to take advantage of the benefits of columnstore indexes. For more information, see [Partitioning tables in SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-partition/) and [Indexing tables in SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-index/)  

  
 ### Rowstore table (heap or clustered index)  
 A rowstore table is a table stored in row-by-row order. It's a heap or clustered index. [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] creates all rowstore tables with page compression; this behavior isn't user-configurable.   
 
 ### Columnstore table (columnstore index)
A columnstore table is a table stored in column-by-column order. The columnstore index is the technology that manages data stored in a columnstore table.  The clustered columnstore index doesn't affect how data are distributed, rather, it affects how the data are stored within each distribution.

To change a rowstore table to a columnstore table, drop all existing indexes on the table and create a clustered columnstore index. For an example, see [CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md).

For more information, see these articles:
- [Columnstore indexes versioned feature summary](https://msdn.microsoft.com/library/dn934994/)
- [Indexing tables in SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-data-warehouse-tables-index/)
- [Columnstore Indexes Guide](~/relational-databases/indexes/columnstore-indexes-overview.md) 
 
<a name="LimitationsRestrictions"></a>  
## Limitations and Restrictions  
 You can't define a DEFAULT constraint on a distribution column.  
  
 ### Partitions
 When using partitions, the partition column can't have a Unicode-only collation. For example, the following statement fails.  
  
 `CREATE TABLE t1 ( c1 varchar(20) COLLATE Divehi_90_CI_AS_KS_WS) WITH (PARTITION (c1 RANGE FOR VALUES (N'')))`  
 
 If *boundary_value* is a literal value that must be implicitly converted to the data type in *partition_column_name*, a discrepancy will occur. The literal value is displayed through the [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] system views, but the converted value is used for [!INCLUDE[tsql](../../includes/tsql-md.md)] operations. 
 
  
 ### Temporary tables
 Global temporary tables that begin with ## aren't supported.  
  
 Local temporary tables have the following limitations and restrictions:  
  
-   They're visible only to the current session. [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] drops them automatically at the end of the session. To drop them explicitly, use the DROP TABLE statement.   
-   They can't be renamed. 
-   They can't have partitions or views.  
-   Their permissions can't be changed. `GRANT`, `DENY`, and `REVOKE` statements can't be used with local temporary tables.   
-   Database console commands are blocked for temporary tables.   
-   If more than one local temporary table is used within a batch, each must have a unique name. If multiple sessions are running the same batch and creating the same local temporary table, [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] internally appends a numeric suffix to the local temporary table name to maintain a unique name for each local temporary table.  
    
<a name="LockingBehavior"></a>   
## Locking behavior  
 Takes an exclusive lock on the table. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION objects.  
 

<a name="ExamplesColumn"></a>   
## Examples for columns

### <a name="ColumnCollation"></a> A. Specify a column collation 
 In the following example, the table `MyTable` is created with two different column collations. By default, the column, `mycolumn1`, has the default collation Latin1_General_100_CI_AS_KS_WS. The column, `mycolumn2` has the collation Frisian_100_CS_AS.  
  
```  
CREATE TABLE MyTable   
  (  
    mycolumnnn1 nvarchar,  
    mycolumn2 nvarchar COLLATE Frisian_100_CS_AS )  
WITH ( CLUSTERED COLUMNSTORE INDEX )  
;  
  
```  
  
### <a name="DefaultConstraint"></a> B. Specify a DEFAULT constraint for a column  
 The following example shows the syntax to specify a default value for a column. The colA column has a default constraint named constraint_colA  and a default value of 0.  
  
```  
  
CREATE TABLE MyTable   
  (  
    colA int CONSTRAINT constraint_colA DEFAULT 0,  
    colB nvarchar COLLATE Frisian_100_CS_AS   
  )  
WITH ( CLUSTERED COLUMNSTORE INDEX )  
;  
```  

<a name="ExamplesTemporaryTables"></a> 
## Examples for temporary tables

### <a name="TemporaryTable"></a> C. Create a local temporary table  
 The following example creates a local temporary table named #myTable. The table is specified with a three-part name, which starts with a #.   
  
```  
CREATE TABLE AdventureWorks.dbo.#myTable   
  (  
   id int NOT NULL,  
   lastName varchar(20),  
   zipCode varchar(6)  
  )  
WITH  
  (   
    DISTRIBUTION = HASH (id),  
    CLUSTERED COLUMNSTORE INDEX   
  )  
;  
```

<a name="ExTableStructure"></a>  
## Examples for table structure

### <a name="ClusteredColumnstoreIndex"></a> D. Create a table with a clustered columnstore index  
 The following example creates a distributed table with a clustered columnstore index. Each distribution will be stored as a columnstore.  
  
 The clustered columnstore index doesn't affect how the data is distributed; data is always distributed by row. The clustered columnstore index affects how the data is stored within each distribution.  
  
```  
  
CREATE TABLE MyTable   
  (  
    colA int CONSTRAINT constraint_colA DEFAULT 0,  
    colB nvarchar COLLATE Frisian_100_CS_AS   
  )  
WITH   
  (   
    DISTRIBUTION = HASH ( colB ),  
    CLUSTERED COLUMNSTORE INDEX   
  )  
;  
```  
 
<a name="ExTableDistribution"></a> 
## Examples for table distribution

### <a name="RoundRobin"></a> E. Create a ROUND_ROBIN table  
 The following example creates a ROUND_ROBIN table with three columns and without partitions. The data is spread across all distributions. The table is created with a CLUSTERED COLUMNSTORE INDEX, which gives better performance and data compression than a heap or rowstore clustered index.  
  
```  
CREATE TABLE myTable   
  (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
  )  
WITH ( CLUSTERED COLUMNSTORE INDEX );  
```  
  
### <a name="HashDistributed"></a> F. Create a hash-distributed table  
 The following example creates the same table as the previous example. However, for this table, rows are distributed (on the `id` column) instead of randomly spread like a ROUND_ROBIN table. The table is created with a CLUSTERED COLUMNSTORE INDEX, which gives better performance and data compression than a heap or rowstore clustered index.  
  
```  
CREATE TABLE myTable   
  (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
  )  
WITH  
  (   
    DISTRIBUTION = HASH (id),   
    CLUSTERED COLUMNSTORE INDEX  
  );  
```  
  
### <a name="Replicated"></a> G. Create a replicated table  
 The following example creates a replicated table similar to the previous examples. Replicated tables are copied in full to each Compute node. With this copy on each Compute node, data movement is reduced for queries. This example is created with a CLUSTERED INDEX, which gives better data compression than a heap. A heap may not contain enough rows to achieve good CLUSTERED COLUMNSTORE INDEX compression.  
  
```  
CREATE TABLE myTable   
  (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
  )  
WITH  
  (   
    DISTRIBUTION = REPLICATE,   
    CLUSTERED INDEX (lastName)  
  );  
```  

<a name="ExTablePartitions"></a> 
## Examples for table partitions

###  <a name="PartitionedTable"></a> H. Create a partitioned table  
 The following example creates the same table as shown in example A, with the addition of RANGE LEFT partitioning on the `id` column. It specifies four partition boundary values, which results in five partitions.  
  
```  
CREATE TABLE myTable   
  (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode int)  
WITH   
  (   
  
    PARTITION ( id RANGE LEFT FOR VALUES (10, 20, 30, 40 )),  
    CLUSTERED COLUMNSTORE INDEX      
  )  
;  
```  
  
 In this example, data will be sorted into the following partitions:  
  
-   Partition 1: col <= 10   
-   Partition 2: 10 < col <= 20   
-   Partition 3: 20 < col <= 30   
-   Partition 4: 30 < col <= 40   
-   Partition 5: 40 < col  
  
 If this same table was partitioned RANGE RIGHT instead of RANGE LEFT (default), the data will be sorted into the following partitions:  
  
-   Partition 1: col < 10  
-   Partition 2: 10 <= col < 20   
-   Partition 3: 20 <= col < 30    
-   Partition 4: 30 <= col < 40   
-   Partition 5: 40 <= col  
  
### <a name="OnePartition"></a> I. Create a partitioned table with one partition  
 The following example creates a partitioned table with one partition. It doesn't specify any boundary value, which results in one partition.  
  
```  
CREATE TABLE myTable (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode int)  
WITH   
    (   
      PARTITION ( id RANGE LEFT FOR VALUES ( )),  
      CLUSTERED COLUMNSTORE INDEX  
    )  
;  
```  
  
### <a name="DatePartition"></a> J. Create a table with date partitioning  
 The following example creates a new table named `myTable`, with partitioning on a `date` column. By using RANGE RIGHT and dates for the boundary values, it puts a month of data in each partition.  
  
```  
CREATE TABLE myTable (  
    l_orderkey      bigint,       
    l_partkey       bigint,                                             
    l_suppkey       bigint,                                           
    l_linenumber    bigint,        
    l_quantity      decimal(15,2),  
    l_extendedprice decimal(15,2),  
    l_discount      decimal(15,2),  
    l_tax           decimal(15,2),  
    l_returnflag    char(1),  
    l_linestatus    char(1),  
    l_shipdate      date,  
    l_commitdate    date,  
    l_receiptdate   date,  
    l_shipinstruct  char(25),  
    l_shipmode      char(10),  
    l_comment       varchar(44))  
WITH   
  (   
    DISTRIBUTION = HASH (l_orderkey),  
    CLUSTERED COLUMNSTORE INDEX,  
    PARTITION ( l_shipdate  RANGE RIGHT FOR VALUES   
      (  
        '1992-01-01','1992-02-01','1992-03-01','1992-04-01','1992-05-01',
        '1992-06-01','1992-07-01','1992-08-01','1992-09-01','1992-10-01',
        '1992-11-01','1992-12-01','1993-01-01','1993-02-01','1993-03-01',
        '1993-04-01','1993-05-01','1993-06-01','1993-07-01','1993-08-01',
        '1993-09-01','1993-10-01','1993-11-01','1993-12-01','1994-01-01',
        '1994-02-01','1994-03-01','1994-04-01','1994-05-01','1994-06-01',
        '1994-07-01','1994-08-01','1994-09-01','1994-10-01','1994-11-01',
        '1994-12-01'  
      ))
  );  
```  
  
<a name="SeeAlso"></a>    
## See also 
 
 [CREATE TABLE AS SELECT &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)   
 [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
