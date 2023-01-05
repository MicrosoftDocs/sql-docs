---
description: "Visual Database Tool Designers"
title: Visual Database Tool Designers
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [SQL Server]"
  - "View Designer"
  - "Visual Database Tools [SQL Server]"
  - "Database Diagram Designer"
  - "Query Designer [SQL Server]"
  - "design tools [Visual Database Tools]"
  - "Table Designer"
  - "Visual Database Tools [SQL Server], designers"
  - "Properties window [Visual Database Tools]"
author: markingmyname
ms.author: maghan
ms.reviewer: 
---
# Visual Database Tool Designers

[!INCLUDE[SQL Server Azure SQL Database PDW](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

The Visual Database Tools are a combination of design tools you can use to work with a data source. You can use them to create queries, design or modify a database structure, or update data. The tools are Database Diagram Designer, Table Designer, and Query and View Designer.  
  
## Properties Window  
The properties window is not specific to Visual Database Tools, but it is where many modifications can be made. It shows the properties for the item currently selected (like a table) and allows you to edit those properties (everything from the properties name to the collation of a column). Some properties can be seen in the properties window but must be modified in a different tool.  
  
## Database Diagram Designer  
Database Diagram Designer provides a window where you can visually create, edit, and show the tables and relationships in a database.  
  
To display Database Diagram Designer, open an existing diagram or right-click the Database node in Object Explorer and choose **New Database Diagram** from the drop-down menu.  
  
Once the designer is open, the **Database Diagram** menu appears in the main menu. This menu is the access point to the designer's special features.  
  
> [!NOTE]  
> This designer works with Microsoft SQL Server databases.  
>   
> This version of Visual Database Tools does not support Microsoft SQL Server version 7 and earlier.  
  
## Table Designer  
Table Designer is a visual tool allowing you to design and visualize a single table in a Microsoft SQL Server database to which you are connected.  
  
Table Designer has two panes. The upper area shows a grid; each row of the grid describes one database column. For each database column, the grid displays its fundamental attributes: column name, data type, and nulls-allowed setting.  
  
The lower area of Table Designer, the Column Properties tab, shows additional attributes for whichever column is highlighted in the upper area.  
  
From Table Designer, you can also right-click in the grid section to access dialog boxes through which you can create and modify relationships, constraints, indexes, and keys for the table.  
  
To display Table Designer, open an existing table, or right-click the **Tables** node in Object Explorer, and choose **Add New Table** from the drop-down menu.  
  
Once the designer is open, the Table Designer menu appears in the main menu. This menu is the access point to the designer's special features.  
  
> [!NOTE]  
> This designer works with Microsoft SQL Server databases.  
>   
> This version of Visual Database Tools does not support Microsoft SQL Server version 7 and earlier.  
  
## Query and View Designer  
Query and View designer is actually two tools that work in very similar ways. Some of the major differences are:  
  
-   Views are saved with the database while a query is saved with a Visual Studio database project.  
  
-   Query Designer works with nearly any data source, where View Designer works only with SQL Server.  
  
-   Query Designer allows you to design SELECT, INSERT, UPDATE and DELETE DML statements, while views can only contain SELECT statements.  
  
### View Designer  
View Designer allows you to design and visualize an existing view or create a new one in a Microsoft SQL Server database to which you are connected.  
  
View Designer has four panes: the Diagram pane, the Criteria pane, the SQL pane, and the Results pane. For more detailed information on each of these panes, see [Query and View Designer Tools &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md).  
  
To display View Designer, open an existing view or right-click the **View** node in Object Explorer and choose **Add New View** from the drop-down menu.  
  
Once the designer is open, the **Query Designer** menu appears in the main menu. This menu is the access point to the designer's special features.  
  
> [!NOTE]  
> This designer works with Microsoft SQL Server databases.  
>   
> This version of Visual Database Tools does not support Microsoft SQL Server version 7 and earlier.  
  
## See Also  
[Design Database Diagrams &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-database-diagrams-visual-database-tools.md)  
[Design Tables &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-tables-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
