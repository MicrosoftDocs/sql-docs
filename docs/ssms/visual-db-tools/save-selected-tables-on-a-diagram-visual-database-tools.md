---
description: "Save Selected Tables on a Diagram (Visual Database Tools)"
title: Save Selected Tables on a Diagram
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "saving tables"
ms.assetid: 86943b49-48f3-432c-8021-928c13edfbcf
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Save Selected Tables on a Diagram (Visual Database Tools)

[!INCLUDE[SQL Server Azure SQL Database PDW](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

You can save a specific table or a set of tables if you do not want to save all the changes you made in a database diagram.  
  
### To save selected tables  
  
1.  In your database diagram, select the tables you want to save.  
  
2.  From the **File** menu, choose **Save Selection**.  
  
3.  The **Save** dialog box displays the list of tables that will be updated in the database when you save your selection.  
  
    Choose **Save Text File** if you want to save the list of tables in a text file in the project directory before continuing.  
  
4.  In the **Save** dialog box, confirm the list of tables and choose **Yes** to save these tables.  
  
    > [!NOTE]  
    > The list of tables may contain tables in addition to those selected. For example, if you change the data type of a column that participates in a relationship with another table, both tables will be included in this list.  
  
## See Also  
[Work with Database Diagrams](../../ssms/visual-db-tools/work-with-database-diagrams-visual-database-tools.md)  
  
