---
title: "Multidimensional Model Databases (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQL Server Management Studio [Analysis Services], databases"
  - "SQL Server Analysis Services, databases"
  - "SSAS, databases"
  - "Analysis Services, databases"
  - "databases [Analysis Services], designing"
  - "Business Intelligence Development Studio, databases [Analysis Services]"
  - "databases [Analysis Services]"
ms.assetid: 78b2f22a-b7bd-4a2b-b6fc-0bff4d2b3168
caps.latest.revision: 55
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Multidimensional Model Databases (SSAS)
  An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database is a collection of data sources, data source views, cubes, dimensions, and roles. Optionally, an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database can include structures for data mining, and custom assemblies that provide a way for you to add user-defined functions to the database.  
  
 Cubes are the fundamental query objects in Analysis Services. When you connect to an Analysis Services database via a client application, you connect to a cube within that database. A database might contain multiple cubes if you are reusing dimensions, assemblies, roles, or mining structures across multiple contexts.  
  
 You can create and modify a [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database programmatically or by using one of these interactive methods:  
  
-   Deploy an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to a designated instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. This process creates an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, if a database with that name does not already exist within that instance, and instantiates the designed objects within the newly created database. When working with an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], changes made to objects in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project take effect only when the project is deployed to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
-   Create an empty [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database within an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and then connect directly to that database using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and create objects within it (rather than within a project). When working with an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in this manner, changes made to objects take effect in the database to which you are connecting when the changed object is saved.  
  
 [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] uses integration with source control software to support multiple developers working with different objects within an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project at the same time. A developer can also interact with an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database directly, rather than through an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, but the risk of this is that the objects in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database can become out of sync with the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project that was used for its deployment. After deployment, you administer an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Certain changes can also be made to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], such as to partitions and roles, which can also cause the objects in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database to become out of sync with the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project that was used for its deployment.  
  
## Related Tasks  
 [Attach and Detach Analysis Services Databases](../../analysis-services/multidimensional-models/attach-and-detach-analysis-services-databases.md)  
  
 [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
 [Document and Script an Analysis Services Database](../../analysis-services/multidimensional-models/document-and-script-an-analysis-services-database.md)  
  
 [Modify or Delete an Analysis Services Database](../../analysis-services/multidimensional-models/modify-or-delete-an-analysis-services-database.md)  
  
 [Move an Analysis Services Database](../../analysis-services/multidimensional-models/move-an-analysis-services-database.md)  
  
 [Rename a Multidimensional Database &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/rename-a-multidimensional-database-analysis-services.md)  
  
 [Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/compatibility-level-of-a-multidimensional-database-analysis-services.md)  
  
 [Set Multidimensional Database Properties &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/set-multidimensional-database-properties-analysis-services.md)  
  
 [Synchronize Analysis Services Databases](../../analysis-services/multidimensional-models/synchronize-analysis-services-databases.md)  
  
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../analysis-services/multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
## See Also  
 [Connect in Online Mode to an Analysis Services Database](../../analysis-services/multidimensional-models/connect-in-online-mode-to-an-analysis-services-database.md)   
 [Create an Analysis Services Project &#40;SSDT&#41;](../../analysis-services/multidimensional-models/create-an-analysis-services-project-ssdt.md)   
 [Querying Multidimensional Data with MDX](../../analysis-services/multidimensional-models/mdx/querying-multidimensional-data-with-mdx.md)  
  
  