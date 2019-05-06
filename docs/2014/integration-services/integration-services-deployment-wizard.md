---
title: "Integration Services Deployment Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.deploymentwizard.f1"
ms.assetid: f3d93e13-2d85-47ff-a913-cda4046491c4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services Deployment Wizard
  The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Deployment Wizard deploys projects to the SSISDB catalog on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance using the project deployment model.  
  
 To start the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Deployment Wizard from an open project in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)], select **Deploy** from the **Project** menu. To start the wizard in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], expand the **Integration Services Catalogs** > **SSISDB** node in Object Explorer, right-click the **Projects** folder, and then click **Deploy Project**.  
  
 The wizard proceeds through the following four steps. Click **Next** to move to the next step, or **Previous** to return to the previous step.  
  
1.  **Select Source** - Select the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that you want to deploy.  
  
2.  **Select Destination** - Select the project destination.  
  
3.  **Review** - Displays your selections.  
  
4.  **Deploy/Results** - Deploys the project and displays the results.  
  
## Select Source  
 To deploy a project deployment file that you created, select **Project deployment file** and enter the path to the .ispac file or click **Browse** to find it in the [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] project folder. To deploy a project that resides in the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog, select **Integration Services catalog**, and then enter the server name and the path to the project in the catalog.  
  
 If you start the wizard in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)], then by default the wizard selects the open project as the source and skips this step. To return to this step and select a different source, click **Previous** or click **Select Source** in the left pane.  
  
## Select Destination  
 To select the destination folder for the project in the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] catalog, enter the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance or click **Browse** to select from a list of servers. Enter the project path in SSISDB or click **Browse** to select it.  
  
 If you start the wizard in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], then by default the wizard selects the connected server instance and enters the path to the selected project. You can change these values to deploy the project to a different location.  
  
## Review  
 The wizard allows you to review the settings you have selected before deploying the project. You can change your selections by clicking **Previous**, or by clicking any of the steps in the left pane.  
  
## Deploy/Results  
 When you click **Deploy** from the **Review** page, the project is deployed and the **Results** page displays the success or failure of each action. If the action fails, click the **Failed** in the **Result** column to display an explanation of the error. Click **Save Report...** to save the results to an XML file.  
  
 Click **Close** to exit the wizard.  
  
## See Also  
 [Deploy Projects to Integration Services Server](../../2014/integration-services/deploy-projects-to-integration-services-server.md)   
 [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md)  
  
  
