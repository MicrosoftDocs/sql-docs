---
title: "Configure Analysis Services Project Properties (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "VS.TOOLSOPTIONSPAGES.BUSINESS_INTELLIGENCE_DESIGNERS.ANALYSIS_SERVICES_DESIGNERS.GENERAL"
helpviewer_keywords: 
  - "projects [Analysis Services], properties"
ms.assetid: d9786c66-7d8c-48e3-950d-3f25044b4ce2
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Analysis Services Project Properties (SSDT)
  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project is defined with certain default properties that affect building and deploying the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.  
  
 To change project properties, right-click the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project object and then click **Properties**. Alternatively, you can click **Properties** on the Project menu.  
  
## Property Description  
 The following table describes each project property, lists its default value, and provides information about changing its value.  
  
|Property|Default Setting|Description|  
|--------------|---------------------|-----------------|  
|Build / Deployment Server Edition|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition used to develop the project|Specifies the edition of the server to which projects will finally be deployed. When working with multiple developers on a project, developers need to understand the server edition to know which features to incorporate into the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
|Build / Deployment Server Edition|The version used to develop the projects|Specifies the version of the server to which projects will finally be deployed.|  
|Build / Outputs|/bin|The relative path for the output of the project build process|  
|Build / Remove Passwords|True|Specifies whether known passwords will be removed from connection strings that are written to the output directory during the build process. Passwords are removed to increase security. If passwords are removed, they will need to be provided when the deployed project is processed in order for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to access the source data.|  
|Debugging / Start Object|\<Currently Active Object>|Determines that object that will be started when you start debugging.|  
|Deployment / Deployment Mode|Deploy Changes Only|By default, only changes to project objects are deployed (provided that no other changes were made to the objects directly outside of the project). You can also choose to have all project objects deployed during each deployment. For best performance, use Deploy Changes Only.|  
|Deployment / Processing Option|Default|By default, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will determine the type of processing required when changes to objects are deployed. This generally results in the fastest deployment time. However, you can also choose to have either full processing or no processing performed with each deployment.|  
|Deployment / Transactional Deployment|False|By default, the deployment of changed or all objects is not transactional with the processing of those deployed objects. Deployment can succeed and persist even though processing fails. You can change this default to incorporate deployment and processing in a single transaction.|  
|Deployment / Target Server|localhost|By default, database objects within the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project will be deployed to the default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on the local computer on which [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] is being used. Change this default to specify a named instance on the local computer or any instance on any remote computer on which you have permission to create [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects.|  
|Deployment / Database|\<project name>|By default, the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in which the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project objects will be instantiated upon deployment is the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project at the time it was defined. Change this property to change the name of database on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance specified by the Server property.|  
  
## Property Configurations  
 Properties are defined on a per configuration basis. Project configurations enable developers to work with an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project with different build, debugging, and deployment settings without editing the underlying XML project files directly.  
  
 A project is initially created with a single configuration, called Development. You can create additional configurations and switch between configurations using the Configuration Manager.  
  
 Until additional configurations are created, all developers use this common configuration. However, during the various phases of project development - such as during the initial development and testing of a project - different developers will may use different data sources and deploy the project to different servers for different purposes. Configurations enable you to retain these different settings in different configuration files.  
  
## See Also  
 [Build Analysis Services Projects &#40;SSDT&#41;](build-analysis-services-projects-ssdt.md)   
 [Deploy Analysis Services Projects &#40;SSDT&#41;](deploy-analysis-services-projects-ssdt.md)  
  
  
