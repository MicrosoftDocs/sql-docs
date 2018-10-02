---
title: "Change Source Control Connections | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [SQL Server Management Studio], source controls"
  - "source controls [SQL Server Management Studio], connections"
ms.assetid: 538e3beb-f99c-4095-bd65-6413e872d26e
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Change Source Control Connections
  The first time you add or open a solution from source control, your source control provider creates an association between the root folder of the local solution directory and its corresponding server folder.  
  
 The root folder (also called unified root) resides on the client. It is the folder beneath which all the files referenced by a solution or project can be found. To find the latest version of a solution, its history, and its status information, locate the server folder, which resides on the source control server. In [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe, server folders are called projects.  
  
 In many situations, you need to unbind or disconnect a solution from its server folder. For example, if the computer on which your source control provider resides is unavailable, you can connect to a backup computer, rebind your solution to a backed-up server folder, and resume working normally. Also, if a source control project is forked, you may have to bind your solution to the server folder where the new project version resides.  
  
 Use the user interface of the source control provider to change the server folder that a solution is bound to. You can open the source control user interface from within the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] environment.  
  
#### To open the source control user interface from the Studio environment  
  
1.  On the **File** menu, point to **Source Control**, and then click **Launch Microsoft Visual SourceSafe**.  
  
## See Also  
 [Source Control Basics](../../2014/database-engine/source-control-basics.md)   
 [Set Source Control Options](../../2014/database-engine/set-source-control-options.md)   
 [Exclude Files from Source Control](../../2014/database-engine/exclude-files-from-source-control.md)  
  
  
