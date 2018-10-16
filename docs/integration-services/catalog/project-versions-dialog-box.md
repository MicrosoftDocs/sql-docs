---
title: "Project Versions Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.ssms.isprojectprop.versions.f1"
ms.assetid: a48a387c-2e70-45bc-be2e-26e57a9bb2c4
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Project Versions Dialog Box
  Use the **Project Versions** dialog box to view versions of a project and to restore a previous version.  
  
 You can also view previous versions in the [catalog.object_versions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-object-versions-ssisdb-database.md) view, and use the [catalog.restore_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-restore-project-ssisdb-database.md) stored procedure to restore previous versions.  
  
 **What do you want to do?**  
  
-   [Open the Project Versions dialog box](#open_dialog)  
  
-   [Restore a Project Version](#restore)  
  
##  <a name="open_dialog"></a> Open the Project Versions dialog box  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     That is, connect to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that hosts the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] database.  
  
2.  In Object Explorer, expand the tree to display the **Integration Services Catalogs** node.  
  
3.  Expand the **Integration Services Catalogs** node to display the **SSISDB** node.  
  
4.  The **SSISDB** node contains one or more folders that each contain one or more projects. Expand the folder that contains the project you are interested in.  
  
5.  Right-click the project, and then click **Versions**.  
  
 In the **Project Versions** dialog box, the **Versions** table displays the list of versions of the project that have been deployed on the server, with the date and time the version was deployed, the date and time the version was restored (if it was restored), the version description, and a version identifier. The currently active version is indicated with a check mark in the **Current** column of the table.  
  
##  <a name="restore"></a> Restore a Project Version  
 To restore a previous version of a project, select the version in the **Versions** table, and then click **Restore to Selected Version**. The project is restored to the selected version and that version is indicated with a check mark in the **Current** column of the **Versions** table.  
  
  
