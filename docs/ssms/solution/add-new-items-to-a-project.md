---
description: "Add New Items to a Project"
title: "Add New Items to a Project"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], item additions"
  - "adding project items"
ms.assetid: 76af8692-324f-4f5e-b1a0-d72ca8a107e3
author: "markingmyname"
ms.author: "maghan"
---
# Add New Items to a Project
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Add new items to a project to extend application functionality. A new item can be a query or a connection. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] has two project types: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Script Project, and Analysis Services Script Project. The project type determines the items that you can add to the project. For example, you can add a [!INCLUDE[tsql](../../includes/tsql-md.md)] query (a file with a .sql extension) to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Script project, but you cannot add it to an Analysis Services Script Project.  
  
SQL Server Management Studio does not allow you to create folders within projects. To organize your work, create multiple projects within the solution.  
  
### To add a new query to an existing project  
  
1.  In Solution Explorer, select a target project.  
  
2.  On the **Project** menu, click **Add New Item**.  
  
3.  In the **Add New Item** dialog box, select a category in the left pane.  
  
4.  Select a query template in the right pane, and then click **Add**. The new query is added in the **Queries** folder of the project.  
  
5.  In the **Connect to Database Engine** dialog box, specify a connection for the new query, and then click **Connect**. You can click **Cancel** on the connection dialog if you do not want to associate a connection to the new query.  
  
6.  Rename the query in Solution Explorer if you wish.  
  
### To add a new connection to an existing project  
  
1.  In Solution Explorer, select a target project.  
  
2.  On the **Project** menu, click **Add New Item**.  
  
3.  Select **Connection** in the left pane.  
  
4.  Select **New Connection** in the right pane, and then click **Add**.  
  
5.  In the **Connect to Database Engine** dialog box, specify a connection for the new query, and then click **Connect**. The new connection gets added in the **Connections** folder of the project.  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Associating File Extensions to a Code Editor](../scripting/associate-file-extensions-to-a-code-editor.md)  
[Add Existing Items to a Project](../../ssms/solution/add-existing-items-to-a-project.md)  
[Remove or Delete an Item or Project](../../ssms/solution/remove-or-delete-an-item-or-project.md)  
