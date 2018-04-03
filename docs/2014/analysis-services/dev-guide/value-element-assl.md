---
title: "Value Element (ASSL) | Microsoft Docs"
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
  - "Value Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Value"
helpviewer_keywords: 
  - "Value element"
ms.assetid: a2fad411-73fd-42df-b4e1-df2cb8454182
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Value Element (ASSL)
  Contains the value of the parent element.  
  
## Syntax  
  
```xml  
  
<AlgorithmParameter> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Value>...</Value>  
   ...  
</AlgorithmParameter>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[AlgorithmParameter](../../../2014/analysis-services/dev-guide/algorithmparameter-element-assl.md)|Any simpleType|  
|[ServerProperty](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md)|Any simpleType|  
|All others|String|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AlgorithmParameter](../../../2014/analysis-services/dev-guide/algorithmparameter-element-assl.md), [Annotation](../../../2014/analysis-services/dev-guide/annotation-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md), [ReportFormatParameter](../../../2014/analysis-services/dev-guide/reportformatparameter-element-assl.md), [ReportParameter](../../../2014/analysis-services/dev-guide/reportparameter-element-assl.md), [ServerProperty](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Value` element contains the value associated with the parent object. The expected value of the `Value` element varies depending on the parent element, as described in the following table.  
  
|Parent element|Expected value|  
|--------------------|--------------------|  
|[AlgorithmParameter](../../../2014/analysis-services/dev-guide/algorithmparameter-element-assl.md)|The value of the algorithm parameter.|  
|[Annotation](../../../2014/analysis-services/dev-guide/annotation-element-assl.md)|The value of the annotation.|  
|[Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md)|A Multidimensional Expressions (MDX) expression used to calculate the value of the key performance indicator (KPI).|  
|[ReportFormatParameter](../../../2014/analysis-services/dev-guide/reportformatparameter-element-assl.md)|The value of the report format parameter.|  
|[ReportParameter](../../../2014/analysis-services/dev-guide/reportparameter-element-assl.md)|An MDX expression used to calculate the value of the report parameter.|  
|[ServerProperty](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md)|The value of the server property for the currently running instance.|  
  
 The elements that correspond to the parents of `Value` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AlgorithmParameter>, <xref:Microsoft.AnalysisServices.Annotation>, <xref:Microsoft.AnalysisServices.Kpi>, <xref:Microsoft.AnalysisServices.ReportParameter>, and <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  