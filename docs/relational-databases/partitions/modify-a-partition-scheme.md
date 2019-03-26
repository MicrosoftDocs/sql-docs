---
title: "Modify a Partition Scheme | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: 515de63f-dfc5-434d-9adb-f3b5992f745a
author: julieMSFT
ms.author: jrasnick
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify a Partition Scheme
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  You can modify a partition scheme in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by designating a filegroup to hold the next partition that is added to a partitioned table using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You do this by assigning the NEXT USED property to a filegroup. You can assign the NEXT USED property to an empty filegroup or to one that already holds a partition. In other words, a filegroup can hold more than one partition.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a partitioned table or index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Any filegroup affected by ALTER PARTITION SCHEME must be online.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The following permissions can be used to execute ALTER PARTITION SCHEME:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition scheme was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition scheme was created.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To modify a partition scheme:**  
  
 This specific action cannot be performed using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. In order to modify a partition scheme, you must first delete the scheme and then create a new one with the desired properties using the Create Partition Wizard. For more information, see [Create Partitioned Tables and Indexes](../../relational-databases/partitions/create-partitioned-tables-and-indexes.md)[Using SQL Server Management Studio](../../relational-databases/partitions/create-partitioned-tables-and-indexes.md#SSMSProcedure) under **Create Partitioned Tables and Indexes**.  
  
#### To delete a partition scheme  
  
1.  Click the plus sign to expand the database where you want to delete the partition scheme.  
  
2.  Click the plus sign to expand the **Storage** folder.  
  
3.  Click the plus sign to expand the **Partition Schemes** folder.  
  
4.  Right-click the partition scheme you want to delete and select **Delete**.  
  
5.  In the **Delete Object** dialog box, ensure that the correct partition scheme is selected, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To modify a partition scheme  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- add five new filegroups to the AdventureWorks2012 database  
    ALTER DATABASE AdventureWorks2012  
    ADD FILEGROUP test1fg;  
    GO  
    ALTER DATABASE AdventureWorks2012  
    ADD FILEGROUP test2fg;  
    GO  
    ALTER DATABASE AdventureWorks2012  
    ADD FILEGROUP test3fg;  
    GO  
    ALTER DATABASE AdventureWorks2012  
    ADD FILEGROUP test4fg;  
    GO  
    ALTER DATABASE AdventureWorks2012  
    ADD FILEGROUP test5fg;  
    GO  
    -- if the "myRangePF1" partition function and the "myRangePS1" partition scheme exist,  
    -- drop them from the AdventureWorks2012 database  
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
  
 For more information, see [ALTER PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-scheme-transact-sql.md).  
  
  
