---
title: "ALTER TABLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 960fb23c-e5d8-46d0-94d3-f1fca0035e0a
caps.latest.revision: 88
author: BarbKess
---
# ALTER TABLE (SQL Server PDW)
Modifies the definition for a table in SQL Server PDW. Use ALTER TABLE to:  
  
-   Modify a column definition.  
  
-   Add or drop a column or column constraint.  
  
-   Split, merge, or switch table partitions in a SQL Server PDW database. Table partitions are often used to manage data storage and archival.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER TABLE [ database_name . [schema_name ] . | schema_name. ] source_table_name   
{  
    ALTER COLUMN column_name  
        {   
            type_name [ ( precision [ , scale ] ) ]   
            [ COLLATE Windows_collation_name ]   
            [ NULL | NOT NULL ]   
        }  
    | ADD { <column_definition> | <column_constraint> FOR column_name} [ ,...n ]  
    | DROP { COLUMN column_name | [CONSTRAINT] constraint_name } [ ,...n ]  
    | REBUILD [ PARTITION = { ALL | partition_number } ]  
    | { SPLIT | MERGE } RANGE (boundary_value)  
    | SWITCH [ PARTITION source_partition_number  
        TO target_table_name [ PARTITION target_partition_number ]  
}  
[;]  
  
<column_definition>::=  
{  
    column_name  
    type_name [ ( precision [ , scale ] ) ]   
    [ <column_constraint> ]  
    [ COLLATE Windows_collation_name ]  
    [ NULL | NOT NULL ]  
}  
  
<column_constraint>::=  
    [ CONSTRAINT constraint_name ] DEFAULT constant_expression  
```  
  
## Arguments  
*database_name*  
Specifies the name of the database that contains the table to update.  
  
*schema_name*  
Specifies the schema of the table to update. If omitted, the default is the default schema of the user, or `dbo`.  
  
*source_table_name*  
Specifies the name of the table to modify.  
  
When using:  
  
-   ALTER COLUMN, the source table is the table to modify. The source table cannot be a temporary table.  
  
-   ADD, the source table will receive the added column.  
  
-   DROP, the source table will relinquish the dropped column.  
  
-   SPLIT or MERGE, the source table will receive the partition updates.  
  
-   SWITCH, the source table contains the data that will be moved to another table.  
  
ALTER COLUMN  
Modifies the column definition for an existing column.  
  
You cannot use ALTER COLUMN to modify a distribution column; if you need to do this, you can copy the data into a new table by using [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md).  
  
You cannot change the data type, length, precision, or scale of a column that has any of the following characteristics:  
  
-   Column is included in an index.  
  
-   Column is in a partitioned table.  
  
-   Column is included in a user-defined statistics object.  
  
For a column that has a DEFAULT constraint:  
  
-   You cannot change the data type.  
  
-   You *can* change the length, precision, or scale.  
  
Some data type changes can cause the data to change. For example:  
  
-   Changing the data type from Unicode (nchar, nvarchar) to ASCII (char, varchar) can cause characters in the extended character set to be converted to ASCII.  
  
-   Reducing the length, precision, or scale of a column can cause data truncation.  
  
-   Converting a column of type FLOAT or REAL to a different data type can cause inconsistencies in data across replicated tables. Likewise, replicated tables can have inconsistencies in data when a column of a different type is converted to FLOAT or REAL.  
  
SQL Server PDW will drop all auto-generated statistics on the modified column.  
  
*column_name*  
For ALTER COLUMN, *column_name*  specifies to modify the definition for an existing column in the source table. The column to be modified cannot be a distribution column.  
  
For ADD, *column_name*  specifies the name of the column to add.  
  
*type_name* [ ( precision [ , scale ] ) ]  
Specifies the new data type for the column.  
  
-   The data type must be one of the supported SQL Server PDW data types. For a complete list of data types, see the data type options and descriptions in [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md).  
  
-   SQL Server PDW must support implicit conversions from the current data type to the new data type. For implicit type conversion rules, use the SQL Server rules. See the Implicit Conversions section in [CAST and CONVERT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187928(v=sql11).aspx). When reading the SQL Server topic, ignore data types that are not supported by SQL Server PDW.  
  
-   For more information about valid precision and scale values, see [Precision, Scale, and Length (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190476(v=sql11).aspx).  
  
COLLATE *Windows_collation_name*  
Specifies the new collation for the modified column. If not specified, the column is assigned the collation of the database.  
  
The collation name must be a Windows collation name. For a list of Windows collations, see [Windows Collation Name (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188046(v=sql11).aspx).  
  
To use the collation clause, the source column:  
  
-   Must be of type char, varchar, nchar, or nvarchar.  
  
-   Cannot be part of an index.  
  
-   Cannot be included in any user-defined statistics. The column *can* have auto-generated statistics. All auto-generated statistics will be dropped on the column receiving the collation change, which is the source column.  
  
For more information about collations in SQL Server PDW, see [Collations &#40;SQL Server PDW&#41;](../sqlpdw/collations-sql-server-pdw.md).  
  
For more information about column definitions in SQL Server PDW, see [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md).  
  
**NULL** | NOT NULL  
Defines whether or not column values can be set to NULL.  
  
[ CONSTRAINT *constraint_name* ] DEFAULT *constant_expression*  
Specifies the default column value.  
  
*constraint_name*  
The optional name for the constraint. The constraint name is unique within the database. The name can be re-used in other databases.  
  
*constant_expression*  
The default value for the column. The expression must be a literal value or a constant.  
  
Constant expression examples - allowed:  
  
-   ‘CA’  
  
-   4  
  
Constant expression examples – not allowed:  
  
-   ‘CA’ + ‘1’  
  
-   2+3  
  
-   CURRENT_TIMESTAMP  
  
Restrictions:  
  
-   You cannot define a DEFAULT constraint on a distribution column.  
  
-   You cannot modify a DEFAULT constraint. If you need to change the value for a default constraint, you can drop and recreate the constraint. This update is quick because the change does not modify existing data. This change only affects new rows to be added. Recreating the constraint does not change old rows.  
  
ADD { <column_definition> | <column_constraint> FOR COLUMN *column_name*} [ ,...*n* ]  
Specifies to add a column or a column constraint. Multiple columns and constraints are allowed.  
  
For more information about defining a column or a column constraint, see [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md)  
  
DROP { [CONSTRAINT] *constraint_name* | COLUMN *column_name* } [ ,...*n* ]  
Specifies to remove a column constraint or a column from the table. One or more constraints and columns can be listed.  
  
You cannot drop a column that:  
  
-   Is a distribution column.  
  
-   Is included in an index.  
  
-   Has a DEFAULT constraint.  
  
-   Is the partition column in a partitioned table. .  
  
REBUILD [ PARTITION = { ALL | *partition_number* } ]  
Specifies to rebuild the table and to rebuild all clustered indexes on the table. This is useful for defragmenting the table data.  
  
Rebuilding a columnstore table can reduce the storage required for the table. Some columnstore data is temporarily stored in rowstore format. Rebuilding the table will bring all of the data into the columnstore.  
  
Rebuild is an online operation for rowstore tables, and a partially offline operation for columnstore tables. During the rebuild of a columnstore table, the table data can be read, but cannot be changed.  
  
REBUILD  
Rebuild the entire table, including all partitions.  
  
REBUILD PARTITION = ALL  
Rebuilds all partitions of the columns in the index .  
  
REBUILD PARTITION = *partition_number*  
Rebuilds the table partition numbered as *partition_number*.  
  
{ SPLIT | MERGE } RANGE *(boundary_value)*  
SPLIT  
Add a new partition by splitting one partition into two partitions. When a columnstore index exists on the table only empty partitions can be split.  
  
MERGE  
Remove a partition by combining two partitions into one partition.  
  
RANGE *(boundary_value)*  
When splitting a partition, *boundary_value* specifies the new partition boundary to be added. When merging two partitions, *boundary_value* specifies the partition boundary to be removed.  
  
SWITCH [ PARTITION *source_partition_number* ] TO *target_table_name* [ PARTITION *target_partition_number* ]  
Move a partition from one table to another table.  
  
PARTITION *source_partition_number*  
Specifies a partition to switch from the source table. When the PARTITION clause is specified, the source table must be a partitioned table. When the PARTITION clause is omitted, the source table must be a non-partitioned table and all table rows will be switched.  
  
*source_partition_number* is the number of the partition to switch. This is a number from 1 to the number of partitions in *source_table_name*. Specify *source_partition_number* as a constant.  
  
*target_table_name*  
The table that logically receives the moved data. The source and target table must be different tables within the same database.  
  
PARTITION *target_partition_number*  
PARTITION specifies to logically move the source table data to a partition in the target table. When the PARTITION clause is specified, the target table must be a partitioned table and the target partition must be empty.  
  
*target_partition_number* is the number of the partition that will receive the data. This is a number from 1 to the number of partitions in the target table. Specify *target_partition_number* as a constant.  
  
The SWITCH statement allows or disallows the following source and destination variations:  
  
-   A table partition *can* be switched to a table partition in another table.  
  
-   A table partition *can* be switched to a non-partitioned table.  
  
-   A non-partitioned table *can* be switched to a non-partitioned table.  
  
-   A non-partitioned table *cannot* be switched to a partitioned table.  
  
Requirements for a partition switch operation:  
  
-   The source and target tables must have identical distribution key columns, column definitions, column order, clustered indexes, and non-clustered indexes.  
  
-   If the source and target tables are both partitioned, they must be partitioned on the same column.  
  
-   The source table and the target table must both be rowstore tables or both be columnstore tables. You cannot switch a partition from a rowstore table to a columnstore table, nor from a columnstore table to a rowstore table.  
  
-   The source table and the target table must both be replicated or both be distributed. You cannot switch a partition from a distributed table to a replicated table, nor from a replicated table to a distributed table.  
  
-   When switching from a source partition to a target partition, the range of boundary values for the source partition must be within the range of boundary values for the target partition.  
  
-   Before performing the SWITCH operation, the target partition or target non-partitioned table must be empty.  
  
## Permissions  
Requires **ALTER** permission on the table or membership in the **db_ddladmin** fixed database role.  
  
**SWITCH** operations require **ALTER** permission on both the source and target tables.  
  
**SPLIT** and **MERGE** operations require the **ALTER ANY DATASPACE** permission.  
  
Adding a column that updates the rows of the table requires **UPDATE** permission on the table. For example, adding a **NOT NULL** column with a default value.  
  
## Error Handling  
The ALTER TABLE statement cannot be used within a transaction, but is transactionally safe. If you cancel an ALTER TABLE statement or have a run-time failure, the ALTER TABLE statement will fail and all changes will be rolled back. All data will remain intact with no changes.  
  
## General Remarks  
Indexes are always partitioned in the same manner as the table. Therefore, when altering table partitions, SQL Server PDW also alters the indexes to align with the altered partitions.  
  
## Limitations and Restrictions  
The ALTER TABLE statement cannot be used within a transaction.  
  
ALTER TABLE cannot be used with the EXPLAIN command.  
  
## Locking Behavior  
Takes an exclusive lock on the table. Takes a shared lock on the DATABASE, and SCHEMA objects. ADD, DROP, and ALTER are offline operations. All read and write requests will be blocked until the operation finished. This includes DDL and DML operations on the table and on the database that contains the table.  
  
REBUILD is an online operation for rowstore tables, and a partially offline operation for columnstore tables. During the rebuild of a columnstore table, the table data is readable, but not writeable.  
  
ALTER TABLE PARTITION takes a shared lock on the SCHEMARESOLUTION object.  
  
## Metadata  
To view the system-supplied name of constraints, use the [sys.default_constraints &#40;SQL Server PDW&#41;](../sqlpdw/sys-default-constraints-sql-server-pdw.md) catalog view.  
  
To view partition metadata, you can use the [sys.partition_range_values &#40;SQL Server PDW&#41;](../sqlpdw/sys-partition-range-values-sql-server-pdw.md) catalog view.  
  
## Performance Considerations  
Use caution when running ALTER TABLE to add, modify, or drop columns. ALTER TABLE can take a long time to run, especially when a large number of rows need to be updated.  
  
Performance tips:  
  
-   Modifying an existing column is much slower than adding a new column.  
  
-   Adding a NOT NULL column with a DEFAULT constraint is IO intensive because the system generates many log records.  
  
-   Conversely, dropping a column is a quick operation because the operation is performed in the metadata.  
  
-   Using [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md) to modify a table in lieu of ALTER TABLE ALTER could improve performance in some situations.  
  
The SPLIT and MERGE operations are usually IO intensive, unless they are applied to *empty* partitions, because they move the physical data among partitions. It is a best practice to perform SPLIT and MERGE operations on empty partitions.  
  
The SWITCH operation is not IO intensive because it moves only metadata and does not move the physical data.  
  
For replicated tables, adding a column executes in parallel across the Compute nodes. For distributed tables, adding a column executes in parallel across the Compute nodes and serially across the distributions within each Compute node. All other alter column or drop column operations are executed in parallel across the Compute nodes and serialized across the distributions within a Compute node.  
  
All read and write operations on the table, and all DDL or DML operations on the database, will block when ALTER COLUMN operations are executing.  
  
## Examples  
The following examples A through C use the FactResellerSales table in the **AdventureWorksPDW2012** database.  
  
### A. Determining if a table is partitioned  
The following query returns one or more rows if the table `FactResellerSales` is partitioned. If the table is not partitioned, no rows are returned.  
  
```  
SELECT * FROM sys.partitions AS p  
JOIN sys.tables AS t  
    ON  p.object_id = t.object_id  
WHERE p.partition_id IS NOT NULL  
    AND t.name = 'FactResellerSales';  
```  
  
### B. Determining boundary values for a partitioned table  
The following query returns the boundary values for each partition in the `FactResellerSales` table.  
  
```  
SELECT t.name AS TableName, i.name AS IndexName, p.partition_number, p.partition_id, i.data_space_id, f.function_id, f.type_desc, r.boundary_id, r.value AS BoundaryValue   
FROM sys.tables AS t  
JOIN sys.indexes AS i  
    ON t.object_id = i.object_id  
JOIN sys.partitions AS p  
    ON i.object_id = p.object_id AND i.index_id = p.index_id   
JOIN  sys.partition_schemes AS s   
    ON i.data_space_id = s.data_space_id  
JOIN sys.partition_functions AS f   
    ON s.function_id = f.function_id  
LEFT JOIN sys.partition_range_values AS r   
    ON f.function_id = r.function_id and r.boundary_id = p.partition_number  
WHERE t.name = 'FactResellerSales' AND i.type <= 1  
ORDER BY p.partition_number;  
```  
  
### C. Determining the partition column for a partitioned table  
The following query returns the name of the partitioning column for table. `FactResellerSales`.  
  
```  
SELECT t.object_id AS Object_ID, t.name AS TableName, ic.column_id as PartitioningColumnID, c.name AS PartitioningColumnName   
FROM sys.tables AS t  
JOIN sys.indexes AS i  
    ON t.object_id = i.object_id  
JOIN sys.columns AS c  
    ON t.object_id = c.object_id  
JOIN sys.partition_schemes AS ps  
    ON ps.data_space_id = i.data_space_id  
JOIN sys.index_columns AS ic  
    ON ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.partition_ordinal > 0  
WHERE t.name = 'FactResellerSales'  
AND i.type <= 1  
AND c.column_id = ic.column_id;  
```  
  
### D. Merging two partitions  
The following example merges two partitions on a table.  
  
The `Customer` table has the following definition:  
  
```  
CREATE TABLE Customer (  
    id int NOT NULL,  
    lastName varchar(20),  
    orderCount int,  
    orderDate date)  
WITH   
    ( DISTRIBUTION = HASH(id),  
    PARTITION ( orderCount RANGE LEFT  
    FOR VALUES (1, 5, 10, 25, 50, 100)));  
```  
  
The following command combines the 10 and 25 partition boundaries.  
  
```  
ALTER TABLE Customer MERGE RANGE (10);  
```  
  
The new DDL for the table is:  
  
```  
CREATE TABLE Customer (  
    id int NOT NULL,  
    lastName varchar(20),  
    orderCount int,  
    orderDate date)  
WITH   
    ( DISTRIBUTION = HASH(id),  
    PARTITION ( orderCount RANGE LEFT  
    FOR VALUES (1, 5, 25, 50, 100)));  
```  
  
### E. Splitting a partition  
The following example splits a partition on a table.  
  
The `Customer` table has the following DDL:  
  
```  
DROP TABLE Customer;  
  
CREATE TABLE Customer (  
    id int NOT NULL,  
    lastName varchar(20),  
    orderCount int,  
    orderDate date)  
WITH   
    ( DISTRIBUTION = HASH(id),  
    PARTITION ( orderCount RANGE LEFT  
    FOR VALUES (1, 5, 10, 25, 50, 100 )));  
```  
  
The following command creates a new partition bound by the value 75, between 50 and 100.  
  
```  
ALTER TABLE Customer SPLIT RANGE (75);  
```  
  
The new DDL for the table is:  
  
```  
CREATE TABLE Customer (  
   id int NOT NULL,  
   lastName varchar(20),  
   orderCount int,  
   orderDate date)  
   WITH DISTRIBUTION = HASH(id),  
   PARTITION ( orderCount (RANGE LEFT  
      FOR VALUES (1, 5, 10, 25, 50, 75, 100 )));  
```  
  
### F. Using SWITCH to move a partition to a history table  
The following example moves the data in a partition of the `Orders` table to a partition in the `OrdersHistory` table.  
  
The `Orders` table has the following DDL:  
  
```  
CREATE TABLE Orders (  
    id INT,  
    city VARCHAR (25),  
    lastUpdateDate DATE,  
    orderDate DATE )  
WITH   
    (DISTRIBUTION = HASH ( id ),  
    PARTITION ( orderDate RANGE RIGHT   
    FOR VALUES ('2004-01-01', '2005-01-01', '2006-01-01', '2007-01-01' )));  
```  
  
In this example, the `Orders` table has the following partitions. Each partition contains data.  
  
|Partition|Has data?|Boundary range|  
|-------------|-------------|------------------|  
|1|Yes|OrderDate < '2004-01-01'|  
|2|Yes|'2004-01-01' <= OrderDate < '2005-01-01'|  
|3|Yes|'2005-01-01' <= OrderDate< '2006-01-01'|  
|4|Yes|'2006-01-01'<= OrderDate < '2007-01-01'|  
|5|Yes|'2007-01-01' <= OrderDate|  
  
-   Partition 1 (has data): OrderDate < '2004-01-01'  
  
-   Partition 2 (has data): '2004-01-01' <= OrderDate < '2005-01-01'  
  
-   Partition 3 (has data): '2005-01-01' <= OrderDate< '2006-01-01'  
  
-   Partition 4 (has data): '2006-01-01'<= OrderDate < '2007-01-01'  
  
-   Partition 5 (has data): '2007-01-01' <= OrderDate  
  
The `OrdersHistory` table has the following DDL, which has identical columns and column names as the `Orders` table. Both are hash-distributed on the `id` column.  
  
```  
CREATE TABLE OrdersHistory (  
   id INT,  
   city VARCHAR (25),  
   lastUpdateDate DATE,  
   orderDate DATE )  
WITH   
    (DISTRIBUTION = HASH ( id ),  
    PARTITION ( orderDate RANGE RIGHT   
    FOR VALUES ( '2004-01-01' )));  
```  
  
Although the columns and column names must be the same, the partition boundaries do not need to be the same. In this example, the `OrdersHistory` table has the following two partitions and both partitions are empty:  
  
-   Partition 1 (no data): OrderDate < '2004-01-01'  
  
-   Partition 2 (empty): '2004-01-01' <= OrderDate  
  
For the previous two tables, the following command moves all rows with `OrderDate < '2004-01-01'` from the `Orders` table to the `OrdersHistory` table.  
  
```  
ALTER TABLE Orders SWITCH PARTITION 1 TO OrdersHistory PARTITION 1;  
```  
  
As a result, the first partition in `Orders` is empty and the first partition in `OrdersHistory` contains data. The tables now appear as follows:  
  
`Orders` table  
  
-   Partition 1 (empty): OrderDate < '2004-01-01'  
  
-   Partition 2 (has data): '2004-01-01' <= OrderDate < '2005-01-01'  
  
-   Partition 3 (has data): '2005-01-01' <= OrderDate< '2006-01-01'  
  
-   Partition 4 (has data): '2006-01-01'<= OrderDate < '2007-01-01'  
  
-   Partition 5 (has data): '2007-01-01' <= OrderDate  
  
`OrdersHistory` table  
  
-   Partition 1 (has data): OrderDate < '2004-01-01'  
  
-   Partition 2 (empty): '2004-01-01' <= OrderDate  
  
To clean up the `Orders` table, you can remove the empty partition by merging partitions 1 and 2 as follows:  
  
```  
ALTER TABLE Orders MERGE RANGE ('2004-01-01');  
```  
  
After the merge, the `Orders` table has the following partitions:  
  
`Orders` table  
  
-   Partition 1 (has data): OrderDate < '2005-01-01'  
  
-   Partition 2 (has data): '2005-01-01' <= OrderDate< '2006-01-01'  
  
-   Partition 3 (has data): '2006-01-01'<= OrderDate < '2007-01-01'  
  
-   Partition 4 (has data): '2007-01-01' <= OrderDate  
  
Suppose another year passes and you are ready to archive the year 2005. You can allocate an empty partition for the year 2005 in the `OrdersHistory` table by splitting the empty partition as follows:  
  
```  
ALTER TABLE OrdersHistory SPLIT RANGE ('2005-01-01');  
```  
  
After the split, the `OrdersHistory` table has the following partitions:  
  
`OrdersHistory` table  
  
-   Partition 1 (has data): OrderDate < '2004-01-01'  
  
-   Partition 2 (empty): '2004-01-01' < '2005-01-01'  
  
-   Partition 3 (empty): '2005-01-01' <= OrderDate  
  
## G. Using partition switching to update one partition at a time in a large table  
If you need to update a value in a large table and think it will take too long to update the entire table, you can use partition switching to update one partition at a time. Follow these steps:  
  
1.  Discover the table structure from the metadata.  
  
2.  Create an empty table with the same structure.  
  
3.  Switch a partition into the empty table, perform the update, and then switch the partition back to the full table. Repeat this step for each partition that needs to be updated.  
  
## H. Change the collation of a column  
This example uses ALTER TABLE to change the collation of the LastName column in the dimEmployee table.  
  
This change affects only the metadata.  
  
```  
USE AdventureWorksPDW2012;  
ALTER TABLE AdventureWorksPDW2012.dbo.dimEmployee  
    ALTER COLUMN LastName nvarchar(50)  
    COLLATE Latin1_General_100_CI_AS_KS_WS;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
