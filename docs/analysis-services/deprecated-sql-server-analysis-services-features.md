---
title: "Deprecated features in SQL Server 2017 Analysis Services| Microsoft Docs"
ms.date: "07/07/2017"
ms.prod: "sql-server-2017"
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
ms.assetid:
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Deprecated and discontinued features in SQL Server 2017 Analysis Services
[!INCLUDE[ssas-appliesto-sql2017-later](../includes/ssas-appliesto-sql2017-later.md)]

A *deprecated feature* will be discontinued from the product in a future release, but is still supported and included in the current release to maintain backward compatibility. It's recommended you discontinue using deprecated features in new and existing projects to maintain compatibility with future releases.

A *discontinued feature* was deprecated in an earlier release. It may continue to be included in the current release, but is no longer supported. Discontinued features may be removed entirely in a future release or update.

## Deprecated features in SQL Server 2017 Analysis Services
The following features are deprecated in this release and will no longer be supported in a future release.
  
|||  
|-|-|  
|**Mode/Category**|**Feature**|
|Tabular|Remote linked measure groups|
|Tabular|Models at the 1100 and 1103 compatibility level|
|Tabular|Tabular Object Model properties: Column.TableDetailPosition, Column.IsDefaultLabel, Column.IsDefaultImage|
|Multidimensional|Data Mining|

## Discontinued features in SQL Server 2017 Analysis Services
The following features were deprecated in an earlier release and are no longer supported in this release.
  
|||  
|-|-|  
|**Mode/Category**|**Feature**|  
|Tabular|VertipaqPagingPolicy memory property value (2), enable paging to disk using memory mapped files|
|Multidimensional|Remote partitions|  
|Multidimensional|Remote linked measure groups|  
|Multidimensional|Dimensional writeback|  
|Multidimensional|Linked dimensions|
|Tools|SQL Server Profiler for Trace Capture<br /><br /> The replacement is to use Extended Events Profiler embedded in SQL Server Management Studio.  <br /> See [Monitor Analysis Services with SQL Server Extended Events](../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md).|  
|Tools|Server Profiler for Trace Replay <br />Replacement. There is no replacement.|  
|Trace Management Objects and Trace APIs|Microsoft.AnalysisServices.Trace objects (contains the APIs for Analysis Services Trace and Replay objects). The replacement is multi-part:<br /><br /> -   Trace Configuration: Microsoft.SqlServer.Management.XEvent<br />-   Trace Reading: Microsoft.SqlServer.XEvent.Linq<br />-   Trace Replay: None|  

