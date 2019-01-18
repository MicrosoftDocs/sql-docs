---
title: "Create a Deployment Utility | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "deploying packages [Integration Services], deployment utility"
  - "deployment utility [Integration Services]"
ms.assetid: 354322a4-ae8c-4d92-8e71-42d29dbd0614
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Create a Deployment Utility
  The first step in deploying packages is to create a deployment utility for an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. The deployment utility is a folder that contains the files you need to deploy the packages in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project on a different server. The deployment utility is created on the computer on which the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project is stored.  
  
 You create a package deployment utility for an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project by first configuring the build process to create a deployment utility, and then building the project. When you build the project, all packages and package configurations in the project are automatically included. To deploy additional files such as a Readme file with the project, place the files in the **Miscellaneous** folder of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. When the project is built, these files are also automatically included.  
  
 You can configure each project deployment differently. Before you build the project and create the package deployment utility, you can set the properties on the deployment utility to customize the way the packages in the project will be deployed. For example, you can specify whether package configurations can be updated when the project is deployed. To access the properties of an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, right-click the project and click **Properties**.  
  
 The following table lists the deployment utility properties.  
  
|Property|Description|  
|--------------|-----------------|  
|AllowConfigurationChange|A value that specifies whether configurations can be updated during deployment.|  
|CreateDeploymentUtility|A value that specifies whether a package deployment utility is created when the project is built. This property must be `True` to create a deployment utility.|  
|DeploymentOutputPath|The location, relative to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, of the deployment utility.|  
  
 When you build an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, a manifest file, \<project name>.SSISDeploymentManifest.xml, is created and added, together with copies of the project packages and package dependencies, to the bin\Deployment folder in the project, or to the location specified in the DeploymentOutputPath property. The manifest file lists the packages, the package configurations, and any miscellaneous files in the project.  
  
 The content of the deployment folder is refreshed every time that you build the project. This means that any file saved to this folder that is not copied to the folder again by the build process will be deleted. For example, package configuration files saved to the deployment folders will be deleted.  
  
### To create a package deployment utility  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution that contains the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project for which you want to create a package deployment utility.  
  
2.  Right-click the project and click **Properties**.  
  
3.  In the **\<project name> Property Pages** dialog box, click **Deployment Utility**.  
  
4.  To update package configurations when packages are deployed, set **AllowConfigurationChanges** to `True`.  
  
5.  Set `CreateDeploymentUtility` to `True`.  
  
6.  Optionally, update the location of the deployment utility by modifying the `DeploymentOutputPath` property.  
  
7.  Click **OK**.  
  
8.  In Solution Explorer, right-click the project, and then click **Build**.  
  
9. View the build progress and build errors in the **Output** window.  
  
## See Also  
 [Package Configurations](../../2014/integration-services/package-configurations.md)   
 [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)   
 [Deploy Packages by Using the Deployment Utility](../../2014/integration-services/deploy-packages-by-using-the-deployment-utility.md)   
 [Package Deployment &#40;SSIS&#41;](packages/legacy-package-deployment-ssis.md)  
  
  
