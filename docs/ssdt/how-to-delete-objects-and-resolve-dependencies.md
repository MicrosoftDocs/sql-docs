---
title: Delete Objects and Resolve Dependencies
description: Find out how to rename or delete database objects. See which dependent objects SSDT automatically updates, and which dependencies you need to update.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "Microsoft.VisualStudio.Data.Tools.Project.HelpKeywords.SqlProjectDropDatabaseConfirmationDialog"
  - "sql.data.tools.dropdatabaseconfirmation.dialog"
  - "sql.data.tools.dropmultipledatabasesconfirmation.dialog"
---

# How to: Delete objects and resolve dependencies

When you rename or delete an object in **SQL Server Object Explorer**, SQL Server Data Tools (SSDT) automatically detects all its dependency objects, and prepares an `ALTER` script to rename or drop the dependency as needed.

## Delete a database

1. Right-click a database in **SQL Server Object Explorer**, and select **Delete**.

1. Accept all the default settings in the **Delete Database** dialog, and select **OK**.

## Rename a table

1. Make sure that the `Customer` table isn't opened in either the Table Designer or the Transact-SQL Editor.

1. Expand the **Tables** node in **SQL Server Object Explorer**. Right-click the `Customer` table and select **Rename**.

1. Change the table name to **Customers** and press ENTER.

1. A **Database Update** operation is immediately invoked on your behalf. SSDT calls the `sp_rename` stored procedure on your behalf to rename the table. If there are any dependent objects such as foreign key constraints, they're also updated.

   > [!WARNING]  
   > Script-based dependencies such as references to a table from a view, or stored procedures aren't automatically updated by SSDT. After the rename, you can use the **Error List** pane to locate all other dependencies and manually fix them.

1. Apply the change following the steps in the previous [How to: Update a connected database with Power Buffer](how-to-update-a-connected-database-with-power-buffer.md) procedure.

1. Right-click the `Customers` table in **SQL Server Object Explorer** again, and select **View Data**. Table data is intact after the rename operation.

1. Right-click the `Products` table and select **View Code**. The foreign key reference has been automatically updated to `REFERENCES [dbo].[Customers] ([Id])` to reflect the renaming.

## Delete a table

1. Right-click the `Customers` table in **SQL Server Object Explorer**, and select **Delete**.

1. In the **Preview Database Updates** dialog, under **User Action**, notice that SSDT has identified all the dependent objects, in this case, a foreign key reference that is dropped.

1. Select **Update Database**.

1. Right-click the `Products` table in **SQL Server Object Explorer**, and select **View Code**. The foreign key reference to the `Customers` table is gone.

   > [!WARNING]  
   > If you already have the `Products` table opened in Table Designer or Transact-SQL Editor when the delete operation occurs, it doesn't automatically refresh to show the deletion of the foreign key reference. In addition, errors about unresolved references might show up in the **Error List**. To resolve this issue, close the Table Designer or Transact-SQL Editor, and reopen the Products table.
