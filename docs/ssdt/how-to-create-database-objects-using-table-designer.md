---
title: Create Database Objects Using Table Designer
description: Learn how to create a new database in SQL Server Object Explorer. See how to create new tables, constraints, and foreign key references in Table Designer.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.design.table.scriptpanel"
  - "sql.data.tools.design.table.context.view"
  - "Microsoft.Data.Relational.Design.PW.RelationshipsDescriptor.OnDelete"
---

# How to: Create database objects using Table Designer

Not only is the **SQL Server** node in **SQL Server Object Explorer** similar to SQL Server Management Studio (SSMS) visually, but you can create new objects using contextual menus that function like their SSMS counterparts.

For example, you can create a new database under the **Databases** node. Similarly, you can select a specific database and create or edit table definitions and their related programming objects on-the-fly using the new Table Designer. From the Table Designer, you can switch to a script pane, which allows you to directly edit the script that defines this table.

## Create a new database

1. In **SQL Server Object Explorer**, under the **SQL Server** node, expand your connected server instance.
1. Right-click the **Databases** node and select **Add New Database**.
1. Rename the new database to `Trade`.

## Create new tables using the Table Designer

1. Expand the newly created `Trade` node. Right-click the **Tables** node and select **Add New Table**.

1. The Table Designer opens in a new window. The designer consists of the Columns Grid, Script pane, and Context pane. The Columns Grid lists all the columns in the table. We revisit other components of the designer in later procedures.

1. In the Script pane, rename the new table to `Suppliers`. Specifically, replace

   ```sql
   CREATE TABLE [dbo].[Table1]
   ```

   with

   ```sql
   CREATE TABLE [dbo].[Suppliers]
   ```

1. Select the empty row in the Columns Grid to add a new column to the table. Enter **CompanyName** for the **Name** field, **nvarchar (128)** for **Data Type** and uncheck the **Allow Nulls** field. As you tab away from the fields, notice that the Script pane is updated immediately.

1. Add another new column. Enter **Address** for the **Name** field, **nvarchar (MAX)** for **Data Type** and uncheck the **Allow Nulls** field.

   > [!WARNING]  
   > When you're editing objects from a connected database, don't save them to your local drive. To save your changes to the database properly, follow the steps in the next [How to: Update a connected database with Power Buffer](how-to-update-a-connected-database-with-power-buffer.md) procedure.

1. Repeat the above steps to create another table named **Customer**. This time, add the following columns to the Customer table using the Columns Grid. And remember to change the script so that the table's name is `[dbo].[Customer]`.

   | Name | Data type | Allow nulls |
   | --- | --- | --- |
   | `Id` | **int** | unchecked |
   | `Name` | **nvarchar (128)** | unchecked |

1. Create one more table named `Products`. Add the following columns to the Products table using the Columns Grid. And remember to change the script so that the table's name is `[dbo].[Products]`.

   | Name | Data type | Allow nulls |
   | --- | --- | --- |
   | `Id` | **int** | unchecked |
   | `Name` | **nvarchar (128)** | unchecked |
   | `ShelfLife` | **int** | checked |
   | `SupplierId` | **int** | checked |
   | `CustomerId` | **int** | checked |

## Create a new check constraint using the Table Designer

1. The Context pane of the Table Designer gives you a logical view of the table definition (Keys, Constraints, Triggers, etc.), and enables you to select an object to highlight its relationships to individual columns.

   For the Products table, right-click the **Check Constraints** node in the Context pane of the table designer, and select **Add New Check Constraint**.

1. The node count automatically increments by 1.

1. Select the Script pane, and replace the default definition of the constraint with the following.

   ```sql
   CONSTRAINT [CK_Products_ShelfLife] CHECK ([ShelfLife] <5),
   ```

   This constraint limits the value of ShelfLife for a row to be under 5.

## Create new foreign key references using the Table Designer

1. For the Products table, right-click the **Foreign Keys** node in the Context pane, and select **Add New Foreign Key**.

1. The node count automatically increments by 1.

1. Select the Script pane, and replace the default definition of the foreign key reference with the following code.

   ```sql
   CONSTRAINT [FK_Products_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([Id]),
   ```

1. Repeat the previous steps to add another foreign key reference to the Products table. This time, replace the default definition with the following code.

   ```sql
   CONSTRAINT [FK_Products_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
   ```

## Related content

- [Manage tables, relationships, and fix errors](manage-tables-relationships-and-fix-errors.md)
