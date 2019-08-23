---
title: "Solution Explorer Source Control | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual SourceSafe"
  - "SQL Server Management Studio [SQL Server], source control services"
  - "source-controlled files [SQL Server]"
  - "source controls [SQL Server Management Studio], services"
  - "version control services [SQL Server]"
  - "file source control services [SQL Server]"
  - "VSS [Visual SourceSafe]"
ms.assetid: 6373adb8-3d81-4945-a9fc-1d0d5799d29a
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Solution Explorer Source Control
  [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] Solution Explorer can be integrated into a separate source control system. Once a solution or project is integrated into a source control system, you can control file access and versioning for the scripts and queries in your projects.  
  
## Solution and Project Source Control  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../includes/ssnotedepfutureavoid-md.md)]  
  
 Source control refers to a system in which a central piece of server software stores and tracks file versions, and also controls access to files. A typical source control system includes a source control provider and two or more source control clients. [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] can be integrated with a source control service. This means you can use the tool as a client for your source control provider. Without leaving the environment, you can manage your individual and team projects easily. The source control provider is not included with [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
#### To select a source control provider  
  
1.  Install the client portion of your source control provider.  
  
2.  In [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], on the **Tools** menu, click **Options**.  
  
3.  In the **Options** dialog box, expand **Source Control**, and then click the **Plug-in Selection** page.  
  
4.  In the **Current source control plug-in** box, select the source control plug-in.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Source Control Basics](../../2014/database-engine/source-control-basics.md)|Defines basic source control terminology, and explains how your project can benefit from using source control services.|  
|[Add Solutions and Projects to Source Control](../../2014/database-engine/add-solutions-and-projects-to-source-control.md)|Explains how to use the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment to add solutions and projects to source control.|  
|[Open Solutions and Projects from Source Control](../../2014/database-engine/open-solutions-and-projects-from-source-control.md)|Explains how to use the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment to open source-controlled solutions and projects.|  
|[Manage Checkouts](../../2014/database-engine/manage-checkouts.md)|Explains how to check out solutions and files from source control.|  
|[Manage Checkins](../../2014/database-engine/manage-checkins.md)|Explains how to check solutions and files into source control.|  
|[Set and Retrieve Version Information](../../2014/database-engine/set-and-retrieve-version-information.md)|Explains how to retrieve the history of a project or item, retrieve a local copy of an item, or compare two item versions.|  
|||  
  
## See Also  
 [Solution Explorer](../ssms/solution/solution-explorer.md)   
 [Solutions &#40;SQL Server Management Studio&#41;](../ssms/sql-server-management-studio-ssms.md)   
 [Projects &#40;SQL Server Management Studio&#41;](../ssms/solution/projects-sql-server-management-studio.md)   
 [Files That Manage Solutions and Projects](../ssms/solution/files-that-manage-solutions-and-projects.md)  
  
  
