---
title: "AssociatedMeasureGroupID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# AssociatedMeasureGroupID Element (ASSL)
  Contains the ID of the [MeasureGroup](../objects/group-element-assl.md) element associated with a [CalculationProperty](../objects/calculationproperty-element-assl.md) element or a [Kpi](../objects/kpi-element-assl.md) element.  
  
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
|Parent element|[CalculationProperty](../objects/calculationproperty-element-assl.md), [Kpi](../objects/kpi-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 When applied to `CalculationProperty` elements, the `AssociatedMeasureGroupID` property applies to elements with a [CalculationType](calculationtype-element-assl.md) of *Member*.  
  
 The elements that correspond to the parents of `AssociatedMeasureGroupID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Kpi>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../collections/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../objects/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../collections/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
