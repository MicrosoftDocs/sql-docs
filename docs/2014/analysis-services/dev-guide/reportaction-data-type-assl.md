---
title: "ReportAction Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "ReportAction Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ReportAction"
helpviewer_keywords: 
  - "ReportAction data type"
ms.assetid: b22f0d52-ed3a-4239-840e-0eaf172d7276
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ReportAction Data Type (ASSL)
  Defines a derived data type that represents an action that generates a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report.  
  
## Syntax  
  
```xml  
  
<ReportAction>  
   <!-- The following elements extend Action -->  
   <ReportServer>...</ReportServer>  
   <Path>...</Path>  
   <ReportParameters>...</ReportParameters>  
   <ReportFormatParameters>...</ReportFormatParameters>  
</ReportAction>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Action](../../../2014/analysis-services/dev-guide/action-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Path](../../../2014/analysis-services/dev-guide/path-element-assl.md), [ReportFormatParameters](../../../2014/analysis-services/dev-guide/reportformatparameters-element-assl.md), [ReportParameters](../../../2014/analysis-services/dev-guide/reportparameters-element-assl.md), [ReportServer](../../../2014/analysis-services/dev-guide/reportserver-element-assl.md)|  
|Derived elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md) ([Actions](../../../2014/analysis-services/dev-guide/actions-element-assl.md) collection of [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) or [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md))|  
  
## Remarks  
 The report server responds to URL-based requests for reports. The report action is defined with a type *Report*. The resources and parameters are sent to the server when the action is created. The server exposes the action as an action of type rowset.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportAction>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  