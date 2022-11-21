---
title: "Create Clustered Indexes"
description: Create Clustered Indexes
author: MikeRayMSFT
ms.author: mikeray
ms.date: "02/17/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "index creation [SQL Server], clustered indexes"
  - "clustered indexes, creating"
  - "clustered indexes, PRIMARY KEY constraint"
  - "clustered indexes, UNIQUE constraint"
  - "indexes [SQL Server], clustered"
ms.assetid: 47148383-c2c7-4f08-a9e4-7016bf2d1d13
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Clustered Indexes
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  You can create clustered indexes on tables by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. With few exceptions, every table should have a clustered index. Besides improving query performance, a clustered index can be rebuilt or reorganized on demand to control table fragmentation. A clustered index can also be created on a view. (Clustered indexes are defined in the topic [Clustered and Nonclustered Indexes Described](../../relational-databases/indexes/clustered-and-nonclustered-indexes-described.md).)  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Typical Implementations](#Implementations)  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a clustered index on a table, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Implementations"></a> Typical Implementations  
 Clustered indexes are implemented in the following ways:  
  
-   **PRIMARY KEY and UNIQUE constraints**  
  
     When you create a PRIMARY KEY constraint, a unique clustered index on the column or columns is automatically created if a clustered index on the table does not already exist and you do not specify a unique nonclustered index. The primary key column cannot allow NULL values.  
  
     When you create a UNIQUE constraint, a unique nonclustered index is created to enforce a UNIQUE constraint by default. You can specify a unique clustered index if a clustered index on the table does not already exist.  
  
     An index created as part of the constraint is automatically given the same name as the constraint name. For more information, see [Primary and Foreign Key Constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md) and [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md).  
  
-   **Index independent of a constraint**  
  
     You can create a clustered index on a column other than primary key column if a nonclustered primary key constraint was specified.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   When a clustered index structure is created, disk space for both the old (source) and new (target) structures is required in their respective files and filegroups. The old structure is not deallocated until the complete transaction commits. Additional temporary disk space for sorting may also be required. For more information, see [Disk Space Requirements for Index DDL Operations](../../relational-databases/indexes/disk-space-requirements-for-index-ddl-operations.md).  
  
-   If a clustered index is created on a heap with several existing nonclustered indexes, all the nonclustered indexes must be rebuilt so that they contain the clustering key value instead of the row identifier (RID). Similarly, if a clustered index is dropped on a table that has several nonclustered indexes, the nonclustered indexes are all rebuilt as part of the DROP operation. This may take significant time on large tables.  
  
     The preferred way to build indexes on large tables is to start with the clustered index and then build any nonclustered indexes. Consider setting the ONLINE option to ON when you create indexes on existing tables. When set to ON, long-term table locks are not held. This enables queries or updates to the underlying table to continue. For more information, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).  
  
-   The index key of a clustered index cannot contain **varchar** columns that have existing data in the ROW_OVERFLOW_DATA allocation unit. If a clustered index is created on a **varchar** column and the existing data is in the IN_ROW_DATA allocation unit, subsequent insert or update actions on the column that would push the data off-row will fail. To obtain information about tables that might contain row-overflow data, use the [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) dynamic management function.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a clustered index by using Object Explorer  
  
1.  In Object Explorer, expand the table on which you want to create a clustered index.  
  
2.  Right-click the **Indexes** folder, point to **New Index**, and select **Clustered Index...**.  
  
3.  In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.  
  
4.  Under **Index key columns**, click **Add...**.  
  
5.  In the **Select Columns from**_table\_name_ dialog box, select the check box of the table column to be added to the clustered index.  
  
6.  Click **OK**.  
  
7.  In the **New Index** dialog box, click **OK**.  
  
#### To create a clustered index by using the Table Designer  
  
1.  In Object Explorer, expand the database on which you want to create a table with a clustered index.  
  
2.  Right-click the **Tables** folder and click **New Table...**.  
  
3.  Create a new table as you normally would. For more information, see [Create Tables &#40;Database Engine&#41;](../../relational-databases/tables/create-tables-database-engine.md).  
  
4.  Right-click the new table created above and click **Design**.  
  
5.  On the **Table Designer** menu, click **Indexes/Keys**.  
  
6.  In the **Indexes/Keys** dialog box, click **Add**.  
  
7.  Select the new index in the **Selected Primary/Unique Key or Index** text box.  
  
8.  In the grid, select **Create as Clustered**, and choose **Yes** from the drop-down list to the right of the property.  
  
9. Click **Close**.  
  
10. On the **File** menu, click **Save**_table\_name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a clustered index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Create a new table with three columns.  
    CREATE TABLE dbo.TestTable  
        (TestCol1 int NOT NULL,  
         TestCol2 nchar(10) NULL,  
         TestCol3 nvarchar(50) NULL);  
    GO  
    -- Create a clustered index called IX_TestTable_TestCol1  
    -- on the dbo.TestTable table using the TestCol1 column.  
    CREATE CLUSTERED INDEX IX_TestTable_TestCol1   
        ON dbo.TestTable (TestCol1);   
    GO  
    ```  
  
 For more information, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
## See Also  
 [Create Primary Keys](../../relational-databases/tables/create-primary-keys.md)   
 [Create Unique Constraints](../../relational-databases/tables/create-unique-constraints.md)  
  
  
