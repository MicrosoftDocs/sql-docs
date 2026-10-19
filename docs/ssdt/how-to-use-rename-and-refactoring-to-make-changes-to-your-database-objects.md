---
title: Rename and Refactoring to Make Changes to Your Database Objects
description: Learn about the Rename and Refactor menus in the Transact-SQL Editor. See how to rename types, switch object schemas, expand wildcards, and fully qualify names.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.dbrefactoring.previewdialog"
  - "sql.data.tools.editor.howto.refactoring"
  - "sql.data.tools.dbrefactoring.renamedialog"
  - "sql.data.tools.dbrefactoring.moveschemadialog"
  - "sql.data.tools.dbrefactoring.renameserverdatabasedialog"
---

# How to: Use rename and refactoring to make changes to your database objects

The **Refactor** contextual menu in the Transact-SQL Editor allows you to rename or move an object to a different schema and preview all affected areas before committing the change. You can also use the **Refactor** menu to fully qualify all references to database objects, or expand any wildcard characters in `SELECT` statements in your database project.

> [!WARNING]  
> The following procedure uses entities created in previous procedures in the [SQL database projects](../tools/sql-database-projects/sql-database-projects.md) sections.

## Rename a type

1. Right-click the `Products` table (`Products.sql`) in **Solution Explorer**, and select **View Code** to open the script in Transact-SQL Editor.

1. Right-click `[Products]` in the script, select **Refactor**, and **Rename**.

1. In the **New Name** field, change it to `Product`. Leave the **Preview Changes** option checked and select **OK**.

1. In the next screen, you can preview a list of scripts that this rename operation is going to affect. Specifically, all the places that refer to `Products` are highlighted. This process is similar to the Find All References task in the previous procedure. Select anything on the top pane and view the actual change in the scripts (highlighted in green) in the bottom pane.

1. Select **Apply**.

1. For script files that are already opened in Table Designer or Transact-SQL Editor, the Transact-SQL Editor highlights the locations where changes took place with a green bar on the left.

1. Notice the addition of `TradeDev.refactorlog` in **Solution Explorer**. Double-click to open it. It contains an XML representation of all the changes in this session.

1. Press **F5** to build and deploy the project to the local database.

1. Right-click the `TradeDev` database under **Local** in **SQL Server Object Explorer**, and select **Refresh**.

1. Expand **Tables**, and you see that the `Products` table was renamed.

1. Right-click `Product` and select **View Data**. Existing data is kept intact regardless of the rename operation.

> [!WARNING]  
> If a refactor log is deleted, the complete history of the refactoring is also deleted. When the project is published to a database where previous refactor operations aren't applied, any refactor operations completed before the refactor file was deleted, are published as drop and create operations. As a result, data loss can occur.

## Expand wildcards

1. Expand the **Functions** node in **Solution Explorer**, and double-click `GetProductsBySupplier.sql`.

1. Place the cursor on the asterisk in this line and right-click. Select **Refactor**, and **Expand Wildcards**.

   ```sql
   SELECT * FROM Product AS p;
   ```

1. In the **Preview Changes** dialog box, select `SELECT * from Product p` on the top pane to highlight it.

1. In the **Preview Changes** pane below, notice that `*` expands to the following columns in the script.

   ```sql
   [Id], [Name], [ShelfLife], [SupplierId], [CustomerId]
   ```

1. Select the **Apply** button. Notice the line that contains changes brought forth by the expand operation is again highlighted with a green bar on the left.

## Fully qualify database object names

1. Make sure `GetProductsBySupplier.sql` is still open in Transact-SQL Editor.

1. Place the cursor on `Product` in this line and right-click. Select **Refactor**, and **Fully-Qualify Names**.

   ```sql
   SELECT [Id],
          [Name],
          [ShelfLife],
          [SupplierId],
          [CustomerId]
   FROM Product AS p;
   ```

1. Select the **Apply** button in the **Preview Changes** dialog box. All the object references are updated to include the name of the object's schema and, if the object has a parent, the name of the parent.

   ```sql
   SELECT [p].[Id],
          [p].[Name],
          [p].[ShelfLife],
          [p].[SupplierId],
          [p].[CustomerId]
   FROM [dbo].[Product] AS p;
   ```

## Move schema

1. Right-click the object that you want move. Select **Refactor**, and **Move Schema**.

1. In the **New Schema** list, select the name of the schema into which you want to move the object. Select **OK**.

   If you select the **Preview changes** check box, **the Preview Changes** dialog box appears. Otherwise, the object name is updated, and the object is moved to the new schema.
