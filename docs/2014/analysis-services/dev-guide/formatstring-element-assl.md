---
title: "FormatString Element (ASSL) | Microsoft Docs"
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
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# FormatString Element (ASSL)
  Describes the display format for a [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md) element or a [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md) element.  
  
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
|Parent elements|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `FormatString` property contains a Multidimensional Expressions (MDX) expression. In the case of `CalculationProperty` elements, it applies to elements with a [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md) of *Member* or *Cells*.  
  
 The elements that correspond to the parents of `FormatString` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  