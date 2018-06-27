---
title: "SQL Server 2016 Analysis Services backward compatibility | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Analysis Services backward compatibility (SQL Server 2016)
[!INCLUDE[ssas-appliesto-sql2016](../includes/ssas-appliesto-sql2016.md)]

This article describes changes in feature availability and behavior between the current version and the previous version.

## Deprecated features
A *deprecated feature* will be discontinued from the product in a future release, but is still supported and included in the current release to maintain backward compatibility. It's recommended you discontinue using deprecated features in new and existing projects to maintain compatibility with future releases.
  
The following features are deprecated in this release:
  
|||  
|-|-|  
|**Mode/Category**|**Feature**|  
|Multidimensional|Remote partitions|  
|Multidimensional|Remote linked measure groups|  
|Multidimensional|Dimensional writeback|  
|Multidimensional|Linked dimensions|   
|Multidimensional|SQL Server table notifications for proactive caching.  <br />The replacement is to use polling for proactive caching. <br />See [Proactive Caching &#40;Dimensions&#41;](../analysis-services/multidimensional-models-olap-logical-dimension-objects/proactive-caching-dimensions.md) and [Proactive Caching &#40;Partitions&#41;](../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).|  
|Multidimensional|Session cubes. There is no replacement.|  
|Multidimensional|Local cubes. There is no replacement.|  
|Tabular|Tabular model 1100 and 1103 compatibility levels will not be supported in a future release. The replacement is to set models at compatibility level 1200 or higher, converting model definitions to tabular metadata. See [Compatibility Level for Tabular models in Analysis Services](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md).|  
|Tools|SQL Server Profiler for Trace Capture<br /><br /> The replacement is to use Extended Events Profiler embedded in SQL Server Management Studio.  <br /> See [Monitor Analysis Services with SQL Server Extended Events](../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md).|  
|Tools|Server Profiler for Trace Replay <br />Replacement. There is no replacement.|  
|Trace Management Objects and Trace APIs|Microsoft.AnalysisServices.Trace objects (contains the APIs for Analysis Services Trace and Replay objects). The replacement is multi-part:<br /><br /> -   Trace Configuration: Microsoft.SqlServer.Management.XEvent<br />-   Trace Reading: Microsoft.SqlServer.XEvent.Linq<br />-   Trace Replay: None|  
  
> [!NOTE]  
>  Previously deprecated feature announcements from [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] remain in effect. Because the code supporting those features has not yet been cut from the product, many  of these features are still present in this release. While previously deprecated features might be accessible, they are still considered deprecated and could be physically removed from the product at any time.  

## Discontinued features
A *discontinued feature* was deprecated in an earlier release. It may continue to be included in the current release, but is no longer supported. Discontinued features may be removed entirely in a future release or update.

The following features were deprecated in an earlier release and are no longer supported in this release.

|||  
|-|-|  
|**Feature**|**Replacment or workaround**|  
|[CalculationPassValue &#40;MDX&#41;](../mdx/calculationpassvalue-mdx.md)|None. This feature was deprecated in SQL Server 2005.|  
|[CalculationCurrentPass &#40;MDX&#41;](../mdx/calculationcurrentpass-mdx.md)|None. This feature was deprecated in SQL Server 2005.|  
|NON_EMPTY_BEHAVIOR query optimizer hint|None. This feature was deprecated in SQL Server 2008.|  
|COM assemblies|None. This feature was deprecated in SQL Server 2008.|  
|CELL_EVALUATION_LIST intrinsic cell property|None. This feature was deprecated in SQL Server 2005.|  
  
> [!NOTE]  
>  Previously deprecated feature announcements from [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] remain in effect. Because the code supporting those features has not yet been cut from the product, many  of these features are still present in this release. While previously deprecated features might be accessible, they are still considered deprecated and could be physically removed from the product at any time.  

## Breaking changes
A *breaking change* causes a data model, application code, or script  to no longer function after upgrading either the model or the server.
  
### .NET 4.0 version upgrade  
 Analysis Services Management Objects (AMO), ADOMD.NET, and Tabular Object Model (TOM) client libraries now target the .NET 4.0 runtime. This can be a breaking change for applications that target .NET 3.5. Applications using newer versions of these assemblies must now target .NET 4.0 or later.  
  
### AMO version upgrade  
 This release is a version upgrade for [Analysis Services Management Objects &#40;AMO&#41;](https://msdn.microsoft.com/library/mt436122.aspx) and is  a breaking change under certain circumstances.  Existing code and scripts that call into AMO will continue to run as before if you upgrade from a previous version. However, if you need to *recompile* your application and you are targeting a SQL Server 2016 Analysis Services instance, you must add the following namespace to make your code or script operational:  
  
```  
  
using Microsoft.AnalysisServices;  
using Microsoft.AnalysisServices.Core;  
  
```  
  
 The [Microsoft.AnalysisServices.Core](https://msdn.microsoft.com/library/microsoft.analysisservices.core.aspx) namespace is now required whenever you reference the Microsoft.AnalysisServices assembly in your code. Objects that were previously only in the **Microsoft.AnalysisServices** namespace are moved to the Core namespace in this release if the object is used the same way in both tabular and multidimensional scenarios.  For example, server-related APIs are relocated to the Core namespace.  
  
 Although there are now multiple namespaces, both exist in the same assembly (Microsoft.AnalysisServices.dll).  
  
### XEvent DISCOVER changes  
 To better support XEvent DISCOVER streaming in SSMS for SQL Server 2016 Analysis Services, `DISCOVER_XEVENT_TRACE_DEFINITION` is replaced with the following XEvent traces:  
  
-   DISCOVER_XEVENT_PACKAGES  
  
-   DISCOVER_XEVENT_OBJECT  
  
-   DISCOVER_XEVENT_OBJECT_COLUMNS  
  
-   DISCOVER_XEVENT_SESSION_TARGETS  

## Behavior changes
A *behavior change* affects how features work or interact in the current version as compared to earlier versions of SQL Server.
  
Revisions to  default values, manual configuration required to complete an upgrade or restore functionality, or a new implementation of an existing feature are all examples of a behavior change in the product.
  
Feature behaviors that changed in this release, yet do not break an existing model or code post-upgrade, are listed here.
  
### Analysis Services in SharePoint mode
 Running the Power Pivot Configuration wizard is no longer required as a post-installation task. This is true for all supported versions of SharePoint that load models from the current SQL Server 2016 Analysis Services.
  
### DirectQuery mode for Tabular models
 *DirectQuery* is a data access mode for tabular models, where query execution is performed on a backend relational database, retrieving a result set in real time. It's often used for very large datasets that cannot fit in memory or when data is volatile and you want the most recent data returned in queries against a tabular model.
  
 DirectQuery has existed as a data access mode for the last several releases. In SQL Server 2016 Analysis Services, the implementation has been slightly revised, assuming the tabular model is at compatibility level 1200 or higher. DirectQuery has fewer restrictions than before. It also has different database properties.
  
 If you are using DirectQuery in an existing tabular model, you can keep the model at its currently compatibility level of 1100 or 1103 and continue to use DirectQuery as its implemented for those levels. Alternatively, you can upgrade to 1200 or higher to benefit from enhancements made to DirectQuery.
  
 There is no in-place upgrade of a DirectQuery model because the settings from older compatibility levels do not have exact counterparts in the newer 1200 and higher compatibility levels. If you have an existing tabular model that runs in DirectQuery mode, you should open the model in SQL Server Data Tools, turn DirectQuery off, set the **Compatibility Level** property to 1200 or higher, and then reconfigure the DirectQuery properties. See [DirectQuery Mode](../analysis-services/tabular-models/directquery-mode-ssas-tabular.md) for details.


## See also
[Analysis Services backward compatibility (SQL Server 2017)](analysis-services-backward-compatibility-sql2017.md)
