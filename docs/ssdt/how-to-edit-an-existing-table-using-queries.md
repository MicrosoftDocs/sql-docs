---
title: Edit an Existing Table using Queries
description: "Learn how to use a Transact-SQL query to edit a table's definition or data. View examples of editing a table definition and inserting rows into a table."
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: 58f4de8e-97b4-4bcb-953f-f3d428432491
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Edit an Existing Table using Queries

You can edit the definition of a table or its data by writing a Transact\-SQL query. To view or enter data in a table visually, use the Data Editor as described in [Connected Database Development](../ssdt/connected-database-development.md).  
  
> [!WARNING]  
> The following procedures use entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To edit the definition of an existing table  
  
1.  Expand the **Tables** node of the **Trade** database in **SQL Server Object Explorer**, and right-click **dbo.Suppliers**.  
  
2.  Select **View Designer** to view the table schema in the Table Designer.  
  
3.  Check the **Allow Nulls** box for the **Address** column. Notice that the corresponding code in the script pane is changed to `NULL` immediately.  
  
4.  Update the database following the steps in the [How to: Update a Connected Database with Power Buffer](../ssdt/how-to-update-a-connected-database-with-power-buffer.md) topic.  
  
### To populate data in new tables using a Transact\-SQL query  
  
1.  Right-click the **Trade** database node and select **New Query**.  
  
2.  In the script pane, paste in the following code.  
  
    ```  
    insert into dbo.Suppliers values  
    (1, 'NorthWind Traders', 'Seattle, WA'),  
    (2, 'Contoso', 'Tacoma, WA')  
    GO  
  
    insert dbo.Customer values  
    (1, 'Fourth Coffee')  
    GO  
  
    insert dbo.Products values  
    (1, 'Apples', 0, 1, 1),  
    (2, 'Instant Coffee', 1, 2, 1)  
    GO  
    ```  
  
3.  Click the **Execute Query** button to run this query. The followings in the **Message** pane indicate that the rows are successfully added to the tables.  
  
**(2 row(s) affected)(1 row(s) affected)(2 row(s) affected)**  
  
4.  Replace the code in the script pane with the following and execute the query. This will attempt to add a new row to the `Products` table with a `ShelfLife` of 6.  
  
    ```  
    insert dbo.Products values  
    (3, 'Potato Chips', 6, 1, 1)  
    GO  
    ```  
  
5.  The **Message** pane indicates that the `INSERT` statement conflicts with your existing check constraint, which limits the value of `ShelfLife` to be under 5. The Products table is not updated due to the statement failing an existing constraint.  
  
6.  Change the code into the following and run the query again. Notice that the row is updated successfully this time.  
  
    ```  
    insert dbo.Products values  
    (3, 'Potato Chips', 2, 1, 1)  
    GO  
    ```  
  
## See Also  
[Manage Tables, Relationships, and Fix Errors](../ssdt/manage-tables-relationships-and-fix-errors.md)  
[Use Transact-SQL Editor to Edit and Execute Scripts](../ssdt/use-transact-sql-editor-to-edit-and-execute-scripts.md)  
  
