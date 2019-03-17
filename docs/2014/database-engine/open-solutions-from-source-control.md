---
title: "Open Solutions from Source Control | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "opening solutions"
  - "solutions [SQL Server Management Studio], opening"
ms.assetid: a96a1f0d-0183-4587-a3b0-4598309cbdd2
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Open Solutions from Source Control
  You can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to open solutions directly from source control. When you do this, the Studio environment creates a copy of the latest version of the solution files at the location you specify.  
  
 If a local copy of the solution does not exist on your local disk, you must open the project from source control before you can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to check out the solution or retrieve solution files.  
  
> [!NOTE]  
>  If you already have a local copy of the solution you are opening from source control, you are prompted to overwrite the local copy.  
  
### To open a solution from source control  
  
1.  On the **File** menu, point to **Source Control**, and click **Open From Source Control**.  
  
2.  If prompted, log on to [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe.  
  
3.  In the **Create local project from SourceSafe** dialog box, select the folder that contains the solution you want to open, and click **OK**.  
  
4.  If a working folder for the solution already exists on your local disk drive, the **Create a new project in the folder** box changes to identify the local directory. If no working folder for the solution exists, you can type or browse to the local directory in which the solution should be opened.  
  
5.  In the **Open Solution** dialog box, select the solution file, and click **OK**.  
  
## See Also  
 [Open Projects from Source Control](../../2014/database-engine/open-projects-from-source-control.md)  
  
  
