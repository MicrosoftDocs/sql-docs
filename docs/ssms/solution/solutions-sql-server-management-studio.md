---
description: "Solutions (SQL Server Management Studio)"
title: "Solutions (SQL Server Management Studio)"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "solutions [SQL Server Management Studio]"
  - "SQL Server Management Studio [SQL Server], solutions"
  - "projects [SQL Server Management Studio], about projects"
  - "SQL Server Management Studio [SQL Server], projects"
  - "scripts [SQL Server], SQL Server Management Studio"
ms.assetid: d06a8a05-7201-4055-8cf3-21bcb4e82c25
author: "markingmyname"
ms.author: "maghan"
---
# Solutions (SQL Server Management Studio)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
A [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] solution is a collection of one or more related projects. Projects are containers that developers use to organize related files, such as sets of commonly used administration scripts.  
  
## Solution Overview  
You can use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] as a script development platform for [!INCLUDE[ssDE](../../includes/ssde_md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion_md.md)]. Use the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] code editors to develop scripts and queries for relational and multidimensional databases, and collect related scripts and queries together in projects.  
  
Projects can contain:  
  
-   Queries and scripts that implement production processes.  
  
-   Connection information and files used by the queries and scripts.  
  
One or more related projects can be combined in a solution. Solutions and projects can be managed by using the Solution Explorer pane in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
Solutions and projects can be integrated into a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual SourceSafe (VSS) database or other third-party source control providers, for development change tracking and life-cycle management.  
  
## Solution Tasks  
  
|Task Description|Topic|  
|--------------------|---------|  
|Describes how to create a new solution that can then be used to contain one or more projects.|[Create a New Solution](../../ssms/solution/create-a-new-solution.md)|  
|Describes how to open an existing solution in Solution Explorer.|[Open an Existing Solution](../../ssms/solution/open-an-existing-solution.md)|  
|Describes how to close a solution.|[Close a Solution](../../ssms/solution/close-a-solution.md)|  
|Describes how to delete a solution.|[Delete a Solution](../../ssms/solution/delete-a-solution.md)|  
|Describes how to copy items between projects.|[Copy Items in a Solution](../../ssms/solution/copy-items-in-a-solution.md)|  
|Describes how to delete items in a project, or an entire project.|[Remove or Delete an Item or Project](../../ssms/solution/remove-or-delete-an-item-or-project.md)|  
|Describes how to move items between projects in a solution.|[Move Items in a Solution](../../ssms/solution/move-items-in-a-solution.md)|  
|Describes how to rename a solution or the items in the solution.|[Rename Solutions and Project Items](../../ssms/solution/rename-solutions-and-project-items.md)|  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Projects &#40;SQL Server Management Studio&#41;](../../ssms/solution/projects-sql-server-management-studio.md)  
[Solution Explorer Source Control](./solution-explorer.md)  
