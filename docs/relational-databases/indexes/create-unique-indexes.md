---
title: "Create Unique Indexes"
description: Create Unique Indexes
author: MikeRayMSFT
ms.author: mikeray
ms.date: "02/17/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "unique indexes"
  - "designing indexes [SQL Server], unique"
  - "clustered indexes, unique"
  - "indexes [SQL Server], unique"
  - "nonclustered indexes [SQL Server], unique"
  - "unique indexes, design guidelines"
ms.assetid: 56b5982e-cb94-46c0-8fbb-772fc275354a
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Unique Indexes
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This topic describes how to create a unique index on a table in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. A unique index guarantees that the index key contains no duplicate values and therefore every row in the table is in some way unique. There are no significant differences between creating a UNIQUE constraint and creating a unique index that is independent of a constraint. Data validation occurs in the same manner, and the query optimizer does not differentiate between a unique index created by a constraint or manually created. However, creating a UNIQUE constraint on the column makes the objective of the index clear. For more information on UNIQUE constraints, see [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md).  
  
 When you create a unique index, you can set an option to ignore duplicate keys. If this option is set to **Yes** and you attempt to create duplicate keys by adding data that affects multiple rows (with the INSERT statement), the row containing a duplicate is not added. If it is set to **No**, the entire insert operation fails and all the data is rolled back.  
  
> [!NOTE]  
>  You cannot create a unique index on a single column if that column contains NULL in more than one row. Similarly, you cannot create a unique index on multiple columns if the combination of columns contains NULL in more than one row. These are treated as duplicate values for indexing purposes.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Benefits of a Unique Index](#Benefits)  
  
     [Typical Implementations](#Implementations)  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a unique index on a table, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Benefits"></a> Benefits of a Unique Index  
  
-   Multicolumn unique indexes guarantee that each combination of values in the index key is unique. For example, if a unique index is created on a combination of **LastName**, **FirstName**, and **MiddleName** columns, no two rows in the table could have the same combination of values for these columns.  
  
-   Provided that the data in each column is unique, you can create both a unique clustered index and multiple unique nonclustered indexes on the same table.  
  
-   Unique indexes ensure the data integrity of the defined columns.  
  
-   Unique indexes provide additional information helpful to the query optimizer that can produce more efficient execution plans.  
  
###  <a name="Implementations"></a> Typical Implementations  
 Unique indexes are implemented in the following ways:  
  
-   **PRIMARY KEY or UNIQUE constraint**  
  
     When you create a PRIMARY KEY constraint, a unique clustered index on the column or columns is automatically created if a clustered index on the table does not already exist and you do not specify a unique nonclustered index. The primary key column cannot allow NULL values.  
  
     When you create a UNIQUE constraint, a unique nonclustered index is created to enforce a UNIQUE constraint by default. You can specify a unique clustered index if a clustered index on the table does not already exist.  
  
     For more information, see [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md) and [Primary and Foreign Key Constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md).  
  
-   **Index independent of a constraint**  
  
     Multiple unique nonclustered indexes can be defined on a table.  
  
     For more information, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
-   **Indexed view**  
  
     To create an indexed view, a unique clustered index is defined on one or more view columns. The view is executed and the result set is stored in the leaf level of the index in the same way table data is stored in a clustered index. For more information, see [Create Indexed Views](../../relational-databases/views/create-indexed-views.md).  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   A unique index, UNIQUE constraint, or PRIMARY KEY constraint cannot be created if duplicate key values exist in the data.  
  
-   A unique nonclustered index can contain included nonkey columns. For more information, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a unique index by using the Table Designer  
  
1.  In Object Explorer, expand the database that contains the table on which you want to create a unique index.  
  
2.  Expand the **Tables** folder.  
  
3.  Right-click the table on which you want to create a unique index and select **Design**.  
  
4.  On the **Table Designer** menu, select **Indexes/Keys**.  
  
5.  In the **Indexes/Keys** dialog box, click **Add**.  
  
6.  Select the new index in the **Selected Primary/Unique Key or Index** text box.  
  
7.  In the main grid, under **(General)**, select **Type** and then choose **Index** from the list.  
  
8.  Select **Columns**, and then click the ellipsis **(...)**.  
  
9. In the **Index Columns** dialog box, under **Column Name**, select the columns you want to index. You can select up to 16 columns. For optimal performance, select only one or two columns per index. For each column you select, indicate whether the index arranges values of this column in ascending or descending order.  
  
10. When all columns for the index are selected, click **OK**.  
  
11. In the grid, under **(General)**, select **Is Unique** and then choose **Yes** from the list.  
  
12. Optional: In the main grid, under **Table Designer**, select **Ignore Duplicate Keys** and then choose **Yes** from the list. Do this if you want to ignore attempts to add data that would create a duplicate key in the unique index.  
  
13. Click **Close**.  
  
14. On the **File** menu, click **Save**_table\_name_.  
  
#### Create a unique index by using Object Explorer  
  
1.  In Object Explorer, expand the database that contains the table on which you want to create a unique index.  
  
2.  Expand the **Tables** folder.  
  
3.  Expand the table on which you want to create a unique index.  
  
4.  Right-click the **Indexes** folder, point to **New Index**, and select **Non-Clustered Index...**.  
  
5.  In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.  
  
6.  Select the **Unique** check box.  
  
7.  Under **Index key columns**, click **Add...**.  
  
8.  In the **Select Columns from**_table\_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the unique index.  
  
9. Click **OK**.  
  
10. In the **New Index** dialog box, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a unique index on a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Find an existing index named AK_UnitMeasure_Name 
    -- on the Production.UnitMeasure table and delete it if found. 
    IF EXISTS (SELECT name from sys.indexes  
               WHERE name = N'AK_UnitMeasure_Name'
               AND object_id = OBJECT_ID(N'Production.UnitMeasure', N'U'))   
       DROP INDEX AK_UnitMeasure_Name ON Production.UnitMeasure;   
    GO  
    -- Create a unique index called AK_UnitMeasure_Name  
    -- on the Production.UnitMeasure table using the Name column.  
    CREATE UNIQUE INDEX AK_UnitMeasure_Name   
       ON Production.UnitMeasure (Name);   
    GO  
    ```  
  
 For more information, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
  
