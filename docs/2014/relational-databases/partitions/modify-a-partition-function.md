---
title: "Modify a Partition Function | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: ae5bfc09-f27a-4ea9-9518-485278b11674
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Modify a Partition Function
  You can change the way a table or index is partitioned in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by adding or subtracting the number of partitions specified, in increments of 1, in the partition function of the partitioned table or index by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. When you add a partition, you do so by "splitting" an existing partition into two partitions and redefining the boundaries of the new partitions. When you drop a partition, you do so by "merging" the boundaries of two partitions into one. This last action repopulates one partition and leaves the other partition unassigned.  
  
> [!CAUTION]  
>  More than one table or index can use the same partition function. When you modify a partition function, you affect all of them in a single transaction. Check the partition function's dependencies before modifying it.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To modify a partition function, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   ALTER PARTITION FUNCTION can only be used for splitting one partition into two, or for merging two partitions into one. To change the way a table or index is partitioned (from 10 partitions to 5, for example), you can use any one of the following options:  
  
    -   Create a new partitioned table with the desired partition function, and then insert the data from the old table into the new table by using either an INSERT INTO ... SELECT FROM [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or the **Manage Partition Wizard** in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
    -   Create a partitioned clustered index on a heap.  
  
        > [!NOTE]  
        >  Dropping a partitioned clustered index results in a partitioned heap.  
  
    -   Drop and rebuild an existing partitioned index by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE INDEX statement with the DROP EXISTING = ON clause.  
  
    -   Perform a sequence of ALTER PARTITION FUNCTION statements.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not provide replication support for modifying a partition function. If you want to make changes to a partition function in the publication database, you must do this manually in the subscription database.  
  
-   All filegroups that are affected by ALTER PARTITION FUNCTION must be online.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Any one of the following permissions can be used to execute ALTER PARTITION FUNCTION:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition function was created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition function was created.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To modify a partition function:**  
  
 This specific action cannot be performed using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. In order to modify a partition function, you must first delete the function and then create a new one with the desired properties using the Create Partition Wizard. For more information, see  
  
#### To delete a partition function  
  
1.  Expand the database where you want to delete the partition function and then expand the **Storage** folder.  
  
2.  Expand the **Partition Functions** folder.  
  
3.  Right-click the partition function you want to delete and select **Delete**.  
  
4.  In the **Delete Object** dialog box, ensure that the correct partition function is selected, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To split a single partition into two partitions  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
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
  
#### To merge two partitions into one partition  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
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
  
 For more information, see [ALTER PARTITION FUNCTION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-partition-function-transact-sql).  
  
  
