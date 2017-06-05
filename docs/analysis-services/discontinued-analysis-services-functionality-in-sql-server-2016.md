---
title: "Discontinued Analysis Services Functionality in SQL Server 2016 | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Analysis Services, backward compatibility"
  - "SSAS, backward compatibility"
  - "SQL Server Analysis Services, backward compatibility"
  - "discontinued functionality [Analysis Services]"
  - "unsupported features [Analysis Services]"
ms.assetid: 39406be1-9819-4629-9c29-b32fb20bab2e
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Discontinued Analysis Services Functionality in SQL Server 2016
  A *discontinued feature* is one that is no longer supported. It might also be physically removed from the product. The following features are discontinued SQL Server 2016 Analysis Services.  
  
|||  
|-|-|  
|**Feature**|**Replacment or workaround**|  
|[CalculationPassValue &#40;MDX&#41;](../mdx/calculationpassvalue-mdx.md)|None. This feature was deprecated in SQL Server 2005.|  
|[CalculationCurrentPass &#40;MDX&#41;](../mdx/calculationcurrentpass-mdx.md)|None. This feature was deprecated in SQL Server 2005.|  
|NON_EMPTY_BEHAVIOR query optimizer hint|None. This feature was deprecated in SQL Server 2008.|  
|COM assemblies|None. This feature was deprecated in SQL Server 2008.|  
|CELL_EVALUATION_LIST intrinsic cell property|None. This feature was deprecated in SQL Server 2005.|  
  
> [!NOTE]  
>  Previously deprecated feature announcements from [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] remain in effect. Because the code supporting those features has not yet been cut from the product, many  of these features are still present in this release. While previously deprecated features might be accessible, they are still considered deprecated and could be physically removed from the product at any point during the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] release. We strongly recommend that you avoid using deprecated features in any new models or applications based on [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
## See Also  
 [Analysis Services Backward Compatibility](../analysis-services/analysis-services-backward-compatibility.md)   
 [Deprecated Analysis Services Features in SQL Server 2016](../analysis-services/deprecated-analysis-services-features-in-sql-server-2016.md)  
  
  