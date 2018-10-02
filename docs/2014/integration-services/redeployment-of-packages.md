---
title: "Redeployment of Packages | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "redeploying packages [Integration Services]"
  - "deploying packages [Integration Services], redeploying"
ms.assetid: 86806efb-8cf4-4f9d-9824-1152cb4c495c
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Redeployment of Packages
  After a project is deployed, you may need to update or extend package functionality and then redeploy the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the updated packages. As part of the process of redeploying packages, you should review the configuration properties that are included in the deployment utility. For example, you may not want to allow configuration changes after the package is redeployed.  
  
## Process for Redeployment  
 After you finish updating the packages, you rebuild the project, copy the deployment folder to the target computer, and then rerun the Package Installation Wizard.  
  
 If you update only a few packages in the project, you may not want to redeploy the entire project. To deploy only a few packages, you can create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, add the updated packages to the new project, and then build and deploy the project. Package configurations are automatically copied with the package when you add the package to a different project.  
  
## Related Tasks  
 [Deploy Packages by Using the Deployment Utility](../../2014/integration-services/deploy-packages-by-using-the-deployment-utility.md)  
  
  
