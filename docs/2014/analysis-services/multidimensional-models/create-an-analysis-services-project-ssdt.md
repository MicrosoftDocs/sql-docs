---
title: "Create an Analysis Services Project (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "templates [Analysis Services]"
  - "templates [Analysis Services], projects"
  - "projects [Analysis Services], creating"
  - "projects [Analysis Services], Business Intelligence Development Studio"
  - "Business Intelligence Development Studio, defining projects [Analysis Services]"
  - "items [Analysis Services]"
ms.assetid: d00913b0-cd6d-4de0-a1e7-4ce86fcc078d
author: minewiskan
ms.author: owend
manager: craigg
---
# Create an Analysis Services Project (SSDT)
  You can define an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] either by using the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Project template or by using the Import [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Database Wizard to read the contents of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. If no solution is currently loaded in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], creating a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project automatically creates a new solution. Otherwise, the new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project will be added to the existing solution. Best practices for solution development call for creating separate projects for different types of application data, using a single solution if the projects are related. For example, you might have a single solution that contains separate projects for Integration Services packages, Analysis Services databases, and Reporting Services reports that are all used by the same business application.  
  
 An Analysis Services project contains objects used in a single Analysis Services database. The deployment properties of the project specify the server and database name by which the project metadata will be deployed as instantiated objects.  
  
 This topic contains the following sections:  
  
 [Create a New Project Using the Analysis Services Project Template](#bkmk_NewUsingTemplate)  
  
 [Create a New Project Using an Existing Analysis Services Database](#bkmk_NewUsingWizard)  
  
 [Add an Analysis Services Project to an Existing Solution](#bkmk_AddtoExistingSolution)  
  
 [Build and Deploy the Solution](#bkmk_buildDeploy)  
  
 [Analysis Services Project Folders](#bkmk_ProjectFolders)  
  
 [Analysis Services File Types](#bkmk_FileTypes)  
  
 [Analysis Services Item Templates](#bkmk_ItemTemplates)  
  
##  <a name="bkmk_NewUsingTemplate"></a> Create a New Project Using the Analysis Services Project Template  
 Use these instructions to create an empty project in which you define [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects that you can then deploy as a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **File**, point to **New**, and click **Project**. In the **New Project** dialog box, in the **Project types** pane, select **Business Intelligence Projects**.  
  
2.  In the **New Project** dialog box, in the **Visual Studio installed templates** category, select **Analysis Services Project**.  
  
3.  In the **Name** text box, type the name of the project. The name you enter will be used as the default database name.  
  
4.  In the **Location** drop-down list, type or select the folder in which to store the files for the project, or click **Browse** to select a folder.  
  
5.  To add the new project to the existing solution, in the **Solution** drop-down list, select **Add to Solution**.  
  
     -or-  
  
     To create a new solution, in the **Solution** drop-down list, select **Create new Solution**. To create a new folder for the new solution, select **Create directory for solution**. In **Solution Name**, type the name of the new solution.  
  
6.  Click **OK**.  
  
##  <a name="bkmk_NewUsingWizard"></a> Create a New Project Using an Existing Analysis Services Database  
 Use the Import [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Database Wizard to create a project based on the objects in the existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. When you define an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project based on an existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, the metadata for that database will open in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. These objects can then be modified within the project without affecting the original objects, and then be deployed to the same [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database if the deployment properties specify that database, or to a newly created [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database for comparison testing. Until the changes are deployed, no changes made will affect the existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.  
  
 You can also use the Import [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Database template to create a project from a production database to which changes have been made directly since the original [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project was deployed.  
  
 Before you process or deploy the project, you might need to change the data provider that is specified in the data sources. If the SQL Server software you are using is newer than the software used to create the database, the data provider specified in your project might not be installed on your computer. During processing, the service account will be used to retrieve the data in your Analysis Services database. If the database is on a remote server, check whether the local service has process and read permissions on that server.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **File**, point to **New**, and click **Project**. In the **New Project** dialog box, in the **Project types** pane, select **Business Intelligence Projects**.  
  
2.  In the **New Project** dialog box, in the **Visual Studio installed templates** category, select **Import Analysis Services Database**.  
  
3.  Enter property information for the project and solution, including name and location for the files. Click **OK**.  
  
4.  On the Welcome to the **Import Analysis Services Database Wizard** page, click **Next**.  
  
5.  On the **Source Database** page, specify the server and the database from which the wizard will extract the contents and create the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, and then click **Next**.  
  
     Supported databases include those created in the following versions of Analysis Services: [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
     You can either type the database name or query the server to view the existing databases on the server. If the database is on a remote server or production server, you might need to request permission to read the database. Firewall configuration settings can further restrict access to a database. If you get an error while attempting to connect to the database, check permissions and firewall settings first.  
  
6.  When the wizard finishes extracting the contents of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, click **Finish** on the **Completing the Wizard** page.  
  
7.  Open the Solution Explorer window to view the contents of the project.  
  
##  <a name="bkmk_AddtoExistingSolution"></a> Add an Analysis Services Project to an Existing Solution  
 If you already have a solution that contains all the source files of a business application, you can add a new Analysis Services project to that solution.  
  
 Adding an existing project to a solution associates, but does not copy, the project with the solution. If the Analysis Services project was created in a different solution, the project files remain with the original solution for which it was created. This means that any changes you make to the project through either solution will operate on the same set of source files. If this behavior is not what you intend, you should copy or move the project files to the new solution folder first, and then add the project to the solution.  
  
1.  Open the solution in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. In Solution Explorer, right-click the solution, point to **Add**, and then click **Existing Project** to select the project you want to add.  
  
2.  Select a .dwproj file to add to the solution.  
  
##  <a name="bkmk_buildDeploy"></a> Build and Deploy the Solution  
 By default, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] deploys a project to the default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on the local computer. You can change this deployment destination by using the **Property Pages** dialog box for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project to change the **Server** configuration property.  
  
> [!NOTE]  
>  By default, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] processes only objects changed by the deployment script and dependent objects when deploying a solution. You can change this functionality by using the **Property Pages** dialog box for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project to change the Processing Option configuration property.  
  
 Build and deploy the solution to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] for testing. Building a solution validates the object definitions and dependencies in the project and generates a deployment script. Deploying a solution uses the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] deployment engine to send the deployment script to a specified instance.  
  
 After you deploy the project, review and test the deployed database. You can then modify object definitions, build, and deploy again until the project is complete.  
  
 After the project is complete, you can use the Deployment Wizard to deploy the deployment script, generated when you build the solution, to destination instances for final testing, staging, and deployment.  
  
##  <a name="bkmk_ProjectFolders"></a> Analysis Services Project Folders  
 An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project contains the following folders, which are used to organize items included in the project.  
  
|Folder|Description|  
|------------|-----------------|  
|Data Sources|Contains data sources for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create these objects with the Data Source Wizard and edit them in Data Source Designer.|  
|Data Source Views|Contains data source views for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create these objects with the Data Source View Wizard and edit them in Data Source View Designer.|  
|Cubes|Contains cubes for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create these objects with the Cube Wizard and edit them in Cube Designer.|  
|Dimensions|Contains dimensions for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create these objects with the Dimension Wizard or the Cube Wizard and edit them in Dimension Designer.|  
|Mining Structures|Contains mining structures for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create these objects with the Mining Model Wizard and edit them in Mining Model Designer.|  
|Roles|Contains database roles for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create and manage roles in Role Designer.|  
|Assemblies|Contains references to COM libraries and [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework assemblies for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You create references with the **Add Reference** dialog box.|  
|Miscellaneous|Contains any type of file except for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] file types. Use this folder to add any miscellaneous files, such as text files that contain notes on the project.|  
  
##  <a name="bkmk_FileTypes"></a> Analysis Services File Types  
 A [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] solution can contain several file types, depending on what projects you included in the solution and what items you included in each project for that solution. Typically, the files for each project in a [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] solution are stored in the solution folder, in a separate folder for each project.  
  
> [!NOTE]  
>  Copying a file for an object to a project folder does not add the object to the project. You must use the **Add** command from the project's context menu in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to add an existing object definition to a project.  
  
 The project folder for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project can contain the file types listed in the following table.  
  
|File type|Description|  
|---------------|-----------------|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project definition file (.dwproj)|Contains metadata about the items, configurations, and assembly references defined and included in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project user settings (.dwproj.user)|Contains configuration information for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, for a specific user.|  
|Data source file (.ds)|Contains [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) elements that define metadata for a data source.|  
|Data source view file (.dsv)|Contains ASSL elements that define metadata for a data source view.|  
|Cube file (.cube)|Contains ASSL elements that define metadata for a cube, including measure groups, measures, and cube dimensions.|  
|Partition file (.partitions)|Contains ASSL elements that define metadata for the partitions of a specified cube.|  
|Dimension file (.dim)|Contains ASSL elements that define metadata for a database dimension.|  
|Mining structure file (.dmm)|Contains ASSL elements that define metadata for a mining structure and associated mining models.|  
|Database file (.database)|Contains ASSL elements that define metadata for a database, including account types, translations, and database permissions.|  
|Database role file (.role)|Contains ASSL elements that define metadata for a database role, including role members.|  
  
##  <a name="bkmk_ItemTemplates"></a> Analysis Services Item Templates  
 If you use the **Add New Item** dialog box to add new items to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, you have the option of using an item template, a predefined script or statement which demonstrates how to perform a specified action.  
  
 The item templates, listed in the following table, are available in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Project Items category in the **Add New Item** dialog box.  
  
|Category|Item template|Description|  
|--------------|-------------------|-----------------|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Project Items|Cube|Starts the Cube Wizard to add a new cube to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
||Data Source|Starts the Data Source Wizard to add a new data source to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
||Data Source View|Starts the Data Source View Wizard to add a new data source view to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
||Database Role|Adds a new database role to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, and then displays Role Designer for the new database role.|  
||Dimension|Starts the Dimension Wizard to add a new database dimension to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
||Mining Structure|Starts the Data Mining Wizard to add a new mining structure and associated mining model to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
  
## See Also  
 [Configure Analysis Services Project Properties &#40;SSDT&#41;](configure-analysis-services-project-properties-ssdt.md)   
 [Build Analysis Services Projects &#40;SSDT&#41;](build-analysis-services-projects-ssdt.md)   
 [Deploy Analysis Services Projects &#40;SSDT&#41;](deploy-analysis-services-projects-ssdt.md)  
  
  
