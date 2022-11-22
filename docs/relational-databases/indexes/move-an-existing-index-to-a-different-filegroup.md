---
title: "Move an Existing Index to a Different Filegroup"
description: Move an Existing Index to a Different Filegroup
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/11/2021"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "moving tables"
  - "switching filegroups for index"
  - "moving indexes"
  - "indexes [SQL Server], moving"
  - "filegroups [SQL Server], switching"
ms.assetid: 167ebe77-487d-4ca8-9452-4b2c7d5cb96e
---
# Move an existing index to a different filegroup

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to move an existing index from its current filegroup to a different filegroup in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

For design considerations including why you might want to place a nonclustered index on a different filegroup, see [Index Placement on Filegroups or Partitions Schemes](../sql-server-index-design-guide.md#Index_placement).

##  <a name="BeforeYouBegin"></a> Before you begin  
  
###  <a name="Restrictions"></a> Limitations and restrictions  
  
-   If a table has a clustered index, moving the clustered index to a new filegroup moves the table to that filegroup.  
  
-   You cannot move indexes created using a UNIQUE or PRIMARY KEY constraint using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. To move these indexes use the [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) statement with the (DROP_EXISTING=ON) option in [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To move an existing index to a different filegroup using Table Designer  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table containing the index that you want to move.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Right-click the table containing the index that you want to move and select **Design**.  
  
4.  On the **Table Designer** menu, click **Indexes/Keys**.  
  
5.  Select the index that you want to move.  
  
6.  In the main grid, expand **Data Space Specification**.  
  
7.  Select **Filegroup or Partition Scheme Name** and select from the list the filegroup or partition scheme to where you want to move the index.  
  
8.  Click **Close**.  
  
9. On the **File** menu, select **Save**_table_name_.  

#### To move an existing index to a different filegroup in Object Explorer  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table containing the index that you want to move.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table containing the index that you want to move.  
  
4.  Click the plus sign to expand the **Indexes** folder.  
  
5.  Right-click the index that you want to move and select **Properties**.  
  
6.  Under **Select a page**, select **Storage**.  
  
7.  Select the filegroup in which to move the index.  
  
     If the table or index is partitioned, select the partition scheme in which to move the index. For more information about partitioned indexes, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
     If you are moving a clustered index, you can use online processing. Online processing allows concurrent user access to the underlying data and to nonclustered indexes during the index operation. For more information, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).  
  
     On multiprocessor computers using [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], you can configure the number of processors used to execute the index statement by specifying a maximum degree of parallelism value. The Parallel indexed operations feature is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see Features Supported by the Editions of SQL Server 2016. For more information about Parallel indexed operations, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
8.  Click **OK**.  
  
 The following information is available on the **Storage** page of the **Index Properties -** _index_name_ dialog box:  
  
 **Filegroup**  
 Stores the index in the specified filegroup. The list only displays standard (row) filegroups. The default list selection is the PRIMARY filegroup of the database.  
  
 **Filestream filegroup**  
 Specifies the filegroup for FILESTREAM data. This list displays only FILESTREAM filegroups. The default list selection is the PRIMARY FILESTREAM filegroup.  
  
 **Partition scheme**  
 Stores the index in a partition scheme. Clicking **Partition Scheme** enables the grid below. The default list selection is the partition scheme that is used for storing the table data. When you select a different partition scheme in the list, the information in the grid is updated.  
  
 The partition scheme option is unavailable if there are no partition schemes in the database.  
  
 **Filestream partition scheme**  
 Specifies the partition scheme for FILESTREAM data. The partition scheme must be symmetric with the scheme that is specified in the **Partition scheme** option.  
  
 If the table is not partitioned, the field is blank.  
  
 **Partition Scheme Parameter**  
 Displays the name of the column that participates in the partition scheme.  
  
 **Table Column**  
 Select the table or view to map to the partition scheme.  
  
 **Column Data Type**  
 Displays data type information about the column.  
  
> [!NOTE]  
>  If the table column is a computed column, **Column Data Type** displays "computed column."  
  
 **Allow online processing of DML statements while moving the index**  
 Allows users to access the underlying table or clustered index data and any associated nonclustered indexes during the index operation.  
  
> [!NOTE]  
>  This option is not available for XML indexes, or if the index is a disabled clustered index.  
  
 **Set maximum degree of parallelism**  
 Limits the number of processors to use during parallel plan execution. The default value, 0, uses the actual number of available CPUs. Setting the value to 1 suppresses parallel plan generation; setting the value to a number greater than 1 restricts the maximum number of processors used by a single query execution. This option only becomes available if the dialog box is in the **Rebuild** or **Recreate** state.  
  
> [!NOTE]  
>  If a value greater than the number of available CPUs is specified, the actual number of available CPUs is used.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To move an existing index to a different filegroup  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Creates the TransactionsFG1 filegroup on the AdventureWorks2012 database  
    ALTER DATABASE AdventureWorks2012  
    ADD FILEGROUP TransactionsFG1;  
    GO  
    /* Adds the TransactionsFG1dat3 file to the TransactionsFG1 filegroup. Please note that you will have to change the filename parameter in this statement to execute it without errors.  
    */  
    ALTER DATABASE AdventureWorks2012   
    ADD FILE   
    (  
        NAME = TransactionsFG1dat3,  
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13\MSSQL\DATA\TransactionsFG1dat3.ndf',  
        SIZE = 5MB,  
        MAXSIZE = 100MB,  
        FILEGROWTH = 5MB  
    )  
    TO FILEGROUP TransactionsFG1;  
    GO  
    /*Creates the IX_Employee_OrganizationLevel_OrganizationNode index  
      on the TransactionsPS1 filegroup and drops the original IX_Employee_OrganizationLevel_OrganizationNode index.  
    */  
    CREATE NONCLUSTERED INDEX IX_Employee_OrganizationLevel_OrganizationNode  
        ON HumanResources.Employee (OrganizationLevel, OrganizationNode)  
        WITH (DROP_EXISTING = ON)  
        ON TransactionsFG1;  
    GO  
    ```  

## Next steps

For more information, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
[SQL Server Index Architecture and Design Guide](../sql-server-index-design-guide.md)
