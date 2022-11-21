---
title: Using Table Designer to Manage Tables and Relationships
description: Become familiar with Table Designer. See how to use this tool to create and edit database table structure and to view relationships among database objects.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.design.table.columnspecification.index.dialog"
  - "sql.data.tools.design.table.columnsgrid.view"
dev_langs: 
  - "TSQL"
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Use the Table Designer to Manage Tables and Relationships

The Table Designer provides a visual experience alongside the Transact\-SQL Editor for creating and editing table structure, including table-specific programming objects, for SQL Server databases.  It is launched when you create a new table for a connected database or a project, or when you double-click to edit a table in either SQL Server Object Explorer or Solution Explorer.  
  
The designer consists of the Columns Grid, Script Pane and Context Pane. The Columns Grid lists all the columns in the table. You can add, edit and delete columns in this grid.  The Context Pane gives you a logical view of the table definition (Keys, Indices, Constraints, Triggers, etc.), and enables you to select an object to highlight its relationships to individual columns. You can also add new objects to the table in this pane, and edit the properties of a selected object in the Properties Grid. Script Pane shows you the definition of the table structure, and highlights the script of the selected object in the Context Pane or Columns Grid. You can edit the script side-by-side with the Columns Grid and Context Pane in view. Any changes from any of the three panes will propagate to the other two immediately.  
  
> [!WARNING]  
> The following procedures use entities created in procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To create a new table  
  
1.  Open the TradeDev project you have been working on in previous procedures.  
  
2.  In **Solution Explorer**, expand the **dbo** folder, right-click the **Tables** folder and select **Add**, then **Table**.  
  
3.  Name the new table **Shipper** and click **Add**.  
  
4.  The Table Designer opens. In the Columns Grid, add a new column to the table with name **ShipperName** and Data Type **int**.  
  
5.  Notice that you can also edit the properties of columns in the **Properties** window. Click the **ShipperName** column, and in the **Properties** window, change the **DataType** of this column to **nvarchar**, and **length** to **128**. Notice that as you shift your focus out of the field, the Script Pane and the Columns Grid of the designer automatically update to reflect your change.  
  
### To create a new foreign key constraint  
  
1.  Right-click the **Foreign Keys** node in the Context Pane of the designer, and select **Add New Foreign Key**.  
  
2.  Notice that the node count automatically increments by 1. Press ENTER to accept the default name of the constraint.  
  
3.  Replace the default definition of the constraint in the Script Pane with the following.  
  
    ```  
    CONSTRAINT [FK_Shipper_Products] FOREIGN KEY ([Id]) REFERENCES [dbo].[Products]([Id])  
    ```  
  
    Notice the experience of creating and editing database entities for an offline project is identical to performing the tasks with a connected database.  
  
## See Also  
[How to: Create Database Objects Using Table Designer](../ssdt/how-to-create-database-objects-using-table-designer.md)  
  
