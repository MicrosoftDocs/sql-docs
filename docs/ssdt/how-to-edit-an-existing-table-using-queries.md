---
title: Edit an Existing Table Using Queries
description: "Learn how to use a Transact-SQL query to edit a table's definition or data. View examples of editing a table definition and inserting rows into a table."
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Edit an existing table using queries

You can edit the definition of a table or its data by writing a Transact-SQL query. To view or enter data in a table visually, use the Data Editor as described in [Connected Database Development](../ssdt/connected-database-development.md).

## Edit the definition of an existing table

1. Expand the **Tables** node of the `Trade` database in **SQL Server Object Explorer**, and right-click **dbo.Suppliers**.

1. Select **View Designer** to view the table schema in the Table Designer.

1. Check the **Allow Nulls** box for the `Address` column. The corresponding code in the script pane is changed to `NULL` immediately.

1. Update the database following the steps in the [How to: Update a connected database with Power Buffer](how-to-update-a-connected-database-with-power-buffer.md) article.

### Populate data in new tables using a Transact-SQL query

1. Right-click the `Trade` database node and select **New Query**.

1. In the script pane, paste in the following code.

   ```sql
   INSERT INTO dbo.Suppliers
   VALUES (1, 'NorthWind Traders', 'Seattle, WA'),
   (2, 'Contoso', 'Tacoma, WA');
   GO

   INSERT dbo.Customer
   VALUES (1, 'Fourth Coffee');
   GO

   INSERT dbo.Products
   VALUES (1, 'Apples', 0, 1, 1),
   (2, 'Instant Coffee', 1, 2, 1);
   GO
   ```

1. Select the **Execute Query** button to run this query. The following in the **Message** pane indicate that the rows are successfully added to the tables.

   ```output
   (2 row(s) affected)(1 row(s) affected)(2 row(s) affected)
   ```

1. Replace the code in the script pane with the following and execute the query. This attempts to add a new row to the `Products` table with a `ShelfLife` of 6.

   ```sql
   INSERT dbo.Products
   VALUES (3, 'Potato Chips', 6, 1, 1);
   GO
   ```

1. The **Message** pane indicates that the `INSERT` statement conflicts with your existing check constraint, which limits the value of `ShelfLife` to be under 5. The Products table isn't updated due to the statement failing an existing constraint.

1. Change the code into the following and run the query again. The row is updated successfully this time.

   ```sql
   INSERT dbo.Products
   VALUES (3, 'Potato Chips', 2, 1, 1);
   GO
   ```

## Related content

- [Manage tables, relationships, and fix errors](manage-tables-relationships-and-fix-errors.md)
- [Use Transact-SQL Editor to edit and execute scripts](use-transact-sql-editor-to-edit-and-execute-scripts.md)
