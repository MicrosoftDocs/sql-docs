---
title: "Work with Analysis Services Projects and Databases in Production | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Work with Analysis Services Projects and Databases in Production
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  After you have developed and deployed your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database from your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, you must decide how you wish to make changes to objects in the deployed database. Certain changes, such changes related to security roles, partitioning, and storage settings, can be made using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. Other changes can only be made using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], either in project mode or in online mode (such as adding attributes or user-defined hierarchies).  
  
 As soon as you make a change to a deployed [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] in online mode, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project that was used for deployment becomes out of date. If a developer makes any changes within the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project and attempts to deploy the modified project, the developer will be prompted to overwrite the entire database. If the developer overwrites the entire database, it must also be processed. This issue becomes compounded if the changes made directly to the deployed database by the production staff were not communicated to the development team because they will not understand why their changes no longer appear in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.  
  
 There are several ways in which you can use SQL Server [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tools to avoid the problems inherent in this situation.  
  
-   Method 1: Whenever a change is made to a production version of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, use [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to create a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project based on the modified version of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. This new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project can be checked into the source control system as the master copy of the project. This method will work regardless of whether the change was made to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] in online mode.  
  
-   Method 2: Only make changes to the production version of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] in project mode. With this method, you can use options available to you in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard to preserve changes made by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], such as security roles and storage settings. This ensures that the design-related settings are kept in the project file (storage settings and security roles can be ignored) and the online server is used for storage settings and security roles.  
  
-   Method 3: Only make changes to the production version of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] in online mode. Since both tools are only working with the same online server, there are no possibilities of getting different version out of sync.  
  
  
