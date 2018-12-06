---
title: "How to: Create Database Objects Using Table Designer | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.design.table.scriptpanel"
  - "sql.data.tools.design.table.context.view"
ms.assetid: 9c9479c1-9bfc-4039-837e-e53fce67723d
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Create Database Objects Using Table Designer
Not only is the new **SQL Server** node in **SQL Server Object Explorer** very similar to SSMS visually, but you can create new objects using contextual menus that function like their SSMS-counterparts.  
  
For example, you can create a new database under the **Databases** node. Similarly, you can select a specific database and create or edit table definitions and their related programming objects on-the-fly using the new Table Designer. From the Table Designer, you can switch to a script pane which allows you to directly edit the script that defines this table.  
  
### To create a new database  
  
1.  In **SQL Server Object Explorer**, under the **SQL Server** node, expand your connected server instance.  
  
2.  Right-click the **Databases** node and select **Add New Database**.  
  
3.  Rename the new database to **Trade**.  
  
### To create new tables using the Table Designer  
  
1.  Expand the newly created **Trade** node. Right-click the **Tables** node and select **Add New Table**.  
  
2.  The Table Designer opens in a new window. The designer consists of the Columns Grid, Script Pane and Context Pane. The Columns Grid lists all the columns in the table. We will revisit other components of the designer in later procedures.  
  
3.  In the Script Pane, rename the new table to `Suppliers`. Specifically, replace  
  
    ```  
    CREATE TABLE [dbo].[Table1]  
    ```  
  
    with  
  
    ```  
    CREATE TABLE [dbo].[Suppliers]  
    ```  
  
4.  Click the empty row in the Columns Grid to add a new column to the table.  Enter **CompanyName** for the **Name** field, **nvarchar (128)** for **Data Type** and uncheck the **Allow Nulls** field. As you tab away from the fields, notice that the Script Pane is updated immediately.  
  
5.  Add another new column. Enter **Address** for the **Name** field, **nvarchar (MAX)** for **Data Type** and uncheck the **Allow Nulls** field.  
  
    > [!WARNING]  
    > When you are editing objects from a connected database, do not save them to your local drive. To save your changes to the database properly, follow the steps in the next [How to: Update a Connected Database with Power Buffer](../ssdt/how-to-update-a-connected-database-with-power-buffer.md) procedure.  
  
6.  Repeat the above steps to create another table named **Customer**. This time, add the following columns to the Customer table using the Columns Grid. And remember to change the script so that the table's name is `[dbo].[Customer]`.  
  
    |Name|Data Type|**Allow Nulls**|  
    |--------|-------------|-------------------|  
    |Id|int|unchecked|  
    |Name|nvarchar (128)|unchecked|  
  
7.  Create one more table named **Products**. Add the following columns to the Products table using the Columns Grid. And remember to change the script so that the table's name is `[dbo].[Products]`.  
  
    |Name|Data Type|**Allow Nulls**|  
    |--------|-------------|-------------------|  
    |Id|int|unchecked|  
    |Name|nvarchar (128)|unchecked|  
    |ShelfLife|int|checked|  
    |SupplierId|int|checked|  
    |CustomerId|int|checked|  
  
### To create a new check constraint using the Table Designer  
  
1.  The Context Pane of the Table Designer gives you a logical view of the table definition (Keys, Constraints, Triggers, etc.), and enables you to select an object to highlight its relationships to individual columns.  
  
    For the Products table, right-click the **Check Constraints** node in the Context Pane of the table designer, and select **Add New Check Constraint**.  
  
2.  Notice that the node count automatically increments by 1.  
  
3.  Click the Script Pane, and replace the default definition of the constraint with the following.  
  
    ```  
    CONSTRAINT [CK_Products_ShelfLife] CHECK ([ShelfLife] <5),  
    ```  
  
    This constraint will limit the value of ShelfLife for a row to be under 5.  
  
### To create new foreign key references using the Table Designer  
  
1.  For the Products table, right-click the **Foreign Keys** node in the Context Pane, and select **Add New Foreign Key**.  
  
2.  Notice that the node count automatically increments by 1.  
  
3.  Click the Script Pane, and replace the default definition of the foreign key reference with the following.  
  
    ```  
    CONSTRAINT [FK_Products_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([Id]),  
    ```  
  
4.  Repeat the steps above to add another foreign key reference to the Products table. This time, replace the default definition with the following.  
  
    ```  
    CONSTRAINT [FK_Products_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])  
    ```  
  
## See Also  
[Manage Tables, Relationships, and Fix Errors](../ssdt/manage-tables-relationships-and-fix-errors.md)  
  
