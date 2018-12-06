---
title: "ALTER PARTITION FUNCTION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER PARTITION FUNCTION"
  - "ALTER_PARTITION_FUNCTION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "splitting partitions [SQL Server]"
  - "partitioned tables [SQL Server], splitting"
  - "ALTER PARTITION FUNCTION statement"
  - "merging partitions [SQL Server]"
  - "partitioned indexes [SQL Server], merging"
  - "partitioned indexes [SQL Server], splitting"
  - "modifying partition functions"
  - "partition functions [SQL Server], modifying"
  - "partitioned tables [SQL Server], merging"
ms.assetid: 70866dac-0a8f-4235-8108-51547949ada4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER PARTITION FUNCTION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Alters a partition function by splitting or merging its boundary values. By executing ALTER PARTITION FUNCTION, one partition of any table or index that uses the partition function can be split into two partitions, or two partitions can be merged into one less partition.  
  
> [!CAUTION]  
>  More than one table or index can use the same partition function. ALTER PARTITION FUNCTION affects all of them in a single transaction.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER PARTITION FUNCTION partition_function_name()  
{   
    SPLIT RANGE ( boundary_value )  
  | MERGE RANGE ( boundary_value )   
} [ ; ]  
```  
  
## Arguments  
 *partition_function_name*  
 Is the name of the partition function to be modified.  
  
 SPLIT RANGE ( *boundary_value* )  
 Adds one partition to the partition function. *boundary_value* determines the range of the new partition, and must differ from the existing boundary ranges of the partition function. Based on *boundary_value*, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] splits one of the existing ranges into two. Of these two, the one where the new *boundary_value* resides is considered the new partition.  
  
 A filegroup must exist online and be marked by the partition scheme that uses the partition function as NEXT USED to hold the new partition. Filegroups are allocated to partitions in a CREATE PARTITION SCHEME statement. If a CREATE PARTITION SCHEME statement allocates more filegroups than necessary (fewer partitions are created in the CREATE PARTITION FUNCTION statement than filegroups to hold them), then there are unassigned filegroups, and one of them is marked NEXT USED by the partition scheme. This filegroup will hold the new partition. If there are no filegroups marked NEXT USED by the partition scheme, you must use ALTER PARTITION SCHEME to either add a filegroup, or designate an existing one, to hold the new partition. A filegroup that already holds partitions can be designated to hold additional partitions. Because a partition function can participate in more than one partition scheme, all the partition schemes that use the partition function to which you are adding partitions must have a NEXT USED filegroup. Otherwise, ALTER PARTITION FUNCTION fails with an error that displays the partition scheme or schemes that lack a NEXT USED filegroup.  
  
 If you create all the partitions in the same filegroup, that filegroup is initially assigned to be the NEXT USED filegroup automatically. However, after a split operation is performed, there is no longer a designated NEXT USED filegroup. You must explicitly assign the filegroup to be the NEXT USED filegroup by using ALTER PARTITION SCHEME or a subsequent split operation will fail.  
  
> [!NOTE]  
>  Limitations with columnstore index: Only empty partitions can be split in when a columnstore index exists on the table. You will need to drop or disable the columnstore index before performing this operation  
  
 MERGE [ RANGE ( *boundary_value*) ]  
 Drops a partition and merges any values that exist in the partition into one of the remaining partitions. RANGE (*boundary_value*) must be an existing boundary value, into which the values from the dropped partition are merged. The filegroup that originally held *boundary_value* is removed from the partition scheme unless it is used by a remaining partition, or is marked with the NEXT USED property. The merged partition resides in the filegroup that originally did not hold *boundary_value*. *boundary_value* is a constant expression that can reference variables (including user-defined type variables) or functions (including user-defined functions). It cannot reference a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression. *boundary_value* must either match or be implicitly convertible to the data type of its corresponding partitioning column, and cannot be truncated during implicit conversion in a way that the size and scale of the value does not match that of its corresponding *input_parameter_type*.  
  
> [!NOTE]  
>  Limitations with columnstore index: Two nonempty partitions containing a columnstore index cannot be merged. You will need to drop or disable the columnstore index before performing this operation  
  
## Best Practices  
 Always keep empty partitions at both ends of the partition range to guarantee that the partition split (before loading new data) and partition merge (after unloading old data) do not incur any data movement. Avoid splitting or merging populated partitions. This can be extremely inefficient, as this may cause as much as four times more log generation, and may also cause severe locking.  
  
## Limitations and Restrictions  
 ALTER PARTITION FUNCTION repartitions any tables and indexes that use the function in a single atomic operation. However, this operation occurs offline, and depending on the extent of repartitioning, may be resource-intensive.  
  
 ALTER PARTITION FUNCTION can only be used for splitting one partition into two, or merging two partitions into one. To change the way a table is otherwise partitioned (for example, from 10 partitions to 5 partitions), you can exercise any of the following options. Depending on the configuration of your system, these options can vary in resource consumption:  
  
-   Create a new partitioned table with the desired partition function, and then insert the data from the old table into the new table by using an INSERT INTO...SELECT FROM statement.  
  
-   Create a partitioned clustered index on a heap.  
  
    > [!NOTE]  
    >  Dropping a partitioned clustered index results in a partitioned heap.  
  
-   Drop and rebuild an existing partitioned index by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE INDEX statement with the DROP EXISTING = ON clause.  
  
-   Perform a sequence of ALTER PARTITION FUNCTION statements.  
  
 All filegroups that are affected by ALTER PARTITION FUNCTION must be online.  
  
 ALTER PARTITION FUNCTION fails when there is a disabled clustered index on any tables that use the partition function.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not provide replication support for modifying a partition function. Changes to a partition function in the publication database must be manually applied in the subscription database.  
  
## Permissions  
 Any one of the following permissions can be used to execute ALTER PARTITION FUNCTION:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition function was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition function was created.  
  
## Examples  
  
### A. Splitting a partition of a partitioned table or index into two partitions  
 The following example creates a partition function to partition a table or index into four partitions. `ALTER PARTITION FUNCTION` splits one of the partitions into two to create a total of five partitions.  
  
```sql  
IF EXISTS (SELECT * FROM sys.partition_functions  
    WHERE name = 'myRangePF1')  
DROP PARTITION FUNCTION myRangePF1;  
GO  
CREATE PARTITION FUNCTION myRangePF1 (int)  
AS RANGE LEFT FOR VALUES ( 1, 100, 1000 );  
GO  
--Split the partition between boundary_values 100 and 1000  
--to create two partitions between boundary_values 100 and 500  
--and between boundary_values 500 and 1000.  
ALTER PARTITION FUNCTION myRangePF1 ()  
SPLIT RANGE (500);  
```  
  
### B. Merging two partitions of a partitioned table into one partition  
 The following example creates the same partition function as above, and then merges two of the partitions into one partition, for a total of three partitions.  
  
```sql  
IF EXISTS (SELECT * FROM sys.partition_functions  
    WHERE name = 'myRangePF1')  
DROP PARTITION FUNCTION myRangePF1;  
GO  
CREATE PARTITION FUNCTION myRangePF1 (int)  
AS RANGE LEFT FOR VALUES ( 1, 100, 1000 );  
GO  
--Merge the partitions between boundary_values 1 and 100  
--and between boundary_values 100 and 1000 to create one partition  
--between boundary_values 1 and 1000.  
ALTER PARTITION FUNCTION myRangePF1 ()  
MERGE RANGE (100);  
```  
  
## See Also  
 [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)   
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)   
 [DROP PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-partition-function-transact-sql.md)   
 [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)   
 [ALTER PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-scheme-transact-sql.md)   
 [DROP PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/drop-partition-scheme-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [sys.partition_functions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)   
 [sys.partition_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-parameters-transact-sql.md)   
 [sys.partition_range_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-range-values-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
