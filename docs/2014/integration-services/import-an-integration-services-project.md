---
title: "Import an Integration Services Project | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: 3301c328-b0f5-4517-915c-93713413e453
author: janinezhang
ms.author: janinez
manager: craigg
---
# Import an Integration Services Project
  Use the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]**Import Project Wizard** to create a project from an existing deployment file (.ispac) or from a project deployed to Integration services catalog. This feature is especially useful when you do not have the original copy of the project but you want to create one from an .ispac file or SSISDB catalog.  
  
### To Import a Project  
  
1.  In [!INCLUDE[vsprvs](../includes/vsprvs-md.md)], click **New** > **Project** on the **File** menu.  
  
2.  In the **Installed Templates** area of the **New Project** window, expand **Business Intelligence**, and click **Integration Services**.  
  
3.  Select **Integration Services Import Project Wizard** from the project types list.  
  
4.  Type a name for the new project to be created in the **Name** text box.  
  
5.  Type the path or location for the project in the **Location** text box, or click **Browse** to select one.  
  
6.  Type a name for the solution in the **Solution name** text box.  
  
7.  Click **OK** to launch the **Integration Services Import Project Wizard** dialog box.  
  
8.  Click **Next** to switch to the **Select Source** page.  
  
9. If you are importing from an **.ispac** file, type the path including file name in the **Path** text box. Click **Browse** to navigate to the folder where you want the solution to be stored and type file name in the **File name** text box, and click **Open**.  
  
     If you are importing from an **Integration Services Catalog**, type the database instance name in the **Server name** text box or click **Browse** and select the database instance that contains the catalog.  
  
     Click **Browse** next to **Path** text box, expand folder in the catalog, select the project you want to import, and click **OK**.  
  
     Click **Next** to switch to the **Review** page.  
  
10. Review the information and click **Import** to create a project based on the existing project you selected.  
  
11. Optional: click **Save Report** to save the results to a file  
  
12. Click **Close** to close the **Integration Services Import Project Wizard** dialog box.  
  
  
