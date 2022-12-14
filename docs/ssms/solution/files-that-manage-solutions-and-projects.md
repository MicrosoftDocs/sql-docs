---
description: "Files That Manage Solutions and Projects"
title: "Files That Manage Solutions and Projects"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], files"
  - ".ssmssln files"
  - ".ssmsmobileproj files"
  - ".ssmsasproj files"
  - ".ssmssqlproj files"
  - ".sqlsuo files"
  - "files [SQL Server Management Studio], projects"
ms.assetid: e19d2859-0b97-4727-ac27-c4c226d86b2f
author: "markingmyname"
ms.author: "maghan"
---
# Files That Manage Solutions and Projects
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
 This topic describes the file types that are specific to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. By default, all solutions and their projects are created in \My Documents\SQL Server Management Studio Projects.  


## Management Studio Solution Files  
SQL Server Management Studio uses different file types than [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio. This means you cannot open a SQL Server Management Studio solution in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or in Visual Studio. SQL Server Management Studio solution files allow Solution Explorer to display a graphical interface for managing your files.  
   
|Extension|File type|Description|Created by|  
|-------------|-------------|---------------|--------------|  
|.ssmssln|SQL Server Management Studio Solution Object|Provides the environment with references to the location on disk of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] projects, project items, and solution|SQL Server Management Studio|  
  
## Management Studio Project Files  
In the same way that solutions contain solution files that manage objects in a solution, projects contain project files. The type of project file that SQL Server Management Studio creates for a project depends on the template used to create the project. The following table describes the type of file created for each project.  
   
|Extension|Project template|  
|-------------|--------------------|  
|.ssmssqlproj|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Scripts Project|  
|.ssmsasproj|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion_md.md)] Scripts Project|  
   
## Location of Solution-Level Files  
By default, solution-level files are created in the physical directory of the first project that is created with the solution. You can specify a directory for the solution by creating a solution, or you can specify the directory when you create a new project.  
 
If you have a directory structure similar to the logical structure shown in Solution Explorer, project and solution files are easier to locate and share with other developers on a team.  
   
## See Also  
[Manage Files with Encoding](../../ssms/solution/manage-files-with-encoding.md)  
[Miscellaneous Files](../../ssms/solution/miscellaneous-files.md)  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Solutions &#40;SQL Server Management Studio&#41;](../../ssms/solution/solutions-sql-server-management-studio.md)  
[Projects &#40;SQL Server Management Studio&#41;](../../ssms/solution/projects-sql-server-management-studio.md)  
[Solution Explorer Source Control](./solution-explorer.md)  
