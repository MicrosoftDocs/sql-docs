---
title: "Deprecated Analysis Services Features in SQL Server 2016 | Microsoft Docs"
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
  - "deprecated features [Analysis Services]"
ms.assetid: 2c96ecfe-a170-41d0-bee3-74503f880197
caps.latest.revision: 52
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Deprecated Analysis Services Features in SQL Server 2016
  A *deprecated feature* is a feature will be cut from the product in a future release, but is still supported and included in the current release to maintain backward compatibility. Typically,  a deprecated feature is removed in a major release, often within two releases of the original  announcement. For example, deprecated features announced in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] are likely to be unsupported by [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
 **Not supported in the next major release of SQL Server**  
  
|||  
|-|-|  
|**Category**|**Feature**|  
|Multidimensional|Remote partitions|  
|Multidimensional|Remote linked measure groups|  
|Multidimensional|Dimensional writeback|  
|Multidimensional|Linked dimensions|  
  
 **Not supported in a future release of SQL Server**  
  
|||  
|-|-|  
|**Category**|**Feature**|  
|Multidimensional|SQL Server table notifications for proactive caching.  <br />The replacement is to use polling for proactive caching. <br />See [Proactive Caching &#40;Dimensions&#41;](../analysis-services/multidimensional-models-olap-logical-dimension-objects/proactive-caching-dimensions.md) and [Proactive Caching &#40;Partitions&#41;](../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).|  
|Multidimensional|Session cubes. There is no replacement.|  
|Multidimensional|Local cubes. There is no replacement.|  
|Tabular|Tabular model 1100 and 1103 compatibility levels will not be supported in a future release. The replacement is to set models at compatibility level 1200 or higher, converting model definitions to tabular metadata. See [Compatibility Level for Tabular models in Analysis Services](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md).|  
|Tools|SQL Server Profiler for Trace Capture<br /><br /> The replacement is to use Extended Events Profiler embedded in SQL Server Management Studio.  <br /> See [Monitor Analysis Services with SQL Server Extended Events](../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md).|  
|Tools|Server Profiler for Trace Replay <br />Replacement. There is no replacement.|  
|Trace Management Objects and Trace APIs|Microsoft.AnalysisServices.Trace objects (contains the APIs for Analysis Services Trace and Replay objects). The replacement is multi-part:<br /><br /> -   Trace Configuration: Microsoft.SqlServer.Management.XEvent<br />-   Trace Reading: Microsoft.SqlServer.XEvent.Linq<br />-   Trace Replay: None|  
  
> [!NOTE]  
>  Previously deprecated feature announcements from [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] remain in effect. Because the code supporting those features has not yet been cut from the product, many  of these features are still present in this release. While previously deprecated features might be accessible, they are still considered deprecated and could be physically removed from the product at any point during the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] release. We strongly recommend that you avoid using deprecated features in any new models or applications based on [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
## See Also  
 [Analysis Services Backward Compatibility](../analysis-services/analysis-services-backward-compatibility.md)   
 [Discontinued Analysis Services Functionality in SQL Server 2016](../analysis-services/discontinued-analysis-services-functionality-in-sql-server-2016.md)  
  
  