---
title: "View Project History | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing project history"
  - "version control services [SQL Server], project history"
  - "projects [SQL Server Management Studio], historical information"
  - "historical information [SQL Server], projects"
ms.assetid: be0ea2ac-4a35-429c-9c9e-4001ea9035a4
author: mashamsft
ms.author: mathoma
manager: craigg
---
# View Project History
  The history of a [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe (VSS) project includes a list of all the actions taken on each of the project files, including file creation, addition, deletion, and recovery.  
  
> [!NOTE]  
>  A Visual SourceSafe project is more commonly referred to as the source control server folder, the location where the server version of a source-controlled file is stored on the server.  
  
 You can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to examine the history of the Visual SourceSafe project to which the currently loaded solution belongs. Based on the information displayed as part of the history of a project, you can retrieve local copies of file versions, restore deleted versions, or share a file version between projects.  
  
### To view the history of a VSS project  
  
1.  In Solution Explorer, select the project.  
  
2.  On the **File** menu, point to **Source Control** and click **View History**.  
  
3.  In the **History of** \<Project> dialog box, perform any of the following actions:  
  
    -   View the source control system's copy of a selected file.  
  
    -   View detailed information about a selected file.  
  
    -   Retrieve a selected file to your local disk.  
  
    -   Check out a selected file.  
  
    -   Share a selected file between two source control projects.  
  
    -   Export the history report to a printer, a file, or the Clipboard.  
  
## See Also  
 [Set and Retrieve Version Information](../../2014/database-engine/set-and-retrieve-version-information.md)   
 [View File Status](../../2014/database-engine/view-file-status.md)   
 [Retrieve Files](../../2014/database-engine/retrieve-files.md)   
 [Compare Files](../../2014/database-engine/compare-files.md)  
  
  
