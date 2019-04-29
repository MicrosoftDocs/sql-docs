---
title: "Package Deployment (SSIS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, deploying"
  - "deploying packages [Integration Services]"
  - "SQL Server Integration Services packages, deploying"
  - "deploying packages [Integration Services], about deploying packages"
  - "packages [Integration Services], deploying"
  - "SSIS packages, deploying"
ms.assetid: 0f5fc7be-e37e-4ecd-ba99-697c8ae3436f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Package Deployment (SSIS)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes tools and wizards that make it simple to deploy packages from the development computer to the production server or to other computers.  
  
 There are four steps in the package deployment process:  
  
1.  The first optional step is optional and involves creating package configurations that update properties of package elements at run time. The configurations are automatically included when you deploy the packages.  
  
2.  The second step is to build the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to create a package deployment utility. The deployment utility for the project contains the packages that you want to deploy  
  
3.  The third step is to copy the deployment folder that was created when you built the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to the target computer.  
  
4.  The fourth step is to run, on the target computer, the Package Installation Wizard to install the packages to the file system or to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Related Tasks  
 For information about how to create a deployment utility, see [Create a Deployment Utility](../create-a-deployment-utility.md).  
  
 For information about how to deploy packages using the deployment utility, see [Deploy Packages by Using the Deployment Utility](../deploy-packages-by-using-the-deployment-utility.md).  
  
 For information about how to create package configurations, see [Create Package Configurations](../create-package-configurations.md).  
  
  
