---
title: "Database Objects (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Database Objects (Analysis Services - Multidimensional Data)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  A [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance contains database objects and assemblies for use with online analytical processing (OLAP) and data mining.  
  
-   Databases contain OLAP and data mining objects, such as data sources, data source views, cubes, measures, measure groups, dimensions, attributes, hierarchies, mining structures, mining models and roles.  
  
-   Assemblies contain user-defined functions that extend the functionality of the intrinsic functions provided with the Multidimensional Expressions (MDX) and Data Mining Extensions (DMX) languages.  
  
 The <xref:Microsoft.AnalysisServices.Database> object is the container for all data objects that are needed for a business intelligence project (such as OLAP cubes, dimensions, and data mining structures), and their supporting objects (such as <xref:Microsoft.AnalysisServices.DataSource>, <xref:Microsoft.AnalysisServices.Account>, and <xref:Microsoft.AnalysisServices.Role>).  
  
 A <xref:Microsoft.AnalysisServices.Database> object provides access to objects and attributes that include the following:  
  
-   All cubes that you can access, as a collection.  
  
-   All dimensions that you can access, as a collection.  
  
-   All mining structures that you can access, as a collection.  
  
-   All data sources and data source views, as two collections.  
  
-   All roles and database permissions, as two collections.  
  
-   The collation value for the database.  
  
-   The estimated size of the database.  
  
-   The language value of the database.  
  
-   The visible setting for the database.  
  
## In This Section  
 The following topics describe objects shared by both OLAP and data mining features in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
|Topic|Description|  
|-----------|-----------------|  
|[Data Sources in Multidimensional Models](../../../analysis-services/multidimensional-models/data-sources-in-multidimensional-models.md)|Describes a data source in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[Data Source Views in Multidimensional Models](../../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)|Describes a logical data model based on one or more data sources, in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[Cubes in Multidimensional Models](../../../analysis-services/multidimensional-models/cubes-in-multidimensional-models.md)|Describes cubes and cube objects, including measures, measure groups, dimension usage relationships, calculations, key performance indicators, actions, translations, partitions, and perspectives.|  
|[Dimensions &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)|Describes dimensions and dimension objects, including attributes, attribute relationships, hierarchies, levels, and members.|  
|[Mining Structures &#40;Analysis Services - Data Mining&#41;](../../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)|Describes mining structures and mining objects, including mining models.|  
|[Security Roles  &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/security-roles-analysis-services-multidimensional-data.md)|Describes a role, the security mechanism used to control access to objects in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[Multidimensional Model Assemblies Management](../../../analysis-services/multidimensional-models/multidimensional-model-assemblies-management.md)|Describes an assembly, a collection of user-defined functions used to extend the MDX and DMX languages, in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
  
## See Also  
 [Supported Data Sources &#40;SSAS - Multidimensional&#41;](../../../analysis-services/multidimensional-models/supported-data-sources-ssas-multidimensional.md)   
 [Multidimensional Model Solutions ](../../../analysis-services/multidimensional-models/multidimensional-model-solutions-ssas.md)   
 [Data Mining Solutions](../../../analysis-services/data-mining/data-mining-solutions.md)  
  
  
