---
title: "Retrieve Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "version control services [SQL Server], file retrieval"
  - "file retrieval [SQL Server]"
  - "retrieving files"
ms.assetid: 37b74426-e262-43b2-a81f-79b1fd1a36ec
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Retrieve Files
  After you have opened a source-controlled project, you can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to retrieve local copies of project files from the source control store to the local directory in which the project resides.  
  
 You can use integrated source control to retrieve files in the following ways:  
  
-   **Get Latest Version (Recursive)** command  
  
     Retrieves the latest checked in version of the selected files. If a solution or project is selected, this command retrieves the latest version of all solution and project files.  
  
-   **Get** command  
  
     Displays the **Get** dialog box, which you can use to retrieve the latest version of a selected file, or to retrieve a subset of the files in the selected solution or project.  
  
### To retrieve the latest version of all the files in a project  
  
1.  In Solution Explorer, select the project.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Get Latest Version (Recursive)**.  
  
 The most recent versions of the files in the project are retrieved to the project location on your local disk.  
  
#### To retrieve only certain files in a project  
  
1.  In Solution Explorer, select the item you want to retrieve.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Get**.  
  
3.  In the **Get** dialog box, click **OK**. Alternatively, if you selected a solution or project in Solution Explorer, clear the check boxes that appear next to the items you do not want to retrieve.  
  
## See Also  
 [Get Dialog Box &#40;Source Control&#41;](../../2014/database-engine/get-dialog-box-source-control.md)   
 [Set and Retrieve Version Information](../../2014/database-engine/set-and-retrieve-version-information.md)   
 [View Project History](../../2014/database-engine/view-project-history.md)   
 [View File Status](../../2014/database-engine/view-file-status.md)  
  
  
