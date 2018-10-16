---
title: "Exclude Files from Source Control | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "excluding files from source control"
  - "source controls [SQL Server Management Studio], file exclusions"
ms.assetid: 7dcb6514-db5c-49eb-8b5a-2c766ce39aa7
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Exclude Files from Source Control
  If the solution you are working on contains files that do not require source control services, you can use the **Exclude from Source Control** command to exclude the file from source control. When you do this, the file remains in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe database, but it is no longer checked in or out with the project.  
  
 You should use the **Exclude from Source Control** command only on generated files. A generated file is one that can be entirely re-created by [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] based on the contents of another file in the solution.  
  
### To exclude a file from source control  
  
1.  In Solution Explorer, select the file to exclude.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Exclude** *\<file name>* **from Source Control**.  
  
## See Also  
 [Source Control Basics](../../2014/database-engine/source-control-basics.md)   
 [Set Source Control Options](../../2014/database-engine/set-source-control-options.md)   
 [Change Source Control Connections](../../2014/database-engine/change-source-control-connections.md)  
  
  
