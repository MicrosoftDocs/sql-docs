---
title: "Discontinued Analysis Services Functionality in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services, backward compatibility"
  - "SSAS, backward compatibility"
  - "SQL Server Analysis Services, backward compatibility"
  - "discontinued functionality [Analysis Services]"
  - "unsupported features [Analysis Services]"
ms.assetid: 39406be1-9819-4629-9c29-b32fb20bab2e
author: minewiskan
ms.author: owend
manager: craigg
---
# Discontinued Analysis Services Functionality in SQL Server 2014
  This topic describes [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features that are no longer available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
## Discontinued Features in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
  
|Category|Deprecated feature|Replacement|  
|--------------|------------------------|-----------------|  
|Local cubes|InsertInto connection string property|Original connection string syntax for populating local cubes is replaced by the Create Global Cube statement. For more information, see [CREATE GLOBAL CUBE Statement  &#40;MDX&#41;](/sql/mdx/mdx-data-definition-create-global-cube).|  
|Local cubes|CreateCube connection string property|Original connection string syntax for populating local cubes is replaced by the Create Global Cube statement. For more information, see [CREATE GLOBAL CUBE Statement  &#40;MDX&#41;](/sql/mdx/mdx-data-definition-create-global-cube).|  
|Data Mining|SQL Server 2000 PMML|The SQL Server 2000 PMML feature produced a form of PMML that had proprietary extensions to support unique features provided by Data Mining algorithms that were not available in the PMML specification. In SQL Server 2005, Analysis Services updated the PMML feature to the newer PMML 2.1 standard. As a result, the proprietary extensions added in SQL Server 2000 are no longer needed, although they are still supported in this release.|  
|MDX Statement|Create Action statement|This statement is included for backwards compatibility. It is replaced by the Action object. For more information about how to create actions in recent versions of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], see [Actions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models/actions-analysis-services-multidimensional-data.md).|  
  
## Discontinued Features in Previous Releases  
 Migration Wizard, used to migrate SQL Server 2000 Analysis Services databases to newer versions, is discontinued because SQL Server 2000 is no longer supported.  
  
 Decision Support Objects (DSO) library that provided compatibility with SQL Server 2000 Analysis Services databases is also discontinued and no longer part of SQL Server.  
  
## See Also  
 [Analysis Services Backward Compatibility](analysis-services-backward-compatibility.md)  
  
  
