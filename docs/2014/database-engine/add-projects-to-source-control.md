---
title: "Add Projects to Source Control | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "adding projects"
  - "projects [SQL Server Management Studio], adding"
ms.assetid: fd4616b2-a564-4a66-ac53-d1f5cba213c2
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Add Projects to Source Control
  [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] solutions can host multiple script projects. How you add a project to source control depends upon whether the solution to which the project belongs is under source control. If the solution is under source control, checking in the solution automatically adds the project to source control. For more information about checking in solutions, see [Check In Files](../../2014/database-engine/check-in-files.md).  
  
 If the solution that this project belongs to is not under source control, you can add that solution to source control, which then automatically adds the solution's projects. For more information about adding solutions to source control, see [Add Solutions to Source Control](../../2014/database-engine/add-solutions-to-source-control.md).  
  
 If you do not want to add the solution to source control, you can use the **Add Selection to Source Control** command to add the project manually.  
  
 Database objects are not directly protected by the source control provider, but you can create scripts of database objects and save the scripts under source control.  
  
### To add a project to source control  
  
1.  In Solution Explorer, select a project.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Add Selected Projects to Source Control**.  
  
    > [!NOTE]  
    >  If you use the **Add Selected Projects to Source Control** command to add a project that belongs to a source-controlled solution, you are prompted whether you want to add the project as a subfolder of the source-controlled solution or to add the project as a separate folder.  
  
3.  If prompted, log on to your source control provider.  
  
4.  The **Add to SourceSafe Project** dialog box appears. The name of your project appears in the **Project** box.  
  
5.  In the **Folders** list, open the folder where you want to place your project. Alternatively, you can click **Create** to create a folder with the name displayed in the **Project** box.  
  
## See Also  
 [Add Solutions and Projects to Source Control](../../2014/database-engine/add-solutions-and-projects-to-source-control.md)  
  
  
