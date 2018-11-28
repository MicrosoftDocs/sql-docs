---
title: "Deploying an Analysis Services Project | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 2-5 - Deploying an Analysis Services Project
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

To view the cube and dimension data for the objects in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project, you must deploy the project to a specified instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and then process the cube and its dimensions. *Deploying* an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project creates the defined objects in an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. *Processing* the objects in an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] copies the data from the underlying data sources into the cube objects. For more information, see [Deploy Analysis Services Projects &#40;SSDT&#41;](../analysis-services/multidimensional-models/deploy-analysis-services-projects-ssdt.md) and [Configure Analysis Services Project Properties &#40;SSDT&#41;](../analysis-services/multidimensional-models/configure-analysis-services-project-properties-ssdt.md).  
  
At this point in the development process, you generally deploy the cube to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] on a development server. Once you have finished developing your business intelligence project, you will generally use the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Deployment Wizard to deploy your project from the development server to a production server. For more information, see [Multidimensional Model Solution Deployment](../analysis-services/multidimensional-models/multidimensional-model-solution-deployment.md) and [Deploy Model Solutions Using the Deployment Wizard](../analysis-services/multidimensional-models/deploy-model-solutions-using-the-deployment-wizard.md).  
  
In the following task, you review the deployment properties of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project and then deploy the project to your local instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
### To deploy the Analysis Services project  
  
1.  In Solution Explorer, right-click the **Analysis Services Tutorial** project, and then click **Properties**.  
  
    The **Analysis Services Tutorial Property Pages** dialog box appears and displays the properties of the Active(Development) configuration. You can define multiple configurations, each with different properties. For example, a developer might want to configure the same project to deploy to different development computers and with different deployment properties, such as database names or processing properties. Notice the value for the **Output Path** property. This property specifies the location in which the XMLA deployment scripts for the project are saved when a project is built. These are the scripts that are used to deploy the objects in the project to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
2.  In the **Configuration Properties** node in the left pane, click **Deployment**.  
  
    Review the deployment properties for the project. By default, the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Project template configures an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project to incrementally deploy all projects to the default instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] on the local computer, to create an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database with the same name as the project, and to process the objects after deployment by using the default processing option. For more information, see [Configure Analysis Services Project Properties &#40;SSDT&#41;](../analysis-services/multidimensional-models/configure-analysis-services-project-properties-ssdt.md).  
  
    > [!NOTE]  
    > If you want to deploy the project to a named instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] on the local computer, or to an instance on a remote server, change the **Server** property to the appropriate instance name, such as \<*ServerName**>\\<**InstanceName**>*.  
  
3.  Click **OK**.  
  
4.  In Solution Explorer, right-click the **Analysis Services Tutorial** project, and then click **Deploy**. You might need to wait.  
  
    > [!NOTE]  
    > If you get errors during deployment, use SQL Server Management Studio to check the database permissions. The account you specified for the data source connection must have a login on the SQL Server instance. Double-click the login to view User Mapping properties. The account must have db_datareader permissions on the **AdventureWorksDW2012** database.  
  
    [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] builds and then deploys the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project to the specified instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] by using a deployment script. The progress of the deployment is displayed in two windows: the **Output** window and the **Deployment Progress - Analysis Services Tutorial** window.  
  
    Open the Output window, if necessary, by clicking **Output** on the **View** menu. The **Output** window displays the overall progress of the deployment. The **Deployment Progress - Analysis Services Tutorial** window displays the detail about each step taken during deployment. For more information, see [Build Analysis Services Projects &#40;SSDT&#41;](../analysis-services/multidimensional-models/build-analysis-services-projects-ssdt.md) and [Deploy Analysis Services Projects &#40;SSDT&#41;](../analysis-services/multidimensional-models/deploy-analysis-services-projects-ssdt.md).  
  
5.  Review the contents of the **Output** window and the **Deployment Progress - Analysis Services Tutorial** window to verify that the cube was built, deployed, and processed without errors.  
  
6.  To hide the **Deployment Progress - Analysis Services Tutorial** window, click the **Auto Hide** icon (it looks like a pushpin) on the toolbar of the window.  
  
7.  To hide the **Output** window, click the **Auto Hide** icon on the toolbar of the window.  
  
You have successfully deployed the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube to your local instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], and then processed the deployed cube.  
  
## Next Task in Lesson  
[Browsing the Cube](../analysis-services/lesson-2-6-browsing-the-cube.md)  
  
## See Also  
[Deploy Analysis Services Projects &#40;SSDT&#41;](../analysis-services/multidimensional-models/deploy-analysis-services-projects-ssdt.md)  
[Configure Analysis Services Project Properties &#40;SSDT&#41;](../analysis-services/multidimensional-models/configure-analysis-services-project-properties-ssdt.md)  
  
  
  
