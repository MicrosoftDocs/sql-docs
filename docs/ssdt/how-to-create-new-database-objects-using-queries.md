---
title: Create New Database Objects Using Queries
description: Get acquainted with the Transact-SQL Editor. See how to open this editor, and view examples that show how to use it to create a new table, function, or view.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Create new database objects using queries

If you prefer to use scripts to create or edit views, stored procedures, functions, triggers, or user-defined-types, you can use the Transact-SQL Editor. The Transact-SQL Editor provides IntelliSense and other language support. For more information, see [Use Transact-SQL Editor to edit and execute scripts](use-transact-sql-editor-to-edit-and-execute-scripts.md).

The Transact-SQL Editor is invoked when you use the **View Code** contextual menu to open a database entity in a connected database or a project. It automatically opens when you use the **New Query** contextual menu from the SQL Server Object Explorer, or when you add a new script object to a database project. If you aren't connected to a database but want to execute a query against it, you can also use the **New Query Connection** dialog box by selecting **Transact-SQL Editor** menu from the **SQL** menu to connect to a database and launch the Transact-SQL Editor.

## Create a new table using a Transact-SQL query

1. Right-click the `Trade` database node and select **New Query**.

1. In the script pane, paste in this code:

   ```sql
   CREATE TABLE [dbo].[Fruits]
   (
       [Id] INT NOT NULL,
       [Perishable] BIT DEFAULT ((1)) NULL,
       PRIMARY KEY CLUSTERED ([Id] ASC),
       FOREIGN KEY ([Id]) REFERENCES [dbo].[Products] ([Id])
   );
   ```

1. Select the **Execute Query** button in the Transact-SQL Editor toolbar to run this query.

1. Right-click the `Trade` database in **SQL Server Object Explorer** and select **Refresh**. A new `Fruits` table has been added to the database.

## Create a new function

1. Replace the code in the current Transact-SQL Editor with the following script:

   ```sql
   CREATE FUNCTION [dbo].GetProductsBySupplier
   (@SupplierId INT)
   RETURNS
       @returntable TABLE (
           [Id] INT NOT NULL,
           [Name] NVARCHAR (128) NOT NULL,
           [Shelflife] INT NOT NULL,
           [SupplierId] INT NOT NULL,
           [CustomerId] INT NOT NULL)
   AS
   BEGIN
       INSERT @returntable
       SELECT *
       FROM Products AS p
       WHERE p.SupplierId = @SupplierId;
       RETURN;
   END
   ```

   This function returns all rows in the `Products` table whose `SupplierId` equals to the specified parameter. Select the **Execute Query** button in the Transact-SQL Editor toolbar to run this query.

1. In SQL Server Object Explorer, under the `Trade` node, expand the **Programmability** and **Functions** nodes. You can find the new function you created under **Table-valued Functions**.

## Create a new view

1. Replace the code in the current Transact-SQL Editor with the following. Then select the **Execute Query** button above the editor to run this query.

   ```sql
   CREATE VIEW [dbo].PerishableFruits AS
       SELECT p.Id,
              p.Name
       FROM dbo.Products AS p
            INNER JOIN dbo.Fruits AS f
                ON f.Id = p.Id
       WHERE f.Perishable = 1;
   ```

1. In SQL Server Object Explorer, under the `Trade` node, expand the **View** node to locate the new view you created.

## Related content

- [Manage tables, relationships, and fix errors](manage-tables-relationships-and-fix-errors.md)
- [Use Transact-SQL Editor to edit and execute scripts](use-transact-sql-editor-to-edit-and-execute-scripts.md)
