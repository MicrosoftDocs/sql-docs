---
title: "Specify Fill Factor for an Index | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "fill factor [SQL Server]"
  - "page splits [SQL Server]"
ms.assetid: 237a577e-b42b-4adb-90cf-aa7fb174f3ab
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Specify Fill Factor for an Index
  This topic describes what fill factor is and how to specify a fill factor value on an index in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 The fill-factor option is provided for fine-tuning index data storage and performance. When an index is created or rebuilt, the fill-factor value determines the percentage of space on each leaf-level page to be filled with data, reserving the remainder on each page as free space for future growth. For example, specifying a fill-factor value of 80 means that 20 percent of each leaf-level page will be left empty, providing space for index expansion as data is added to the underlying table. The empty space is reserved between the index rows rather than at the end of the index.  
  
 The fill-factor value is a percentage from 1 to 100, and the server-wide default is 0 which means that the leaf-level pages are filled to capacity.  
  
> [!NOTE]  
>  Fill-factor values 0 and 100 are the same in all respects.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Performance Considerations](#Performance)  
  
     [Security](#Security)  
  
-   **To specify a fill factor in an index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Performance"></a> Performance Considerations  
  
#### Page Splits  
 A correctly chosen fill-factor value can reduce potential page splits by providing enough space for index expansion as data is added to the underlying table.When a new row is added to a full index page, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] moves approximately half the rows to a new page to make room for the new row. This reorganization is known as a page split. A page split makes room for new records, but can take time to perform and is a resource intensive operation. Also, it can cause fragmentation that causes increased I/O operations. When frequent page splits occur, the index can be rebuilt by using a new or existing fill-factor value to redistribute the data. For more information, see [Reorganize and Rebuild Indexes](reorganize-and-rebuild-indexes.md).  
  
 Although a low, nonzero fill-factor value may reduce the requirement to split pages as the index grows, the index will require more storage space and can decrease read performance. Even for an application oriented for many insert and update operations, the number of database reads typically outnumber database writes by a factor of 5 to 10. Therefore, specifying a fill factor other than the default can decrease database read performance by an amount inversely proportional to the fill-factor setting. For example, a fill-factor value of 50 can cause database read performance to decrease by two times. Read performance is decreased because the index contains more pages, therefore increasing the disk IO operations required to retrieve the data.  
  
#### Adding Data to the End of the Table  
 A nonzero fill factor other than 0 or 100 can be good for performance if the new data is evenly distributed throughout the table. However, if all the data is added to the end of the table, the empty space in the index pages will not be filled. For example, if the index key column is an IDENTITY column, the key for new rows is always increasing and the index rows are logically added to the end of the index. If existing rows will be updated with data that lengthens the size of the rows, use a fill factor of less than 100. The extra bytes on each page will help to minimize page splits caused by extra length in the rows.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
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
  
    ```  
    USE AdventureWorks2012;  
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
  
    ```  
    USE AdventureWorks2012;  
    GO  
    /* Drops and re-creates the IX_Employee_OrganizationLevel_OrganizationNode index on the HumanResources.Employee table with a fill factor of 80.  
    */  
  
    CREATE INDEX IX_Employee_OrganizationLevel_OrganizationNode ON HumanResources.Employee  
       (OrganizationLevel, OrganizationNode)   
    WITH (DROP_EXISTING = ON, FILLFACTOR = 80);   
    GO  
    ```  
  
 For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql).  
  
  
