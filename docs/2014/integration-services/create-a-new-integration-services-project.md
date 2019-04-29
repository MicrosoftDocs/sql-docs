---
title: "Create a New Integration Services Project | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [Integration Services], creating"
  - "Integration Services projects, creating"
  - "SQL Server Integration Services projects, creating"
  - "SSIS projects, creating"
ms.assetid: 1e23f259-0401-4333-ab4f-89809aae63b1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Create a New Integration Services Project
  This procedure creates a new project and a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] solution.  
  
### To create a new Integration Services project  
  
1.  Open [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
2.  On the **File** menu, point to **New**, and then click **Project**.  
  
3.  In the **New Project** dialog box, in the **Templates** pane, select the **Integration Services Project** template.  
  
     The **Integration Services Project** template creates an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains a single, empty package.  
  
4.  (Optional) Edit the project name and the location.  
  
     The solution name is automatically updated to match the project name.  
  
5.  To create a separate folder for the solution file, select **Create directory for solution**. This is the default option.  
  
6.  If source control software is installed on the computer, select **Add to source control**  to associate the project with source control.  
  
7.  If the source control software is [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe, the **Visual SourceSafe Login** dialog box opens. In **Visual SourceSafe Login**, provide a user name, a password, and the name of the [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe database. Click **Browse** to locate the database.  
  
    > [!NOTE]  
    >  To view and change the selected source control plug-in and to configure the source control environment, click **Options** on the **Tools** menu, and then expand the **Source Control** node.  
  
8.  Click **OK** to add the solution to **Solution Explore**r and add the project to the solution.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Projects](integration-services-ssis-projects-and-solutions.md)   
 [Add or Remove an Integration Services Project in a Solution](../../2014/integration-services/add-or-remove-an-integration-services-project-in-a-solution.md)  
  
  
