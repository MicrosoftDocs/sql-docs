---
title: "Integration Services Deployment Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "2016-08-25"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.ssis.deploymentwizard.f1"
ms.assetid: f3d93e13-2d85-47ff-a913-cda4046491c4
caps.latest.revision: 14
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Integration Services Deployment Wizard
  The **Integration Services Deployment Wizard** supports two deployment models:
   - Project deployment model
   - Package deployment model 
   
 The **Project Deployment model** allows you to deploy a SQL Server Integration Services (SSIS) project as a single unit to the SSIS Catalog.
 
 The **Package Deployment model** allows you to deploy packages that you have updated to the SSIS Catalog without having to deploy the whole project. 
 
 > **NOTE:** The Wizard default deployment is the Project Deployment model.  
  
## Launch the wizard
Launch the wizard by either:

 - Typing **"SQL Server Deployment Wizard"** in Windows Search 

**OR**

 - Search for the executable file **ISDeploymentWizard.exe** under the SQL Server installation folder; for example: “C:\Program Files (x86)\Microsoft SQL Server\130\DTS\Binn”. 
 
 > **NOTE:** If you see the **Introduction** page, click **Next** to switch to the **Select Source** page. 
 
 The settings on this page are different for each deployment model. Follow  steps in the [Project Deployment Model](../../integration-services/packages/integration-services-deployment-wizard.md#ProjectModel) section or [Package Deployment Model](../../integration-services/packages/integration-services-deployment-wizard.md#PackageModel) section based on the model you selected in this page.  
  
##  <a name="ProjectModel"></a> Project Deployment Model  
  
### Select Source  
 To deploy a project deployment file that you created, select **Project deployment file** and enter the path to the .ispac file. To deploy a project that resides in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog, select **Integration Services catalog**, and then enter the server name and the path to the project in the catalog. Click **Next** to see the **Select Destination** page.  
  
### Select Destination  
 To select the destination folder for the project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog, enter the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance or click **Browse** to select from a list of servers. Enter the project path in SSISDB or click **Browse** to select it. Click **Next** to see the **Review** page.  
  
### Review (and deploy)  
 The page allows you to review the settings you have selected. You can change your selections by clicking **Previous**, or by clicking any of the steps in the left pane. Click **Deploy** to start the deployment process.  
  
### Results  
 After the deployment process is complete, you should see the **Results** page. This page displays the success or failure of each action. If the action fails, click the **Failed** in the **Result** column to display an explanation of the error. Click **Save Report...** to save the results to an XML file or Click **Close** to exit the wizard..  
  
##  <a name="PackageModel"></a> Package Deployment Model  
  
### Select Source  
 The **Select Source** page in the **Integration Services Deployment Wizard** shows settings specific to the package deployment model when you selected the **Package Deployment** option for the **deployment model**.  
  
 To select the source packages, click **Browse…** button to select the **folder** that contains the packages or type the folder path in the **Packages folder path** textbox and click **Refresh** button at the bottom of the page. Now, you should see all the packages in the specified folder in the list box. By default, all the packages are selected. Click the **checkbox** in the first column to choose which packages you want to be deployed to server.  
  
 Refer to the **Status** and **Message** columns to verify the status of package. If the status is set to **Ready** or **Warning**, the deployment wizard would not block the deployment process. Whereas, if the status is set to **Error**, the wizard would not proceed further to deploy selected packages. To view the detailed Warning/Error messages, click the link in the **Message** column.  
  
 If the sensitive data or package data are encrypted with a password, type the password in the **Password** column and click the **Refresh** button to verify whether the password is accepted. If the password is correct, the status would change to **Ready** and the warning message will disappear. If there are multiple packages with the same password, select the packages with the same encryption password, type the password in the **Password** textbox and click **Apply** button. The password would be applied to the selected packages.  
  
 If the status of all the selected packages is not set to **Error**, the **Next** button will be enabled so that you can continue with the package deployment process.  
  
### Select Destination  
 After selecting package sources, click **Next** button to switch to the **Select Destination** page. Packages must be deployed to a project in the SSIS Catalog (SSISDB). Therefore, before deploying packages, please ensure the destination project already exists in the SSIS Catalog. , otherwise create an empty project.In the **Select Destination** page, type the server name in the **Server Name** textbox or click the **Browse…** button to select a server instance. Then click the **Browse…** button next to **Path** textbox to specify the destination project. If the project does not exist, click the **New project…** to create an empty project as the destination project. The project **MUST** be created under a folder.  
  
### Review and deploy  
 Click **Next** on the **Select Destination** page to switch to the **Review** page in the **Integration Services Deployment Wizard**. In the review page, review the summary report about the deployment action. After the verification, click **Deploy** button to perform the deployment action.  
  
### Results  
 After the deployment is complete, you should see the **Results** page. In the **Results** page, review results from each step in the deployment process. On the **Results** page, click **Save Report** to save the deployment report or **Close** to the close the wizard.  
  
## See Also  
 [Deploy Projects to Integration Services Server](../../integration-services/packages/deploy-projects-to-integration-services-server.md)   
 [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx)  
  
  