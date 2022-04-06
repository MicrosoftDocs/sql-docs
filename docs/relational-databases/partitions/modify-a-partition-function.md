---
description: "Modify a Partition Function"
title: "Modify a Partition Function"

ms.custom: ""
ms.date: "4/6/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: ae5bfc09-f27a-4ea9-9518-485278b11674
author: LitKnd
ms.author: kendralittle
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify a Partition Function
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can change the way a table or index is partitioned in SQL Server, Azure SQL Database, and Azure SQL Managed Instance by adding or subtracting the number of partitions specified, in increments of 1, in the partition function of the partitioned table or index by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. When you add a partition, you do so by "splitting" an existing partition into two partitions and redefining the boundaries of the new partitions. When you drop a partition, you do so by "merging" the boundaries of two partitions into one. This last action repopulates one partition and leaves the other partition unassigned.  
  
> [!CAUTION]  
>  More than one table or index can use the same partition function. When you modify a partition function, you affect all of them in a single transaction. Check the [partition function's dependencies](#query-partitioned-objects-in-a-database) before modifying it.  

Table partitioning is also available in dedicated SQL pools in Azure Synapse Analytics, with some syntax differences. Learn more in [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition).
  
##  <a name="Restrictions"></a> Limitations
  
-   ALTER PARTITION FUNCTION can only be used for splitting one partition into two, or for merging two partitions into one. To change the way a table or index is partitioned (from 10 partitions to 5, for example), you can use any one of the following options:  
  
    -   Create a new partitioned table with the desired partition function, and then insert the data from the old table into the new table by using either an INSERT INTO ... SELECT FROM [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or the **Manage Partition Wizard** in [SQL Server Management Studio (SSMS)](../../ssms/sql-server-management-studio-ssms.md).  
  
    -   Create a partitioned clustered index on a heap.  
  
        > [!NOTE]  
        >  Dropping a partitioned clustered index results in a partitioned heap.  
  
    -   Drop and rebuild an existing partitioned index by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE INDEX statement with the DROP EXISTING = ON clause.  
  
    -   Perform a sequence of ALTER PARTITION FUNCTION statements.  
  
- The database engine does not provide replication support for modifying a partition function. If you want to make changes to a partition function in the publication database, you must do this manually in the subscription database.  
  
-   All filegroups that are affected by ALTER PARTITION FUNCTION must be online.  
  
##  <a name="Permissions"></a> Permissions  
 Any one of the following permissions can be used to execute ALTER PARTITION FUNCTION:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition function was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition function was created.  

## Query partitioned objects in a database

The following query lists all partitioned objects in a database. This can be used to check the dependencies for a partition function before modifying it.

```sql
SELECT 
	PF.name AS PartitionFunction,
	ds.name AS PartitionScheme,
	OBJECT_NAME(si.object_id) AS PartitionedTable, 
	si.name as IndexName
FROM sys.indexes AS si
JOIN sys.data_spaces AS ds
	ON ds.data_space_id = si.data_space_id
JOIN sys.partition_schemes AS PS
	ON PS.data_space_id = si.data_space_id
JOIN sys.partition_functions AS PF
	ON PF.function_id = PS.function_id
WHERE ds.type = 'PS'
AND OBJECTPROPERTYEX(si.object_id, 'BaseType') = 'U'
ORDER BY PartitionFunction, PartitionScheme, PartitionedTable;
```  

## Split a partition with Transact-SQL
  
1.  In **Object Explorer**, connect to your target database.  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```  
    -- Look for a previous version of the partition function "myRangePF1" and deletes it if it is found.  
    IF EXISTS (SELECT * FROM sys.partition_functions  
        WHERE name = 'myRangePF1')  
        DROP PARTITION FUNCTION myRangePF1;  
    GO

    -- Create a new partition function called "myRangePF1" that partitions a table into four partitions.  
    CREATE PARTITION FUNCTION myRangePF1 (int)  
    AS RANGE LEFT FOR VALUES ( 1, 100, 1000 );  
    GO  

    --Split the partition between boundary_values 100 and 1000  
    --to create two partitions between boundary_values 100 and 500  
    --and between boundary_values 500 and 1000.  
    ALTER PARTITION FUNCTION myRangePF1 ()  
    SPLIT RANGE (500);  
    ```  
  
## Merge two partitions with Transact-SQL
  
1.  In **Object Explorer**, connect to your target database.  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```  
    -- Look for a previous version of the partition function "myRangePF1" and deletes it if it is found.  
    IF EXISTS (SELECT * FROM sys.partition_functions  
        WHERE name = 'myRangePF1')  
        DROP PARTITION FUNCTION myRangePF1;  
    GO 

    -- Create a new partition function called "myRangePF1" that partitions a table into four partitions.  
    CREATE PARTITION FUNCTION myRangePF1 (int)  
    AS RANGE LEFT FOR VALUES ( 1, 100, 1000 );  
    GO  

    --Merge the partitions between boundary_values 1 and 100  
    --and between boundary_values 100 and 1000 to create one partition  
    --between boundary_values 1 and 1000.  
    ALTER PARTITION FUNCTION myRangePF1 ()  
    MERGE RANGE (100);  
    ```  

## Delete a partition function with SSMS

1. In **Object Explorer**, connect to your target database.
  
1. Expand the database where you want to delete the partition function and then expand the **Storage** folder.  
  
1. Expand the **Partition Functions** folder.  
  
1. Right-click the partition function you want to delete and select **Delete**.  
  
1. In the **Delete Object** dialog box, ensure that the correct partition function is selected, and then select **OK**. 

## Next steps

Learn more about related concepts in the following articles:

- [Partitioned tables and indexes](partitioned-tables-and-indexes.md)
- [Create partitioned tables and indexes](create-partitioned-tables-and-indexes.md)
- [ALTER PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-function-transact-sql.md)
- [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition)