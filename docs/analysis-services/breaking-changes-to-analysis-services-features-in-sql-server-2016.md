---
title: "Breaking Changes to Analysis Services Features in SQL Server 2016 | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "breaking changes [Analysis Services]"
  - "upgrading Analysis Services"
ms.assetid: aeb02542-5a6c-458c-a110-13413dd3e9d9
caps.latest.revision: 55
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Breaking Changes to Analysis Services Features in SQL Server 2016
  A *breaking change* causes a data model, application code, or script  to no longer function after upgrading either the model or the server.  
  
> [!NOTE]  
>  In contrast, a *behavior change* is characterized as a code change that doesn't break a model or application, but does introduce a different behavior from a previous release.  Examples of behavior changes might include changing a default value, or disallowing a configuration of properties or options that was previously allowed. To learn more about behavior changes in this release, see [Behavior Changes to Analysis Services Features in SQL Server 2016](../analysis-services/behavior-changes-to-analysis-services-features-in-sql-server-2016.md).  
  
## .NET 4.0 version upgrade  
 Analysis Services Management Objects (AMO), ADOMD.NET, and Tabular Object Model (TOM) client libraries now target the .NET 4.0 runtime. This can be a breaking change for applications that target .NET 3.5. Applications using newer versions of these assemblies must now target .NET 4.0 or later.  
  
## AMO version upgrade  
 This release is a version upgrade for [Analysis Services Management Objects &#40;AMO&#41;](https://msdn.microsoft.com/library/mt436122.aspx) and is  a breaking change under certain circumstances.  Existing code and scripts that call into AMO will continue to run as before if you upgrade from a previous version. However, if you need to *recompile* your application and you are targeting a SQL Server 2016 Analysis Services instance, you must add the following namespace to make your code or script operational:  
  
```  
  
using Microsoft.AnalysisServices;  
using Microsoft.AnalysisServices.Core;  
  
```  
  
 The [Microsoft.AnalysisServices.Core](https://msdn.microsoft.com/library/microsoft.analysisservices.core.aspx) namespace is now required whenever you reference the Microsoft.AnalysisServices assembly in your code. Objects that were previously only in the **Microsoft.AnalysisServices** namespace are moved to the Core namespace in this release if the object is used the same way in both tabular and multidimensional scenarios.  For example, server-related APIs are relocated to the Core namespace.  
  
 Although there are now multiple namespaces, both exist in the same assembly (Microsoft.AnalysisServices.dll).  
  
## XEvent DISCOVER changes  
 To better support XEvent DISCOVER streaming in SSMS for [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], `DISCOVER_XEVENT_TRACE_DEFINITION` is replaced with the following XEvent traces:  
  
-   DISCOVER_XEVENT_PACKAGES  
  
-   DISCOVER_XEVENT_OBJECT  
  
-   DISCOVER_XEVENT_OBJECT_COLUMNS  
  
-   DISCOVER_XEVENT_SESSION_TARGETS  
  
## See Also  
 [Analysis Services Backward Compatibility](../analysis-services/analysis-services-backward-compatibility.md)  
  
  