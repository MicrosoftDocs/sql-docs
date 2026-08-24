---
title: "Modify a partition scheme"
titleSuffix: SQL Server, Azure SQL Database, Azure SQL Managed Instance
description: Modify a partition scheme
author: VanMSFT
ms.author: vanto
ms.date: 07/29/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Modify a partition scheme

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

You can modify a partition scheme by designating a filegroup to hold the next partition that is added to a partitioned table using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You do this by assigning the NEXT USED property to a filegroup. 

You can assign the NEXT USED property to an empty filegroup or to one that already holds a partition. In other words, a filegroup can hold more than one partition. Learn more about filegroups and  partitioning strategies in the [Filegroups](partitioned-tables-and-indexes.md#filegroups).

##  <a name="Restrictions"></a> Limitations

Any filegroup affected by ALTER PARTITION SCHEME must be online.  

Partitioning is fully supported in Azure SQL Database and SQL database in Fabric. All partitions must be placed on the `PRIMARY` filegroup because only the `PRIMARY` filegroup is provided in Azure SQL Database and SQL database in Fabric.

Table partitioning is available in dedicated SQL pools in Azure Synapse Analytics, with some syntax differences. For more information, see [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition).
  
## Permissions  

The following permissions can be used to execute ALTER PARTITION SCHEME:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition scheme was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition scheme was created.
  
## Modify a partition scheme with Transact-SQL

This example uses the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.

    > [!NOTE]
    > For simplicity, this code creates new filegroups but does not assign files to them. This  allows the demonstration of how to modify the partition scheme but is not a complete example of configuring a partitioned object. Find examples of creating partitioned tables and indexes in [Create partitioned tables and indexes](create-partitioned-tables-and-indexes.md).
  
    ```sql
    USE AdventureWorks2022;  
    GO
    -- add five new filegroups to the AdventureWorks2022 database  
    ALTER DATABASE AdventureWorks2022  
    ADD FILEGROUP test1fg;  
    GO  
    ALTER DATABASE AdventureWorks2022  
    ADD FILEGROUP test2fg;  
    GO  
    ALTER DATABASE AdventureWorks2022  
    ADD FILEGROUP test3fg;  
    GO  
    ALTER DATABASE AdventureWorks2022  
    ADD FILEGROUP test4fg;  
    GO  
    ALTER DATABASE AdventureWorks2022  
    ADD FILEGROUP test5fg;  
    GO 

    -- if the "myRangePF1" partition function and the "myRangePS1" partition scheme exist,  
    -- drop them from the AdventureWorks2022 database  
    IF EXISTS (SELECT * FROM sys.partition_functions  
        WHERE name = 'myRangePF1')  
    DROP PARTITION FUNCTION myRangePF1;  
    GO  
    IF EXISTS (SELECT * FROM sys.partition_schemes  
        WHERE name = 'myRangePS1')  
    DROP PARTITION SCHEME myRangePS1;  
    GO  

    -- create the new partition function "myRangePF1" with four partition groups  
    CREATE PARTITION FUNCTION myRangePF1 (int)  
    AS RANGE LEFT FOR VALUES ( 1, 100, 1000 );  
    GO  

    -- create the new partition scheme "myRangePS1"that will use   
    -- the "myRangePF1" partition function with five file groups.  
    -- The last filegroup, "test5fg," will be kept empty but marked  
    -- as the next used filegroup in the partition scheme.  
    CREATE PARTITION SCHEME myRangePS1  
    AS PARTITION myRangePF1  
    TO (test1fg, test2fg, test3fg, test4fg, test5fg);  
    GO  

    --Split "myRangePS1" between boundary_values 100 and 1000  
    --to create two partitions between boundary_values 100 and 500  
    --and between boundary_values 500 and 1000.  
    ALTER PARTITION FUNCTION myRangePF1 ()  
    SPLIT RANGE (500);  
    GO  

    -- Allow the "myRangePS1" partition scheme to use the filegroup "test5fg"  
    -- for the partition with boundary_values of 100 and 500  
    ALTER PARTITION SCHEME myRangePS1  
    NEXT USED test5fg;  
    GO  
    ```  

## Delete a partition scheme with SSMS

1. In **Object Explorer**, connect to your target database.

1. Select the plus sign to expand the database where you want to delete the partition scheme.  
  
1. Select the plus sign to expand the **Storage** folder.  
  
1. Select the plus sign to expand the **Partition Schemes** folder.  
  
1. Right-click the partition scheme you want to delete and select **Delete**.  
  
1. In the **Delete Object** dialog box, ensure that the correct partition scheme is selected, and then select **OK**.

## Related content

- [Create partitioned tables and indexes](create-partitioned-tables-and-indexes.md)
- [ALTER PARTITION SCHEME (Transact-SQL)](../../t-sql/statements/alter-partition-scheme-transact-sql.md)
- [ALTER PARTITION FUNCTION (Transact-SQL)](../../t-sql/statements/alter-partition-function-transact-sql.md)
- [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition)
