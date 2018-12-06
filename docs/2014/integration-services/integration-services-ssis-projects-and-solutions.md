---
title: "Integration Services (SSIS) Projects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [Integration Services], creating"
  - "folders [Integration Services], projects"
  - "files [Integration Services], projects"
  - "folders [Integration Services]"
  - "projects [Integration Services], about projects"
ms.assetid: 28ea8120-0a79-4029-93f0-07d521b32bee
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services (SSIS) Projects
  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the development of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages.  
  
 When you deploy packages to a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database or the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store, you use the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service to manage the packages. The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service is available only in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. For more information about the service, see [Integration Services Service &#40;SSIS Service&#41;](service/integration-services-service-ssis-service.md). For more information about package deployment, see [Package Deployment &#40;SSIS&#41;](packages/legacy-package-deployment-ssis.md).  
  
 When you deploy [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] projects to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server, you use Transact-SQL views and stored procedures in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to manage the projects. For more information about project deployment, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md). For more information about the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server, see [Integration Services &#40;SSIS&#41; Server](catalog/integration-services-ssis-server-and-catalog.md).  
  
 For an overview of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], see [Integration Services &#40;SSIS&#41; and Studio Environments](integration-services-ssis-development-and-management-tools.md).  
  
## Understanding Integration Services Projects  
 A project is a container in which you develop [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages.  
  
 In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project stores and groups the files that are related to the package. For example, a project includes the files that are required to create a specific extract, transfer, and load (ETL) solution.  
  
 Before you create an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, you should become familiar with the basic contents of this kind of project. After you understand what a project contains, you can begin creating and working with an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
### Folders in Integration Services Projects  
 The following diagram shows the folders in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 ![Folders in an Integration Services project](media/solutionexplorer.gif "Folders in an Integration Services project")  
  
 The following table describes the folders that appear in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
|Folder|Description|  
|------------|-----------------|  
|[!INCLUDE[ssIS](../includes/ssis-md.md)] Packages|Contains packages. For more information, see [Integration Services &#40;SSIS&#41; Packages](../../2014/integration-services/integration-services-ssis-packages.md).|  
|Miscellaneous|Contains files other than package files.|  
  
### Files in Integration Services Projects  
 When you add a new or an existing [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project to a solution, [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] creates project files that have the extensions .dtproj and .dtproj.user and .database.  
  
-   The *.dtproj file contains information about project configurations and items such as packages.  
  
-   The *.dtproj.user file contains information about your preferences for working with the project.  
  
-   The *.database file contains information that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] requires to open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
## Understanding Solutions  
 A solution is a container that groups and manages the projects that you use when you develop end-to-end business solutions. A solution lets you handle multiple projects as one unit and to bring together one or more related projects that contribute to a business solution.  
  
 Solutions can include different types of projects. If you want to use [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to create an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package, you work in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project in a solution provided by [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 When you create a new solution, [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] adds a Solution folder to Solution Explorer, and creates files that have the extensions, .sln and .suo:  
  
-   The *.sln file contains information about the solution configuration and lists the projects in the solution.  
  
-   The *.suo file contains information about your preferences for working with the solution.  
  
 While [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] automatically creates a solution when you create a new project, you can also create a blank solution, and then add projects later.  
  
> [!NOTE]  
>  By default, when you create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the solution is not shown in the **Project Explorer** pane. To change this default behavior, on the **Tools** menus, click **Options**. In the **Options** dialog box, expand **Projects and Solutions**, and then click **General**. On the **General** page, select **Always show solution**.  
  
## Related Tasks  
 [Add or Remove an Integration Services Project in a Solution](../../2014/integration-services/add-or-remove-an-integration-services-project-in-a-solution.md)  
  
 [Create a New Integration Services Project](../../2014/integration-services/create-a-new-integration-services-project.md)  
  
 [Add an Item to an Integration Services Project](../../2014/integration-services/add-an-item-to-an-integration-services-project.md)  
  
 [Copy Project Items](../../2014/integration-services/copy-project-items.md)  
  
## Related Content  
 [Development of an Integration Services Project](../../2014/integration-services/development-of-an-integration-services-project.md)  
  
  
