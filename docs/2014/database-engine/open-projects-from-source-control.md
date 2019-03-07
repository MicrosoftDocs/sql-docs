---
title: "Open Projects from Source Control | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], opening"
  - "opening projects"
ms.assetid: 942f93a3-e264-4ec4-ba72-a28e0509a527
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Open Projects from Source Control
  You can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to open projects directly from source control. When you do this, [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] retrieves the latest version of the project and copies it to your local disk. The [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment also creates a solution for the project automatically.  
  
 After you have opened a project from source control, you can check out and modify the project files.  
  
> [!NOTE]  
>  The following procedure should only be used once. Thereafter, you should open the project like any other project (by clicking **File**, **Open**, and then **Project**).  
  
### To open a project from source control  
  
1.  On the **File** menu, point to **Source Control**, and click **Open From Source Control**.  
  
2.  If prompted, log on to [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe.  
  
3.  In the **Create local project from SourceSafe** dialog box, open the folder that contains the project to open.  
  
4.  The **Create a new project in the folder** box changes to identify the local directory in which the project will be created. If you want to place the project in a different directory, type the path to the directory or use the **Browse** button to locate the directory on your local disk.  
  
5.  In the **Create a new project in the folder box**, verify that the correct folder is displayed, and then click **OK**.  
  
6.  In the **Open Solution** dialog box, select the project you want to open, and click **Open**.  
  
## See Also  
 [Open Solutions and Projects from Source Control](../../2014/database-engine/open-solutions-and-projects-from-source-control.md)   
 [Open Solutions from Source Control](../../2014/database-engine/open-solutions-from-source-control.md)  
  
  
