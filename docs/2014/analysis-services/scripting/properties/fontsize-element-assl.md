---
title: "FontSize Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "FontSize Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "FontSize"
helpviewer_keywords: 
  - "FontSize element"
ms.assetid: 49f66a73-946a-4fbd-9749-a3ca1b717ff3
author: minewiskan
ms.author: owend
manager: craigg
---
# FontSize Element (ASSL)
  Describes font-related display characteristics of the [CalculationProperty](../objects/calculationproperty-element-assl.md) or [Measure](../objects/measure-element-assl.md) parent element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or Measure -->  
   ...  
   <FontSize>...</FontSize>  
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
|Parent elements|[CalculationProperty](../objects/calculationproperty-element-assl.md), [Measure](../objects/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `FontSize` property contains a Multidimensional Expressions (MDX) expression and applies to `CalculationProperty` elements that have a [CalculationType](calculationtype-element-assl.md) of *Member* or *Cells*.  
  
 The elements that correspond to the parents of `FontSize` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../collections/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../objects/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../collections/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
