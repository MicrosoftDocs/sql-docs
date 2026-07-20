---
title: Specify Fill Factor for an Index
description: Specify fill factor for an index
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/19/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "fill factor [SQL Server]"
  - "page splits [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Specify fill factor for an index

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This article describes what fill factor is and how to specify a fill factor value for an index using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

The fill factor option is provided for fine-tuning index data storage and performance. When an index is created or rebuilt, the fill factor value determines the percentage of space on each leaf-level page to be filled with data, reserving the remainder on each page as free space for future growth. For example, specifying a fill factor value of 80 means that 20 percent of each leaf-level page will be left empty, providing space for data growth. The empty space is reserved on each page of the index rather than at the end of the index.

The fill factor value is a percentage from 1 to 100, and the server-wide default is 0 which means that the leaf-level pages are filled to capacity.

> [!NOTE]  
> Fill factor values 0 and 100 are the same in all respects.

**In This Topic**  

- **Before you begin:**  

  [Performance Considerations](#Performance)

  [Security](#Security)

- **To specify a fill factor in an index, using:**

  [SQL Server Management Studio](#SSMSProcedure)

  [Transact-SQL](#TsqlProcedure)

## <a name="BeforeYouBegin"></a> Before You Begin

### <a name="Performance"></a> Performance Considerations

#### Page splits

When a new row is added to a full index page, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] moves approximately half the rows to a new page to make room for the new row. This reorganization is known as a page split. A page split makes room for new rows, but if it occurs in the middle of the index, it can take time to perform and is a resource intensive operation. Also, it can cause fragmentation that reduces the effectiveness of [page read-ahead](../reading-pages.md#read-ahead) during large index scans.

A well-chosen fill factor value can reduce page splits by providing enough space for index expansion when data is added in the middle of the index. If page splits affect performance, the index can be rebuilt by using a new or existing fill factor value in the 70-95 percent range. However, don't reduce the fill factor unnecessarily or set it too low. For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).

Most workloads perform optimally with the default fill factor (100 percent). With a reduced fill factor, the index requires more storage space, memory, and disk I/O, which can decrease performance. Even for a write-intensive workload, database reads typically outnumber database writes by a factor of five to ten. Therefore, specifying a fill factor other than the default can increase resource utilization by an amount inversely proportional to the fill factor setting.

For example, a fill factor value of 50 doubles the disk I/O and memory required to read and cache the same amount of data.

#### Data added to the end of the table

A lower fill factor might reduce page splits and improve performance if the new data is evenly distributed throughout the index. However, if new data is added to the end of the index, the empty space in the index pages might not be filled. For example, if the index key column is an `IDENTITY` column, the key for new rows is always increasing and the index rows are logically added to the end of the index.

### <a name="Security"></a> Security

#### <a name="Permissions"></a> Permissions

Requires the `ALTER` permission on the table or view. User must be a member of the `sysadmin` fixed server role or the `db_ddladmin` and `db_owner` fixed database roles.

## <a name="SSMSProcedure"></a> Using SQL Server Management Studio

#### To specify a fill factor by using Table Designer
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to specify an index's fill factor.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Right-click the table on which you want to specify an index's fill factor and select **Design**.  
  
4.  On the **Table Designer** menu, click **Indexes/Keys**.  
  
5.  Select the index with the fill factor that you want to specify.  
  
6.  Expand **Fill Specification**, select the **Fill Factor** row and enter the fill factor you want in the row.  
  
7.  Click **Close**.  
  
8.  On the **File** menu, select **Save**_table_name_.  
  
#### To specify a fill factor in an index by using Object Explorer  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to specify an index's fill factor.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to specify an index's fill factor.  
  
4.  Click the plus sign to expand the **Indexes** folder.  
  
5.  Right-click the index with the fill factor that you want to specify and select **Properties**.  
  
6.  Under **Select a page**, select **Options**.  
  
7.  In the **Fill factor** row, enter the fill factor that you want.  
  
8.  Click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To specify a fill factor in an existing index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example rebuilds an existing index and applies the specified fill factor during the rebuild operation.  
  
    ```sql
    USE AdventureWorks2022;  
    GO  
    -- Rebuilds the IX_Employee_OrganizationLevel_OrganizationNode index   
    -- with a fill factor of 80 on the HumanResources.Employee table.  
  
    ALTER INDEX IX_Employee_OrganizationLevel_OrganizationNode ON HumanResources.Employee  
    REBUILD WITH (FILLFACTOR = 80);   
    GO  
    ```  
  
#### Another way to specify a fill factor in an index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```sql
    USE AdventureWorks2022;  
    GO  
    -- Drops and re-creates the IX_Employee_OrganizationLevel_OrganizationNode index
    -- on the HumanResources.Employee table with a fill factor of 80.   
  
    CREATE INDEX IX_Employee_OrganizationLevel_OrganizationNode ON HumanResources.Employee  
       (OrganizationLevel, OrganizationNode)   
    WITH (DROP_EXISTING = ON, FILLFACTOR = 80);   
    GO  
    ```  
  
For more information, see [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md).
