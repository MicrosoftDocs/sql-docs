---
title: "Visible Element (ASSL) | Microsoft Docs"
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
  - "Visible Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Visible"
helpviewer_keywords: 
  - "Visible element"
ms.assetid: 3e9baf1b-351f-4ebf-b57d-13d561f72d6f
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Visible Element (ASSL)
  Determines the visibility of the parent element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or one of the elements listed below in the Element Relationships table -->  
  
   <Visible >...</Visible >  
  
</CalculationProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|True|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md), [CubeHierarchy](../../../2014/analysis-services/dev-guide/cubehierarchy-data-type-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), [MemberProperty](../../../2014/analysis-services/dev-guide/attributerelationship-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Visible` element determines whether user interface components should display the parent element.  
  
 The elements that correspond to the parents of `Visible` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty>, <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.CubeDimension>, <xref:Microsoft.AnalysisServices.CubeHierarchy>, <xref:Microsoft.AnalysisServices.Database>, and <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  