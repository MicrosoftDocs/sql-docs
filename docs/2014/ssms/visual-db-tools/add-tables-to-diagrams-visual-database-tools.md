---
title: "Add Tables to Diagrams (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "inserting tables"
  - "adding tables"
ms.assetid: 5440fdf7-ac04-4325-9f32-181f4cd402e5
author: stevestein
ms.author: sstein
manager: craigg
---
# Add Tables to Diagrams (Visual Database Tools)
  You can add a table to your database diagram to edit its structure or relate it to other tables in your diagram. You can either add existing database tables to a diagram or insert a new table that has not yet been defined in the database.  
  
### To insert a new table into a diagram  
  
1.  Make sure you are connected to the database in which you want to create the table.  
  
     To create a table in your current diagram, click the **New Table** button on the toolbar.  
  
     -or-  
  
     Right-click in the diagram and select **New Table**.  
  
2.  Modify or accept the system-assigned table name, in the **Choose Name** dialog box, and then choose **OK**.  
  
     A new table appears in the diagram in Standard view.  
  
3.  In the first cell of the new table, type a column name. Then press the TAB key to move to the next cell.  
  
4.  Under **Data Type**, select a data type for the column. Each column must have a name and data type.  
  
     You can set the column's other properties in Table Designer.  
  
5.  Repeat steps 3 and 4 for each column you want to add to the table.  
  
> [!NOTE]  
>  When you save your database diagram, the new table will be added to your database.  
  
### To add an existing table to a diagram  
  
1.  Make sure you are connected to the database whose tables you want to edit.  
  
2.  Select a table in the **Tables** folder.  
  
3.  Drag the table into your database diagram.  
  
4.  Release the mouse button.  
  
> [!NOTE]  
>  If relationships exist between the selected table and other tables in your diagram, relationship lines are automatically drawn.  
  
### To add related tables to a diagram  
  
1.  Select one or more tables with foreign key constraints in the database diagram.  
  
2.  Right-click any of the selected tables and choose **Add Related Tables**.  
  
> [!NOTE]  
>  Both those tables referenced by a foreign key constraint from the selected table(s) and those referencing the selected table(s) with a foreign key constraint are added to the diagram.  
  
## See Also  
 [Work with Database Diagrams &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Work with Tables in Database Diagram &#40;Visual Database Tools&#41;](work-with-tables-in-database-diagram-visual-database-tools.md)  
  
  
