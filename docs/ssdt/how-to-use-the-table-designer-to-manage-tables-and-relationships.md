---
title: Using Table Designer to Manage Tables and Relationships
description: Become familiar with Table Designer. See how to use this tool to create and edit database table structure and to view relationships among database objects.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.design.table.columnspecification.index.dialog"
  - "sql.data.tools.design.table.columnsgrid.view"
dev_langs:
  - "TSQL"
---

# How to: Use the Table Designer to manage tables and relationships

The Table Designer provides a visual experience alongside the Transact-SQL Editor for creating and editing table structure, including table-specific programming objects, for SQL Server databases. It launches when you create a new table for a connected database or a project, or when you double-click to edit a table in either SQL Server Object Explorer or Solution Explorer.

The designer consists of the Columns Grid, Script pane, and Context pane. The Columns Grid lists all the columns in the table. You can add, edit, and delete columns in this grid. The Context pane gives you a logical view of the table definition (Keys, Indices, Constraints, Triggers, etc.), and enables you to select an object to highlight its relationships to individual columns. You can also add new objects to the table in this pane, and edit the properties of a selected object in the Properties Grid. Script pane shows you the definition of the table structure, and highlights the script of the selected object in the Context pane or Columns Grid. You can edit the script side-by-side with the Columns Grid and Context pane in view. Any changes from any of the three panes propagate to the other two immediately.

> [!WARNING]  
> The following procedure uses entities created in previous procedures in the [SQL database projects](../tools/sql-database-projects/sql-database-projects.md) sections.

## Create a new table

1. Open the TradeDev project you have been working on in previous procedures.

1. In **Solution Explorer**, expand the **dbo** folder, right-click the **Tables** folder, select **Add**, and then select **Table**.

1. Name the new table `Shipper` and select **Add**.

1. The Table Designer opens. In the Columns Grid, add a new column to the table with name `ShipperName` and Data Type **int**.

1. You can also edit the properties of columns in the **Properties** window. Select the `ShipperName` column, and in the **Properties** window, change the **DataType** of this column to **nvarchar**, and **length** to `128`. As you shift your focus out of the field, the Script pane and the Columns Grid of the designer automatically update to reflect your change.

## Create a new foreign key constraint

1. Right-click the **Foreign Keys** node in the Context pane of the designer, and select **Add New Foreign Key**.

1. The node count automatically increments by 1. Press ENTER to accept the default name of the constraint.

1. Replace the default definition of the constraint in the Script pane with the following.

   ```sql
   CONSTRAINT [FK_Shipper_Products] FOREIGN KEY ([Id]) REFERENCES [dbo].[Products]([Id])
   ```

   Notice the experience of creating and editing database entities for an offline project is identical to performing the tasks with a connected database.

## Related content

- [How to: Create database objects using Table Designer](how-to-create-database-objects-using-table-designer.md)
