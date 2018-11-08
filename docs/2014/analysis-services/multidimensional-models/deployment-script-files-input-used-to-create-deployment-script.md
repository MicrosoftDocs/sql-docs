---
title: "Understanding the Input Files Used to Create the Deployment Script | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "input files [Analysis Services]"
  - "Analysis Services Deployment Wizard, scripts"
  - "deploying [Analysis Services], input files"
  - "Analysis Services Deployment Wizard, input files"
  - "scripts [Analysis Services], deployment"
  - "Analysis Services deployments, input files"
  - "modifying input files"
ms.assetid: 20e080cd-6a0e-4591-b022-ea4cd3638e36
author: minewiskan
ms.author: owend
manager: craigg
---
# Understanding the Input Files Used to Create the Deployment Script
  When you build a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] generates XML files for the project. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] puts these XML files in the Output folder of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. By default output is out in the \Bin folder. The following table lists the XML files that [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] creates.  
  
|XMLA file|Description|  
|---------------|-----------------|  
|\<*project name*>.asdatabase|Contains the declarative definitions for all the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects in the project.|  
|\<*project name*>.deploymenttargets|Contains the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and database in which the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects will be created.|  
|\<*project name*>.configsettings|Contains environment specific settings, such as data source connection information and object storage locations. Settings in this file override settings in the \<*project name*>.asdatabase file.|  
|\<*project name*>.deploymentoptions|Contains deployment options, such as whether deployment is transactional and whether deployed objects should be processed after deployment.|  
  
> [!NOTE]  
>  [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] never stores passwords in its project files.  
  
## Modifying the Input Files  
 Modifying the values in the input files, or the values retrieved from the input files, makes it possible to change the deployment destination, the configuration settings, and deployment options without editing the whole \<*project name*>.asdatabase file (or a whole XMLA script file if you generate a script from an existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database). Being able to modify individual files lets you easily create different deployment scripts for different purposes.  
  
 The following topics explain how to modify values in the various input files:  
  
-   [Specifying the Installation Target](deployment-script-files-specifying-the-installation-target.md)  
  
-   [Specifying Partition and Role Deployment Options](deployment-script-files-partition-and-role-deployment-options.md)  
  
-   [Specifying Configuration Settings for Solution Deployment](deployment-script-files-solution-deployment-config-settings.md)  
  
-   [Specifying Processing Options](deployment-script-files-specifying-processing-options.md)  
  
## See Also  
 [Running the Analysis Services Deployment Wizard](running-the-analysis-services-deployment-wizard.md)   
 [Understanding the Analysis Services Deployment Script](understanding-the-analysis-services-deployment-script.md)  
  
  
