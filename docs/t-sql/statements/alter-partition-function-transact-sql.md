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

  Alters a partition function by splitting or merging its boundary values. Running an ALTER PARTITION FUNCTION statement can split one table partition or index that uses the partition function into two partitions. The statement can also merge two partitions into one less partition.  
  
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
 Adds one partition to the partition function. *boundary_value* determines the range of the new partition, and must differ from the existing boundary ranges of the partition function. Based on *boundary_value*, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] splits one of the existing ranges into two. Of these two ranges, the one with the new *boundary_value* is the new partition.  
  
 A filegroup must exist online. And, the partition scheme that uses the partition function as NEXT USED to hold the new partition must mark the filegroup. A CREATE PARTITION SCHEME statement assigns filegroups to partitions. The CREATE PARTITION FUNCTION statement creates fewer partitions than filegroups to hold them. A CREATE PARTITION SCHEME statement may set aside more filegroups than needed. If that happens, then you'll end up with unassigned filegroups and the partition scheme marks one of the filegroups as NEXT USED. This filegroup holds the new partition. If there are no filegroups the partition scheme marks as NEXT USED, you must use an ALTER PARTITION SCHEME statement. 

 The ALTER PARTITION SCHEME statement can either add a filegroup, or select an existing one, to hold the new partition. You can assign a filegroup that already holds partitions to hold additional partitions. A partition function can participate in more than one partition scheme. For this reason, all the partition schemes that use the partition function to which you're adding partitions must have a NEXT USED filegroup. Otherwise, the ALTER PARTITION FUNCTION statement fails with an error that displays the partition scheme or schemes that lack a NEXT USED filegroup.  
  
 If you create all the partitions in the same filegroup, that filegroup is initially assigned to be the NEXT USED filegroup automatically. However, after a split operation runs, there's no longer a selected NEXT USED filegroup. Explicitly assign the filegroup as the NEXT USED filegroup by using ALTER PARTITION SCHEME or an upcoming split operation will fail.  
  
> [!NOTE]  
>  Limitations with columnstore index: Only empty partitions can be split in when a columnstore index exists on the table. You will need to drop or disable the columnstore index before performing this operation.  
  
 MERGE [ RANGE ( *boundary_value*) ]  
 Drops a partition and merges any values that exist in the partition into one of the remaining partitions. RANGE (*boundary_value*) must be an existing boundary value, into which the values from the dropped partition are merged. This argument removes the filegroup that originally held *boundary_value* from the partition scheme unless a remaining partition uses it, or marks it with the NEXT USED property. The merged partition exists in the filegroup that originally didn't hold *boundary_value*. *boundary_value* is a constant expression that can reference variables (including user-defined type variables) or functions (including user-defined functions). It can't reference a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression. *boundary_value* must either match or be implicitly convertible to the data type of its corresponding partitioning column. You also can't truncate *boundary_value* during implicit conversion in a way that the size and scale of the value doesn't match that of its corresponding *input_parameter_type*.  
  
> [!NOTE]  
>  Limitations with columnstore index: Two nonempty partitions containing a columnstore index can't be merged. You will need to drop or disable the columnstore index before performing this operation  
  
## Best Practices  
 Always keep empty partitions at both ends of the partition range. Keep the partitions at both ends to guarantee that the partition split, which occurs before loading new data, and the partition merge, which occurs after unloading old data, don't incur any data movement. Avoid splitting or merging populated partitions. Splitting or merging populated partitions can be inefficient. They can be inefficient because the split or merge may cause as much as four times more log generation, and may also cause severe locking.  
  
## Limitations and Restrictions  
 ALTER PARTITION FUNCTION repartitions any tables and indexes that use the function in a single atomic operation. However, this operation occurs offline, and depending on the extent of repartitioning, may be resource-intensive.  
  
 Only use ALTER PARTITION FUNCTION for splitting one partition into two, or merging two partitions into one. To change the way a table is otherwise partitioned (for example, from 10 partitions to five partitions), exercise any of the following options. Depending on the configuration of your system, these options may vary in resource consumption:  
  
-   Create a new partitioned table with the necessary partition function. Then, insert the data from the old table into the new table by using an INSERT INTO...SELECT FROM statement.  
  
-   Create a partitioned clustered index on a heap.  
  
    > [!NOTE]  
    >  Dropping a partitioned clustered index results in a partitioned heap.  
  
-   Drop and rebuild an existing partitioned index by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE INDEX statement with the DROP EXISTING = ON clause.  
  
-   Run a sequence of ALTER PARTITION FUNCTION statements.  
  
 All filegroups that are affected by ALTER PARTITION FUNCTION must be online.  
  
 ALTER PARTITION FUNCTION fails when a disabled clustered index exists on any tables that use the partition function.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't provide replication support for modifying a partition function. Changes to a partition function in the publication database must be manually applied in the subscription database.  
  
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
  
  
