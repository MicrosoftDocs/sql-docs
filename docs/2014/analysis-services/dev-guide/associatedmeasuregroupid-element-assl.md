---
title: "AssociatedMeasureGroupID Element (ASSL) | Microsoft Docs"
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
  - "AssociatedMeasureGroupID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AssociatedMeasureGroupID"
helpviewer_keywords: 
  - "AssociatedMeasureGroupID element"
ms.assetid: a18ff25b-00a2-4ddf-abcc-ef4d52c8a462
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AssociatedMeasureGroupID Element (ASSL)
  Contains the ID of the [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) element associated with a [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md) element or a [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or Kpi -->  
   ...  
   <AssociatedMeasureGroupID>...</AssociatedMeasureGroupID>  
   ...  
</CalculationProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 When applied to `CalculationProperty` elements, the `AssociatedMeasureGroupID` property applies to elements with a [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md) of *Member*.  
  
 The elements that correspond to the parents of `AssociatedMeasureGroupID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Kpi>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  