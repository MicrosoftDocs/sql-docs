---
title: "CREATE TABLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 39c19ea5-66db-413f-a976-1dba044804cd
caps.latest.revision: 151
author: BarbKess
---
# CREATE TABLE (SQL Server PDW)
Creates a new table in SQL Server PDW. Use the CLUSTERED COLUMNSTORE INDEX table option to store the table as an xVelocity memory-optimized clustered (and updateable) columnstore index. Use additional table options to specify the distribution method and the partitioning scheme.  
  
> [!NOTE]  
> We view the clustered columnstore index as the standard for storing tables in SQL Server PDW, and expect it will be used in most scenarios.  
  
For more information, see [Clustered Columnstore Indexes &#40;SQL Server PDW&#41;](../sqlpdw/clustered-columnstore-indexes-sql-server-pdw.md).  
  
**For copying data**, use the CREATE TABLE AS SELECT (CTAS) statement to create and populate a table with the results from a SELECT statement. For example, you can use the CTAS statement to redistribute a table using a different distribution column. For more information, see [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md).  
  
**For storing data**, replicate the table to each Compute node, or hash-distribute the table across all of the Compute nodes. To distribute a table, you must specify a distribution column; SQL Server PDW assigns each row to be stored in one distribution of one Compute node by hashing the distribution column value. For more information about distributed and replicated tables, see [Distributed and Replicated Tables &#40;SQL Server PDW&#41;](../sqlpdw/distributed-and-replicated-tables-sql-server-pdw.md).  
  
**For managing data**, create table partitions that are logical subsets of rows tracked with metadata. To partition a table, you specify one partitioning column and a set of boundary values. This does not affect which distribution or Compute node where the data are stored. Instead, it allows you to perform data management operations on logical subsets of the table. For example, if you have monthly data, you can partition the table by month, and then archive an older month by switching its partitions to an archive table.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
Create a new table.  
CREATE TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name   
    (   
        { column_name <data_type>   
        [ COLLATE Windows_collation_name ]  
        [ NULL | NOT NULL ]   
        [ [ CONSTRAINT constraint_name ] DEFAULT constant_expression  ] }  
        [ ,...n ]   
    )  
    [ WITH ( <table_option> [ ,...n ] ) ]  
[;]  
  
Create a new temporary table.  
CREATE TABLE [ database_name . [ dbo ] . | dbo. ] #table_name   
    (   
        { column_name <data_type>   
        [ COLLATE Windows_collation_name ]  
        [ NULL | NOT NULL ] }   
        [ ,...n ]   
    )  
    WITH ( LOCATION = USER_DB [, <table_option> [ ,...n ] ] )  
[;]  
  
<table_option> ::=CLUSTERED COLUMNSTORE INDEX  
    | HEAP  
    | CLUSTERED INDEX ( { index_column_name [ ASC | DESC ] } [ ,...n ] )   
    | DISTRIBUTION = { HASH ( distribution_column_name ) | REPLICATE | ROUND_ROBIN }   
    | PARTITION ( partition_column_name RANGE [ LEFT | RIGHT ]  
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
    | money  
    | smallmoney  
    | bigint  
    | int   
    | smallint  
    | tinyint  
    | bit  
    | nvarchar [ ( n ) ]  
    | nchar [ ( n ) ]  
    | varchar [ ( n ) ]  
    | char [ ( n ) ]  
    | varbinary [ ( n ) ]  
    | binary [ ( n ) ]  
```  
  
## Arguments  
*database_name*  
The name of the database that will contain the new table. The default is the current database.  
  
*schema_name*  
The schema for the table. Specifying *schema* is optional. If blank, the users default schema will be used.  
  
*table_name*  
The name of the new table. To create a local temporary table, precede the table name with #.  
  
For more details on permitted table names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md)  
  
For more information about temporary tables, see [tempdb Database &#40;SQL Server PDW&#41;](../sqlpdw/tempdb-database-sql-server-pdw.md).  
  
*column_name*  
The name of a table column. For details on permitted column names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
COLLATE *Windows_collation_name*  
Specifies the collation for the expression. The collation must be one of the Windows collations supported by SQL Server. For a list of Windows collations supported by SQL Server, see [Windows Collation Name (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188046(v=sql11).aspx).  
  
The default collation is the appliance-level collation, Latin1_General_100_CI_AS_KS_WS. For more information about using collations in SQL Server PDW, see [Collations &#40;SQL Server PDW&#41;](../sqlpdw/collations-sql-server-pdw.md)  
  
**NULL** | NOT NULL  
Specifies whether NULL values are allowed in the column. The default is NULL.  
  
[ CONSTRAINT *constraint_name* ] DEFAULT *constant_expression*  
Specifies the default column value.  
  
*constraint_name*  
The optional name for the constraint. The constraint name is unique within the database. The name can be re-used in other databases.  
  
*constant_expression*  
The default value for the column. The expression must be a literal value or a  a constant.  
  
Constant expression examples - allowed:  
  
-   ‘CA’  
  
-   4  
  
Constant expression examples – not allowed:  
  
-   ‘CA’ + ‘1’  
  
-   2+3  
  
-   CURRENT_TIMESTAMP  
  
<data_type>  
**datetimeoffset** [ ( *n* ) ]  
The default value for *n* is 7.  
  
**datetime2** [ ( *n* ) ]  
Same as **datetime**, except that you can specify the number of fractional seconds. The default value for *n* is 7.  
  
|*n* value|Precision|Scale|  
|-------------|-------------|---------|  
|**0**|19|0|  
|**1**|21|1|  
|**2**|22|2|  
|**3**|23|3|  
|**4**|24|4|  
|**5**|25|5|  
|**6**|26|6|  
|**7**|27|7|  
  
**datetime**  
Stores date and time of day with 19 to 23 characters according to the Gregorian calendar. The date can contain year, month, and day. The time contains hour, minutes, seconds.As an option, you can display three digits for fractional seconds. The storage size is 8 bytes.  
  
**smalldatetime**  
Stores a date and a time. Storage size is 4 bytes.  
  
**date**  
Stores a date using a maximum of 10 characters for year, month, and day according to the Gregorian calendar. The storage size is 3 bytes. Date is stored as an integer.  
  
**time** [ ( *n* ) ]  
The default value for *n* is 7.  
  
**float** [ ( *n* ) ]  
Approximate number data type for use with floating point numeric data. Floating point data is approximate, which means that not all values in the data type range can be represented exactly. *n* specifies the number of bits used to store the mantissa of the **float** number in scientific notation. Therefore, n dictates the precision and storage size. If *n* is specified, it must be a value between **1** and **53**. The default value of *n* is **53**.  
  
|*n* value|Precision|Storage size|  
|-------------|-------------|----------------|  
|**1-24**|7 digits|4 bytes|  
|**25-53**|15 digits|8 bytes|  
  
SQL Server PDW treats *n* as one of two possible values. If **1**<=n<=**24**, *n* is treated as **24**. If **25**<=n<=**53**, *n* is treated as **53**.  
  
The SQL Server PDW **float** data type complies with the ISO standard for all values of *n* from **1** through **53**. The synonym for **double precision** is **float(53)**.  
  
**real** [ ( *n* ) ]  
The definition of real is the same as float. The ISO synonym for **real** is **float(24)**.  
  
**decimal** [ ( *precision* [ *, scale* ] **)** ]  
Stores fixed precision and scale numbers.  
  
*precision*  
The maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point. The precision must be a value from 1 through the maximum precision of 38. The default precision is 18.  
  
*scale*  
The maximum number of decimal digits that can be stored to the right of the decimal point. *scale* must be a value from 0 through *precision*. Scale can only be specified if precision is specified. The default scale is 0; therefore, 0 <= *scale* <= *precision*. Maximum storage sizes vary, based on the precision.  
  
|Precision|Storage bytes|  
|-------------|-----------------|  
|1 - 9|5|  
|10-19|9|  
|20-28|13|  
|29-38|17|  
  
**money** | **smallmoney**  
Data types that represent currency values.  
  
|Data Type|Storage bytes|  
|-------------|-----------------|  
|**money**|8|  
|**smallmoney**|4|  
  
**bigint** | **int** | **smallint** | **tinyint**  
Exact-number data types that use integer data. The storage is shown in the following table.  
  
|Data Type|Storage bytes|  
|-------------|-----------------|  
|**bigint**|8|  
|**int**|4|  
|**smallint**|2|  
|**tinyint**|1|  
  
**bit**  
An integer data type that can take the value of 1, 0, or NULL. SQL Server PDW optimizes storage of bit columns. If there are 8 or fewer bit columns in a table, the columns are stored as 1 byte. If there are from 9-16 bit columns, the columns are stored as 2 bytes, and so on.  
  
**nvarchar** [ ( *n* ) ]  
Variable-length Unicode character data. *n* can be a value from 1 through 4000. Storage size in bytes is two times the number of characters entered + 2 bytes. The data entered can be 0 characters in length.  
  
**nchar** [ ( *n* ) ]  
Fixed-length Unicode character data with a length of *n* characters. *n* must be a value from 1 through 4000. The storage size is two times *n* bytes.  
  
**varchar** [ ( *n* ) ]  
Variable-length, non-Unicode character data with a length of *n* bytes. *n* must be a value from 1 to 8000. The storage size is the actual length of data entered + 2 bytes.  
  
**char** [ ( *n* ) ]  
Fixed-length, non-Unicode character data with a length of *n* bytes. *n* must be a value from 1 to 8000. The storage size is *n* bytes. The default for *n* is 1.  
  
**varbinary** [ ( *n* ) ]  
Variable-length binary data. *n* can be a value from 1 to 8000. The storage size is the actual length of data entered + 2 bytes. The default value for *n* is 7.  
  
**binary** [ ( *n* ) ]  
Fixed-length binary data with a length of *n* bytes. *n* can be a value from 1 to 8000. The storage size is *n* bytes. The default value for *n* is 7.  
  
For a table of data type conversions, see the Implicit Conversions section, of [CAST and CONVERT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187928.aspx).  
  
AS SELECT *select_criteria*  
Populates the new table with the results from a SELECT statement. *select_criteria* is the query that determines which data to copy to the new table. For information about SELECT statements, see [SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md).  
  
LOCATION = USER_DB  
This option is no longer required and no longer affects behavior.  Avoid using this option in preparation for eventual deprecation.  
  
**CLUSTERED COLUMNSTORE INDEX**  
Creates an xVelocity memory-optimized clustered columnstore index on the table. The clustered columnstore index applies to all of the table data.  
  
For more information, see **Columnstore Tables** in the General Remarks.  
  
CLUSTERED INDEX ( *index_column_name* [,…n] )  
Creates a rowstore clustered index on one or more key columns.  This stores the data by row. Use *index_column_name* to specify the name of one or more key columns in the index.  
  
For more information, see **Rowstore Tables** in the General Remarks.  
  
DISTRIBUTION = { HASH ( *distribution_column_name* ) | **REPLICATE** }  
Determines whether the table is a distributed table or a replicated table. If omitted, the table is replicated.  
  
HASH ( *distribution_column_name* )  
Creates a distributed table, in which the rows are spread across the Compute nodes and stored into individual parallel processing units called distributions. The HASH algorithm assigns each row to one distribution by hashing the value in *distribution_column_name*. In most cases, the distribution column should not be nullable. For more information about how to choose a distribution column, see [Distributed and Replicated Tables &#40;SQL Server PDW&#41;](../sqlpdw/distributed-and-replicated-tables-sql-server-pdw.md).  
  
**REPLICATE**  
Stores a copy of the table in full on each Compute node. Within each Compute node, the table is stored in a SQL Server filegroup that spans the Compute node..  
  
ROUND_ROBIN  
Stores the rows evenly across the Compute nodes. There is no particular distribution column.  This is recommended for quick migrations where you don't know which distribution column to choose.    Joins and aggregations can be slow with this distribution method.  
  
PARTITION ( *partition_column_name* RANGE [ **LEFT** | RIGHT ] FOR VALUES ( [ *boundary_value* [,...n] ] ))  
Creates one or more table partitions, which are horizontal table slices that allow you to perform operations on subsets of rows. Unlike the distribution column, table partitions do not determine the distribution where each row is stored. Instead, table partitions determine how the rows are grouped and stored within each distribution.  
  
*partition_column_name*  
Specifies the column  that SQL Server PDW will use to partition the rows. This column can be any data type. SQL Server PDW sorts the partition column values in ascending order. The low-to-high ordering goes from LEFT to RIGHT for the purpose of the RANGE specification.  
  
RANGE [ **LEFT** or RANGE RIGHT ]  
Specifies whether the boundary value belongs to the partition on the left (lower values) or the partition on the right (higher values). The default is LEFT.  
  
FOR VALUES( *boundary_value* \[E. Create a partitioned table](#PartitionExample) in the [Examples](#Examples) below.  
  
## Permissions  
Requires **CREATE TABLE** permission on the database and **ALTER SCHEMA** permission on the schema that will contain the table. Or requires membership in the **db_ddladmin** fixed database role. Creating a partitioned table requires the **ALTER ANY DATASPACE** permission or membership in the **db_ddladmin** fixed database role.  
  
The login that creates a local temporary table receives CONTROL, INSERT, SELECT and UPDATE permissions to the table.  
  
## <a name="GeneralRemarks"></a>General Remarks  
**Rowstore Tables**  
  
-   A table created without a clustered columnstore index is stored by row, and is called a rowstore table.  
  
-   A rowstore table can have 1 clustered index and up to 999 nonclustered indexes at the same time.  
  
-   Clustered and nonclustered indexes on a rowstore table can be dropped at any time.  
  
-   You can change a rowstore table to a columnstore table by dropping all existing indexes on the table and creating a clustered columnstore index. For more information, see  [CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-columnstore-index-sql-server-pdw.md).  
  
-   All rowstore tables are created with page compression. This is not user-configurable in SQL Server PDW.  
  
**Columnstore Tables**  
  
-   A columnstore table is a table that is stored by column.  
  
-   A columnstore table has one clustered columnstore index and no other indexes.  
  
-   To store a table by column, you can use CREATE TABLE to create the table with a clustered columnstore index, or you can use CREATE COLUMNSTORE INDEX to change an existing rowstore table to a columnstore table.  
  
-   The columnstore index includes all columns in the table. These are displayed as included columns in the metadata. None of the columns are key columns.  
  
-   You can change a columnstore table to a rowstore table by using [CREATE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-index-sql-server-pdw.md) with the DROP_EXISTING clause.  
  
-   All columnstore tables are created with columnstore compression. This is not user-configurable in SQL Server PDW.  
  
-   For a distributed table, the clustered columnstore index does not affect how the data is distributed; data is always distributed by row. The clustered columnstore index affects how the data is stored within each distribution.  
  
For more information, see [Clustered Columnstore Indexes &#40;SQL Server PDW&#41;](../sqlpdw/clustered-columnstore-indexes-sql-server-pdw.md).  
  
**Local Temporary Tables** are stored in the tempdb database. For more information about temporary tables, see. [tempdb Database &#40;SQL Server PDW&#41;](../sqlpdw/tempdb-database-sql-server-pdw.md).  
  
For more information about how to choose a distribution column, see [Distributed and Replicated Tables &#40;SQL Server PDW&#41;](../sqlpdw/distributed-and-replicated-tables-sql-server-pdw.md).  
  
For tables that are distributed and partitioned, each distribution contains all of the table partitions. For example, if there are eight distributions per Compute node and four table partitions, there will be 32 partitions per Compute node.  
  
For table partitions, if you do not specify a boundary value, SQL Server PDW will create a partitioned table with one partition. If you specify one boundary value, the resulting table has two partitions — one for the values lower than the boundary value and one for the values higher than the boundary value. If you move a partition into a non-partitioned table, the non-partitioned table will receive the data, but will not have the partition boundaries in its metadata.  
  
## Limitations and Restrictions  
Each table is a rowstore or a columnstore.  Table data that is stored row-by-row is called a rowstore. Table data that is stored column-by-column is called a columnstore.  Mixing the two forms of data storage is not allowed.  
  
You cannot define a DEFAULT constraint on a distribution column.  
  
When using partitions, the partition column cannot have a Unicode-only collation. For example, the following statement fails.  
  
`CREATE TABLE t1 ( c1 varchar(20) COLLATE Divehi_90_CI_AS_KS_WS) WITH (PARTITION (c1 RANGE FOR VALUES (N'')))`  
  
Global temporary tables that begin with ## are not supported.  
  
**Local Temporary Tables** have the following limitations and restrictions:  
  
-   They are visible only to the current session. SQL Server PDW drops them automatically at the end of the session. They can be dropped explicitly with the DROP TABLE statement.  
  
-   They cannot be renamed.  
  
-   They cannot have partitions or views.  
  
-   Their permissions cannot be changed. GRANT, DENY, and REVOKE statements cannot be used with local temporary tables.  
  
-   Database console commands are blocked for temporary tables.  
  
-   If more than one local temporary table is used within a batch, each must have a unique name. If multiple sessions are running the same batch and creating the same local temporary table, SQL Server PDW internally appends a numeric suffix to the local temporary table name to maintain a unique name for each local temporary table.  
  
For information on minimum and maximum constraints on tables, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
If *boundary_value* is a literal value that must be implicitly converted to the data type in *partition_column_name*, a discrepancy will occur. The literal value is displayed through the SQL Server PDW system views, but the converted value is used for SQL operations.  
  
## Locking  
Takes an exclusive lock on the table. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION objects.  
  
## <a name="Examples"></a>Examples  
  
### A. Create a replicated table  
The following example creates a replicated table with three columns and without partitions. The `id` column includes a NOT NULL constraint. The table is created with a CLUSTERED COLUMNSTORE INDEX, which gives better performance better data compression over a heap or rowstore clustered index.  
  
```  
CREATE TABLE myTable   
  (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
  )  
WITH ( CLUSTERED COLUMNSTORE INDEX );  
```  
  
### B. Create a hash-distributed table  
The following example creates the same table as the previous example. However, this table is distributed (on the `id` column) instead of replicated. The table is created with a CLUSTERED COLUMNSTORE INDEX, which gives better performance better data compression over a heap or rowstore clustered index.  
  
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
  
### C. Create a round robin table  
The following example creates the same table as the previous example, but instead of distributing this table by hashing a column, the rows are spread evenly across the Compute nodes.   This is a quick way to load a table into SQL Server PDW without having to make a choice on the best distribution column. For joins and aggregations, query performance can be slower since you can't join on the distribution column to avoid data movement.  
  
```  
CREATE TABLE myTable   
  (  
    id int NOT NULL,  
    lastName varchar(20),  
    zipCode varchar(6)  
  )  
WITH  
  (   
    DISTRIBUTION = ROUND_ROBIN,   
    CLUSTERED COLUMNSTORE INDEX  
  );  
```  
  
### D. Create a local temporary table  
The following creates a local temporary table named #myTable in AdventureWorkdsDW2008. The table is specified with a 3-part name. The temporary table name starts with a #.  
  
Both distributed and replicated tables are allowed for temporary tables. This example shows how to create a distributed temporary table.  
  
```  
CREATE TABLE AdventureWorksPDW2012.dbo.#myTable   
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
  
### <a name="PartitionExample"></a>E. Create a partitioned table  
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
  
### F. Create a table with date partitioning  
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
    PARTITION ( l_shipdate   
    RANGE RIGHT FOR VALUES   
      (  
      '1992-01-01','1992-02-01','1992-03-01','1992-04-01','1992-05-01','1992-06-01','1992-07-01','1992-08-01','1992-09-01','1992-10-01','1992-11-01','1992-12-01',  
      '1993-01-01','1993-02-01','1993-03-01','1993-04-01','1993-05-01','1993-06-01','1993-07-01','1993-08-01','1993-09-01','1993-10-01','1993-11-01','1993-12-01',  
      '1994-01-01','1994-02-01','1994-03-01','1994-04-01','1994-05-01','1994-06-01','1994-07-01','1994-08-01','1994-09-01','1994-10-01','1994-11-01','1994-12-01'  
    ))  
  );  
```  
  
### G. Specify a column collation for a table  
In the following example, the table `MyTable` is created with two different column collations. By default, the column, `mycolumn1`, has the appliance-level collation Latin1_General_100_CI_AS_KS_WS. The column, `mycolumn2` has the collation Frisian_100_CS_AS.  
  
```  
CREATE TABLE MyTable   
  (  
    mycolumnnn1 nvarchar,  
    mycolumn2 nvarchar COLLATE Frisian_100_CS_AS )  
WITH ( CLUSTERED COLUMNSTORE INDEX )  
;  
```  
  
## H. Specify a DEFAULT constraint for a column  
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
  
## I. Create a table with a clustered columnstore index  
The following example creates a distributed table with a clustered columnstore index. Each distribution will be stored as a columnstore.  
  
The clustered columnstore index does not affect how the data is distributed; data is always distributed by row. The clustered columnstore index affects how the data is stored within each distribution.  
  
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
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md)  
[DROP TABLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-table-sql-server-pdw.md)  
[ALTER TABLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-table-sql-server-pdw.md)  
  
