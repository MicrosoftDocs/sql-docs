---
title: "Copy Tables from One Database Diagrams to Another (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "copying tables"
  - "duplicating tables"
ms.assetid: 155a4f09-9321-4887-a7d4-aa2ce6b51277
author: stevestein
ms.author: sstein
manager: craigg
---
# Copy Tables from One Database Diagrams to Another (Visual Database Tools)
  You can copy a table from one database diagram to another in the same database.  
  
 Copying a table from one database diagram to another diagram adds a reference to the table in the second diagram. The table is not duplicated in your database. For example, if you copy the `authors` table from one database diagram to another, each diagram references the same `authors` table in the database.  
  
### To copy a table from another database diagram  
  
1.  Make sure you are connected to the database whose table you want to copy.  
  
2.  Open the source and target database diagrams and within the source diagram, select the table that you want to copy to the target diagram.  
  
3.  Click the **Copy** button on the toolbar. This action places the selected table definition on the Clipboard.  
  
4.  Switch to the target diagram. This diagram must be in the same database as the source diagram.  
  
5.  Click the **Paste** button on the toolbar. The Clipboard contents appear at the new location and remain highlighted until you click elsewhere. If relationships exist between the selected tables and other tables in the target diagram, relationship lines are automatically drawn.  
  
 When you edit the table in either diagram, your changes are reflected in both diagrams. Similarly, once you save the table in either diagram, the table is no longer considered "modified" in either diagram.  
  
## See Also  
 [Work with Database Diagrams &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Add Tables to Diagrams &#40;Visual Database Tools&#41;](add-tables-to-diagrams-visual-database-tools.md)  
  
  
