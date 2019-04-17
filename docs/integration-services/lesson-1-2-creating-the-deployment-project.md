---
title: "Step 2: Creating the Deployment Project | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 59990fe2-7036-4e9c-8efc-6ece9e66eda7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-2 - Creating the Deployment Project
In [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], the deployable unit is an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. Before you can deploy packages, you must create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project and add all the packages and any ancillary files that you want to deploy with the packages to that project.  
  
### To create the Integration Services project  
  
1.  Click **Start**, point to **All Programs**, point to **Microsoft SQL Server**, and then click **SQL ServerSQL Server Data Tools**.  
  
2.  On the **File** menu, point to **New**, and then click **Project** to create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
3.  In the **New Project** dialog box, select **Integration Services Project** in the **Templates** pane.  
  
4.  In the **Name** box, change the default name to **Deployment Tutorial**. Optionally, clear the **Create directory for solution** check box.  
  
5.  Accept the default location, or click **Browse** to locate the folder you want to use.  
  
6.  In the **Project Location** dialog box, click the folder, and then click **Open**.  
  
7.  Click **OK**.  
  
8.  By default, an empty package, named Package.dtsx, is created and added to your project. However, you will not use this package; instead, you will add existing packages to the project. Because all the packages in a project will be included in the deployment, you should delete Package.dtsx. To delete it, right-click it, and then click **Delete**.  
  
## Next Task in Lesson  
[Step 3: Adding Packages and Other Files](../integration-services/lesson-1-3-adding-packages-and-other-files.md)  
  
## See Also  
[Integration Services &#40;SSIS&#41; Projects](~/integration-services/integration-services-ssis-projects-and-solutions.md)  
  
  
  

