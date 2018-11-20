---
title: "Set Multidimensional Database Properties (Analysis Services) | Microsoft Docs"
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
# Set Multidimensional Database Properties (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  There are a number of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database properties that you can configure in the [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] database designer.  
  
 In this designer, you can perform the following types of tasks:  
  
-   If you are connected to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in online mode, you can change the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. If you are working in project mode, you can change the database name for the next deployment of the project. For more information, see [Rename a Multidimensional Database &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/rename-a-multidimensional-database-analysis-services.md) and [Configure Analysis Services Project Properties &#40;SSDT&#41;](../../analysis-services/multidimensional-models/configure-analysis-services-project-properties-ssdt.md).  
  
-   You can provide a description of the database that can be presented to users. You can also view the name of the database, but cannot change it. To change the database name, you must edit the properties of the project.  
  
-   You can provide translations for the database name and description for one or more languages. For more information, see [Cube Translations](../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-translations.md), [Dimension Translations](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimension-translations.md), and [Translation support in Analysis Services](../../analysis-services/translation-support-in-analysis-services.md).  
  
-   You can view and modify default account type mappings. Account type mappings are used when one or more measures use the *ByAccount* aggregation function. For each account type, you can specify an alias and modify the default aggregation function associated with the account type. For more information modifying the default aggregation, see [Define Semiadditive Behavior](../../analysis-services/multidimensional-models/define-semiadditive-behavior.md).  
  
## Database Properties  
 In addition to the above, there are a number of properties of a database that you can configure in the Properties window.  
  
|Property|Description|  
|--------------|-----------------|  
|Aggregation Prefix|The common prefix that can be used for aggregation names for all of the partitions in a database. For more information, see [AggregationPrefix Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/aggregationprefix-element-assl).|  
|Collation|When the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project is deployed to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, the database will inherit from the Collation server property unless a different value is provided here.|  
|DataSourceImpersonationInfo|Specifies the default impersonation mode for all data source objects in the database. This is the mode that the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service uses when processing objects, synchronizing servers, and executing the OpenQuery and SystemOpenSchema data mining statements.|  
|Estimated Size|Provides an estimated size of the database files on disk. If data is stored in multiple locations, this estimate will be limited to just the data files stored under the database folder.<br /><br /> **EstimatedSize** can also be used as a basis for estimating memory. Typically, memory requirements are larger than the size of data on disk due to additional data structures that are created when the database is loaded into memory.<br /><br /> To further estimate memory requirements, you can also use Task Manager to look at the Analysis Services process memory before and after processing the database and observe the memory utilized as a method for understanding the memory requirements of the database.|  
|Language|When the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project is deployed to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, the database will inherit from the Language server property unless a different value is provided here.|  
|MasterDataSource ID|Used with remote partitions. For more information, see [Remote Partitions](../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-remote-partitions.md).|  
  
## See Also  
 [Database Properties Dialog Box &#40;SSAS - Multidimensional&#41;](http://msdn.microsoft.com/library/70f000b7-917f-4699-b142-7a0d13ff767c)   
 [Configure Analysis Services Project Properties &#40;SSDT&#41;](../../analysis-services/multidimensional-models/configure-analysis-services-project-properties-ssdt.md)  
  
  
