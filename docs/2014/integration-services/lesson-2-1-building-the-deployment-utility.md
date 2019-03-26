---
title: "Step 1: Building the Deployment Utility | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 1ff4dcff-89b3-4b99-a725-5f7963e98abf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Step 1: Building the Deployment Utility
  In this task, you will configure and build a deployment utility for the Deployment Tutorial project.  
  
 Before you can build the deployment utility, you must modify the properties of the Deployment Tutorial project. You will use the **Deployment Tutorial Property Pages** dialog box to configure these properties. In this dialog box, you must enable the ability to update configurations during deployment and specify that the build process generates a deployment utility. After you set the properties, you will build the project.  
  
 [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] provides a set of windows that you can use to debug packages. You can view error, warning, and information messages, track about the status of packages at run time, or view the progress and results of build processes. For this lesson, you will use the Output window to view the progress and results of building the deployment utility.  
  
### To set the deployment utility properties  
  
1.  If [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is not already open, click **Start**, point to **All Programs**, point to **Microsoft SQL Server**, and then click **Business Intelligence Development Studio**.  
  
2.  On the **File** menu, click **Open**, click **Project/Solution**, click the **Deployment Tutorial** folder and click **Open**, and then double-click **Deployment Tutorial.sln**.  
  
3.  In Solution Explorer, right-click Deployment Tutorial and click **Properties**.  
  
4.  In the **Deployment Tutorial Property Pages** dialog box, expand Configuration Properties, and click Deployment Utility.  
  
5.  In the right pane of the **Deployment Tutorial Property Pages** dialog box, verify that `AllowConfigurationChanges` is set to `true`, set `CreateDeploymentUtility` to `true`, and optionally update the default value of `DeploymentOutputPath`.  
  
6.  Click **OK**.  
  
### To build the deployment utility  
  
1.  In Solution Explorer, click **Deployment Tutorial**.  
  
2.  On the **View** menu, click **Output**. By default, the Output window is located in the bottom left corner of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
3.  On the **Build** menu, click **Build Deployment Tutorial**.  
  
4.  In the Output window, verify the following information:  
  
     Build started: SQL Integration Services project: Incremental ...  
  
     Creating deployment utility...  
  
     Deployment Utility created.  
  
     Build complete -- 0 errors, 0 warnings  
  
     ========== Build: 0 succeeded, 0 failed, 1 up-to-date, 0 skipped ==========  
  
5.  On the **File** menu, click **Exit**. If prompted to save changes to Deployment Tutorial items, click **Yes**.  
  
## Next Task in Lesson  
 [Step 2: Verifying the Deployment Bundle](../integration-services/lesson-2-2-verifying-the-deployment-bundle.md)  
  
![Integration Services icon (small)](media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Create a Deployment Utility](../../2014/integration-services/create-a-deployment-utility.md)  
  
  
