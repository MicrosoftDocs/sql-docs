---
title: "FormatString Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "FormatString Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "FormatString"
helpviewer_keywords: 
  - "FormatString element"
ms.assetid: 7b996221-936e-4f36-a3a8-676eb9869c55
author: minewiskan
ms.author: owend
manager: craigg
---
# FormatString Element (ASSL)
  Describes the display format for a [CalculationProperty](../objects/calculationproperty-element-assl.md) element or a [Measure](../objects/measure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or Measure -->  
   ...  
   <FormatString>...</FormatString>  
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
 The `FormatString` property contains a Multidimensional Expressions (MDX) expression. In the case of `CalculationProperty` elements, it applies to elements with a [CalculationType](calculationtype-element-assl.md) of *Member* or *Cells*.  
  
 The elements that correspond to the parents of `FormatString` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../collections/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../objects/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../collections/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
