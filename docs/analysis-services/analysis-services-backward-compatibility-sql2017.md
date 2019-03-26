---
title: "SQL Server 2017 Analysis Services backward compatibility | Microsoft Docs"
ms.date: 03/08/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
monikerRange: ">= sql-server-2017 || = sqlallproducts-allversions"
---
# Analysis Services backward compatibility (SQL 2017)
[!INCLUDE[ssas-appliesto-sql2017](../includes/ssas-appliesto-sql2017.md)]

This article describes changes in feature availability and behavior between the current release and the previous release.

## Deprecated features
A *deprecated feature* will be discontinued from the product in a future release, but is still supported and included in the current release to maintain backward compatibility. It's recommended you discontinue using deprecated features in new and existing projects to maintain compatibility with future releases. Documentation is not updated for deprecated features.

The following features are deprecated in this release:
  
|||  
|-|-|  
|**Mode/Category**|**Feature**|
|Multidimensional|Data Mining|
|Multidimensional|Remote linked measure groups|
|Tabular|Models at the 1100 and 1103 compatibility level|
|Tabular|Tabular Object Model properties: Column.TableDetailPosition, Column.IsDefaultLabel, Column.IsDefaultImage|
|Tools|SQL Server Profiler for Trace Capture<br /><br /> The replacement is to use Extended Events Profiler embedded in SQL Server Management Studio.  <br /> See [Monitor Analysis Services with SQL Server Extended Events](../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md).|  
|Tools|Server Profiler for Trace Replay <br />Replacement. There is no replacement.|  
|Trace Management Objects and Trace APIs|Microsoft.AnalysisServices.Trace objects (contains the APIs for Analysis Services Trace and Replay objects). The replacement is multi-part:<br /><br /> -   Trace Configuration: Microsoft.SqlServer.Management.XEvent<br />-   Trace Reading: Microsoft.SqlServer.XEvent.Linq<br />-   Trace Replay: None|  


## Discontinued features
A *discontinued feature* was deprecated in an earlier release. It may continue to be included in the current release, but is no longer supported. Discontinued features may be removed entirely in a future release or update.

The following features were deprecated in an earlier release and are no longer supported in this release.
  
|||  
|-|-|  
|**Mode/Category**|**Feature**|  
|Tabular|VertipaqPagingPolicy memory property value (2), enable paging to disk using memory mapped files.|
|Multidimensional|Remote partitions|  
|Multidimensional|Remote linked measure groups|  
|Multidimensional|Dimensional writeback|  
|Multidimensional|Linked dimensions|


## Breaking changes
A *breaking change* causes a feature, data model, application code, or script to no longer function after upgrading to the current release.

There are no breaking changes in this release.

## Behavior changes
A *behavior change* affects how the same feature works in the current release as compared to the previous release. Only significant behavior changes are described. Changes in  user interface are not included.

Changes to MDSCHEMA_MEASUREGROUP_DIMENSIONS and DISCOVER_CALC_DEPENDENCY, detailed in the [What's new in SQL Server 2017 CTP 2.1 for Analysis Services](https://blogs.msdn.microsoft.com/analysisservices/2017/05/18/whats-new-in-sql-server-2017-ctp-2-1-for-analysis-services/) announcement.


## See also
[Analysis Services backward compatibility (SQL Server 2016)](analysis-services-backward-compatibility.md)
